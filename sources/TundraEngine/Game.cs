using System;

using static SDL.SDL;

namespace TundraEngine
{
    public abstract class Game : IDisposable
    {
        private static bool _quitRequested = false;
        private bool _disposedValue = false; // To detect redundant calls

        public static double DeltaTime { get; private set; }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public static void Quit ()
        {
            _quitRequested = true;
        }

        protected abstract void Initialize ();
        protected abstract void Simulate (double deltaTime);
        protected abstract void Shutdown ();

        public void Run ()
        {
            // Initialize window
            SDL_SetHint (HintFrameBufferAcceleration, "1");
            SDL_Init (SDL_InitFlags.Video);
            Window window = new Window ();

            // Initialize game
            Initialize ();

            ulong currentTime = SDL_GetPerformanceCounter ();
            ulong lastTime = 0;
            while (!_quitRequested)
            {
                // Calculate delta time
                lastTime = currentTime;
                currentTime = SDL_GetPerformanceCounter ();
                DeltaTime = (currentTime - lastTime) * 1000 / SDL_GetPerformanceFrequency ();

                // Process input
                ProcessInput ();

                // Simulate game
                Simulate (DeltaTime);

                // Render
                Render ();
            }

            // Shutdown
            Shutdown ();
            window.Dispose ();
            SDL_Quit ();
        }

        internal void ProcessInput ()
        {
            SDL_Event sdlEvent;
            while (SDL_PollEvent (out sdlEvent) == 1)
            {
                // TODO: Here we should fill the high-level input manager
                if (sdlEvent.Type == SDL_EventType.Quit ||
                    sdlEvent.Type == SDL_EventType.KeyDown)
                {
                    Quit ();
                }
            }
        }

        internal void Render ()
        {

        }
        
        protected virtual void Dispose (bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposedValue = true;
            }
        }
        
        ~Game ()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose (false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose ()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose (true);
            GC.SuppressFinalize(this);
        }
    }
}