using ConsoleGameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.ExampleGame
{
    public class TestScene : Scene
    {
#nullable disable
        private ImageObject _imageObject;
#nullable enable

        public TestScene(Game game) : base(game)
        {
            
        }

        public override void Preload()
        {
            Load.Image("test", "Assets/TestImage.txt");
        }

        public override void Create()
        {
            _imageObject = Add.Image("test", 0, 0);
        }

        public override void Update(TimeSpan delta)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                if (keyInfo.Key == ConsoleKey.Escape)
                    Game.Exit();

                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    _imageObject.Position.X += 1;
                }

                if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    _imageObject.Position.X -= 1;
                }
            }
            base.Update(delta);
        }
    }
}
