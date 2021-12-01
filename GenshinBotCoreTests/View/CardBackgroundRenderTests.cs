using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenshinBotCore.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using System.IO;
using System.Diagnostics;

namespace GenshinBotCore.View.Tests
{
    [TestClass()]
    public class CardBackgroundRenderTests
    {
        [TestMethod()]
        public void CardRenderTest()
        {
            var bitmap = new SKBitmap(1920, 1080);
            var canvas = new SKCanvas(bitmap);
            var renderer = new CardBackgroundRender(canvas, new SKPoint(0,0), new SKSize(1920, 1080));

            renderer.Render();

            var image = SKImage.FromBitmap(bitmap);
            var file = File.Open("CardRenderTest.png", FileMode.Create);
            image.Encode(SKEncodedImageFormat.Png, 90).SaveTo(file);
            file.Close();

            var fileInfo = new FileInfo("CardRenderTest.png");
            Assert.IsTrue(File.Exists("CardRenderTest.png"));
            Process.Start("explorer.exe", fileInfo.FullName);
        }
    }
}