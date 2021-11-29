using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services.Data
{
    public interface IPictureStorage
    {
        /// <summary>
        /// 获取指定URL的图片
        /// </summary>
        /// <param name="originUrl"></param>
        /// <returns></returns>
        Task<string> GetPicture(string originUrl);

        /// <summary>
        /// 存储指定URL的图片，并返回该图片
        /// </summary>
        /// <param name="originUrl"></param>
        /// <returns></returns>
        Task<string> StoragePicture(string originUrl);
    }
}
