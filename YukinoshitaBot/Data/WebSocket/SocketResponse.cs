// <copyright file="SocketResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.WebSocket
{
    /// <summary>
    /// SocketIO数据返回类型
    /// </summary>
    /// <typeparam name="T">消息类型</typeparam>
    public class SocketResponse<T>
    {
        /// <summary>
        /// 绑定到Socket的QQ号
        /// </summary>
        public long CurrentQQ { get; set; }

        /// <summary>
        /// 数据包
        /// </summary>
        public SocketPacket<T>? CurrentPacket { get; set; }
    }
}
