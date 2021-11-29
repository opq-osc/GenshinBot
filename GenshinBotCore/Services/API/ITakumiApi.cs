using GenshinBotCore.Models;
using GenshinBotCore.Models.TakumiApi;
using Index = GenshinBotCore.Models.TakumiApi.Index;

namespace GenshinBotCore.Services
{
    public interface ITakumiApi
    {
        /// <summary>
        /// 使用Ticket获取Token
        /// </summary>
        /// <param name="loginTicket">登陆时得到的Ticket</param>
        /// <param name="uid">米游社Uid</param>
        /// <param name="tokenType">Token类型</param>
        /// <returns></returns>
        public Task<IApiResponse<MultiToken>> GetMultiTokenByLoginTicketAsync(string loginTicket, string uid, int tokenType);

        /// <summary>
        /// 获取游戏账号信息
        /// </summary>
        /// <param name="uid">米游社Uid</param>
        /// <returns></returns>
        public Task<IApiResponse<GameAccounts>> GetGameAccounts(string uid);

        public Task<IApiResponse<SpiralAbyss>> GetSpiralAbyssAsync(string roleId, string serverId, int scheduleType);

        /// <summary>
        /// 获取每日任务卡片信息
        /// </summary>
        /// <param name="roleId">原神Uid</param>
        /// <param name="serverId">服务器Id</param>
        /// <returns></returns>
        public Task<IApiResponse<DailyNote>> GetDailyNoteAsync(string roleId, string serverId);

        /// <summary>
        /// 获取主页信息
        /// </summary>
        /// <param name="roleId">原神Uid</param>
        /// <param name="serverId">服务器Id</param>
        /// <returns></returns>
        public Task<IApiResponse<Index>> GetIndexAsync(string roleId, string serverId);
    }
}
