using System;
using SharpVk;

namespace SDL
{
    public static partial class SDL
    {
        public static bool SDL_CreateVulkanSurface (IntPtr window, Instance instance, out Surface surface)
        {
            if (window == null)
            {
                SDL_SetError ("window is null");
                surface = null;
                return false;
            }
            if (instance == null)
            {
                SDL_SetError ("instance is null");
                surface = null;
                return false;
            }

            SysWMInfo wmInfo = new SysWMInfo ();
            FillVersion (out wmInfo.Version);
            if (!GetWindowWMInfo (window, ref wmInfo))
            {
                SDL_SetError ("Invalid SDL version number");
                surface = null;
                return false;
            }

            switch (wmInfo.SubSystem)
            {
                case SysWMType.Windows:
                    surface = instance.CreateWin32Surface (new Win32SurfaceCreateInfo
                    {
                        Hwnd = wmInfo.Info.Windows.Window,
                        Hinstance = wmInfo.Info.Windows.HInstance
                    });
                    break;
                case SysWMType.X11:
                    surface = instance.CreateXcbSurface (new XcbSurfaceCreateInfo
                    {
                        Connection = wmInfo.Info.X11.Display,
                        Window = wmInfo.Info.X11.Window
                    });
                    break;
                default:
                    surface = null;
                    SDL_SetError ("Unsupported subsystem: " + wmInfo.SubSystem);
                    return false;
            }
            return true;
        }
    }
}