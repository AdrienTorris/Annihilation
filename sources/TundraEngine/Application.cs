using TundraEngine.Graphics;

using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    /// <summary>
    /// The application manages all systems (Input, Graphics, etc.) and worlds.
    /// </summary>
    public abstract class Application
    {
        /// <summary>
        /// When set to true, the engine will exit the main loop and begin shutdown.
        /// </summary>
        private static bool _quitRequested = false;

        /// <summary>
        /// The startup settings of the game.
        /// </summary>
        public static GameInfo GameInfo;
        /// <summary>
        /// The command-line arguments that were supplied to the game when it was executed, if any.
        /// </summary>
        public static string[] Args { get; private set; }
        
        /// <summary>
        /// The application must override this method to fill the <see cref="GameInfo"/> struct from code or from a resource.
        /// </summary>
        protected abstract void GetGameInfo(out GameInfo gameInfo);
        /// <summary>
        /// This is called after all engine subsystems have been initialized and before the main loop.
        /// </summary>
        protected abstract void Initialize();
        /// <summary>
        /// This is called in the main loop, after processing input and before rendering.
        /// </summary>
        protected abstract void Update(double deltaTime);
        /// <summary>
        /// This is called when exiting the main loop, before shutting down engine subsystems.
        /// </summary>
        protected abstract void Shutdown();

        public void Run(string[] args)
        {
            Args = args;
            GetGameInfo(out GameInfo);

            // Init SDL
            {
                bool result = SDL_SetHint(HintFrameBufferAcceleration, "1");
                Assert.IsTrue(result, "Unable to set hint \"" + HintFrameBufferAcceleration + "\"");
            }
            {
                int result = SDL_Init(SDL_InitFlags.Video | SDL_InitFlags.Timer);
                Assert.IsZero(result, "Unable to init SDL");
            }

            // Create window
            using (Window window = new Window(GameInfo.Name.ToString(), GameInfo.WindowInfo))
            // Create graphics device
            using (GraphicsDevice graphicsDevice = new GraphicsDevice(GameInfo.Name.ToString(), GameInfo.GraphicsInfo, window.WindowManagerInfo))
            {
                // Create timer
                Timer timer = new Timer();

                // Do game-specific initialization
                Initialize();

                while (!_quitRequested)
                {
                    // Update timer
                    timer.Update();

                    // Do game-specific update
                    Update(timer.DeltaTime);
                }

                // Do game-specific shutdown
                Shutdown();
            }

            // Shutdown SDL
            SDL_Quit();
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