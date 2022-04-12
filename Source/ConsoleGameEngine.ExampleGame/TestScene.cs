using Box2DX.Common;
using ConsoleGameEngine.GameObjects;
using ConsoleGameEngine.Graphics;
using ConsoleGameEngine.Physics.Box2D;
using ConsoleGameEngine.Physics.Box2D.GameObjects;

namespace ConsoleGameEngine.ExampleGame
{
    public class TestScene : Scene
    {
#nullable disable
        private ImageObject _imageObject;
        private SpriteWithBody _sprite;
        private readonly List<SpriteWithBody> _sprites = new();
#nullable enable

        public TestScene(Game game) : base(game)
        {
            
        }

        public override void Preload()
        {
            Load.Image("star", "Assets/Star.txt");
            Load.Image("test", "Assets/TestImage.txt");
            Load.Spritesheet("test", "Assets/TestSpritesheet.txt", new FrameSize { Width = 3, Height = 3, MarginLeft = 1, MarginTop = 1 });
        }

        public override void Create()
        {
            var config = new Box2dPhysicsConfig
            {
                Gravity = Vec2.Zero
            };
            Box2dPhysics.Initialize(config);
            DefaultBackgroundColor = ConsoleColor.DarkBlue;
            _imageObject = Add.Image("test", 0, 0);
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                SpriteWithBody sprite = Box2dPhysics.Add.Sprite("star", random.Next(Console.WindowWidth), random.Next(Console.WindowHeight));
                var velocity = new Vec2
                {
                    X = random.Next((int)(-Console.WindowWidth * Box2dPhysics.MetersPerChar), (int)(Console.WindowWidth * Box2dPhysics.MetersPerChar)),
                    Y = random.Next((int)(-Console.WindowHeight * Box2dPhysics.MetersPerChar), (int)(Console.WindowHeight * Box2dPhysics.MetersPerChar))
                };
                sprite.Body.SetLinearVelocity(velocity);
                _sprites.Add(sprite);
            }
            _sprite = Box2dPhysics.Add.Sprite("test", 0, 0, 0);
            _sprite.Body.SetLinearVelocity(new Vec2(2, -1));
            Animations.Add("test", "test", 0, 4, 10, -1);
            _sprite.PlayAnimation("test");
            Box2dPhysics.Start();
        }

        public override void Update(GameTime time)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                if (keyInfo.Key == ConsoleKey.Escape)
                    Game.Exit();

                if (keyInfo.Key == ConsoleKey.RightArrow)
                    _sprite.Position.X += 1;

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                    _sprite.Position.X -= 1;

                if (keyInfo.Key == ConsoleKey.UpArrow)
                    _sprite.Position.Y -= 1;

                if (keyInfo.Key == ConsoleKey.DownArrow)
                    _sprite.Position.Y += 1;
            }
        }
    }
}
