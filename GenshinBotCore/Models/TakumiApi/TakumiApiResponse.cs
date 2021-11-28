using System.Text.Json.Serialization;

namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// 通用API返回结构
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    public class TakumiApiResponse<T> : IApiResponse<T>
    {
        [JsonPropertyName("retcode")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public T? Data { get; set; } = default;

        [JsonIgnore]
        public bool IsSuccess => Code == 0;

        [JsonIgnore]
        public Type ApiDataType => typeof(T);

        [JsonIgnore]
        public T? Payload => Data;
    }
}
