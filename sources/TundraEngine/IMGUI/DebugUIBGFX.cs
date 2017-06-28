using System.Runtime.CompilerServices;

using TundraEngine.Mathematics;
using TundraEngine.Windowing;
using TundraEngine.Rendering;
using SharpBgfx;

using static SharpBgfx.Bgfx;

namespace TundraEngine.IMGUI
{
    public class DebugUIBGFX : IDebugUI
    {
        public static bool IsInitialized { get; private set; }

        public void Initialize(ref ApplicationInfo applicationInfo, IWindow window)
        {
            // Initialize BGFX if it wasn't done in the renderer.
            if (!RendererBGFX.IsInitialized)
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

                int width = (int)(applicationInfo.WindowInfo.Width * applicationInfo.RendererInfo.RenderScale);
                int height = (int)(applicationInfo.WindowInfo.Height * applicationInfo.RendererInfo.RenderScale);
                Reset(width, height);

                // Set view 0 clear state
                SetViewClear(0, ClearTargets.Color | ClearTargets.Depth, 0x303030ff);
            }

            // Enable debug text
            SetDebugFeatures(DebugFeatures.DisplayText);

            IsInitialized = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            DebugTextClear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Text(int x, int y, string text)
        {
            DebugTextWrite(x, y, DebugColor.White, DebugColor.Transparent, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Text(int x, int y, Color foreColor, string text)
        {
            // TODO: Implement color.ToBGFX()
            DebugTextWrite(x, y, DebugColor.White, DebugColor.Transparent, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Text(int x, int y, Color foreColor, Color backColor, string text)
        {
            // TODO: Implement color.ToBGFX()
            DebugTextWrite(x, y, DebugColor.White, DebugColor.Transparent, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Image(int x, int y, int width, int height, byte[] pixels, int stride)
        {
            DebugTextImage(x, y, width, height, pixels, stride);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // The debug UI system is always disposed before the renderer, so check if it's safe to shutdown BGFX here.
                if (!RendererBGFX.IsInitialized)
                {
                    Shutdown();
                }

                disposedValue = true;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls


        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~DebugUIBGFX() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}