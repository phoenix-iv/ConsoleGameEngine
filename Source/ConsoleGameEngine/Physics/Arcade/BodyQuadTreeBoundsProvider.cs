using Auios.QuadTree;
using ConsoleGameEngine.Components;

namespace ConsoleGameEngine.Physics.Arcade
{
    internal class BodyQuadTreeBoundsProvider : IQuadTreeObjectBounds<Body>
    {
        public float GetBottom(Body obj) => obj.Entity.Get<Position>().Y + obj.Offset.Y + obj.Size.Height;
        public float GetLeft(Body obj) => obj.Entity.Get<Position>().X + obj.Offset.X;
        public float GetRight(Body obj) => obj.Entity.Get<Position>().X + obj.Offset.X + obj.Size.Width;
        public float GetTop(Body obj) => obj.Entity.Get<Position>().Y + obj.Offset.Y;
    }
}
