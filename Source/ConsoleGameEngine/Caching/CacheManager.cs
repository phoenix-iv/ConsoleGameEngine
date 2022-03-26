using ConsoleGameEngine.Graphics;

namespace ConsoleGameEngine.Caching
{
    /// <summary>
    /// Manages the different caches used to store content.
    /// </summary>
    public class CacheManager
    {
        /// <summary>
        /// The image cache.
        /// </summary>
        public Cache<Image> Images { get; } = new Cache<Image>();
        /// <summary>
        /// The texture atlas cache.
        /// </summary>
        public Cache<TextureAtlas> TextureAtlases { get; } = new Cache<TextureAtlas>();
    }
}
