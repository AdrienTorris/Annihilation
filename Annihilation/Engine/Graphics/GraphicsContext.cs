using System;
using Vulkan;

namespace Engine.Graphics
{
    public class GraphicsContext : IDisposable
    {
        public VkInstance _instance;

        public void Finish()
        {

        }

        private void Dispose(bool disposing)
        {
            if (_instance.Handle == IntPtr.Zero)
                return;

            _instance = IntPtr.Zero;
        }
        
        ~GraphicsContext()
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