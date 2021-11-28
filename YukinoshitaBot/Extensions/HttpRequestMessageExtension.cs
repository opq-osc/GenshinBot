// <copyright file="HttpRequestMessageExtension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Extensions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using YukinoshitaBot.Services;

    /// <summary>
    /// <see cref="HttpRequestMessage"/>的拓展
    /// </summary>
    public static class HttpRequestMessageExtension
    {
        /// <summary>
        /// 将请求添加到队列
        /// </summary>
        /// <param name="httpRequest">Http请求</param>
        /// <param name="opqApi">OPQ接口对象</param>
        public static void AddToQueue(this HttpRequestMessage httpRequest, OpqApi opqApi)
        {
            opqApi.AddRequest(httpRequest);
        }

        /// <summary>
        /// 直接发送请求
        /// </summary>
        /// <param name="httpRequest">Http请求</param>
        /// <param name="httpClient">使用该<see cref="HttpClient"/>发送</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<HttpResponseMessage> SendAsync(this HttpRequestMessage httpRequest, HttpClient httpClient)
        {
            return await httpClient.SendAsync(httpRequest).ConfigureAwait(false);
        }
    }
}
