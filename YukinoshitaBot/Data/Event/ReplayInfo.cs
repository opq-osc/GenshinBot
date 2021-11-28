// <copyright file="ReplayInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    public record ReplayInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplayInfo"/> class.
        /// </summary>
        public ReplayInfo()
        {
            this.ReplayContent = string.Empty;
            this.SrcContent = string.Empty;
        }

        /// <summary>
        /// 回复的消息的序列号，仅当消息为回复消息时有效
        /// </summary>
        public int MsgSeq { get; set; }

        /// <summary>
        /// 回复消息的内容，仅当消息为回复消息时有效
        /// </summary>
        public string ReplayContent { get; set; }

        /// <summary>
        /// 回复的原消息，仅当消息为回复消息时有效
        /// </summary>
        public string SrcContent { get; set; }
    }
}
