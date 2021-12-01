namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using YukinoshitaBot.Data.Event;

    /// <summary>
    /// 群组图片消息
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GroupImageAttribute : Attribute
    {
    }
}
