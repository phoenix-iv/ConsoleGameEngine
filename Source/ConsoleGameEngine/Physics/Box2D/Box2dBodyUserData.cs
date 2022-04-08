namespace ConsoleGameEngine.Physics.Box2D
{
    /// <summary>
    /// The data that must be assigned to the <see cref="Box2DX.Dynamics.BodyDef.UserData"/>.
    /// </summary>
    public class Box2dBodyUserData
    {
        /// <summary>
        /// The ID of the entity that the body is associated with.
        /// </summary>
        public int EntityId { get; }
        /// <summary>
        /// The object that contains the body.
        /// </summary>
        public IBox2dWithBody ObjectWithBody { get; }

        /// <summary>
        /// Creates a new instance of <see cref="Box2dBodyUserData"/>.
        /// </summary>
        /// <param name="entityId">The ID of the entity associated with the body.</param>
        /// <param name="objectWithBody">The object that contains the body.</param>
        public Box2dBodyUserData(int entityId, IBox2dWithBody objectWithBody)
        {
            EntityId = entityId;
            ObjectWithBody = objectWithBody;
        }
    }
}
