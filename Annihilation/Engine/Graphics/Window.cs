using System;
using System.Runtime.InteropServices;
using SDL2;
using Vulkan;

namespace Engine.Graphics
{
    public unsafe class Window : IDisposable
    {
        public const int DefaultWidth = 1280;
        public const int DefaultHeight = 720;
        private const Vk.FormatFeatureFlags RequiredColorBufferFeatures =
            Vk.FormatFeatureFlags.ColorAttachment |
            Vk.FormatFeatureFlags.ColorAttachmentBlend |
            Vk.FormatFeatureFlags.SampledImage |
            Vk.FormatFeatureFlags.StorageImage |
            Vk.FormatFeatureFlags.SampledImageFilterLinear;
        private const int CommandBufferCount = 2;
        private const int ColorBufferCount = 2;
        private const int MaxSwapchainImages = 8;

        public static bool VideoSubSystemInitialized { get; private set; }

        private static Vk.CreateInstanceDelegate vkCreateInstance;
        private static Vk.DestroyInstanceDelegate vkDestroyInstance;

        private static Vk.EnumeratePhysicalDevicesDelegate vkEnumeratePhysicalDevices;
        private static Vk.EnumerateDeviceExtensionPropertiesDelegate vkEnumerateDeviceExtensionProperties;

        private static Vk.GetPhysicalDevicePropertiesDelegate vkGetPhysicalDeviceProperties;
        private static Vk.GetPhysicalDeviceMemoryPropertiesDelegate vkGetPhysicalDeviceMemoryProperties;
        private static Vk.GetPhysicalDeviceQueueFamilyPropertiesDelegate vkGetPhysicalDeviceQueueFamilyProperties;
        private static Vk.GetPhysicalDeviceFeaturesDelegate vkGetPhysicalDeviceFeatures;
        private static Vk.GetPhysicalDeviceFormatPropertiesDelegate vkGetPhysicalDeviceFormatProperties;

        private static Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate vkGetPhysicalDeviceSurfaceSupport;
        private static Vk.GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate vkGetPhysicalDeviceSurfaceCapabilities;
        private static Vk.GetPhysicalDeviceSurfaceFormatsKHRDelegate vkGetPhysicalDeviceSurfaceFormats;
        private static Vk.GetPhysicalDeviceSurfacePresentModesKHRDelegate vkGetPhysicalDeviceSurfacePresentModes;

        private static Vk.CreateDeviceDelegate vkCreateDevice;

        private static Vk.GetDeviceQueueDelegate vkGetDeviceQueue;
        private static Vk.CreateCommandPoolDelegate vkCreateCommandPool;
        private static Vk.AllocateCommandBuffersDelegate vkAllocateCommandBuffers;
        private static Vk.CreateFenceDelegate vkCreateFence;
        private static Vk.CreateSemaphoreDelegate vkCreateSemaphore;

        private static Vk.CreateSwapchainKHRDelegate vkCreateSwapchain;
        private static Vk.DestroySwapchainKHRDelegate vkDestroySwapchain;
        private static Vk.GetSwapchainImagesKHRDelegate vkGetSwapchainImages;
        private static Vk.AcquireNextImageKHRDelegate vkAcquireNextImage;
        private static Vk.QueuePresentKHRDelegate vkQueuePresent;
#if DEBUG
        private static Vk.CreateDebugReportCallbackEXTDelegate vkCreateDebugReportCallback;
        private static Vk.DestroyDebugReportCallbackEXTDelegate vkDestroyDebugReportCallback;
        private static Vk.DebugMarkerSetObjectNameEXTDelegate vkDebugMarkerSetObjectName;
#endif

        public bool HasFocus { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        private IntPtr _handle;
#if PLATFORM_LINUX
        private IntPtr _display;
#endif
        private SDL.Window _sdlWindow;
        private Vk.Instance _vulkanInstance;
        private Vk.Surface _vulkanSurface;
        private Vk.PhysicalDevice _vulkanPhysicalDevice;
        private Vk.PhysicalDeviceFeatures _vulkanPhysicalDeviceFeatures;

        private Vk.CommandPool _commandPool;
        private Vk.CommandPool _transientCommandPool;
        private Vk.CommandBuffer[] _commandBuffers = new Vk.CommandBuffer[CommandBufferCount];
        private Vk.Fence[] _commandBufferFences = new Vk.Fence[CommandBufferCount];
        private Vk.Semaphore[] _drawCompleteSemaphores = new Vk.Semaphore[CommandBufferCount];

        private Vk.SurfaceCapabilities _vulkanSurfaceCapabilities;
        private Vk.Swapchain _vulkanSwapchain;

        private bool[] _isCommandBufferSubmitted = new bool[CommandBufferCount];
        private uint _currentSwapchainBuffer;
#if DEBUG
        private Vk.DebugReportCallback _debugReportCallback;

        private static Vk.Bool32 DebugMessageCallback(Vk.DebugReportFlags flags, Vk.DebugReportObjectType objectType, ulong @object, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData)
        {
            if (objectType == Vk.DebugReportObjectType.DebugReportCallback && messageCode == 1)
            {
                return false;
            }

            string output = "[" + new StringUtf8(layerPrefix).ToString() + "] Code " + messageCode + ": " + new StringUtf8(message).ToString();

            switch (flags)
            {
                case Vk.DebugReportFlags.Information: Log.Info(output); return false;
                case Vk.DebugReportFlags.Warning: Log.Warning(output); return false;
                case Vk.DebugReportFlags.PerformanceWarning: Log.Performance(output); return false;
                case Vk.DebugReportFlags.Error: Log.Error(output); return true;
                case Vk.DebugReportFlags.Debug: Log.Info(output); return false;
                default: return false;
            }
        }
#endif

        public Window(ref StringUtf8 title)
        {
            // Initializing SDL video subsystem.");
            if (VideoSubSystemInitialized == false)
            {
                SDL.InitSubSystem(SDL.InitFlags.Video).CheckError();
                VideoSubSystemInitialized = true;
            }

            // Loading Vulkan loader library.");
            SDL.VulkanLoadLibrary(null).CheckError();

            // Creating SDL window.");
            _sdlWindow = SDL.CreateWindow(
                title,
                SDL.WindowPositionUndefined,
                SDL.WindowPositionUndefined,
                VariableManager.GetVar(Application.ConfigResolutionX, 1280),
                VariableManager.GetVar(Application.ConfigResolutionY, 720),
                SDL.WindowFlags.Shown | SDL.WindowFlags.Vulkan
            );
            _sdlWindow.CheckError();

            // Getting window manager info.");
            SDL.SysWMInfo sysWMInfo = default(SDL.SysWMInfo);
            SDL.GetVersion(out sysWMInfo.Version);
            SDL.GetWindowWMInfo(_sdlWindow, ref sysWMInfo).CheckError();

#if PLATFORM_WINDOWS
            _handle = sysWMInfo.Info.Windows.Window;
#elif PLATFORM_LINUX
            switch(sysWMInfo.SubSystem)
            {
                case SDL.SysWMType.X11:
                    Handle = sysWMInfo.Info.X11.Window;
                    Display = sysWMInfo.Info.X11.Display;
                    break;
                case SDL.SysWMType.Wayland:
                    Handle = sysWMInfo.Info.Wayland.Surface;
                    Display = sysWMInfo.Info.Wayland.Display;
                    break;
                case SDL.SysWMType.Mir:
                    Handle = sysWMInfo.Info.Mir.Connection;
                    Display = sysWMInfo.Info.Mir.Surface;
                    break;
            }
#elif PLATFORM_MACOS
            Handle = sysWMInfo.Info.Cocoa.Window;
#endif
            // Initializing Vulkan.");
            InitInstance(title);
            InitDevice();
            InitCommandBuffers();
            CreateSwapchain();
            CreateColorBuffer();
            CreateDepthBuffer();
            CreateRenderPasses();
            CreateFramebuffers();
            InitStagingBuffers();
            CreateDescriptorSetLayouts();
            CreateDescriptorPool();
            InitDynamicBuffers();
            InitSamplers();
            CreatePipelineLayouts();
            CreatePipelines();
            CreateDescriptorSets();
        }


        public void SetMode(int width, int height, int refreshRate, int bpp, bool fullscreen)
        {
            // Creating SDL window.");
            if (_sdlWindow.Handle == IntPtr.Zero)
            {
                SDL.WindowFlags flags = SDL.WindowFlags.Hidden | SDL.WindowFlags.Vulkan;

                //if (VariableManager.GetVar(""))
            }
        }

        private void GetCurrentSize(out int width, out int height)
        {
            SDL.VulkanGetDrawableSize(_sdlWindow, out width, out height);
        }

        private int GetCurrentWidth()
        {
            SDL.VulkanGetDrawableSize(_sdlWindow, out int width, out int height);
            return width;
        }

        private int GetCurrentHeight()
        {
            SDL.VulkanGetDrawableSize(_sdlWindow, out int width, out int height);
            return height;
        }

        private void InitInstance(byte* title)
        {
            // Loading Vulkan getInstanceProcAddr function.");
            Vk.GetInstanceProcAddr = Vk.GetInstanceProcAddr ?? Marshal.GetDelegateForFunctionPointer<Vk.GetInstanceProcAddrDelegate>(SDL.VulkanGetVkGetInstanceProcAddr());

            // Getting required Vulkan instance extensions for surface.");
            uint extensionCount = 0;
            SDL.VulkanGetInstanceExtensions(_sdlWindow, ref extensionCount, null).CheckError();

            // TODO: Free those
            StringUtf8Array extensionNames = new StringUtf8Array(extensionCount + 1);
            SDL.VulkanGetInstanceExtensions(_sdlWindow, ref extensionCount, extensionNames.ArrayPtr).CheckError();

#if DEBUG
            bool enableValidation = VariableManager.GetVar(Application.ConfigEnableVulkanValidation, true);
            if (enableValidation)
            {
                extensionNames[extensionCount] = Vk.ExtDebugReportExtensionName.ToUtf8();
            }
#endif
            // Loading Vulkan createInstance function.");
            vkCreateInstance = vkCreateInstance ?? Vk.LoadGlobalFunction<Vk.CreateInstanceDelegate>();

            // Creating Vulkan instance.");
            Vk.ApplicationInfo applicationInfo = new Vk.ApplicationInfo(
                title,
                Vulkan.Version.One,
                title,
                Vulkan.Version.One,
                Vulkan.Version.One
            );

            Vk.InstanceCreateInfo instanceCreateInfo = new Vk.InstanceCreateInfo(
                &applicationInfo,
                extensionCount,
                extensionNames.ArrayPtr
            );
#if DEBUG
            if (enableValidation)
            {
                // TODO: Free these
                StringUtf8Array layerNames = new StringUtf8Array(1);
                layerNames[0] = "VK_LAYER_LUNARG_standard_validation".ToUtf8();

                instanceCreateInfo.EnabledExtensionCount = extensionCount + 1;
                instanceCreateInfo.EnabledLayerCount = 1;
                instanceCreateInfo.EnabledLayerNames = layerNames.ArrayPtr;
            }
#endif
            vkCreateInstance(ref instanceCreateInfo, null, out _vulkanInstance).CheckError();

            if (_vulkanInstance == Vk.Instance.Null)
            {
                Log.Error("Vulkan instance is null.");
            }

            // Creating Vulkan surface.");
            SDL.VulkanCreateSurface(_sdlWindow, _vulkanInstance, out _vulkanSurface).CheckError();

            // Loading Vulkan instance functions.");
            Vk.GetDeviceProcAddr = Vk.GetDeviceProcAddr ?? Vk.LoadInstanceFunction<Vk.GetDeviceProcAddrDelegate>(_vulkanInstance);

            vkDestroyInstance = vkDestroyInstance ?? Vk.LoadInstanceFunction<Vk.DestroyInstanceDelegate>(_vulkanInstance);

            vkEnumeratePhysicalDevices = vkEnumeratePhysicalDevices ?? Vk.LoadInstanceFunction<Vk.EnumeratePhysicalDevicesDelegate>(_vulkanInstance);
            vkEnumerateDeviceExtensionProperties = vkEnumerateDeviceExtensionProperties ?? Vk.LoadInstanceFunction<Vk.EnumerateDeviceExtensionPropertiesDelegate>(_vulkanInstance);

            vkGetPhysicalDeviceProperties = vkGetPhysicalDeviceProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDevicePropertiesDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceMemoryProperties = vkGetPhysicalDeviceMemoryProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceMemoryPropertiesDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceQueueFamilyProperties = vkGetPhysicalDeviceQueueFamilyProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceQueueFamilyPropertiesDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceFeatures = vkGetPhysicalDeviceFeatures ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceFeaturesDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceFormatProperties = vkGetPhysicalDeviceFormatProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceFormatPropertiesDelegate>(_vulkanInstance);

            vkGetPhysicalDeviceSurfaceSupport = vkGetPhysicalDeviceSurfaceSupport ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceSurfaceCapabilities = vkGetPhysicalDeviceSurfaceCapabilities ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceSurfaceFormats = vkGetPhysicalDeviceSurfaceFormats ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceFormatsKHRDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceSurfacePresentModes = vkGetPhysicalDeviceSurfacePresentModes ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfacePresentModesKHRDelegate>(_vulkanInstance);

            vkCreateDevice = vkCreateDevice ?? Vk.LoadInstanceFunction<Vk.CreateDeviceDelegate>(_vulkanInstance);

            vkGetSwapchainImages = vkGetSwapchainImages ?? Vk.LoadInstanceFunction<Vk.GetSwapchainImagesKHRDelegate>(_vulkanInstance);

#if DEBUG
            if (enableValidation)
            {
                vkCreateDebugReportCallback = Vk.LoadInstanceFunction<Vk.CreateDebugReportCallbackEXTDelegate>(_vulkanInstance);
                vkDestroyDebugReportCallback = Vk.LoadInstanceFunction<Vk.DestroyDebugReportCallbackEXTDelegate>(_vulkanInstance);

                // Creating Vulkan debug report callback.");
                Vk.DebugReportCallbackCreateInfo debugReportCallbackCreateInfo = new Vk.DebugReportCallbackCreateInfo(
                    Vk.DebugReportFlags.Error | Vk.DebugReportFlags.Warning | Vk.DebugReportFlags.PerformanceWarning | Vk.DebugReportFlags.Information,
                    DebugMessageCallback
                );

                vkCreateDebugReportCallback(_vulkanInstance, ref debugReportCallbackCreateInfo, null, out _debugReportCallback).CheckError();
            }
#endif

            // TODO: Free allocated Texts?
        }

        private void InitDevice()
        {
            // Enumerating physical devices.");
            uint physicalDeviceCount = 0;
            vkEnumeratePhysicalDevices(_vulkanInstance, ref physicalDeviceCount, null).CheckError();

            if (physicalDeviceCount == 0)
            {
                Log.Error("No Vulkan physical device found.");
            }

            // TODO: This should be user-configurable through command line args and settings
            int deviceIndex = 0;
            Vk.PhysicalDevice[] physicalDevices = new Vk.PhysicalDevice[(int)physicalDeviceCount];
            vkEnumeratePhysicalDevices(_vulkanInstance, ref physicalDeviceCount, physicalDevices).CheckError();

            _vulkanPhysicalDevice = physicalDevices[deviceIndex];

            if (_vulkanPhysicalDevice == Vk.PhysicalDevice.Null)
            {
                Log.Error("Vulkan physical device is null.");
            }

            // Getting device memory properties.");
            vkGetPhysicalDeviceMemoryProperties(_vulkanPhysicalDevice, out Graphics.DeviceMemoryProperties);

            // Enumerating device extensions.");
            uint deviceExtensionCount = 0;
            vkEnumerateDeviceExtensionProperties(_vulkanPhysicalDevice, null, ref deviceExtensionCount, null).CheckError();

            if (deviceExtensionCount == 0)
            {
                Log.Error("No device extensions found.");
            }

            Vk.ExtensionProperties[] extensionProperties = new Vk.ExtensionProperties[deviceExtensionCount];
            vkEnumerateDeviceExtensionProperties(_vulkanPhysicalDevice, null, ref deviceExtensionCount, extensionProperties).CheckError();

            bool foundSwapchainExtension = false;
#if DEBUG
            bool foundDebugMarkerExtension = false;
#endif
            Graphics.DedicatedAllocation = false;

            foreach (Vk.ExtensionProperties extension in extensionProperties)
            {
                if (extension.IsNamed(Vk.SwapchainExtensionName))
                {
                    foundSwapchainExtension = true;
                }
#if DEBUG
                else if (extension.IsNamed(Vk.DebugMarkerExtensionName))
                {
                    foundDebugMarkerExtension = true;
                }
#endif
                else if (extension.IsNamed(Vk.DedicatedAllocationExtensionName))
                {
                    Graphics.DedicatedAllocation = true;
                }
            }

            if (!foundSwapchainExtension)
            {
                Log.Error($"Couldn't find {Vk.SwapchainExtensionName} extension.");
            }

            // Getting device properties.");
            vkGetPhysicalDeviceProperties(_vulkanPhysicalDevice, out Graphics.DeviceProperties);
            Log.Info($"Device: {Graphics.VendorNames[Graphics.DeviceProperties.VendorId]} {Graphics.DeviceProperties.GetDeviceName()}");

            // Getting queue family properties.");
            uint queueFamilyCount = 0;
            vkGetPhysicalDeviceQueueFamilyProperties(_vulkanPhysicalDevice, ref queueFamilyCount, null);

            if (queueFamilyCount == 0)
            {
                Log.Error("No Vulkan queues found.");
            }

            Vk.QueueFamilyProperties[] queueFamilyProperties = new Vk.QueueFamilyProperties[queueFamilyCount];
            vkGetPhysicalDeviceQueueFamilyProperties(_vulkanPhysicalDevice, ref queueFamilyCount, queueFamilyProperties);

            // Finding queues with present support.");
            Vk.Bool32[] queueSupportsPresent = new Vk.Bool32[queueFamilyCount];
            for (uint i = 0; i < queueFamilyCount; ++i)
            {
                vkGetPhysicalDeviceSurfaceSupport(_vulkanPhysicalDevice, i, _vulkanSurface, out queueSupportsPresent[i]);
            }

            // Finding queue with graphics and present support.");
            bool foundGraphicsQueue = false;

            for (uint i = 0; i < queueFamilyCount; ++i)
            {
                if ((queueFamilyProperties[i].QueueFlags & Vk.QueueFlags.Graphics) != 0 && queueSupportsPresent[i])
                {
                    foundGraphicsQueue = true;
                    Graphics.GraphicsQueueFamily = i;
                }
            }

            if (!foundGraphicsQueue)
            {
                Log.Error("No graphics+present queue found.");
            }

            // TODO: Find exclusive compute and transfer queues

            // Getting Vulkan physical device features.");
            vkGetPhysicalDeviceFeatures(_vulkanPhysicalDevice, out _vulkanPhysicalDeviceFeatures);

            Vk.PhysicalDeviceFeatures physicalDeviceFeatures = new Vk.PhysicalDeviceFeatures
            {
                ShaderStorageImageExtendedFormats = _vulkanPhysicalDeviceFeatures.ShaderStorageImageExtendedFormats,
                SamplerAnisotropy = _vulkanPhysicalDeviceFeatures.SamplerAnisotropy
            };

            // Creating Vulkan device.");
            Vk.DeviceQueueCreateInfo deviceQueueCreateInfo = new Vk.DeviceQueueCreateInfo(
                Graphics.GraphicsQueueFamily,
                1,
                new float[] { 0f }
            );

            StringUtf8Array deviceExtensions = new StringUtf8Array(2);
            deviceExtensions[0] = Vk.SwapchainExtensionName.ToUtf8();
            deviceExtensions[1] = Vk.DebugMarkerExtensionName.ToUtf8();

            Vk.DeviceCreateInfo deviceCreateInfo = new Vk.DeviceCreateInfo(
                deviceQueueCreateInfo,
                1,
                deviceExtensions.ArrayPtr,
                physicalDeviceFeatures
            );
#if DEBUG
            if (foundDebugMarkerExtension)
            {
                deviceCreateInfo.EnabledExtensionCount = 2;
            }
#endif
            vkCreateDevice(_vulkanPhysicalDevice, ref deviceCreateInfo, null, out Graphics.Device).CheckError();

            if (Graphics.Device == Vk.Device.Null)
            {
                Log.Error("Vulkan device is null.");
            }

            // Loading Vulkan device functions.");
            vkGetDeviceQueue = vkGetDeviceQueue ?? Vk.LoadDeviceFunction<Vk.GetDeviceQueueDelegate>(Graphics.Device);
            vkCreateCommandPool = vkCreateCommandPool ?? Vk.LoadDeviceFunction<Vk.CreateCommandPoolDelegate>(Graphics.Device);
            vkAllocateCommandBuffers = vkAllocateCommandBuffers ?? Vk.LoadDeviceFunction<Vk.AllocateCommandBuffersDelegate>(Graphics.Device);
            vkCreateFence = vkCreateFence ?? Vk.LoadDeviceFunction<Vk.CreateFenceDelegate>(Graphics.Device);
            vkCreateSemaphore = vkCreateSemaphore ?? Vk.LoadDeviceFunction<Vk.CreateSemaphoreDelegate>(Graphics.Device);

            vkCreateSwapchain = vkCreateSwapchain ?? Vk.LoadDeviceFunction<Vk.CreateSwapchainKHRDelegate>(Graphics.Device);
            vkDestroySwapchain = vkDestroySwapchain ?? Vk.LoadDeviceFunction<Vk.DestroySwapchainKHRDelegate>(Graphics.Device);
            vkGetSwapchainImages = vkGetSwapchainImages ?? Vk.LoadDeviceFunction<Vk.GetSwapchainImagesKHRDelegate>(Graphics.Device);
            vkAcquireNextImage = vkAcquireNextImage ?? Vk.LoadDeviceFunction<Vk.AcquireNextImageKHRDelegate>(Graphics.Device);
            vkQueuePresent = vkQueuePresent ?? Vk.LoadDeviceFunction<Vk.QueuePresentKHRDelegate>(Graphics.Device);
#if DEBUG
            if (foundDebugMarkerExtension)
            {
                Log.Info($"Using {Vk.DebugMarkerExtensionName}.");
                vkDebugMarkerSetObjectName = vkDebugMarkerSetObjectName ?? Vk.LoadDeviceFunction<Vk.DebugMarkerSetObjectNameEXTDelegate>(Graphics.Device);
            }
#endif
            if (Graphics.DedicatedAllocation)
            {
                Log.Info($"Using {Vk.DedicatedAllocationExtensionName}.");
            }

            // Getting graphics queue.");
            vkGetDeviceQueue(Graphics.Device, Graphics.GraphicsQueueFamily, 0, out Graphics.GraphicsQueue);

            // Finding color buffer format.");
            Vk.FormatProperties formatProperties;
            Graphics.ColorFormat = Vk.Format.R8G8B8A8UNorm;

            if (_vulkanPhysicalDeviceFeatures.ShaderStorageImageExtendedFormats)
            {
                vkGetPhysicalDeviceFormatProperties(_vulkanPhysicalDevice, Vk.Format.A2B10G10R10UNormPack32, out formatProperties);
                bool a2b10g10r10Support = (formatProperties.OptimalTilingFeatures & RequiredColorBufferFeatures) == RequiredColorBufferFeatures;

                if (a2b10g10r10Support)
                {
                    // Using A2B10G10R10 color buffer format.");
                    Graphics.ColorFormat = Vk.Format.A2B10G10R10UNormPack32;
                }
            }

            // Finding depth buffer format.");
            vkGetPhysicalDeviceFormatProperties(_vulkanPhysicalDevice, Vk.Format.X8D24UNormPack32, out formatProperties);
            bool x8d24Support = (formatProperties.OptimalTilingFeatures & Vk.FormatFeatureFlags.DepthStencilAttachment) != 0;
            vkGetPhysicalDeviceFormatProperties(_vulkanPhysicalDevice, Vk.Format.D32SFloat, out formatProperties);
            bool d32Support = (formatProperties.OptimalTilingFeatures & Vk.FormatFeatureFlags.DepthStencilAttachment) != 0;

            Graphics.DepthFormat = Vk.Format.D16UNorm;
            if (x8d24Support)
            {
                // Using X8_D24 depth buffer format.");
                Graphics.DepthFormat = Vk.Format.X8D24UNormPack32;
            }
            else if (d32Support)
            {
                // Using D32 depth buffer format.");
                Graphics.DepthFormat = Vk.Format.D32SFloat;
            }
        }

        private void InitCommandBuffers()
        {
            // Creating Vulkan command pool.");
            Vk.CommandPoolCreateInfo commandPoolCreateInfo = new Vk.CommandPoolCreateInfo(
                Vk.CommandPoolCreateFlags.ResetCommandBuffer,
                Graphics.GraphicsQueueFamily
            );

            vkCreateCommandPool(Graphics.Device, ref commandPoolCreateInfo, null, out _commandPool).CheckError();

            commandPoolCreateInfo.Flags = Vk.CommandPoolCreateFlags.Transient;
            vkCreateCommandPool(Graphics.Device, ref commandPoolCreateInfo, null, out _transientCommandPool).CheckError();

            // Creating Vulkan command buffers.");
            Vk.CommandBufferAllocateInfo commandBufferAllocateInfo = new Vk.CommandBufferAllocateInfo(_commandPool, CommandBufferCount);

            vkAllocateCommandBuffers(Graphics.Device, ref commandBufferAllocateInfo, _commandBuffers).CheckError();

            // Creating Vulkan command buffer fences and semaphores.");
            Vk.FenceCreateInfo fenceCreateInfo = new Vk.FenceCreateInfo(Vk.FenceCreateFlags.None);

            for (int i = 0; i < CommandBufferCount; ++i)
            {
                vkCreateFence(Graphics.Device, ref fenceCreateInfo, null, out _commandBufferFences[i]).CheckError();

                Vk.SemaphoreCreateInfo semaphoreCreateInfo = new Vk.SemaphoreCreateInfo(Vk.SemaphoreCreateFlags.None);

                vkCreateSemaphore(Graphics.Device, ref semaphoreCreateInfo, null, out _drawCompleteSemaphores[i]).CheckError();
            }
        }

        private void CreateSwapchain()
        {
            // Creating Vulkan swapchain.");

            vkGetPhysicalDeviceSurfaceCapabilities(_vulkanPhysicalDevice, _vulkanSurface, out _vulkanSurfaceCapabilities).CheckError();

            //if (_vulkanSurfaceCapabilities.CurrentExtent.Width != )
        }

        private void CreateColorBuffer()
        {

        }

        private void CreateDepthBuffer()
        {

        }

        private void CreateRenderPasses()
        {

        }

        private void CreateFramebuffers()
        {

        }

        private void InitStagingBuffers()
        {

        }

        private void CreateDescriptorSetLayouts()
        {

        }

        private void CreateDescriptorPool()
        {

        }

        private void InitDynamicBuffers()
        {

        }

        private void InitSamplers()
        {

        }

        private void CreatePipelineLayouts()
        {

        }

        private void CreatePipelines()
        {

        }

        private void CreateDescriptorSets()
        {

        }

        // TODO: Dispose of Vulkan stuff
        private void Dispose(bool isDisposing)
        {
            if (_sdlWindow == IntPtr.Zero)
            {
                return;
            }

            SDL.DestroyWindow(_sdlWindow);
            _sdlWindow = IntPtr.Zero;
            _handle = IntPtr.Zero;
#if PLATFORM_LINUX
            _display = IntPtr.Zero;
#endif
        }

        ~Window()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}