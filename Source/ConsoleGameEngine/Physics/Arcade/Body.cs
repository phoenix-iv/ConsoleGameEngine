using ConsoleGameEngine.Components;
using ConsoleGameEngine.Physics.Arcade.Components;
using DefaultEcs;
using System.Drawing;

namespace ConsoleGameEngine.Physics.Arcade
{
    /// <summary>
    /// Represents a physics body.
    /// </summary>
    public abstract class Body
    {
        /// <summary>
        /// The entity associated with this body's owner.
        /// </summary>
        public Entity Entity { get; }
        /// <summary>
        /// The body postition relative to its owner.
        /// </summary>
        public ref Position Offset => ref Entity.Get<BodyPosition>().Offset;

        /// <summary>
        /// The size of the body.
        /// </summary>
        public ref SizeF Size => ref Entity.Get<BodySize>().Size;

        /// <summary>
        /// Creates a new instance of <see cref="Body"/>.
        /// </summary>
        /// <param name="entity">The entity associated with the body owner.</param>
        public Body(Entity entity)
        {
            Entity = entity;
            entity.Set(new BodyPosition());
            entity.Set(new BodySize());
        }
    }
}
