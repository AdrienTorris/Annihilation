using System.Runtime.CompilerServices;

namespace Engine.SDL
{
    public static class PixelFormatExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this uint pixelFormat, string message)
        {
            Assert.IsTrue(pixelFormat != SDL.PixelFormatUnknown, "[SDL] " + message + ": " + SDL.GetError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint CheckErrorAndReturn(this uint pixelFormat, string message)
        {
            Assert.IsTrue(pixelFormat != SDL.PixelFormatUnknown, "[SDL] " + message + ": " + SDL.GetError());
            return pixelFormat;
        }
    }
}