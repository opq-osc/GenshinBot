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
    using YukinoshitaBot.Data.Controller;
    using YukinoshitaBot.Data.Event;

    /// <summary>
    /// 实现控制器
    /// </summary>
    internal class YukinoshitaController : IMessageHandler
    {
        private readonly ControllerCollection controllers;

        /// <summary>
        /// Initializes a new instance of the <see cref="YukinoshitaController"/> class.
        /// </summary>
        /// <param name="controllerCollection">控制器容器</param>
        public YukinoshitaController(
            ControllerCollection controllerCollection)
        {
            this.controllers = controllerCollection;
        }

        /// <inheritdoc/>
        public void OnFriendPictureMsgRecieved(PictureMessage msg)
        {
            foreach (var controller in this.controllers.ResolvedControllers)
            {
                if (!controller.FriendImageHandlers.Any()
                    || msg.Content.Length > controller.YukinoControllerAttribute.MaxLength
                    || !controller.YukinoControllerAttribute.TryMatch(msg.Content, out var matchPairs))
                {
                    continue;
                }
                BotControllerBase controllerObj = controllers.GetController(controller.ControllerType)
                    ?? throw new NullReferenceException();
                controllerObj.MatchPairs = matchPairs;
                controllerObj.Message = msg;
                List<MethodInfo> methods = controller.FriendImageHandlers;
                if (InvokeMethod(controllerObj, methods)
                    && controller.YukinoControllerAttribute.Mode == HandleMode.Break)
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
                if (!controller.FriendTextHandlers.Any() || msg.Content.Length > controller.YukinoControllerAttribute.MaxLength)
                {
                    continue;
                }
                if (!controller.YukinoControllerAttribute.TryMatch(msg.Content, out var matchPairs))
                {
                    continue;
                }
                BotControllerBase controllerObj = controllers.GetController(controller.ControllerType)
                    ?? throw new NullReferenceException();
                controllerObj.MatchPairs = matchPairs;
                controllerObj.Message = msg;
                List<MethodInfo> methods = controller.FriendTextHandlers;
                if (InvokeMethod(controllerObj, methods)
                    && controller.YukinoControllerAttribute.Mode == HandleMode.Break)
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
                if (!controller.GroupImageHandlers.Any() || msg.Content.Length > controller.YukinoControllerAttribute.MaxLength)
                {
                    continue;
                }
                if (!controller.YukinoControllerAttribute.TryMatch(msg.Content, out var matchPairs))
                {
                    continue;
                }
                BotControllerBase controllerObj = controllers.GetController(controller.ControllerType)
                    ?? throw new NullReferenceException();
                controllerObj.MatchPairs = matchPairs;
                controllerObj.Message = msg;
                List<MethodInfo> methods = controller.GroupImageHandlers;
                if (InvokeMethod(controllerObj, methods)
                    && controller.YukinoControllerAttribute.Mode == HandleMode.Break)
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
                if (!controller.GroupTextHandlers.Any() || msg.Content.Length > controller.YukinoControllerAttribute.MaxLength)
                {
                    continue;
                }
                if (!controller.YukinoControllerAttribute.TryMatch(msg.Content, out var matchPairs))
                {
                    continue;
                }
                BotControllerBase controllerObj = controllers.GetController(controller.ControllerType)
                    ?? throw new NullReferenceException();
                controllerObj.MatchPairs = matchPairs;
                controllerObj.Message = msg;
                List<MethodInfo> methods = controller.GroupTextHandlers;
                if (InvokeMethod(controllerObj, methods)
                    && controller.YukinoControllerAttribute.Mode == HandleMode.Break)
                {
                    break;
                }
            }
        }

        private bool InvokeMethod(BotControllerBase controllerObj, List<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                var @params = method.GetParameters();
                var paramsIn = new object[@params.Length];
                for (int i = 0; i < @params.Length; i++)
                {
                    var name = @params[i].Name ?? throw new ArgumentNullException("name can't be null");
                    if (!controllerObj.MatchPairs.TryGetValue(name, out var value))
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
