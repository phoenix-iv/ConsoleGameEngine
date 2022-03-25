using ConsoleGameEngine.Caching;
using DefaultEcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.GameObjects
{
    public class GameObjectFactory
    {
        private readonly Scene _scene;
        private readonly CacheManager _cache;

        public GameObjectFactory(Scene scene, CacheManager cache)
        {
            _scene = scene;
            _cache = cache;
        }

        public ImageObject Image(string key, float x, float y)
        {
            Image image = _cache.Images.Get(key);
            Entity entity = _scene.World.CreateEntity();
            var imageObject = new ImageObject(entity, image);
            imageObject.Position.X = x;
            imageObject.Position.Y = y;
            _scene.AddChild(imageObject);
            return imageObject;
        }
    }
}
