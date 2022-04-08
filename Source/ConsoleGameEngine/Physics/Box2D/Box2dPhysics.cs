using Box2DX.Common;
using Box2DX.Collision;
using Box2DX.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Caching;
using ConsoleGameEngine.Physics.Box2D.Systems;
using ConsoleGameEngine.Physics.Box2D.GameObjects;

namespace ConsoleGameEngine.Physics.Box2D
{
    /// <summary>
    /// Represents the Box2D physics system.
    /// </summary>
    public class Box2dPhysics
    {
        /// <summary>
        /// The factory used to add game objects to the physics system.
        /// </summary>
        public Box2dGameObjectFactory Add { get; }
        /// <summary>
        /// The number of characters per meter.
        /// </summary>
        public int CharsPerMeter { get; private set; }
        /// <summary>
        /// The gravity of the physics world.
        /// </summary>
        public Vec2 Gravity { get; private set; }
        /// <summary>
        /// The number of meters per char.  Calculated based on <see cref="CharsPerMeter"/>.
        /// </summary>
        public float MetersPerChar { get; private set; }
        /// <summary>
        /// The number of position iterations per step.
        /// </summary>
        public int PositionIterationsPerStep { get; set; }
        /// <summary>
        /// The number of velocity iterations per step.
        /// </summary>
        public int VelocityIterationsPerStep { get; set; }
        private World? _world;
        /// <summary>
        /// The physics world.
        /// </summary>
        public World World => _world ?? throw new NullReferenceException(); 
        /// <summary>
        /// The physics world height in meters.
        /// </summary>
        public int WorldHeight { get; private set; }
        /// <summary>
        /// The physics world width in meters.
        /// </summary>
        public int WorldWidth { get; private set; }

        private readonly Scene _scene;

        /// <summary>
        /// Creates a new instance of <see cref="Box2dPhysics"/>.
        /// </summary>
        /// <param name="scene">The scene that this belongs to.</param>
        /// <param name="animationManager">The game level animation manager.</param>
        /// <param name="cache">The game level cache manager.</param>
        public Box2dPhysics(Scene scene, AnimationManager animationManager, CacheManager cache)
        {
            _scene = scene;
            Add = new Box2dGameObjectFactory(this, scene, animationManager, cache);
        }

        /// <summary>
        /// Initializes the Box2D physics system with the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void Initialize(Box2dPhysicsConfig config)
        {
            CharsPerMeter = config.CharsPerMeter;
            Gravity = config.Gravity;
            MetersPerChar = config.MetersPerChar;
            PositionIterationsPerStep = config.PositionIterationsPerStep;
            VelocityIterationsPerStep = config.VelocityIterationsPerStep;
            WorldWidth = config.WorldWidth;
            WorldHeight = config.WorldHeight;
            var worldBounds = new AABB();
            worldBounds.LowerBound.SetZero();
            worldBounds.UpperBound = new Vec2(config.WorldWidth, config.WorldHeight);
            _world = new World(worldBounds, config.Gravity, config.AllowSleep);
        }

        /// <summary>
        /// Starts the physics system running.
        /// </summary>
        public void Start()
        {
            _scene.InjectPhysicsSystems(new DefaultEcs.System.ISystem<GameTime>[] { new PhysicsSystem(_scene.World, this) });
        }

        /// <summary>
        /// Transforms the specified screen point to a world point.
        /// </summary>
        /// <param name="screenPoint">The screen point.</param>
        /// <returns>The world point.</returns>
        public Vec2 ScreenToWorldPoint(PointF screenPoint)
        {
            return new Vec2(screenPoint.X * MetersPerChar, WorldHeight - (screenPoint.Y * MetersPerChar));
        }

        /// <summary>
        /// Transforms the specified screen point to a world point.
        /// </summary>
        /// <param name="screenPoint">The screen point.</param>
        /// <returns>The world point.</returns>
        public Vec2 ScreenToWorldPoint(Components.Position screenPoint)
        {
            return ScreenToWorldPoint(new PointF(screenPoint.X, screenPoint.Y));
        }

        /// <summary>
        /// Transforms the specified world point to a screen point.
        /// </summary>
        /// <param name="worldPoint">The world point.</param>
        /// <returns>The screen point.</returns>
        public PointF WorldToScreenPoint(Vec2 worldPoint)
        {
            return new PointF(worldPoint.X * CharsPerMeter, (WorldHeight - worldPoint.Y) * CharsPerMeter);
        }
    }
}
