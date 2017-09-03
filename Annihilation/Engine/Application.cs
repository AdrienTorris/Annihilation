using System;
using System.Threading.Tasks;

using Engine.Input;
using Engine.Config;

using SDL2;

namespace Engine
{
    public enum GameState : byte
    {
        Running,
        Quitting,
    }

    public class Application : IDisposable
    {
        // TODO: Best place/best name for these?
        public const string ConfigResolutionX = "ResolutionX";
        public const string ConfigResolutionY = "ResolutionY";

        /// <summary>
        /// The platform-specific path where you can write files. Perfect for save games.
        /// </summary>
        /// <remarks>
        /// Must be called after <see cref="Start(Action, Action{double}, Action)"/>.
        /// </remarks>
        public static string PreferencePath { get; private set; }

        public ApplicationSettings ApplicationSettings { get; private set; }
        public GraphicsSettings GraphicsSettings { get; private set; }
        public InputSettings InputSettings { get; private set; }
        public Window Window { get; private set; }

        private GameState _state = GameState.Running;
        private bool _isDisposed = false;
        
        private Application() { }

        public Application(ref ApplicationSettings applicationSettings,
                           ref GraphicsSettings graphicsSettings,
                           ref InputSettings inputSettings)
        {
            applicationSettings.CheckError();

            ApplicationSettings = applicationSettings;
            GraphicsSettings = graphicsSettings;
            InputSettings = inputSettings;
        }

        public void Start(Action initFunction, Action<double> updateFunction, Action shutdownFunction)
        {
            Log.Info("Loading SDL functions.");
            
            // Load required SDL functions if not already done
            SDL.LoadFunctions(SDLModule.SDL);
            SDL.LoadFunctions(SDLModule.Video);
            SDL.LoadFunctions(SDLModule.Events);
            SDL.LoadFunctions(SDLModule.Keyboard);
            SDL.LoadFunctions(SDLModule.Mouse);
            SDL.LoadFunctions(SDLModule.Version);
            SDL.LoadFunctions(SDLModule.SysWm);
            SDL.LoadFunctions(SDLModule.FileSystem);
            Log.Info("SDL functions loaded.");

            // Get the "pref" directory if not already done
            PreferencePath = PreferencePath ?? SDL.GetPrefPath(ApplicationSettings.Organization, ApplicationSettings.Title).ToString(128);
            Log.Info("Preference path found: " + PreferencePath);
            
            // Create window
            Window = new Window(this);
            Log.Info("Window created.");
   
            // Init game
            initFunction?.Invoke();
            Log.Info("Init function called.");

            // Main loop
            while (_state == GameState.Running)
            {
                InputManager.Update();

                // Update game
                updateFunction?.Invoke(1f / 144);
            }
            
            // Shutdown game
            shutdownFunction?.Invoke();
            Log.Info("Shutdown function called.");
        }
        
        public void Quit()
        {
            _state = GameState.Quitting;
        }
        
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing) { }

                Window.Dispose();

                _isDisposed = true;
            }
        }
        
        ~Application()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}