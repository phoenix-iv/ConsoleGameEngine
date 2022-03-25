using ConsoleGameEngine.Cameras;
using ConsoleGameEngine.GameObjects;
using ConsoleGameEngine.Loading;
using ConsoleGameEngine.Systems;
using DefaultEcs;
using DefaultEcs.System;

namespace ConsoleGameEngine
{
    public class Scene : IGameObjectContainer
    {
        public GameObjectFactory Add { get; }
        public Camera Camera { get; }
        public Game Game { get; }
        public Loader Load { get; }
        public World World { get; }
        private List<GameObject> _children = new List<GameObject>();
        public IReadOnlyList<GameObject> Children => _children;

        private readonly SequentialSystem<TimeSpan> _system;

        public Scene(Game game)
        {
            World = new World();
            Game = game;
            Add = new GameObjectFactory(this, game.Cache);
            Camera = new Camera();
            Load = new Loader(Game.Cache);
            _system = new SequentialSystem<TimeSpan>(new RenderSystem(World, Camera));
        }

        public virtual void Preload()
        {

        }

        public virtual void Create()
        {

        }

        public virtual void Update(TimeSpan delta)
        {
            _system.Update(delta);
        }

        public virtual void Shutdown()
        {

        }

        public void AddChild(GameObject gameObject)
        {
            _children.Add(gameObject);
        }

        public void RemoveChild(GameObject gameObject)
        {
            _children.Remove(gameObject);
        }
    }
}