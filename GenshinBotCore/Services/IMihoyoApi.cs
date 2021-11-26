using GenshinBotCore.Models;
using GenshinBotCore.Models.MihoyoAccount;

namespace GenshinBotCore.Services
{
    public interface IMihoyoApi
    {
        /// <summary>
        /// 短信验证码登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="captcha">验证码</param>
        /// <returns></returns>
        public Task<IApiResponse<Account>> Login(string phone, string captcha);
    }
}
