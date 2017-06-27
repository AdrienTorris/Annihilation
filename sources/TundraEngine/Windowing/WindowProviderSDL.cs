using System;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Windowing
{
    public class WindowProviderSDL : IWindowProvider
    {
        public int UndefinedPosition => SDL_WindowPositionUndefined;

        public IntPtr CreateWindow(ref WindowInfo windowInfo)
        {
            SDL_WindowFlags windowFlags = SDL_WindowFlags.Shown | SDL_WindowFlags.Vulkan;
            if (windowInfo.AllowHighDPI) windowFlags |= SDL_WindowFlags.AllowHighDPI;
            if (windowInfo.AlwaysOnTop) windowFlags |= SDL_WindowFlags.AlwaysOnTop;
            switch (windowInfo.Mode)
            {
                case WindowMode.Fullscreen:
                    windowFlags |= SDL_WindowFlags.Fullscreen;
                    break;
                case WindowMode.FullscreenDesktop:
                    windowFlags |= SDL_WindowFlags.FullscreenDeskTop;
                    break;
            }

            IntPtr window = SDL_CreateWindow(
                windowInfo.Name,
                windowInfo.PositionX,
                windowInfo.PositionY,
                windowInfo.Width,
                windowInfo.Height,
                windowFlags);

            return window;
        }

        public void DestroyWindow(IntPtr window)
        {
            SDL_DestroyWindow(window);
        }

        public void GetWindowManagerInfo(IntPtr window, out WindowManagerInfo windowManagerInfo)
        {
            SysWMInfo wmInfo = new SysWMInfo();
            FillVersion(out wmInfo.Version);
            GetWindowWMInfo(window, ref wmInfo);

            windowManagerInfo = new WindowManagerInfo();

            switch (wmInfo.SubSystem)
            {
                case SysWMType.Windows:
                    windowManagerInfo.Type = WindowManagerType.Windows;
                    windowManagerInfo.Windows.HWindow = wmInfo.Info.Windows.Window;
                    windowManagerInfo.Windows.HInstance = wmInfo.Info.Windows.HInstance;
                    break;
                case SysWMType.X11:
                    windowManagerInfo.Type = WindowManagerType.X11;
                    windowManagerInfo.X11.Window = wmInfo.Info.X11.Window;
                    windowManagerInfo.X11.Connection = wmInfo.Info.X11.Display;
                    break;
                case SysWMType.Wayland:
                    windowManagerInfo.Type = WindowManagerType.Wayland;
                    windowManagerInfo.Wayland.Display = wmInfo.Info.Wayland.Display;
                    windowManagerInfo.Wayland.Surface = wmInfo.Info.Wayland.Surface;
                    break;
            }

            windowManagerInfo.Type = WindowManagerType.None;
        }
    }
}