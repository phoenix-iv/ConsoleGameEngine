using ConsoleGameEngine.GameObjects;

namespace ConsoleGameEngine.Physics.Arcade
{
    /// <summary>
    /// Defines collision detection types.
    /// </summary>
    public enum CollisionDetectionType
    {
        /// <summary>
        /// Indicates that collision should be detected but no separation should occur.  AKA overlap.
        /// </summary>
        DetectOnly,
        /// <summary>
        /// Indicates that collision should be detected and bodies should be separated.
        /// </summary>
        DetectAndSeparate
    }

    /// <summary>
    /// Represents a set of collision configurations
    /// </summary>
    public class CollisionSet
    {
        /// <summary>
        /// A callback that gets called when a collision between two objects occurs.
        /// </summary>
        public Action<GameObject, GameObject>? CollideCallback { get; set; }
        /// <summary>
        /// The first set of objects.
        /// </summary>
        public IEnumerable<GameObject> Objects1 { get; set; }
        /// <summary>
        /// The second set of objects.
        /// </summary>
        public IEnumerable<GameObject> Objects2 { get; set; }
        /// <summary>
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// <see cref="CollideCallback"/> will only be called if this callback returns `true`.
        /// </summary>
        public Func<GameObject, GameObject, bool>? ProcessCallback { get; set; }
        /// <summary>
        /// The type of collision that should occur for this set.
        /// </summary>
        public CollisionDetectionType Type { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="CollisionSet"/> with the specified objects.
        /// </summary>
        /// <param name="type">The type of collision detection.</param>
        /// <param name="object1">The first object.</param>
        /// <param name="object2">The second object.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        public CollisionSet(CollisionDetectionType type, GameObject object1, GameObject object2, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
            : this(type, new[] { object1 }, new[] { object2 }, collideCallback, processCallback)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="CollisionSet"/> with the specified objects.
        /// </summary>
        /// <param name="type">The type of collision detection.</param>
        /// <param name="object1">The first object.</param>
        /// <param name="objects2">The set of objects.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        public CollisionSet(CollisionDetectionType type, GameObject object1, IEnumerable<GameObject> objects2, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
            : this(type, new[] { object1 }, objects2, collideCallback, processCallback)
        {

        }

        /// <summary>
        /// Creates a new instance of <see cref="CollisionSet"/> with the specified objects.
        /// </summary>
        /// <param name="type">The type of collision detection.</param>
        /// <param name="objects1">The first set of objects.</param>
        /// <param name="objects2">The second set of objects.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        public CollisionSet(CollisionDetectionType type, IEnumerable<GameObject> objects1, IEnumerable<GameObject> objects2, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            Type = type;
            Objects1 = objects1;
            Objects2 = objects2;
            CollideCallback = collideCallback;
            ProcessCallback = processCallback;
        }
    }
}
