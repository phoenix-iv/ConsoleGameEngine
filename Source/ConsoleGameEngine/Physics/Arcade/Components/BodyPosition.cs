using ConsoleGameEngine.Components;

namespace ConsoleGameEngine.Physics.Arcade.Components
{
    /// <summary>
    /// Contains data about a body's position. 
    /// </summary>
    public struct BodyPosition
    {
        /// <summary>
        /// The body offset relative to its owner.
        /// </summary>
        public Position Offset;
        /// <summary>
        /// The calculated projected position of the body.
        /// </summary>
        public Position ProjectedPosition;
    }
}
