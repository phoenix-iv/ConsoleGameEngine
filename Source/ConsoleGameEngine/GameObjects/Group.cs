using DefaultEcs;

namespace ConsoleGameEngine.GameObjects
{
    /// <summary>
    /// Represents a group of game objects.
    /// </summary>
    public class Group : IGameObjectContainer
    {
        private readonly List<GameObject> _children = new();
        /// <summary>
        /// The child game objects that are part of this group.
        /// </summary>
        public IReadOnlyList<GameObject> Children => _children;

        /// <summary>
        /// Creates a new instance of <see cref="Group"/>.
        /// </summary>
        public Group()
        {

        }

        /// <summary>
        /// Adds the specified game object to this group.
        /// </summary>
        /// <param name="gameObject">The game object to add.</param>
        public void AddGameObject(GameObject gameObject)
        {
            _children.Add(gameObject);
        }

        /// <summary>
        /// Gets the children of this group.
        /// </summary>
        /// <returns>The children of this group.</returns>
        public IEnumerable<GameObject> GetGameObjects() => Children;

        /// <summary>
        /// Removes the specified game object from this goup.
        /// </summary>
        /// <param name="gameObject">The game object to remove.</param>
        public void RemoveGameObject(GameObject gameObject)
        {
            _children.Remove(gameObject);
        }
    }
}
