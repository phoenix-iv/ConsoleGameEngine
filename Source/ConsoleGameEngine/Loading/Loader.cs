using ConsoleGameEngine.Caching;
using ConsoleGameEngine.Graphics;
using System.Text;

namespace ConsoleGameEngine.Loading
{
    /// <summary>
    /// An object that is used to load content for the game.
    /// </summary>
    public class Loader
    {
        private CacheManager _cache;

        /// <summary>
        /// Creates a new instance of <see cref="Loader"/>.
        /// </summary>
        /// <param name="cache">The cache used to store content.</param>
        public Loader(CacheManager cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Loads an image.
        /// </summary>
        /// <param name="key">The key used to uniquely identify the image.</param>
        /// <param name="path">The path to the file containing image data.</param>
        public void Image(string key, string path)
        {
            string[] imageData = File.ReadAllLines(path, Encoding.UTF8);
            _cache.Images.Set(key, new Image(imageData));
        }
    }
}
