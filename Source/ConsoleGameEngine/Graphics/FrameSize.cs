namespace ConsoleGameEngine.Graphics
{
    /// <summary>
    /// Contains information about the size of frames within an image (ie. spritesheet).
    /// </summary>
    public class FrameSize
    {
        /// <summary>
        /// The width of the frames.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// The height of the frames.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// The margin at the bottom of each frame.
        /// </summary>
        public int MarginBottom { get; set; }
        /// <summary>
        /// The margin at the top of each frame.
        /// </summary>
        public int MarginLeft { get; set; }
        /// <summary>
        /// The margin at the right of each frame.
        /// </summary>
        public int MarginRight { get; set; }
        /// <summary>
        /// The marging at the top of each frame.
        /// </summary>
        public int MarginTop { get; set; }
    }
}
