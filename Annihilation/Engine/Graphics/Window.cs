//using System;
//using System.Runtime.InteropServices;
//using SDL2;
//using Vulkan;

//namespace Engine.Graphics
//{
//    public unsafe class Window : IDisposable
//    {
//        public const int DefaultWidth = 1280;
//        public const int DefaultHeight = 720;
//        private const VkFormatFeatureFlags RequiredColorBufferFeatures =
//            VkFormatFeatureFlags.ColorAttachment |
//            VkFormatFeatureFlags.ColorAttachmentBlend |
//            VkFormatFeatureFlags.SampledImage |
//            VkFormatFeatureFlags.StorageImage |
//            VkFormatFeatureFlags.SampledImageFilterLinear;
//        private const int CommandBufferCount = 2;
//        private const int ColorBufferCount = 2;
//        private const int MaxSwapchainImages = 8;

//        public static bool VideoSubSystemInitialized { get; private set; }

//        private static Vk.CreateInstanceDelegate vkCreateInstance;
//        private static Vk.DestroyInstanceDelegate vkDestroyInstance;

//        private static Vk.EnumeratePhysicalDevicesDelegate vkEnumeratePhysicalDevices;
//        private static Vk.EnumerateDeviceExtensionPropertiesDelegate vkEnumerateDeviceExtensionProperties;

//        private static Vk.GetPhysicalDevicePropertiesDelegate vkGetPhysicalDeviceProperties;
//        private static Vk.GetPhysicalDeviceMemoryPropertiesDelegate vkGetPhysicalDeviceMemoryProperties;
//        private static Vk.GetPhysicalDeviceQueueFamilyPropertiesDelegate vkGetPhysicalDeviceQueueFamilyProperties;
//        private static Vk.GetPhysicalDeviceFeaturesDelegate vkGetPhysicalDeviceFeatures;
//        private static Vk.GetPhysicalDeviceFormatPropertiesDelegate vkGetPhysicalDeviceFormatProperties;

//        private static Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate vkGetPhysicalDeviceSurfaceSupport;
//        private static Vk.GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate vkGetPhysicalDeviceSurfaceCapabilities;
//        private static Vk.GetPhysicalDeviceSurfaceFormatsKHRDelegate vkGetPhysicalDeviceSurfaceFormats;
//        private static Vk.GetPhysicalDeviceSurfacePresentModesKHRDelegate vkGetPhysicalDeviceSurfacePresentModes;

//        private static Vk.CreateDeviceDelegate vkCreateDevice;

//        private static Vk.GetDeviceQueueDelegate vkGetDeviceQueue;
//        private static Vk.CreateCommandPoolDelegate vkCreateCommandPool;
//        private static Vk.AllocateCommandBuffersDelegate vkAllocateCommandBuffers;
//        private static Vk.CreateFenceDelegate vkCreateFence;
//        private static Vk.CreateSemaphoreDelegate vkCreateSemaphore;

//        private static Vk.CreateSwapchainKHRDelegate vkCreateSwapchain;
//        private static Vk.DestroySwapchainKHRDelegate vkDestroySwapchain;
//        private static Vk.GetSwapchainImagesKHRDelegate vkGetSwapchainImages;
//        private static Vk.AcquireNextImageKHRDelegate vkAcquireNextImage;
//        private static Vk.QueuePresentKHRDelegate vkQueuePresent;
//#if DEBUG
//        private static Vk.CreateDebugReportCallbackEXTDelegate vkCreateDebugReportCallback;
//        private static Vk.DestroyDebugReportCallbackEXTDelegate vkDestroyDebugReportCallback;
//        private static Vk.DebugMarkerSetObjectNameEXTDelegate vkDebugMarkerSetObjectName;
//#endif

//        public bool HasFocus { get; set; }
//        public int Width { get; private set; }
//        public int Height { get; private set; }
        
//        private IntPtr _handle;
//#if PLATFORM_LINUX
//        private IntPtr _display;
//#endif
//        private SDL.Window _sdlWindow;
//        private VkInstance _vulkanInstance;
//        private VkSurface _vulkanSurface;
//        private VkPhysicalDevice _vulkanPhysicalDevice;
//        private VkPhysicalDeviceFeatures _vulkanPhysicalDeviceFeatures;

//        private VkCommandPool _commandPool;
//        private VkCommandPool _transientCommandPool;
//        private VkCommandBuffer[] _commandBuffers = new VkCommandBuffer[CommandBufferCount];
//        private VkFence[] _commandBufferFences = new VkFence[CommandBufferCount];
//        private VkSemaphore[] _drawCompleteSemaphores = new VkSemaphore[CommandBufferCount];

//        private VkSurfaceCapabilities _vulkanSurfaceCapabilities;
//        private VkSwapchain _vulkanSwapchain;

//        private bool[] _isCommandBufferSubmitted = new bool[CommandBufferCount];
//        private uint _currentSwapchainBuffer;
//#if DEBUG
//        private VkDebugReportCallback _debugReportCallback;

//        private static VkBool32 DebugMessageCallback(VkDebugReportFlags flags, VkDebugReportObjectType objectType, ulong @object, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData)
//        {
//            if (objectType == VkDebugReportObjectType.DebugReportCallback && messageCode == 1)
//            {
//                return false;
//            }

//            string output = "[" + Utf8.ToString(layerPrefix) + "] Code " + messageCode + ": " + Utf8.ToString(message);

//            switch (flags)
//            {
//                case VkDebugReportFlags.Information: Log.Info(output); return false;
//                case VkDebugReportFlags.Warning: Log.Warning(output); return false;
//                case VkDebugReportFlags.PerformanceWarning: Log.Performance(output); return false;
//                case VkDebugReportFlags.Error: Log.Error(output); return true;
//                case VkDebugReportFlags.Debug: Log.Info(output); return false;
//                default: return false;
//            }
//        }
//#endif

//        public Window(byte* title)
//        {
//            // Initializing SDL video subsystem.
//            if (VideoSubSystemInitialized == false)
//            {
//                SDL.InitSubSystem(SDL.InitFlags.Video).CheckError();
//                VideoSubSystemInitialized = true;
//            }

//            // Loading Vulkan loader library.
//            SDL.VulkanLoadLibrary(null).CheckError();

//            // Creating SDL window.
//            _sdlWindow = SDL.CreateWindow(
//                title,
//                SDL.WindowPositionCentered,
//                SDL.WindowPositionCentered,
//                GraphicsSystem.DisplayWidth,
//                GraphicsSystem.DisplayHeight,
//                SDL.WindowFlags.Shown | SDL.WindowFlags.Vulkan
//            );
//            _sdlWindow.CheckError();

//            // Getting window manager info.
//            SDL.SysWMInfo sysWMInfo = default(SDL.SysWMInfo);
//            SDL.GetVersion(out sysWMInfo.Version);
//            SDL.GetWindowWMInfo(_sdlWindow, ref sysWMInfo).CheckError();

//#if PLATFORM_WINDOWS
//            _handle = sysWMInfo.Info.Windows.Window;
//#elif PLATFORM_LINUX
//            switch(sysWMInfo.SubSystem)
//            {
//                case SDL.SysWMType.X11:
//                    Handle = sysWMInfo.Info.X11.Window;
//                    Display = sysWMInfo.Info.X11.Display;
//                    break;
//                case SDL.SysWMType.Wayland:
//                    Handle = sysWMInfo.Info.Wayland.Surface;
//                    Display = sysWMInfo.Info.Wayland.Display;
//                    break;
//                case SDL.SysWMType.Mir:
//                    Handle = sysWMInfo.Info.Mir.Connection;
//                    Display = sysWMInfo.Info.Mir.Surface;
//                    break;
//            }
//#elif PLATFORM_MACOS
//            Handle = sysWMInfo.Info.Cocoa.Window;
//#endif
//            // Initializing Vulkan.
//            InitInstance(title);
//            InitDevice();
//            InitCommandBuffers();
//            CreateSwapchain();
//            CreateColorBuffer();
//            CreateDepthBuffer();
//            CreateRenderPasses();
//            CreateFramebuffers();
//            InitStagingBuffers();
//            CreateDescriptorSetLayouts();
//            CreateDescriptorPool();
//            InitDynamicBuffers();
//            InitSamplers();
//            CreatePipelineLayouts();
//            CreatePipelines();
//            CreateDescriptorSets();
//        }

//        public void Show()
//        {
//            SDL.ShowWindow(_sdlWindow);
//        }

//        public void SetMode(int width, int height, int refreshRate, int bpp, bool fullscreen)
//        {
//            // Creating SDL window.
//            if (_sdlWindow.Handle == IntPtr.Zero)
//            {
//                SDL.WindowFlags flags = SDL.WindowFlags.Hidden | SDL.WindowFlags.Vulkan;

//                //if (VariableManager.GetVar(""))
//            }
//        }

//        private void GetCurrentSize(out int width, out int height)
//        {
//            SDL.VulkanGetDrawableSize(_sdlWindow, out width, out height);
//        }

//        private int GetCurrentWidth()
//        {
//            SDL.VulkanGetDrawableSize(_sdlWindow, out int width, out int height);
//            return width;
//        }

//        private int GetCurrentHeight()
//        {
//            SDL.VulkanGetDrawableSize(_sdlWindow, out int width, out int height);
//            return height;
//        }

//        private void InitInstance(byte* title)
//        {
//            // Loading Vulkan getInstanceProcAddr function.
//            Vk.GetInstanceProcAddr = Vk.GetInstanceProcAddr ?? Marshal.GetDelegateForFunctionPointer<Vk.GetInstanceProcAddrDelegate>(SDL.VulkanGetVkGetInstanceProcAddr());

//            // Getting required Vulkan instance extensions for surface.
//            uint extensionCount = 0;
//            SDL.VulkanGetInstanceExtensions(_sdlWindow, ref extensionCount, null).CheckError();

//            // TODO: Free those
//            StringUtf8Array extensionNames = new StringUtf8Array(extensionCount + 1);
//            SDL.VulkanGetInstanceExtensions(_sdlWindow, ref extensionCount, extensionNames.ArrayPtr).CheckError();

//#if DEBUG
//            bool enableValidation = VariableSystem.GetVar(Application.ConfigEnableVulkanValidation, true);
//            if (enableValidation)
//            {
//                extensionNames[extensionCount] = Vk.ExtDebugReportExtensionName.ToUtf8();
//            }
//#endif
//            // Loading Vulkan createInstance function.
//            vkCreateInstance = vkCreateInstance ?? Vk.LoadGlobalFunction<Vk.CreateInstanceDelegate>();

//            // Creating Vulkan instance.
//            VkApplicationInfo applicationInfo = new VkApplicationInfo(
//                title,
//                Vulkan.Version.One,
//                title,
//                Vulkan.Version.One,
//                Vulkan.Version.One
//            );

//            VkInstanceCreateInfo instanceCreateInfo = new VkInstanceCreateInfo(
//                &applicationInfo,
//                extensionCount,
//                extensionNames.ArrayPtr
//            );
//#if DEBUG
//            if (enableValidation)
//            {
//                // TODO: Free these
//                StringUtf8Array layerNames = new StringUtf8Array(1);
//                layerNames[0] = "VK_LAYER_LUNARG_standard_validation".ToUtf8();

//                instanceCreateInfo.EnabledExtensionCount = extensionCount + 1;
//                instanceCreateInfo.EnabledLayerCount = 1;
//                instanceCreateInfo.EnabledLayerNames = layerNames.ArrayPtr;
//            }
//#endif
//            vkCreateInstance(ref instanceCreateInfo, null, out _vulkanInstance).CheckError();

//            if (_vulkanInstance == Vk.VkInstance.Null)
//            {
//                Log.Error("Vulkan instance is null.");
//            }

//            // Creating Vulkan surface.
//            SDL.VulkanCreateSurface(_sdlWindow, _vulkanInstance, out _vulkanSurface).CheckError();

//            // Loading Vulkan instance functions.
//            Vk.GetDeviceProcAddr = Vk.GetDeviceProcAddr ?? Vk.LoadInstanceFunction<Vk.GetDeviceProcAddrDelegate>(_vulkanInstance);

//            vkDestroyInstance = vkDestroyInstance ?? Vk.LoadInstanceFunction<Vk.DestroyInstanceDelegate>(_vulkanInstance);

//            vkEnumeratePhysicalDevices = vkEnumeratePhysicalDevices ?? Vk.LoadInstanceFunction<Vk.EnumeratePhysicalDevicesDelegate>(_vulkanInstance);
//            vkEnumerateDeviceExtensionProperties = vkEnumerateDeviceExtensionProperties ?? Vk.LoadInstanceFunction<Vk.EnumerateDeviceExtensionPropertiesDelegate>(_vulkanInstance);

//            vkGetPhysicalDeviceProperties = vkGetPhysicalDeviceProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDevicePropertiesDelegate>(_vulkanInstance);
//            vkGetPhysicalDeviceMemoryProperties = vkGetPhysicalDeviceMemoryProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceMemoryPropertiesDelegate>(_vulkanInstance);
//            vkGetPhysicalDeviceQueueFamilyProperties = vkGetPhysicalDeviceQueueFamilyProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceQueueFamilyPropertiesDelegate>(_vulkanInstance);
//            vkGetPhysicalDeviceFeatures = vkGetPhysicalDeviceFeatures ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceFeaturesDelegate>(_vulkanInstance);
//            vkGetPhysicalDeviceFormatProperties = vkGetPhysicalDeviceFormatProperties ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceFormatPropertiesDelegate>(_vulkanInstance);

//            vkGetPhysicalDeviceSurfaceSupport = vkGetPhysicalDeviceSurfaceSupport ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate>(_vulkanInstance);
//            vkGetPhysicalDeviceSurfaceCapabilities = vkGetPhysicalDeviceSurfaceCapabilities ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate>(_vulkanInstance);
//            vkGetPhysicalDeviceSurfaceFormats = vkGetPhysicalDeviceSurfaceFormats ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceFormatsKHRDelegate>(_vulkanInstance);
//            vkGetPhysicalDeviceSurfacePresentModes = vkGetPhysicalDeviceSurfacePresentModes ?? Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfacePresentModesKHRDelegate>(_vulkanInstance);

//            vkCreateDevice = vkCreateDevice ?? Vk.LoadInstanceFunction<Vk.CreateDeviceDelegate>(_vulkanInstance);

//            vkGetSwapchainImages = vkGetSwapchainImages ?? Vk.LoadInstanceFunction<Vk.GetSwapchainImagesKHRDelegate>(_vulkanInstance);

//#if DEBUG
//            if (enableValidation)
//            {
//                vkCreateDebugReportCallback = Vk.LoadInstanceFunction<Vk.CreateDebugReportCallbackEXTDelegate>(_vulkanInstance);
//                vkDestroyDebugReportCallback = Vk.LoadInstanceFunction<Vk.DestroyDebugReportCallbackEXTDelegate>(_vulkanInstance);

//                // Creating Vulkan debug report callback.
//                Vk.DebugReportCallbackCreateInfo debugReportCallbackCreateInfo = new Vk.DebugReportCallbackCreateInfo(
//                    Vk.DebugReportFlags.Error | Vk.DebugReportFlags.Warning | Vk.DebugReportFlags.PerformanceWarning | Vk.DebugReportFlags.Information,
//                    DebugMessageCallback
//                );

//                vkCreateDebugReportCallback(_vulkanInstance, ref debugReportCallbackCreateInfo, null, out _debugReportCallback).CheckError();
//            }
//#endif

//            // TODO: Free allocated Texts?
//        }

//        private void InitDevice()
//        {
//            // Enumerating physical devices.
//            uint physicalDeviceCount = 0;
//            vkEnumeratePhysicalDevices(_vulkanInstance, ref physicalDeviceCount, null).CheckError();

//            if (physicalDeviceCount == 0)
//            {
//                Log.Error("No Vulkan physical device found.");
//            }

//            // TODO: This should be user-configurable through command line args and settings
//            int deviceIndex = 0;
//            Vk.VkPhysicalDevice[] physicalDevices = new Vk.VkPhysicalDevice[(int)physicalDeviceCount];
//            vkEnumeratePhysicalDevices(_vulkanInstance, ref physicalDeviceCount, physicalDevices).CheckError();

//            _vulkanPhysicalDevice = physicalDevices[deviceIndex];

//            if (_vulkanPhysicalDevice == Vk.VkPhysicalDevice.Null)
//            {
//                Log.Error("Vulkan physical device is null.");
//            }

//            // Getting device memory properties.
//            vkGetPhysicalDeviceMemoryProperties(_vulkanPhysicalDevice, out GraphicsSystem.DeviceMemoryProperties);

//            // Enumerating device extensions.
//            uint deviceExtensionCount = 0;
//            vkEnumerateDeviceExtensionProperties(_vulkanPhysicalDevice, null, ref deviceExtensionCount, null).CheckError();

//            if (deviceExtensionCount == 0)
//            {
//                Log.Error("No device extensions found.");
//            }

//            Vk.ExtensionProperties[] extensionProperties = new Vk.ExtensionProperties[deviceExtensionCount];
//            vkEnumerateDeviceExtensionProperties(_vulkanPhysicalDevice, null, ref deviceExtensionCount, extensionProperties).CheckError();

//            bool foundSwapchainExtension = false;
//#if DEBUG
//            bool foundDebugMarkerExtension = false;
//#endif
//            GraphicsSystem.DedicatedAllocation = false;

//            foreach (Vk.ExtensionProperties extension in extensionProperties)
//            {
//                if (extension.IsNamed(Vk.SwapchainExtensionName))
//                {
//                    foundSwapchainExtension = true;
//                }
//#if DEBUG
//                else if (extension.IsNamed(Vk.DebugMarkerExtensionName))
//                {
//                    foundDebugMarkerExtension = true;
//                }
//#endif
//                else if (extension.IsNamed(Vk.DedicatedAllocationExtensionName))
//                {
//                    GraphicsSystem.DedicatedAllocation = true;
//                }
//            }

//            if (!foundSwapchainExtension)
//            {
//                Log.Error($"Couldn't find {Vk.SwapchainExtensionName} extension.");
//            }

//            // Getting device properties.
//            vkGetPhysicalDeviceProperties(_vulkanPhysicalDevice, out GraphicsSystem.DeviceProperties);
//            Log.Info($"Device: {GraphicsSystem.VendorNames[GraphicsSystem.DeviceProperties.VendorId]} {GraphicsSystem.DeviceProperties.GetDeviceName()}");

//            // Getting queue family properties.
//            uint queueFamilyCount = 0;
//            vkGetPhysicalDeviceQueueFamilyProperties(_vulkanPhysicalDevice, ref queueFamilyCount, null);

//            if (queueFamilyCount == 0)
//            {
//                Log.Error("No Vulkan queues found.");
//            }

//            Vk.QueueFamilyProperties[] queueFamilyProperties = new Vk.QueueFamilyProperties[queueFamilyCount];
//            vkGetPhysicalDeviceQueueFamilyProperties(_vulkanPhysicalDevice, ref queueFamilyCount, queueFamilyProperties);

//            // Finding queues with present support.
//            Vk.Bool32[] queueSupportsPresent = new Vk.Bool32[queueFamilyCount];
//            for (uint i = 0; i < queueFamilyCount; ++i)
//            {
//                vkGetPhysicalDeviceSurfaceSupport(_vulkanPhysicalDevice, i, _vulkanSurface, out queueSupportsPresent[i]);
//            }

//            // Finding queue with graphics and present support.
//            bool foundGraphicsQueue = false;

//            for (uint i = 0; i < queueFamilyCount; ++i)
//            {
//                if ((queueFamilyProperties[i].QueueFlags & Vk.QueueFlags.Graphics) != 0 && queueSupportsPresent[i])
//                {
//                    foundGraphicsQueue = true;
//                    GraphicsSystem.GraphicsQueueFamily = i;
//                }
//            }

//            if (!foundGraphicsQueue)
//            {
//                Log.Error("No graphics+present queue found.");
//            }

//            // TODO: Find exclusive compute and transfer queues

//            // Getting Vulkan physical device features.
//            vkGetPhysicalDeviceFeatures(_vulkanPhysicalDevice, out _vulkanPhysicalDeviceFeatures);

//            Vk.PhysicalDeviceFeatures physicalDeviceFeatures = new Vk.PhysicalDeviceFeatures
//            {
//                ShaderStorageImageExtendedFormats = _vulkanPhysicalDeviceFeatures.ShaderStorageImageExtendedFormats,
//                SamplerAnisotropy = _vulkanPhysicalDeviceFeatures.SamplerAnisotropy
//            };

//            // Creating Vulkan device.
//            Vk.DeviceQueueCreateInfo deviceQueueCreateInfo = new Vk.DeviceQueueCreateInfo(
//                GraphicsSystem.GraphicsQueueFamily,
//                1,
//                new float[] { 0f }
//            );

//            StringUtf8Array deviceExtensions = new StringUtf8Array(2);
//            deviceExtensions[0] = Vk.SwapchainExtensionName.ToUtf8();
//            deviceExtensions[1] = Vk.DebugMarkerExtensionName.ToUtf8();

//            Vk.DeviceCreateInfo deviceCreateInfo = new Vk.DeviceCreateInfo(
//                deviceQueueCreateInfo,
//                1,
//                deviceExtensions.ArrayPtr,
//                physicalDeviceFeatures
//            );
//#if DEBUG
//            if (foundDebugMarkerExtension)
//            {
//                deviceCreateInfo.EnabledExtensionCount = 2;
//            }
//#endif
//            vkCreateDevice(_vulkanPhysicalDevice, ref deviceCreateInfo, null, out GraphicsSystem.Device).CheckError();

//            if (GraphicsSystem.Device == Vk.VkDevice.Null)
//            {
//                Log.Error("Vulkan device is null.");
//            }

//            // Loading Vulkan device functions.
//            vkGetDeviceQueue = vkGetDeviceQueue ?? Vk.LoadDeviceFunction<Vk.GetDeviceQueueDelegate>(GraphicsSystem.Device);
//            vkCreateCommandPool = vkCreateCommandPool ?? Vk.LoadDeviceFunction<Vk.CreateCommandPoolDelegate>(GraphicsSystem.Device);
//            vkAllocateCommandBuffers = vkAllocateCommandBuffers ?? Vk.LoadDeviceFunction<Vk.AllocateCommandBuffersDelegate>(GraphicsSystem.Device);
//            vkCreateFence = vkCreateFence ?? Vk.LoadDeviceFunction<Vk.CreateFenceDelegate>(GraphicsSystem.Device);
//            vkCreateSemaphore = vkCreateSemaphore ?? Vk.LoadDeviceFunction<Vk.CreateSemaphoreDelegate>(GraphicsSystem.Device);

//            vkCreateSwapchain = vkCreateSwapchain ?? Vk.LoadDeviceFunction<Vk.CreateSwapchainKHRDelegate>(GraphicsSystem.Device);
//            vkDestroySwapchain = vkDestroySwapchain ?? Vk.LoadDeviceFunction<Vk.DestroySwapchainKHRDelegate>(GraphicsSystem.Device);
//            vkGetSwapchainImages = vkGetSwapchainImages ?? Vk.LoadDeviceFunction<Vk.GetSwapchainImagesKHRDelegate>(GraphicsSystem.Device);
//            vkAcquireNextImage = vkAcquireNextImage ?? Vk.LoadDeviceFunction<Vk.AcquireNextImageKHRDelegate>(GraphicsSystem.Device);
//            vkQueuePresent = vkQueuePresent ?? Vk.LoadDeviceFunction<Vk.QueuePresentKHRDelegate>(GraphicsSystem.Device);
//#if DEBUG
//            if (foundDebugMarkerExtension)
//            {
//                Log.Info($"Using {Vk.DebugMarkerExtensionName}.");
//                vkDebugMarkerSetObjectName = vkDebugMarkerSetObjectName ?? Vk.LoadDeviceFunction<Vk.DebugMarkerSetObjectNameEXTDelegate>(GraphicsSystem.Device);
//            }
//#endif
//            if (GraphicsSystem.DedicatedAllocation)
//            {
//                Log.Info($"Using {Vk.DedicatedAllocationExtensionName}.");
//            }

//            // Getting graphics queue.
//            vkGetDeviceQueue(GraphicsSystem.Device, GraphicsSystem.GraphicsQueueFamily, 0, out GraphicsSystem.GraphicsQueue);

//            // Finding color buffer format.
//            Vk.FormatProperties formatProperties;
//            GraphicsSystem.ColorFormat = Vk.Format.R8G8B8A8UNorm;

//            if (_vulkanPhysicalDeviceFeatures.ShaderStorageImageExtendedFormats)
//            {
//                vkGetPhysicalDeviceFormatProperties(_vulkanPhysicalDevice, Vk.Format.A2B10G10R10UNormPack32, out formatProperties);
//                bool a2b10g10r10Support = (formatProperties.OptimalTilingFeatures & RequiredColorBufferFeatures) == RequiredColorBufferFeatures;

//                if (a2b10g10r10Support)
//                {
//                    // Using A2B10G10R10 color buffer format.
//                    GraphicsSystem.ColorFormat = Vk.Format.A2B10G10R10UNormPack32;
//                }
//            }

//            // Finding depth buffer format.
//            vkGetPhysicalDeviceFormatProperties(_vulkanPhysicalDevice, Vk.Format.X8D24UNormPack32, out formatProperties);
//            bool x8d24Support = (formatProperties.OptimalTilingFeatures & Vk.FormatFeatureFlags.DepthStencilAttachment) != 0;
//            vkGetPhysicalDeviceFormatProperties(_vulkanPhysicalDevice, Vk.Format.D32SFloat, out formatProperties);
//            bool d32Support = (formatProperties.OptimalTilingFeatures & Vk.FormatFeatureFlags.DepthStencilAttachment) != 0;

//            GraphicsSystem.DepthFormat = Vk.Format.D16UNorm;
//            if (x8d24Support)
//            {
//                // Using X8_D24 depth buffer format.
//                GraphicsSystem.DepthFormat = Vk.Format.X8D24UNormPack32;
//            }
//            else if (d32Support)
//            {
//                // Using D32 depth buffer format.
//                GraphicsSystem.DepthFormat = Vk.Format.D32SFloat;
//            }
//        }

//        private void InitCommandBuffers()
//        {
//            // Creating Vulkan command pool.
//            Vk.CommandPoolCreateInfo commandPoolCreateInfo = new Vk.CommandPoolCreateInfo(
//                Vk.CommandPoolCreateFlags.ResetCommandBuffer,
//                GraphicsSystem.GraphicsQueueFamily
//            );

//            vkCreateCommandPool(GraphicsSystem.Device, ref commandPoolCreateInfo, null, out _commandPool).CheckError();

//            commandPoolCreateInfo.Flags = Vk.CommandPoolCreateFlags.Transient;
//            vkCreateCommandPool(GraphicsSystem.Device, ref commandPoolCreateInfo, null, out _transientCommandPool).CheckError();

//            // Creating Vulkan command buffers.
//            Vk.CommandBufferAllocateInfo commandBufferAllocateInfo = new Vk.CommandBufferAllocateInfo(_commandPool, CommandBufferCount);

//            vkAllocateCommandBuffers(GraphicsSystem.Device, ref commandBufferAllocateInfo, _commandBuffers).CheckError();

//            // Creating Vulkan command buffer fences and semaphores.
//            Vk.FenceCreateInfo fenceCreateInfo = new Vk.FenceCreateInfo(Vk.FenceCreateFlags.None);

//            for (int i = 0; i < CommandBufferCount; ++i)
//            {
//                vkCreateFence(GraphicsSystem.Device, ref fenceCreateInfo, null, out _commandBufferFences[i]).CheckError();

//                Vk.SemaphoreCreateInfo semaphoreCreateInfo = new Vk.SemaphoreCreateInfo(Vk.SemaphoreCreateFlags.None);

//                vkCreateSemaphore(GraphicsSystem.Device, ref semaphoreCreateInfo, null, out _drawCompleteSemaphores[i]).CheckError();
//            }
//        }

//        private void CreateSwapchain()
//        {
//            // Creating Vulkan swapchain.

//            vkGetPhysicalDeviceSurfaceCapabilities(_vulkanPhysicalDevice, _vulkanSurface, out _vulkanSurfaceCapabilities).CheckError();

//            //if (_vulkanSurfaceCapabilities.CurrentExtent.Width != )
//        }

//        private void CreateColorBuffer()
//        {

//        }

//        private void CreateDepthBuffer()
//        {

//        }

//        private void CreateRenderPasses()
//        {

//        }

//        private void CreateFramebuffers()
//        {

//        }

//        private void InitStagingBuffers()
//        {

//        }

//        private void CreateDescriptorSetLayouts()
//        {

//        }

//        private void CreateDescriptorPool()
//        {

//        }

//        private void InitDynamicBuffers()
//        {

//        }

//        private void InitSamplers()
//        {

//        }

//        private void CreatePipelineLayouts()
//        {

//        }

//        private void CreatePipelines()
//        {

//        }

//        private void CreateDescriptorSets()
//        {

//        }

//        // TODO: Dispose of Vulkan stuff
//        private void Dispose(bool isDisposing)
//        {
//            if (_sdlWindow == IntPtr.Zero)
//            {
//                return;
//            }

//            SDL.DestroyWindow(_sdlWindow);
//            _sdlWindow = IntPtr.Zero;
//            _handle = IntPtr.Zero;
//#if PLATFORM_LINUX
//            _display = IntPtr.Zero;
//#endif
//        }

//        ~Window()
//        {
//            Dispose(false);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}