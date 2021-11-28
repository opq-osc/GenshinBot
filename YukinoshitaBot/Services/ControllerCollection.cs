// <copyright file="ControllerCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using System;
    using System.Collections.Generic;
    using YukinoshitaBot.Data.Attributes;

    /// <summary>
    /// 控制器容器
    /// </summary>
    internal class ControllerCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerCollection"/> class.
        /// </summary>
        public ControllerCollection()
        {
            // todo 通过子类获取 而不是 属性
            this.ResolvedControllers = new SortedSet<YukinoshitaControllerInfo>();
        }

        /// <summary>
        /// 已解析的控制器
        /// </summary>
        public SortedSet<YukinoshitaControllerInfo> ResolvedControllers { get; init; }


        /// <summary>
        /// 添加一个控制器
        /// </summary>
        /// <param name="controllerType">控制器类型</param>
        public void AddController(Type controllerType)
        {
            this.ResolvedControllers.Add(new YukinoshitaControllerInfo(controllerType));
        }
    }
}
