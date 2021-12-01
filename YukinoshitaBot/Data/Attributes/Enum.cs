// <copyright file="Enum.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 指令识别方式
    /// </summary>
    public enum CommandMatchMethod
    {
        /// <summary>
        /// 开头识别
        /// </summary>
        StartWith,

        /// <summary>
        /// 严格一致
        /// </summary>
        Strict,

        /// <summary>
        /// 正则匹配
        /// </summary>
        Regex,
    }

    /// <summary>
    /// 处理方式
    /// </summary>
    public enum HandleMode
    {
        /// <summary>
        /// 本方法处理完毕后中断处理链
        /// </summary>
        Break,

        /// <summary>
        /// 本方法处理完毕后继续将请求向之后的处理者传递
        /// </summary>
        Pass,
    }
}
