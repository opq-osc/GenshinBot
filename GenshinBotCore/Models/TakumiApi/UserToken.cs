using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// Token数据
    /// </summary>
    public class UserToken
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("token")]
        public string Token { get; set; } = null!;
    }

    public class MultiToken
    {
        [JsonPropertyName("list")]
        public IEnumerable<UserToken> Tokens { get; set; } = null!;
    }
}
