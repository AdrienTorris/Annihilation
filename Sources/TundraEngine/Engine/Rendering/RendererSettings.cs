using CoreVulkan;

namespace Engine.Rendering
{
    public struct RendererSettings
    {
        public int Width;
        public int Height;
        public bool EnableDebugReport;
        public DebugReportFlags DebugReportFlags;
        public bool EnableValidation;
        public PresentMode PresentMode;
        public SampleCountFlags SampleCount;
    }
}