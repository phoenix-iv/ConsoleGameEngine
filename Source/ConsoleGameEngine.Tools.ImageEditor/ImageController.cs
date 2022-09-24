using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Tools.ImageEditor
{
    internal class ImageController
    {
        private readonly GraphicsView _view;
        private readonly ImageDrawer _drawer;

        public ImageController(GraphicsView view)
        {
            _view = view;
            _drawer = (ImageDrawer)_view.Drawable;
        }

        public void Hover(TouchEventArgs e)
        {
            _drawer.HoverPoint = e.Touches.First();
            _view.Invalidate();
        }

        public void EndHover()
        {
            _drawer.HoverPoint = default;
            _view.Invalidate();
        }

        public void Redraw()
        {
            _view.Invalidate();
        }

        public void SetImage(Graphics.Image image)
        {
            _drawer.Image = image;
            _view.Invalidate();
        }
    }
}
