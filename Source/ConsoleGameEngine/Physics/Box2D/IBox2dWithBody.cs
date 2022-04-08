using Box2DX.Dynamics;

namespace ConsoleGameEngine.Physics.Box2D
{
    /// <summary>
    /// Represents an object that has a Box2D body.
    /// </summary>
    public interface IBox2dWithBody
    {
        /// <summary>
        /// The Box2D physics body.
        /// </summary>
        public Body Body { get; set; }
    }
}
