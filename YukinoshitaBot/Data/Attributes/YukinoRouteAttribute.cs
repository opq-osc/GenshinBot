// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 定义为YukinoshitaController
    /// </summary>
    public abstract class YukinoRouteAttribute : Attribute
    {
        /// <summary>
        /// 匹配的指令
        /// </summary>
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// 优先级，越小优先级越高
        /// </summary>
        public int Priority { get; set; } = int.MaxValue;

        /// <summary>
        /// 处理模式
        /// </summary>
        public HandleMode Mode { get; set; } = HandleMode.Break;

        /// <summary>
        /// 超过则忽略此消息
        /// </summary>
        public int MaxLength { get; set; } = 40;

        /// <summary>
        /// 检验输入指令是否匹配控制器
        /// </summary>
        /// <param name="msg">输入的指令</param>
        /// <param name="matchPairs">得到的匹配键值对</param>
        /// <returns></returns>
        public abstract bool TryMatch(string msg, out Dictionary<string, string> matchPairs);

        /// <summary>
        /// 检验输入指令是否符合
        /// 排除空字符串和超长字符串
        /// </summary>
        /// <param name="content">输入指令</param>
        /// <returns></returns>
        public bool CheckLength(string content)
        {
            return !string.IsNullOrWhiteSpace(content) && content.Length <= MaxLength;
        }
    }
}
