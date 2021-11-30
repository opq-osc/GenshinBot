using GenshinBotCore.View;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCoreTests.Mocks
{
    public class TestRender1 : ViewRenderer
    {
        public TestRender1(SKCanvas canvas) : base(canvas)
        {
            this.AddPart(SKRect.Create(0, 100, 100, 100), canvas =>
            {
                canvas.Clear(SKColors.Red);
                return canvas;
            });
            this.AddPart(SKRect.Create(100, 0, 100, 100), canvas =>
            {
                canvas.Clear(SKColors.Green);
                return canvas;
            });
        }
    }
}
