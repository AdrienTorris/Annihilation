namespace TundraEngine.Graphics
{
    public enum PresentMode : byte
    {
        Immediate,
        Mailbox,
        Fifo,
        FifoRelaxed
    }

    public struct GraphicsInfo
    {
        /// <summary>
        /// Window resolution multiplier to use for the renderer.
        /// <para/> 1.0 means "use window size as rendering resolution".
        /// </summary>
        public float RenderScale;
        public bool VSync;
        public uint SSAA;
        public PresentMode PresentMode;
        public bool EnableValidation;
    }
}