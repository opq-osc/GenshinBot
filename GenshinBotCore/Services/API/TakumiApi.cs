using Flurl.Http;
using GenshinBotCore.Configs;
using GenshinBotCore.Extensions;
using GenshinBotCore.Models;
using GenshinBotCore.Models.TakumiApi;
using Index = GenshinBotCore.Models.TakumiApi.Index;

namespace GenshinBotCore.Services
{
    public class TakumiApi : ITakumiApi
    {
        public TakumiApi(IUserManager userManager, ISecretHeaderGenerator secretHeaderGenerator, Action<TakumiApiConfiguration> configuration)
        {
            this.configuration = new();
            configuration.Invoke(this.configuration);
            this.secretHeaderGenerator = secretHeaderGenerator;
            this.userManager = userManager;
        }

        private readonly TakumiApiConfiguration configuration;
        private readonly ISecretHeaderGenerator secretHeaderGenerator;
        private readonly IUserManager userManager;

        ///<inheritdoc/>
        public async Task<IApiResponse<DailyNote>> GetDailyNoteAsync(string roleId, string serverId)
        {
            var requestParams = new Dictionary<string, string>
            {
                { "role_id", roleId },
                { "server", serverId },
            };
            var queryString = requestParams.ToQueryString();
            var user = userManager.GetUserByGenshinUid(roleId);

            if (user is null) throw new InvalidOperationException("找不到指定用户");

            var secretHeader = secretHeaderGenerator.GenerateSecretHeader(user.Id, queryString);

            var response = await (configuration.BaseUrl + configuration.DailyNoteUrl)
                                 .HasQuery(requestParams)
                                 .WithHeaders(secretHeader)
                                 .GetAsync()
                                 .ConfigureAwait(false);
            if (response.StatusCode != 200) throw new HttpRequestException();

            return await response.CastTo<TakumiApiResponse<DailyNote>>().ConfigureAwait(false) ?? throw new InvalidCastException();
        }

        ///<inheritdoc/>
        public async Task<IApiResponse<GameAccounts>> GetGameAccounts(string uid)
        {
            var requsetParams = new Dictionary<string, string>
            {
                { "uid", uid }
            };
            var queryString = requsetParams.ToQueryString();
            var user = userManager.GetUserByMihoyoId(uid);

            if (user is null) throw new InvalidOperationException("找不到指定用户");

            var secretHeader = secretHeaderGenerator.GenerateSecretHeader(user.Id, queryString);

            var response = await (configuration.BaseUrl + configuration.GameAccountsUrl)
                                 .HasQuery(requsetParams)
                                 .WithHeaders(secretHeader)
                                 .GetAsync()
                                 .ConfigureAwait(false);
            if (response.StatusCode != 200) throw new HttpRequestException();

            return await response.CastTo<TakumiApiResponse<GameAccounts>>().ConfigureAwait(false) ?? throw new InvalidCastException();
        }

        ///<inheritdoc/>
        public async Task<IApiResponse<Index>> GetIndexAsync(string roleId, string serverId)
        {
            var requestParams = new Dictionary<string, string>
            {
                { "role_id", roleId },
                { "server", serverId },
            };
            var queryString = requestParams.ToQueryString();
            var user = userManager.GetUserByGenshinUid(roleId);

            if (user is null) throw new InvalidOperationException("找不到指定用户");

            var secretHeader = secretHeaderGenerator.GenerateSecretHeader(user.Id, queryString);

            var response = await (configuration.BaseUrl + configuration.IndexUrl)
                                 .HasQuery(requestParams)
                                 .WithHeaders(secretHeader)
                                 .GetAsync()
                                 .ConfigureAwait(false);
            if (response.StatusCode != 200) throw new HttpRequestException();

            return await response.CastTo<TakumiApiResponse<Index>>().ConfigureAwait(false) ?? throw new InvalidCastException();
        }

        ///<inheritdoc/>
        public async Task<IApiResponse<MultiToken>> GetMultiTokenByLoginTicketAsync(string loginTicket, string uid, int tokenType)
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

            return await response.CastTo<TakumiApiResponse<MultiToken>>().ConfigureAwait(false) ?? throw new InvalidCastException();
        }

        ///<inheritdoc/>
        public Task<IApiResponse<SpiralAbyss>> GetSpiralAbyssAsync(string roleId, string serverId, int scheduleType)
        {
            throw new NotImplementedException();
        }
    }
}
