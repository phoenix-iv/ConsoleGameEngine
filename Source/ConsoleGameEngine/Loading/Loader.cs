using ConsoleGameEngine.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Loading
{
    public class Loader
    {
        private CacheManager _cache;

        public Loader(CacheManager cache)
        {
            _cache = cache;
        }

        public void Image(string key, string path)
        {
            string[] imageData = File.ReadAllLines(path, Encoding.ASCII);
            _cache.Images.Set(key, new Image(imageData));
        }
    }
}
