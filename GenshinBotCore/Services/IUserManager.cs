using GenshinBotCore.Entities;

namespace GenshinBotCore.Services
{
    /// <summary>
    /// 用户管理器
    /// </summary>
    public interface IUserManager
    {
        User? GetUserById(int id);
        User? GetUserByQQ(long qqId);
        User? GetUserByMihoyoId(string mihoyoId);
        User? GetUserByGenshinUid(string genshinUid);
        Task<User?> UpdateUserAsync(User user);
    }
}
