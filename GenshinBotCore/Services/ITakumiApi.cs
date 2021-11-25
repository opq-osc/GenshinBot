using GenshinBotCore.Models;
using GenshinBotCore.Models.TakumiApi;
using Index = GenshinBotCore.Models.TakumiApi.Index;

namespace GenshinBotCore.Services
{
    public interface ITakumiApi
    {
        public Task<IApiResponse<UserToken>> GetMultiTokenByLoginTicketAsync(string loginTicket, string uid, int tokenType);

        public Task<IApiResponse<GameRoles>> GetGameRolesBySTokenAsync(string stoken, string uid);

        public Task<IApiResponse<SpiralAbyss>> GetSpiralAbyssAsync(string roleId, string serverId, int scheduleType);

        public Task<IApiResponse<DailyNote>> GetDailyNoteAsync(string roleId, string serverId);

        public Task<IApiResponse<Index>> GetIndexAsync(string roleId, string serverId);
    }
}
