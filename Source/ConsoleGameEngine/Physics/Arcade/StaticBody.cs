using ConsoleGameEngine.Physics.Arcade.Components;
using DefaultEcs;

namespace ConsoleGameEngine.Physics.Arcade
{
    /// <summary>
    /// Represents a physics body that does not move.
    /// </summary>
    public class StaticBody : Body
    {
        /// <summary>
        /// Creates a new instance of <see cref="StaticBody"/>.
        /// </summary>
        /// <param name="entity">The entity associated with the body owner.</param>
        public StaticBody(Entity entity) : base(entity)
        {
            entity.Set(new BodyType { Type = BodyTypeCode.Static });
        }
    }
}
