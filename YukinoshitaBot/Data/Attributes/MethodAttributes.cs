using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YukinoshitaBot.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FriendTextAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class GroupTextAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class TempTextAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class FriendImageAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class GroupImageAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class TempImageAttribute : Attribute { }
}
