using ConsoleGameEngine.Components;
using ConsoleGameEngine.Graphics;
using DefaultEcs;
using System.Drawing;

namespace ConsoleGameEngine.GameObjects
{
    /// <summary>
    /// Represents a game object that displays an image.
    /// </summary>
    public class ImageObject : GameObject
    {
        /// <summary>
        /// The position of the image object.
        /// </summary>
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
