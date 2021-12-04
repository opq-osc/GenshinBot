// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 当输入的指令以设置的命令为起始时匹配
    /// Command = "Hello"
    /// "Hello" 匹配
    /// "Hello world" 匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class StartRouteAttribute : YukinoRouteAttribute
    {
        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            matchPairs = new();
            return msg.StartsWith(Command);
        }
    }
}
