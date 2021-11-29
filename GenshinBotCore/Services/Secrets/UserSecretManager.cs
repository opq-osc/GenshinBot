using GenshinBotCore.Configs;
using GenshinBotCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace GenshinBotCore.Services
{
    public class UserSecretManager : ISecretManager
    {
        /// <summary>
        /// 初始化<see cref="UserSecretManager"/>的新实例
        /// </summary>
        /// <param name="dbContext">数据库</param>
        /// <param name="logger">日志</param>
        /// <param name="configuration">配置</param>
        public UserSecretManager(ApplicationDbContext dbContext,
                                 ILogger<UserSecretManager> logger,
                                 Action<SecretManagerConfiguration> configuration)
        {
            var config = new SecretManagerConfiguration();
            configuration.Invoke(config);
            symmetricKey = config.SymmetricKey;
            symmetricSalt = config.SymmetricSalt;
            hashSalt = config.HashSalt;
            secrets = dbContext.Set<UserSecret>();
            users = dbContext.Set<User>();
            db = dbContext;
            this.logger = logger;
        }

        private readonly ILogger logger;
        private readonly DbContext db;
        private readonly DbSet<UserSecret> secrets;
        private readonly DbSet<User> users;
        private readonly string symmetricKey;
        private readonly string symmetricSalt;
        private readonly string hashSalt;
        private User? user;

        /// <summary>
        /// 设置该密钥管理器的用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SetUser(string userId)
        {
            var userQuery = users.Where(u => u.Id.ToString() == userId);

            if (!userQuery.Any())
            {
                throw new InvalidOperationException("指定用户不存在");
            }
            user = userQuery.SingleOrDefault();
        }

        /// <summary>
        /// 获取指定密钥
        /// </summary>
        /// <param name="secretKey">密钥名称</param>
        /// <returns>指定的密钥</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string GetSymmetricSecret(string secretKey)
        {
            if (user is null) throw new InvalidOperationException("未绑定用户");
            var secretQuery = secrets.Where(s => s.User == user && s.Name == secretKey)
                .AsNoTracking();
            if (!secretQuery.Any()) return string.Empty;
            logger.LogInformation("trying to get {secretName} secret of {userEmail}..."
                , secretKey, user.QQ);
            var secret = secretQuery.Single();
            return AesDecryptString(secret.Content);
        }

        /// <summary>
        /// 取密钥哈希
        /// </summary>
        /// <param name="secret">密钥原文</param>
        /// <returns>密钥哈希</returns>
        public string HashSecret(string secret)
        {
            using var sha256 = SHA256.Create();
            var saltedSecret = $"{hashSalt}{secret}{hashSalt}";
            var secretBytes = Encoding.ASCII.GetBytes(saltedSecret);
            var hash = sha256.ComputeHash(secretBytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// 存储密钥
        /// </summary>
        /// <param name="secretKey">密钥名称</param>
        /// <param name="secret">密钥原文</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void StorageSecret(string secretKey, string secret)
        {
            if (user is null) throw new InvalidOperationException("未绑定用户");
            var secretQuery = secrets.Where(s => s.User == user && s.Name == secretKey);
            var encryptdSecret = AesEncryptString(secret);

            if (secretQuery.Any())
            {
                var oldSecret = secretQuery.Single();
                oldSecret.Content = encryptdSecret;
            }
            else
            {
                secrets.Add(new()
                {
                    User = user,
                    Name = secretKey,
                    Content = encryptdSecret
                });
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 比对密码与哈希
        /// </summary>
        /// <param name="rawSecret">密码</param>
        /// <param name="hash">哈希</param>
        /// <returns>是否匹配</returns>
        public bool HashCompare(string rawSecret, string hash)
        {
            return hash == HashSecret(rawSecret);
        }

        private string AesEncryptString(string content)
        {
            using var sha256 = SHA256.Create();
            using var aes = Aes.Create();
            using var ms = new MemoryStream();
            var timeBytes = BitConverter.GetBytes(DateTime.Now.ToBinary());
            using var salt = new MemoryStream();
            salt.Write(timeBytes);
            salt.Write(Encoding.ASCII.GetBytes(symmetricSalt));
            var saltBytes = sha256.ComputeHash(salt.ToArray())[..16];
            var rawKeyBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(symmetricKey))[..16];
            var keyBytes = new byte[16];
            for (int i = 0; i < 16; i++) keyBytes[i] = (byte)(saltBytes[i] ^ rawKeyBytes[i]);
            aes.Key = keyBytes;
            ms.Write(timeBytes);
            ms.Write(aes.IV, 0, aes.IV.Length);
            using var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using var encryptWriter = new StreamWriter(cryptoStream);
            encryptWriter.Write(content);
            encryptWriter.Flush();
            encryptWriter.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        private string AesDecryptString(string content)
        {
            using var sha256 = SHA256.Create();
            using var aes = Aes.Create();
            using var ms = new MemoryStream(Convert.FromBase64String(content));
            byte[] timeBytes = new byte[8];
            byte[] ivBytes = new byte[aes.IV.Length];
            ms.Read(timeBytes, 0, timeBytes.Length);
            ms.Read(ivBytes, 0, ivBytes.Length);
            using var salt = new MemoryStream();
            salt.Write(timeBytes);
            salt.Write(Encoding.ASCII.GetBytes(symmetricSalt));
            var saltBytes = sha256.ComputeHash(salt.ToArray())[..16];
            var rawKeyBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(symmetricKey))[..16];
            var keyBytes = new byte[16];
            for (int i = 0; i < 16; i++) keyBytes[i] = (byte)(saltBytes[i] ^ rawKeyBytes[i]);
            using var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(keyBytes, ivBytes), CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        void ISecretManager.Bind(string userId)
        {
            SetUser(userId);
        }
    }
}
