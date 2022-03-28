using ConsoleGameEngine.Components;
using ConsoleGameEngine.GameObjects;

namespace ConsoleGameEngine.Physics.Arcade.GameObjects
{
    /// <summary>
    /// Represents a sprite that has a physics body.
    /// </summary>
    public abstract class SpriteWithBody<T> : Sprite where T : Body
    {
        private T? _body;
        /// <summary>
        /// The physics body associated with this sprite.
        /// </summary>
        public T Body
        {
            get => _body ?? throw new NullReferenceException();
            protected set => _body = value;
        }

        /// <summary>
        /// Resets the body to match the current frame.
        /// </summary>
        public void ResetBody()
        {
            Body.Offset.X = 0;
            Body.Offset.Y = 0;
            var clipping = Entity.Get<ClippingInfo>();
            Body.Size.Width = clipping.Width;
            Body.Size.Height = clipping.Height;
        }
    }
}
