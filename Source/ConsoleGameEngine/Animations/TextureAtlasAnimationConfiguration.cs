using ConsoleGameEngine.Graphics;

namespace ConsoleGameEngine.Animations
{
    /// <summary>
    /// Represents a configuraion for an animation that is based off a texture atlas.
    /// </summary>
    public class TextureAtlasAnimationConfiguration : AnimationConfiguration
    {
        /// <summary>
        /// The texture atlas containing information about animation frames.
        /// </summary>
        public TextureAtlas? TextureAtlas { get; set; }
        /// <summary>
        /// The key to a texture atlas containing information about animation frames.
        /// </summary>
        public string? TextureAtlasKey { get; set; }
        /// <summary>
        /// The names of the frames within the texture atlas.
        /// </summary>
        public string[] FrameNames { get; set; } = Array.Empty<string>();
        /// <summary>
        /// The prefix of the frame names within the texture atlas.
        /// </summary>
        public string? FrameNamePrefix { get; set; }
        /// <summary>
        /// The number of zeros to pad to.  For example if frame numbers are 01,02,etc then this would be two.
        /// </summary>
        public int FrameNameZeroPad { get; set; } = 1;
    }

    /// <summary>
    /// Represents the confuguration for an individual animation frame that comes from a texture atlas.
    /// </summary>
    public class TextureAtlasAnimationFrameConfiguration : AnimationFrameConfiguration
    {
        /// <summary>
        /// The texture atlas containing information about animation frames.
        /// </summary>
        public TextureAtlas? TextureAtlas { get; set; }
        /// <summary>
        ///  The key to a texture atlas containing information about animation frames.
        /// </summary>
        public string? TextureAtlasKey { get; set; }
        /// <summary>
        /// The name of the frame within the texture atlas.
        /// </summary>
        public string? Name { get; set; }
    }
}
