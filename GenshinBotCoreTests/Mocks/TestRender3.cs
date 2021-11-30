using GenshinBotCore.View;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCoreTests.Mocks
{
    public class TestRender3 : ViewRenderer
    {
        public TestRender3(SKCanvas canvas) : base(canvas)
        {
            this.AddSubView(SKRect.Create(0, 0, 200, 200), (canvas, _) => new TestRender1(canvas));
            this.AddSubView(SKRect.Create(0, 0, 200, 200), (canvas, _) => new TestRender2(canvas));
        }
    }
}
