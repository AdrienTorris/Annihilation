using System;

using static SDL.SDL;

namespace TundraEngine
{
    public static class Game
    {
        private static bool _quitRequested = false;

        public static Action OnGameQuit;

        static void Main (string[] args)
        {
            // Initialize
            SDL_SetHint (HintFrameBufferAcceleration, "1");
            SDL_Init (SDL_InitFlags.Video);
            Window window = new Window ();
            
            SDL_Event sdlEvent;
            while (!_quitRequested)
            {
                // Events
                while (SDL_PollEvent (out sdlEvent) == 1)
                {
                    if (sdlEvent.Type == SDL_EventType.Quit ||
                        sdlEvent.Type == SDL_EventType.KeyDown)
                    {
                        Quit ();
                    }
                }

                // Simulation
                

                // Rendering

            }
            
            OnGameQuit?.Invoke ();

            // Shutdown
            window.Dispose ();
            SDL_Quit ();
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public static void Quit ()
        {
            _quitRequested = true;
        }
    }
}