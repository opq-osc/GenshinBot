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
        public ViewRenderer(SKCanvas canvas, SKPoint origin)
        {
            this.canvas = canvas;
            this.origin = origin;
            this.renderers = new();
            this.subViews = new();
        }

        static ViewRenderer()
        {
            TypefaceNormal = SKTypeface.FromFile("Assets/segoeui.ttf");
            TypefaceBold = SKTypeface.FromFile("Assets/segoeuib.ttf");
        }

        protected static SKTypeface TypefaceNormal { get; private set; }
        protected static SKTypeface TypefaceBold { get; private set; }

        private readonly SKCanvas canvas;
        private readonly SKPoint origin;
        private readonly List<(SKRect, Func<SKCanvas, SKPoint, SKCanvas>)> renderers;
        private readonly List<(SKRect, Func<SKCanvas, SKPoint, SKSize, ViewRenderer>)> subViews;

        protected void AddPart(SKRect partRange, Func<SKCanvas, SKPoint, SKCanvas> renderer)
        {
            renderers.Add(new(partRange, renderer));
        }

        protected void AddSubView(SKRect subViewRange, Func<SKCanvas, SKPoint, SKSize, ViewRenderer> builder)
        {
            subViews.Add(new(subViewRange, builder));
        }

        public SKCanvas Render()
        {
            foreach(var renderer in renderers)
            {
                canvas.Save();
                canvas.ClipRect(renderer.Item1);
                var clipCanvas = renderer.Item2(canvas, origin + renderer.Item1.Location);
                clipCanvas.Restore();
            }
            foreach (var renderer in subViews)
            {
                canvas.Save();
                canvas.ClipRect(renderer.Item1);

                var subView = renderer.Item2(canvas, origin + renderer.Item1.Location, new SKSize(renderer.Item1.Width, renderer.Item1.Height));
                
                subView.Render();
                canvas.Restore();
            }

            return canvas;
        }
    }
}
