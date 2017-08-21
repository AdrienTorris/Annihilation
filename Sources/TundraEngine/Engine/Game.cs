using System;
using System.Threading.Tasks;

using Engine.Input;
using Engine.Rendering;

namespace Engine
{
    public class Game
    {
        private bool _quitRequested = false;
        
        public string[] Args { get; private set; }

        public static GameSettings Settings;
        public static WindowType WindowType;

        public IWindow Window { get; private set; }
        public IRenderer Renderer { get; private set; }
        public InputSystem InputSystem { get; private set; }
        
        public Game(GameSettings settings, Action initialize, Action<double> update, Action shutdown)
        {
            Instance = this;
            Settings = settings;

            RendererCreateInfo rendererCreateInfo = new RendererCreateInfo
            {
                ApplicationName = settings.Name,
                EnableValidation = true
            };

            // Create all engine systems
            using (Window = new Window())
            using (Renderer = new RendererVulkan(ref rendererCreateInfo))
            using (var eventProvider = new EventProviderSDL())
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