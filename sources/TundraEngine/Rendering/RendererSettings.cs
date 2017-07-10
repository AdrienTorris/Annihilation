using System.Runtime.InteropServices;

using SharpVk;
using SharpBgfx;

namespace TundraEngine.Rendering
{
    public enum RendererType : byte
    {
        Vulkan,
        BGFX,
        SDL,
        None
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    public struct VulkanInfo
    {
        public bool EnableValidation;
        public PresentMode PresentMode;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    public struct BGFXInfo
    {
        public bool EnableDebug;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct RendererSettings
    {
        [FieldOffset(0)] public RendererType RendererType;
        [FieldOffset(1)] public VulkanInfo VulkanInfo;
        [FieldOffset(1)] public BGFXInfo BGFXInfo;
        [FieldOffset(9)] public int Width;
        [FieldOffset(13)] public int Height;
        [FieldOffset(17)] public bool VSync;
        [FieldOffset(18)] public uint SSAA;

        public const int DefaultSize = -1;
    }
}