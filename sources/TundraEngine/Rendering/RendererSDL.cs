using System;
using System.Threading.Tasks;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Rendering
{
    internal class RendererSDL : LibrarySystem<LibSDL>, IRenderer
    {
        private IntPtr _renderer;

        public RendererSDL(IntPtr window)
        {
            _renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.Accelerated | SDL_RendererFlags.PresentVSync);
            Assert.IsNotNull(_renderer, "Could not create the renderer.");
        }

        public async Task RenderAsync()
        {
            SDL_RenderClear(_renderer);

            SDL_RenderPresent(_renderer);

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TargetFrameStepTime)).ConfigureAwait(false);
        }

        protected override void InitializeLibrary()
        {
            LibraryUtility.InitializeSDL();
        }

        protected override void ShutdownLibrary()
        {
            SDL_Quit();
        }

        protected override void DisposeUnmanaged()
        {
            SDL_DestroyRenderer(_renderer);
        }
    }
}