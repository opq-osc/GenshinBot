// <copyright file="TextMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessage"/> class.
        /// </summary>
        /// <param name="sender">发送者信息</param>
        /// <param name="content">文本消息</param>
        public TextMessage(SenderInfo sender, string content) : base(sender)
        {
            this.Content = content;
            this.MessageType = MessageType.TextMessage;
        }

        /// <summary>
        /// 文本消息
        /// </summary>
        public string Content { get; set; }
    }
}
