// <copyright file="OpqApi.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Concurrent;
    using System.Net.Http;

    /// <summary>
    /// OPQ机器人Http队列
    /// </summary>
    public class OpqApi
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly long loginQQ;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpqApi"/> class.
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="configuration">配置</param>
        public OpqApi(ILogger<OpqApi> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;

            // 读取必要配置
            var botConfig = this.configuration.GetSection("OpqApiSettings");
            this.loginQQ = botConfig.GetValue<long>("LoginQQ");
            var msgQueueConfig = this.configuration.GetSection("MsgQueueSettings");
            var queueCapacity = msgQueueConfig.GetValue<int>("Capacity");

            this.MessageQueue = new BlockingCollection<HttpRequestMessage>(queueCapacity);
        }

        /// <summary>
        /// 存放待发送的Http请求
        /// </summary>
        public BlockingCollection<HttpRequestMessage> MessageQueue { get; init; }

        /// <summary>
        /// 向队列中添加一个请求
        /// </summary>
        /// <param name="request">要添加的请求</param>
        public void AddRequest(HttpRequestMessage request)
        {
            // 重写请求URL
            var method = request.RequestUri?.ToString() ?? string.Empty;
            request.RequestUri = new Uri($"?qq={this.loginQQ}&funcname={method}", UriKind.Relative);
            this.logger.LogDebug("Request added: {request}", request);
            this.MessageQueue.Add(request);
        }
    }
}
