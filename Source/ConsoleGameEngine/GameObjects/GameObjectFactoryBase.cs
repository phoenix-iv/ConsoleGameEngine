using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Caching;
using ConsoleGameEngine.Components;
using ConsoleGameEngine.Graphics;
using DefaultEcs;

namespace ConsoleGameEngine.GameObjects
{
    /// <summary>
    /// A base class containing common methods for creating game objects.
    /// </summary>
    public abstract class GameObjectFactoryBase
    {
        private readonly Scene _scene;
        /// <summary>
        /// The game level animation manager
        /// </summary>
        protected readonly AnimationManager AnimationManager;
        /// <summary>
        /// The game level cache manager.
        /// </summary>
        protected readonly CacheManager Cache;

        /// <summary>
        /// Creates a new instance of <see cref="GameObjectFactoryBase"/>/
        /// </summary>
        /// <param name="scene">The scene to add objects to.</param>
        /// <param name="animationManager">The game level animation manager.</param>
        /// <param name="cache">The game level cache manager.</param>
        protected GameObjectFactoryBase(Scene scene, AnimationManager animationManager, CacheManager cache)
        {
            _scene = scene;
            AnimationManager = animationManager;
            Cache = cache;
        }

        /// <summary>
        /// Creates a new sprite at the specified coordinates and optionally adds it do the scene.
        /// </summary>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="addToScene">Whether or not to add it to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        private T AddSprite<T>(float x, float y, bool addToScene) where T : Sprite, new()
        {
            Entity entity = CreateEntity();
            var sprite = new T();
            sprite.Initialize(entity, AnimationManager);
            sprite.Position.X = x;
            sprite.Position.Y = y;
            AddToScene(sprite, addToScene);
            return sprite;
        }

        /// <summary>
        /// Creates a new sprite with the specified image and optionally adds it to the scene.
        /// </summary>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="addToScene">Whether or not to add the sprite to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        protected T AddSprite<T>(string imageKey, float x, float y, bool addToScene) where T : Sprite, new()
        {
            T sprite = AddSprite<T>(x, y, addToScene);
            Image image = Cache.Images.Get(imageKey);
            sprite.InitializeImage(image);
            sprite.SetFrame(0);
            return sprite;
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
        protected T AddSprite<T>(string spritesheetKey, float x, float y, int initialFrame, bool addToScene) where T : Sprite, new()
        {
            T sprite = AddSprite<T>(x, y, addToScene);
            Spritesheet spritesheet = Cache.Spritesheets.Get(spritesheetKey);
            sprite.InitializeSpritesheet(spritesheet);
            sprite.SetFrame(initialFrame);
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
        protected T AddSprite<T>(string textureAtlasKey, float x, float y, string? initialFrame, bool addToScene) where T : Sprite, new()
        {
            T sprite = AddSprite<T>(x, y, addToScene);
            TextureAtlas atlas = Cache.TextureAtlases.Get(textureAtlasKey);
            sprite.InitializeTextureAtlas(atlas);
            if (initialFrame == null)
                initialFrame = atlas.Frames.First().FileName;
            sprite.SetFrame(initialFrame);
            return sprite;
        }

        /// <summary>
        /// Optionally adds the specified game object to the scene.
        /// </summary>
        /// <param name="o">The object to add.</param>
        /// <param name="addToScene">Whether or not to add it to the scene.</param>
        protected void AddToScene(GameObject o, bool addToScene)
        {
            if (addToScene)
                _scene.AddChild(o);
            else
                o.Entity.Disable();
        }

        /// <summary>
        /// Creates an entity for a game object.
        /// </summary>
        /// <returns>The newly created entity.</returns>
        protected Entity CreateEntity()
        {
            var entity = _scene.World.CreateEntity();
            int id = _scene.World.Count();
            entity.Set(new EntityIdentifier { Id = id });
            return entity;
        }

    }
}
