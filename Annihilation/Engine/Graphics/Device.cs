using System;
using Vulkan;

namespace Engine.Rendering
{
    public class Device : IDisposable
    {
        private bool _isDisposed = false;
        
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }
                
                _isDisposed = true;
            }
        }
        
        ~Device()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
