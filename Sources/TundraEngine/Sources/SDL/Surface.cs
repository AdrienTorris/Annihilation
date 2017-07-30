using System;

namespace TundraEngine.SDL
{
    /// <summary>
    /// A collection of pixels used in software blitting.
    /// <para/> This structure should be treated as read-only, except for <see cref="Pixels"/>, which, if not null, contains the raw pixel data for the surface.
    /// </summary>
    public unsafe struct Surface
    {
        public readonly uint Flags;
        public readonly PixelFormat* Format;
        public readonly int W;
        public readonly int H;
        public readonly int Pitch;
        public void* Pixels;
        public void* UserData;
        public readonly int Locked;
        public readonly void* LockData;
        public readonly Rect ClipRect;
        private IntPtr Map;
        public int RefCount;
    }
}