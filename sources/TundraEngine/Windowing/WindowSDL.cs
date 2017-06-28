using System;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Windowing
{
    public class WindowSDL : IWindow
    {
        public IntPtr Window { get; set; }
        public WindowManagerInfo WindowManagerInfo { get; set; }
        public int UndefinedPosition => SDL_WindowPositionUndefined;

        public void CreateWindow(ref WindowInfo windowInfo)
        {
            // Window
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

            Window = SDL_CreateWindow(
                windowInfo.Name,
                windowInfo.PositionX,
                windowInfo.PositionY,
                windowInfo.Width,
                windowInfo.Height,
                windowFlags);
            Assert.IsTrue(Window != IntPtr.Zero, "Could not create SDL window.");

            // Window manager
            SysWMInfo wmInfo = new SysWMInfo();
            FillVersion(out wmInfo.Version);
            GetWindowWMInfo(Window, ref wmInfo);
            
            switch (wmInfo.SubSystem)
            {
                case SysWMType.Windows:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.Windows,
                        Windows = new WindowManagerInfo.WindowsInfo
                        {
                            HWindow = wmInfo.Info.Windows.Window,
                            HInstance = wmInfo.Info.Windows.HInstance
                        }
                    };
                    break;
                case SysWMType.X11:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.X11,
                        X11 = new WindowManagerInfo.X11Info
                        {
                            Window = wmInfo.Info.X11.Window,
                            Connection = wmInfo.Info.X11.Display
                        }
                    };
                    break;
                case SysWMType.Wayland:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.Wayland,
                        Wayland = new WindowManagerInfo.WaylandInfo
                        {
                            Surface = wmInfo.Info.Wayland.Surface,
                            Display = wmInfo.Info.Wayland.Display
                        }
                    };
                    break;
                default:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.None
                    };
                    throw new Exception("Could not find a supported window manager.");
            }
        }

        public void DestroyWindow()
        {
            SDL_DestroyWindow(Window);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                SDL_DestroyWindow(Window);

                disposedValue = true;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        
        ~WindowSDL()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}