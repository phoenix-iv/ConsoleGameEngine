using ConsoleGameEngine.Graphics;

namespace ConsoleGameEngine.Components
{
    /// <summary>
    /// Contains data about a single animation frame.
    /// </summary>
    public struct AnimationFrame
    {
        /// <summary>
        /// The image containg the animation frame.
        /// </summary>
        public Image Image;
        /// <summary>
        /// The clipping info that discribes where on the image the frame exists.
        /// </summary>
        public ClippingInfo ClippingInfo;
    }
}
