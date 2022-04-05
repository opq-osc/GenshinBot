﻿using GenshinBotCore.Services;
using System.Text;
using System.Text.RegularExpressions;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace GenshinBotCore.Controllers
{
    [StrictRoute(Command = "查询原神状态", Priority = 3)]
    public class DailyNoteController : BotControllerBase
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

        [FriendText, GroupText]
        public async Task DailyNoteHandler()
        {
            var user = userManager.GetUserByQQ(this.Message.SenderInfo.FromQQ ?? default);
            if (user == null)
            {
                ReplyTextMsg("你还没有登陆过呢");
                return;
            }
            try
            {
                var dailyNoteResponse = await takumiApi.GetDailyNoteAsync(user.GenshinUid, user.ServerId);
                if (!dailyNoteResponse.IsSuccess || dailyNoteResponse.Payload is null)
                {
                    ReplyTextMsg("获取信息失败，请打开米游社中每日便签功能！");
                    return;
                }
                var dailyNoteInfo = dailyNoteResponse.Payload;

                var sb = new StringBuilder();
                sb.AppendLine("每日便签：");
                sb.AppendLine($"树脂：{dailyNoteInfo.CurrentResin}/{dailyNoteInfo.MaxResin}，{TimeSpan.FromSeconds(int.Parse(dailyNoteInfo.ResinRecoveryTime))}后回满");
                sb.Append($"每日任务：{dailyNoteInfo.FinishedTaskNum}/{dailyNoteInfo.TotalTaskNum}，额外奖励");
                sb.AppendLine(dailyNoteInfo.IsExtraTaskRewardReceived ? "已领取" : "未领取");
                sb.AppendLine($"周本折扣次数：{dailyNoteInfo.RemainResinDiscountNum}/{dailyNoteInfo.ResinDiscountNumLimit}");
                sb.AppendLine($"洞天宝钱：{dailyNoteInfo.CurrentHomeCoin}/{dailyNoteInfo.MaxHomeCoin}, {TimeSpan.FromSeconds(int.Parse(dailyNoteInfo.HomeCoinRecoveryTime))}后回满");
                sb.AppendLine($"探索派遣：{dailyNoteInfo.CurrentExpeditionNum}/{dailyNoteInfo.MaxExpeditionNum}");
                sb.AppendLine($"-----探索派遣详细信息-----");
                foreach (var role in dailyNoteInfo.Expeditions)
                {
                    var name = Regex.Match(role.CharacterAvatar.ToString(), @"UI_AvatarIcon_Side_(.*?)\.png")
                                    .Groups[1].Value;
                    sb.Append($"角色：{name}，");
                    sb.AppendLine(role.Status == "Finished" ? "已完成" : $"剩余时间：{TimeSpan.FromSeconds(int.Parse(role.RemainedTime))}");
                }

                ReplyTextMsg(sb.ToString().TrimEnd('\n', '\r'));
            }
            catch (Exception ex)
            {
                ReplyTextMsg($"获取信息失败：{ex.Message}");
            }
        }
    }
}
