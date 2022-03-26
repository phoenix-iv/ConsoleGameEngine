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
        /// The scene's camera.
        /// </summary>
        public Camera Camera { get; }
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
        private readonly List<GameObject> _children = new();
        /// <summary>
        /// The child game objects that are part of this scene.
        /// </summary>
        public IReadOnlyList<GameObject> Children => _children;

        private readonly SequentialSystem<GameTime> _system;

        /// <summary>
        /// Creates a new instance of <see cref="Scene"/>.
        /// </summary>
        /// <param name="game">The game that the scene will be a part of.</param>
        public Scene(Game game)
        {
            World = new World();
            Game = game;
            Add = new GameObjectFactory(this, game.Cache);
            Camera = new Camera();
            Load = new Loader(Game.Cache);
            _system = new SequentialSystem<GameTime>(new RenderSystem(World, Camera));
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
        /// When overridden performs any updates to the scene.
        /// </summary>
        /// <param name="time">The game time.</param>
        public virtual void Update(GameTime time)
        {
        }

        internal void UpdateInternal(GameTime time)
        {
            Update(time);
            _system.Update(time);
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
        public void AddChild(GameObject gameObject)
        {
            gameObject.Entity.Enable();
            _children.Add(gameObject);
        }

        /// <summary>
        /// Removes a child game object from this scene.
        /// </summary>
        /// <param name="gameObject">The object to remove.</param>
        public void RemoveChild(GameObject gameObject)
        {
            _children.Remove(gameObject);
            gameObject.Entity.Disable();
        }
    }
}