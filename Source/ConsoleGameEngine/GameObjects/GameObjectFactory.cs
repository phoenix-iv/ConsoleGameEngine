using ConsoleGameEngine.Animations;
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
        private readonly AnimationManager _animationManager;
        private readonly CacheManager _cache;

        /// <summary>
        /// Creates a neww instance of the <see cref="GameObjectFactory"/>
        /// </summary>
        /// <param name="scene">The scene to add objects to.</param>
        /// <param name="animationManager">The global animation manager.</param>
        /// <param name="cache">The cache manager to retreive content from.</param>
        public GameObjectFactory(Scene scene, AnimationManager animationManager, CacheManager cache)
        {
            _scene = scene;
            _animationManager = animationManager;
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
            AddToScene(imageObject, addToScene);
            return imageObject;
        }

        /// <summary>
        /// Creates a new sprite with the specified image and optionally adds it to the scene.
        /// </summary>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="addToScene">Whether or not to add the sprite to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        public Sprite Sprite(string imageKey, float x, float y, bool addToScene = true)
        {
            Image image = _cache.Images.Get(imageKey);
            Entity entity = CreateEntity();
            var sprite = new Sprite(entity, _animationManager, image);
            sprite.Position.X = x;
            sprite.Position.Y = y;
            sprite.SetFrame(0);
            AddToScene(sprite, addToScene);
            return sprite;
        }

        /// <summary>
        /// Creates a new sprite with the specified spritesheet and optionally adds it to the scene.
        /// </summary>
        /// <param name="spritesheetKey">The key to the spritesheet.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame index.</param>
        /// <param name="addToScene">Whether or not to add the sprite to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        public Sprite Sprite(string spritesheetKey, float x, float y, int initialFrame, bool addToScene = true)
        {
            Spritesheet spritesheet = _cache.Spritesheets.Get(spritesheetKey);
            Entity entity = CreateEntity();
            var sprite = new Sprite(entity, _animationManager, spritesheet);
            sprite.Position.X = x;
            sprite.Position.Y = y;
            sprite.SetFrame(initialFrame);
            AddToScene(sprite, addToScene);
            return sprite;
        }

        /// <summary>
        /// Creates a new sprite with the specified texture atlas and optionally adds it to the scene.
        /// </summary>
        /// <param name="textureAtlasKey">The key to the texture atlas.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame.  If null, the first frame in the atlas will be used.</param>
        /// <param name="addToScene">Whether or not to add the sprite to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        public Sprite Sprite(string textureAtlasKey, float x, float y, string? initialFrame, bool addToScene = true)
        {
            TextureAtlas atlas = _cache.TextureAtlases.Get(textureAtlasKey);
            Entity entity = CreateEntity();
            var sprite = new Sprite(entity, _animationManager, atlas);
            sprite.Position.X = x;
            sprite.Position.Y = y;
            if (initialFrame == null)
                initialFrame = atlas.Frames.First().FileName;
            sprite.SetFrame(initialFrame);
            AddToScene(sprite, addToScene);
            return sprite;
        }

        private void AddToScene(GameObject o, bool addToScene)
        {
            if (addToScene)
                _scene.AddChild(o);
            else
                o.Entity.Disable();
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
