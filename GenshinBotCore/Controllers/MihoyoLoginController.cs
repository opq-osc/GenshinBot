using GenshinBotCore.Services;
using System.Text;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace GenshinBotCore.Controllers
{
    [YukinoshitaController(Command = "原神登录", MatchMethod = CommandMatchMethod.StartWith, Priority = 1)]
    public class MihoyoLoginController : IBotController
    {
        public MihoyoLoginController(
            ILogger<MihoyoLoginController> logger,
            ITakumiApi takumiApi,
            IMihoyoApi mihoyoApi,
            IUserManager userManager,
            ISecretManager secretManager)
        {
            this.logger = logger;
            this.takumiApi = takumiApi;
            this.mihoyoApi = mihoyoApi;
            this.userManager = userManager;
            this.secretManager = secretManager;
        }

        private readonly ILogger logger;
        private readonly ITakumiApi takumiApi;
        private readonly IMihoyoApi mihoyoApi;
        private readonly IUserManager userManager;
        private readonly ISecretManager secretManager;

        public Task FriendPicMsgHandlerAsync(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        public async Task FriendTextMsgHandlerAsync(TextMessage message)
        {
            await Login(message);
        }

        public Task GroupPicMsgHandlerAsync(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        public async Task GroupTextMsgHandlerAsync(TextMessage message)
        {
            await Login(message);
        }

        private async Task Login(TextMessage message)
        {
            #region 指令验证
            logger.LogInformation("qq{qq}: {content}", message.SenderInfo.FromQQ, message.Content);
            var cmd = message.Content.Split(' ');
            if (cmd.Length < 3)
            {
                message.ReplyTextMsg("指令格式错误");
                return;
            }
            var phone = cmd[1];
            var code = cmd[2];
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(code))
            {
                message.ReplyTextMsg("不是正确的手机号和验证码");
                return;
            }
            #endregion
            try
            {
                // 第一步，短信验证码登录拿LoginTicket
                var loginResponse = await mihoyoApi.Login(phone, code);
                if (!loginResponse.IsSuccess || loginResponse.Payload is null)
                {
                    message.ReplyTextMsg($"米游社登陆失败, 短信验证码登陆失败");
                    return;
                }
                // 第二步，LoginTicket换取MultiToken
                var tokenResponse = await takumiApi.GetMultiTokenByLoginTicketAsync
                    (loginResponse.Payload.Token, loginResponse.Payload.Id.ToString(), 3);
                if (!tokenResponse.IsSuccess || tokenResponse.Payload is null)
                {
                    message.ReplyTextMsg("米游社登陆失败, 获取Token失败");
                    return;
                }
                // 第三步，存储用户信息
                var user = await userManager.UpdateUserAsync(new Entities.User
                {
                    QQ = message.SenderInfo.FromQQ ?? default,
                    MihoyoId = loginResponse.Payload.Id.ToString(),
                });
                if (user is null)
                {
                    message.ReplyTextMsg("米游社登陆失败, 用户创建失败");
                    return;
                }
                // 第四步，存储密钥信息
                secretManager.Bind(user.Id.ToString());
                secretManager.StorageSecret("ticket", loginResponse.Payload.Token);
                foreach (var token in tokenResponse.Payload.Tokens)
                {
                    secretManager.StorageSecret(token.Name, token.Token);
                }
                // 第五步，获取游戏账号信息
                var accountResponse = await takumiApi.GetGameAccounts(loginResponse.Payload.Id.ToString());
                if (!accountResponse.IsSuccess || accountResponse.Payload is null)
                {
                    message.ReplyTextMsg("米游社登陆失败, 账号信息获取失败");
                    return;
                }
                // 第六步，更新用户信息
                var genshinAccount = accountResponse.Payload.List.Where(n => n.GameId == 2).Single();
                user.GenshinUid = genshinAccount.GameUid;
                user.ServerId = genshinAccount.Region;
                user = await userManager.UpdateUserAsync(user);
                // 返回用户信息
                var sb = new StringBuilder();
                sb.AppendLine("账号信息：");
                sb.Append("昵称：").AppendLine(genshinAccount.Nickname);
                sb.Append("服务器：").AppendLine(genshinAccount.RegionName);
                sb.Append("Uid：").AppendLine(genshinAccount.GameUid);
                sb.Append("等级：").Append(genshinAccount.Level);

                message.ReplyTextMsg(sb.ToString());
            }
            catch (Exception ex)
            {
                message.ReplyTextMsg($"登陆失败: {ex.Message}");
                return;
            }
        }
    }
}
