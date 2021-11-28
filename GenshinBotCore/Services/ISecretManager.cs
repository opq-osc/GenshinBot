namespace GenshinBotCore.Services
{
    public interface ISecretManager
    {
        /// <summary>
        /// 绑定
        /// </summary>
        /// <param name="key">要绑定的对象</param>
        void Bind(string key);

        /// <summary>
        /// 获取指定名称的密钥
        /// </summary>
        /// <param name="secretName">密钥名称</param>
        /// <returns>密钥内容</returns>
        string GetSymmetricSecret(string secretName);

        /// <summary>
        /// 以对称加密方式存储密钥
        /// </summary>
        /// <param name="secretName">密钥名称</param>
        /// <param name="secret">密钥内容</param>
        void StorageSecret(string secretName, string secret);

        /// <summary>
        /// 获取密钥哈希值
        /// </summary>
        /// <param name="secret">密钥内容</param>
        /// <returns>密钥哈希</returns>
        string HashSecret(string secret);

        /// <summary>
        /// 哈希对比
        /// </summary>
        /// <param name="rawSecret">明文密码</param>
        /// <param name="hash">哈希值</param>
        /// <returns>是否匹配</returns>
        bool HashCompare(string rawSecret, string hash);
    }
}
