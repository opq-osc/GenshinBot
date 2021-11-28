using System.Text.Json.Serialization;

namespace GenshinBotCore.Models.MihoyoAccount
{
    public class AccountData
    {
        [JsonPropertyName("account_info")]
        public Account Account { get; set; } = null!;
    }

    public class Account
    {
        [JsonPropertyName("account_id")]
        public long Id { get; set; }

        [JsonPropertyName("weblogin_token")]
        public string Token { get; set; } = null!;
    }
}
