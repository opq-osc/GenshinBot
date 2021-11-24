using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YukinoshitaBot.Data.Event;

namespace YukinoshitaBot.Data.Controller
{
    /// <summary>
    /// 机器人消息处理接口
    /// </summary>
    public interface IBotController
    {
        /// <summary>
        /// 处理好友文字消息
        /// </summary>
        /// <param name="message"><see cref="TextMessage"/></param>
        public Task FriendTextMsgHandler(TextMessage message);

        /// <summary>
        /// 处理群组文字消息
        /// </summary>
        /// <param name="message"><see cref="TextMessage"/></param>
        public Task GroupTextMsgHandler(TextMessage message);

        /// <summary>
        /// 处理好友图片消息
        /// </summary>
        /// <param name="message"><see cref="TextMessage"/></param>
        public Task FriendPicMsgHandler(PictureMessage message);

        /// <summary>
        /// 处理群组图片消息
        /// </summary>
        /// <param name="message"><see cref="PictureMessage"/></param>
        public Task GroupPicMsgHandler(PictureMessage message);
    }
}
