namespace YukinoshitaBot.Data.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using YukinoshitaBot.Data.Event;
    using YukinoshitaBot.Extensions;

    /// <summary>
    /// 机器人消息类
    /// </summary>
    public abstract class BotControllerBase
    {
        /// <see cref="Message"/>
        public Message Message { get; set; } = null!;

        /// <summary>
        /// 匹配得到的参数键值对
        /// </summary>
        public Dictionary<string, string> MatchPairs { get; set; } = null!;

        /// <summary>
        /// 参数验证中的错误信息
        /// </summary>
        public List<string> ParamErrors { get; set; } = new();

        /// <summary>
        /// 参数验证是否成功
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// 当参数验证失败时返回错误消息  
        /// </summary>
        public void EmitErrorMsg()
        {
            if (ParamErrors.Any())
            {
                Message.ReplyTextMsg(string.Join('\n', ParamErrors));
            }
        }
    }
}
