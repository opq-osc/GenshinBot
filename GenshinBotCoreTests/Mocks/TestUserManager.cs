using GenshinBotCore.Entities;
using GenshinBotCore.Services;
using System;
using System.Threading.Tasks;

namespace GenshinBotCoreTests.Mocks
{
    public class TestUserManager : IUserManager
    {
        private static readonly User user = new User
        {
            GenshinUid = "200277243",
            Id = Guid.NewGuid(),
            MihoyoId = "296714852",
            QQ = 1137361788
        };
        public User? GetUserByGenshinUid(string genshinUid)
        {
            return user;
        }

        public User? GetUserById(Guid id)
        {
            return user;
        }

        public User? GetUserByMihoyoId(string mihoyoId)
        {
            return user;
        }

        public User? GetUserByQQ(long qqId)
        {
            return user;
        }

        public Task<User?> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
