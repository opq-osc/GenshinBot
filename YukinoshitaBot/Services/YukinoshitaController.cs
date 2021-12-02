// <copyright file="YukinoshitaController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using YukinoshitaBot.Data.Attributes;
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
                if (!controller.FriendImageHandlers.Any() || msg.Content.Length > controller.ControllerAttribute.MaxLength)
                {
                    continue;
                }
                List<MethodInfo> methods = controller.FriendImageHandlers;
                if (InvokeMethod(msg, msg.Content, controller, methods)
                    && controller.ControllerAttribute.Mode == HandleMode.Break)
                {
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnFriendTextMsgRecieved(TextMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!controller.FriendTextHandlers.Any() || msg.Content.Length > controller.ControllerAttribute.MaxLength)
                {
                    continue;
                }
                List<MethodInfo> methods = controller.FriendTextHandlers;
                if (InvokeMethod(msg, msg.Content, controller, methods)
                    && controller.ControllerAttribute.Mode == HandleMode.Break)
                {
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnGroupPictureMsgRecieved(PictureMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!controller.GroupImageHandlers.Any() || msg.Content.Length > controller.ControllerAttribute.MaxLength)
                {
                    continue;
                }
                List<MethodInfo> methods = controller.GroupImageHandlers;
                if (InvokeMethod(msg, msg.Content, controller, methods)
                    && controller.ControllerAttribute.Mode == HandleMode.Break)
                {
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public void OnGroupTextMsgRecieved(TextMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!controller.GroupTextHandlers.Any() || msg.Content.Length > controller.ControllerAttribute.MaxLength)
                {
                    continue;
                }
                List<MethodInfo> methods = controller.GroupTextHandlers;
                if (InvokeMethod(msg, msg.Content, controller, methods)
                    && controller.ControllerAttribute.Mode == HandleMode.Break)
                {
                    break;
                }
            }
        }

        private bool InvokeMethod(Message msg, string content, YukinoshitaControllerInfo controller, List<MethodInfo> methods)
        {
            var controllerType = controller.ControllerType;
            if (controllerType.GetCustomAttribute<YukinoControllerAttribute>() is not YukinoControllerAttribute yukinoRoute
                || !yukinoRoute.TryMatch(content, out var matchPairs))
            {
                return false;
            }
            var controllerObj = controllers.GetController(controllerType);
            controllerObj.Message = msg;
            foreach (var method in methods)
            {
                var @params = method.GetParameters();
                var paramsIn = new object[@params.Length];
                for (int i = 0; i < @params.Length; i++)
                {
                    var name = @params[i].Name ?? throw new ArgumentNullException("name can't be null");
                    if (!matchPairs.TryGetValue(name, out var value))
                    {
                        throw new ArgumentException($"can't get the value of key:{name} from the regex groups, please check your regex.");
                    }
                    paramsIn[i] = Convert.ChangeType(value, @params[i].ParameterType);
                }
                method.Invoke(controllerObj, paramsIn);
            }
            return true;
        }
    }
}
