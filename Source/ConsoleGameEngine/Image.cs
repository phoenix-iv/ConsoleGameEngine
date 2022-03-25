using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine
{
    public class Image
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        private ColorChar[][] _data = Array.Empty<ColorChar[]>();
        public ColorChar[][] Data 
        {
            get => _data;
            set
            {
                _data = value;
                CalculateSize();
            }
        }

        public Image(string[] data)
        {
            SetDataFromStrings(data);
        }

        public void SetDataFromStrings(string[] lines)
        {
            int width = lines.Max(l => l.Length / 2);
            var data = new ColorChar[lines.Length][];

            for (int y = 0; y < lines.Length; y++)
            {
                data[y] = new ColorChar[width];
                for(int x = 0; x < lines[y].Length; x += 2)
                {
                    var color = lines[y][x + 1] switch
                    {
                        '0' => ConsoleColor.Black,
                        '1' => ConsoleColor.DarkBlue,
                        '2' => ConsoleColor.DarkGreen,
                        '3' => ConsoleColor.DarkCyan,
                        '4' => ConsoleColor.DarkRed,
                        '5' => ConsoleColor.DarkMagenta,
                        '6' => ConsoleColor.DarkYellow,
                        '7' => ConsoleColor.Gray,
                        '8' => ConsoleColor.DarkGray,
                        '9' => ConsoleColor.Blue,
                        'a' or 'A' => ConsoleColor.Green,
                        'b' or 'B' => ConsoleColor.Cyan,
                        'c' or 'C' => ConsoleColor.Red,
                        'd' or 'D' => ConsoleColor.Magenta,
                        'e' or 'E' => ConsoleColor.Yellow,
                        'f' or 'F' => ConsoleColor.White,
                        _ => ConsoleColor.Black
                    };
                    data[y][x / 2] = new ColorChar { Char = lines[y][x], Color = color };
                }
            }

            Data = data;
        }

        private void CalculateSize()
        {
            Width = 0;
            Height = 0;

            if (_data.Length == 0)
                return;

            Width = _data[0].Length;
            Height = _data.Length;
        }
    }
}
