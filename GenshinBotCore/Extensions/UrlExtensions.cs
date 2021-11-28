namespace GenshinBotCore.Extensions
{
    public static class UrlExtensions
    {
        /// <summary>
        /// 添加查询字符串
        /// </summary>
        /// <param name="baseUrl">请求地址</param>
        /// <param name="query">查询参数字典</param>
        /// <returns></returns>
        public static string HasQuery(this string baseUrl, IDictionary<string, string> query)
        {
            var queryString = string.Join('&', query.Select(x => $"{x.Key}={x.Value}"));
            return baseUrl + '?' + queryString;
        }

        /// <summary>
        /// 转换为查询字符串
        /// </summary>
        /// <param name="queryDictionary">查询参数字典</param>
        /// <returns></returns>
        public static string ToQueryString(this IDictionary<string, string> queryDictionary)
            => string.Join('&', queryDictionary.Select(x => $"{x.Key}={x.Value}"));
    }
}
