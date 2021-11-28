// <copyright file="IMessageHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Services
{
    using System.Threading.Tasks;
    using YukinoshitaBot.Data.Event;

    /// <summary>
    /// 消息处理器
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// 群文本消息处理
        /// </summary>
        /// <param name="msg">消息</param>
        Task OnGroupTextMsgRecievedAsync(TextMessage msg);

        /// <summary>
        /// 群图片消息处理
        /// </summary>
        /// <param name="msg">消息</param>
        Task OnGroupPictureMsgRecievedAsync(PictureMessage msg);

        /// <summary>
        /// 好友文本消息处理
        /// </summary>
        /// <param name="msg">消息</param>
        Task OnFriendTextMsgRecievedAsync(TextMessage msg);

        /// <summary>
        /// 好友图片消息处理
        /// </summary>
        /// <param name="msg">消息</param>
        Task OnFriendPictureMsgRecievedAsync(PictureMessage msg);
    }
}
