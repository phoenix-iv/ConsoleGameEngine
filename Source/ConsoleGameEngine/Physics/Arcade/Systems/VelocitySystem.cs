using ConsoleGameEngine.Components;
using ConsoleGameEngine.Components.Physics.Arcade;
using DefaultEcs;
using DefaultEcs.System;

namespace ConsoleGameEngine.Physics.Arcade.Systems
{
    internal class VelocitySystem : AEntitySetSystem<GameTime>
    {
        public VelocitySystem (World world) : base(world.GetEntities().With<DynamicBodyInfo>().With<Position>().AsSet())
        {

        }

        protected override void Update(GameTime state, in Entity entity)
        {
            ref var dynamicBody = ref entity.Get<DynamicBodyInfo>();
            var position = entity.Get<Position>();
            dynamicBody.NewPosition.X = (float)(position.X + dynamicBody.Velocity.X * state.Delta.TotalSeconds);
            dynamicBody.NewPosition.Y = (float)(position.Y + dynamicBody.Velocity.Y * state.Delta.TotalSeconds);
        }
    }
}
