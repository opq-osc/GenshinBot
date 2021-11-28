// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;

    /// <summary>
    /// 定义为YukinoshitaController
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class YukinoshitaControllerAttribute : Attribute
    {
        /// <summary>
        /// 匹配的指令
        /// </summary>
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// 指令识别方式
        /// </summary>
        public CommandMatchMethod MatchMethod { get; set; } = CommandMatchMethod.Strict;

        /// <summary>
        /// 优先级，越小优先级越高
        /// </summary>
        public int Priority { get; set; } = int.MaxValue;

        /// <summary>
        /// 处理模式
        /// </summary>
        public HandleMode Mode { get; set; } = HandleMode.Break;
    }
}
