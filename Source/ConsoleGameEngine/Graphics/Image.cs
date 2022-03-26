namespace ConsoleGameEngine.Graphics
{
    /// <summary>
    /// Represents an text-based image.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// The height of the image.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// The width of the image.
        /// </summary>
        public int Width { get; private set; }
        private ColorChar[][] _data = Array.Empty<ColorChar[]>();
        /// <summary>
        /// The image data.  An array of arrays of color/character pairs representing text pixels.
        /// </summary>
        public ColorChar[][] Data 
        {
            get => _data;
            set
            {
                _data = value;
                CalculateSize();
            }
        }

        /// <summary>
        /// Creates a new empty image.
        /// </summary>
        public Image()
        {

        }

        /// <summary>
        /// Creates a new image using the specified strings.
        /// </summary>
        /// <param name="data">The data used to translate to ColorChars.</param>
        public Image(string[] data)
        {
            SetDataFromStrings(data);
        }

        /// <summary>
        /// Sets data from the specified array of strings.
        /// </summary>
        /// <param name="strings">The strings to translate to ColorChars.</param>
        public void SetDataFromStrings(string[] strings)
        {
            int width = strings.Max(l => l.Length / 2);
            var data = new ColorChar[strings.Length][];

            for (int y = 0; y < strings.Length; y++)
            {
                data[y] = new ColorChar[width];
                for(int x = 0; x < strings[y].Length; x += 2)
                {
                    var color = strings[y][x + 1] switch
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
                    data[y][x / 2] = new ColorChar { Char = strings[y][x], Color = color };
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
