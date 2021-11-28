using GenshinBotCoreTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var userManager = new TestUserManager();
            var user = userManager.GetUserByQQ(1137361788);
            Assert.IsNotNull(user);
            var result = takumiApi.GetGameAccounts(user.MihoyoId).Result;

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod()]
        public void GetIndexAsyncTest()
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
                    config.IndexUrl = "/game_record/app/genshin/api/index";
                });
            var userManager = new TestUserManager();
            var user = userManager.GetUserByQQ(1137361788);
            Assert.IsNotNull(user);
            var result = takumiApi.GetIndexAsync(user.GenshinUid, "cn_gf01").Result;

            Assert.IsTrue(result.IsSuccess);
        }
    }
}