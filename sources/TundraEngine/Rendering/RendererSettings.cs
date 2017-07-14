using SharpVk;

namespace TundraEngine.Rendering
{
    public struct VulkanSettings
    {
        public bool EnableValidation;
        public DebugReportFlags DebugFlags;
        public PresentMode PresentMode;

        public const DebugReportFlags DefaultDebugFlags = DebugReportFlags.Error | DebugReportFlags.PerformanceWarning | DebugReportFlags.Warning;
        public const PresentMode DefaultPresentMode = PresentMode.Mailbox;
    }
    
    public struct RendererSettings
    {
        public VulkanSettings VulkanSettings;
        public int Width;
        public int Height;
        public bool UseDepthBuffer;
        public bool VSync;
        public uint SSAA;

        public const int DefaultSize = -1;
    }
}