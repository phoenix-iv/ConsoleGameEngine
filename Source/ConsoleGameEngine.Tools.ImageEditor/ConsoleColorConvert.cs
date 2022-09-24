namespace ConsoleGameEngine.Tools.ImageEditor
{
    internal static class ConsoleColorConvert
    {
        public static Color ToMauiColor(ConsoleColor consoleColor)
        {
            return consoleColor switch
            {
                ConsoleColor.Black => Colors.Black,
                ConsoleColor.DarkBlue => Color.FromArgb("000080"),
                ConsoleColor.DarkGreen => Color.FromArgb("008000"),
                ConsoleColor.DarkCyan => Color.FromArgb("008080"),
                ConsoleColor.DarkRed => Color.FromArgb("800000"),
                ConsoleColor.DarkMagenta => Color.FromArgb("800080"),
                ConsoleColor.DarkYellow => Color.FromArgb("808000"),
                ConsoleColor.Gray => Color.FromArgb("C0C0C0"),
                ConsoleColor.DarkGray => Color.FromArgb("808080"),
                ConsoleColor.Blue => Color.FromArgb("0000FF"),
                ConsoleColor.Green => Color.FromArgb("00FF00"),
                ConsoleColor.Cyan => Color.FromArgb("00FFFF"),
                ConsoleColor.Red => Color.FromArgb("FF0000"),
                ConsoleColor.Magenta => Color.FromArgb("FF00FF"),
                ConsoleColor.Yellow => Color.FromArgb("FFFF00"),
                ConsoleColor.White => Colors.White,
                _ => Colors.Black
            };
        }
    }
}
