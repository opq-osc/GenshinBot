// <copyright file="GroupMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.WebSocket
{
    /// <summary>
    /// 群消息
    /// </summary>
    public class GroupMessage : MessageBase
    {
        /// <summary>
        /// 消息来源群号
        /// </summary>
        public long FromGroupId { get; set; }

        /// <summary>
        /// 消息来源群名称
        /// </summary>
        public string? FromGroupName { get; set; }

        /// <summary>
        /// 消息来源用户QQ号
        /// </summary>
        public long FromUserId { get; set; }

        /// <summary>
        /// 消息来源用户群昵称
        /// </summary>
        public string? FromNickName { get; set; }

        /// <summary>
        /// 消息时间(10位时间戳)
        /// </summary>
        public int MsgTime { get; set; }

        /// <summary>
        /// 消息序列号
        /// </summary>
        public int MsgSeq { get; set; }

        /// <summary>
        /// 消息随机数
        /// </summary>
        public long MsgRandom { get; set; }

        /// <summary>
        /// 红包信息
        /// </summary>
        public object? RedBaginfo { get; set; }
    }
}
