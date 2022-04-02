using ConsoleGameEngine.GameObjects;
using ConsoleGameEngine.Graphics;
using ConsoleGameEngine.Physics.Arcade.GameObjects;

namespace ConsoleGameEngine.ExampleGame
{
    public class TestScene : Scene
    {
#nullable disable
        private ImageObject _imageObject;
        private SpriteWithDynamicBody _sprite;
        private List<SpriteWithDynamicBody> _sprites = new List<SpriteWithDynamicBody>();
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
            ArcadePhysics.Start();
            _imageObject = Add.Image("test", 0, 0);
            var random = new Random();
            for (int i = 0; i < 200; i++)
            {
                SpriteWithDynamicBody sprite = ArcadePhysics.Add.Sprite("star", random.Next(Console.WindowWidth), random.Next(Console.WindowHeight));
                sprite.Body.Velocity.X = random.Next(-Console.WindowWidth, Console.WindowWidth);
                sprite.Body.Velocity.Y = random.Next(-Console.WindowHeight, Console.WindowHeight);
                _sprites.Add(sprite);
            }
            ArcadePhysics.AddCollisionCheck(_sprites);
            _sprite = ArcadePhysics.Add.Sprite("test", 0, 0, 0, true);
            _sprite.Body.Velocity.X = 4;
            _sprite.Body.Velocity.Y = 2;
            ArcadePhysics.AddCollisionCheck(_sprite, _sprites);
            Animations.Add("test", "test", 0, 4, 10, -1);
            _sprite.PlayAnimation("test");
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
