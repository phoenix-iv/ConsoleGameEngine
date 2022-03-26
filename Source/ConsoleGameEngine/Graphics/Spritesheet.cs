using ConsoleGameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameEngine.Graphics
{
    /// <summary>
    /// Represents a spritesheet, which is an image with a defined frame size.
    /// </summary>
    public class Spritesheet
    {
        /// <summary>
        /// The size information about each frame in the image.
        /// </summary>
        public FrameSize FrameSize { get; private set; }
        /// <summary>
        /// The image containing the frames.
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="Spritesheet"/>.
        /// </summary>
        /// <param name="image">The image containing the frames.</param>
        /// <param name="frameSize">The size information for each frame.</param>
        public Spritesheet(Image image, FrameSize frameSize)
        {
            Image = image;
            FrameSize = frameSize;
        }

        /// <summary>
        /// Calculates the clipping information for the specified frame index.
        /// </summary>
        /// <param name="frameIndex">The index of the chosen frame.</param>
        /// <returns>The clipping information.</returns>
        public ClippingInfo CalculateClipping(int frameIndex)
        {
            int perRow = Image.Width / (FrameSize.Width + FrameSize.MarginLeft + FrameSize.MarginRight);
            int row = frameIndex / perRow;
            int col = frameIndex % perRow;
            int x = (FrameSize.Width + FrameSize.MarginLeft + FrameSize.MarginRight) * col + FrameSize.MarginLeft;
            int y = (FrameSize.Height + FrameSize.MarginTop + FrameSize.MarginBottom) * row + FrameSize.MarginTop;
            return new ClippingInfo
            {
                X = x,
                Y = y,
                Width = FrameSize.Width,
                Height = FrameSize.Height
            };
        }

    }
}
