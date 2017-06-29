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

    public struct RendererInfo
    {
        public RendererType RendererType;
        public int ResolutionX;
        public int ResolutionY;
        public bool VSync;
        public uint SSAA;
        public PresentMode PresentMode;
        public bool EnableValidation;
    }
}