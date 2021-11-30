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
        public void SingleRendererTest1()
        {
            var bitmap = new SKBitmap(200, 200);
            var canvas = new SKCanvas(bitmap);
            var renderer = new TestRender1(canvas);

            renderer.Render();

           
            var image = SKImage.FromBitmap(bitmap);
            var file = File.Open("SingleRendererTest1.png", FileMode.Create);
            image.Encode(SKEncodedImageFormat.Png, 90).SaveTo(file);
            file.Close();

            Assert.IsTrue(File.Exists("SingleRendererTest1.png"));
        }

        [TestMethod()]
        public void SingleRendererTest2()
        {
            var bitmap = new SKBitmap(200, 200);
            var canvas = new SKCanvas(bitmap);
            var renderer = new TestRender2(canvas);

            renderer.Render();


            var image = SKImage.FromBitmap(bitmap);
            var file = File.Open("SingleRendererTest2.png", FileMode.Create);
            image.Encode(SKEncodedImageFormat.Png, 90).SaveTo(file);
            file.Close();

            Assert.IsTrue(File.Exists("SingleRendererTest2.png"));
        }

        [TestMethod()]
        public void CombinedRendererTest()
        {
            var bitmap = new SKBitmap(200, 200);
            var canvas = new SKCanvas(bitmap);
            var renderer = new TestRender3(canvas);

            renderer.Render();

            var image = SKImage.FromBitmap(bitmap);
            var file = File.Open("SingleRendererTest3.png", FileMode.Create);
            image.Encode(SKEncodedImageFormat.Png, 90).SaveTo(file);
            file.Close();

            var fileInfo = new FileInfo("SingleRendererTest3.png");
            Assert.IsTrue(File.Exists("SingleRendererTest3.png"));
            Process.Start("explorer.exe", fileInfo.FullName);
        }
    }
}