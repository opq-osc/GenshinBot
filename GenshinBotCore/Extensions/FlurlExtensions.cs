using Flurl.Http;
using System.Dynamic;
using System.Text.Json;

namespace GenshinBotCore.Extensions
{
    public static class FlurlExtensions
    {
        /// <summary>
        /// 将响应映射到指定类型
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="response">响应</param>
        /// <returns></returns>
        public static async Task<T?> CastTo<T>(this IFlurlResponse response)
        {
            return await JsonSerializer.DeserializeAsync<T>(await response.GetStreamAsync()).ConfigureAwait(false);
        }

        /// <summary>
        /// 添加请求头
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="extraHeaders">请求头字典</param>
        /// <returns></returns>
        public static IFlurlRequest WithHeaders(this string url, IDictionary<string, string> extraHeaders)
        {
            var dynamicObj = new ExpandoObject();
            var propertyDict = (ICollection<KeyValuePair<string, object?>>)dynamicObj;
            var boxedHeaders = extraHeaders.Select(x => new KeyValuePair<string, object?>(x.Key, x.Value));

            foreach (var header in boxedHeaders) propertyDict.Add(header);

            return url.WithHeaders(dynamicObj);
        }
    }
}
