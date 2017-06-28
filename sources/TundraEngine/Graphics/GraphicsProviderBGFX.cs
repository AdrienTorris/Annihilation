using System;
using System.Threading.Tasks;

using TundraEngine.Windowing;
using SharpBgfx;

using static SharpBgfx.Bgfx;

namespace TundraEngine.Graphics
{
    public class GraphicsProviderBGFX : GraphicsProvider
    {
        private int _width;
        private int _height;
        
        public GraphicsProviderBGFX(ref ApplicationInfo applicationInfo, ref WindowManagerInfo windowManagerInfo)
            : base(ref applicationInfo, ref windowManagerInfo)
        {
            _width = (int)(applicationInfo.WindowInfo.Width * applicationInfo.GraphicsInfo.RenderScale);
            _height = (int)(applicationInfo.WindowInfo.Height * applicationInfo.GraphicsInfo.RenderScale);
            
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

        public async Task RenderAsync()
        {
            // Set view 0 viewport
            SetViewRect(0, 0, 0, _width, _height);

            // Make sure view 0 is cleared if no other draw calls are submitted
            Touch(0);

            // Write some debug text
            DebugTextClear();
            DebugTextWrite(0, 1, DebugColor.White, DebugColor.Blue, "SharpBgfx/Samples/00-HelloWorld");
            DebugTextWrite(0, 2, DebugColor.White, DebugColor.Cyan, "Description: Initialization and debug text.");

            // Advance to the next frame. Rendering thread will be kicked to process submitted rendering primitives.
            Frame();

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TargetFrameStepTime)).ConfigureAwait(false);
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
        }
    }
}