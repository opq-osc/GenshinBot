// <copyright file="PictureInfo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace YukinoshitaBot.Data.WebSocket
{
    /// <summary>
    /// 图片信息
    /// </summary>
    public class PictureInfo
    {
        /// <summary>
        /// 文件序号
        /// </summary>
        public long FileId { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMD5 { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 文件URL
        /// </summary>
        public string? Url { get; set; }
    }
}
