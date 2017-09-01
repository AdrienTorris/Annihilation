using System;
using System.Threading.Tasks;
using Engine.Input;
using Engine.Rendering;
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
            Settings = settings;

            // Load required SDL functions
            SDL.LoadFunctions(SDLModule.Video);
            SDL.LoadFunctions(SDLModule.Events);
            SDL.LoadFunctions(SDLModule.Keyboard);
            SDL.LoadFunctions(SDLModule.Mouse);
            SDL.LoadFunctions(SDLModule.Version);
            SDL.LoadFunctions(SDLModule.SysWm);
            SDL.LoadFunctions(SDLModule.FileSystem);

            // Get the "pref" directory
            PreferencePath = SDL.GetPrefPath(settings.Organization, settings.Title);
            
            // Create window
            Window = new Window(settings.Title);
   
            // Init game
            initFunction?.Invoke();

            // Main loop
            while (_state == GameState.Running)
            {
                InputManager.Update();

                // Update game
                updateFunction?.Invoke(1f / 144);
            }
            
            // Shutdown game
            shutdownFunction?.Invoke();

            // Dispose of everything
            Window.Dispose();
        }
        
        public static void Quit()
        {
            _state = GameState.Quitting;
        }
    }
}