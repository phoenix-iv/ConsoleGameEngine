﻿using ConsoleGameEngine.Cameras;
using ConsoleGameEngine.Components;
using ConsoleGameEngine.Graphics;
using DefaultEcs;
using DefaultEcs.System;
using System.Drawing;

namespace ConsoleGameEngine.Systems
{
    /// <summary>
    /// Represents a system that renders to the console.
    /// </summary>
    internal class RenderSystem : AEntitySetSystem<object?>
    {
        private readonly Scene _scene;
        private readonly Camera _camera;
        private ColorChar[,] _previousBuffer;
        private int _previousBufferWidth;
        private int _previousBufferHeight;

        /// <summary>
        /// Creates a new instance of the <see cref="RenderSystem"/>.
        /// </summary>
        /// <param name="world">The ECS world the system uses.</param>
        /// <param name="scene">The scene.</param>
        public RenderSystem(World world, Scene scene) : base(world.GetEntities().With<Position>().With<ClippingInfo>().With<Image>().AsSet())
        {
            _scene = scene;
            _camera = scene.Camera;
            _previousBufferWidth = Console.WindowWidth;
            _previousBufferHeight = Console.WindowHeight;
            _previousBuffer = new ColorChar[Console.WindowWidth, Console.WindowHeight];
        }

        /// <summary>
        /// Updates the render system and renders to the console.
        /// </summary>
        /// <param name="state">The render state.  Currently will always be null.</param>
        /// <param name="entities">The entities that contain rendering information.</param>
        protected override void Update(object? state, ReadOnlySpan<Entity> entities)
        {
            Console.CursorVisible = false;
            int bufferWidth = Console.WindowWidth;
            int bufferHeight = Console.WindowHeight;
            var buffer = new ColorChar[bufferWidth, bufferHeight];

            foreach(Entity entity in entities)
            {
                var position = entity.Get<Position>();
                var clipping = entity.Get<ClippingInfo>();
                var image = entity.Get<Image>();
                int startX = (int)Math.Round(position.X - _camera.Position.X);
                int y = (int)Math.Round(position.Y - _camera.Position.Y);
                if (y > Console.WindowHeight - 1)
                    continue;
                int endSourceY = clipping.Y + clipping.Height;
                int endSourceX = clipping.X + clipping.Width;
                for (int sourceY = clipping.Y; sourceY < endSourceY; sourceY++)
                {
                    if (y < 0)
                        continue;

                    int x = startX;
                    for (int sourceX = clipping.X; sourceX < endSourceX; sourceX++)
                    {
                        if (x < 0)
                        {
                            x++;
                            continue;
                        }

                        if (x > bufferWidth - 1)
                        {
                            break;
                        }

                        ColorChar c = image.Data[sourceY][sourceX];
                        buffer[x, y] = c;
                        x++;
                    }
                    y++;
                    if (y > bufferHeight - 1)
                        break;
                }
            }
            
            if (_previousBufferWidth != bufferWidth || _previousBufferHeight != bufferHeight)
            {
                FullRender(buffer, bufferWidth, bufferHeight);
            }
            else
            {
                var bufferDiff = new Dictionary<Point, ColorChar>();
                for (int y = 0; y < bufferHeight; y++)
                {
                    for (int x = 0; x < bufferWidth; x++)
                    {
                        ColorChar c = buffer[x, y];
                        if (_previousBuffer[x, y] != c)
                        {
                            bufferDiff.Add(new Point(x, y), c);
                        }
                    }
                }

                if (bufferDiff.Count > 0)
                    PartialRender(bufferDiff);
            }

            _previousBufferWidth = bufferWidth;
            _previousBufferHeight = bufferHeight;
            _previousBuffer = buffer;
        }

        private void FullRender(ColorChar[,] buffer, int bufferWidth, int bufferHeight)
        {
            // Console window size changed, skip this render
            if (bufferWidth != Console.WindowWidth || bufferHeight != Console.WindowHeight)
                return;

            Console.Clear();
            for(int y = 0; y < bufferHeight; y++)
            {
                // Sanity check in case window size changed during render
                if (y > Console.WindowHeight - 1)
                    break;

                for (int x = 0; x < bufferWidth; x++)
                {
                    // Sanity check in case window size changed during render
                    if (x > Console.WindowWidth - 1)
                        break;

                    ColorChar c = buffer[x, y];
                    if (c.Char == ' ' || c.Char == '\0')
                        continue;

                    RenderChar(x, y, c);
                }
            }
        }

        private void PartialRender(Dictionary<Point, ColorChar> bufferDiff)
        {
            foreach (var kvp in bufferDiff)
            {
                Point p = kvp.Key;
                if (p.X > Console.WindowWidth - 1 || p.Y > Console.WindowHeight - 1)
                    continue;

                RenderChar(p.X, p.Y, kvp.Value);
            }
        }

        private void RenderChar(int x, int y, ColorChar c)
        {
            if (c.Char == '\0')
                c.Char = ' ';
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = c.BackColor ?? _scene.DefaultBackgroundColor;
            Console.ForegroundColor = c.ForeColor;
            Console.Write(c.Char);
        }

    }
}
