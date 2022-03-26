namespace ConsoleGameEngine
{
    /// <summary>
    /// Contains information about the game time.
    /// </summary>
    public struct GameTime
    {
        /// <summary>
        /// The amount of time between the last game step and the current one.
        /// </summary>
        public TimeSpan Delta;
        /// <summary>
        /// The total amount of time since the game started.
        /// </summary>
        public TimeSpan Total;
    }
}
