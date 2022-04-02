using Auios.QuadTree;
using ConsoleGameEngine.Components;
using ConsoleGameEngine.GameObjects;
using ConsoleGameEngine.Physics.Arcade.Components;
using DefaultEcs;
using DefaultEcs.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Arcade.Systems
{
    internal class CollisionSystem : AEntitySetSystem<GameTime>
    {
        private readonly World _world;
        private readonly PhysicsWorld _physicsWorld;
        
        public CollisionSystem(World world, PhysicsWorld physicsWorld) 
            : base(world.GetEntities().With<EntityIdentifier>().With<Position>().With<BodyPosition>().With<BodySize>().With<BodyType>().AsSet())
        {
            _world = world;
            _physicsWorld = physicsWorld;
        }

        private class CollisionEntity
        {
            public RectangleF Bounds;
            public BodyTypeCode BodyType;
            public Entity Entity;
            public readonly int Id;
            public bool HasCollided;
            public readonly bool IsDynamic;
            public Position Position;
            public SizeF Size;
            public Vector2 Velocity;

            public CollisionEntity(Entity entity)
            {
                Entity = entity;
                Id = entity.Get<EntityIdentifier>().Id;
                Position = entity.Get<Position>();
                Position offset = entity.Get<BodyPosition>().Offset;
                Position.X += offset.X;
                Position.Y += offset.Y;
                Size = entity.Get<BodySize>().Size;
                Bounds = new RectangleF(Position.X, Position.Y, Size.Width, Size.Height);
                IsDynamic = entity.Has<Velocity>();
                BodyType = entity.Get<BodyType>().Type;
                HasCollided = false;

                if (IsDynamic)
                    Velocity = entity.Get<Velocity>().Value;
                else
                    Velocity = Vector2.Zero;
            }

            public void Collided()
            {
                if (!IsDynamic)
                    return;

                StepBack();
                HasCollided = true;
            }

            public void StepForward()
            {
                if (!HasCollided)
                    Step(Velocity);
            }

            public void StepBack()
            {
                if (!HasCollided)
                    Step(-Velocity);
            }

            private void Step(Vector2 velocity)
            {
                if (!IsDynamic)
                    return;
                Position.X += velocity.X;
                Position.Y += velocity.Y;
                UpdateBounds();
            }

            public void UpdateBounds()
            {
                Bounds.X = Position.X;
                Bounds.Y = Position.Y;
                Bounds.Width = Size.Width;
                Bounds.Height = Size.Height;
            }
        }

        protected override void Update(GameTime time, ReadOnlySpan<Entity> entities)
        {
            var dynamicEntities = new List<CollisionEntity>();
            var allEntities = new Dictionary<int, CollisionEntity>();
            
            float maxVelocity = 0;
            CollisionEntity? fastestEntity = null;

            foreach (var entity in entities)
            {
                var collisionEntity = new CollisionEntity(entity);
                allEntities[collisionEntity.Id] = collisionEntity;
                if (collisionEntity.IsDynamic)
                {
                    dynamicEntities.Add(collisionEntity);
                    Vector2 velocity = collisionEntity.Velocity;

                    if (velocity.X > maxVelocity)
                    {
                        fastestEntity = collisionEntity;
                        maxVelocity = velocity.X;
                    }
                    if (velocity.Y > maxVelocity)
                    {
                        fastestEntity = collisionEntity;
                        maxVelocity = velocity.Y;
                    }
                }
            }

            // Nothing is moving, nothing to collide.
            if (maxVelocity == 0)
                return;

            float distance = (float)(maxVelocity * time.Delta.TotalSeconds);
            int count = 1;
            while (distance > 1)
            {
                distance /= 2;
                count *= 2;
            }

            for (int i = 0; i < dynamicEntities.Count; i++)
            {
                var entity = dynamicEntities[i];
                entity.Velocity.X = (float)((entity.Velocity.X / count) * time.Delta.TotalSeconds);
                entity.Velocity.Y = (float)((entity.Velocity.Y / count) * time.Delta.TotalSeconds);
            }

            for (int i = 0; i < count; i++)
            {
                for (int d = 0; d < dynamicEntities.Count; d++)
                {
                    var entity = dynamicEntities[d];
                    entity.StepForward();
                }

                foreach (CollisionSet collisionSet in _physicsWorld.CollisionSets)
                {
                    foreach (GameObject gameObject1 in collisionSet.Objects1)
                    {
                        CollisionEntity entity1 = allEntities[gameObject1.Id];
                        
                        foreach (GameObject gameObject2 in collisionSet.Objects2)
                        {
                            CollisionEntity entity2 = allEntities[gameObject2.Id];
                            if (gameObject1 == gameObject2 || (!entity1.IsDynamic && !entity1.IsDynamic))
                                continue;

                            if (entity1.Bounds.IntersectsWith(entity2.Bounds))
                            {
                                if (collisionSet.Type == CollisionDetectionType.DetectAndSeparate)
                                {
                                    entity1.Collided();
                                    entity2.Collided();
                                }

                                if (collisionSet.ProcessCallback?.Invoke(gameObject1, gameObject2) ?? true)
                                {
                                    collisionSet.CollideCallback?.Invoke(gameObject1, gameObject2);
                                }
                            }
                        }
                    }
                }
            }

            for (int d = 0; d < dynamicEntities.Count; d++)
            {
                var entity = dynamicEntities[d];
                ref var position = ref entity.Entity.Get<Position>();
                
                if (entity.Velocity.X != 0)
                    position.X = entity.Position.X;
                if (entity.Velocity.Y != 0)
                    position.Y = entity.Position.Y;

                if (position.X < 0)
                    position.X = 0;
                if (position.Y < 0)
                    position.Y = 0;
                if (entity.Bounds.Right > _physicsWorld.Bounds.Right)
                    position.X = _physicsWorld.Bounds.Right - entity.Size.Width;
                if (entity.Bounds.Bottom > _physicsWorld.Bounds.Bottom)
                    position.Y = _physicsWorld.Bounds.Bottom - entity.Size.Height;
            }
        }
    }
}
