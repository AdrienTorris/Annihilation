using System;
using System.Threading.Tasks;
using Engine.Input;
using SDL2;

namespace Engine
{
    public enum GameState : byte
    {
        Running,
        Quitting,
    }

    public static class Game
    {
        public static Settings Settings;
        public static string PreferencePath { get; private set; }

        public static Window Window { get; private set; }

        private static GameState _state;
        
        public static void Start(Settings settings, Action initFunction, Action<double> updateFunction, Action shutdownFunction)
        {
            Log.Warning("This is a warning.");
            Log.Error("This is an error.");
            Log.Performance("This is a performance warning");

            Log.Info("Starting game.");

            settings.CheckError();
            Settings = settings;

            // Load required SDL functions
            SDL.LoadFunctions(SDLModule.SDL);
            SDL.LoadFunctions(SDLModule.Video);
            SDL.LoadFunctions(SDLModule.Events);
            SDL.LoadFunctions(SDLModule.Keyboard);
            SDL.LoadFunctions(SDLModule.Mouse);
            SDL.LoadFunctions(SDLModule.Version);
            SDL.LoadFunctions(SDLModule.SysWm);
            SDL.LoadFunctions(SDLModule.FileSystem);
            Log.Info("SDL functions loaded.");

            // Get the "pref" directory
            PreferencePath = SDL.GetPrefPath(settings.Organization, settings.Title).ToString(128);
            Log.Info("Preference path found: " + PreferencePath);
            
            // Create window
            // TODO: Find a clean way to get saved/default video options here
            Window = new Window(settings.Title, 1768, 992);
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

            // Dispose of everything
            Window.Dispose();
            Log.Info("All objects are disposed.");
        }
        
        public static void Quit()
        {
            _state = GameState.Quitting;
        }
    }
}