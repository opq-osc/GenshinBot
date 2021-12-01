using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenshinBotCore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using GenshinBotCoreTests.Mocks;
using System.IO;
using System.Diagnostics;

namespace GenshinBotCore.View.Tests
{
    [TestClass()]
    public class ViewRendererTests
    {
        [TestMethod()]
        public void TestCardRendererTest()
        {
            var bitmap = new SKBitmap(1080, 2340);
            var canvas = new SKCanvas(bitmap);
            var renderer = new TestCardRender(canvas, new SKPoint(0, 0));

            renderer.Render();

            var image = SKImage.FromBitmap(bitmap);
            var file = File.Open("TestCardRendererTest.png", FileMode.Create);
            image.Encode(SKEncodedImageFormat.Png, 90).SaveTo(file);
            file.Close();

            var fileInfo = new FileInfo("TestCardRendererTest.png");
            Assert.IsTrue(fileInfo.Exists);
            Process.Start("explorer.exe", fileInfo.FullName);
        }
    }
}