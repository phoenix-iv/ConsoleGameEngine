using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Components;
using ConsoleGameEngine.Graphics;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.GameObjects
{
    /// <summary>
    /// Represents a sprite.
    /// </summary>
    public class Sprite : GameObject, IAnimationTarget
    {
        private AnimationManager? _animationManager;
        internal AnimationManager AnimationManager
        {
            get => _animationManager ?? throw new NullReferenceException();
            set => _animationManager = value;
        }
        private AnimationTargetEntityHandler? _animationHandler;
        private AnimationTargetEntityHandler AnimationHandler
        {
            get => _animationHandler ?? throw new NullReferenceException();
            set => _animationHandler = value;
        }

        /// <summary>
        /// The current animation being played.
        /// </summary>
        public Animation? CurrentAnimation 
        {
            get => AnimationHandler.GetCurrentAnimation();
            set => AnimationHandler.SetCurrentAnimation(value);
        }

        private Entity _entity;
        /// <summary>
        /// The ECS entity associated with this sprite.
        /// </summary>
        internal override Entity Entity 
        { 
            get => _entity; 
            set
            {
                _entity = value;
                _animationHandler = new AnimationTargetEntityHandler(_entity);
            }
                
        }

        /// <summary>
        /// The image associated with this sprite.
        /// </summary>
        public Image? Image { get; private set; }
        /// <summary>
        /// The position of the sprite. 
        /// </summary>
        public ref Position Position { get => ref Entity.Get<Position>(); }
        /// <summary>
        /// The spritesheet associated with this sprite.
        /// </summary>
        public Spritesheet? Spritesheet { get; private set; }
        /// <summary>
        /// The texture atlas associated with this sprite.
        /// </summary>
        public TextureAtlas? TextureAtlas { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="Sprite"/>.
        /// WARNING: If called directly, the <see cref="Initialize(Entity, AnimationManager)"/> method and one of 
        /// <see cref="InitializeImage(Image)"/>, <see cref="InitializeSpritesheet(Spritesheet)"/> or <see cref="InitializeTextureAtlas(TextureAtlas)"/>
        /// must be called in order for this sprite to function correctly.  Ideally, you should not call this constructor but use one of methods in 
        /// <see cref="GameObjectFactory"/>.
        /// </summary>
        public Sprite()
        {
            
        }

        /// <summary>
        /// Initializes this sprite with the specified parameters.
        /// </summary>
        /// <param name="entity">The ECS entity associated with this sprite.</param>
        /// <param name="animationManager">The animation manager used to play animations on this sprite.</param>
        public virtual void Initialize(Entity entity, AnimationManager animationManager)
        {
            Entity = entity;
            Entity.Set(new Position());
            Entity.Set(new ClippingInfo());
            _animationManager = animationManager;
            _animationHandler = new AnimationTargetEntityHandler(entity);
        }

        /// <summary>
        /// Initializes this sprite with an image.
        /// </summary>
        /// <param name="image">The image to use.</param>
        public void InitializeImage(Image image)
        {
            Image = image;
            Initialize(image);
        }

        /// <summary>
        /// Initializes this sprite with a spritesheet.
        /// </summary>
        /// <param name="spritesheet">The spritesheet to use.</param>
        public void InitializeSpritesheet(Spritesheet spritesheet)
        {
            Spritesheet = spritesheet;
            Initialize(spritesheet.Image);
        }

        /// <summary>
        /// Initializes this sprite with a texture atlas.
        /// </summary>
        /// <param name="atlas">The texture atlas to use.</param>
        public void InitializeTextureAtlas(TextureAtlas atlas)
        {
            TextureAtlas = atlas;
            Initialize(TextureAtlas.Image);
        }

        private void Initialize(Image image)
        {
            Entity.Set(image);
        }

        /// <summary>
        /// Plays the specified animation on this sprite.
        /// </summary>
        /// <param name="key">The animation's key.</param>
        /// <param name="repeatOverride">If supplied, overrides the configured repeat on the animation.</param>
        public void PlayAnimation(string key, int? repeatOverride = null)
        {
            AnimationManager.Play(key, this, repeatOverride);
        }

        /// <summary>
        /// Sets the frame to the specified spritesheet or image frame index.
        /// </summary>
        /// <param name="index">The frame index.  This parameter is ignored if the image source is an <see cref="Image"/>.</param>
        public void SetFrame(int index)
        {
            if (Image == null && Spritesheet == null)
                throw new NullReferenceException($"The {nameof(Image)} or {nameof(Spritesheet)} must be set to call this method.");

            if (Spritesheet != null)
            {
                Entity.Set(Spritesheet.Image);
                Entity.Set(Spritesheet.CalculateClipping(index));
            }

            if (Image != null)
            {
                Entity.Set(Image);
                Entity.Set(new ClippingInfo { X = 0, Y = 0, Width = Image.Width, Height = Image.Height });
            }
        }

        /// <summary>
        /// Sets the frame to the specified texture atlas frame name.
        /// </summary>
        /// <param name="name">The frame name.</param>
        public void SetFrame(string name)
        {
            if (TextureAtlas == null)
                throw new NullReferenceException($"The {nameof(TextureAtlas)} must be set to call this method.");
            Entity.Set(TextureAtlas.Image);
            Entity.Set(TextureAtlas.CalculateClipping(name));
        }

        /// <summary>
        /// Stops the currently played animation, if any.
        /// </summary>
        public void StopAnimation()
        {
            AnimationManager.Stop(this);
        }

    }
}
