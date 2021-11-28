using System.Text.Json.Serialization;

namespace GenshinBotCore.Models.MihoyoAccount
{
    public class MihoyoApiResponse<T> : IApiResponse<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Code == 200;

        [JsonIgnore]
        public Type ApiDataType => typeof(T);

        [JsonIgnore]
        public T? Payload => Data;
    }
}
