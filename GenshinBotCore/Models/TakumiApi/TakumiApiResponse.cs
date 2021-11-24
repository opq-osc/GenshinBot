using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenshinBotCore.Models.TakumiApi
{
    /// <summary>
    /// 通用API返回结构
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    public class TakumiApiResponse<T>
    {
        [JsonPropertyName("retcode")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("data")]
        public T? Data { get; set; } = default;
    }
}
