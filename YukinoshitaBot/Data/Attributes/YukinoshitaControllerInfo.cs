// <copyright file="YukinoshitaControllerInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Attributes
{
    using System;
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
            var attribute = controllerType.GetCustomAttribute<YukinoshitaControllerAttribute>();
            if (attribute is null)
            {
                throw new InvalidCastException($"Type '{controllerType.FullName}' is not a YukinoshitaController.");
            }

            this.ControllerAttribute = attribute;
        }

        /// <summary>
        /// 控制器类型
        /// </summary>
        public Type ControllerType { get; set; }

        /// <summary>
        /// 控制器属性
        /// </summary>
        public YukinoshitaControllerAttribute ControllerAttribute { get; set; }

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
