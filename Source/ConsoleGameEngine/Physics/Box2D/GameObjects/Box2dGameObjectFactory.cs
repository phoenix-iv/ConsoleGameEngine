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
        /// <summary>
        /// Defines a method that sets up a physics body.
        /// </summary>
        /// <param name="bodyDef">The body definition.</param>
        /// <param name="polygonDef">The polygon fixture definition.  Will be set as a box.</param>
        /// <returns>Either the supplied polygonDef or a new fixture definition which will replace the polygonDef.</returns>
        public delegate FixtureDef SetupBodyDelegate(ref BodyDef bodyDef, PolygonDef polygonDef);

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
        /// <param name="setupBody">A callback used to do any custom setup of the physics body.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody Sprite(string imageKey, float x, float y, SetupBodyDelegate? setupBody = null)
        {
            var sprite = AddSprite<SpriteWithBody>(imageKey, x, y, true);
            return AddBodyAndToPhysicsWorld(sprite, false, setupBody);
        }

        /// <summary>
        /// Creates a new sprite with the specified spritesheet and optionally adds it to the scene.
        /// </summary>
        /// <param name="spritesheetKey">The key to the spritesheet.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame index.</param>
        /// <param name="setupBody">A callback used to do any custom setup of the physics body.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody Sprite(string spritesheetKey, float x, float y, int initialFrame, SetupBodyDelegate? setupBody = null)
        {
            var sprite = AddSprite<SpriteWithBody>(spritesheetKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, false, setupBody);
        }

        /// <summary>
        /// Creates a new sprite with the specified texture atlas and optionally adds it to the scene.
        /// </summary>
        /// <param name="textureAtlasKey">The key to the texture atlas.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame.  If null, the first frame in the atlas will be used.</param>
        /// <param name="setupBody">A callback used to do any custom setup of the physics body.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody Sprite(string textureAtlasKey, float x, float y, string? initialFrame, SetupBodyDelegate? setupBody = null)
        {
            var sprite = AddSprite<SpriteWithBody>(textureAtlasKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, false, setupBody);
        }


        /// <summary>
        /// Creates a new sprite with the specified image and optionally adds it to the scene.
        /// </summary>
        /// <param name="imageKey">The key to the image.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="setupBody">A callback used to do any custom setup of the physics body.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody StaticSprite(string imageKey, float x, float y, SetupBodyDelegate? setupBody = null)
        {
            var sprite = AddSprite<SpriteWithBody>(imageKey, x, y, true);
            return AddBodyAndToPhysicsWorld(sprite, true, setupBody);
        }

        /// <summary>
        /// Creates a new sprite with the specified spritesheet and optionally adds it to the sct78gt88rjijgjugerijrgtuivfervfvervfrhgvergfhveghvgefvhgerbhfene.
        /// </summary>
        /// <param name="spritesheetKey">The key to the spritesheet.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame index.</param>
        /// <param name="setupBody">A callback used to do any custom setup of the physics body.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody StaticSprite(string spritesheetKey, float x, float y, int initialFrame, SetupBodyDelegate? setupBody = null)
        {
            var sprite = AddSprite<SpriteWithBody>(spritesheetKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, true, setupBody);
        }

        /// <summary>
        /// Creates a new sprite with the specified texture atlas and optionally adds it to the scene.
        /// </summary>
        /// <param name="textureAtlasKey">The key to the texture atlas.</param>
        /// <param name="x">The initial x coordinate of the sprite.</param>
        /// <param name="y">The initial y coordinate of the sprite.</param>
        /// <param name="initialFrame">The initial frame.  If null, the first frame in the atlas will be used.</param>
        /// <param name="setupBody">A callback used to do any custom setup of the physics body.</param>
        /// <returns>The newly created sprite.</returns>
        public SpriteWithBody StaticSprite(string textureAtlasKey, float x, float y, string? initialFrame, SetupBodyDelegate? setupBody = null)
        {
            var sprite = AddSprite<SpriteWithBody>(textureAtlasKey, x, y, initialFrame, true);
            return AddBodyAndToPhysicsWorld(sprite, true, setupBody);
        }

        private T AddBodyAndToPhysicsWorld<T>(T o, bool isStatic, SetupBodyDelegate? setupBody = null) where T : GameObject, IBox2dWithBody
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
            var polygonDef = new PolygonDef();
            polygonDef.SetAsBox(size.HalfWidth * _physics.MetersPerChar, size.HalfHeight * _physics.MetersPerChar);

            if (!isStatic)
                polygonDef.Density = 1;

            FixtureDef fixtureDef = polygonDef;

            if (setupBody != null)
                fixtureDef = setupBody(ref bodyDef, polygonDef);

            Body body = _physics.World.CreateBody(bodyDef);
            body.CreateFixture(fixtureDef);

            if (isStatic)
                body.SetStatic();
            else
                body.SetMassFromShapes();

            o.Body = body;
            return o;
        }

    }
}
