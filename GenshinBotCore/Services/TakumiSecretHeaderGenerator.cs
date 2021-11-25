using GenshinBotCore.Configs;
using GenshinBotCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services
{
    public class TakumiSecretHeaderGenerator : ISecreatHeaderGenerator
    {
        public TakumiSecretHeaderGenerator(Action<TakumiSecretHeaderGeneratorConfiguration> config)
        {
            this.config = new();
            config.Invoke(this.config);
        }

        private readonly TakumiSecretHeaderGeneratorConfiguration config;

        public IDictionary<string, string> GenerateSecretHeader(string query)
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
