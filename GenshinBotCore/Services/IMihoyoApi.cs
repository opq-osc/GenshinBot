using GenshinBotCore.Models;
using GenshinBotCore.Models.MihoyoAccount;

namespace GenshinBotCore.Services
{
    public interface IMihoyoApi
    {
        public Task<IApiResponse<Account>> Login(string phone, string captcha);
    }
}
