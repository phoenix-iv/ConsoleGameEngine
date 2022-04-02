using Auios.QuadTree;
using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Caching;
using ConsoleGameEngine.GameObjects;
using ConsoleGameEngine.Physics.Arcade.GameObjects;
using ConsoleGameEngine.Physics.Arcade.Systems;
using DefaultEcs.System;

namespace ConsoleGameEngine.Physics.Arcade
{
    /// <summary>
    /// Represents an arcade physics system.
    /// </summary>
    public class ArcadePhysics
    {
        /// <summary>
        /// The factory used to add game objects to the arcade physics system.
        /// </summary>
        public ArcadePhysicsGameObjectFactory Add { get; }
        /// <summary>
        /// The physics world.
        /// </summary>
        public PhysicsWorld World { get; }

        private readonly Scene _scene;

        /// <summary>
        /// Creates a new instance of <see cref="ArcadePhysics"/>.
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="animationManager">The game-level animation manager.</param>
        /// <param name="cache">The game-level cache manager.</param>
        public ArcadePhysics(Scene scene, AnimationManager animationManager, CacheManager cache)
        {
            _scene = scene;
            Add = new ArcadePhysicsGameObjectFactory(this, _scene, animationManager, cache);
            World = new PhysicsWorld();
        }

        /// <summary>
        /// Starts the physics system running.
        /// </summary>
        public void Start()
        {
            _scene.RebuildSystem(new ISystem<GameTime>[] { new CollisionSystem(_scene.World, World) });
        }

        /// <summary>
        /// Sets up a collision check and separation between the two physics enabled objects given, which can be single Game Object or Group.
        /// </summary>
        /// <param name="gameObject1">The first game object to check.</param>
        /// <param name="gameObject2">The second game object to check.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you don't require separation then use <see cref="AddOverlapCheck(GameObject, GameObject, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// 
        /// Arcade Physics uses the Projection Method of collision resolution and separation. While it's fast and suitable
        /// for 'arcade' style games it lacks stability when multiple objects are in close proximity or resting upon each other.
        /// The separation that stops two objects penetrating may create a new penetration against a different object. If you
        /// require a high level of stability please consider using an alternative physics system.
        /// </remarks>
        public void AddCollisionCheck(GameObject gameObject1, GameObject gameObject2, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectAndSeparate, gameObject1, gameObject2, collideCallback, processCallback));
        }

        /// <summary>
        /// Sets up a collision check and separation between the two physics enabled objects give.
        /// </summary>
        /// <param name="gameObject">The first game object to check.</param>
        /// <param name="gameObjects">The game objects to check.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you don't require separation then use <see cref="AddOverlapCheck(GameObject, GameObject, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// 
        /// Arcade Physics uses the Projection Method of collision resolution and separation. While it's fast and suitable
        /// for 'arcade' style games it lacks stability when multiple objects are in close proximity or resting upon each other.
        /// The separation that stops two objects penetrating may create a new penetration against a different object. If you
        /// require a high level of stability please consider using an alternative physics system.
        /// </remarks>
        public void AddCollisionCheck(GameObject gameObject, IEnumerable<GameObject> gameObjects, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            AddCollisionCheck(new[] { gameObject }, gameObjects, collideCallback, processCallback);
        }

        /// <summary>
        /// Sets up a collision check and separation among all the objects in the group.
        /// </summary>
        /// <param name="container">The group containing the objects to check collisions against.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you don't require separation then use <see cref="AddCollisionCheck(IGameObjectContainer, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// 
        /// Arcade Physics uses the Projection Method of collision resolution and separation. While it's fast and suitable
        /// for 'arcade' style games it lacks stability when multiple objects are in close proximity or resting upon each other.
        /// The separation that stops two objects penetrating may create a new penetration against a different object. If you
        /// require a high level of stability please consider using an alternative physics system.
        /// </remarks>
        public void AddCollisionCheck(IGameObjectContainer container, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectAndSeparate, container.GetGameObjects(), container.GetGameObjects(), collideCallback, processCallback));
        }

        /// <summary>
        /// Sets up a collision check and separation among all the supplied objects.  The enumerable is iterated and every element in it is tested against the others.
        /// </summary>
        /// <param name="gameObjects">The game objects to check collisions against.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you don't require separation then use <see cref="AddOverlapCheck(IGameObjectContainer, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// 
        /// Arcade Physics uses the Projection Method of collision resolution and separation. While it's fast and suitable
        /// for 'arcade' style games it lacks stability when multiple objects are in close proximity or resting upon each other.
        /// The separation that stops two objects penetrating may create a new penetration against a different object. If you
        /// require a high level of stability please consider using an alternative physics system.
        /// </remarks>
        public void AddCollisionCheck(IEnumerable<GameObject> gameObjects, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectAndSeparate, gameObjects, gameObjects, collideCallback, processCallback));
        }

        /// <summary>
        /// Sets up a collision check and separation among all the supplied objects.  Each member of one group will be tested against each member of the other.
        /// </summary>
        /// <param name="group1">The first group to check.</param>
        /// <param name="group2">The second group to check.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you don't require separation then use <see cref="AddOverlapCheck(IGameObjectContainer, IGameObjectContainer, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// 
        /// Arcade Physics uses the Projection Method of collision resolution and separation. While it's fast and suitable
        /// for 'arcade' style games it lacks stability when multiple objects are in close proximity or resting upon each other.
        /// The separation that stops two objects penetrating may create a new penetration against a different object. If you
        /// require a high level of stability please consider using an alternative physics system.
        /// </remarks>
        public void AddCollisionCheck(IGameObjectContainer group1, IGameObjectContainer group2, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            AddCollisionCheck(group1.GetGameObjects(), group2.GetGameObjects(), collideCallback, processCallback);
        }

        /// <summary>
        /// Sets up a collision check and separation among all the supplied objects.  Each member of one enumerable will be tested against each member of the other.
        /// </summary>
        /// <param name="gameObjects1">The first enumerable to check.</param>
        /// <param name="gameObjects2">The second enumerable to check.</param>
        /// <param name="collideCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you don't require separation then use <see cref="AddOverlapCheck(IEnumerable{GameObject}, IEnumerable{GameObject}, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// 
        /// Arcade Physics uses the Projection Method of collision resolution and separation. While it's fast and suitable
        /// for 'arcade' style games it lacks stability when multiple objects are in close proximity or resting upon each other.
        /// The separation that stops two objects penetrating may create a new penetration against a different object. If you
        /// require a high level of stability please consider using an alternative physics system.
        /// </remarks>
        public void AddCollisionCheck(IEnumerable<GameObject> gameObjects1, IEnumerable<GameObject> gameObjects2, Action<GameObject, GameObject>? collideCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectAndSeparate, gameObjects1, gameObjects2, collideCallback, processCallback));
        }

        /// <summary>
        /// Sets up a collision check between the two physics enabled objects given, which can be single Game Object or Group.
        /// </summary>
        /// <param name="gameObject1">The first game object to check.</param>
        /// <param name="gameObject2">The second game object to check.</param>
        /// <param name="overlapCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you require separation then use <see cref="AddCollisionCheck(GameObject, GameObject, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// </remarks>
        public void AddOverlapCheck(GameObject gameObject1, GameObject gameObject2, Action<GameObject, GameObject>? overlapCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectOnly, gameObject1, gameObject2, overlapCallback, processCallback));
        }

        /// <summary>
        /// Sets up an collision check among all the objects in the group.
        /// </summary>
        /// <param name="container">The group containing the objects to check collisions against.</param>
        /// <param name="overlapCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `collideCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you require separation then use <see cref="AddCollisionCheck(IGameObjectContainer, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `collideCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// </remarks>
        public void AddOverlapCheck(IGameObjectContainer container, Action<GameObject, GameObject>? overlapCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectOnly, container.GetGameObjects(), container.GetGameObjects(), overlapCallback, processCallback));
        }

        /// <summary>
        /// Sets up a collision check among all the supplied objects.  The enumerable is iterated and every element in it is tested against the others.
        /// </summary>
        /// <param name="gameObjects">The game objects to check collisions against.</param>
        /// <param name="overlapCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `overlapCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you require separation then use <see cref="AddCollisionCheck(IEnumerable{GameObject}, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `overlapCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// </remarks>
        public void AddOverlapCheck(IEnumerable<GameObject> gameObjects, Action<GameObject, GameObject>? overlapCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectOnly, gameObjects, gameObjects, overlapCallback, processCallback));
        }

        /// <summary>
        /// Sets up a collision check among all the supplied objects.  Each member of one group will be tested against each member of the other.
        /// </summary>
        /// <param name="group1">The first group to check.</param>
        /// <param name="group2">The second group to check.</param>
        /// <param name="overlapCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `overlapCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you require separation then use <see cref="AddCollisionCheck(IGameObjectContainer, IGameObjectContainer, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `overlapCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// </remarks>
        public void AddOverlapCheck(IGameObjectContainer group1, IGameObjectContainer group2, Action<GameObject, GameObject>? overlapCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            AddOverlapCheck(group1.GetGameObjects(), group2.GetGameObjects(), overlapCallback, processCallback);
        }

        /// <summary>
        /// Sets up a collision check and separation among all the supplied objects.  Each member of one enumerable will be tested against each member of the other.
        /// </summary>
        /// <param name="gameObjects1">The first enumerable to check.</param>
        /// <param name="gameObjects2">The second enumerable to check.</param>
        /// <param name="overlapCallback">An optional callback function that is called if the objects collide.</param>
        /// <param name="processCallback">
        /// An optional callback function that lets you perform additional checks against the two objects if they collide. If this is set then 
        /// `overlapCallback` will only be called if this callback returns `true`.
        /// </param>
        /// <remarks>
        /// If you require separation then use <see cref="AddCollisionCheck(IEnumerable{GameObject}, IEnumerable{GameObject}, Action{GameObject, GameObject}?, Func{GameObject, GameObject, bool}?)"/> instead.
        /// 
        /// Two callbacks can be provided. The `overlapCallback` is invoked if a collision occurs and the two colliding objects are passed to it.
        /// </remarks>
        public void AddOverlapCheck(IEnumerable<GameObject> gameObjects1, IEnumerable<GameObject> gameObjects2, Action<GameObject, GameObject>? overlapCallback = null, Func<GameObject, GameObject, bool>? processCallback = null)
        {
            World.CollisionSets.Add(new CollisionSet(CollisionDetectionType.DetectOnly, gameObjects1, gameObjects2, overlapCallback, processCallback));
        }

    }
}
