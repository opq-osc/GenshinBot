// <copyright file="Message.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    using System.Collections.Generic;
    using System.Linq;
    using YukinoshitaBot.Data.Content;
    using YukinoshitaBot.Data.OpqApi;
    using YukinoshitaBot.Data.WebSocket;
    using YukinoshitaBot.Services;

    /// <summary>
    /// 消息基类，提供类型判断、回复等功能
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="sender">发送者信息</param>
        public Message(SenderInfo sender)
        {
            this.SenderInfo = sender;
        }

        /// <summary>
        /// 是否为回复消息
        /// </summary>
        public bool IsReplayMsg { get; set; }

        /// <summary>
        /// 回复详细数据
        /// </summary>
        public ReplayInfo? ReplayInfo { get; set; }

        /// <summary>
        /// 是否为at消息
        /// </summary>
        public bool IsAtMsg { get; set; }

        /// <summary>
        /// at详细数据
        /// </summary>
        public AtInfo? AtInfo { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// 消息发送者
        /// </summary>
        public SenderInfo SenderInfo { get; set; }

        /// <summary>
        /// OpqApi
        /// </summary>
        public OpqApi? OpqApi { get; set; }

        /// <summary>
        /// 从<see cref="GroupMessage"/>创建Message
        /// </summary>
        /// <param name="rawMessage">原始消息</param>
        /// <exception cref="System.ArgumentException">ArgumentException</exception>
        /// <exception cref="System.NotImplementedException">NotImplementedException</exception>
        /// <returns><see cref="Message"/>的子类</returns>
        public static Message Parse(GroupMessage? rawMessage)
        {
            if (rawMessage is null)
            {
                throw new System.ArgumentException("参数不能为空", nameof(rawMessage));
            }

            var sender = new SenderInfo(rawMessage.FromUserId, rawMessage.FromGroupId,
                rawMessage.FromGroupName ?? string.Empty, rawMessage.FromNickName ?? string.Empty);

            // 若为TextMsg，则不必可能有At和Replay
            if (rawMessage.MsgType == "TextMsg")
            {
                return new TextMessage(sender, rawMessage.Content ?? string.Empty);
            }

            // 若不为TextMsg，则消息体必为Json
            var rawContent = rawMessage.ParseContent<GroupMixtureContent>();

            // 处理可能存在的At和Replay数据
            var atInfo = new AtInfo();
            var replayInfo = new ReplayInfo();

            // 存在At数据
            if (rawContent.UserExt is not null && rawContent.UserID is not null)
            {
                atInfo.AtUsers.AddRange(from user in rawContent.UserExt
                                        select new QQUser { NickName = user.NickName, QQ = user.QQ });
                atInfo.UserID.AddRange(rawContent.UserID);
            }

            // 存在Replay数据
            if (rawContent.MsgSeq is int msgSeq && rawContent.ReplayContent is not null && rawContent.SrcContent is not null)
            {
                replayInfo.MsgSeq = msgSeq;
                replayInfo.ReplayContent = rawContent.ReplayContent;
                replayInfo.SrcContent = rawContent.SrcContent;
            }

            // 消息类型判别（OPQ返回的消息类型会互相覆盖，不能单纯根据MsgType判断）
            rawMessage.MsgType = (rawMessage.MsgType, rawContent.Tips) switch
            {
                ("PicMsg", _) => "PicMsg",
                (_, "[群图片]" or "[群消息-QQ闪照]") => "PicMsg",
                ("VoiceMsg", _) => "VoiceMsg",
                (_, "[语音]") => "VoiceMsg",
                (_, _) => rawMessage.MsgType
            };

            if (rawMessage.MsgType == "PicMsg")
            {
                var picMessage = rawContent?.IsFlashPicture switch
                {
                    true => new PictureMessage(sender, rawContent.Url ?? string.Empty),
                    false => new PictureMessage(sender, from pic in rawContent.GroupPic select pic.Url, rawContent.Content ?? string.Empty),
                    null => throw new System.ArgumentException("cannot determine content type.")
                };

                picMessage.ReplayInfo = replayInfo;
                picMessage.AtInfo = atInfo;

                return picMessage;
            }
            else if (rawMessage.MsgType == "VoiceMsg")
            {
                return new VoiceMessage(sender, rawContent.Url ?? string.Empty)
                {
                    ReplayInfo = replayInfo,
                    AtInfo = atInfo,
                };
            }
            else if (rawMessage.MsgType == "XmlMsg")
            {
                return new XmlMessage(sender, rawContent.Content ?? string.Empty)
                {
                    ReplayInfo = replayInfo,
                    AtInfo = atInfo,
                };
            }
            else
            {
                // 未解析的消息全部视为文本消息
                return new TextMessage(sender, rawContent.Content ?? string.Empty)
                {
                    ReplayInfo = replayInfo,
                    AtInfo = atInfo,
                };
            }
        }

        /// <summary>
        /// 从<see cref="FriendMessage"/>创建Message
        /// </summary>
        /// <param name="rawMessage">原始消息</param>
        /// <exception cref="System.ArgumentException">ArgumentException</exception>
        /// <exception cref="System.NotImplementedException">NotImplementedException</exception>
        /// <returns><see cref="Message"/>的子类</returns>
        public static Message Parse(FriendMessage? rawMessage)
        {
            if (rawMessage is null)
            {
                throw new System.ArgumentException("参数不能为空", nameof(rawMessage));
            }

            if (rawMessage.MsgType == "TextMsg")
            {
                var sender = new SenderInfo(rawMessage.FromUin);
                return new TextMessage(sender, rawMessage.Content ?? string.Empty);
            }
            else if (rawMessage.MsgType == "PicMsg")
            {
                var sender = new SenderInfo(rawMessage.FromUin);
                var rawContent = rawMessage.ParseContent<FriendMixtureContent>();
                var picMessage = rawContent?.IsFlashPicture switch
                {
                    true => new PictureMessage(sender, rawContent.Url ?? string.Empty),
                    false => new PictureMessage(sender, from pic in rawContent.FriendPic select pic.Url, rawContent.Content ?? string.Empty),
                    null => throw new System.ArgumentException("cannot determine content type.")
                };
                return picMessage;
            }
            else if (rawMessage.MsgType == "TempSessionMsg")
            {
                var sender = new SenderInfo(rawMessage.FromUin, rawMessage.GroupID);
                var rawContent = rawMessage.ParseContent<FriendMixtureContent>();
                if (rawContent?.FriendPic is List<PictureInfo>)
                {
                    return new PictureMessage(sender, from pic in rawContent.FriendPic select pic.Url, rawContent.Content ?? string.Empty);
                }
                else if (rawContent?.IsFlashPicture is true)
                {
                    return new PictureMessage(sender, rawContent.Url ?? string.Empty);
                }
                else
                {
                    return new TextMessage(sender, rawContent?.Content ?? string.Empty);
                }
            }
            else
            {
                throw new System.NotImplementedException("not surpported content type " + rawMessage.MsgType);
            }
        }

        /// <summary>
        /// 回复消息
        /// </summary>
        /// <param name="resp">要回复的消息</param>
        public void Reply(MessageRequestBase resp)
        {
            var request = this.SenderInfo.SenderType switch
            {
                SenderType.Friend => resp.SendToFriend(this.SenderInfo.FromQQ ?? default),
                SenderType.Group => resp.SendToGroup(this.SenderInfo.FromGroupId ?? default),
                SenderType.TempSession => resp.SendToGroupMember(this.SenderInfo.FromQQ ?? default, this.SenderInfo.FromGroupId ?? default),
                _ => throw new System.NotImplementedException()
            };

            this.OpqApi?.AddRequest(request);
        }
    }
}
