using System;
using YukinoshitaBot.Data.Event;
using YukinoshitaBot.Data.OpqApi;

namespace YukinoshitaBot.Extensions
{
    /// <summary>
    /// <see cref="Message"/>的扩展
    /// </summary>
    public static class MessageExtension
    {
        /// <summary>
        /// 返回文本消息
        /// </summary>
        /// <param name="message"><see cref="Message"/></param>
        /// <param name="msg">需要发送的文本</param>
        /// <exception cref="ArgumentException">不支持其他类型</exception>
        public static void ReplyTextMsg(this Message message, string msg)
        {
            var resp = new TextMessageRequest(msg);
            var request = message.SenderInfo.SenderType switch
            {
                SenderType.Friend => resp.SendToFriend(message.SenderInfo.FromQQ ?? default),
                SenderType.Group => resp.SendToGroup(message.SenderInfo.FromGroupId ?? default),
                SenderType.TempSession => resp.SendToGroupMember(message.SenderInfo.FromQQ ?? default, message.SenderInfo.FromGroupId ?? default),
                _ => throw new ArgumentException()
            };

            message.OpqApi?.AddRequest(request);
        }
    }
}
