using Box2DX.Common;
using Box2DX.Dynamics;
using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Caching;
using ConsoleGameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Physics.Box2D.GameObjects
{
    /// <summary>
    /// Creates game objects that are attached to Box2D physics bodies.
    /// </summary>
    public class Box2dGameObjectFactory : GameObjectFactoryBase
    {
        private readonly Box2dPhysics _physics;

        /// <summary>
        /// Creates a new instance of <see cref="Box2dGameObjectFactory"/>
        /// </summary>
        /// <param name="physics">The physics system to add bodies to.</param>
        /// <param name="scene">The scene to add objects to.</param>
        /// <param name="animationManager">The game level animation manager used for objects that need it.</param>
        /// <param name="cache">The game level cache manager.</param>
        public Box2dGameObjectFactory(Box2dPhysics physics, Scene scene, AnimationManager animationManager, CacheManager cache) : base(scene, animationManager, cache)
        {
            _physics = physics;
        }

        /// <summary>
        /// Creates a new sprite with the specified image and optionally adds it to the scene.
        /// </summary>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="collisionFilter">The collision filter data.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody Sprite(string imageKey, float x, float y, FilterData? collisionFilter = null)
        {
            var sprite = AddSprite<SpriteWithBody>(imageKey, x, y, true);
            return AddBodyAndToPhysicsWorld(sprite, false, collisionFilter);
        }

        /// <summary>
        /// Creates a new sprite with the specified spritesheet and optionally adds it to the scene.
        /// </summary>
        /// <param name="spritesheetKey">The key to the spritesheet.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame index.</param>
        /// <param name="collisionFilter">The collision filter data.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody Sprite(string spritesheetKey, float x, float y, int initialFrame, FilterData? collisionFilter = null)
        {
            var sprite = AddSprite<SpriteWithBody>(spritesheetKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, false, collisionFilter);
        }

        /// <summary>
        /// Creates a new sprite with the specified texture atlas and optionally adds it to the scene.
        /// </summary>
        /// <param name="textureAtlasKey">The key to the texture atlas.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame.  If null, the first frame in the atlas will be used.</param>
        /// <param name="collisionFilter">The collision filter data.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody Sprite(string textureAtlasKey, float x, float y, string? initialFrame, FilterData? collisionFilter = null)
        {
            var sprite = AddSprite<SpriteWithBody>(textureAtlasKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, false, collisionFilter);
        }


        /// <summary>
        /// Creates a new sprite with the specified image and optionally adds it to the scene.
        /// </summary>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="collisionFilter">The collision filter data.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody StaticSprite(string imageKey, float x, float y, FilterData? collisionFilter = null)
        {
            var sprite = AddSprite<SpriteWithBody>(imageKey, x, y, true);
            return AddBodyAndToPhysicsWorld(sprite, true, collisionFilter);
        }

        /// <summary>
        /// Creates a new sprite with the specified spritesheet and optionally adds it to the sct78gt88rjijgjugerijrgtuivfervfvervfrhgvergfhveghvgefvhgerbhfene.
        /// </summary>
        /// <param name="spritesheetKey">The key to the spritesheet.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame index.</param>
        /// <param name="collisionFilter">The collision filter data.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody StaticSprite(string spritesheetKey, float x, float y, int initialFrame, FilterData? collisionFilter = null)
        {
            var sprite = AddSprite<SpriteWithBody>(spritesheetKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, true, collisionFilter);
        }

        /// <summary>
        /// Creates a new sprite with the specified texture atlas and optionally adds it to the scene.
        /// </summary>
        /// <param name="textureAtlasKey">The key to the texture atlas.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame.  If null, the first frame in the atlas will be used.</param>
        /// <param name="collisionFilter">The collision filter data.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody StaticSprite(string textureAtlasKey, float x, float y, string? initialFrame, FilterData? collisionFilter = null)
        {
            var sprite = AddSprite<SpriteWithBody>(textureAtlasKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, true, collisionFilter);
        }

        private T AddBodyAndToPhysicsWorld<T>(T o, bool isStatic, FilterData? collisionFilter) where T : GameObject, IBox2dWithBody
        {
            var bodyDef = new BodyDef
            {
                FixedRotation = true,
                UserData = new Box2dBodyUserData(o.Id, o)
            };
            var position = o.Entity.Get<Components.Position>();
            var size = o.Entity.Get<Components.EntitySize>();
            Vec2 bodyPosition = _physics.ScreenToWorldPoint(position);
            bodyPosition.X += size.HalfWidth * _physics.MetersPerChar;
            bodyPosition.Y -= size.HalfHeight * _physics.MetersPerChar;
            bodyDef.Position = bodyPosition;
            Body body = _physics.World.CreateBody(bodyDef);
            var shapeDef = new PolygonDef();
            shapeDef.SetAsBox(size.Width * _physics.MetersPerChar, size.Height * _physics.MetersPerChar);

            if (collisionFilter != null)
                shapeDef.Filter = collisionFilter.Value;  

            if (isStatic)
                body.SetStatic();
            else
                shapeDef.Density = 1;

            body.CreateFixture(shapeDef);

            if (!isStatic)
                body.SetMassFromShapes();

            o.Body = body;
            return o;
        }

    }
}
