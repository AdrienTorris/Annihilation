using Vulkan;

namespace Engine.Rendering
{
    public struct RendererSettings
    {
        public int Width;
        public int Height;
        public PresentMode PresentMode;
        public SampleCountFlags SampleCount;
    }
}