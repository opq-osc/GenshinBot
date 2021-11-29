using GenshinBotCore.Configs;
using GenshinBotCore.Extensions;
using System.Security.Cryptography;
using System.Text;

namespace GenshinBotCore.Services
{
    public class TakumiSecretHeaderGenerator : ISecretHeaderGenerator
    {
        public TakumiSecretHeaderGenerator(IUserManager userManager, ISecretManager secretManager,
            Action<TakumiSecretHeaderGeneratorConfiguration> config)
        {
            this.config = new();
            config.Invoke(this.config);
            this.secretManager = secretManager;
            this.userManager = userManager;
        }

        private readonly TakumiSecretHeaderGeneratorConfiguration config;
        private readonly ISecretManager secretManager;
        private readonly IUserManager userManager;

        ///<inheritdoc/>
        public IDictionary<string, string> GenerateSecretHeader(int userId, string query)
        {
            var result = new Dictionary<string, string>();

            var time = DateTime.Now.ToShortTimeStamp();
            var rand = GenerateRandomAsciiString(6);

            var raw = $"salt={config.Salt}&t={time}&r={rand}&b=&q={query}";

            using var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(raw));
            var hashStr = string.Join(string.Empty, hash.Select(d => d.ToString("x2")));

            var headerValue = $"{time},{rand},{hashStr}";

            result.Add("DS", headerValue);

            secretManager.Bind(userId.ToString());
            var token = secretManager.GetSymmetricSecret("ltoken");
            var ticket = secretManager.GetSymmetricSecret("ticket");
            var mihoyoId = userManager.GetUserById(userId)?.MihoyoId ?? throw new InvalidOperationException("用户不存在");

            var cookieDict = new Dictionary<string, string>()
            {
                { "ltoken", token }, { "ltuid", mihoyoId }, { "login_ticket", ticket }
            };

            var cookie = string.Join(string.Empty, cookieDict.Select(c => $"{c.Key}={c.Value};"));
            result.Add("Cookie", cookie);
            result.Add("x-rpc-app_version", config.AppVersion);
            result.Add("x-rpc-client_type", config.ClientType.ToString());

            return result;
        }

        private static string GenerateRandomAsciiString(int length)
        {
            var seed = "0123456789abcdefghijklmnopqrstuvwxyz";
            var randomSeq = from _ in Enumerable.Range(0, length)
                            let index = Random.Shared.Next(0, seed.Length)
                            select seed[index];
            return string.Join(string.Empty, randomSeq);
        }
    }
}
