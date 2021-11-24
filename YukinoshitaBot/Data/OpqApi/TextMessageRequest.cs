// <copyright file="TextMessageRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.OpqApi
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;

    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextMessageRequest : MessageRequestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessageRequest"/> class.
        /// </summary>
        /// <param name="content">消息内容</param>
        public TextMessageRequest(string content) : base()
        {
            this.Content = content;
            this.SendMsgType = "TextMsg";
        }

        /// <inheritdoc/>
        public override HttpRequestMessage SendToFriend(long friendQQ)
        {
            var request = base.SendToFriend(friendQQ);
            request.Content = new StringContent(JsonSerializer.Serialize(this, typeof(TextMessageRequest)), Encoding.UTF8, "application/json");

            return request;
        }

        /// <inheritdoc/>
        public override HttpRequestMessage SendToGroup(long groupId)
        {
            var request = base.SendToGroup(groupId);
            request.Content = new StringContent(JsonSerializer.Serialize(this, typeof(TextMessageRequest)), Encoding.UTF8, "application/json");

            return request;
        }

        /// <inheritdoc/>
        public override HttpRequestMessage SendToGroupMember(long userQQ, long groupId)
        {
            var request = base.SendToGroupMember(userQQ, groupId);
            request.Content = new StringContent(JsonSerializer.Serialize(this, typeof(TextMessageRequest)), Encoding.UTF8, "application/json");

            return request;
        }
    }
}
