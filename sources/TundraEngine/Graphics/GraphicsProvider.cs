using System;

using TundraEngine.Windowing;

namespace TundraEngine.Graphics
{
    public abstract class GraphicsProvider : IDisposable
    {
        public GraphicsProvider(ref ApplicationInfo applicationInfo, ref WindowManagerInfo windowManagerInfo) { }

        public abstract void Render(int width, int height);

        protected abstract void DisposeUnmanaged();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }

                DisposeUnmanaged();

                disposedValue = true;
            }
        }
        
        ~GraphicsProvider()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}