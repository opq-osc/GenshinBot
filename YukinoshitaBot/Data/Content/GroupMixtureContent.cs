// <copyright file="GroupMixtureContent.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.Content
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using YukinoshitaBot.Data.WebSocket;

    /// <summary>
    /// 群图文消息
    /// </summary>
    public class GroupMixtureContent
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<PictureInfo>? GroupPic { get; set; }

        /// <summary>
        /// 文件大小，仅当图片为闪照时有效
        /// </summary>
        public int? FileSize { get; set; }

        /// <summary>
        /// 文件URL，仅当图片为闪照时有效
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// At用户列表，仅当消息包含at时有效
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<QQUser>? UserExt { get; set; }

        /// <summary>
        /// At用户QQ号列表，仅当消息包含at时有效
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<long>? UserID { get; set; }

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
