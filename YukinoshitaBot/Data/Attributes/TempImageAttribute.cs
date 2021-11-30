namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using YukinoshitaBot.Data.Event;

    [AttributeUsage(AttributeTargets.Method)]
    public class TempImageAttribute : Attribute
    {
    }
}
