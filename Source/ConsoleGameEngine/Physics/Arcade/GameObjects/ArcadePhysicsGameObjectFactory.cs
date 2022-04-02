using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Caching;
using ConsoleGameEngine.GameObjects;

namespace ConsoleGameEngine.Physics.Arcade.GameObjects
{
    /// <summary>
    /// Adds objects to the scene and Arcade physics simulation.
    /// </summary>
    public class ArcadePhysicsGameObjectFactory : GameObjectFactoryBase
    {
        private readonly ArcadePhysics _physics;

        /// <summary>
        /// Creates a new instance of <see cref="ArcadePhysicsGameObjectFactory"/>
        /// </summary>
        /// <param name="physics">The physics system to add bodies to.</param>
        /// <param name="scene">The scene to add objects to.</param>
        /// <param name="animationManager">The game level animation manager used for objects that need it.</param>
        /// <param name="cache">The game level cache manager.</param>
        public ArcadePhysicsGameObjectFactory(ArcadePhysics physics, Scene scene, AnimationManager animationManager, CacheManager cache) : base(scene, animationManager, cache)
        {
            _physics = physics;
        }

        /// <summary>
        /// Creates a new sprite with the specified image and optionally adds it to the scene.
        /// </summary>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="addToScene">Whether or not to add the sprite to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithDynamicBody Sprite(string imageKey, float x, float y, bool addToScene = true)
        {
            var sprite = AddSprite<SpriteWithDynamicBody>(imageKey, x, y, addToScene);
            return AddSpriteToPhysicsWorld<SpriteWithDynamicBody, DynamicBody>(sprite);
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
        public SpriteWithDynamicBody Sprite(string spritesheetKey, float x, float y, int initialFrame, bool addToScene = true)
        {
            var sprite = AddSprite<SpriteWithDynamicBody>(spritesheetKey, x, y, initialFrame, addToScene);
            return AddSpriteToPhysicsWorld<SpriteWithDynamicBody, DynamicBody>(sprite);
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
            var sprite = AddSprite<SpriteWithDynamicBody>(textureAtlasKey, x, y, initialFrame, addToScene);
            return AddSpriteToPhysicsWorld<SpriteWithDynamicBody, DynamicBody>(sprite);
        }


        /// <summary>
        /// Creates a new sprite with the specified image and optionally adds it to the scene.
        /// </summary>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="addToScene">Whether or not to add the sprite to the scene.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithStaticBody StaticSprite(string imageKey, float x, float y, bool addToScene = true)
        {
            var sprite = AddSprite<SpriteWithStaticBody>(imageKey, x, y, addToScene);
            return AddSpriteToPhysicsWorld<SpriteWithStaticBody, StaticBody>(sprite);
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
        public SpriteWithStaticBody StaticSprite(string spritesheetKey, float x, float y, int initialFrame, bool addToScene = true)
        {
            var sprite = AddSprite<SpriteWithStaticBody>(spritesheetKey, x, y, initialFrame, addToScene);
            return AddSpriteToPhysicsWorld<SpriteWithStaticBody, StaticBody>(sprite);
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
        public SpriteWithStaticBody StaticSprite(string textureAtlasKey, float x, float y, string? initialFrame, bool addToScene = true)
        {
            var sprite = AddSprite<SpriteWithStaticBody>(textureAtlasKey, x, y, initialFrame, addToScene);
            return AddSpriteToPhysicsWorld<SpriteWithStaticBody, StaticBody>(sprite);
        }

        private T AddSpriteToPhysicsWorld<T, TBody>(T sprite) where T : SpriteWithBody<TBody> where TBody : Body
        {
            sprite.ResetBody();
            _physics.World.AddBody(sprite.Body);
            return sprite;
        }
    }  
}
