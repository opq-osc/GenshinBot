// <copyright file="YukinoshitaControllerInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// 控制器信息
    /// </summary>
    internal struct YukinoshitaControllerInfo : IComparable<YukinoshitaControllerInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YukinoshitaControllerInfo"/> struct.
        /// </summary>
        /// <param name="controllerType">控制器类型</param>
        public YukinoshitaControllerInfo(Type controllerType)
        {
            this.ControllerType = controllerType;
            var attribute = controllerType.GetCustomAttribute<YukinoRouteAttribute>();
            if (attribute is null)
            {
                throw new InvalidCastException($"Type '{controllerType.FullName}' is not a YukinoshitaController.");
            }

            var methods = controllerType.GetMethods();

            this.FriendTextHandlers = (from method in methods
                                  let attr = method.GetCustomAttribute<FriendTextAttribute>()
                                  where attr != null
                                  select method).ToList();
            this.FriendImageHandlers = (from method in methods
                                       let attr = method.GetCustomAttribute<FriendImageAttribute>()
                                       where attr != null
                                       select method).ToList();
            this.GroupTextHandlers = (from method in methods
                                       let attr = method.GetCustomAttribute<GroupTextAttribute>()
                                       where attr != null
                                       select method).ToList();
            this.GroupImageHandlers = (from method in methods
                                       let attr = method.GetCustomAttribute<GroupImageAttribute>()
                                       where attr != null
                                       select method).ToList();
            this.TempSessionTextHandlers = (from method in methods
                                       let attr = method.GetCustomAttribute<TempTextAttribute>()
                                       where attr != null
                                       select method).ToList();
            this.TempSessionImageHandlers = (from method in methods
                                       let attr = method.GetCustomAttribute<TempImageAttribute>()
                                       where attr != null
                                       select method).ToList();

            this.ControllerAttribute = attribute;
        }

        /// <summary>
        /// 控制器类型
        /// </summary>
        public Type ControllerType { get; set; } = null!;

        /// <summary>
        /// 好友文本消息处理的方法
        /// </summary>
        public List<MethodInfo> FriendTextHandlers { get; set; } = null!;

        /// <summary>
        /// 好友图片消息的处理方法
        /// </summary>

        public List<MethodInfo> FriendImageHandlers { get; set; } = null!;

        /// <summary>
        /// 群组文本消息的处理方法
        /// </summary>
        public List<MethodInfo> GroupTextHandlers { get; set; } = null!;

        /// <summary>
        /// 群组图片消息的处理方法
        /// </summary>
        public List<MethodInfo> GroupImageHandlers { get; set; } = null!;

        /// <summary>
        /// 临时会话文本消息的处理方法
        /// </summary>
        public List<MethodInfo> TempSessionTextHandlers { get; set; } = null!;

        /// <summary>
        /// 临时会话图片消息的处理方法
        /// </summary>

        public List<MethodInfo> TempSessionImageHandlers { get; set; } = null!;

        /// <summary>
        /// 控制器属性
        /// </summary>
        public YukinoRouteAttribute ControllerAttribute { get; set; } = null!;

        public static bool operator >(YukinoshitaControllerInfo obj1, YukinoshitaControllerInfo obj2)
        {
            return obj1.ControllerAttribute.Priority > obj2.ControllerAttribute.Priority;
        }

        public static bool operator <(YukinoshitaControllerInfo obj1, YukinoshitaControllerInfo obj2)
        {
            return obj1.ControllerAttribute.Priority < obj2.ControllerAttribute.Priority;
        }

        /// <inheritdoc/>
        public int CompareTo(YukinoshitaControllerInfo other)
        {
            return this.ControllerAttribute.Priority - other.ControllerAttribute.Priority;
        }
    }
}
