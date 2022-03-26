using ConsoleGameEngine.Graphics;

namespace ConsoleGameEngine.Animations
{
    /// <summary>
    /// Represents a configuration for an animation that is based off a image.
    /// </summary>
    public class SpritesheetAnimationConfiguration : AnimationConfiguration
    {
        /// <summary>
        /// The image containing the frames of the animation.
        /// </summary>
        public Spritesheet? Spritesheet { get; set; }
        /// <summary>
        /// The key of an image containing the frames of the animation.
        /// </summary>
        public string? SpritesheetKey { get; set; }
        /// <summary>
        /// The indexes of the frames for the animation.
        /// </summary>
        public int[] FrameIndexes { get; set; } = Array.Empty<int>();
    }

    /// <summary>
    /// Represents a configuration for an individual frame that is based off an image.
    /// </summary>
    public class SpritesheetAnimationFrameConfiguration : AnimationFrameConfiguration
    {
        /// <summary>
        /// The spritesheet containing the frame.
        /// </summary>
        public Spritesheet? Spritesheet { get; set; }
        /// <summary>
        /// The key to the spritesheet containing the frame.
        /// </summary>
        public string? SpritesheetKey { get; set; }
        /// <summary>
        /// The index of the frame.
        /// </summary>
        public int Index { get; set; }
    }

}
