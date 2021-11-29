using GenshinBotCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenshinBotCore.Services
{
    public class UserManager : IUserManager
    {
        public UserManager(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private readonly ApplicationDbContext dbContext;

        public User? GetUserByGenshinUid(string genshinUid) =>
            dbContext.Users.Where(u => u.GenshinUid == genshinUid).AsNoTracking().SingleOrDefault();

        public User? GetUserById(int id) =>
            dbContext.Users.Where(u => u.Id == id).AsNoTracking().SingleOrDefault();

        public User? GetUserByMihoyoId(string mihoyoId) =>
            dbContext.Users.Where(u => u.MihoyoId == mihoyoId).AsNoTracking().SingleOrDefault();

        public User? GetUserByQQ(long qqId) =>
            dbContext.Users.Where(u => u.QQ == qqId).AsNoTracking().SingleOrDefault();

        public async Task<User?> UpdateUserAsync(User user)
        {
            try
            {
                var exsistUser = GetUserByQQ(user.QQ);
                if (exsistUser is null)
                {
                    exsistUser = dbContext.Users.Add(user).Entity;
                }
                exsistUser.GenshinUid = user.GenshinUid;
                exsistUser.MihoyoId = user.MihoyoId;

                user = exsistUser;
            }
            catch
            {
                return null;
            }
            await dbContext.SaveChangesAsync().ConfigureAwait(false);
            return user;
        }
    }
}
