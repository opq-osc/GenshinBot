using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.View
{
    public class CardBackgroundRender : ViewRenderer
    {
        public CardBackgroundRender(SKCanvas canvas, SKPoint origin, SKSize size) : base(canvas, origin)
        {
            var fillRect = new SKRoundRect(SKRect.Create(origin.X, origin.Y, size.Width, size.Height), 8);
            var strokeRect = new SKRoundRect(SKRect.Create(origin.X + 2, origin.Y + 2, size.Width - 4, size.Height - 4), 8);
            using var fillPaint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse("#F8F8F8")
            };
            using var strokePaint = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColor.Parse("#CEBD91"),
                StrokeWidth = 4
            };
            canvas.DrawRoundRect(fillRect, fillPaint);
            canvas.DrawRoundRect(strokeRect, strokePaint);
        }
    }
}
