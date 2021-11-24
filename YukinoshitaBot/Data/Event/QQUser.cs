﻿// <copyright file="QQUser.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    using System.Text.Json.Serialization;

    public record QQUser 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QQUser"/> class.
        /// </summary>
        public QQUser()
        {
            this.NickName = string.Empty;
            this.QQ = default;
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; init; }

        /// <summary>
        /// QQ号
        /// </summary>
        public long QQ { get; init; }
    }
}
