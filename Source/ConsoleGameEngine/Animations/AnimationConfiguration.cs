using ConsoleGameEngine.Components;
using ConsoleGameEngine.Graphics;

namespace ConsoleGameEngine.Animations
{
    /// <summary>
    /// Represents a configuration used to build an animation.
    /// </summary>
    public class AnimationConfiguration
    {
        /// <summary>
        /// The key used to uniquely identify the animation.
        /// </summary>
        public string Key { get; set; } = "";
        /// <summary>
        /// The frame rate of the animation.
        /// </summary>
        public int FramesPerSecond { get; set; } = 30;
        /// <summary>
        /// The number of times to repeat the animation. Set to -1 for infinite repeats.
        /// </summary>
        public int Repeat { get; set; }
        /// <summary>
        /// The starting frame index. 
        /// </summary>
        public int FrameStart { get; set; }
        /// <summary>
        /// The number of frames in the animation.
        /// </summary>
        public int FrameCount { get; set; }
        /// <summary>
        /// The configuration for individual frames of the animation.
        /// </summary>
        public AnimationFrameConfiguration[] Frames { get; set; } = Array.Empty<AnimationFrameConfiguration>();
    }
    
    /// <summary>
    /// Represents a configuration for an individual animation frame.
    /// </summary>
    public class AnimationFrameConfiguration
    {
        /// <summary>
        /// The image containing the animation frame.
        /// </summary>
        public Image? Image { get; set; }
        /// <summary>
        /// The key to an image containing the animation frame.
        /// </summary>
        public string? ImageKey { get; set; }
        /// <summary>
        /// The clipping information describing where on the image the frame exists. 
        /// </summary>
        public ClippingInfo Clipping { get; set; }
    }
}
