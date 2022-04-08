using ConsoleGameEngine.Components;
using DefaultEcs;
using DefaultEcs.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Box2D.Systems
{
    internal class PhysicsSystem : AEntitySetSystem<GameTime>
    {
        private readonly Box2dPhysics _physics;

        public PhysicsSystem(World world, Box2dPhysics physics) : base(world.GetEntities().With<EntityIdentifier>().With<EntitySize>().With<Position>().AsSet())
        {
            _physics = physics;
        }

        protected override void Update(GameTime time, ReadOnlySpan<Entity> entities)
        {
            if (_physics.World == null)
                throw new NullReferenceException();

            var entityMap = new Dictionary<int, Entity>();
            foreach(var entity in entities)
                entityMap.Add(entity.Get<EntityIdentifier>().Id, entity);

            _physics.World.Step((float)time.Delta.TotalSeconds, _physics.VelocityIterationsPerStep, _physics.PositionIterationsPerStep);
            Box2DX.Dynamics.Body? body = _physics.World.GetBodyList();
            while (body != null)
            {
                PointF point = _physics.WorldToScreenPoint(body.GetPosition());
                var data = (Box2dBodyUserData)body.GetUserData();
                if (data != null)
                {
                    Entity entity = entityMap[data.EntityId];
                    var size = entity.Get<EntitySize>();
                    ref var position = ref entity.Get<Position>();
                    position.X = point.X - size.HalfWidth;
                    position.Y = point.Y - size.HalfHeight;
                }
                body = body.GetNext();
            }
        }
    }
}
