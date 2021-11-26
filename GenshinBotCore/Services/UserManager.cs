﻿using GenshinBotCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public User? GetUserById(Guid id) =>
            dbContext.Users.Where(u => u.Id == id).AsNoTracking().SingleOrDefault();
        
        public User? GetUserByMihoyoId(string mihoyoId) =>
            dbContext.Users.Where(u => u.MihoyoId == mihoyoId).AsNoTracking().SingleOrDefault();

        public User? GetUserByQQ(long qqId) => 
            dbContext.Users.Where(u => u.QQ == qqId).AsNoTracking().SingleOrDefault();
    }
}
