using GenshinBotCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services
{
    public interface IUserManager
    {
        User? GetUserById(Guid id);
        User? GetUserByQQ(long qqId);
        User? GetUserByMihoyoId(string mihoyoId);
        User? GetUserByGenshinUid(string genshinUid);
    }
}
