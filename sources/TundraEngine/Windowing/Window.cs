using System;

namespace TundraEngine.Windowing
{
    public class Window : IDisposable
    {
        private IWindowProvider _windowProvider;
        private IntPtr _windowHandle;

        public WindowManagerInfo WindowManagerInfo;
        
        public Window (ref WindowInfo windowInfo, IWindowProvider windowProvider)
        {
            _windowProvider = windowProvider;
            _windowHandle = windowProvider.CreateWindow(ref windowInfo);
            windowProvider.GetWindowManagerInfo(_windowHandle, out WindowManagerInfo);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }

                _windowProvider.DestroyWindow(_windowHandle);
                _windowHandle = IntPtr.Zero;

                disposedValue = true;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
       
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