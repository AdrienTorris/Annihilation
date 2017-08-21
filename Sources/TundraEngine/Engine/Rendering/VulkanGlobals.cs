using System;
using System.Numerics;
using System.Collections.Generic;

using CoreVulkan;
using CoreVulkan.Handle;

using static CoreVulkan.Constants;

using Version = CoreVulkan.Version;

namespace Engine.Rendering
{
    public static unsafe class VulkanGlobals
    {
        // Global functions
        public static readonly EnumerateInstanceExtensionProperties EnumerateInstanceExtensionProperties;
        public static readonly EnumerateInstanceLayerProperties EnumerateInstanceLayerProperties;
        public static readonly CreateInstance CreateInstance;

        // Instance functions
        public static readonly DestroyInstance DestroyInstance;

        // Device functions

        // Objects
        private static Instance _instance;
        private static PhysicalDevice _physicalDevice;
        private static PhysicalDeviceFeatures _physicalDeviceFeatures;
        private static Surface _surface;
        private static SurfaceCapabilities _surfaceCapabilities;
        private static Swapchain _swapchain;
        private static uint _numSwapChainImages;
        private static uint _currentCommandBuffer;
        private static CommandPool _commandPool;
        private static CommandPool _transientCommandPool;
        private static CommandBuffer[] _commandBuffers;
        private static Fence[] _commandBufferFences;
        private static bool[] _commandBufferSubmitted;
        private static Framebuffer[] _mainFramebuffers;
        private static Semaphore[] _drawCompleteSemaphores;
        private static Framebuffer[] _uiFramebuffers;
        private static Image[] _swapchainImages;
        private static ImageView[] _swapchainImageViews;
        private static Semaphore[] _imageAcquiredSemaphores;
        private static DeviceMemory[] _colorBuffersMemory;
        private static ImageView[] _colorBuffersView;
        private static Image _msaaColorBuffer;
        private static DeviceMemory _msaaColorBufferMemory;
        private static ImageView _msaaColorBufferView;
        private static DescriptorSet _postprocessDescriptorSet;
        private static Image _depthBuffer;
        private static DeviceMemory _depthBufferMemory;
        private static ImageView _depthBufferView;

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

        static VulkanGlobals()
        {
            // Global functions
            EnumerateInstanceExtensionProperties = Vulkan.LoadGlobalFunction<EnumerateInstanceExtensionProperties>();
            EnumerateInstanceLayerProperties = Vulkan.LoadGlobalFunction<EnumerateInstanceLayerProperties>();
            CreateInstance = Vulkan.LoadGlobalFunction<CreateInstance>();

            // Instance
            Text[] desiredInstanceExtensions = new Text[]
            {
                SurfaceExtensionName,
                Game.WindowType == WindowType.Win32 ? Win32SurfaceExtensionName :
                Game.WindowType == WindowType.Xlib ? XlibSurfaceExtensionName :
                Game.WindowType == WindowType.Xcb ? XcbSurfaceExtensionName :
                Game.WindowType == WindowType.Mir ? MirSurfaceExtensionName :
                Game.WindowType == WindowType.Wayland ? WaylandSurfaceExtensionName :
                Game.WindowType == WindowType.Android ? AndroidSurfaceExtensionName :
                Game.WindowType == WindowType.IOS ? IOSSurfaceExtensionName :
                Game.WindowType == WindowType.MacOS ? MacOSSurfaceExtensionName :
                Game.WindowType == WindowType.Switch ? ViSurfaceExtensionName :
                throw new PlatformNotSupportedException(),
#if DEBUG
                Game.Settings.RendererSettings.DebugReport ? DebugReportExtensionName : string.Empty
#endif
            };
            EnumerateInstanceExtensionProperties(Text.Null, out uint extensionCount, null).CheckError();
            ExtensionProperties[] availableExtensions = new ExtensionProperties[(int)extensionCount];
            EnumerateInstanceExtensionProperties(Text.Null, out extensionCount, availableExtensions).CheckError();
            foreach (Text desiredExtension in desiredInstanceExtensions)
            {
                foreach (ExtensionProperties availableExtension in availableExtensions)
                {
                    if (!availableExtension.ExtensionName.Compare(desiredExtension))
                    {
                        throw new InvalidOperationException("Extension " + desiredExtension + " is not supported");
                    }
                }
            }

            ApplicationInfo applicationInfo = new ApplicationInfo(Game.Settings.Name, new Version(1, 0, 0), "Pillar Engine", new Version(1, 0, 0), new Version(1, 0, 0));
            InstanceCreateInfo instanceCreateInfo;
            fixed (Text* extensionsPointer = &desiredInstanceExtensions[0])
            {
                instanceCreateInfo = new InstanceCreateInfo(&applicationInfo, desiredInstanceExtensions.Length, extensionsPointer);
            }

            CreateInstance(ref instanceCreateInfo, ref AllocationCallbacks.Null, out _instance).CheckError();

            // Instance functions

        }
    }
}