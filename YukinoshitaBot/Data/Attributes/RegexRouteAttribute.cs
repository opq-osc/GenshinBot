// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    /// <summary>
    /// 使用正则进行匹配
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegexRouteAttribute : YukinoRouteAttribute
    {
        private Regex CompileCommand()
        {
            return new Regex(this.Command);
        }

        public override bool TryMatch(string msg, out Dictionary<string, string> matchPairs)
        {
            var regex = this.CompileCommand();
            return TryRegexMatch(msg, out matchPairs, regex);
        }

        protected bool TryRegexMatch(string msg, out Dictionary<string, string> matchPairs, Regex regex)
        {
            matchPairs = new();
            var match = regex.Match(msg);
            if (match.Success == false)
            {
                return false;
            }
            var groups = match.Groups.Values;
            foreach (var group in groups)
            {
                if (string.IsNullOrEmpty(group.Name) || string.IsNullOrWhiteSpace(group.Value))
                {
                    continue;
                }
                matchPairs.Add(group.Name, group.Value);
            }
            return true;
        }
    }
}
