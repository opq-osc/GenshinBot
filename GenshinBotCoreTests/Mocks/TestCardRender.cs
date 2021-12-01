using GenshinBotCore.View;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCoreTests.Mocks
{
    public class TestCardRender : ViewRenderer
    {
        public TestCardRender(SKCanvas canvas, SKPoint origin) : base(canvas, origin)
        {
            canvas.Clear(SKColor.Parse("#EEEEEE"));
            this.AddSubView(SKRect.Create(100, 64, 880, 150), (c, o, s) =>
                  new CardBackgroundRender(c, o, s));
            this.AddSubView(SKRect.Create(100, 315, 880, 250), (c, o, s) =>
                  new CardBackgroundRender(c, o, s));
            this.AddSubView(SKRect.Create(100, 665, 880, 500), (c, o, s) =>
                  new CardBackgroundRender(c, o, s));
        }
    }
}
