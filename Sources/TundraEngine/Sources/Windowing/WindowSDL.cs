using System;

using TundraEngine.SDL;
using static TundraEngine.SDL.SDL;

namespace TundraEngine.Windowing
{
    internal class WindowSDL : IWindow
    {
        public WindowManagerInfo WindowManagerInfo { get; set; }
        
        internal Window Window;
        internal SysWMInfo SysWMInfo;
        
        public WindowSDL(ref WindowSettings settings)
        {
            int previousDisplay = -1;

            // Init video subsystem
            InitSubSystem(InitFlags.Video);
            
            // Get desktop display mode
            GetDesktopDisplayMode(settings.Monitor, out DisplayMode displayMode);

            // Create the window if needed, hidden
            if (Window.IsNull)
            {
                WindowFlags flags = WindowFlags.Hidden;
                if (settings.Mode == WindowMode.BorderlessWindow)
                {
                    flags |= WindowFlags.Borderless;
                }

                Window = new Window(settings.Name, Window.PositionUndefined, Window.PositionUndefined, settings.Width, settings.Height, flags);

                GetVersion(out SysWMInfo.Version);
                Window.GetWMInfo(ref SysWMInfo);
            }
            else
            {
                previousDisplay = Window.DisplayIndex;
            }

            // Ensure the window is not fullscreen
            if (Window.Flags.Has(WindowFlags.Fullscreen))
            {
                Window.SetFullscreen(0);
            }

            // Set window size and display mode
            Window.SetSize(settings.Width, settings.Height);
            if (previousDisplay >= 0)
            {
                Window.SetPosition((int)Window.PositionCenteredDisplay((uint)previousDisplay), (int)Window.PositionCenteredDisplay((uint)previousDisplay));
            }
            else
            {
                Window.SetPosition(Window.PositionCentered, Window.PositionCentered);
            }
            Window.SetDefaultDisplayMode();
            Window.SetBordered(false);
                
            // Window manager
            switch (SysWMInfo.SubSystem)
            {
                case SysWMType.Windows:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.Windows,
                        Windows = new WindowManagerInfo.WindowsInfo
                        {
                            HWindow = SysWMInfo.Info.Windows.Window,
                            HInstance = SysWMInfo.Info.Windows.HInstance
                        }
                    };
                    break;
                case SysWMType.X11:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.X11,
                        X11 = new WindowManagerInfo.X11Info
                        {
                            Window = SysWMInfo.Info.X11.Window,
                            Connection = SysWMInfo.Info.X11.Display
                        }
                    };
                    break;
                case SysWMType.Wayland:
                    WindowManagerInfo = new WindowManagerInfo
                    {
                        Type = WindowManagerType.Wayland,
                        Wayland = new WindowManagerInfo.WaylandInfo
                        {
                            Surface = SysWMInfo.Info.Wayland.Surface,
                            Display = SysWMInfo.Info.Wayland.Display
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

        private void SetWindowMode(int width, int height, bool fullscreen)
        {

        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void DisposeUnmanaged()
        {
            if (!disposedValue)
            {
                Window.Destroy();

                disposedValue = true;
            }
        }
        
        ~WindowSDL()
        {
            DisposeUnmanaged();
        }
        
        public void Dispose()
        {
            DisposeUnmanaged();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}