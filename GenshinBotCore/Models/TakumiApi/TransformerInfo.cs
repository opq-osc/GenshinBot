using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenshinBotCore.Models.TakumiApi
{
    public class TransformerInfo
    {
        [JsonPropertyName("obtained")]
        public bool Obtained { get; set; } 

        [JsonPropertyName("recovery_time")] 
        public TimeStruct? RecoveryTime { get; set; } 
    }

    public class TimeStruct
    {
        public int Day { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int Second { get; set; }

        [JsonPropertyName("reached")]
        public bool Reached { get; set; }

        public override string ToString()
        {
            return Reached ? "已就绪" : $"{Day * 24 + Hour}:{Minute}:{Second}";
        }
    }
}
