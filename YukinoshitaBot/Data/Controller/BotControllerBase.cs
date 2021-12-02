namespace YukinoshitaBot.Data.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using YukinoshitaBot.Data.Event;

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
        public Dictionary<string,string> MatchPairs { get; set; } = null!;
    }
}
