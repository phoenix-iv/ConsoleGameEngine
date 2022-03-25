using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Caching
{
    public class Cache<T>
    {
        private readonly Dictionary<string, T> _cache = new();

        public T Get(string key)
        {
            return _cache[key];
        }

        public void Set(string key, T value)
        {
            _cache[key] = value;
        }
    }
}
