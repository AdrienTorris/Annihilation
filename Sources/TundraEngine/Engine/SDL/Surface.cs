using System;
using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [Flags]
    public enum SurfaceFlags : uint
    {
        Software = 0,
        PreAllocated = 1 << 0,
        EncodedRLE = 1 << 1,
        DontFree = 1 << 2
    }

    [StructLayout(LayoutKind.Sequential)]
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