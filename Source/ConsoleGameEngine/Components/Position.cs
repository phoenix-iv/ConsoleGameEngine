using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Components
{
    public struct Position
    {
        public float X;
        public float Y;
    }

    public interface IPosition
    {
        float X { get; set; }
        float Y { get; set; }
    }
}
