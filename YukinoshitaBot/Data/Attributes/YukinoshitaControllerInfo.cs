// <copyright file="YukinoshitaControllerInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using YukinoshitaBot.Data.Event;

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
            this.YukinoRouteAttribute = attribute;

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
        }

        /// <summary>
        /// 获取处理对应消息类型的方法
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="methods">方法</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool TryGetHandlers(MessageType msgType, SenderType senderType, out List<MethodInfo> methods)
        {
            methods = (msgType, senderType) switch
            {
                (MessageType.TextMessage, SenderType.Friend) => FriendTextHandlers,
                (MessageType.TextMessage, SenderType.Group) => GroupTextHandlers,
                (MessageType.TextMessage, SenderType.TempSession) => TempSessionTextHandlers,
                (MessageType.PictureMessage, SenderType.Friend) => FriendImageHandlers,
                (MessageType.PictureMessage, SenderType.Group) => GroupImageHandlers,
                (MessageType.PictureMessage, SenderType.TempSession) => TempSessionImageHandlers,
                _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(msgType))
            };
            return methods.Any();
        }

        /// <summary>
        /// 控制器类型
        /// </summary>
        public Type ControllerType { get; set; } = null!;

        /// <summary>
        /// 控制器属性
        /// </summary>
        public YukinoRouteAttribute YukinoRouteAttribute { get; set; } = null!;

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

        public static bool operator >(YukinoshitaControllerInfo obj1, YukinoshitaControllerInfo obj2)
        {
            return obj1.YukinoRouteAttribute.Priority > obj2.YukinoRouteAttribute.Priority;
        }

        public static bool operator <(YukinoshitaControllerInfo obj1, YukinoshitaControllerInfo obj2)
        {
            return obj1.YukinoRouteAttribute.Priority < obj2.YukinoRouteAttribute.Priority;
        }

        /// <inheritdoc/>
        public int CompareTo(YukinoshitaControllerInfo other)
        {
            return this.YukinoRouteAttribute.Priority - other.YukinoRouteAttribute.Priority;
        }
    }
}
