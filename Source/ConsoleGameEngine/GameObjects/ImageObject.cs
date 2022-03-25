using DefaultEcs;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGameEngine.Components;

namespace ConsoleGameEngine.GameObjects
{
    public class ImageObject : GameObject
    {
        public ref Position Position
        {
            get => ref Entity.Get<Position>();
        }

        internal ImageObject(Entity entity, Image image) : base(entity)
        {
            Entity.Set(new Position());
            Entity.Set(new Size(image.Width, image.Height));
            Entity.Set(new ClippingInfo { X = 0, Y = 0, Height = image.Height, Width = image.Width });
            Entity.Set(image);
        }
    }
}
