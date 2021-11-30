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
        private readonly List<(SKRect, Type)> subViews;

        protected void AddPart(SKRect partRange, Func<SKCanvas, SKCanvas> renderer)
        {
            renderers.Add(new(partRange, renderer));
        }

        protected void AddSubView<T>(SKRect subViewRange) where T : ViewRenderer
        {
            var type = typeof(T);
            subViews.Add(new(subViewRange, type));
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

                var type = renderer.Item2;
                var constructor = type.GetConstructor(new Type[] { typeof(SKCanvas) });
                var subView = (ViewRenderer?)constructor?.Invoke(new object[] { canvas });
                if (subView is null) throw new Exception();
                subView.Render();

                canvas.Restore();
            }

            return canvas;
        }
    }
}
