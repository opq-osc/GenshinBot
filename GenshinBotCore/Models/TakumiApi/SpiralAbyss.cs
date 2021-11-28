using System.Text.Json.Serialization;

// TODO 补充数据
namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// 深渊螺旋数据
    /// </summary>
    public class SpiralAbyss
    {
        [JsonPropertyName("schedule_id")]
        public int ScheduleId { get; set; }

        //  TODO 添加转换器
        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        //  TODO 添加转换器
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }

        [JsonPropertyName("total_battle_times")]
        public int TotalBattleTimes { get; set; }

        [JsonPropertyName("total_win_times")]
        public int TotalWinTimes { get; set; }

        [JsonPropertyName("max_floor")]
        public string MaxFloor { get; set; } = string.Empty;

        [JsonPropertyName("total_star")]
        public int TotalStars { get; set; }

        [JsonPropertyName("is_unlock")]
        public bool IsUnlock { get; set; } = false;

        [JsonPropertyName("reveal_rank")]
        public IEnumerable<dynamic> RevalRank { get; set; } = null!;

        [JsonPropertyName("damage_rank")]
        public IEnumerable<dynamic> DamageRank { get; set; } = null!;

        [JsonPropertyName("defeat_rank")]
        public IEnumerable<dynamic> DefeatRank { get; set; } = null!;

        [JsonPropertyName("take_damage_rank")]
        public IEnumerable<dynamic> TakeDamageRank { get; set; } = null!;

        [JsonPropertyName("normal_skill_rank")]
        public IEnumerable<dynamic> NormalSkillRank { get; set; } = null!;

        [JsonPropertyName("energy_skill_rank")]
        public IEnumerable<dynamic> SkillRank { get; set; } = null!;

        [JsonPropertyName("floors")]
        public IEnumerable<dynamic> Floors { get; set; } = null!;
    }
}
