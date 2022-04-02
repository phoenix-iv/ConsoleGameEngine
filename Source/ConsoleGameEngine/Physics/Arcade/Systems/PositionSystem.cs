using ConsoleGameEngine.Components;
using ConsoleGameEngine.Physics.Arcade.Components;
using DefaultEcs;
using DefaultEcs.System;

namespace ConsoleGameEngine.Physics.Arcade.Systems
{
    /// <summary>
    /// A system that computes the final position of relevant entities.
    /// </summary>
    internal class PositionSystem : AEntitySetSystem<GameTime>
    {
        public PositionSystem(World world) : base(world.GetEntities().With<BodyPosition>().With<Position>().AsSet())
        {

        }

        protected override void Update(GameTime state, in Entity entity)
        {
            ref var position = ref entity.Get<Position>();
            var bodyPosition = entity.Get<BodyPosition>();
            position.X = bodyPosition.ProjectedPosition.X;
            position.Y = bodyPosition.ProjectedPosition.Y;
        }
    }
}
