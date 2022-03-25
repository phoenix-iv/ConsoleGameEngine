using ConsoleGameEngine.Caching;

namespace ConsoleGameEngine
{
    public class Game : IDisposable
    {
        public CacheManager Cache { get; } = new CacheManager();
        public Scene? Scene { get; set; }
        private bool _exitRequested = false;

        public void Start()
        {
            Console.CursorVisible = false;
            Scene?.Preload();
            Scene?.Create();
            var lastTime = DateTime.Now;
            while(!_exitRequested)
            {
                TimeSpan delta = DateTime.Now.Subtract(lastTime);
                lastTime = DateTime.Now;
                Scene?.Update(delta);
            }
        }

        public void Exit()
        {
            _exitRequested = true;
        }
        

        #region IDisposable
        private bool disposedValue;
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

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
