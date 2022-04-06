using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenshinBotCore.Models.TakumiApi
{
    public class DetailedAvatar : GenshinAvatar
    {
        [JsonPropertyName("icon")]
        public string IconUrl { get; set; } = null!;

        [JsonPropertyName("image")]
        public string ImageUrl { get; set; } = null!;

        [JsonPropertyName("reliquaries")]
        public IList<Reliquary> Reliquaries { get; set; } = null!;

        [JsonPropertyName("weapon")]
        public Weapon Weapon { get; set; } = null!;

        [JsonPropertyName("constellations")]
        public IList<Constellation> Constellations { get; set; } = null!;
    }

    public class Constellation
    {
        [JsonPropertyName("effect")]
        public string Effect { get; set; } = null!;

        [JsonPropertyName("icon")]
        public string IconUrl { get;set; } = null!;

        [JsonPropertyName("is_actived")]
        public bool IsActived { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("pos")]
        public int Position { get; set; }
    }

    public class Weapon
    {
        /// <summary>
        /// 精炼等级
        /// </summary>
        [JsonPropertyName("affix_level")]
        public int AffixLevel { get; set; }

        [JsonPropertyName("desc")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("icon")]
        public string IconUrl { get; set; } = null!;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 强化等级
        /// </summary>
        [JsonPropertyName("promote_level")]
        public int PromoteLevel { get; set; }

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("type")]
        public int TypeId { get; set; }

        [JsonPropertyName("type_name")]
        public string Type { get; set; } = null!;
    }

    public class Reliquary
    {
        [JsonPropertyName("icon")]
        public string IconUrl { get; set; } = null!;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("pos")]
        public int Position { get; set; }

        [JsonPropertyName("rarity")]
        public int Rarity { get; set; }

        [JsonPropertyName("set")]
        public ReliquarySet Set { get; set; } = null!;
    }

    public class ReliquarySet
    {
        [JsonPropertyName("affixes")]
        public IList<ReliquarySetAffix> Affixes { get; set; } = null!;

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }

    public class ReliquarySetAffix
    {
        [JsonPropertyName("activation_number")]
        public int ActivationNum { get; set; }

        [JsonPropertyName("effect")]
        public string Effect { get; set; } = null!;
    }
}
