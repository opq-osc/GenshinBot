using System.Text.Json.Serialization;

namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// 首页信息
    /// </summary>
    public class Index
    {
        [JsonPropertyName("avatars")]
        public IEnumerable<GenshinAvatar> Avatars { get; set; } = null!;

        [JsonPropertyName("homes")]
        public IEnumerable<GenshinHome> Homes { get; set; } = null!;

        [JsonPropertyName("stats")]
        public GenshinCharacterStates States { get; set; } = null!;

        [JsonPropertyName("world_explorations")]
        public IEnumerable<WordExploration> WordExplorations { get; set; } = null!;
    }

    /// <summary>
    /// 角色信息
    /// </summary>
    public class GenshinAvatar
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 头像URL
        /// </summary>
        [JsonPropertyName("image")]
        public string AvatarUrl { get; set; } = null!;

        /// <summary>
        /// 等级
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }

        /// <summary>
        /// 稀有度
        /// </summary>
        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        /// <summary>
        /// 元素
        /// </summary>
        [JsonPropertyName("element")]
        public string Element { get; set; } = null!;

        /// <summary>
        /// 好感度
        /// </summary>
        [JsonPropertyName("fetter")]
        public int Fetter { get; set; }

        /// <summary>
        /// 命座激活数
        /// </summary>
        [JsonPropertyName("actived_constellation_num")]
        public int Constellation { get; set; }
    }

    /// <summary>
    /// 尘歌壶
    /// </summary>
    public class GenshinHome
    {
        [JsonPropertyName("comfort_level_icon")]
        public string LevelIconUrl { get; set; } = string.Empty;

        [JsonPropertyName("comfort_level_name")]
        public string LevelName { get; set; } = string.Empty;

        [JsonPropertyName("comfort_num")]
        public int ComfortNum { get; set; }

        [JsonPropertyName("icon")]
        public string BackgroundUrl { get; set; } = string.Empty;

        [JsonPropertyName("item_num")]
        public int ItemNum { get; set; }

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("visit_num")]
        public int VisitNum { get; set; }
    }

    /// <summary>
    /// 原神信息总览
    /// </summary>
    public class GenshinCharacterStates
    {
        /// <summary>
        /// 成就数
        /// </summary>
        [JsonPropertyName("achievement_number")]
        public int AchievementNum { get; set; }

        /// <summary>
        /// 活跃天数
        /// </summary>
        [JsonPropertyName("active_day_number")]
        public int ActiveDayNum { get; set; }

        /// <summary>
        /// 风神瞳数量
        /// </summary>
        [JsonPropertyName("anemoculus_number")]
        public int AnemoculusNum { get; set; }

        /// <summary>
        /// 已获取角色数
        /// </summary>
        [JsonPropertyName("avatar_number")]
        public int AvatarNum { get; set; }

        /// <summary>
        /// 已解锁秘境数量
        /// </summary>
        [JsonPropertyName("domain_number")]
        public int DomainNum { get; set; }

        /// <summary>
        /// 雷神瞳数量
        /// </summary>
        [JsonPropertyName("electroculus_number")]
        public int ElectroculusNum { get; set; }

        /// <summary>
        /// 岩神瞳数量
        /// </summary>
        [JsonPropertyName("geoculus_number")]
        public int GeoculusNum { get; set; }

        /// <summary>
        /// 深渊最高等级
        /// </summary>
        [JsonPropertyName("spiral_abyss")]
        public string SpiralAbyss { get; set; } = string.Empty;

        /// <summary>
        /// 已解锁路径点数量
        /// </summary>
        [JsonPropertyName("way_point_number")]
        public int WayPointNumber { get; set; }

        /// <summary>
        /// 普通宝箱数
        /// </summary>
        [JsonPropertyName("common_chest_number")]
        public int CommonChestNum { get; set; }

        /// <summary>
        /// 精致宝箱数
        /// </summary>
        [JsonPropertyName("exquisite_chest_number")]
        public int ExquisiteChestNum { get; set; }

        /// <summary>
        /// 珍贵宝箱数
        /// </summary>
        [JsonPropertyName("pricious_chest_number")]
        public int PriciousChestNum { get; set; }

        /// <summary>
        /// 华丽宝箱数
        /// </summary>
        [JsonPropertyName("luxurious_chest_number")]
        public int LuxuriousChestNum { get; set; }

        /// <summary>
        /// 奇馈宝箱数
        /// </summary>
        [JsonPropertyName("magic_chest_number")]
        public int MagicChestNum { get; set; }
    }

    /// <summary>
    /// 世界探索信息
    /// </summary>
    public class WordExploration
    {
        public string Name { get; set; } = null!;

        [JsonPropertyName("exploration_percentage")]
        public int Percentage { get; set; }

        [JsonPropertyName("icon")]
        public string IconUrl { get; set; } = string.Empty;

        public IEnumerable<Offering>? Offerings { get; set; }
    }

    public class Offering
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
    }
}
