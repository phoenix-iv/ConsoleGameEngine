using System.Numerics;

namespace ConsoleGameEngine.Components.Physics.Arcade
{
    /// <summary>
    /// Contains data about a physics body that moves.
    /// </summary>
    public struct DynamicBodyInfo
    {
        /// <summary>
        /// The current velocity of the body.
        /// </summary>
        public Vector2 Velocity;
        /// <summary>
        /// The calculated new position of the body.
        /// </summary>
        public Position NewPosition;
    }
}
