using System.Text.Json.Serialization;

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
