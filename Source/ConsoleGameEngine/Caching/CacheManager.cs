using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Caching
{
    public class CacheManager
    {
        public Cache<Image> Images { get; } = new Cache<Image>();
    }
}
