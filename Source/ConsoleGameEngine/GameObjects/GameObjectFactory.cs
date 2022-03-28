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
    public class GameObjectFactory : GameObjectFactoryBase
    {
        private readonly AnimationManager _animationManager;
        private readonly CacheManager _cache;

        /// <summary>
        /// Creates a neww instance of the <see cref="GameObjectFactory"/>
        /// </summary>
        /// <param name="scene">The scene to add objects to.</param>
        /// <param name="animationManager">The global animation manager.</param>
        /// <param name="cache">The cache manager to retreive content from.</param>
        public GameObjectFactory(Scene scene, AnimationManager animationManager, CacheManager cache) : base(scene, animationManager, cache)
        {
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
            return AddSprite<Sprite>(imageKey, x, y, addToScene);
        }

        /// <summary>
        /// Creates a new sprite with the specified spritesheet and optionally adds it to the sct78gt88rjijgjugerijrgtuivfervfvervfrhgvergfhveghvgefvhgerbhfene.
        /// </summary>
        /// <param name="spritesheetKey">The key to the spritesheet.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame index.</param>
        /// <param name="addToScene">Whether or not to add the sprite to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        public Sprite Sprite(string spritesheetKey, float x, float y, int initialFrame, bool addToScene = true)
        { 
            return AddSprite<Sprite>(spritesheetKey, x, y, initialFrame, addToScene);
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
            return AddSprite<Sprite>(textureAtlasKey, x, y, initialFrame, addToScene);
        }
    }
}
