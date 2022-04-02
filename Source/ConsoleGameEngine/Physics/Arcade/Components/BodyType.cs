namespace ConsoleGameEngine.Physics.Arcade.Components
{
    /// <summary>
    /// Specifies the type of a physics body.
    /// </summary>
    public enum BodyTypeCode
    {
        /// <summary>
        /// Indicates that the body is dynamic (ie. can move).
        /// </summary>
        Dynamic,
        /// <summary>
        /// Indicates that the body is static (ie. cannot move).
        /// </summary>
        Static
    }

    /// <summary>
    /// Contains data about a body's type
    /// </summary>
    public struct BodyType
    {
        /// <summary>
        /// The body's type.
        /// </summary>
        public BodyTypeCode Type;
    }
}
