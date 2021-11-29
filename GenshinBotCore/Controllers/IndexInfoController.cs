using GenshinBotCore.Services;
using System.Text;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace GenshinBotCore.Controllers
{
    [YukinoshitaController(Command = "我的原神", MatchMethod = CommandMatchMethod.Strict, Priority = 2)]
    public class IndexInfoController : IBotController
    {
        public IndexInfoController(
            ILogger<MihoyoLoginController> logger,
            ITakumiApi takumiApi,
            IUserManager userManager)
        {
            this.logger = logger;
            this.takumiApi = takumiApi;
            this.userManager = userManager;
        }

        private readonly ILogger logger;
        private readonly ITakumiApi takumiApi;
        private readonly IUserManager userManager;

        public async Task FriendTextMsgHandlerAsync(TextMessage message)
        {
            await IndexInfoHandler(message);
        }

        public async Task GroupTextMsgHandlerAsync(TextMessage message)
        {
            await IndexInfoHandler(message);
        }

        public Task FriendPicMsgHandlerAsync(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        public Task GroupPicMsgHandlerAsync(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        private async Task IndexInfoHandler(TextMessage message)
        {
            var user = userManager.GetUserByQQ(message.SenderInfo.FromQQ ?? default);
            if (user == null)
            {
                message.ReplyTextMsg("你还没有登陆过呢");
                return;
            }
            try
            {
                // 获取原神账号信息
                var accountResponse = await takumiApi.GetGameAccounts(user.MihoyoId);
                if (!accountResponse.IsSuccess || accountResponse.Payload is null)
                {
                    message.ReplyTextMsg("米游社登陆失败, 账号信息获取失败");
                    return;
                }
                var genshinAccountInfo = accountResponse.Payload.List.Where(n => n.GameId == 2).Single();
                // 获取首页信息
                var indexResponse = await takumiApi.GetIndexAsync(user.GenshinUid, user.ServerId);
                if (!indexResponse.IsSuccess || indexResponse.Payload is null)
                {
                    message.ReplyTextMsg("获取信息失败，你可能还没有注册米游社！");
                    return;
                }
                var indexInfo = indexResponse.Payload;
                var sb = new StringBuilder();

                sb.Append("昵称：").AppendLine(genshinAccountInfo.Nickname);
                sb.Append("等级：").AppendLine(genshinAccountInfo.Level.ToString());
                sb.Append("服务器：").AppendLine(genshinAccountInfo.RegionName);
                sb.Append("UID：").AppendLine(genshinAccountInfo.GameUid);
                sb.AppendLine("------------------");
                sb.Append("成就数量：").AppendLine(indexInfo.States.AchievementNum.ToString());
                sb.Append("深渊层数：").AppendLine(indexInfo.States.SpiralAbyss.ToString());
                sb.Append("已解锁路径点：").AppendLine(indexInfo.States.WayPointNumber.ToString());
                sb.Append("已获取角色数量：").AppendLine(indexInfo.States.AvatarNum.ToString());
                sb.AppendLine("-----五星角色-----");
                foreach (var character in indexInfo.Avatars.Where(c => c.Rarity == 5))
                {
                    sb.AppendLine($"{character.Name}：等级{character.Level}，命座{character.Constellation}，好感{character.Fetter}");
                }
                sb.AppendLine("-----四星角色-----");
                foreach (var character in indexInfo.Avatars.Where(c => c.Rarity == 4))
                {
                    sb.AppendLine($"{character.Name}：等级{character.Level}，命座{character.Constellation}，好感{character.Fetter}");
                }

                message.ReplyTextMsg(sb.ToString().TrimEnd('\n', '\r'));

            }
            catch (Exception ex)
            {
                message.ReplyTextMsg($"获取信息失败：{ex.Message}");
            }
        }
    }
}
