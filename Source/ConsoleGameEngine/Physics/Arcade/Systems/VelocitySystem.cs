using ConsoleGameEngine.Components;
using ConsoleGameEngine.Physics.Arcade.Components;
using DefaultEcs;
using DefaultEcs.System;
using System.Numerics;

namespace ConsoleGameEngine.Physics.Arcade.Systems
{
    internal class VelocitySystem : AEntitySetSystem<GameTime>
    {
        public VelocitySystem (World world) : base(world.GetEntities().With<Position>().With<BodyPosition>().With<Velocity>().AsSet())
        {

        }

        protected override void Update(GameTime time, in Entity entity)
        {
            ref var bodyPosition = ref entity.Get<BodyPosition>();
            Vector2 velocity = entity.Get<Velocity>().Value;
            var position = entity.Get<Position>();
            bodyPosition.ProjectedPosition.X = (float)(position.X + velocity.X * time.Delta.TotalSeconds);
            bodyPosition.ProjectedPosition.Y = (float)(position.Y + velocity.Y * time.Delta.TotalSeconds);
        }
    }
}
