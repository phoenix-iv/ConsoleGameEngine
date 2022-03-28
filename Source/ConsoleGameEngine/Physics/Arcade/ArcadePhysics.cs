using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Caching;
using ConsoleGameEngine.Physics.Arcade.GameObjects;
using ConsoleGameEngine.Physics.Arcade.Systems;
using DefaultEcs.System;

namespace ConsoleGameEngine.Physics.Arcade
{
    /// <summary>
    /// Represents an arcade physics system.
    /// </summary>
    public class ArcadePhysics
    {
        /// <summary>
        /// The factory used to add game objects to the arcade physics system.
        /// </summary>
        public ArcadePhysicsGameObjectFactory Add { get; }
        private Scene _scene;

        /// <summary>
        /// Creates a new instance of <see cref="ArcadePhysics"/>.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="animationManager">The game-level animation manager.</param>
        /// <param name="cache">The game-level cache manager.</param>
        public ArcadePhysics(Scene scene, AnimationManager animationManager, CacheManager cache)
        {
            _scene = scene;
            Add = new ArcadePhysicsGameObjectFactory(_scene, animationManager, cache);
        }

        /// <summary>
        /// Starts the physics system running.
        /// </summary>
        public void Start()
        {
            _scene.RebuildSystem(new ISystem<GameTime>[] { new VelocitySystem(_scene.World), new PositionSystem(_scene.World) });
        }
    }
}
