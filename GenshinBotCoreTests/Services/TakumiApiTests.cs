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
    public class TakumiApiTests
    {
        [TestMethod()]
        public void GetGameAccountsTest()
        {
            var takumiApi = new TakumiApi(new TestUserManager(), new TakumiSecretHeaderGenerator
                (
                    new TestUserManager(), new TestSecretManager(), config =>
                    {
                        config.AppVersion = "2.16.1";
                        config.ClientType = 5;
                        config.Salt = "xV8v4Qu54lUKrEYFZkJhB8cuOh9Asafs";
                    }), 
                config =>
                {
                    config.BaseUrl = "https://api-takumi.mihoyo.com";
                    config.GameAccountsUrl = "/game_record/app/card/wapi/getGameRecordCard";
                });
            var result = takumiApi.GetGameAccounts("296714852").Result;

            Assert.IsTrue(result.IsSuccess);
        }
    }
}