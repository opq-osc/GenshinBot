using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenshinBotCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinBotCoreTests.Mocks;

namespace GenshinBotCore.Services.Tests
{
    [TestClass()]
    public class TakumiSecretHeaderGeneratorTests
    {
        [TestMethod()]
        public void GenerateSecretHeaderTest()
        {
            TakumiSecretHeaderGenerator takumiSecretHeaderGenerator = new TakumiSecretHeaderGenerator(new TestUserManager(), new TestSecretManager(),
                (config) =>
                    {
                        config.Salt = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
                        config.AppVersion = "2.16.1";
                        config.ClientType = 5;
                    });
            var header = takumiSecretHeaderGenerator.GenerateSecretHeader(new(), "querry");
            Assert.IsTrue(header.Any());
        }
    }
}