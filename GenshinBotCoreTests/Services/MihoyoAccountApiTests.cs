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