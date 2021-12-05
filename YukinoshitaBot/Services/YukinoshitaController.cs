// <copyright file="YukinoshitaController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
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
            OnPictureMsgRecieved(msg);
        }

        /// <inheritdoc/>
        public void OnFriendTextMsgRecieved(TextMessage msg)
        {
            OnTextMsgRecieved(msg);
        }

        /// <inheritdoc/>
        public void OnGroupPictureMsgRecieved(PictureMessage msg)
        {
            OnPictureMsgRecieved(msg);
        }

        /// <inheritdoc/>
        public void OnGroupTextMsgRecieved(TextMessage msg)
        {
            OnTextMsgRecieved(msg);
        }

        /// <summary>
        /// 文本消息处理
        /// </summary>
        /// <param name="msg">文本消息</param>
        /// <exception cref="NullReferenceException"></exception>
        public void OnTextMsgRecieved(TextMessage msg)
        {
            foreach (var controller in controllers.ResolvedControllers)
            {
                if (!controller.TryGetHandlers(msg.MessageType, msg.SenderInfo.SenderType, out var methods))
                {
                    continue;
                }
                if (!controller.YukinoRouteAttribute.CheckLength(msg.Content))
                {
                    continue;
                }
                if (!controller.YukinoRouteAttribute.TryMatch(msg.Content, out var matchPairs))
                {
                    continue;
                }
                BotControllerBase controllerObj = controllers.GetController(controller.ControllerType)
                    ?? throw new NullReferenceException();
                controllerObj.MatchPairs = matchPairs;
                controllerObj.Message = msg;
                if (InvokeMethod(controllerObj, methods)
                    && controller.YukinoRouteAttribute.Mode == HandleMode.Break)
                {
                    break;
                }
            }
        }

        public void OnPictureMsgRecieved(PictureMessage msg)
        {
            foreach (var controller in controllers.ResolvedControllers)
            {
                if (!controller.TryGetHandlers(msg.MessageType, msg.SenderInfo.SenderType, out var methods))
                {
                    continue;
                }
                if (!controller.YukinoRouteAttribute.CheckLength(msg.Content))
                {
                    continue;
                }
                if (!controller.YukinoRouteAttribute.TryMatch(msg.Content, out var matchPairs))
                {
                    continue;
                }
                var controllerObj = controllers.GetController(controller.ControllerType)
                    ?? throw new NullReferenceException();
                controllerObj.MatchPairs = matchPairs;
                controllerObj.Message = msg;
                if (InvokeMethod(controllerObj, methods)
                    && controller.YukinoRouteAttribute.Mode == HandleMode.Break)
                {
                    break;
                }
            }
        }

        private bool InvokeMethod(BotControllerBase controllerObj, List<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                object?[] paramsIn = ValidMethod(controllerObj, method);
                if (controllerObj.IsValid)
                {
                    method.Invoke(controllerObj, paramsIn);
                }
                else
                {
                    controllerObj.OnValidationError();
                }
            }
            return true;
        }

        private object?[] ValidMethod(BotControllerBase controllerObj, MethodInfo method)
        {
            var @params = method.GetParameters();
            var paramsIn = new object?[@params.Length];
            for (int i = 0; i < @params.Length; i++)
            {
                var name = @params[i].Name ?? throw new ArgumentNullException("name can't be null");
                if (controllerObj.MatchPairs.TryGetValue(name, out var value))
                {
                    if (TryValidParam(value, @params[i], out var errorInfo))
                    {
                        paramsIn[i] = Convert.ChangeType(value, @params[i].ParameterType);
                        continue;
                    }
                    else
                    {
                        if (errorInfo != null)
                        {
                            controllerObj.ParamErrors.Add(errorInfo);
                        }
                        controllerObj.IsValid = false;
                    }
                }
                else
                {
                    paramsIn[i] = @params[i].DefaultValue
                        ?? throw new ArgumentException($"can't get the value of key:{name} from the regex groups, and the parameter doesn't have a default value, please check your regex.");
                }
            }

            return paramsIn;
        }

        private bool TryValidParam(string value, ParameterInfo info, out string? errorInfo)
        {
            var attrs = info.GetCustomAttributes<ValidationAttribute>();
            foreach (var attr in attrs)
            {
                if (!attr.IsValid(value))
                {
                    errorInfo = attr.ErrorMessage;
                    return false;
                }
            }
            errorInfo = null;
            return true;
        }
    }
}
