using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Extensions
{
    public static class UrlExtensions
    {
        public static string HasQuery(this string baseUrl, IDictionary<string, string> query)
        {
            var queryString = string.Join('&', query.Select(x => $"{x.Key}={x.Value}"));
            return baseUrl + '?' + queryString;
        }

        public static string ToQueryString(this IDictionary<string, string> queryDictionary)
            => string.Join('&', queryDictionary.Select(x => $"{x.Key}={x.Value}"));
    }
}
