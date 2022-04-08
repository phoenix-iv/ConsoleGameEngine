namespace ConsoleGameEngine.Components
{
    /// <summary>
    /// Contains data about an entity's size.
    /// </summary>
    public struct EntitySize
    {
        /// <summary>
        /// Half of <see cref="Height"/>.
        /// </summary>
        public float HalfHeight { get; private set; }
        /// <summary>
        /// Half of <see cref="Width"/>.
        /// </summary>
        public float HalfWidth { get; private set; }

        private float _height;
        /// <summary>
        /// The height of the entity.
        /// </summary>
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                HalfHeight = value / 2;
            }
        }

        private float _width;
        /// <summary>
        /// The width of the entity.
        /// </summary>
        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                HalfWidth = value / 2;
            }
        }
    }
}
