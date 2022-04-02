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
        void AddGameObject(GameObject gameObject);
        /// <summary>
        /// Removes a child game object from the container.
        /// </summary>
        /// <param name="gameObject">The object to remove.</param>
        void RemoveGameObject(GameObject gameObject);
        /// <summary>
        /// Gets the child objects of this container.
        /// </summary>
        /// <returns>The child objects of this container</returns>
        IEnumerable<GameObject> GetGameObjects();
    }
}
