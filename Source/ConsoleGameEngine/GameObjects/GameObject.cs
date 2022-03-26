using ConsoleGameEngine.Components;
using DefaultEcs;

namespace ConsoleGameEngine.GameObjects
{
    /// <summary>
    /// Represents an object within the scene.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// The unique ID of the object.
        /// </summary>
        public int Id => Entity.Get<EntityIdentifier>().Id;
        /// <summary>
        /// The entity tied to this object.
        /// </summary>
        public Entity Entity { get; }

        /// <summary>
        /// Creates a new instance of the game object.
        /// </summary>
        /// <param name="entity">The entity that is tied to this game object</param>
        public GameObject(Entity entity)
        {
            Entity = entity;
        }
    }
}
