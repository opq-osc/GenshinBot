using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.View
{
    public class CardTitleRender : ViewRenderer
    {
        public CardTitleRender(SKCanvas canvas, SKPoint origin, SKSize size) : base(canvas, origin)
        {
            // width: 256 height: 80
            using var rectPaint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse("#CEBD91")
            };
            canvas.DrawRect(origin.X, origin.Y + size.Height / 2 - 15, 12, 30, rectPaint);
        }
    }
}
