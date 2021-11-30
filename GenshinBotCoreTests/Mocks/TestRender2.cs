using GenshinBotCore.View;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCoreTests.Mocks
{
    public class TestRender2 : ViewRenderer
    {
        public TestRender2(SKCanvas canvas) : base(canvas)
        {
            this.AddPart(SKRect.Create(0, 0, 100, 100), canvas =>
            {
                canvas.Clear(SKColors.Blue);
                return canvas;
            });
            this.AddPart(SKRect.Create(100, 100, 100, 100), canvas =>
            {
                canvas.Clear(SKColors.Yellow);
                return canvas;
            });
        }
    }
}
