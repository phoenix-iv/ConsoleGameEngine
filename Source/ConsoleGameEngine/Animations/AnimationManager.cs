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
        public Animation Add(ImageAnimationConfiguration config)
        {
            if (config.Image == null && config.ImageKey == null && config.Frames.Length == 0)
                throw new ArgumentNullException(nameof(config), $"Either {nameof(config.Image)}, {nameof(config.ImageKey)} or {nameof(config.Frames)} must be set.");

            if (config.Image == null && config.ImageKey != null)
                config.Image = _cache.Images.Get(config.ImageKey);

            if (config.Frames.Length == 0)
            {
                if (config.FrameIndexes.Length == 0)
                {
                    if (config.FrameCount == 0)
                        throw new ArgumentException($"You must provide {nameof(config.FrameCount)} if {nameof(config.FrameIndexes)} is not provided.");
                    config.FrameIndexes = Enumerable.Range(config.FrameStart, config.FrameCount).ToArray();
                }

                config.Frames = new ImageAnimationFrameConfiguration[config.FrameIndexes.Length];
                for (int i = 0; i < config.FrameIndexes.Length; i++)
                {
                    config.Frames[i] = new ImageAnimationFrameConfiguration
                    {
                        Image = config.Image,
                        FrameSize = config.FrameSize,
                        Index = config.FrameIndexes[i]
                    };
                }
            }
            ResolveAnimationFrameConfigurations(config.Frames, config.Image, null, config.FrameSize);
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
            if (config.TextureAtlas == null && config.TextureAtlasKey == null)
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

            ResolveAnimationFrameConfigurations(config.Frames, config.TextureAtlas.Image, config.TextureAtlas, null);
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
        /// <param name="frameStart">The frame index to start at.</param>
        /// <param name="frameCount">The number of frames in the animation.</param>
        /// <param name="framesPerSecond">The frame rate of the animation.</param>
        /// <param name="repeat">The number of times to repeat the animation.  Supply -1 to repeat infinitely.</param>
        /// <returns>The animation data.</returns>
        public Animation Add(string key, Image image, int frameWidth, int frameHeight, int frameStart, int frameCount, int framesPerSecond, int repeat = 0)
        {
            var config = new ImageAnimationConfiguration
            {
                Key = key,
                Image = image,
                FramesPerSecond = framesPerSecond,
                Repeat = repeat,
                FrameStart = frameStart,
                FrameCount = frameCount,
                FrameSize = new FrameSize
                {
                    Width = frameWidth,
                    Height = frameHeight
                }
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
        /// Calculates the clipping information for the specified texture atlas and frame name.
        /// </summary>
        /// <param name="atlas">The texture atlas.</param>
        /// <param name="frameName">The frame name.</param>
        /// <returns>The clipping information.</returns>
        public ClippingInfo CalculateClipping(TextureAtlas atlas, string frameName)
        {
            TextureAtlasFrame frame = atlas.Frames.First(f => f.FileName == frameName);
            return new ClippingInfo
            {
                X = frame.Frame.X,
                Y = frame.Frame.Y,
                Width = frame.Frame.W,
                Height = frame.Frame.H
            };
        }

        /// <summary>
        /// calculates the clipping information for the specified image and frame information.
        /// </summary>
        /// <param name="image">The image containing the frames.</param>
        /// <param name="frame">The frame size information.</param>
        /// <param name="frameIndex">The index of the chosen frame.</param>
        /// <returns>The clipping information.</returns>
        public ClippingInfo CalculateClipping(Image image, FrameSize frame, int frameIndex)
        {
            int perRow = image.Width / (frame.Width + frame.MarginLeft + frame.MarginRight);
            int row = frameIndex / perRow;
            int col = frameIndex % perRow;
            int x = (frame.Width + frame.MarginLeft + frame.MarginRight) * col + frame.MarginLeft;
            int y = (frame.Height + frame.MarginTop + frame.MarginBottom) * row + frame.MarginTop;
            return new ClippingInfo
            {
                X = x,
                Y = y,
                Width = frame.Width,
                Height = frame.Height
            };
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

        private void ResolveAnimationFrameConfigurations(AnimationFrameConfiguration[] frames, Image? image, TextureAtlas? textureAtlas, FrameSize? frameConfig)
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
                    animFrameConfig.Clipping = CalculateClipping(atlasConfig.TextureAtlas, atlasConfig.Name);
                }

                if (animFrameConfig is ImageAnimationFrameConfiguration imageConfig)
                {
                    if (imageConfig.FrameSize == null && frameConfig != null)
                        imageConfig.FrameSize = frameConfig;
                    else
                        throw new ArgumentNullException(nameof(frameConfig), "frameConfig is required when animation Frame config is not set.");
                    animFrameConfig.Clipping = CalculateClipping(animFrameConfig.Image, imageConfig.FrameSize, imageConfig.Index);
                }
            }
        }

    }
}
