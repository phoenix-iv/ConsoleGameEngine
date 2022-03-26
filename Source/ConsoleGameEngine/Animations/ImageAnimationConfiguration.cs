using ConsoleGameEngine.Graphics;

namespace ConsoleGameEngine.Animations
{
    /// <summary>
    /// Represents a configuration for an animation that is based off a image.
    /// </summary>
    public class ImageAnimationConfiguration : AnimationConfiguration
    {
        /// <summary>
        /// The image containing the frames of the animation.
        /// </summary>
        public Image? Image { get; set; }
        /// <summary>
        /// The key of an image containing the frames of the animation.
        /// </summary>
        public string? ImageKey { get; set; }
        /// <summary>
        /// The frame size information.
        /// </summary>
        public FrameSize FrameSize { get; set; } = new FrameSize();
        /// <summary>
        /// The indexes of the frames for the animation.
        /// </summary>
        public int[] FrameIndexes { get; set; } = Array.Empty<int>();
    }

    /// <summary>
    /// Represents a configuration for an individual frame that is based off an image.
    /// </summary>
    public class ImageAnimationFrameConfiguration : AnimationFrameConfiguration
    {
        /// <summary>
        /// The index of the frame.
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// The frame size information.
        /// </summary>
        public FrameSize? FrameSize { get; set; }
    }

}
