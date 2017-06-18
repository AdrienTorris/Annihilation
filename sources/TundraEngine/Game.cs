using TundraEngine.Graphics;

using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    public abstract class Game
    {
        private static bool _quitRequested = false;
        
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

        public void Run (GameInfo gameInfo)
        {
            InitVideoAndTimer ();
            
            using (Window window = new Window (gameInfo.Name, gameInfo.WindowInfo))
            using (GraphicsDevice graphicsDevice = new GraphicsDevice(gameInfo.Name, window.WindowManagerInfo))
            {
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

                // Shutdown game
                Shutdown ();
            }

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
    }
}