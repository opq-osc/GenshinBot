// <copyright file="VoiceMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoiceMessage"/> class.
        /// </summary>
        /// <param name="sender">发送者信息</param>
        /// <param name="fileUrl">文件URL</param>
        public VoiceMessage(SenderInfo sender, string fileUrl) : base(sender)
        {
            this.Url = fileUrl;
        }

        /// <summary>
        /// 语音文件URL
        /// </summary>
        public string Url { get; set; }
    }
}
