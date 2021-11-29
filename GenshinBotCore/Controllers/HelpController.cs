using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Attributes;
using YukinoshitaBot.Data.Controller;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Extensions;

namespace GenshinBotCore.Controllers
{
    [YukinoshitaController(Command = "帮助", MatchMethod = CommandMatchMethod.Strict, Priority = 5)]
    public class HelpController : IBotController
    {
        public Task FriendPicMsgHandlerAsync(PictureMessage message)
        {
            throw new NotImplementedException();
        }

        public Task FriendTextMsgHandlerAsync(TextMessage message)
        {
            message.ReplyTextMsg(Help());
            return Task.CompletedTask;
        }

        public Task GroupPicMsgHandlerAsync(PictureMessage message)
        {
            throw new NotImplementedException();
        }

        public Task GroupTextMsgHandlerAsync(TextMessage message)
        {
            message.ReplyTextMsg(Help());
            return Task.CompletedTask;
        }

        private string Help()
        {
            var sb = new StringBuilder();
            sb.AppendLine("--------原神查询机器人--------")
              .AppendLine("**不要中括号，指令参数空格隔开**")
              .AppendLine("-----------------------------")
              .AppendLine("1. 绑定账号：[原神登录 手机号 验证码]")
              .AppendLine("   https://bbs.mihoyo.com/ys 获取验证码")
              .AppendLine("2. 基本信息：[我的原神]")
              .AppendLine("3. 状态查询：[查询原神状态]")
              .AppendLine("4. 随机表情：[随机表情]")
              .AppendLine("5. 随机表情：[随机表情 筛选条件]")
              .AppendLine("    例：[随机表情 刻晴]")
              .AppendLine("-----------------------------")
              .AppendLine("项目开发中：https://github.com/opq-osc/GenshinBot")
              .AppendLine("求Star，求关注");

            return sb.ToString().TrimEnd('\r','\n');
        }
    }
}
