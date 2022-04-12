namespace ConsoleGameEngine
{
    /// <summary>
    /// A foreground color, background color, character triplet.  Acts as a text "pixel".
    /// </summary>
    public struct ColorChar : IEquatable<ColorChar>
    {
        /// <summary>
        /// The background color.
        /// </summary>
        public ConsoleColor? BackColor;
        /// <summary>
        /// The character.
        /// </summary>
        public char Char;
        /// <summary>
        /// The foreground color.
        /// </summary>
        public ConsoleColor ForeColor;

        /// <summary>
        /// Determines if this equals the other object.
        /// </summary>
        /// <param name="obj">The other object to compare to this one.</param>
        /// <returns>Whether or not this object is equal to the other one.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ColorChar colorChar && Equals(colorChar);
        }

        /// <summary>
        /// Determines if this ColorChar is equal to the other one.
        /// </summary>
        /// <param name="other">The other ColorChar to compare against.</param>
        /// <returns>Whether or not this ColorChar is equal to the other one.</returns>
        public bool Equals(ColorChar other)
        {
            return Char == other.Char &&
                   ForeColor == other.ForeColor &&
                   BackColor == other.BackColor;
        }

        /// <summary>
        /// Gets a hash code for this object.
        /// </summary>
        /// <returns>A hash code for this object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Char, ForeColor, BackColor);
        }

        /// <summary>
        /// Compares two ColorChars and determines if they are equal to each other.
        /// </summary>
        /// <param name="left">The first ColorChar.</param>
        /// <param name="right">The second ColorChar.</param>
        /// <returns>Whether or not the two ColorChars are equal.</returns>
        public static bool operator ==(ColorChar left, ColorChar right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two ColorChars and determines if they are not equal to each other.
        /// </summary>
        /// <param name="left">The first ColorChar.</param>
        /// <param name="right">The second ColorChar.</param>
        /// <returns>Whether or not the two ColorChars are not equal.</returns>
        public static bool operator !=(ColorChar left, ColorChar right)
        {
            return !(left == right);
        }
    }
}
