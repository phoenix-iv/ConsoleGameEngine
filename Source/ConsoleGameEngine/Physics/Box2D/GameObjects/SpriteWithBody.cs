using Box2DX.Dynamics;
using ConsoleGameEngine.GameObjects;

namespace ConsoleGameEngine.Physics.Box2D
{
    /// <summary>
    /// Represents a sprite that has a physics body.
    /// </summary>
    public class SpriteWithBody : Sprite, IBox2dWithBody
    {
        private Body? _body;
        /// <summary>
        /// The physics body associated with this sprite.
        /// </summary>
        public Body Body
        {
            get => _body ?? throw new NullReferenceException();
            set => _body = value;
        }
    }
}
