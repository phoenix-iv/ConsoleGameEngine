using ConsoleGameEngine.Components;
using System.Drawing;

namespace ConsoleGameEngine.Physics.Arcade.Components
{
    /// <summary>
    /// Contains data for a physics body.
    /// </summary>
    public struct BodyInfo
    {
        /// <summary>
        /// The body postition relative to its owner.
        /// </summary>
        public Position Offset;
        /// <summary>
        /// The size of the body.
        /// </summary>
        public Size Size;
    }

}
