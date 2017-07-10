using System;
using System.Threading.Tasks;

using SharpBgfx;

namespace TundraEngine.Rendering
{
    internal class RendererBGFX : LibrarySystem<LibBGFX>, IRenderer
    {
        public async Task RenderAsync()
        {
            // Set view 0 viewport
            Bgfx.SetViewRect(0, 0, 0, Game.Instance.Settings.RendererSettings.Width, Game.Instance.Settings.RendererSettings.Height);

            // Make sure view 0 is cleared if no other draw calls are submitted
            Bgfx.Touch(0);

            // Advance to the next frame. Rendering thread will be kicked to process submitted rendering primitives.
            Bgfx.Frame();

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TargetFrameStepTime)).ConfigureAwait(false);
        }

        protected override void DisposeUnmanaged()
        {

        }

        protected override void InitializeLibrary()
        {
            LibraryUtility.InitializeBGFX();
        }

        protected override void ShutdownLibrary()
        {
            Bgfx.Shutdown();
        }
    }
}