using ConsoleGameEngine.Animations;
using ConsoleGameEngine.Caching;

namespace ConsoleGameEngine
{
    /// <summary>
    /// The game is the entry point to running the main game loop.
    /// </summary>
    public class Game : IDisposable
    {
        /// <summary>
        /// The global animation manager for the game.
        /// </summary>
        public AnimationManager Animations { get; }
        /// <summary>
        /// The global cache manager for the game.
        /// </summary>
        public CacheManager Cache { get; } = new CacheManager();
        /// <summary>
        /// The scene manager.
        /// </summary>
        public SceneManager Scenes { get; }
        private bool _exitRequested = false;

        /// <summary>
        /// Creates a new instance of the <see cref="Game"/>.
        /// </summary>
        public Game()
        {
            Animations = new AnimationManager(Cache);
            Scenes = new SceneManager();
        }

        /// <summary>
        /// Starts the main game loop.  This is a blocking call and will run until <see cref="Exit"/> is called.
        /// </summary>
        /// <param name="initialScene">The initial scene to load up.</param>
        /// <param name="fixedDelta">The fixed delta time between update steps.  Default 1/60th of a second.</param>
        public void Start(Scene initialScene, TimeSpan fixedDelta = default)
        {
            Console.CursorVisible = false;
            Scenes.SwitchTo(initialScene);
            if (fixedDelta == default)
            {
                fixedDelta = TimeSpan.FromSeconds(1.0 / 60.0);
            }
            var currentTime = DateTime.Now;
            var totalTime = TimeSpan.Zero;
            var accumulator = TimeSpan.Zero;

            while(!_exitRequested)
            {
                var newTime = DateTime.Now;
                TimeSpan frameTime = newTime - currentTime;
                currentTime = newTime;

                accumulator += frameTime;

                while (accumulator >= fixedDelta)
                {
                    if (!Scenes.CurrentScene?.IsShutDown ?? false)
                        Scenes.CurrentScene?.UpdateInternal(new GameTime { Delta = fixedDelta, Total = totalTime });
                    accumulator -= fixedDelta;
                    totalTime += fixedDelta;
                }

                Scenes.CurrentScene?.Render();
            }
        }

        /// <summary>
        /// Sets a signal to request that the game exit.
        /// </summary>
        public void Exit()
        {
            _exitRequested = true;
        }
        

        #region IDisposable
        private bool disposedValue;
        /// <summary>
        /// Disposes the game and its scenes.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Game()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        /// <summary>
        /// Disposes the game resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
