using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenshinBotCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.Services.Tests
{
    [TestClass()]
    public class TakumiSecretHeaderGeneratorTests
    {
        [TestMethod()]
        public void GenerateSecretHeaderTest()
        {
            TakumiSecretHeaderGenerator takumiSecretHeaderGenerator = new TakumiSecretHeaderGenerator((config) =>
            {
                config.Salt = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
            });
            var header = takumiSecretHeaderGenerator.GenerateSecretHeader();
            Assert.IsTrue(header.Any());
        }
    }
}