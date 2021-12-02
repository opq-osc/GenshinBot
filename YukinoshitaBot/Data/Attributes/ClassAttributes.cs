// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class YukinoControllerAttribute: Attribute
    {
        /// <summary>
        /// 优先级，越小优先级越高
        /// </summary>
        public int Priority { get; set; } = int.MaxValue;

        /// <summary>
        /// 处理模式
        /// </summary>
        public HandleMode Mode { get; set; } = HandleMode.Break;
    }

    /// <summary>
    /// 定义为YukinoshitaController
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class YukinoRouteAttribute : YukinoControllerAttribute
    {
        /// <summary>
        /// 匹配的指令
        /// </summary>
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// 指令识别方式
        /// </summary>
        public CommandMatchMethod MatchMethod { get; set; } = CommandMatchMethod.Strict;

        public bool CheckMatch(string msg) => MatchMethod switch
        {
            CommandMatchMethod.Strict => msg == Command,
            CommandMatchMethod.StartWith => msg.StartsWith(Command),
            CommandMatchMethod.Regex => Regex.IsMatch(msg, Command),
            _ => false
        };
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RegexRouteAttribute : YukinoControllerAttribute
    {
        private Regex regex = null!;
        public string Regex {get => regex.ToString(); init { regex = new Regex(value); } }


        public bool TryMatch(string input, out Dictionary<string, string> matchPairs)
        {
            matchPairs = new();
            var match = this.regex.Match(input);
            if (match.Success == false)
            {
                return false;
            }
            var groups = match.Groups.Values;
            foreach (var group in groups)
            {
                if(string.IsNullOrEmpty(group.Name) || string.IsNullOrEmpty(group.Value)) {
                    continue;
                }
                matchPairs.Add(group.Name, group.Value);
            }
            return true;
        }
    }
}
