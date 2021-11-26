using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserSecret> UsersSecret { get; set; } = null!;
    }
}
