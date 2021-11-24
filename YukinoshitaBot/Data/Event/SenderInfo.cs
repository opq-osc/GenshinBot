// <copyright file="SenderInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Event
{
    /// <summary>
    /// 发消息者信息
    /// </summary>
    public struct SenderInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SenderInfo"/> struct.
        /// </summary>
        /// <param name="fromQQ">好友QQ</param>
        public SenderInfo(long fromQQ)
        {
            this.SenderType = SenderType.Friend;
            this.FromQQ = fromQQ;
            this.FromGroupId = null;
            this.FromGroupName = null;
            this.FromNickName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SenderInfo"/> struct.
        /// </summary>
        /// <param name="fromQQ">群友QQ</param>
        /// <param name="fromGroup">群号</param>
        public SenderInfo(long fromQQ, long fromGroup)
        {
            this.SenderType = SenderType.TempSession;
            this.FromQQ = fromQQ;
            this.FromGroupId = fromGroup;
            this.FromGroupName = null;
            this.FromNickName = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SenderInfo"/> struct.
        /// </summary>
        /// <param name="fromQQ">群友QQ</param>
        /// <param name="fromGroup">群号</param>
        /// <param name="groupName">群名称</param>
        /// <param name="nickName">群友群昵称</param>
        public SenderInfo(long fromQQ, long fromGroup, string groupName, string nickName)
        {
            this.SenderType = SenderType.Group;
            this.FromQQ = fromQQ;
            this.FromGroupId = fromGroup;
            this.FromGroupName = groupName;
            this.FromNickName = nickName;
        }

        /// <summary>
        /// 消息来源群号
        /// </summary>
        public long? FromGroupId { get; set; }

        /// <summary>
        /// 消息来源群名称，仅群消息有效
        /// </summary>
        public string? FromGroupName { get; set; }

        /// <summary>
        /// 消息来源用户QQ号
        /// </summary>
        public long? FromQQ { get; set; }

        /// <summary>
        /// 消息来源用户群昵称，仅群消息有效
        /// </summary>
        public string? FromNickName { get; set; }

        /// <summary>
        /// 发送者类型
        /// </summary>
        public SenderType SenderType { get; set; }
    }
}
