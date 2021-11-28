using System.Text.Json.Serialization;

namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// 游戏账号数据
    /// </summary>
    public class GameAccount
    {
        [JsonPropertyName("game_id")]
        public int GameId { get; set; }

        [JsonPropertyName("game_role_id")]
        public string GameUid { get; set; } = string.Empty;

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; } = string.Empty;
    }

    public class GameAccounts
    {
        [JsonPropertyName("list")]
        public IEnumerable<GameAccount> List { get; set; } = null!;
    }
}
