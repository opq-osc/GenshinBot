// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using YukinoshitaBot.Extensions;

    /// <summary>
    /// [CmdRoute("测试{requsite}{option?}{key:append}")] <br/>
    /// {key}  ->  (?&lt;key&gt;.+?)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CmdRouteAttribute : YukinoRouteAttribute
    {
        public CmdRouteAttribute() { }

        public CmdRouteAttribute(string cmd)
        {
            this.Command = cmd;
        }

        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            var regex = this.CompileCommand();
            return regex.TryGetMatchPairs(msg, out matchPairs);
        }

        // TODO 也许可以写个单独的命令解析类
        private Regex CompileCommand()
        {
            if (string.IsNullOrWhiteSpace(this.Command))
            {
                throw new ArgumentException("Command can't be Empty");
            }
            // TODO 写一个类用于控制 命令是否能进入队列 比如 可选参数就必须在最后
            var queue = new Queue<CmdUnit>();
            var head = 0;
            var cur = -1;
            while (++cur < this.Command.Length)
            {
                var c = this.Command[cur];
                if (c == '}')
                {
                    throw new ArgumentException($"unpaired '{c}' found!");
                }
                if (c == '{')
                {
                    queue.Enqueue(new CmdUnit
                    {
                        Type = CmdType.Plain,
                        Content = Regex.Escape(Command[head..cur])
                    });
                    // head -> '{' 的前一个字符
                    head = ++cur;
                    while (Command[cur] != '}')
                    {
                        cur++;
                        if (cur >= Command.Length)
                        {
                            throw new ArgumentException("found '{' but can't find another '}'.");
                        }
                    }
                    queue.Enqueue(ParseCommandUnit(Command[head..cur]));
                    // head -> '}' 的后一个字符
                    head = cur + 1;
                }
            }
            queue.Enqueue(new CmdUnit
            {
                Type = CmdType.Plain,
                Content = Regex.Escape(Command[head..cur])
            });

            
            return BuildCmdRegex(queue);
        }

        /// <summary>
        /// 构建用于命令匹配的正则表达式
        /// </summary>
        /// <param name="queue">命令单元列表</param>
        /// <returns></returns>
        private Regex BuildCmdRegex(Queue<CmdUnit> queue)
        {
            var sb = new StringBuilder();
            sb.Append('^');
            while (queue.TryDequeue(out var unit))
            {
                sb.Append(unit.Content);
            }
            sb.Append('$');
            return new Regex(sb.ToString());
        }

        /// <summary>
        /// 命令单元
        /// </summary>
        private class CmdUnit
        {
            internal CmdType Type { get; set; }
            internal string Content { get; set; } = null!;
        }

        /// <summary>
        /// 命令单元类型
        /// </summary>
        private enum CmdType
        {
            Plain,
            Requisite,
            Option,
        }

        private CmdUnit ParseCommandUnit(string input)
        {
            // TODO 标识符的正则表达式
            // TODO 更丰富的类型 append
            var match = Regex.Match(input, @"^(?<key>[a-zA-Z0-9@_]+)(?<append>:[a-z]+)?(?<option>\?)?$");
            if (!match.Success)
            {
                throw new ArgumentException($"\"{input}\" is not valid command.");
            }

            var key = match.Groups["key"].Value;
            if (!key.IsValidIdentifier())
            {
                throw new ArgumentException($"\"{key}\" is not valid identifier.");
            }

            var append = match.Groups["append"]?.Value;
            if (append != null)
            {
                // TODO 还没写
            }

            var option = match.Groups["option"];
            if (option != null)
            {
                return new CmdUnit
                {
                    Type = CmdType.Option,
                    Content = @$"(?:\s+(?<{key}>\S+))?",
                };
            }
            else
            {
                return new CmdUnit
                {
                    Type = CmdType.Requisite,
                    Content = @$"\s+(?<{key}>\S+)",
                };
            }
        }
    }
}
