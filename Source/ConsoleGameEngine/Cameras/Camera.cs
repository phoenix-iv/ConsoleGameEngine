using ConsoleGameEngine.Components;

namespace ConsoleGameEngine.Cameras
{
    /// <summary>
    /// Represents a camera used to transform the view.
    /// </summary>
    public class Camera
    {
        private Position _position;
        /// <summary>
        /// The camera position.
        /// </summary>
        public ref Position Position { get => ref _position; }
    }
}
