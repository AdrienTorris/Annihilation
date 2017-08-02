using System.Numerics;

using TundraEngine.Vulkan;

namespace TundraEngine.Rendering
{
    public static class VulkanGlobals
    {
        public static Device Device;
        public static bool IsDeviceIdle;
        public static bool EnableValidation;
        public static Queue GraphicsQueue;
        public static Queue ComputeQueue;
        public static CommandBuffer CommandBuffer;
        public static ClearValue ColorClearValue;
        public static Format SwapChainFormat;
        public static PhysicalDeviceProperties DeviceProperties;
        public static PhysicalDeviceMemoryProperties MemoryProperties;
        public static uint GraphicsQueueFamilyIndex;
        public static uint ComputeQueueFamilyIndex;
        public static Format ColorFormat;
        public static Format DepthFormat;
        public static SampleCountFlags SampleCount;
        public static bool EnableSupersampling;

        // Buffers
        public static Image[] ColorBuffers;

        // Render passes
        public static RenderPass MainRenderPass;
        public static ClearValue[] MainClearValues;
        public static RenderPassBeginInfo[] MainRenderPassBeginInfos;
        public static RenderPass UIRenderPass;
        public static RenderPassBeginInfo UIRenderPassBeginInfo;

        // Pipelines
        public static Pipeline[] BasicAlphaTestPipelines;
        public static PipelineLayout BasicPipelineLayout;
        public static Pipeline PostprocessPipeline;
        public static PipelineLayout PostprocessPipelineLayout;

        // Descriptors
        public static DescriptorPool DescriptorPool;
        public static DescriptorSetLayout UboSetLayout;
        public static DescriptorSetLayout SingleTextureSetLayout;
        public static DescriptorSetLayout InputAttachmentSetLayout;

        // Samplers
        public static Sampler PointSampler;
        public static Sampler LinearSampler;
        public static Sampler PointAnisoSampler;
        public static Sampler LinearAnisoSampler;

        // Matrices
        public static Matrix4x4 ProjectionMatrix;
        public static Matrix4x4 ViewMatrix;
        public static Matrix4x4 ViewProjectionMatrix;
    }
}