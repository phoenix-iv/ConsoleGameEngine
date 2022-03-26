using ConsoleGameEngine.Components;

namespace ConsoleGameEngine.Graphics
{
    /// <summary>
    /// Represents a texture atlas that defines frame locations within an image.
    /// </summary>
    public class TextureAtlas
    {
        /// <summary>
        /// The frame definitions.
        /// </summary>
        public List<TextureAtlasFrame> Frames { get; set; } = new List<TextureAtlasFrame>();
        /// <summary>
        /// The image that contains the frames.
        /// </summary>
        public Image Image { get; set; } = new Image();

        /// <summary>
        /// Calculates the clipping information for the specified texture atlas and frame name.
        /// </summary>
        /// <param name="frameName">The frame name.</param>
        /// <returns>The clipping information.</returns>
        public ClippingInfo CalculateClipping(string frameName)
        {
            TextureAtlasFrame frame = Frames.First(f => f.FileName == frameName);
            return new ClippingInfo
            {
                X = frame.Frame.X,
                Y = frame.Frame.Y,
                Width = frame.Frame.W,
                Height = frame.Frame.H
            };
        }
    }

    /// <summary>
    /// Represents a frame definition within a texture atlas.
    /// </summary>
    public class TextureAtlasFrame
    {
        /// <summary>
        /// The name of the frame.
        /// </summary>
        public string FileName { get; set; } = "";
        /// <summary>
        /// Whether or not the frame was rotated.
        /// </summary>
        public bool Rotated { get; set; } 
        /// <summary>
        /// Whether or not the frame was trimmed.
        /// </summary>
        public bool Trimmed { get; set; }
        /// <summary>
        /// The frame's coordinates.
        /// </summary>
        public TextureAtlasCoordinates Frame { get; set; } = new TextureAtlasCoordinates();
        /// <summary>
        /// The sprite source coordinates.
        /// </summary>
        public TextureAtlasCoordinates SpriteSourceSize { get; set; } = new TextureAtlasCoordinates();
        /// <summary>
        /// The source size.
        /// </summary>
        public TextureAtlasSize SourceSize { get; set; } = new TextureAtlasSize();
    }

    /// <summary>
    /// Represents a texture atlas size.
    /// </summary>
    public class TextureAtlasSize
    {
        /// <summary>
        /// The width.
        /// </summary>
        public int W { get; set; }
        /// <summary>
        /// The height.
        /// </summary>
        public int H { get; set; }
    }

    /// <summary>
    /// Represents coordinates within a texture atlas.
    /// </summary>
    public class TextureAtlasCoordinates : TextureAtlasSize
    {
        /// <summary>
        /// The x coordinate.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The y coordinate.
        /// </summary>
        public int Y { get; set; }
    }
}
