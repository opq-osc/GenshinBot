// <copyright file="MessageQueueScanner.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// 消息队列扫描线程
    /// </summary>
    public class MessageQueueScanner : BackgroundService
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly OpqApi opqApi;
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueueScanner"/> class.
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="configuration">配置</param>
        /// <param name="opqApi">OPQ服务</param>
        public MessageQueueScanner(ILogger<MessageQueueScanner> logger, IConfiguration configuration, OpqApi opqApi)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.opqApi = opqApi;
            this.httpClient = new HttpClient();
        }

        /// <inheritdoc/>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Message queue scanner starting...");

            var botConfig = this.configuration.GetSection("OpqApiSettings");
            var httpApi = botConfig.GetValue<string>("HttpApi");

            this.httpClient.BaseAddress = new Uri(httpApi);

            return base.StartAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(
                async () =>
                {
                    var msgQueueConfig = this.configuration.GetSection("MsgQueueSettings");
                    var delayValue = msgQueueConfig.GetValue<int>("DelayAfterSent");
                    this.logger.LogInformation("Message queue scanner started...");
                    foreach (var request in this.opqApi.MessageQueue.GetConsumingEnumerable(stoppingToken))
                    {
                        await this.httpClient.SendAsync(request, stoppingToken).ConfigureAwait(false);
                        await Task.Delay(delayValue, stoppingToken);
                    }
                }, stoppingToken);
        }
    }
}
