// <copyright file="MessageRequestBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.OpqApi
{
    using System.Net.Http;
    using System.Text.Json.Serialization;

    /// <summary>
    /// 用于发送消息的基类
    /// </summary>
    public abstract class MessageRequestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRequestBase"/> class.
        /// </summary>
        public MessageRequestBase()
        {
            this.HttpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "SendMsgV2");
            this.SendMsgType = string.Empty;
        }

        /// <summary>
        /// 接收者ID
        /// </summary>
        public long ToUserUid { get; set; }

        /// <summary>
        /// 发送类型
        /// </summary>
        public int SendToType { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string SendMsgType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }

        /// <summary>
        /// 临时消息的群号
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? GroupID { get; set; }

        /// <summary>
        /// 与对象关联的<see cref="System.Net.Http.HttpRequestMessage"/>
        /// </summary>
        [JsonIgnore]
        protected HttpRequestMessage HttpRequestMessage { get; set; }

        /// <summary>
        /// 发送给好友
        /// </summary>
        /// <param name="friendQQ">好友QQ</param>
        /// <returns>用于发送好友消息的<see cref="HttpRequestMessage"/></returns>
        public virtual HttpRequestMessage SendToFriend(long friendQQ)
        {
            this.SendToType = 1;
            this.ToUserUid = friendQQ;
            this.GroupID = null;

            return this.HttpRequestMessage;
        }

        /// <summary>
        /// 发送到群
        /// </summary>
        /// <param name="groupId">好友QQ</param>
        /// <returns>用于发送群消息的<see cref="HttpRequestMessage"/></returns>
        public virtual HttpRequestMessage SendToGroup(long groupId)
        {
            this.SendToType = 2;
            this.ToUserUid = groupId;
            this.GroupID = null;

            return this.HttpRequestMessage;
        }

        /// <summary>
        /// 发送到临时会话
        /// </summary>
        /// <param name="userQQ">目的QQ号</param>
        /// <param name="groupId">该用户所在群的群号</param>
        /// <returns>用于发送临时会话的<see cref="HttpRequestMessage"/></returns>
        public virtual HttpRequestMessage SendToGroupMember(long userQQ, long groupId)
        {
            this.SendToType = 3;
            this.ToUserUid = userQQ;
            this.GroupID = groupId;

            return this.HttpRequestMessage;
        }
    }
}
