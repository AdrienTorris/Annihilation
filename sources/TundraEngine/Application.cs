﻿using System.Threading.Tasks;

using TundraEngine.Input;
using TundraEngine.Windowing;
using TundraEngine.Rendering;
using TundraEngine.IMGUI;

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
        public static ApplicationInfo ApplicationInfo;
        /// <summary>
        /// The command-line arguments that were supplied to the game when it was executed, if any.
        /// </summary>
        public static string[] Args { get; private set; }
        
        /// <summary>
        /// The application must override this method to fill the <see cref="ApplicationInfo"/> struct from code or from a resource.
        /// </summary>
        protected abstract void GetApplicationInfo(out ApplicationInfo applicationInfo);
        /// <summary>
        /// This is called after all engine subsystems have been initialized and before the main loop.
        /// </summary>
        protected abstract void Initialize();
        /// <summary>
        /// This is called in the main loop, after processing input and before rendering.
        /// </summary>
        protected abstract Task UpdateAsync(double deltaTime);
        /// <summary>
        /// This is called when exiting the main loop, before shutting down engine subsystems.
        /// </summary>
        protected abstract void Shutdown();
        
        public void Run(string[] args)
        {
            Args = args;
            GetApplicationInfo(out ApplicationInfo);
            
            // Init SDL
            // TODO: Do this in the first SDL-related system to init.
            {
                bool result = SDL_SetHint(HintFrameBufferAcceleration, "1");
                Assert.IsTrue(result, "Unable to set hint \"" + HintFrameBufferAcceleration + "\"");
            }
            {
                int result = SDL_Init(SDL_InitFlags.Video);
                Assert.IsZero(result, "Unable to init SDL");
            }
            
            using (var window = new WindowSDL())
            using (var renderer = new RendererBGFX())
            using (var eventProvider = new EventProviderSDL())
            using (var debugUI = new DebugUIBGFX())
            {
                window.CreateWindow(ref ApplicationInfo.WindowInfo);
                renderer.Initialize(ref ApplicationInfo, window);
                debugUI.Initialize(ref ApplicationInfo, window);
                
                // Do application-specific initialization
                Initialize();

                // Main loop
                while (!_quitRequested)
                {
                    eventProvider.PumpEvents(out InputEvent inputEvent);

                    UpdateAsync(Constants.TargetFrameStepTime * 0.001f).Wait();
                    renderer.RenderAsync().Wait();
                }

                // Do application-specific shutdown
                Shutdown();
            }
            
            // Shutdown SDL
            // TODO: Do this in the last SDL-related system to dispose.
            SDL_Quit();
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public static void Quit ()
        {
            _quitRequested = true;
        }
    }
}