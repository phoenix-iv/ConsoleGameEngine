using ConsoleGameEngine.Animations;
using ConsoleGameEngine.GameObjects;
using ConsoleGameEngine.Graphics;
using DefaultEcs;

namespace ConsoleGameEngine.Physics.Arcade.GameObjects
{
    /// <summary>
    /// Represents a sprite with a static physics body.
    /// </summary>
    public class SpriteWithStaticBody : SpriteWithBody<StaticBody>
    {
        /// <summary>
        /// Creates a new instance of <see cref="SpriteWithStaticBody"/>.
        /// WARNING: See remarks, calling this constructor directly requires the calling of other methods. 
        /// </summary>
        /// <remarks>
        /// If called directly, the <see cref="Initialize(Entity, AnimationManager)"/> method, <see cref="SpriteWithBody{T}.ResetBody"/> and one of 
        /// <see cref="Sprite.InitializeImage(Image)"/>, <see cref="Sprite.InitializeSpritesheet(Spritesheet)"/> or <see cref="Sprite.InitializeTextureAtlas(TextureAtlas)"/>
        /// must be called in order for this sprite to function correctly.  Ideally, you should not call this constructor but use one of methods in 
        /// <see cref="ArcadePhysicsGameObjectFactory"/>.
        /// </remarks>
        public SpriteWithStaticBody()
        {

        }

        /// <summary>
        /// Initializes this sprite with the specified parameters.
        /// </summary>
        /// <param name="entity">The ECS entity.</param>
        /// <param name="animationManager">The game-level animation manager.</param>
        public override void Initialize(Entity entity, AnimationManager animationManager)
        {
            base.Initialize(entity, animationManager);
            Body = new StaticBody(entity);
        }
    }
}
