using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenshinBotCore.Services.Tests
{
    [TestClass()]
    public class MihoyoAccountApiTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            var api = new MihoyoAccountApi();
            var response = api.Login("xxxxxxxxxxx", "xxxxxx").Result;
            Assert.IsFalse(string.IsNullOrEmpty(response.Payload?.Token));
        }
    }
}