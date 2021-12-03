// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
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
    public abstract class YukinoControllerAttribute : Attribute
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

    /// <summary>
    /// 当输入的指令与设置指令完全一致时才匹配
    /// Command = "Hello"
    /// "Hello" 匹配
    /// "Hello world" 不匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class StrictRouteAttribute : YukinoControllerAttribute
    {
        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            matchPairs = new();
            return msg == Command;
        }
    }

    /// <summary>
    /// 当输入的指令以设置的命令为起始时匹配
    /// Command = "Hello"
    /// "Hello" 匹配
    /// "Hello world" 匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class StartRouteAttribute : YukinoControllerAttribute
    {
        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            matchPairs = new();
            return msg.StartsWith(Command);
        }
    }

    /// <summary>
    /// 使用正则进行匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegexRouteAttribute : YukinoControllerAttribute
    {
        protected Regex regex = null!;

        protected string rawCmd = null!;

        public new string Command
        {
            get => rawCmd;
            init
            {
                rawCmd = value;
                regex = new Regex(value);
            }
        }

        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            matchPairs = new();
            var match = this.regex.Match(msg);
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

        public new string Command
        {
            get => rawCmd;
            init
            {
                rawCmd = value;
                var matchStr = Regex.Replace(Regex.Replace(rawCmd, @"{(.+?)}", @"(?<$1>\S+)"), @"_", @"\s+");
                // TODO 由用户决定是否匹配结尾字符 直接写另一个属性不能确保设置的时候存在
                regex = new Regex(@$"^{matchStr}");
            }
        }
    }
}
