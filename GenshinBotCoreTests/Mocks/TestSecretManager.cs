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
                "ltoken" => "cu3g96ud7QlZO42ZksKXf4nAgFO9PfUcLC3NhJct",
                "ticket" => "wwb8Rz0NCjAoTtYrDdk5P0inMS30JdnisfbPFzg1",
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
