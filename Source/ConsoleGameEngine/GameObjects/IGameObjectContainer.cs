namespace ConsoleGameEngine.GameObjects
{
    /// <summary>
    /// Defines methods for a game object container.
    /// </summary>
    public interface IGameObjectContainer
    {
        /// <summary>
        /// Adds a child game object to the container.
        /// </summary>
        /// <param name="gameObject">The object to add.</param>
        void AddChild(GameObject gameObject);
        /// <summary>
        /// Removes a child game object from the container.
        /// </summary>
        /// <param name="gameObject">The object to remove.</param>
        void RemoveChild(GameObject gameObject);
    }
}
