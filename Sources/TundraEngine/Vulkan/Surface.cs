using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    [Flags] public enum DisplaySurfaceCreateFlags : uint { }

    [Flags]
    public enum DisplayPlaneAlphaFlags : uint
    {
        Opaque = 0x00000001,
        Global = 0x00000002,
        PerPixel = 0x00000004,
        PerPixelPremultiplied = 0x00000008
    }

    public unsafe struct DisplaySurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DisplaySurfaceCreateFlags Flags;
        public DisplayMode DisplayMode;
        public uint PlaneIndex;
        public uint PlaneStackIndex;
        public SurfaceTransformFlags Transform;
        public float GlobalAlpha;
        public DisplayPlaneAlphaFlags AlphaMode;
        public Extent2D ImageExtent;
    }

    public struct Surface
    {
        internal ulong NativeHandle; 
    }
}