using System;
using Annihilation.Vk;

namespace Annihilation.Graphics
{
    public class GraphicsContext : IDisposable
    {
        public Instance _instance;

        public void Finish()
        {

        }

        private void Dispose(bool disposing)
        {
            if (_instance.IsNull) return;

            if (disposing)
            {
                _instance.Destroy();
            }
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