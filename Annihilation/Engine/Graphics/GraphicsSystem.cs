using System;
using System.Runtime.InteropServices;
using Engine.Config;
using Vulkan;
using SDL2;

namespace Engine.Graphics
{
    public static unsafe class GraphicsSystem
    {
        //
        // Constants
        //
        private const int CommandBufferCount = 2;
        private const int ColorBufferCount = 2;
        private const int MaxSwapchainImages = 8;
        private const VkFormatFeatureFlags RequiredColorBufferFeatures = VkFormatFeatureFlags.ColorAttachment |
                                                                         VkFormatFeatureFlags.ColorAttachmentBlend |
                                                                         VkFormatFeatureFlags.SampledImage |
                                                                         VkFormatFeatureFlags.StorageImage |
                                                                         VkFormatFeatureFlags.SampledImageFilterLinear;
        //
        // Fields
        //
        public static int DisplayWidth = 1280;
        public static int DisplayHeight = 720;

        public static BoolVar EnableVSync = new BoolVar("Graphics/VSync", false);
        public static BoolVar EnableValidation = new BoolVar("Graphics/Validation", true);
        
        public static ulong FrameCount { get; private set; }
        
        private static VkInstance _instance;
        private static VkSurface _surface;
        private static VkPhysicalDevice _physicalDevice;
        private static VkPhysicalDeviceFeatures _physicalDeviceFeatures;
        private static VkCommandPool _commandPool;
        private static VkCommandPool _transientCommandPool;
        private static VkCommandBuffer* _commandBuffers;
        private static VkFence[] _commandBufferFences = new VkFence[CommandBufferCount];
        private static VkSemaphore[] _drawCompleteSemaphores = new VkSemaphore[CommandBufferCount];
        private static VkSurfaceCapabilities _surfaceCapabilities;
        private static VkSwapchain _swapchain;
        private static bool[] _isCommandBufferSubmitted = new bool[CommandBufferCount];
        private static uint _currentSwapchainBuffer;
        private static VkDevice _device;
        private static uint _graphicsQueueFamily;
        private static uint _computeQueueFamily;
        private static uint _transferQueueFamily;
        private static VkQueue _graphicsQueue;
        private static VkQueue _computeQueue;
        private static VkQueue _transferQueue;
        private static VkCommandBuffer _commandBuffer;
        private static VkPhysicalDeviceProperties _deviceProperties;
        private static VkPhysicalDeviceMemoryProperties _deviceMemoryProperties;
        private static VkFormat _colorFormat;
        private static VkFormat _depthFormat;
        private static VkSampleCountFlags _sampleCount;
        private static bool _supersampling;
        private static bool _dedicatedAllocation;
#if DEBUG
        private static VkDebugReportCallback _debugReportCallback;
#endif

        // External function
        private static VkGetInstanceProcAddr _vkGetInstanceProcAddr;

        // Global functions
        private static VkCreateInstance _vkCreateInstance;

        // Instance functions
        private static VkDestroyInstance _vkDestroyInstance;
        private static VkGetDeviceProcAddr _vkGetDeviceProcAddr;
        private static VkEnumeratePhysicalDevices _vkEnumeratePhysicalDevices;
        private static VkEnumerateDeviceExtensionProperties _vkEnumerateDeviceExtensionProperties;
        private static VkGetPhysicalDeviceProperties _vkGetPhysicalDeviceProperties;
        private static VkGetPhysicalDeviceMemoryProperties _vkGetPhysicalDeviceMemoryProperties;
        private static VkGetPhysicalDeviceQueueFamilyProperties _vkGetPhysicalDeviceQueueFamilyProperties;
        private static VkGetPhysicalDeviceFeatures _vkGetPhysicalDeviceFeatures;
        private static VkGetPhysicalDeviceFormatProperties _vkGetPhysicalDeviceFormatProperties;
        private static VkGetPhysicalDeviceSurfaceSupportKHR _vkGetPhysicalDeviceSurfaceSupportKHR;
        private static VkGetPhysicalDeviceSurfaceCapabilitiesKHR _vkGetPhysicalDeviceSurfaceCapabilitiesKHR;
        private static VkGetPhysicalDeviceSurfaceFormatsKHR _vkGetPhysicalDeviceSurfaceFormatsKHR;
        private static VkGetPhysicalDeviceSurfacePresentModesKHR _vkGetPhysicalDeviceSurfacePresentModesKHR;
        private static VkCreateDevice _vkCreateDevice;
#if DEBUG
        private static VkCreateDebugReportCallbackEXT _vkCreateDebugReportCallbackEXT;
        private static VkDestroyDebugReportCallbackEXT _vkDestroyDebugReportCallbackEXT;
#endif

        // Device functions
        private static VkGetDeviceQueue _vkGetDeviceQueue;
        private static VkCreateCommandPool _vkCreateCommandPool;
        private static VkAllocateCommandBuffers _vkAllocateCommandBuffers;
        private static VkCreateFence _vkCreateFence;
        private static VkCreateSemaphore _vkCreateSemaphore;
        private static VkCreateSwapchainKHR _vkCreateSwapchainKHR;
        private static VkDestroySwapchainKHR _vkDestroySwapchainKHR;
        private static VkGetSwapchainImagesKHR _vkGetSwapchainImagesKHR;
        private static VkAcquireNextImageKHR _vkAcquireNextImageKHR;
        private static VkQueuePresentKHR _vkQueuePresentKHR;
#if DEBUG
        private static VkDebugMarkerSetObjectNameEXT _vkDebugMarkerSetObjectNameEXT;
#endif

        public static void Initialize(ref byte* title, ref SDL.Window window)
        {
            Assert.IsTrue(_swapchain == VkSwapchain.Null, "Graphics system has already been initialized.");

            IntPtr funcPtr = SDL.VulkanGetVkGetInstanceProcAddr();
            funcPtr.CheckError();
            _vkGetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<VkGetInstanceProcAddr>(funcPtr);

            LoadGlobalFunctions();

            InitInstance(ref title, ref window);
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

        public static void Present()
        {
            FrameCount++;
        }

        public static void Resize(int width, int height)
        {

        }

        public static void Terminate()
        {

        }

        public static void Shutdown()
        {

        }

        private static void InitInstance(ref byte* title, ref SDL.Window window)
        {
            VkApplicationInfo applicationInfo = new VkApplicationInfo(
                title,
                Vulkan.Version.One,
                title,
                Vulkan.Version.One,
                Vulkan.Version.One
            );

            // Get required extensions
            uint requiredExtensionCount = 0;
            bool result = SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, null);
            Assert.IsTrue(result, Utf8.ToString(SDL.GetError()));

            uint desiredExtensionCount = requiredExtensionCount;
#if DEBUG
            desiredExtensionCount += 1;
#endif
            byte** extensionNames = (byte**)Memory.AllocatePointers((int)desiredExtensionCount);
            result = SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, extensionNames);
            Assert.IsTrue(result, Utf8.ToString(SDL.GetError()));

#if DEBUG
            if (EnableValidation)
            {
                extensionNames[requiredExtensionCount] = Utf8.AllocateFromAsciiString("VK_EXT_debug_report");
            }
#endif

            VkInstanceCreateInfo instanceCreateInfo = new VkInstanceCreateInfo(
                &applicationInfo,
                desiredExtensionCount,
                extensionNames
            );

#if DEBUG
            byte** layerNames = null;
            uint layerCount = 0;
            if (EnableValidation)
            {
                layerCount = 1;
                layerNames = (byte**)Memory.AllocatePointers(1);
                layerNames[0] = Utf8.AllocateFromAsciiString("VK_LAYER_LUNARG_standard_validation");

                instanceCreateInfo.EnabledLayerCount = layerCount;
                instanceCreateInfo.EnabledLayerNames = layerNames;
            }
#endif

            // Create the instance
            _vkCreateInstance(ref instanceCreateInfo, null, out _instance).CheckError();

            // Create the surface
            SDL.VulkanCreateSurface(window, _instance, out ulong surfaceHandle).CheckError();
            _surface = new VkSurface { Handle = surfaceHandle };

            LoadInstanceFunctions();

#if DEBUG
            if (EnableValidation)
            {
                // Create debug report callback
                VkDebugReportCallbackCreateInfo debugReportCallbackCreateInfo = new VkDebugReportCallbackCreateInfo(
                    VkDebugReportFlags.Error | VkDebugReportFlags.Warning | VkDebugReportFlags.PerformanceWarning,
                    DebugMessageCallback
                );

                _vkCreateDebugReportCallbackEXT(_instance, ref debugReportCallbackCreateInfo, null, out _debugReportCallback).CheckError();
            }
#endif

            // Free extension names
            Memory.Free(extensionNames);

#if DEBUG
            // Free layer names
            if (EnableValidation)
            {
                Memory.Free(layerNames);
            }
#endif
        }

        private static void InitDevice()
        {
            // Enumerate physical devices
            uint physicalDeviceCount = 0;
            _vkEnumeratePhysicalDevices(_instance, ref physicalDeviceCount, null).CheckError();

            if (physicalDeviceCount == 0)
            {
                Log.Error("No Vulkan physical device found.");
            }

            int deviceIndex = 0;
            VkPhysicalDevice* physicalDevices = (VkPhysicalDevice*)Marshal.AllocHGlobal((int)physicalDeviceCount * sizeof(VkPhysicalDevice));
            _vkEnumeratePhysicalDevices(_instance, ref physicalDeviceCount, physicalDevices).CheckError();

            _physicalDevice = physicalDevices[deviceIndex];
            Memory.Free(physicalDevices);

            if (_physicalDevice == VkPhysicalDevice.Null)
            {
                Log.Error("Vulkan physical device is null.");
            }

            // Get device memory properties
            _vkGetPhysicalDeviceMemoryProperties(_physicalDevice, out _deviceMemoryProperties);

            // Enumerate device extensions
            uint deviceExtensionCount = 0;
            _vkEnumerateDeviceExtensionProperties(_physicalDevice, null, ref deviceExtensionCount, null).CheckError();
            
            VkExtensionProperties* extensionProperties = (VkExtensionProperties*)Marshal.AllocHGlobal((int)deviceExtensionCount * sizeof(VkExtensionProperties));
            _vkEnumerateDeviceExtensionProperties(_physicalDevice, null, ref deviceExtensionCount, extensionProperties).CheckError();

            bool foundSwapchainExtension = false;
#if DEBUG
            bool foundDebugMarkerExtension = false;
#endif
            _dedicatedAllocation = false;

            byte* swapchainExtensionName = Utf8.AllocateFromAsciiString(Vk.SwapchainExtensionName);
            byte* dedicatedAllocationExtensionName = Utf8.AllocateFromAsciiString(Vk.DedicatedAllocationExtensionName);
#if DEBUG
            byte* debugMarkerExtensionName = Utf8.AllocateFromAsciiString(Vk.DebugMarkerExtensionName);
#endif
            string str = Utf8.ToString(extensionProperties[0].ExtensionName);

            for (int i = 0; i < deviceExtensionCount; ++i)
            {
                if (Utf8.Compare(extensionProperties[i].ExtensionName, swapchainExtensionName))
                {
                    foundSwapchainExtension = true;
                }
                if (Utf8.Compare(extensionProperties[i].ExtensionName, dedicatedAllocationExtensionName))
                {
                    _dedicatedAllocation = true;
                }
#if DEBUG
                if (Utf8.Compare(extensionProperties[i].ExtensionName, debugMarkerExtensionName))
                {
                    foundDebugMarkerExtension = true;
                }
#endif
            }
            Utf8.Free(swapchainExtensionName);
            Utf8.Free(dedicatedAllocationExtensionName);
#if DEBUG
            Utf8.Free(debugMarkerExtensionName);
#endif
            Memory.Free(extensionProperties);

            if (!foundSwapchainExtension)
            {
                Log.Error($"Couldn't find {Vk.SwapchainExtensionName} extension.");
            }

            // Getting device properties.
            _vkGetPhysicalDeviceProperties(_physicalDevice, out _deviceProperties);

            // Getting queue family properties.
            uint queueFamilyCount = 0;
            _vkGetPhysicalDeviceQueueFamilyProperties(_physicalDevice, ref queueFamilyCount, null);

            if (queueFamilyCount == 0)
            {
                Log.Error("No Vulkan queues found.");
            }

            VkQueueFamilyProperties* queueFamilyProperties = (VkQueueFamilyProperties*)Marshal.AllocHGlobal((int)queueFamilyCount * sizeof(VkQueueFamilyProperties));
            _vkGetPhysicalDeviceQueueFamilyProperties(_physicalDevice, ref queueFamilyCount, queueFamilyProperties);

            // Finding queues with present support.
            VkBool32[] queueSupportsPresent = new VkBool32[queueFamilyCount];
            for (uint i = 0; i < queueFamilyCount; ++i)
            {
                _vkGetPhysicalDeviceSurfaceSupportKHR(_physicalDevice, i, _surface, out queueSupportsPresent[i]);
            }

            // Finding queue with graphics and present support.
            bool foundGraphicsQueue = false;

            for (uint i = 0; i < queueFamilyCount; ++i)
            {
                if ((queueFamilyProperties[i].QueueFlags & VkQueueFlags.Graphics) != 0 && queueSupportsPresent[i])
                {
                    foundGraphicsQueue = true;
                    _graphicsQueueFamily = i;
                }
            }

            if (!foundGraphicsQueue)
            {
                Log.Error("No graphics+present queue found.");
            }

            // TODO: Find exclusive compute and transfer queues

            Memory.Free(queueFamilyProperties);

            // Getting Vulkan physical device features.
            _vkGetPhysicalDeviceFeatures(_physicalDevice, out _physicalDeviceFeatures);

            VkPhysicalDeviceFeatures physicalDeviceFeatures = new VkPhysicalDeviceFeatures
            {
                ShaderStorageImageExtendedFormats = _physicalDeviceFeatures.ShaderStorageImageExtendedFormats,
                SamplerAnisotropy = _physicalDeviceFeatures.SamplerAnisotropy
            };

            // Creating Vulkan device.
            float queuePriority = 0f;
            VkDeviceQueueCreateInfo deviceQueueCreateInfo = new VkDeviceQueueCreateInfo(
                _graphicsQueueFamily,
                1,
                &queuePriority
            );

            byte** deviceExtensions = (byte**)Memory.AllocatePointers(2);
            deviceExtensions[0] = Utf8.AllocateFromAsciiString(Vk.SwapchainExtensionName);
            deviceExtensions[1] = Utf8.AllocateFromAsciiString(Vk.DebugMarkerExtensionName);

            VkDeviceCreateInfo deviceCreateInfo = new VkDeviceCreateInfo(
                deviceQueueCreateInfo,
                1,
                deviceExtensions,
                physicalDeviceFeatures
            );
#if DEBUG
            if (foundDebugMarkerExtension)
            {
                deviceCreateInfo.EnabledExtensionCount = 2;
            }
#endif
            _vkCreateDevice(_physicalDevice, ref deviceCreateInfo, null, out _device).CheckError();

            Memory.Free(deviceExtensions);

            // Loading Vulkan device functions
            LoadDeviceFunctions(foundDebugMarkerExtension);
            
            // Get graphics queue
            _vkGetDeviceQueue(_device,_graphicsQueueFamily, 0, out _graphicsQueue);

            // Find color buffer format
            VkFormatProperties formatProperties;
            _colorFormat = VkFormat.R8G8B8A8UNorm;

            if (_physicalDeviceFeatures.ShaderStorageImageExtendedFormats)
            {
                _vkGetPhysicalDeviceFormatProperties(_physicalDevice, VkFormat.A2B10G10R10UNormPack32, out formatProperties);
                bool a2b10g10r10Support = (formatProperties.OptimalTilingFeatures & RequiredColorBufferFeatures) == RequiredColorBufferFeatures;

                if (a2b10g10r10Support)
                {
                    _colorFormat = VkFormat.A2B10G10R10UNormPack32;
                }
            }

            // Find depth buffer format
            _vkGetPhysicalDeviceFormatProperties(_physicalDevice, VkFormat.X8D24UNormPack32, out formatProperties);
            bool x8d24Support = (formatProperties.OptimalTilingFeatures & VkFormatFeatureFlags.DepthStencilAttachment) != 0;
            _vkGetPhysicalDeviceFormatProperties(_physicalDevice, VkFormat.D32SFloat, out formatProperties);
            bool d32Support = (formatProperties.OptimalTilingFeatures & VkFormatFeatureFlags.DepthStencilAttachment) != 0;

            _depthFormat = VkFormat.D16UNorm;
            if (x8d24Support)
            {
                _depthFormat = VkFormat.X8D24UNormPack32;
            }
            else if (d32Support)
            {
                _depthFormat = VkFormat.D32SFloat;
            }
        }

        private static void InitCommandBuffers()
        {
            // Create Vulkan command pool
            VkCommandPoolCreateInfo commandPoolCreateInfo = new VkCommandPoolCreateInfo(
                VkCommandPoolCreateFlags.ResetCommandBuffer,
                _graphicsQueueFamily
            );

            _vkCreateCommandPool(_device, ref commandPoolCreateInfo, null, out _commandPool).CheckError();

            commandPoolCreateInfo.Flags = VkCommandPoolCreateFlags.Transient;
            _vkCreateCommandPool(_device, ref commandPoolCreateInfo, null, out _transientCommandPool).CheckError();

            // Create Vulkan command buffers
            VkCommandBufferAllocateInfo commandBufferAllocateInfo = new VkCommandBufferAllocateInfo(_commandPool, CommandBufferCount);
            _commandBuffers = (VkCommandBuffer*)Marshal.AllocHGlobal(CommandBufferCount * sizeof(VkCommandBuffer));
            _vkAllocateCommandBuffers(_device, ref commandBufferAllocateInfo, _commandBuffers).CheckError();

            // Create Vulkan command buffer fences and semaphores
            VkFenceCreateInfo fenceCreateInfo = new VkFenceCreateInfo(VkFenceCreateFlags.None);

            for (int i = 0; i < CommandBufferCount; ++i)
            {
                _vkCreateFence(_device, ref fenceCreateInfo, null, out _commandBufferFences[i]).CheckError();

                VkSemaphoreCreateInfo semaphoreCreateInfo = new VkSemaphoreCreateInfo(VkSemaphoreCreateFlags.None);

                _vkCreateSemaphore(_device, ref semaphoreCreateInfo, null, out _drawCompleteSemaphores[i]).CheckError();
            }
        }

        private static void CreateSwapchain()
        {
            // Create Vulkan swapchain

            _vkGetPhysicalDeviceSurfaceCapabilitiesKHR(_physicalDevice, _surface, out _surfaceCapabilities).CheckError();

            //if (_vulkanSurfaceCapabilities.CurrentExtent.Width != )
        }

        private static void CreateColorBuffer()
        {

        }

        private static void CreateDepthBuffer()
        {

        }

        private static void CreateRenderPasses()
        {

        }

        private static void CreateFramebuffers()
        {

        }

        private static void InitStagingBuffers()
        {

        }

        private static void CreateDescriptorSetLayouts()
        {

        }

        private static void CreateDescriptorPool()
        {

        }

        private static void InitDynamicBuffers()
        {

        }

        private static void InitSamplers()
        {

        }

        private static void CreatePipelineLayouts()
        {

        }

        private static void CreatePipelines()
        {

        }

        private static void CreateDescriptorSets()
        {

        }

        private static void LoadGlobalFunctions()
        {
            byte* name = Utf8.AllocateFromAsciiString("vkCreateInstance");
            IntPtr funPtr = _vkGetInstanceProcAddr(IntPtr.Zero, name);

            Assert.IsFalse(funPtr == IntPtr.Zero, "Could not load Vulkan function vkCreateInstance");

            _vkCreateInstance = Marshal.GetDelegateForFunctionPointer<VkCreateInstance>(funPtr);

            Memory.Free(name);
        }

        private static void LoadInstanceFunctions()
        {
            Assert.IsFalse(_instance == VkInstance.Null, "Vulkan instance must not be null when calling " + nameof(LoadInstanceFunctions));

            string[] names = new string[]
            {
                nameof(VkDestroyInstance),
                nameof(VkGetDeviceProcAddr),
                nameof(VkEnumeratePhysicalDevices),
                nameof(VkEnumerateDeviceExtensionProperties),
                nameof(VkGetPhysicalDeviceProperties),
                nameof(VkGetPhysicalDeviceMemoryProperties),
                nameof(VkGetPhysicalDeviceQueueFamilyProperties),
                nameof(VkGetPhysicalDeviceFeatures),
                nameof(VkGetPhysicalDeviceFormatProperties),
                nameof(VkGetPhysicalDeviceSurfaceSupportKHR),
                nameof(VkGetPhysicalDeviceSurfaceCapabilitiesKHR),
                nameof(VkGetPhysicalDeviceSurfaceFormatsKHR),
                nameof(VkGetPhysicalDeviceSurfacePresentModesKHR),
                nameof(VkCreateDevice),
#if DEBUG
                nameof(VkCreateDebugReportCallbackEXT),
                nameof(VkDestroyDebugReportCallbackEXT),
#endif
            };

            int[] offsets = new int[names.Length];
            int byteCount = 0;
            for (int i = 0; i < names.Length; ++i)
            {
                offsets[i] = byteCount + i;
                byteCount += names[i].Length;
            }
            byteCount += names.Length;

            byte* namesPtr = Memory.AllocateBytes(byteCount);

            for (int i = 0; i < names.Length; ++i)
            {
                int index = offsets[i];
                for (int j = 0; j < names[i].Length; ++j)
                {
                    namesPtr[index + j] = j == 0 ? (byte)char.ToLower(names[i][j]) : (byte)names[i][j];
                }
                namesPtr[index + names[i].Length] = 0;
            }

            IntPtr[] funcPtrs = new IntPtr[names.Length];
            for (int i = 0; i < names.Length; ++i)
            {
                funcPtrs[i] = _vkGetInstanceProcAddr(_instance, namesPtr + offsets[i]);

                Assert.IsFalse(funcPtrs[i] == IntPtr.Zero, "Could not load Vulkan function " + Utf8.ToString(namesPtr + offsets[i]));
            }

            _vkDestroyInstance = Marshal.GetDelegateForFunctionPointer<VkDestroyInstance>(funcPtrs[0]);
            _vkGetDeviceProcAddr = Marshal.GetDelegateForFunctionPointer<VkGetDeviceProcAddr>(funcPtrs[1]);
            _vkEnumeratePhysicalDevices = Marshal.GetDelegateForFunctionPointer<VkEnumeratePhysicalDevices>(funcPtrs[2]);
            _vkEnumerateDeviceExtensionProperties = Marshal.GetDelegateForFunctionPointer<VkEnumerateDeviceExtensionProperties>(funcPtrs[3]);
            _vkGetPhysicalDeviceProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceProperties>(funcPtrs[4]);
            _vkGetPhysicalDeviceMemoryProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceMemoryProperties>(funcPtrs[5]);
            _vkGetPhysicalDeviceQueueFamilyProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceQueueFamilyProperties>(funcPtrs[6]);
            _vkGetPhysicalDeviceFeatures = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceFeatures>(funcPtrs[7]);
            _vkGetPhysicalDeviceFormatProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceFormatProperties>(funcPtrs[8]);
            _vkGetPhysicalDeviceSurfaceSupportKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfaceSupportKHR>(funcPtrs[9]);
            _vkGetPhysicalDeviceSurfaceCapabilitiesKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfaceCapabilitiesKHR>(funcPtrs[10]);
            _vkGetPhysicalDeviceSurfaceFormatsKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfaceFormatsKHR>(funcPtrs[11]);
            _vkGetPhysicalDeviceSurfacePresentModesKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfacePresentModesKHR>(funcPtrs[12]);
            _vkCreateDevice = Marshal.GetDelegateForFunctionPointer<VkCreateDevice>(funcPtrs[13]);
#if DEBUG
            _vkCreateDebugReportCallbackEXT = Marshal.GetDelegateForFunctionPointer<VkCreateDebugReportCallbackEXT>(funcPtrs[14]);
            _vkDestroyDebugReportCallbackEXT = Marshal.GetDelegateForFunctionPointer<VkDestroyDebugReportCallbackEXT>(funcPtrs[15]);
#endif

            Memory.Free(namesPtr);
        }

        private static void LoadDeviceFunctions(bool foundDebugMarkerExtension)
        {
            Assert.IsFalse(_device == VkDevice.Null, "Vulkan device must not be null when calling " + nameof(LoadDeviceFunctions));

            string[] names = new string[]
            {
                nameof(VkGetDeviceQueue),
                nameof(VkCreateCommandPool),
                nameof(VkAllocateCommandBuffers),
                nameof(VkCreateFence),
                nameof(VkCreateSemaphore),
                nameof(VkCreateSwapchainKHR),
                nameof(VkDestroySwapchainKHR),
                nameof(VkGetSwapchainImagesKHR),
                nameof(VkAcquireNextImageKHR),
                nameof(VkQueuePresentKHR),
#if DEBUG
                nameof(VkDebugMarkerSetObjectNameEXT),
#endif
            };

            int[] offsets = new int[names.Length];
            int byteCount = 0;
            for (int i = 0; i < names.Length; ++i)
            {
                offsets[i] = byteCount + i;
                byteCount += names[i].Length;
            }
            byteCount += names.Length;

            byte* namesPtr = Memory.AllocateBytes(byteCount);

            for (int i = 0; i < names.Length; ++i)
            {
                int index = offsets[i];
                for (int j = 0; j < names[i].Length; ++j)
                {
                    namesPtr[index + j] = j == 0 ? (byte)char.ToLower(names[i][j]) : (byte)names[i][j];
                }
                namesPtr[index + names[i].Length] = 0;
            }

            IntPtr[] funcPtrs = new IntPtr[names.Length];
            for (int i = 0; i < names.Length; ++i)
            {
                funcPtrs[i] = _vkGetDeviceProcAddr(_device, namesPtr + offsets[i]);

                Assert.IsFalse(funcPtrs[i] == IntPtr.Zero, "Could not load Vulkan function " + Utf8.ToString(namesPtr + offsets[i]));
            }

            _vkGetDeviceQueue = Marshal.GetDelegateForFunctionPointer<VkGetDeviceQueue>(funcPtrs[0]);
            _vkCreateCommandPool = Marshal.GetDelegateForFunctionPointer<VkCreateCommandPool>(funcPtrs[1]);
            _vkAllocateCommandBuffers = Marshal.GetDelegateForFunctionPointer<VkAllocateCommandBuffers>(funcPtrs[2]);
            _vkCreateFence = Marshal.GetDelegateForFunctionPointer<VkCreateFence>(funcPtrs[3]);
            _vkCreateSemaphore = Marshal.GetDelegateForFunctionPointer<VkCreateSemaphore>(funcPtrs[4]);
            _vkCreateSwapchainKHR = Marshal.GetDelegateForFunctionPointer<VkCreateSwapchainKHR>(funcPtrs[5]);
            _vkDestroySwapchainKHR = Marshal.GetDelegateForFunctionPointer<VkDestroySwapchainKHR>(funcPtrs[6]);
            _vkGetSwapchainImagesKHR = Marshal.GetDelegateForFunctionPointer<VkGetSwapchainImagesKHR>(funcPtrs[7]);
            _vkAcquireNextImageKHR = Marshal.GetDelegateForFunctionPointer<VkAcquireNextImageKHR>(funcPtrs[8]);
            _vkQueuePresentKHR = Marshal.GetDelegateForFunctionPointer<VkQueuePresentKHR>(funcPtrs[9]);
#if DEBUG
            if (foundDebugMarkerExtension)
            {
                _vkDebugMarkerSetObjectNameEXT = Marshal.GetDelegateForFunctionPointer<VkDebugMarkerSetObjectNameEXT>(funcPtrs[10]);
            }
#endif

            Memory.Free(namesPtr);
        }

#if DEBUG
        private static VkBool32 DebugMessageCallback(VkDebugReportFlags flags, VkDebugReportObjectType objectType, ulong @object, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData)
        {
            if (objectType == VkDebugReportObjectType.DebugReportCallback && messageCode == 1)
            {
                return false;
            }

            string output = "[" + Utf8.ToString(layerPrefix) + "] Code " + messageCode + ": " + Utf8.ToString(message);

            switch (flags)
            {
                case VkDebugReportFlags.Information: Log.Info(output); return false;
                case VkDebugReportFlags.Warning: Log.Warning(output); return false;
                case VkDebugReportFlags.PerformanceWarning: Log.Performance(output); return false;
                case VkDebugReportFlags.Error: Log.Error(output); return true;
                case VkDebugReportFlags.Debug: Log.Info(output); return false;
                default: return false;
            }
        }
#endif
    }
}