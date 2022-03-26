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
        /// <summary>
        /// The current animation being played.
        /// </summary>
        public Animation? CurrentAnimation 
        {
            get => _animationHandler.GetCurrentAnimation();
            set => _animationHandler.SetCurrentAnimation(value);
        }

        /// <summary>
        /// The image associated with this sprite.
        /// </summary>
        public Image? Image { get; set; }
        /// <summary>
        /// The position of the sprite. 
        /// </summary>
        public ref Position Position { get => ref Entity.Get<Position>(); }
        /// <summary>
        /// The spritesheet associated with this sprite.
        /// </summary>
        public Spritesheet? Spritesheet { get; set; }
        /// <summary>
        /// The texture atlas associated with this sprite.
        /// </summary>
        public TextureAtlas? TextureAtlas { get; set; }

        private AnimationManager _animationManager;
        private AnimationTargetEntityHandler _animationHandler;

        internal Sprite(Entity entity, AnimationManager animationManager, Image image) : base(entity)
        {
            _animationManager = animationManager;
            _animationHandler = new AnimationTargetEntityHandler(entity);
            Image = image;
            Initialize(image);
        }

        internal Sprite(Entity entity, AnimationManager animationManager, Spritesheet spritesheet) : base(entity)
        {
            _animationManager = animationManager;
            _animationHandler = new AnimationTargetEntityHandler(entity);
            Spritesheet = spritesheet;
            Initialize(spritesheet.Image);
        }

        internal Sprite(Entity entity, AnimationManager animationManager, TextureAtlas atlas) : base(entity)
        {
            _animationManager = animationManager;
            _animationHandler = new AnimationTargetEntityHandler(entity);
            TextureAtlas = atlas;
            Initialize(atlas.Image);
        }

        private void Initialize(Image image)
        {
            Entity.Set(new Position());
            Entity.Set(new ClippingInfo());
            Entity.Set(image);
        }

        /// <summary>
        /// Plays the specified animation on this sprite.
        /// </summary>
        /// <param name="key">The animation's key.</param>
        /// <param name="repeatOverride">If supplied, overrides the configured repeat on the animation.</param>
        public void PlayAnimation(string key, int? repeatOverride = null)
        {
            _animationManager.Play(key, this, repeatOverride);
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
            _animationManager.Stop(this);
        }

    }
}
