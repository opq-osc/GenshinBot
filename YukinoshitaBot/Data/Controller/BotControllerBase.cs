namespace YukinoshitaBot.Data.Controller
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using YukinoshitaBot.Data.Event;
    using YukinoshitaBot.Extensions;

    /// <summary>
    /// 机器人消息类
    /// </summary>
    public abstract class BotControllerBase
    {
        /// <summary>
        /// 接收到的消息
        /// </summary>
        public Message Message { get; set; } = null!;

        /// <summary>
        /// 消息发送者的信息
        /// </summary>
        public SenderInfo SenderInfo { get => Message.SenderInfo; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType MessageType { get => Message.MessageType; }

        /// <summary>
        /// 发送者类型
        /// </summary>
        public SenderType SenderType { get => SenderInfo.SenderType; }

        /// <summary>
        /// 消息来源用户QQ号
        /// </summary>
        public long? FromQQ { get => SenderInfo.FromQQ; }

        /// <summary>
        /// 消息来源群号
        /// </summary>
        public long? FromGroupId { get => SenderInfo.FromGroupId; }

        /// <summary>
        /// 返回文本消息
        /// </summary>
        /// <param name="msg">需要发送的文本</param>
        public void ReplyTextMsg(string msg)
            => Message.ReplyText(msg);

        /// <summary>
        /// 返回图片消息
        /// </summary>
        /// <param name="base64EncodedImage">base64图片</param>
        public void ReplyPictureMsg(string base64EncodedImage)
            => Message.ReplyPicture(base64EncodedImage);

        /// <summary>
        /// 返回图片消息
        /// </summary>
        /// <param name="picUri">图片UR</param>
        public void ReplyPictureMsg(Uri picUri)
            => Message.ReplyPicture(picUri);

        /// <summary>
        /// 返回图片消息
        /// </summary>
        /// <param name="localPicture">本地图片文件</param>
        public void ReplyPictureMsg(FileInfo localPicture)
            => Message.ReplyPicture(localPicture);

        /// <summary>
        /// 匹配得到的参数键值对
        /// </summary>
        public Dictionary<string, string> MatchPairs { get; set; } = null!;

        /// <summary>
        /// 参数验证中的错误信息
        /// </summary>
        public List<string> ParamErrors { get; set; } = new();

        /// <summary>
        /// 参数验证是否成功
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// 当参数验证失败时调用，重写以自定义参数验证失败时的处理逻辑。
        /// </summary>
        public virtual void OnValidationError()
        {
            if (ParamErrors.Any())
            {
                Message.ReplyText(string.Join('\n', ParamErrors));
            }
        }
    }
}
