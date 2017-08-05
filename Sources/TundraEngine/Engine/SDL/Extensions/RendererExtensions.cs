using System.Runtime.CompilerServices;

namespace Engine.SDL
{
    public static class RendererExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this Renderer renderer, string message)
        {
            Assert.IsTrue(renderer != Renderer.Null, "[SDL] " + message + ": " + SDL.GetError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Renderer CheckErrorAndReturn(this Renderer renderer, string message)
        {
            Assert.IsTrue(renderer != Renderer.Null, "[SDL] " + message + ": " + SDL.GetError());
            return renderer;
        }
    }
}