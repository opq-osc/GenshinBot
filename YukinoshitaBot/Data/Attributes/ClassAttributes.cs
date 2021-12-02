﻿// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 定义为YukinoshitaController
    /// </summary>
    public class YukinoControllerAttribute : Attribute
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
    /// 基本的匹配能力
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

        public bool TryMatch(string msg) => MatchMethod switch
        {
            CommandMatchMethod.Strict => msg == Command,
            CommandMatchMethod.StartWith => msg.StartsWith(Command),
            CommandMatchMethod.Regex => Regex.IsMatch(msg, Command),
            _ => false
        };
    }

    /// <summary>
    /// 使用正则进行匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegexRouteAttribute : YukinoControllerAttribute
    {
        protected Regex regex = null!;
        public string Command { get => regex.ToString(); init { regex = new Regex(value); } }

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
                if (string.IsNullOrEmpty(group.Name) || string.IsNullOrEmpty(group.Value))
                {
                    continue;
                }
                matchPairs.Add(group.Name, group.Value);
            }
            return true;
        }
    }

    /// <summary>
    /// [CmdRoute("绑定_{username}_{password}"))
    /// "_" 将被替换为正则中的 whitespace 即 \s+
    /// "{key}" 将被替换为 (?&lt;key&gt;.+?)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CmdRouteAttribute : RegexRouteAttribute
    {
        public CmdRouteAttribute() { }

        public CmdRouteAttribute(string cmd)
        {
            this.Command = cmd;
        }

        private string cmd = null!;

        public new string Command
        {
            get => cmd;
            init
            {
                cmd = value;
                var matchStr = Regex.Replace(Regex.Replace(cmd, @"{(.+?)}", "(?<$1>.+?)"), @"_", @"\s+");
                regex = new Regex(@$"^{matchStr}$");
            }
        }
    }
}
