using System;

using TundraEngine.Windowing;
using SharpBgfx;

using static SharpBgfx.Bgfx;

namespace TundraEngine.Graphics
{
    public class GraphicsProviderBGFX : GraphicsProvider
    {
        public GraphicsProviderBGFX(ref ApplicationInfo applicationInfo, ref WindowManagerInfo windowManagerInfo)
            : base(ref applicationInfo, ref windowManagerInfo)
        {
            // Hook window
            SetWindowHandle(
                windowManagerInfo.Type == WindowManagerType.Windows
                ? windowManagerInfo.Windows.HWindow
                : windowManagerInfo.Type == WindowManagerType.X11
                ? windowManagerInfo.X11.Window
                : windowManagerInfo.Wayland.Display);

            // Initialize
            Init(RendererBackend.Direct3D12);
            Reset(applicationInfo.WindowInfo.Width, applicationInfo.WindowInfo.Height);

            // Enable debug text
            SetDebugFeatures(DebugFeatures.DisplayText);

            // Set view 0 clear state
            SetViewClear(0, ClearTargets.Color | ClearTargets.Depth, 0x303030ff);
        }

        public override void Render(int width, int height)
        {
            // Set view 0 viewport
            SetViewRect(0, 0, 0, width, height);

            // Make sure view 0 is cleared if no other draw calls are submitted
            Touch(0);

            // Write some debug text
            DebugTextClear();
            DebugTextWrite(0, 1, DebugColor.White, DebugColor.Blue, "SharpBgfx/Samples/00-HelloWorld");
            DebugTextWrite(0, 2, DebugColor.White, DebugColor.Cyan, "Description: Initialization and debug text.");

            // Advance to the next frame. Rendering thread will be kicked to process submitted rendering primitives.
            Frame();
        }

        protected override void DisposeUnmanaged()
        {
            throw new NotImplementedException();
        }
    }
}