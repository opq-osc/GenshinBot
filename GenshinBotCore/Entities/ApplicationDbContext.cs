using Microsoft.EntityFrameworkCore;

namespace GenshinBotCore.Entities
{
    /// <summary>
    /// 应用程序数据库实体
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> Users { get; set; } = null!;

        /// <summary>
        /// 用户密钥表
        /// </summary>
        public DbSet<UserSecret> UsersSecret { get; set; } = null!;

        /// <summary>
        /// 图片缓存
        /// </summary>
        public DbSet<Pictures> Pictures { get; set; } = null!;
    }
}
