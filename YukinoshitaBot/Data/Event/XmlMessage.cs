// <copyright file="XmlMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    using System.Xml.Linq;

    /// <summary>
    /// XML消息
    /// </summary>
    public class XmlMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlMessage"/> class.
        /// </summary>
        /// <param name="sender">发送者信息</param>
        /// <param name="xmlContent">xml文本</param>
        public XmlMessage(SenderInfo sender, string xmlContent) : base(sender)
        {
            this.RawContent = xmlContent;
        }

        /// <summary>
        /// 原始内容
        /// </summary>
        public string RawContent { get; set; }

        /// <summary>
        /// XmlObject，使用Linq解析
        /// </summary>
        public XElement XmlObject => XElement.Parse(this.RawContent);
    }
}
