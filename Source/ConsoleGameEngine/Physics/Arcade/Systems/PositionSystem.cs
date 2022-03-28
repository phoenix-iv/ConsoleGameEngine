using ConsoleGameEngine.Components;
using ConsoleGameEngine.Components.Physics.Arcade;
using DefaultEcs;
using DefaultEcs.System;

namespace ConsoleGameEngine.Physics.Arcade.Systems
{
    /// <summary>
    /// A system that computes the final position of relevant entities.
    /// </summary>
    internal class PositionSystem : AEntitySetSystem<GameTime>
    {
        public PositionSystem(World world) : base(world.GetEntities().With<DynamicBodyInfo>().With<Position>().AsSet())
        {

        }

        protected override void Update(GameTime state, in Entity entity)
        {
            ref var position = ref entity.Get<Position>();
            ref var dynamicBody = ref entity.Get<DynamicBodyInfo>();
            position.X = dynamicBody.NewPosition.X;
            position.Y = dynamicBody.NewPosition.Y;
        }
    }
}
