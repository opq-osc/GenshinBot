using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GenshinBotCore.Extensions
{
    public static class FlurlExtensions
    {
        public static async Task<T?> CastTo<T>(this IFlurlResponse response)
        {
            return await JsonSerializer.DeserializeAsync<T>(await response.GetStreamAsync()).ConfigureAwait(false);
        }

        public static IFlurlRequest WithHeaders(this string url, IDictionary<string, string?> extraHeaders)
        {
            var dynamicObj = new ExpandoObject();
            var propertyDict = (ICollection<KeyValuePair<string, object?>>)dynamicObj;
            var boxedHeaders = extraHeaders.Select(x => new KeyValuePair<string, object?>(x.Key, x.Value));

            foreach (var header in boxedHeaders) propertyDict.Add(header);

            return url.WithHeaders(dynamicObj);
        }
    }
}
