using ConsoleGameEngine.Components;
using ConsoleGameEngine.Messages;
using ConsoleGameEngine.Graphics;
using DefaultEcs;
using DefaultEcs.System;

namespace ConsoleGameEngine.Systems
{
    /// <summary>
    /// Represents a system that processes animations.
    /// </summary>
    public class AnimationSystem : AEntitySetSystem<GameTime>
    {
        private readonly World _world;

        /// <summary>
        /// Creates a new instance of the <see cref="AnimationSystem"/> class.
        /// </summary>
        /// <param name="world">The ECS world that this system uses.</param>
        public AnimationSystem(World world) : base(world.GetEntities().With<EntityIdentifier>().With<Animation>().With<ClippingInfo>().With<Image>().AsSet(), true)
        {
            _world = world;
        }

        /// <summary>
        /// Updates the specified entities animation.
        /// </summary>
        /// <param name="time">The game time</param>
        /// <param name="entity">The entity whos animation to process.</param>
        protected override void Update(GameTime time, in Entity entity)
        {
            ref var animation = ref entity.Get<Animation>();
            var identifier = entity.Get<EntityIdentifier>();
            
            if (animation.IsStopped)
            {
                return;
            }

            int framesToAdvance = (int)((time.Total - animation.LastFrameTime).TotalSeconds * animation.FramesPerSecond);
            
            if (framesToAdvance > 0)
            {
                animation.LastFrameTime = time.Total;
                animation.FrameIndex += framesToAdvance;
                if (animation.FrameIndex > animation.Frames.Length - 1)
                {
                    if (animation.Repeat == -1 || (animation.Repeat > 0 && animation.RepeatedCount < animation.Repeat))
                    {
                        animation.FrameIndex = 0;
                        animation.RepeatedCount++;
                    }
                    else
                    {
                        _world.Publish(new AnimationCompleteMessage(identifier.Id, animation.Key));
                        return;
                    }
                }

                AnimationFrame frame = animation.Frames[animation.FrameIndex];
                entity.Set(frame.Image);
                entity.Set(frame.ClippingInfo);
            }
        }
    }
}
