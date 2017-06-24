using System;

using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    public class Window : IDisposable
    {
        private IntPtr _windowHandle;

        public SysWMInfo WindowManagerInfo { get; private set; }
        
        public Window (string name, WindowInfo windowInfo)
        {
            // SDL Window
            SDL_WindowFlags windowFlags = SDL_WindowFlags.Shown | SDL_WindowFlags.Vulkan;
            if (windowInfo.AllowHighDPI) windowFlags |= SDL_WindowFlags.AllowHighDPI;
            if (windowInfo.AlwaysOnTop) windowFlags |= SDL_WindowFlags.AlwaysOnTop;
            switch (windowInfo.Mode)
            {
                case WindowMode.Fullscreen:
                    windowFlags |= SDL_WindowFlags.Fullscreen;
                    break;
                case WindowMode.FullscreenBorderless:
                    windowFlags |= SDL_WindowFlags.FullscreenDeskTop;
                    break;
            }

            _windowHandle = SDL_CreateWindow (
                name,
                windowInfo.PositionX,
                windowInfo.PositionY,
                windowInfo.Width,
                windowInfo.Height,
                windowFlags);

            // Window Manager
            SysWMInfo wmInfo = new SysWMInfo ();
            FillVersion (out wmInfo.Version);
            GetWindowWMInfo (_windowHandle, ref wmInfo);
            WindowManagerInfo = wmInfo;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose (bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _windowHandle = IntPtr.Zero;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~Window ()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose (false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose ()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose (true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}