using System;

namespace TundraEngine
{
    internal interface ILibrary { }
    internal class LibSDL : ILibrary { }
    internal class LibBGFX : ILibrary { }

    /// <summary>
    /// This is used as a base class for systems that use one or multiple parts of a library but share common initialization and shutdown methods, such as SDL and BGFX.
    /// </summary>
    internal abstract class LibrarySystem<T> : IDisposable
        where T : ILibrary
    {
        private bool _wasDisposed = false;

        private static int _systemCount = 0;

        public LibrarySystem()
        {
            if (_systemCount == 0)
            {
                InitializeLibrary();
            }
            ++_systemCount;
        }

        protected abstract void InitializeLibrary();
        protected abstract void ShutdownLibrary();
        protected abstract void DisposeUnmanaged();

        private void Dispose(bool disposing)
        {
            if (!_wasDisposed)
            {
                if (disposing)
                {
                    --_systemCount;
                    Assert.IsTrue(_systemCount >= 0, "Disposed more than created.");
                }

                DisposeUnmanaged();

                if (_systemCount == 0)
                {
                    ShutdownLibrary();
                }

                _wasDisposed = true;
            }
        }

        ~LibrarySystem()
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
    }
}