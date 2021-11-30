// <copyright file="ControllerCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using YukinoshitaBot.Data.Attributes;
    using YukinoshitaBot.Data.Controller;

    /// <summary>
    /// 控制器容器
    /// </summary>
    internal class ControllerCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerCollection"/> class.
        /// </summary>
        public ControllerCollection(IServiceProvider serviceProvider)
        {
            this.scope = serviceProvider.CreateScope();
            this.ResolvedControllers = new SortedSet<YukinoshitaControllerInfo>();
        }

        /// <summary>
        /// 已解析的控制器
        /// </summary>
        public SortedSet<YukinoshitaControllerInfo> ResolvedControllers { get; init; }
        private IServiceScope scope { get; set; }


        /// <summary>
        /// 添加一个控制器
        /// </summary>
        /// <param name="controllerType">控制器类型</param>
        public void AddController(Type controllerType)
        {
            this.ResolvedControllers.Add(new YukinoshitaControllerInfo(controllerType));
        }

        public BotControllerBase GetController(Type controllerType)
        {
            if (this.scope.ServiceProvider.GetService(controllerType) is not BotControllerBase controllerObj)
            {
                throw new InvalidOperationException("controller is not resolved.");
            }

            return controllerObj;
        }
    }
}
