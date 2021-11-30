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
            this.AddSubView<TestRender1>(SKRect.Create(0, 0, 200, 200));
            this.AddSubView<TestRender2>(SKRect.Create(0, 0, 200, 200));
        }
    }
}
