// <copyright file="AtMessage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    using System.Collections.Generic;

    /// <summary>
    /// At信息
    /// </summary>
    public record AtInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AtInfo"/> class.
        /// </summary>
        public AtInfo()
        {
            this.AtUsers = new List<QQUser>();
            this.UserID = new List<long>();
        }

        /// <summary>
        /// At用户列表
        /// </summary>
        public List<QQUser> AtUsers { get; init; }

        /// <summary>
        /// At用户QQ号列表
        /// </summary>
        public List<long> UserID { get; init; }
    }
}
