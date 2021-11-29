using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenshinBotCore.Models.MihoyoBBS
{
    public class EmoticonSet
    {
        [JsonPropertyName("list")]
        public IEnumerable<EmoticonCatalog> Catalogs { get; set; } = null!;
    }

    public class EmoticonCatalog
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("list")]
        public IEnumerable<Emoticon> Emoticons { get; set; } = null!;
    }

    public class Emoticon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("icon")]
        public string Url { get; set; } = null!;
    }
}
