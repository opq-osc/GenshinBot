using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime _unixBase = new(1970, 1, 1);

        /// <summary>
        /// 转换为10位时间戳
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static long ToShortTimeStamp(this DateTime dateTime)
        {
            return (long)(dateTime - _unixBase).TotalSeconds;
        }
    }
}
