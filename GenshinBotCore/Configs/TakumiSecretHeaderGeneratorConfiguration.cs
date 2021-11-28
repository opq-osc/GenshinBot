namespace GenshinBotCore.Configs
{
    /// <summary>
    /// 机密请求头生成配置
    /// </summary>
    public class TakumiSecretHeaderGeneratorConfiguration
    {
        /// <summary>
        /// APP版本
        /// </summary>
        public string AppVersion { get; set; } = string.Empty;

        /// <summary>
        /// 客户端类型
        /// </summary>
        public int ClientType { get; set; }

        /// <summary>
        /// 客户端MD5盐
        /// </summary>
        public string Salt { get; set; } = string.Empty;
    }
}
