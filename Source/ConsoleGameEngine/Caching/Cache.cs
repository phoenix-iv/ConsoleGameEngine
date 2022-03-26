namespace ConsoleGameEngine.Caching
{
    /// <summary>
    /// A cache used to store content.
    /// </summary>
    /// <typeparam name="T">The type of content this cache stores.</typeparam>
    public class Cache<T>
    {
        private readonly Dictionary<string, T> _cache = new();

        
        /// <summary>
        /// Gets an item from the cache.
        /// </summary>
        /// <param name="key">The unique key for the item.</param>
        /// <returns></returns>
        public T Get(string key)
        {
            return _cache[key];
        }

        /// <summary>
        /// Puts an item in the cache.
        /// </summary>
        /// <param name="key">A unique key for the item.</param>
        /// <param name="value">The item to put in the cache.</param>
        public void Set(string key, T value)
        {
            _cache[key] = value;
        }
    }
}
