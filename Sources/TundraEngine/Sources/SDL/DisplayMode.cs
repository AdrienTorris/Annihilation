using System;

namespace TundraEngine.SDL
{
    /// <summary> The structure that defines a display mode. </summary>
    public unsafe struct SDL_DisplayMode
    {
        /// <summary> Pixel format </summary>
        public PixelFormat Format;
        public int Width;
        public int Height;
        public int RefreshRate;
        public void* DriverData;
    }
}