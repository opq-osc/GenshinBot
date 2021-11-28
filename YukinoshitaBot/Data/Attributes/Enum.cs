// <copyright file="Enum.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
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
        Regex
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
        Pass
    }

    /// <summary>
    /// 会话类型
    /// </summary>
    public enum SessionType
    {
        /// <summary>
        /// 无会话状态
        /// </summary>
        None,

        /// <summary>
        /// 总是按照按QQ号进行会话
        /// </summary>
        Person,

        /// <summary>
        /// 群消息按照群号进行会话
        /// </summary>
        Group
    }
}
