using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Tools.ImageEditor
{
    public class ImageDrawer : IDrawable
    {
        private struct ColorCharPoint
        {
            public ColorChar Char;
            public int CharX;
            public int CharY;
            public float X;
            public float Y;
        }

        private SizeF _charSize;
        public PointF HoverPoint { get; set; }
        private PointF _lastHoverPoint;
        private Graphics.Image? _image;
        public Graphics.Image? Image 
        {
            get => _image; 
            set
            {
                _image = value;
                NeedsRedraw = true;
            }
        }
        public bool NeedsRedraw { get; set; } = true;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Image == null)
                return;

            canvas.StrokeColor = Colors.Gray;
            var font = new Microsoft.Maui.Graphics.Font("Consolas");
            canvas.Font = font;
            float fontSize = 20;
            canvas.FontSize = fontSize;

            if (_charSize == default)
            {
                _charSize = canvas.GetStringSize("A", font, fontSize);
                _charSize.Width *= 1.3f;
                _charSize.Height *= 2.0f;
            }

            for (int cy = 0; cy < Image.Height; cy++)
            {
                for (int cx = 0; cx < Image.Width; cx++)
                {
                    DrawChar(canvas, Image.Data[cy][cx], cx, cy, false);
                }
            }

            if (HoverPoint != default && HoverPoint != _lastHoverPoint)
            {
                ColorCharPoint? charPoint = PointToCharPoint(HoverPoint);

                if (_lastHoverPoint != default)
                {
                    ColorCharPoint? lastCharPoint = PointToCharPoint(_lastHoverPoint);
                    if (lastCharPoint != null)  
                    {
                        if (lastCharPoint.Value.CharX != charPoint?.CharX || lastCharPoint.Value.CharY != charPoint?.CharY)
                            DrawChar(canvas, lastCharPoint.Value.Char, lastCharPoint.Value.X, lastCharPoint.Value.Y, false);
                    }
                }

                if (charPoint == null)
                    return;

                DrawChar(canvas, charPoint.Value.Char, charPoint.Value.X, charPoint.Value.Y, true);
                _lastHoverPoint = HoverPoint;
                HoverPoint = default;
            }

        }

        private void DrawChar(ICanvas canvas, ColorChar c, int cx, int cy, bool isHighlighted)
        {
            float x = cx * _charSize.Width;
            float y = cy * _charSize.Height;
            DrawChar(canvas, c, x, y, isHighlighted);
        }

        private void DrawChar(ICanvas canvas, ColorChar c, float x, float y, bool isHighlighted)
        {
            var rect = new RectF(x, y, _charSize.Width, _charSize.Height);
            canvas.FillColor = (c.BackColor == null) ? Colors.Purple : ConsoleColorConvert.ToMauiColor(c.BackColor.Value);
            if(isHighlighted)
            {
                canvas.FillColor = Colors.Pink;
            }
            canvas.FillRectangle(rect);
            canvas.DrawRectangle(rect);
            canvas.FontColor = ConsoleColorConvert.ToMauiColor(c.ForeColor);
            canvas.DrawString(c.Char.ToString(), x + 1, y, _charSize.Width, _charSize.Height, HorizontalAlignment.Left, VerticalAlignment.Top);
        }

        private ColorCharPoint? PointToCharPoint(PointF point)
        {
            if (Image == null)
                throw new InvalidOperationException($"{nameof(PointToCharPoint)} called with a null {nameof(Image)}.");

            int cx = (int)(point.X / _charSize.Width);
            int cy = (int)(point.Y / _charSize.Height);
            float x = cx * _charSize.Width;
            float y = cy * _charSize.Height;
            
            if (cx >= Image.Width || cy >= Image.Height)
                return default;

            return new ColorCharPoint
            {
                Char = Image.Data[cy][cx],
                CharX = cx,
                CharY = cy,
                X = x,
                Y = y
            };
        }

        private void Redraw(ICanvas canvas)
        {
            if (Image == null)
                throw new InvalidOperationException($"{nameof(Redraw)} called with a null {nameof(Image)}.");
            

            NeedsRedraw = false;
        }
    }
}
