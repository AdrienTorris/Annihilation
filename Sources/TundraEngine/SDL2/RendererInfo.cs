using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    [Flags]
    public enum RendererFlags : uint
    {
        Software = 1 << 0,
        Accelerated = 1 << 1,
        PresentVSync = 1 << 2,
        TargetTexture = 1 << 3
    }

    [Flags]
    public enum RendererFlip
    {
        None = 0,
        Horizontal = 1 << 0,
        Vertical = 1 << 1
    }

    public enum TextureAccess
    {
        Static,
        Streaming,
        Target
    }

    [Flags]
    public enum TextureModulate
    {
        None = 0,
        Horizontal = 1 << 0,
        Vertical = 1 << 1
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RendererInfo
    {
        public byte* Name;
        public RendererFlags Flags;
        public uint NumTextureFormats;
        public fixed uint TextureFormats[16];
        public int MaxTextureWidth;
        public int MaxTextureHeight;
    }
}