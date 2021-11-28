namespace GenshinBotCore.Configs
{
    /// <summary>
    /// 机密管理器配置
    /// </summary>
    public class SecretManagerConfiguration
    {
        /// <summary>
        /// 对称加密密钥
        /// </summary>
        public string SymmetricKey { get; set; } = null!;
        /// <summary>
        /// 对称加密盐
        /// </summary>
        public string SymmetricSalt { get; set; } = null!;
        /// <summary>
        /// 哈希盐
        /// </summary>
        public string HashSalt { get; set; } = null!;
    }
}
