// <copyright file="MessageBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.WebSocket
{
    using System.Text.Json;

    /// <summary>
    /// 消息基类，提供消息解析功能
    /// </summary>
    public abstract class MessageBase
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string? MsgType { get; set; }

        /// <summary>
        /// 解析Json格式的消息内容
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns>解析后类型的实例</returns>
        public T ParseContent<T>() where T : new()
        {
            if (string.IsNullOrEmpty(this.Content))
            {
                return new();
            }

            return JsonSerializer.Deserialize<T>(this.Content) ?? new T();
        }
    }
}