using ConsoleGameEngine.Caching;
using ConsoleGameEngine.Components;
using ConsoleGameEngine.Graphics;
using DefaultEcs;

namespace ConsoleGameEngine.GameObjects
{
    /// <summary>
    /// Creates game objects and optionally adds them to a Scene.
    /// </summary>
    public class GameObjectFactory
    {
        private readonly Scene _scene;
        private readonly CacheManager _cache;

        /// <summary>
        /// Creates a neww instance of the <see cref="GameObjectFactory"/>
        /// </summary>
        /// <param name="scene">The scene to add objects to.</param>
        /// <param name="cache">The cache manager to retreive content from.</param>
        public GameObjectFactory(Scene scene, CacheManager cache)
        {
            _scene = scene;
            _cache = cache;
        }

        /// <summary>
        /// Creates a new image game object and optionally adds it to the scene.
        /// </summary>
        /// <param name="key">The key that uniquely identifies the image in the cache.</param>
        /// <param name="x">The x cooridinate to place the image at.</param>
        /// <param name="y">The y coordinate to place the image at.</param>
        /// <param name="addToScene">Whether or not to add it to the scene.</param>
        /// <returns>An image game object.</returns>
        public ImageObject Image(string key, float x, float y, bool addToScene = true)
        {
            Image image = _cache.Images.Get(key);
            Entity entity = CreateEntity();
            var imageObject = new ImageObject(entity, image);
            imageObject.Position.X = x;
            imageObject.Position.Y = y;
            if (addToScene)
                _scene.AddChild(imageObject);
            else
                entity.Disable();
            return imageObject;
        }

        private Entity CreateEntity()
        {
            var entity = _scene.World.CreateEntity();
            int id = _scene.World.Count();
            entity.Set(new EntityIdentifier { Id = id });
            return entity;
        }
    }
}
