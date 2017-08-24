using System;
using System.Numerics;

using Vulkan;

using Version = Vulkan.Version;

namespace Engine.Rendering
{
    public static unsafe class VulkanGlobals
    {
        // Global functions
        public static readonly EnumerateInstanceExtensionProperties EnumerateInstanceExtensionProperties;
        public static readonly EnumerateInstanceLayerProperties EnumerateInstanceLayerProperties;
        public static readonly CreateInstance CreateInstance;

        // Instance functions
        public static readonly EnumeratePhysicalDevices EnumeratePhysicalDevices;
        public static readonly EnumerateDeviceExtensionProperties EnumerateDeviceExtensionProperties;
        public static readonly GetPhysicalDeviceFeatures GetPhysicalDeviceFeatures;
        public static readonly GetPhysicalDeviceProperties GetPhysicalDeviceProperties;
        public static readonly GetPhysicalDeviceQueueFamilyProperties GetPhysicalDeviceQueueFamilyProperties;
        public static readonly GetPhysicalDeviceMemoryProperties GetPhysicalDeviceMemoryProperties;
        public static readonly GetPhysicalDeviceFormatProperties GetPhysicalDeviceFormatProperties;
        public static readonly CreateDevice CreateDevice;
        public static readonly GetDeviceProcAddr GetDeviceProcAddr;
        public static readonly DestroyInstance DestroyInstance;

        // Instance functions from extensions
        public static readonly GetPhysicalDeviceSurfaceSupport GetPhysicalDeviceSurfaceSupport;
        public static readonly GetPhysicalDeviceSurfaceCapabilities GetPhysicalDeviceSurfaceCapabilities;
        public static readonly GetPhysicalDeviceSurfaceFormats GetPhysicalDeviceSurfaceFormats;
        public static readonly GetPhysicalDeviceSurfacePresentModes GetPhysicalDeviceSurfacePresentModes;
        public static readonly DestroySurface DestroySurface;
#if WINDOW_WIN32
        public static readonly CreateWin32Surface CreateWin32Surface;
#elif WINDOW_XLIB
        public static readonly CreateXlibSurface CreateXlibSurface;
#elif WINDOW_XCB
        public static readonly CreateXcbSurface CreateXcbSurface;
#elif WINDOW_MIR
        public static readonly CreateMirSurface CreateMirSurface;
#elif WINDOW_WAYLAND
        public static readonly CreateWaylandSurface CreateWaylandSurface;
#elif WINDOW_MACOS
        public static readonly CreateMacOSSurface CreateMacOSSurface;
#elif WINDOW_ANDROID
        public static readonly CreateAndroidSurface CreateAndroidSurface;
#elif WINDOW_IOS
        public static readonly CreateIOSSurface CreateIOSSurface;
#elif WINDOW_SWITCH
        public static readonly CreateViSurface CreateViSurface;
#endif

        // Device functions
        public static readonly GetDeviceQueue GetDeviceQueue;
        public static readonly DeviceWaitIdle DeviceWaitIdle;
        public static readonly DestroyDevice DestroyDevice;
        public static readonly CreateBuffer CreateBuffer;
        public static readonly GetBufferMemoryRequirements GetBufferMemoryRequirements;
        public static readonly AllocateMemory AllocateMemory;
        public static readonly BindBufferMemory BindBufferMemory;
        public static readonly CmdPipelineBarrier CmdPipelineBarrier;
        public static readonly CreateImage CreateImage;
        public static readonly GetImageMemoryRequirements GetImageMemoryRequirements;
        public static readonly BindImageMemory BindImageMemory;
        public static readonly CreateImageView CreateImageView;
        public static readonly MapMemory MapMemory;
        public static readonly FlushMappedMemoryRanges FlushMappedMemoryRanges;
        public static readonly UnmapMemory UnmapMemory;
        public static readonly CmdCopyBuffer CmdCopyBuffer;
        public static readonly CmdCopyBufferToImage CmdCopyBufferToImage;
        public static readonly CmdCopyImageToBuffer CmdCopyImageToBuffer;
        public static readonly BeginCommandBuffer BeginCommandBuffer;
        public static readonly EndCommandBuffer EndCommandBuffer;
        public static readonly QueueSubmit QueueSubmit;
        public static readonly DestroyImageView DestroyImageView;
        public static readonly DestroyImage DestroyImage;
        public static readonly DestroyBuffer DestroyBuffer;
        public static readonly FreeMemory FreeMemory;
        public static readonly CreateCommandPool CreateCommandPool;
        public static readonly AllocateCommandBuffers AllocateCommandBuffers;
        public static readonly CreateSemaphore CreateSemaphore;
        public static readonly CreateFence CreateFence;
        public static readonly WaitForFences WaitForFences;
        public static readonly ResetFences ResetFences;
        public static readonly DestroyFence DestroyFence;
        public static readonly DestroySemaphore DestroySemaphore;
        public static readonly ResetCommandBuffer ResetCommandBuffer;
        public static readonly FreeCommandBuffers FreeCommandBuffers;
        public static readonly ResetCommandPool ResetCommandPool;
        public static readonly DestroyCommandPool DestroyCommandPool;
        public static readonly CreateBufferView CreateBufferView;
        public static readonly DestroyBufferView DestroyBufferView;
        public static readonly QueueWaitIdle QueueWaitIdle;
        public static readonly CreateSampler CreateSampler;
        public static readonly CreateDescriptorSetLayout CreateDescriptorSetLayout;
        public static readonly CreateDescriptorPool CreateDescriptorPool;
        public static readonly AllocateDescriptorSets AllocateDescriptorSets;
        public static readonly UpdateDescriptorSets UpdateDescriptorSets;
        public static readonly CmdBindDescriptorSets CmdBindDescriptorSets;
        public static readonly FreeDescriptorSets FreeDescriptorSets;
        public static readonly ResetDescriptorPool ResetDescriptorPool;
        public static readonly DestroyDescriptorPool DestroyDescriptorPool;
        public static readonly DestroyDescriptorSetLayout DestroyDescriptorSetLayout;
        public static readonly DestroySampler DestroySampler;
        public static readonly CreateRenderPass CreateRenderPass;
        public static readonly CreateFramebuffer CreateFramebuffer;
        public static readonly DestroyFramebuffer DestroyFramebuffer;
        public static readonly DestroyRenderPass DestroyRenderPass;
        public static readonly CmdBeginRenderPass CmdBeginRenderPass;
        public static readonly CmdNextSubpass CmdNextSubpass;
        public static readonly CmdEndRenderPass CmdEndRenderPass;
        public static readonly CreatePipelineCache CreatePipelineCache;
        public static readonly GetPipelineCacheData GetPipelineCacheData;
        public static readonly MergePipelineCaches MergePipelineCaches;
        public static readonly DestroyPipelineCache DestroyPipelineCache;
        public static readonly CreateGraphicsPipelines CreateGraphicsPipelines;
        public static readonly CreateComputePipelines CreateComputePipelines;
        public static readonly DestroyPipeline DestroyPipeline;
        public static readonly DestroyEvent DestroyEvent;
        public static readonly DestroyQueryPool DestroyQueryPool;
        public static readonly CreateShaderModule CreateShaderModule;
        public static readonly DestroyShaderModule DestroyShaderModule;
        public static readonly CreatePipelineLayout CreatePipelineLayout;
        public static readonly DestroyPipelineLayout DestroyPipelineLayout;
        public static readonly CmdBindPipeline CmdBindPipeline;
        public static readonly CmdSetViewport CmdSetViewport;
        public static readonly CmdSetScissor CmdSetScissor;
        public static readonly CmdBindVertexBuffers CmdBindVertexBuffers;
        public static readonly CmdDraw CmdDraw;
        public static readonly CmdDrawIndexed CmdDrawIndexed;
        public static readonly CmdDispatch CmdDispatch;
        public static readonly CmdCopyImage CmdCopyImage;
        public static readonly CmdPushConstants CmdPushConstants;
        public static readonly CmdClearColorImage CmdClearColorImage;
        public static readonly CmdClearDepthStencilImage CmdClearDepthStencilImage;
        public static readonly CmdBindIndexBuffer CmdBindIndexBuffer;
        public static readonly CmdSetLineWidth CmdSetLineWidth;
        public static readonly CmdSetDepthBias CmdSetDepthBias;
        public static readonly CmdSetBlendConstants CmdSetBlendConstants;
        public static readonly CmdExecuteCommands CmdExecuteCommands;
        public static readonly CmdClearAttachments CmdClearAttachments;

        // Device functions from extensions
        public static readonly CreateSwapchain CreateSwapchain;
        public static readonly GetSwapchainImages GetSwapchainImages;
        public static readonly AcquireNextImage AcquireNextImage;
        public static readonly QueuePresent QueuePresent;
        public static readonly DestroySwapchain DestroySwapchain;

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
            EnumerateInstanceExtensionProperties = Vk.LoadGlobalFunction<EnumerateInstanceExtensionProperties>();
            EnumerateInstanceLayerProperties = Vk.LoadGlobalFunction<EnumerateInstanceLayerProperties>();
            CreateInstance = Vk.LoadGlobalFunction<CreateInstance>();

            // Instance extensions
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
                Game.Settings.RendererSettings.EnableDebugReport ? DebugReportExtensionName : string.Empty
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

            // Instance
            ApplicationInfo applicationInfo = new ApplicationInfo(Game.Settings.Name, new Version(1, 0, 0), "Pillar Engine", new Version(1, 0, 0), new Version(1, 0, 0));
            InstanceCreateInfo instanceCreateInfo = new InstanceCreateInfo(&applicationInfo, desiredInstanceExtensions);
            CreateInstance(ref instanceCreateInfo, ref AllocationCallbacks.Null, out _instance).CheckError();

            // Instance functions
            EnumeratePhysicalDevices = Vk.LoadInstanceFunction<EnumeratePhysicalDevices>(_instance);
            EnumerateDeviceExtensionProperties = Vk.LoadInstanceFunction<EnumerateDeviceExtensionProperties>(_instance);
            GetPhysicalDeviceProperties = Vk.LoadInstanceFunction<GetPhysicalDeviceProperties>(_instance);
            GetPhysicalDeviceFeatures = Vk.LoadInstanceFunction<GetPhysicalDeviceFeatures>(_instance);
            CreateDevice = Vk.LoadInstanceFunction<CreateDevice>(_instance);
            GetDeviceProcAddr = Vk.LoadInstanceFunction<GetDeviceProcAddr>(_instance);

            // Physical devices
            EnumeratePhysicalDevices(_instance, out uint deviceCount, null).CheckError();
            PhysicalDevice[] availablePhysicalDevices = new PhysicalDevice[(int)deviceCount];
            EnumeratePhysicalDevices(_instance, out deviceCount, availablePhysicalDevices).CheckError();

            foreach (PhysicalDevice physicalDevice in availablePhysicalDevices)
            {
                // Device extensions
                EnumerateDeviceExtensionProperties(physicalDevice, Text.Null, out extensionCount, null).CheckError();
                availableExtensions = new ExtensionProperties[(int)extensionCount];
                EnumerateDeviceExtensionProperties(physicalDevice, Text.Null, out extensionCount, availableExtensions).CheckError();

                // Queue families
                GetPhysicalDeviceQueueFamilyProperties(physicalDevice, out uint queueFamilyPropertyCount, null);
                if (queueFamilyPropertyCount == 0)
                {
                    throw new InvalidOperationException();
                }
                QueueFamilyProperties[] queueFamilies = new QueueFamilyProperties[(int)queueFamilyPropertyCount];
                GetPhysicalDeviceQueueFamilyProperties(physicalDevice, out queueFamilyPropertyCount, queueFamilies);
                if (queueFamilyPropertyCount == 0)
                {
                    throw new InvalidOperationException();
                }

                // Select graphics queue family
                for (int i = 0; i < queueFamilies.Length; ++i)
                {
                    if (queueFamilies[i].QueueCount > 0 &&
                        (queueFamilies[i].QueueFlags & QueueFlags.Graphics) != 0)
                    {

                    }
                }
            }
        }
    }
}