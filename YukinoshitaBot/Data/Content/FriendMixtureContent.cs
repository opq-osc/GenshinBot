// <copyright file="FriendMixtureContent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Content
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using YukinoshitaBot.Data.WebSocket;

    /// <summary>
    /// 好友图文消息
    /// </summary>
    public class FriendMixtureContent
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<PictureInfo>? FriendPic { get; set; }

        /// <summary>
        /// 文件大小，仅当图片为闪照时有效
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? FileSize { get; set; }

        /// <summary>
        /// 文件URL，仅当图片为闪照时有效
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        /// <summary>
        /// 回复的消息的序列号，仅当消息为回复消息时有效
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? MsgSeq { get; set; }

        /// <summary>
        /// 回复消息的内容，仅当消息为回复消息时有效
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReplayContent { get; set; }

        /// <summary>
        /// 回复的原消息，仅当消息为回复消息时有效
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SrcContent { get; set; }

        /// <summary>
        /// 消息Tips
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Tips { get; set; }

        /// <summary>
        /// 是否闪照
        /// </summary>
        [JsonIgnore]
        public bool IsFlashPicture => !string.IsNullOrEmpty(this.Url);
    }
}
