using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine
{
    public struct ColorChar : IEquatable<ColorChar>
    {
        public char Char;
        public ConsoleColor Color;

        public override bool Equals(object? obj)
        {
            return obj is ColorChar colorChar && Equals(colorChar);
        }

        public bool Equals(ColorChar other)
        {
            return Char == other.Char &&
                   Color == other.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Char, Color);
        }

        public static bool operator ==(ColorChar left, ColorChar right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ColorChar left, ColorChar right)
        {
            return !(left == right);
        }
    }
}
