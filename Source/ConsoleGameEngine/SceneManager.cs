namespace ConsoleGameEngine
{
    /// <summary>
    /// Represents an object than manages scenes.
    /// </summary>
    public class SceneManager
    {
        /// <summary>
        /// The current scene.
        /// </summary>
        public Scene? CurrentScene { get; set; }
        private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
        
        /// <summary>
        /// Creates a new instance of <see cref="SceneManager"/>.
        /// </summary>
        public SceneManager()
        {

        }

        /// <summary>
        /// Adds the specified scene to the scene manager.
        /// </summary>
        /// <param name="key">The key used to identify the scene.</param>
        /// <param name="scene">The scene to add.</param>
        public void Add(string key, Scene scene)
        {
            _scenes.Add(key, scene);
        }

        /// <summary>
        /// Switches to the the specified scene.
        /// </summary>
        /// <param name="sceneKey">The key of the scene to switch to.</param>
        /// <param name="shutdown">Whether or not to shut down the current scene.</param>
        public void SwitchTo(string sceneKey, bool shutdown = false)
        {
            SwitchTo(_scenes[sceneKey], shutdown);
        }

        /// <summary>
        /// Switches to the the specified scene.
        /// </summary>
        /// <param name="scene">The scene to switch to.</param>
        /// <param name="shutdown">Whether or not to shut down the current scene.</param>
        public void SwitchTo(Scene scene, bool shutdown = false)
        {
            if (shutdown)
                CurrentScene?.StartShutdown();

            LoadScene(scene);
            CurrentScene = scene;
        }

        private static void LoadScene(Scene scene)
        {
            scene.Preload();
            scene.Create();
        }
    }
}
