using GenshinBotCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services
{
    /// <summary>
    /// 用户管理器
    /// </summary>
    public interface IUserManager
    {
        User? GetUserById(Guid id);
        User? GetUserByQQ(long qqId);
        User? GetUserByMihoyoId(string mihoyoId);
        User? GetUserByGenshinUid(string genshinUid);
        Task<User?> UpdateUserAsync(User user);
    }
}
