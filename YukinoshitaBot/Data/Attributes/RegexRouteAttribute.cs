// <copyright file="YukinoshitaControllerAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using YukinoshitaBot.Extensions;

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
            return regex.TryGetMatchPairs(msg, out matchPairs);
        }
    }
}
