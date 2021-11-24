// <copyright file="SocketPacket.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.WebSocket
{
    /// <summary>
    /// SocketIO数据包格式
    /// </summary>
    /// <typeparam name="T">消息类型</typeparam>
    public class SocketPacket<T>
    {
        /// <summary>
        /// 连接ID
        /// </summary>
        public string? WebConnId { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public T? Data { get; set; }
    }
}
