using ConsoleGameEngine.Caching;
using ConsoleGameEngine.Graphics;
using System.Text.Json;

namespace ConsoleGameEngine.Loading
{
    /// <summary>
    /// An object that is used to load content for the game.
    /// </summary>
    public class Loader
    {
        private readonly CacheManager _cache;

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
            _cache.Images.Set(key, LoadImage(path));
        }

        /// <summary>
        /// Loads an image as a spritesheet.
        /// </summary>
        /// <param name="key">The key used to uniquely identify the spritesheet.</param>
        /// <param name="path">The path to the file containing image data.</param>
        /// <param name="frameSize">The frame size information.</param>
        public void Spritesheet(string key, string path, FrameSize frameSize)
        {
            Image image = LoadImage(path);
            _cache.Spritesheets.Set(key, new Spritesheet(image, frameSize));
        }

        /// <summary>
        /// Loads an image as a spritesheet.
        /// </summary>
        /// <param name="key">The key used to uniquely identify the spritesheet.</param>
        /// <param name="path">The path to the file containing image data.</param>
        /// <param name="frameWidth">The width of each frame.</param>
        /// <param name="frameHeight">The height of each frame.</param>
        public void Spritesheet(string key, string path, int frameWidth, int frameHeight)
        {
            Spritesheet(key, path, new FrameSize { Width = frameWidth, Height = frameHeight });
        }

        /// <summary>
        /// Loads a texture atlas and its associated image.
        /// </summary>
        /// <param name="key">The key used to uniquely identify the atlas.</param>
        /// <param name="atlasPath">The path to the texture atlas JSON file.</param>
        /// <param name="imagePath">The path to the image.</param>
        public void TextureAtlas(string key, string atlasPath, string imagePath)
        {
            string json = File.ReadAllText(atlasPath);
            var atlas = JsonSerializer.Deserialize<TextureAtlas>(json);
            if (atlas == null)
                throw new NullReferenceException("Invalid texture atlas format.");

            atlas.Image = LoadImage(imagePath);
            _cache.TextureAtlases.Set(key, atlas);
        }


        /// <summary>
        /// Loads the specified file as an Image.
        /// </summary>
        /// <param name="path">The path to the image.</param>
        /// <returns>An image.</returns>
        protected Image LoadImage(string path)
        {
            string[] imageData = File.ReadAllLines(path);
            return new Image(imageData);
        }

    }
}
