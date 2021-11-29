using GenshinBotCore.Services;
using System;

namespace GenshinBotCoreTests.Mocks
{
    public class TestSecretManager : ISecretManager
    {
        public void Bind(string key)
        {

        }

        public string GetSymmetricSecret(string secretName)
        {
            return secretName switch
            {
                "ltoken" => "",
                "ticket" => "",
                _ => throw new NotImplementedException()
            };
        }

        public bool HashCompare(string rawSecret, string hash)
        {
            throw new NotImplementedException();
        }

        public string HashSecret(string secret)
        {
            throw new NotImplementedException();
        }

        public void StorageSecret(string secretName, string secret)
        {
            throw new NotImplementedException();
        }
    }
}
