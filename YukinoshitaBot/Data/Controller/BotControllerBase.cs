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
        /// <inheritdoc/>
        public Message Message { get; set; } = null!;
    }
}
