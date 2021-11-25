using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenshinBotCore.Models.MihoyoAccount
{
    public class Account
    {
        [JsonPropertyName("account_id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("weblogin_token")]
        public string Token { get; set; } = null!;
    }
}
