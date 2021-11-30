using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBotCore.View
{
    public abstract class ViewRenderer
    {
        public ViewRenderer(SKCanvas canvas)
        {
            this.canvas = canvas;
            this.renderers = new();
            this.subViews = new();
        }

        private readonly SKCanvas canvas;
        private readonly List<(SKRect, Func<SKCanvas, SKCanvas>)> renderers;
        private readonly List<(SKRect, Func<SKCanvas, SKSize, ViewRenderer>)> subViews;

        protected void AddPart(SKRect partRange, Func<SKCanvas, SKCanvas> renderer)
        {
            renderers.Add(new(partRange, renderer));
        }

        protected void AddSubView(SKRect subViewRange, Func<SKCanvas, SKSize, ViewRenderer> builder)
        {
            subViews.Add(new(subViewRange, builder));
        }

        public SKCanvas Render()
        {
            foreach(var renderer in renderers)
            {
                canvas.Save();
                canvas.ClipRect(renderer.Item1);
                var clipCanvas = renderer.Item2(canvas);
                clipCanvas.Restore();
            }
            foreach (var renderer in subViews)
            {
                canvas.Save();
                canvas.ClipRect(renderer.Item1);

                var subView = renderer.Item2(canvas, new SKSize(renderer.Item1.Width, renderer.Item1.Height));
                
                subView.Render();

                canvas.Restore();
            }

            return canvas;
        }
    }
}
