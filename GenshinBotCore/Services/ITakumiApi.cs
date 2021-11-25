using GenshinBotCore.Models.TakumiApi;
using Index = GenshinBotCore.Models.TakumiApi.Index;

namespace GenshinBotCore.Services
{
    internal interface ITakumiApi
    {
        public IApiResponse<UserToken> GetMultiTokenByLoginTicket(string loginTicket, string uid, int tokenType);

        public IApiResponse<GameRoles> GetGameRolesBySToken(string stoken, string uid);

        public IApiResponse<SpiralAbyss> GetSpiralAbyss(string roleId, string serverId, int scheduleType);

        public IApiResponse<DailyNote> GetDailyNote(string roleId, string serverId);

        public IApiResponse<Index> GetIndex(string roleId, string serverId);
    }
}
