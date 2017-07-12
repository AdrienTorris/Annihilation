using SharpVk;

namespace TundraEngine.Rendering
{
    public struct VulkanSettings
    {
        public bool EnableValidation;
        public DebugReportFlags DebugFlags;
        public PresentMode PresentMode;
    }
    
    public struct RendererSettings
    {
        public VulkanSettings VulkanSettings;
        public int Width;
        public int Height;
        public bool VSync;
        public uint SSAA;

        public const int DefaultSize = -1;
    }
}