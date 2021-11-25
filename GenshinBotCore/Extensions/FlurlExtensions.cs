using Flurl.Http;
using System;
using System.Collections.Generic;
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
    }
}
