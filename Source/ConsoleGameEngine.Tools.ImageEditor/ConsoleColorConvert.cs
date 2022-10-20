using System;

namespace ConsoleGameEngine.Tools.ImageEditor
{
    internal static class ConsoleColorConvert
    {
        public static string ToHexColor(ConsoleColor color)
        {
            return color switch
            {
                ConsoleColor.Black => "000000",
                ConsoleColor.DarkBlue => "000080",
                ConsoleColor.DarkGreen => "008000",
                ConsoleColor.DarkCyan => "008080",
                ConsoleColor.DarkRed => "800000",
                ConsoleColor.DarkMagenta => "800080",
                ConsoleColor.DarkYellow => "808000",
                ConsoleColor.Gray => "C0C0C0",
                ConsoleColor.DarkGray => "808080",
                ConsoleColor.Blue => "0000FF",
                ConsoleColor.Green => "00FF00",
                ConsoleColor.Cyan => "00FFFF",
                ConsoleColor.Red => "FF0000",
                ConsoleColor.Magenta => "FF00FF",
                ConsoleColor.Yellow => "FFFF00",
                ConsoleColor.White => "FFFFFF",
                _ => "000000"
            };
        }

        public static Color ToMauiColor(ConsoleColor color)
        {
            return Color.FromArgb(ToHexColor(color));
        }
    }
}
