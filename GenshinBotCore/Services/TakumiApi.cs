using Flurl.Http;
using GenshinBotCore.Configs;
using GenshinBotCore.Extensions;
using GenshinBotCore.Models;
using GenshinBotCore.Models.TakumiApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services
{
    public class TakumiApi : ITakumiApi
    {
        public TakumiApi(Action<TakumiApiConfiguration> configuration)
        {
            this.configuration = new();
            configuration.Invoke(this.configuration);
        }

        private readonly TakumiApiConfiguration configuration;

        public Task<IApiResponse<DailyNote>> GetDailyNoteAsync(string roleId, string serverId)
        {
            throw new NotImplementedException();
        }

        public Task<IApiResponse<GameAccounts>> GetGameAccounts(string uid)
        {
            throw new NotImplementedException();
        }

        public Task<IApiResponse<Models.TakumiApi.Index>> GetIndexAsync(string roleId, string serverId)
        {
            throw new NotImplementedException();
        }

        public async Task<IApiResponse<UserToken>> GetMultiTokenByLoginTicketAsync(string loginTicket, string uid, int tokenType)
        {
            var requestParams = new Dictionary<string, string>
            {
                { "login_ticket", loginTicket },
                { "token_types", tokenType.ToString() },
                { "uid", uid },
            };

            var response = await (configuration.BaseUrl + configuration.LoginTicketUrl).HasQuery(requestParams).GetAsync()
                .ConfigureAwait(false);
            if (response.StatusCode != 200) throw new HttpRequestException();

            return await response.CastTo<TakumiApiResponse<UserToken>>().ConfigureAwait(false) ?? throw new InvalidCastException();
        }

        public Task<IApiResponse<SpiralAbyss>> GetSpiralAbyssAsync(string roleId, string serverId, int scheduleType)
        {
            throw new NotImplementedException();
        }
    }
}
