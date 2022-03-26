namespace ConsoleGameEngine.Components
{
    /// <summary>
    /// Contains data about an animation.
    /// </summary>
    public struct Animation
    {
        /// <summary>
        /// The key that uniquely identifies this animation.
        /// </summary>
        public string Key;
        /// <summary>
        /// The animation frames.
        /// </summary>
        public AnimationFrame[] Frames;
        /// <summary>
        /// The current frame index.
        /// </summary>
        public int FrameIndex;
        /// <summary>
        /// The frame rate.
        /// </summary>
        public int FramesPerSecond;
        /// <summary>
        /// The game time that the last frame was played.
        /// </summary>
        public TimeSpan LastFrameTime;
        /// <summary>
        /// The number of times the animation will be repeated.
        /// </summary>
        public int Repeat;
        /// <summary>
        /// The number of times the animation has been repeated.
        /// </summary>
        public int RepeatedCount;
        /// <summary>
        /// Whether or not the animation has been requested to stop.
        /// </summary>
        public bool IsStopped;
    }
}
