using System.Threading.Tasks;

using TundraEngine.Input;
using TundraEngine.Windowing;
using TundraEngine.Rendering;
using TundraEngine.IMGUI;

using SharpBgfx;
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
        public static ApplicationSettings Settings;
        /// <summary>
        /// The command-line arguments that were supplied to the game when it was executed, if any.
        /// </summary>
        public static string[] Args { get; private set; }

        // TODO: Find a better name for these (System?)
        public static IWindow Window { get; private set; }
        public static IRenderer Renderer { get; private set; }
        public static IEventProvider EventProvider { get; private set; }
        public static IDebugUI DebugUI { get; private set; }

        public void Run(string[] args)
        {
            Args = args;
            GetApplicationSettings(out Settings);

            // Create all engine systems
            using (Window = new WindowSDL())
            using (Renderer = Settings.RendererSettings.RendererType == RendererType.Vulkan
                ? new RendererVulkan()
                : Settings.RendererSettings.RendererType == RendererType.BGFX
                    ? (IRenderer)new RendererBGFX()
                    : new RendererNull())
            using (EventProvider = new EventProviderSDL())
            using (DebugUI = new DebugUIBGFX())
            {
                // Do application-specific initialization
                Initialize();

                // Main loop
                while (!_quitRequested)
                {
                    EventProvider.PollEvents();

                    UpdateAsync(Constants.TargetFrameStepTime * 0.001f).Wait();
                    Renderer.RenderAsync().Wait();
                }

                // Do application-specific shutdown
                Shutdown();
            }
        }

        /// <summary>
        /// The application must override this method to fill the <see cref="Settings"/> struct from code or from a resource.
        /// </summary>
        protected abstract void GetApplicationSettings(out ApplicationSettings applicationInfo);
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

        /// <summary>
        /// Quits the application.
        /// </summary>
        public static void Quit()
        {
            _quitRequested = true;
        }

        internal static void InitializeSDL()
        {
            {
                bool result = SDL_SetHint(HintFrameBufferAcceleration, "1");
                Assert.IsTrue(result, "Unable to set hint \"" + HintFrameBufferAcceleration + "\"");
            }
            {
                // BUG: Initializing more than video crashes
                int result = SDL_Init(SDL_InitFlags.Video);
                Assert.IsZero(result, "Unable to init SDL");
            }
        }

        internal static void InitializeBGFX()
        {
            WindowManagerInfo windowManagerInfo = Window.WindowManagerInfo;

            // Hook window
            Bgfx.SetWindowHandle(
                windowManagerInfo.Type == WindowManagerType.Windows
                ? windowManagerInfo.Windows.HWindow
                : windowManagerInfo.Type == WindowManagerType.X11
                ? windowManagerInfo.X11.Window
                : windowManagerInfo.Wayland.Display);

            // Initialize
            Bgfx.Init(RendererBackend.Direct3D12);
            Bgfx.Reset(Settings.RendererSettings.ResolutionX, Settings.RendererSettings.ResolutionY);

            // Set view 0 clear state
            Bgfx.SetViewClear(0, ClearTargets.Color | ClearTargets.Depth, 0x303030ff);
        }
    }
}