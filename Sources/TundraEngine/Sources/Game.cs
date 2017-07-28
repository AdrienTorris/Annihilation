using System;
using System.Threading.Tasks;

using TundraEngine.Input;
using TundraEngine.Windowing;
using TundraEngine.Rendering;
using TundraEngine.IMGUI;

namespace TundraEngine
{
    /// <summary>
    /// The game manages all systems (Input, Graphics, etc.) and worlds.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// When set to true, the engine will exit the main loop and begin shutdown.
        /// </summary>
        private bool _quitRequested = false;

        /// <summary>
        /// The startup settings of the game.
        /// </summary>
        public GameSettings Settings { get; private set; }
        /// <summary>
        /// The command-line arguments that were supplied to the game when it was executed, if any.
        /// </summary>
        public string[] Args { get; private set; }
        
        public IWindowProvider Window { get; private set; }
        public IRenderer Renderer { get; private set; }
        public InputSystem InputSystem { get; private set; }
        public IDebugUI DebugUI { get; private set; }

        public static Game Instance { get; private set; }

        public Game(GameSettings settings, Action initialize, Action<float> update, Action shutdown)
        {
            Instance = this;
            Settings = settings;

            RendererCreateInfo rendererCreateInfo = new RendererCreateInfo
            {
                ApplicationName = settings.Name,
                EnableValidation = true
            };

            // Create all engine systems
            using (Window = new WindowSDL())
            using (Renderer = new RendererVulkan(ref rendererCreateInfo))
            using (var eventProvider = new EventProviderSDL())
            //using (DebugUI = new DebugUIBGFX())
            {
                // Create systems
                InputSystem = new InputSystem(eventProvider);

                // Do application-specific initialization
                initialize?.Invoke();

                // Main loop
                while (!_quitRequested)
                {
                    InputSystem.Update();

                    update?.Invoke(1f / 144);
                    //UpdateAsync(Constants.TargetFrameStepTime * 0.001f).Wait();
                    Renderer.Render();
                }

                // Do application-specific shutdown
                shutdown?.Invoke();
            }
        }
        
        /// <summary>
        /// Quits the application.
        /// </summary>
        public void Quit()
        {
            _quitRequested = true;
        }
    }
}