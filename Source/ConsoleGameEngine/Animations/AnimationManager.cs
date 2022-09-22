using ConsoleGameEngine.Caching;
using ConsoleGameEngine.Components;
using ConsoleGameEngine.Graphics;

namespace ConsoleGameEngine.Animations
{
    /// <summary>
    /// Manages animations for the game.
    /// </summary>
    public class AnimationManager
    {
        private readonly CacheManager _cache;
        private readonly Dictionary<string, Animation> _animations = new();

        /// <summary>
        /// Creates a new instance of the  AnimationManager class.
        /// </summary>
        /// <param name="cache">The cache manager containing animation content.</param>
        public AnimationManager(CacheManager cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Adds an animation using the specified image animation configuration.
        /// </summary>
        /// <param name="config">The configuration that specifies how to build the animation.</param>
        /// <returns>The animation data.</returns>
        /// <exception cref="ArgumentNullException">Occurs when required properties on the configuration are null.</exception>
        /// <exception cref="ArgumentException">Occurs when required configuration data is not set.</exception>
        public Animation Add(SpritesheetAnimationConfiguration config)
        {
            if (config.Spritesheet == null && config.SpritesheetKey == null && config.Frames.Length == 0)
                throw new ArgumentNullException(nameof(config), $"Either {nameof(config.Spritesheet)}, {nameof(config.SpritesheetKey)} or {nameof(config.Frames)} must be set.");

            if (config.Spritesheet == null && config.SpritesheetKey != null)
                config.Spritesheet = _cache.Spritesheets.Get(config.SpritesheetKey);

            if (config.Frames.Length == 0)
            {
                if (config.FrameIndexes.Length == 0)
                {
                    if (config.FrameCount == 0)
                        throw new ArgumentException($"You must provide {nameof(config.FrameCount)} if {nameof(config.FrameIndexes)} is not provided.");
                    config.FrameIndexes = Enumerable.Range(config.FrameStart, config.FrameCount).ToArray();
                }

                config.Frames = new SpritesheetAnimationFrameConfiguration[config.FrameIndexes.Length];
                for (int i = 0; i < config.FrameIndexes.Length; i++)
                {
                    config.Frames[i] = new SpritesheetAnimationFrameConfiguration
                    {
                        Spritesheet = config.Spritesheet,
                        Image = config.Spritesheet?.Image ?? throw new NullReferenceException($"{nameof(config.Spritesheet)} must be set."),
                        Index = config.FrameIndexes[i]
                    };
                }
            }

            ResolveAnimationFrameConfigurations(config.Frames, config.Spritesheet?.Image, config.Spritesheet, null);
            return Add((AnimationConfiguration)config);
        }

        /// <summary>
        /// Adds an animation using the speccified texture atlas animation configuration.
        /// </summary>
        /// <param name="config">The configuration that specifies how to build the animation.</param>
        /// <returns>The animation data.</returns>
        /// <exception cref="ArgumentNullException">Occurs when required properties on the configuration are null.</exception>
        /// <exception cref="ArgumentException">Occurs when required configuration data is not set.</exception>
        public Animation Add(TextureAtlasAnimationConfiguration config)
        {
            if (config.TextureAtlas == null && config.TextureAtlasKey == null && config.Frames.Length == 0)
                throw new ArgumentNullException(nameof(config), $"Either {nameof(config.TextureAtlas)} or {nameof(config.TextureAtlasKey)} must be set.");

            if (config.TextureAtlas == null)
                config.TextureAtlas = _cache.TextureAtlases.Get(config.TextureAtlasKey ?? "");

            if (config.Frames.Length == 0)
            {
                if (config.FrameNames.Length == 0)
                {
                    if (config.FrameNamePrefix == null || config.FrameCount == 0)
                        throw new ArgumentException($"You must provide {nameof(config.FrameNamePrefix)} and {nameof(config.FrameCount)} if {nameof(config.FrameNames)} is not provided.", nameof(config));
                    config.FrameNames = GenerateFrameNames(config.FrameNamePrefix, config.FrameStart, config.FrameCount, config.FrameNameZeroPad);
                }

                config.Frames = new TextureAtlasAnimationFrameConfiguration[config.FrameNames.Length];
                for(int i = 0; i < config.FrameNames.Length; i++)
                {
                    config.Frames[i] = new TextureAtlasAnimationFrameConfiguration
                    {
                        TextureAtlas = config.TextureAtlas,
                        Name = config.FrameNames[i]
                    };
                }
            }

            ResolveAnimationFrameConfigurations(config.Frames, config.TextureAtlas.Image, null, config.TextureAtlas);
            return Add((AnimationConfiguration)config);
        }

        private Animation Add(AnimationConfiguration config)
        {
            var animation = new Animation
            {
                Key = config.Key,
                FramesPerSecond = config.FramesPerSecond,
                Repeat = config.Repeat,
                Frames = new AnimationFrame[config.Frames.Length]
            };
            
            for(int i = 0; i < animation.Frames.Length; i++)
            {
                animation.Frames[i] = new AnimationFrame
                {
                    Image = config.Frames[i].Image ?? throw new NullReferenceException(),
                    ClippingInfo = config.Frames[i].Clipping
                };
            }

            _animations.Add(config.Key, animation);
            return animation;
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="image">The image containing the animation frames.</param>
        /// <param name="frameWidth">The width of each frame.</param>
        /// <param name="frameHeight">The height of each frame.</param>
        /// <param name="frameIndexes">The indexes of the frames.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, Image image, int frameWidth, int frameHeight, int[] frameIndexes, int framesPerSecond, int repeat = 0)
        {
            var spritesheet = new Spritesheet(image, new FrameSize { Width = frameWidth, Height = frameHeight });
            return Add(key, spritesheet, frameIndexes, framesPerSecond, repeat);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="image">The image containing the animation frames.</param>
        /// <param name="frameWidth">The width of each frame.</param>
        /// <param name="frameHeight">The height of each frame.</param>
        /// <param name="frameStart">The frame index to start at.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, Image image, int frameWidth, int frameHeight, int frameStart, int frameCount, int framesPerSecond, int repeat = 0)
        {
            var spritesheet = new Spritesheet(image, new FrameSize { Width = frameWidth, Height = frameHeight });
            return Add(key, spritesheet, frameStart, frameCount, framesPerSecond, repeat);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="spritesheet">The spritesheet containing the animation frames.</param>
        /// <param name="frameIndexes">The frame index to start at.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, Spritesheet spritesheet, int[] frameIndexes, int framesPerSecond, int repeat = 0)
        {
            var config = new SpritesheetAnimationConfiguration
            {
                Key = key,
                Spritesheet = spritesheet,
                FrameIndexes = frameIndexes,
                FramesPerSecond = framesPerSecond,
                Repeat = repeat
            };
            return Add(config);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="spritesheet">The spritesheet containing the animation frames.</param>
        /// <param name="frameStart">The frame index to start at.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, Spritesheet spritesheet, int frameStart, int frameCount, int framesPerSecond, int repeat = 0)
        {
            var config = new SpritesheetAnimationConfiguration
            {
                Key = key,
                Spritesheet = spritesheet,
                FrameStart = frameStart,
                FrameCount = frameCount,
                FramesPerSecond = framesPerSecond,
                Repeat = repeat
            };
            return Add(config);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="atlas">The texture atlas containing the animation frames.</param>
        /// <param name="frameNames">The names of the frames within the atlas.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, TextureAtlas atlas, string[] frameNames, int framesPerSecond, int repeat = 0)
        {
            var config = new TextureAtlasAnimationConfiguration
            {
                Key = key,
                TextureAtlas = atlas,
                FrameNames = frameNames,
                FramesPerSecond = framesPerSecond,
                Repeat = repeat
            };
            return Add(config);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="atlas">The texture atlas containing the animation frames.</param>
        /// <param name="frameNamePrefix">The frame name prefix.</param>
        /// <param name="frameStart">The starting frame number.</param>
        /// <param name="frameCount">heT number of frames in the animation.</param>
        /// <param name="framesPerSecond">The frame rate.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <param name="zeroPad">The number of zeros to pad to.For example if frame numbers are 01,02,etc then this would be two.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, TextureAtlas atlas, string frameNamePrefix, int frameStart, int frameCount, int framesPerSecond, int repeat = 0, int zeroPad = 1)
        {
            var config = new TextureAtlasAnimationConfiguration
            {
                Key = key,
                TextureAtlas = atlas,
                FrameNamePrefix = frameNamePrefix,
                FrameStart = frameStart,
                FrameCount = frameCount,
                FramesPerSecond = framesPerSecond,
                Repeat = repeat,
                FrameNameZeroPad = zeroPad
            };
            return Add(config);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="imageKey">The key of the image containing the animation frames.</param>
        /// <param name="frameWidth">The width of each frame.</param>
        /// <param name="frameHeight">The height of each frame.</param>
        /// <param name="frameIndexes">The indexes of the frames.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, string imageKey, int frameWidth, int frameHeight, int[] frameIndexes, int framesPerSecond, int repeat = 0)
        {
            return Add(key, _cache.Images.Get(imageKey), frameWidth, frameHeight, frameIndexes, framesPerSecond, repeat);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="frameWidth">The width of each frame.</param>
        /// <param name="frameHeight">The height of each frame.</param>
        /// <param name="frameStart">The starting frame index.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <param name="framesPerSecond">The frame rate.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, string imageKey, int frameWidth, int frameHeight, int frameStart, int frameCount, int framesPerSecond, int repeat = 0)
        {
            return Add(key, _cache.Images.Get(imageKey), frameWidth, frameHeight, frameStart, frameCount, framesPerSecond, repeat);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="spritesheetKey">The key of the spritesheet containing the animation frames.</param>
        /// <param name="frameIndexes">The frame index to start at.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, string spritesheetKey, int[] frameIndexes, int framesPerSecond, int repeat = 0)
        {
            return Add(key, _cache.Spritesheets.Get(spritesheetKey), frameIndexes, framesPerSecond, repeat);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="spritesheetKey">The key of the spritesheet containing the animation frames.</param>
        /// <param name="frameStart">The frame index to start at.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, string spritesheetKey, int frameStart, int frameCount, int framesPerSecond, int repeat = 0)
        {
            return Add(key, _cache.Spritesheets.Get(spritesheetKey), frameStart, frameCount, framesPerSecond, repeat);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="textureAtlasKey">The key to the texture atlas.</param>
        /// <param name="frameNames">The names of the frames in the texture atlas.</param>
        /// <param name="framesPerSecond">The frame rate.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely</param>
        /// <returns></returns>
        public Animation Add(string key, string textureAtlasKey, string[] frameNames, int framesPerSecond, int repeat = 0)
        {
            return Add(key, _cache.TextureAtlases.Get(textureAtlasKey), frameNames, framesPerSecond, repeat);
        }

        /// <summary>
        /// Adds an animation with specified parameters.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="textureAtlasKey">The key to the texture atlas.</param>
        /// <param name="frameNamePrefix">The frame name prefix.</param>
        /// <param name="frameStart">The starting frame index.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <param name="framesPerSecond">The frame rate.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <param name="zeroPad">The number of zeros to pad to.For example if frame numbers are 01,02,etc then this would be two.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, string textureAtlasKey, string frameNamePrefix, int frameStart, int frameCount, int framesPerSecond, int repeat = 0, int zeroPad = 1)
        {
            return Add(key, _cache.TextureAtlases.Get(textureAtlasKey), frameNamePrefix, frameStart, frameCount, framesPerSecond, repeat, zeroPad);
        }

        /// <summary>
        /// Adds an animation from an array of images.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="frames">The images to use as frames.</param>
        /// <param name="framesPerSecond">The frame rate.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, Image[] frames, int framesPerSecond, int repeat = 0)
        {
            var config = new AnimationConfiguration
            {
                Key = key,
                FramesPerSecond = framesPerSecond,
                Repeat = repeat
            };
            config.Frames = frames.Select(f => new AnimationFrameConfiguration
            {
                Image = f,
                Clipping = new ClippingInfo
                {
                    X = 0,
                    Y = 0,
                    Width = f.Width,
                    Height = f.Height
                }
            }).ToArray();
            return Add(config);
        }

        /// <summary>
        /// Generates frame names based on the prefix and frame index information. 
        /// </summary>
        /// <param name="prefix">The frame name prefix.</param>
        /// <param name="frameStart">The starting frame index.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <param name="zeroPad">The number of zeros to pad to.For example if frame numbers are 01,02,etc then this would be two.</param>
        /// <returns>An array of frame names.</returns>
        public string[] GenerateFrameNames(string prefix, int frameStart, int frameCount, int zeroPad = 1)
        {
            var frameNames = new string[frameCount];
            for (int i = 0; i < frameCount; i++)
            {
                frameNames[i] = prefix + (frameStart + i).ToString(new string('0', zeroPad));
            }
            return frameNames;
        }

        /// <summary>
        /// Plays the specified animation on the target.
        /// </summary>
        /// <param name="key">The unique key used to identify the animation.</param>
        /// <param name="target">The target on which the animation will be played.</param>
        /// <param name="repeatOverride">If supplied, overrides the configured repeat.</param>
        public void Play(string key, IAnimationTarget target, int? repeatOverride = null)
        {
            var animation = _animations[key];
            
            if (repeatOverride != null)
                animation.Repeat = repeatOverride.Value;

            target.CurrentAnimation = animation;
        }

        /// <summary>
        /// Stops the specified animation on the specified target.
        /// </summary>
        /// <param name="target">The target that is currently playing the animation.</param>
        /// <param name="rewind">Whether or not to rewind the animation back to the first frame.</param>
        public void Stop(IAnimationTarget target, bool rewind = false)
        {
            if (target.CurrentAnimation == null)
                return;
            Animation animation = target.CurrentAnimation.Value;
            animation.IsStopped = true;
            if (rewind)
            {
                animation.FrameIndex = 0;
            }
            target.CurrentAnimation = animation;
        }

        private void ResolveAnimationFrameConfigurations(AnimationFrameConfiguration[] frames, Image? image, Spritesheet? spritesheet, TextureAtlas? textureAtlas)
        {
            foreach (AnimationFrameConfiguration animFrameConfig in frames)
            {
                if (animFrameConfig.Image == null)
                {
                    if (animFrameConfig.ImageKey != null)
                        animFrameConfig.Image = _cache.Images.Get(animFrameConfig.ImageKey);
                    else if (image != null)
                        animFrameConfig.Image = image;
                    else
                        throw new ArgumentException("Cannot resolve frame image.", nameof(frames));
                }

                if (animFrameConfig is TextureAtlasAnimationFrameConfiguration atlasConfig)
                {
                    if (atlasConfig.Name == null)
                        throw new ArgumentNullException(nameof(frames), "Frame Name must not be null.");

                    if (atlasConfig.TextureAtlas == null)
                    {
                        if (atlasConfig.TextureAtlasKey != null)
                            atlasConfig.TextureAtlas = _cache.TextureAtlases.Get(atlasConfig.TextureAtlasKey);
                        else if (textureAtlas != null)
                            atlasConfig.TextureAtlas = textureAtlas;
                        else
                            throw new ArgumentException("Cannot resolve frame texture atlas.", nameof(frames));
                    }
                    animFrameConfig.Clipping = atlasConfig.TextureAtlas.CalculateClipping(atlasConfig.Name);
                }

                if (animFrameConfig is SpritesheetAnimationFrameConfiguration spritesheetConfig)
                {
                    if (spritesheetConfig.Spritesheet == null)
                    {
                        if (spritesheetConfig.SpritesheetKey != null)
                            spritesheetConfig.Spritesheet = _cache.Spritesheets.Get(spritesheetConfig.SpritesheetKey);
                        else if (spritesheet != null)
                            spritesheetConfig.Spritesheet = spritesheet;
                        else
                            throw new ArgumentNullException(nameof(spritesheet), "If frame configuration spritesheet is null, then spritesheet must be provided at the animation level.");
                    }

                    // We have to use null coalescing here to keep the compiler happy even though we know that the spritesheet is not null.
                    animFrameConfig.Clipping = spritesheetConfig.Spritesheet?.CalculateClipping(spritesheetConfig.Index) ?? new ClippingInfo();
                }
            }
        }
    }
}
