using Flurl.Http;
using GenshinBotCore.Extensions;
using GenshinBotCore.Models;
using GenshinBotCore.Models.MihoyoAccount;

namespace GenshinBotCore.Services
{
    public class MihoyoAccountApi : IMihoyoApi
    {
        private static readonly string _loginUrl = "https://webapi.account.mihoyo.com/Api/login_by_mobilecaptcha";

        ///<inheritdoc/>
        public async Task<IApiResponse<Account>> Login(string phone, string captcha)
        {
            var response = await _loginUrl.HasQuery(new Dictionary<string, string>()
            {
                { "mobile", phone },
                { "mobile_captcha", captcha }
            }).GetAsync().ConfigureAwait(false);

            if (response.StatusCode != 200) throw new HttpRequestException();
            var account = await response.CastTo<MihoyoApiResponse<AccountData>>().ConfigureAwait(false);
            return new MihoyoApiResponse<Account>
            {
                Code = account?.Data?.Account is null ? 0 : 200,
                Data = account?.Data?.Account
            };
        }
    }
}
