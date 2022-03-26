using ConsoleGameEngine.Caching;

namespace ConsoleGameEngine
{
    /// <summary>
    /// The game is the entry point to running the main game loop.
    /// </summary>
    public class Game : IDisposable
    {
        /// <summary>
        /// The global cache manager for the game.
        /// </summary>
        public CacheManager Cache { get; } = new CacheManager();
        /// <summary>
        /// The scene to run.
        /// </summary>
        public Scene? Scene { get; set; }
        private bool _exitRequested = false;

        /// <summary>
        /// Starts the main game loop.  This is a blocking call and will run until <see cref="Game.Exit"/> is called.
        /// </summary>
        public void Start()
        {
            Console.CursorVisible = false;
            Scene?.Preload();
            Scene?.Create();
            var startTime = DateTime.Now;
            var lastTime = DateTime.Now;
            while(!_exitRequested)
            {
                TimeSpan delta = DateTime.Now.Subtract(lastTime);
                TimeSpan total = DateTime.Now.Subtract(startTime);
                lastTime = DateTime.Now;
                Scene?.UpdateInternal(new GameTime { Delta = delta, Total = total });
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
