using GenshinBotCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace GenshinBotCore.Controllers
{
    [YukinoshitaController(Command = "查询原神状态", MatchMethod = CommandMatchMethod.Strict, Priority = 3)]
    public class DailyNoteController : IBotController
    {
        public DailyNoteController(
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

        public Task FriendPicMsgHandlerAsync(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        public Task FriendTextMsgHandlerAsync(TextMessage message)
        {
            return Task.CompletedTask;
        }

        public Task GroupPicMsgHandlerAsync(PictureMessage message)
        {
            return Task.CompletedTask;
        }

        public Task GroupTextMsgHandlerAsync(TextMessage message)
        {
            return Task.CompletedTask;
        }

        private async Task DailyNoteHandler(TextMessage message)
        {
            var user = userManager.GetUserByQQ(message.SenderInfo.FromQQ ?? default);
            if (user == null)
            {
                message.ReplyTextMsg("你还没有登陆过呢");
                return;
            }
            try
            {
                // TODO 服务器ID加入用户表
                var dailyNoteResponse = await takumiApi.GetDailyNoteAsync(user.GenshinUid, "cn_gf01");
                if (!dailyNoteResponse.IsSuccess || dailyNoteResponse.Payload is null)
                {
                    message.ReplyTextMsg("获取信息失败，请尝试重新登陆！");
                    return;
                }
                var dailyNoteInfo = dailyNoteResponse.Payload;

                var sb = new StringBuilder();
                sb.AppendLine("每日便签：");
                sb.AppendLine($"树脂：{dailyNoteInfo.CurrentResin}/{dailyNoteInfo.MaxResin}，{dailyNoteInfo.ResinRecoveryTime}后回满");
                sb.Append($"每日任务：{dailyNoteInfo.FinishedTaskNum}/{dailyNoteInfo.TotalTaskNum}，额外奖励");
                sb.AppendLine(dailyNoteInfo.IsExtraTaskRewardReceived ? "已领取" : "未领取");
                sb.AppendLine($"周本折扣次数：{dailyNoteInfo.RemainResinDiscountNum}/{dailyNoteInfo.RemainResinDiscountNum}");
                sb.AppendLine($"探索派遣：{dailyNoteInfo.CurrentExpeditionNum}/{dailyNoteInfo.MaxExpeditionNum}");
                sb.AppendLine($"-----探索派遣详细信息-----");
                foreach(var role in dailyNoteInfo.Expeditions)
                {
                    var name = Regex.Match(role.CharacterAvatar.ToString(), @"UI_AvatarIcon_Side_(.*?)\.png")
                                    .Groups[1].Value;
                    sb.Append($"角色：{name}，");
                    sb.AppendLine(role.RemainedTime == 0 ? "已完成" : $"剩余时间：{ TimeSpan.FromMinutes(role.RemainedTime):hh: mm}");
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
