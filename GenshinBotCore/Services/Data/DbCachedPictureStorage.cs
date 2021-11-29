using Flurl.Http;
using GenshinBotCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services.Data
{
    public class DbCachedPictureStorage : IPictureStorage
    {
        public DbCachedPictureStorage(ILogger<DbCachedPictureStorage> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        private readonly ILogger logger;
        private readonly ApplicationDbContext dbContext;

        /// <inheritdoc/>
        public async Task<string> GetPicture(string originUrl)
        {
            if (dbContext.Pictures.Any(p => p.Url == originUrl))
            {
                logger.LogInformation("CACHED: {url}", originUrl);
                return dbContext.Pictures.AsNoTracking().Single(p => p.Url == originUrl).Picture;
            }
            return await StoragePicture(originUrl);
        }

        /// <inheritdoc/>
        public async Task<string> StoragePicture(string originUrl)
        {
            logger.LogInformation("GET: {url}", originUrl);
            var response = await originUrl.GetAsync();
            if (response.StatusCode != 200) throw new HttpRequestException(originUrl + ":" + response.StatusCode.ToString());
            using var stream = await response.GetStreamAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            stream.Close();
            memoryStream.Close();

            var picture = Convert.ToBase64String(bytes);

            var exsistCache = dbContext.Pictures.SingleOrDefault(p => p.Url == originUrl);
            if (exsistCache != null)
            {
                exsistCache.Picture = picture;
            }
            else
            {
                dbContext.Pictures.Add(new Pictures()
                {
                    Picture = picture,
                    Url = originUrl
                });
            }

            await dbContext.SaveChangesAsync();
            return picture;
        }
    }
}
