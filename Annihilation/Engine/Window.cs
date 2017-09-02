using System;
using SDL2;

namespace Engine
{
    public class Window : IDisposable
    {
        private static bool _videoSubsystemInitialized = false;

        public bool HasFocus = false;

        public IntPtr Handle { get; private set; }
#if PLATFORM_LINUX
        public IntPtr Display { get; private set; }
#endif
        public SDL.Window SdlHandle { get; private set; }

        private bool _isDisposing;
        
        public Window(string title, int width, int height)
        {
            if (_videoSubsystemInitialized == false)
            {
                SDL.InitSubSystem(SDL.InitFlags.Video).CheckError();
                _videoSubsystemInitialized = true;
            }

            SdlHandle = SDL.CreateWindow(
                Game.Settings.Title,
                SDL.WindowPositionUndefined,
                SDL.WindowPositionUndefined,
                width, 
                height,
                SDL.WindowFlags.Shown | SDL.WindowFlags.Vulkan
            );
            SdlHandle.Handle.CheckError();
            
            SDL.SysWMInfo sysWMInfo = default(SDL.SysWMInfo);
            SDL.GetVersion(out sysWMInfo.Version);
            if (SDL.GetWindowWMInfo(SdlHandle, ref sysWMInfo) == false)
            {
                throw new Exception(SDL.GetError());
            }

#if PLATFORM_WINDOWS
            Handle = sysWMInfo.Info.Windows.Window;
#elif PLATFORM_LINUX
            switch(sysWMInfo.SubSystem)
            {
                case SDL.SysWMType.X11:
                    Handle = sysWMInfo.Info.X11.Window;
                    Display = sysWMInfo.Info.X11.Display;
                    break;
                case SDL.SysWMType.Wayland:
                    Handle = sysWMInfo.Info.Wayland.Surface;
                    Display = sysWMInfo.Info.Wayland.Display;
                    break;
                case SDL.SysWMType.Mir:
                    Handle = sysWMInfo.Info.Mir.Connection;
                    Display = sysWMInfo.Info.Mir.Surface;
                    break;
            }
#elif PLATFORM_MACOS
            Handle = sysWMInfo.Info.Cocoa.Window;
#endif
        }

        private void Dispose(bool isDisposing)
        {
            if (SdlHandle == IntPtr.Zero)
            {
                return;
            }

            if (_isDisposing)
            {

            }

            SDL.DestroyWindow(SdlHandle);
            SdlHandle = IntPtr.Zero;
            Handle = IntPtr.Zero;
#if PLATFORM_LINUX
            Display = IntPtr.Zero;
#endif
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}