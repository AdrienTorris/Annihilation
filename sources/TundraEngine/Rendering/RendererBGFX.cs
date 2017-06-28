using System;
using System.Threading.Tasks;

using TundraEngine.Windowing;
using SharpBgfx;

using static SharpBgfx.Bgfx;

namespace TundraEngine.Rendering
{
    public class RendererBGFX : IRenderer
    {
        private int _width;
        private int _height;
        
        public static bool IsInitialized { get; private set; }

        public void Initialize(ref ApplicationInfo applicationInfo, IWindow window)
        {
            // Hook window
            SetWindowHandle(
                window.WindowManagerInfo.Type == WindowManagerType.Windows
                ? window.WindowManagerInfo.Windows.HWindow
                : window.WindowManagerInfo.Type == WindowManagerType.X11
                ? window.WindowManagerInfo.X11.Window
                : window.WindowManagerInfo.Wayland.Display);

            // Initialize
            Init(RendererBackend.Vulkan);

            _width = (int)(applicationInfo.WindowInfo.Width * applicationInfo.RendererInfo.RenderScale);
            _height = (int)(applicationInfo.WindowInfo.Height * applicationInfo.RendererInfo.RenderScale);
            Reset(_width, _height);

            // Set view 0 clear state
            SetViewClear(0, ClearTargets.Color | ClearTargets.Depth, 0x303030ff);

            // Mark initialized so that other BGFX systems don't repeat initialization.
            IsInitialized = true;
        }

        public async Task RenderAsync()
        {
            // Set view 0 viewport
            SetViewRect(0, 0, 0, _width, _height);

            // Make sure view 0 is cleared if no other draw calls are submitted
            Touch(0);
            
            // Advance to the next frame. Rendering thread will be kicked to process submitted rendering primitives.
            Frame();

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TargetFrameStepTime)).ConfigureAwait(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }

                Shutdown();

                disposedValue = true;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        
        ~RendererBGFX()
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