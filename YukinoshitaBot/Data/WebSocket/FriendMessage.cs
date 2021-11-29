// <copyright file="FriendMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.WebSocket
{
    /// <summary>
    /// 好友消息
    /// </summary>
    public class FriendMessage : MessageBase
    {
        /// <summary>
        /// 发送者QQ号
        /// </summary>
        public long FromUin { get; set; }

        /// <summary>
        /// 接收者QQ号
        /// </summary>
        public long ToUin { get; set; }

        /// <summary>
        /// 消息序列
        /// </summary>
        public int MsgSeq { get; set; }

        /// <summary>
        /// 发送者所在群的群号，仅<see cref="MessageBase.MsgType"/>为TempSessionMsg时有效
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// 红包信息
        /// </summary>
        public object? RedBaginfo { get; set; }
    }
}
