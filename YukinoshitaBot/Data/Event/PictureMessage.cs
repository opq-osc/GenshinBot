// <copyright file="PictureMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 图片消息
    /// </summary>
    public class PictureMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PictureMessage"/> class.
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="picUrls">图片URL</param>
        /// <param name="content">文本消息</param>
        public PictureMessage(SenderInfo sender, IEnumerable<string> picUrls, string content) : base(sender)
        {
            this.PictureUrls = picUrls;
            this.MessageType = MessageType.PictureMessage;
            this.Content = content;
            this.IsFlashPicture = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureMessage"/> class.
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="flashPicUrl">闪照URL</param>
        public PictureMessage(SenderInfo sender, string flashPicUrl) : base(sender)
        {
            this.MessageType = MessageType.PictureMessage;
            this.Content = string.Empty;
            this.PictureUrls = new List<string> { flashPicUrl };
            this.IsFlashPicture = true;
        }

        /// <summary>
        /// 图片URL
        /// </summary>
        public IEnumerable<string> PictureUrls { get; set; }

        /// <summary>
        /// 文本消息
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否闪照
        /// </summary>
        public bool IsFlashPicture { get; set; }

        /// <summary>
        /// 获取第一张图片的URL
        /// </summary>
        public string FirstPicture => this.PictureUrls.First();
    }
}
