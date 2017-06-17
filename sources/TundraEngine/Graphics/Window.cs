using System;
using SharpVk;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Graphics
{
    public class Window : IDisposable
    {
        private IntPtr _window;
        private Instance _instance;

        public Window (WindowInfo windowInfo, Instance instance)
        {
            // Window
            _window = SDL_CreateWindow (
                windowInfo.Name,
                windowInfo.PositionX,
                windowInfo.PositionY,
                windowInfo.Width,
                windowInfo.Height,
                SDL_WindowFlags.Shown | SDL_WindowFlags.Vulkan);

            // System WM
            SysWMInfo wmInfo = new SysWMInfo ();
            FillVersion (out wmInfo.Version);
            GetWindowWMInfo (_window, ref wmInfo);

            // Surface
            Surface surface;
            switch (wmInfo.SubSystem)
            {
                case SysWMType.Windows:
                    surface = _instance.CreateWin32Surface (new Win32SurfaceCreateInfo
                    {
                        Hwnd = wmInfo.Info.Windows.Window,
                        Hinstance = wmInfo.Info.Windows.HInstance
                    });
                    break;
                case SysWMType.X11:
                    surface = _instance.CreateXcbSurface (new XcbSurfaceCreateInfo
                    {
                        Connection = wmInfo.Info.X11.Display,
                        Window = wmInfo.Info.X11.Window
                    });
                    break;
            }
        }

        ~Window ()
        {
            Dispose (false);
        }

        private void Dispose (bool isDisposing)
        {
            SDL_DestroyWindow (_window);
            _window = IntPtr.Zero;
            _instance.Dispose ();

            GC.SuppressFinalize (this);
        }

        public void Dispose ()
        {
            Dispose (true);
        }
    }
}