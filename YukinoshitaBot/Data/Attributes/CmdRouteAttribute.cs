// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// [CmdRoute("测试_{must}_[option]")] <br/>
    /// _      ->  \s* <br/>
    /// {key}  ->  (?&lt;key&gt;.+?) <br/>
    /// [key]  ->  (?&lt;key&gt;.*?)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CmdRouteAttribute : RegexRouteAttribute
    {
        public CmdRouteAttribute() { }

        public CmdRouteAttribute(string cmd)
        {
            this.Command = cmd;
        }

        public bool AllowRedundancy { get; set; } = true;

        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            var regex = this.CompileCommand();
            return TryRegexMatch(msg, out matchPairs, regex);
        }

        private Regex CompileCommand()
        {
            var sb = new StringBuilder();
            var head = 0;
            var cur = -1;
            while (++cur < this.Command.Length)
            {
                var c = this.Command[cur];
                if ("}]".Contains(c))
                {
                    throw new ArgumentException($"unpaired '{c}' found!");
                }
                else if (c == '{')
                {
                    sb.Append(Regex.Escape(this.Command[head..cur]));
                    head = cur++;
                    while (cur < this.Command.Length)
                    {
                        c = this.Command[cur];
                        // TODO 这部分单独写一个方法 判断是不是正确的标识符
                        if ((c >= 'a') && (c <= 'z') || (c >= 'A') && (c <= 'Z') || (c >= '0') && (c <= '9'))
                        {
                            cur++;
                            continue;
                        }
                        else if (c == '}')
                        {
                            // head 指向 '[' cur 指向 ']'
                            // [(head + 1)..cur] 选取 [] 之间的内容
                            var key = this.Command[(head + 1)..cur];
                            sb.Append(@$"(?<{key}>\S+)");
                            head = cur + 1;
                            break;
                        }
                        else
                        {
                            throw new ArgumentException($"character '{c}' found! only character which can be used in identifier are allowed");
                        }
                    }
                }
                else if (c == '[')
                {
                    sb.Append(Regex.Escape(this.Command[head..cur]));
                    head = cur++;
                    while (cur < this.Command.Length)
                    {
                        c = this.Command[cur];
                        if ((c >= 'a') && (c <= 'z') || (c >= 'A') && (c <= 'Z') || (c >= '0') && (c <= '9'))
                        {
                            cur++;
                            continue;
                        }
                        else if (c == ']')
                        {
                            // head 指向 '{' cur 指向 '}'
                            // [(head + 1)..cur] 选取 {} 之间的内容
                            var key = this.Command[(head + 1)..cur];
                            sb.Append(@$"(?<{key}>\S*)");
                            head = cur + 1;
                            break;
                        }
                        else
                        {
                            throw new ArgumentException($"character '{c}' found! only character which can be used in identifier are allowed");
                        }
                    }
                }
                else if (c == '_')
                {
                    sb.Append(Regex.Escape(this.Command[head..cur]));
                    sb.Append(@"\s*");
                    head = cur + 1;
                }
            }
            sb.Append(this.Command[head..cur]);
            if (!this.AllowRedundancy)
            {
                sb.Append('$');
            }
            return new Regex(sb.ToString());
        }
    }
}
