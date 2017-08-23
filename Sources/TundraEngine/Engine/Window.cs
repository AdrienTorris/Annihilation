using Engine.Rendering;
using Vulkan;
using SDL2;
using static SDL2.SDL;

namespace Engine
{
    public class Window
    {
        public static SDL.Window SDLWindow;
        public static SysWMInfo SysWMInfo;
        
        public Window()
        {
            // Init SDL video subsystem
            InitSubSystem(InitFlags.Video);

            // Create SDL window

            // Create Vulkan instance
            Instance instance = new Instance("Howitzer", "Pillar Engine");
            
            // Create Vulkan surface
#if WINDOW_WIN32
            Win32SurfaceCreateInfo surfaceCreateInfo = new Win32SurfaceCreateInfo();
#endif
        }

        public static void Initialize(ref WindowSettings settings)
        {
            int previousDisplay = -1;

            // Init video subsystem
            InitSubSystem(InitFlags.Video);
            
            // Get desktop display mode
            GetDesktopDisplayMode(settings.Monitor, out SDL.DisplayMode displayMode);

            // Create the window if needed, hidden
            if (SDLWindow == SDL.Window.Null)
            {
                WindowFlags flags = WindowFlags.Hidden;
                if (settings.Mode == WindowMode.BorderlessWindow)
                {
                    flags |= WindowFlags.Borderless;
                }

                SDLWindow = new SDL.Window(settings.Name, SDL.Window.PositionUndefined, SDL.Window.PositionUndefined, settings.Width, settings.Height, flags);

                GetVersion(out SysWMInfo.Version);
                SDLWindow.GetWMInfo(ref SysWMInfo);
            }
            else
            {
                previousDisplay = SDLWindow.GetDisplayIndex();
            }

            // Ensure the window is not fullscreen
            if (SDLWindow.GetFlags().Has(WindowFlags.Fullscreen))
            {
                SDLWindow.SetFullscreen(0);
            }

            // Set window size and display mode
            SDLWindow.SetSize(settings.Width, settings.Height);
            if (previousDisplay >= 0)
            {
                SDLWindow.SetPosition((int)SDL.Window.PositionCenteredDisplay((uint)previousDisplay), (int)SDL.Window.PositionCenteredDisplay((uint)previousDisplay));
            }
            else
            {
                SDLWindow.SetPosition(SDL.Window.PositionCentered, SDL.Window.PositionCentered);
            }
            SDLWindow.SetDefaultDisplayMode();
            SDLWindow.SetBordered(false);
        }

        public static void Shutdown()
        {
            SDLWindow.Destroy();
        }
        
        private static void SetWindowMode(int width, int height, bool fullscreen)
        {

        }
    }
}