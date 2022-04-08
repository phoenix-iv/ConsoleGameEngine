using Box2DX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Box2D
{
    /// <summary>
    /// Represents a configuration for the Box2D physics system.
    /// </summary>
    public class Box2dPhysicsConfig
    {
        /// <summary>
        /// Allow bodies to "sleep" when not being interacted with.
        /// </summary>
        public bool AllowSleep { get; set; } = true;
        /// <summary>
        /// The number of characters per meter.
        /// </summary>
        public int CharsPerMeter { get; set; } = 2;
        /// <summary>
        /// The gravity of the physics world.  Defaults to Earth's gravity.
        /// </summary>
        public Vec2 Gravity { get; set; } = new Vec2(0, -9.807f);
        /// <summary>
        /// The number of meters per char.  Calculated based on <see cref="CharsPerMeter"/>.
        /// </summary>
        public float MetersPerChar => (float)1 / CharsPerMeter;
        /// <summary>
        /// The number of position iterations per step.
        /// </summary>
        public int PositionIterationsPerStep { get; set; } = 3;
        /// <summary>
        /// The number of velocity iterations per step.
        /// </summary>
        public int VelocityIterationsPerStep { get; set; } = 8;
        /// <summary>
        /// The physics world height in meters.
        /// </summary>
        public int WorldHeight { get; set; } = 200;
        /// <summary>
        /// The physics world width in meters.
        /// </summary>
        public int WorldWidth { get; set; } = 200;
    }
}
