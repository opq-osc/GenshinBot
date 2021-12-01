// <copyright file="YukinoshitaController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using YukinoshitaBot.Data.Attributes;
    using YukinoshitaBot.Data.Controller;
    using YukinoshitaBot.Data.Event;

    /// <summary>
    /// 实现控制器
    /// </summary>
    internal class YukinoshitaController : IMessageHandler
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ControllerCollection controllers;

        /// <summary>
        /// Initializes a new instance of the <see cref="YukinoshitaController"/> class.
        /// </summary>
        /// <param name="serviceProvider">服务容器</param>
        /// <param name="controllerCollection">控制器容器</param>
        public YukinoshitaController(
            IServiceProvider serviceProvider,
            ControllerCollection controllerCollection)
        {
            this.serviceProvider = serviceProvider;
            this.controllers = controllerCollection;
        }

        /// <inheritdoc/>
        public void OnFriendPictureMsgRecieved(PictureMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.FriendImageHandlers.Any())
                {
                    var controllerObj = controllers.GetController(controller.ControllerType);
                    foreach (var method in controller.FriendImageHandlers)
                    {
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnFriendTextMsgRecieved(TextMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.FriendTextHandlers.Any())
                {
                    var controllerObj = controllers.GetController(controller.ControllerType);
                    foreach (var method in controller.FriendTextHandlers)
                    {
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnGroupPictureMsgRecieved(PictureMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.GroupImageHandlers.Any())
                {
                    var controllerObj = controllers.GetController(controller.ControllerType);
                    foreach (var method in controller.GroupImageHandlers)
                    {
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnGroupTextMsgRecieved(TextMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!CheckMatch(msg.Content, controller.ControllerAttribute.Command, controller.ControllerAttribute.MatchMethod))
                {
                    continue;
                }

                if (controller.GroupTextHandlers.Any())
                {
                    var controllerObj = controllers.GetController(controller.ControllerType);
                    foreach (var method in controller.GroupTextHandlers)
                    {
                        method.Invoke(controllerObj, new object[] { msg });
                    }

                    break;
                }
            }
        }

        private static bool CheckMatch(string msg, string cmd, CommandMatchMethod method) => method switch
        {
            CommandMatchMethod.Strict => msg == cmd,
            CommandMatchMethod.StartWith => msg.StartsWith(cmd),
            CommandMatchMethod.Regex => Regex.IsMatch(msg, cmd),
            _ => false
        };
    }
}
