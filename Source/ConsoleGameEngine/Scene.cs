using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Cameras;
using ConsoleGameEngine.GameObjects;
using ConsoleGameEngine.Loading;
using ConsoleGameEngine.Systems;
using DefaultEcs;
using DefaultEcs.System;

namespace ConsoleGameEngine
{
    /// <summary>
    /// Represents a game scene.
    /// </summary>
    public class Scene : IGameObjectContainer
    {
        /// <summary>
        /// A game object factory used to add objects to the scene.
        /// </summary>
        public GameObjectFactory Add { get; }
        /// <summary>
        /// The animation manager for this scene.
        /// </summary>
        public AnimationManager Animations { get; }
        /// <summary>
        /// Access to the Box2D Physics system.
        /// </summary>
        public Physics.Box2D.Box2dPhysics Box2dPhysics { get; }
        /// <summary>
        /// The scene's camera.
        /// </summary>
        public Camera Camera { get; }
        private readonly List<GameObject> _children = new();
        /// <summary>
        /// The child game objects that are part of this scene.
        /// </summary>
        public IReadOnlyList<GameObject> Children => _children;
        /// <summary>
        /// The game that this scene is a part of.
        /// </summary>
        public Game Game { get; }
        /// <summary>
        /// The loader used to load game content.
        /// </summary>
        public Loader Load { get; }
        /// <summary>
        /// The ECS world used to update game state.
        /// </summary>
        public World World { get; }

        private SequentialSystem<GameTime> _updatePipeline;
        private SequentialSystem<object?> _renderPipeline;

        /// <summary>
        /// Creates a new instance of <see cref="Scene"/>.
        /// </summary>
        /// <param name="game">The game that the scene will be a part of.</param>
        public Scene(Game game)
        {
            World = new World();
            Game = game;
            Add = new GameObjectFactory(this, game.Animations, game.Cache);
            Animations = game.Animations;
            Box2dPhysics = new Physics.Box2D.Box2dPhysics(this, game.Animations, game.Cache);
            Camera = new Camera();
            Load = new Loader(Game.Cache);
            _updatePipeline = BuildUpdatePipeline();
            _renderPipeline = BuildRenderPipeline();
        }

        /// <summary>
        /// When overridden, preloads any game content.
        /// </summary>
        public virtual void Preload()
        {

        }

        /// <summary>
        /// When overridden, creates the scene by adding objects and doing any other setup tasks.
        /// </summary>
        public virtual void Create()
        {

        }
        
        /// <summary>
        /// Injects the specified physics systems into the update system pipeline.
        /// </summary>
        /// <param name="physicsSystems">The physics systems to include in the update pipeline.</param>
        public void InjectPhysicsSystems(IEnumerable<ISystem<GameTime>>? physicsSystems)
        {
            _updatePipeline.Dispose();
            _updatePipeline = BuildUpdatePipeline(physicsSystems);
        }

        /// <summary>
        /// Renders the scene to the screen.
        /// </summary>
        public void Render()
        {
            _renderPipeline.Update(null);
        }

        /// <summary>
        /// When overridden performs any updates to the scene.
        /// </summary>
        /// <param name="time">The game time.</param>
        public virtual void Update(GameTime time)
        {

        }

        internal void UpdateInternal(GameTime time)
        {
            Update(time);
            _updatePipeline.Update(time);
        }

        /// <summary>
        /// When overridden, performs any tasks needed to shutdown a scene.
        /// </summary>
        public virtual void Shutdown()
        {

        }

        /// <summary>
        /// Adds a child game object to this scene.
        /// </summary>
        /// <param name="gameObject">The object to add.</param>
        public void AddGameObject(GameObject gameObject)
        {
            gameObject.Entity.Enable();
            _children.Add(gameObject);
        }

        /// <summary>
        /// Gets the child game objects of this scene.
        /// </summary>
        /// <returns>The child game objects of this scene.</returns>
        public IEnumerable<GameObject> GetGameObjects() => Children;

        /// <summary>
        /// Removes a child game object from this scene.
        /// </summary>
        /// <param name="gameObject">The object to remove.</param>
        public void RemoveGameObject(GameObject gameObject)
        {
            _children.Remove(gameObject);
            gameObject.Entity.Disable();
        }

        private SequentialSystem<object?> BuildRenderPipeline()
        {
            return new SequentialSystem<object?>(new RenderSystem(World, Camera));
        }

        private SequentialSystem<GameTime> BuildUpdatePipeline(IEnumerable<ISystem<GameTime>>? physicsSystems = null)
        {
            var systems = new List<ISystem<GameTime>>();
            if (physicsSystems != null)
            {
                systems.AddRange(physicsSystems);
            }
            systems.Add(new AnimationSystem(World));
            return new SequentialSystem<GameTime>(systems);
        }
    }
}