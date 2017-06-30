using System.Runtime.InteropServices;

namespace TundraEngine.Rendering
{
    public enum RendererType : byte
    {
        Vulkan,
        BGFX,
        SDL,
        None
    }

    public enum PresentMode : byte
    {
        Immediate,
        Mailbox,
        Fifo,
        FifoRelaxed
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
    public struct RendererInfo
    {
        [FieldOffset(0)] public RendererType RendererType;
        [FieldOffset(1)] public VulkanInfo VulkanInfo;
        [FieldOffset(1)] public BGFXInfo BGFXInfo;
        [FieldOffset(9)] public int ResolutionX;
        [FieldOffset(13)] public int ResolutionY;
        [FieldOffset(17)] public bool VSync;
        [FieldOffset(18)] public uint SSAA;
    }
}