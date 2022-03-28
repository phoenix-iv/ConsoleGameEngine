using ConsoleGameEngine.Components.Physics.Arcade;
using DefaultEcs;
using System.Numerics;

namespace ConsoleGameEngine.Physics.Arcade
{
    /// <summary>
    /// Represents a physics body that moves.
    /// </summary>
    public class DynamicBody : Body
    {
        /// <summary>
        /// The body's current velocity.
        /// </summary>
        public ref Vector2 Velocity => ref Entity.Get<DynamicBodyInfo>().Velocity;

        /// <summary>
        /// Creates a new instance of <see cref="DynamicBody"/>.
        /// </summary>
        /// <param name="entity">The entity associated with the body's owner.</param>
        public DynamicBody(Entity entity) : base(entity)
        {
            entity.Set(new DynamicBodyInfo());
        }
    }
}
