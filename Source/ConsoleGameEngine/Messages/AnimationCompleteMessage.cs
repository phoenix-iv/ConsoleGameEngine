namespace ConsoleGameEngine.Messages
{
    /// <summary>
    /// A message that is published when an animation has completed.
    /// </summary>
    public readonly struct AnimationCompleteMessage
    {
        /// <summary>
        /// The ID of the entity that the animation was for.
        /// </summary>
        public readonly int EntityId;
        /// <summary>
        /// The animation key.
        /// </summary>
        public readonly string AnimationKey;

        /// <summary>
        /// Creates a new <see cref="AnimationCompleteMessage"/>.
        /// </summary>
        /// <param name="entityId">The ID of the entity that the animation was for.</param>
        /// <param name="animationKey">The key of the animation.</param>
        public AnimationCompleteMessage(int entityId, string animationKey)
        {
            EntityId = entityId;
            AnimationKey = animationKey;
        }
    }
}
