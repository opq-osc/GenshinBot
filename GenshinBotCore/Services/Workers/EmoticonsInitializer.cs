using Flurl.Http;
using GenshinBotCore.Extensions;
using GenshinBotCore.Models.TakumiApi;
using GenshinBotCore.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services.Workers
{
    public class EmoticonsInitializer : BackgroundService
    {
        public EmoticonsInitializer(ILogger<EmoticonsInitializer> logger, EmoticonSet emoticonSet) : base()
        {
            this.logger = logger;
            this.emoticonSet = emoticonSet;
        }

        private readonly ILogger logger;
        private readonly EmoticonSet emoticonSet;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var emoticonUrl = "https://bbs-api.mihoyo.com/misc/api/emoticon_set";
            var response = await emoticonUrl.GetAsync();

            if (response.StatusCode != 200)
            {
                logger.LogError("Get emoticons failed: {code}", response.StatusCode);
                return;
            }

            var emoticonInfo = await response.CastTo<TakumiApiResponse<Models.MihoyoBBS.EmoticonSet>>();

            if (!emoticonInfo?.IsSuccess ?? false || emoticonInfo?.Payload is null)
            {
                logger.LogError("Get emoticons failed: {msg}", emoticonInfo?.Message);
                return;
            }

            var genshinEmoticonSets = emoticonInfo?.Payload?.Catalogs.Where(e => e.Name.Contains("原神")) ?? throw new Exception();
            foreach(var set in genshinEmoticonSets)
            {
                emoticonSet.Emoticons = emoticonSet.Emoticons.Concat(set.Emoticons);
            }

            logger.LogInformation("got {count} emoticons.", emoticonSet.Emoticons.Count());
        }
    }
}
