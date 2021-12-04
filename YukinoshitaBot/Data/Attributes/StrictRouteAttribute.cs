// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 当输入的指令与设置指令完全一致时才匹配
    /// Command = "Hello"
    /// "Hello" 匹配
    /// "Hello world" 不匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class StrictRouteAttribute : YukinoRouteAttribute
    {
        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            matchPairs = new();
            return msg == Command;
        }
    }
}
