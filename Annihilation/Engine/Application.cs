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

            // Create window
            Window = new Window(settings.Title);
   
            initFunction?.Invoke();

            // Main loop
            while (_state == GameState.Running)
            {
                InputManager.Update();

                updateFunction?.Invoke(1f / 144);
            }
            
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