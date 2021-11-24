using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// 游戏角色数据
    /// </summary>
    public class GameRole
    {
        [JsonPropertyName("game_uid")]
        public string GenshinUid { get; set; } = string.Empty;

        [JsonPropertyName("nickname")]
        public string Nickname { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("region_name")]
        public string Region { get; set; } = string.Empty;
    }

    public record GameRoles(IEnumerable<GameRole> List);
}
