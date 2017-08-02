namespace TundraEngine.Vulkan
{
    public struct SurfaceCapabilities
    {
        public uint MinImageCount;
        public uint MaxImageCount;
        public Extent2D CurrentExtent;
        public Extent2D MinImageExtent;
        public Extent2D MaxImageExtent;
        public uint MaxImageArrayLayers;
        public SurfaceTransformFlags SupportedTransforms;
        public SurfaceTransformFlags CurrentTransform;
        public CompositeAlphaFlags SupportedCompositeAlpha;
        public ImageUsageFlags SupportedUsageFlags;
    }
}