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
#nullable enable

        public TestScene(Game game) : base(game)
        {
            
        }

        public override void Preload()
        {
            Load.Image("test", "Assets/TestImage.txt");
            Load.Spritesheet("test", "Assets/TestSpritesheet.txt", new FrameSize { Width = 3, Height = 3, MarginLeft = 1, MarginTop = 1 });
        }

        public override void Create()
        {
            ArcadePhysics.Start();
            _imageObject = Add.Image("test", 0, 0);
            _sprite = ArcadePhysics.Add.Sprite("test", 0, 0, 0, true);
            _sprite.Body.Velocity.X = 1;
            _sprite.Body.Velocity.Y = 2;
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
                {
                    _sprite.Position.X += 1;
                }

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    _sprite.Position.X -= 1;
                }
            }
        }
    }
}
