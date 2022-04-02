using Auios.QuadTree;
using System.Drawing;

namespace ConsoleGameEngine.Physics.Arcade
{
    /// <summary>
    /// Represents the physics world.
    /// </summary>
    public class PhysicsWorld
    {
        private RectangleF _bounds;
        /// <summary>
        /// The bounds of this world.
        /// </summary>
        public RectangleF Bounds => _bounds;
        internal List<CollisionSet> CollisionSets { get; private set; }
         /// <summary>
        /// The quad tree used to partition physics bodies.
        /// </summary>
        internal QuadTree<Body> Tree { get; private set; }

        private List<Body> _bodies = new();
        private BodyQuadTreeBoundsProvider _bodyBounds;

        internal PhysicsWorld()
        {
            _bodyBounds = new BodyQuadTreeBoundsProvider();
            Tree = new QuadTree<Body>(Console.WindowWidth, Console.WindowHeight, _bodyBounds);
            CollisionSets = new List<CollisionSet>();
        }

        internal void AddBody(Body body)
        {
            float newWidth = Tree.Area.Width;
            float newHeight = Tree.Area.Height;

            float right = _bodyBounds.GetRight(body);
            float bottom = _bodyBounds.GetBottom(body);

            if (right > Bounds.Right)
                _bounds.Width = right;

            if (bottom > Bounds.Bottom) 
                _bounds.Height = bottom;

            if (right > Tree.Area.Right)
                newWidth *= 2;

            if (bottom > Tree.Area.Bottom)
                newHeight *= 2;

            if (newWidth > Tree.Area.Width || newHeight > Tree.Area.Height)
            {
                Tree = new QuadTree<Body>(newWidth, newHeight, _bodyBounds);
                foreach (var existingBody in _bodies)
                {
                    Tree.Insert(existingBody);
                }
            }

            _bodies.Add(body);
            Tree.Insert(body);
        }



    }
}
