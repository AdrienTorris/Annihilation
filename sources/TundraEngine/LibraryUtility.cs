using TundraEngine.Windowing;

using SharpBgfx;
using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    public static class LibraryUtility
    {
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
            WindowManagerInfo windowManagerInfo = Game.Instance.Window.WindowManagerInfo;

            // Hook window
            Bgfx.SetWindowHandle(
                windowManagerInfo.Type == WindowManagerType.Windows
                ? windowManagerInfo.Windows.HWindow
                : windowManagerInfo.Type == WindowManagerType.X11
                ? windowManagerInfo.X11.Window
                : windowManagerInfo.Wayland.Display);

            // Initialize
            Bgfx.Init(RendererBackend.Direct3D12);
            Bgfx.Reset(Game.Instance.Settings.RendererSettings.Width, Game.Instance.Settings.RendererSettings.Height);

            // Set view 0 clear state
            Bgfx.SetViewClear(0, ClearTargets.Color | ClearTargets.Depth, 0x303030ff);
        }
    }
}