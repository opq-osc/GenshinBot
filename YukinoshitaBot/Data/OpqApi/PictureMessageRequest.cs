// <copyright file="PictureMessageRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.OpqApi
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// 图片消息
    /// </summary>
    public class PictureMessageRequest : MessageRequestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PictureMessageRequest"/> class.
        /// </summary>
        /// <param name="base64EncodedImage">base64图片</param>
        public PictureMessageRequest(string base64EncodedImage) : base()
        {
            this.SendMsgType = "PicMsg";
            this.PicBase64Buf = base64EncodedImage;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureMessageRequest"/> class.
        /// </summary>
        /// <param name="picUri">图片URL</param>
        public PictureMessageRequest(Uri picUri) : base()
        {
            this.SendMsgType = "PicMsg";
            this.PicUrl = picUri.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PictureMessageRequest"/> class.
        /// </summary>
        /// <param name="localPicture">本地图片文件</param>
        public PictureMessageRequest(FileInfo localPicture) : base()
        {
            this.SendMsgType = "PicMsg";
            this.PicPath = localPicture.FullName;
        }

        /// <summary>
        /// 图片URL
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PicUrl { get; set; }

        /// <summary>
        /// 本地图片路径
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PicPath { get; set; }

        /// <summary>
        /// 图片base64
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PicBase64Buf { get; set; }

        /// <summary>
        /// 为图片消息添加文本内容
        /// </summary>
        /// <param name="content">文本消息</param>
        /// <returns>添加文本消息后的图文消息</returns>
        public PictureMessageRequest AddContent(string content)
        {
            this.Content = content;
            return this;
        }

        /// <inheritdoc/>
        public override HttpRequestMessage SendToFriend(long friendQQ)
        {
            var request = base.SendToFriend(friendQQ);
            request.Content = new StringContent(JsonSerializer.Serialize(this, typeof(PictureMessageRequest)), Encoding.UTF8, "application/json");

            return request;
        }

        /// <inheritdoc/>
        public override HttpRequestMessage SendToGroup(long groupId)
        {
            var request = base.SendToGroup(groupId);

            string content = JsonSerializer.Serialize(this, typeof(PictureMessageRequest));
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            return request;
        }

        /// <inheritdoc/>
        public override HttpRequestMessage SendToGroupMember(long userQQ, long groupId)
        {
            var request = base.SendToGroupMember(userQQ, groupId);
            request.Content = new StringContent(JsonSerializer.Serialize(this, typeof(PictureMessageRequest)), Encoding.UTF8, "application/json");

            return request;
        }
    }
}
