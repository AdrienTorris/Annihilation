using System;
using System.Runtime.InteropServices;
using Engine.Config;
using Engine.Collections;
using Vulkan;
using SDL2;

using Version = Vulkan.Version;

namespace Engine.Graphics
{
    /*
    ===========================================================================
    GraphicsSystem
    ===========================================================================
    */
    public static unsafe class GraphicsSystem
    {
        /*
        =============
        Constants
        =============
        */
        private const int CommandBufferCount = 2;
        private const int ColorBufferCount = 2;
        private const int MaxSwapchainImages = 8;
        private const VkFormatFeatureFlags RequiredColorBufferFeatures = VkFormatFeatureFlags.ColorAttachment |
                                                                         VkFormatFeatureFlags.ColorAttachmentBlend |
                                                                         VkFormatFeatureFlags.SampledImage |
                                                                         VkFormatFeatureFlags.StorageImage |
                                                                         VkFormatFeatureFlags.SampledImageFilterLinear;

        /*
        =============
        Fields
        =============
        */
        public static int DisplayWidth = 1280;
        public static int DisplayHeight = 720;
        public static BoolVar EnableVSync = new BoolVar("Graphics/VSync", false);
        public static BoolVar EnableValidation = new BoolVar("Graphics/Validation", true);

        private static VkInstance _instance;
        private static VkSurface _surface;
#if DEBUG
        private static VkDebugReportCallback _debugReportCallback;
#endif

        private static VkPhysicalDevice _physicalDevice;
        private static VkPhysicalDeviceProperties _physicalDeviceProperties;
        private static VkPhysicalDeviceMemoryProperties _physicalDeviceMemoryProperties;
        private static VkPhysicalDeviceFeatures _physicalDeviceFeatures;
        private static VkSurfaceCapabilities _surfaceCapabilities;
        private static VkSurfaceFormat* _surfaceFormats;
        private static VkPresentMode* _presentModes;
        private static VkQueueFamilyProperties* _queueFamilyProperties;
        private static VkExtensionProperties* _extensionProperties;

        private static VkFormat _swapchainFormat;
        private static VkSwapchain _swapchain;
        private static uint _swapchainImageCount;
        private static VkImage* _swapchainImages;
        private static VkImageView[] _swapchainImageViews = new VkImageView[MaxSwapchainImages];
        private static VkSemaphore[] _imageAcquiredSemaphores = new VkSemaphore[MaxSwapchainImages];

        private static VkCommandPool _commandPool;
        private static VkCommandPool _transientCommandPool;
        private static VkCommandBuffer* _commandBuffers;
        private static VkFence[] _commandBufferFences = new VkFence[CommandBufferCount];
        private static VkSemaphore[] _drawCompleteSemaphores = new VkSemaphore[CommandBufferCount];

        private static VkImage[] _colorBuffers = new VkImage[ColorBufferCount];

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

        private static bool _hasDedicatedAllocation;
        private static bool _hasDebugMarker;

        /*
        =============
        Vulkan functions
        =============
        */
        private static VkGetInstanceProcAddr _vkGetInstanceProcAddr;

        private static VkCreateInstance _vkCreateInstance;

        private static VkDestroyInstance _vkDestroyInstance;
        private static VkCreateDevice _vkCreateDevice;
        private static VkGetDeviceProcAddr _vkGetDeviceProcAddr;
        private static VkEnumeratePhysicalDevices _vkEnumeratePhysicalDevices;
        private static VkEnumerateDeviceExtensionProperties _vkEnumerateDeviceExtensionProperties;
        private static VkGetPhysicalDeviceProperties _vkGetPhysicalDeviceProperties;
        private static VkGetPhysicalDeviceMemoryProperties _vkGetPhysicalDeviceMemoryProperties;
        private static VkGetPhysicalDeviceQueueFamilyProperties _vkGetPhysicalDeviceQueueFamilyProperties;
        private static VkGetPhysicalDeviceFeatures _vkGetPhysicalDeviceFeatures;
        private static VkGetPhysicalDeviceFormatProperties _vkGetPhysicalDeviceFormatProperties;
        private static VkGetPhysicalDeviceImageFormatProperties _vkGetPhysicalDeviceImageFormatProperties;
        private static VkGetPhysicalDeviceSurfaceSupportKHR _vkGetPhysicalDeviceSurfaceSupportKHR;
        private static VkGetPhysicalDeviceSurfaceCapabilitiesKHR _vkGetPhysicalDeviceSurfaceCapabilitiesKHR;
        private static VkGetPhysicalDeviceSurfaceFormatsKHR _vkGetPhysicalDeviceSurfaceFormatsKHR;
        private static VkGetPhysicalDeviceSurfacePresentModesKHR _vkGetPhysicalDeviceSurfacePresentModesKHR;
#if DEBUG
        private static VkCreateDebugReportCallbackEXT _vkCreateDebugReportCallbackEXT;
        private static VkDestroyDebugReportCallbackEXT _vkDestroyDebugReportCallbackEXT;
#endif

        private static VkGetDeviceQueue _vkGetDeviceQueue;
        private static VkCreateCommandPool _vkCreateCommandPool;
        private static VkAllocateCommandBuffers _vkAllocateCommandBuffers;
        private static VkCreateFence _vkCreateFence;
        private static VkCreateSemaphore _vkCreateSemaphore;
        private static VkCreateImageView _vkCreateImageView;
        private static VkCreateImage _vkCreateImage;
        private static VkGetImageMemoryRequirements _vkGetImageMemoryRequirements;
        private static VkVkAllocateMemory _vkVkAllocateMemory;
        private static VkBindImageMemory _vkBindImageMemory;
        private static VkCreateSwapchainKHR _vkCreateSwapchainKHR;
        private static VkDestroySwapchainKHR _vkDestroySwapchainKHR;
        private static VkGetSwapchainImagesKHR _vkGetSwapchainImagesKHR;
        private static VkAcquireNextImageKHR _vkAcquireNextImageKHR;
        private static VkQueuePresentKHR _vkQueuePresentKHR;
#if DEBUG
        private static VkDebugMarkerSetObjectNameEXT _vkDebugMarkerSetObjectNameEXT;
#endif

        /*
        =============
        Properties
        =============
        */
        public static ulong FrameCount { get; private set; }

        /*
        =============
        Initialize
        =============
        */
        public static void Initialize(ref byte* title, ref SDL.Window window)
        {
            Assert.IsTrue(_swapchain == VkSwapchain.Null, "Graphics system has already been initialized.");

            LoadGetInstanceProcAddrFunction();
            LoadGlobalFunctions();
            CreateInstance(ref title, ref window);
            LoadInstanceFunctions();
            CreateSurface(ref window);
            SelectPhysicalDevice();
            CreateDeviceAndQueues();
            LoadDeviceFunctions();
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

        /*
        =============
        Present
        =============
        */
        public static void Present()
        {
            FrameCount++;
        }

        /*
        =============
        Resize
        =============
        */
        public static void Resize(int width, int height)
        {

        }

        /*
        =============
        Shutdown
        =============
        */
        public static void Shutdown()
        {
            SDL.QuitSubSystem(SDL.InitFlags.Video);
        }

        /*
        =============
        FindInstanceExtensions
        =============
        */
        private static void FindInstanceExtensions(ref SDL.Window window, out uint extensionCount, out byte** extensionNames)
        {
            uint requiredExtensionCount = 0;
            SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, null).CheckError();
            extensionCount = EnableValidation ? requiredExtensionCount + 1 : requiredExtensionCount;

            extensionNames = (byte**)Memory.AllocatePointers((int)extensionCount);
            SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, extensionNames).CheckError();

            if (EnableValidation)
            {
                extensionNames[requiredExtensionCount] = Utf8.AllocateFromAsciiString(Vk.ExtDebugReportExtensionName);
            }
        }

        /*
        =============
        CreateInstance
        =============
        */
        private static void CreateInstance(ref byte* title, ref SDL.Window window)
        {
            VkApplicationInfo applicationInfo = new VkApplicationInfo
            {
                Type = VkStructureType.ApplicationInfo,
                ApplicationName = title,
                ApplicationVersion = Version.One,
                EngineName = title,
                EngineVersion = Version.One,
                ApiVersion = new Version(1, 0, Vk.HeaderVersion)
            };

            FindInstanceExtensions(ref window, out uint extensionCount, out byte** extensionNames);

            VkInstanceCreateInfo instanceCreateInfo = new VkInstanceCreateInfo
            {
                Type = VkStructureType.InstanceCreateInfo,
                ApplicationInfo = &applicationInfo,
                EnabledExtensionCount = extensionCount,
                EnabledExtensionNames = extensionNames,
            };

            Text layerName = new Text();
            if (EnableValidation)
            {
                layerName = new Text("VK_LAYER_LUNARG_standard_validation");
                instanceCreateInfo.EnabledLayerCount = 1;
                instanceCreateInfo.EnabledLayerNames = layerName.BufferPtr;
            }

            _vkCreateInstance(ref instanceCreateInfo, null, out _instance).CheckError();

            Memory.Free(extensionNames);
            layerName.Dispose();

            if (EnableValidation)
            {
                CreateDebugReportCallback();
            }
        }

        /*
        =============
        CreateSurface
        =============
        */
        private static void CreateSurface(ref SDL.Window window)
        {
            SDL.VulkanCreateSurface(window, _instance, out ulong surfaceHandle).CheckError();
            _surface = new VkSurface { Handle = surfaceHandle };
        }

        /*
        =============
        SelectPhysicalDevice
        =============
        */
        private static void SelectPhysicalDevice()
        {
            uint physicalDeviceCount = 0;
            _vkEnumeratePhysicalDevices(_instance, ref physicalDeviceCount, null).CheckError();
            VkPhysicalDevice* physicalDevices = (VkPhysicalDevice*)Marshal.AllocHGlobal((int)physicalDeviceCount * sizeof(VkPhysicalDevice));
            _vkEnumeratePhysicalDevices(_instance, ref physicalDeviceCount, physicalDevices).CheckError();

            for (int i = 0; i < physicalDeviceCount; ++i)
            {
                VkPhysicalDevice physicalDevice = physicalDevices[i];

                // Check for extension support
                {
                    uint extensionCount = 0;
                    _vkEnumerateDeviceExtensionProperties(physicalDevice, null, ref extensionCount, null).CheckError();
                    _extensionProperties = (VkExtensionProperties*)Marshal.AllocHGlobal((int)extensionCount * sizeof(VkExtensionProperties));
                    _vkEnumerateDeviceExtensionProperties(physicalDevice, null, ref extensionCount, _extensionProperties).CheckError();

                    if (!SupportsRequiredExtensions()) continue;

                    bool SupportsRequiredExtensions()
                    {
                        string[] requiredExtensions = new string[]
                        {
                            Vk.SwapchainExtensionName
                        };

                        using (TextPool textPool = new TextPool(requiredExtensions.Length, Vk.SwapchainExtensionName.Length))
                        {
                            for (int j = 0; j < extensionCount; ++j)
                            {
                                for (int k = 0; k < requiredExtensions.Length; ++k)
                                {
                                    byte* requiredExtension = textPool.Get(requiredExtensions[k]);
                                    if (!Utf8.Compare(_extensionProperties[j].ExtensionName, requiredExtension))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                        return true;
                    }
                }

                uint familyCount = 0;
                _vkGetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref familyCount, null);
                _queueFamilyProperties = (VkQueueFamilyProperties*)Marshal.AllocHGlobal((int)familyCount * sizeof(VkQueueFamilyProperties));
                _vkGetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref familyCount, _queueFamilyProperties);

                _vkGetPhysicalDeviceSurfaceCapabilitiesKHR(physicalDevice, _surface, out _surfaceCapabilities).CheckError();

                count = 0;
                _vkGetPhysicalDeviceSurfaceFormatsKHR(physicalDevice, _surface, ref count, null).CheckError();
                _surfaceFormats = (VkSurfaceFormat*)Marshal.AllocHGlobal((int)count * sizeof(VkSurfaceFormat));
                _vkGetPhysicalDeviceSurfaceFormatsKHR(physicalDevice, _surface, ref count, _surfaceFormats).CheckError();

                // GetPhysicalDeviceSurfaceCapabilities
                count = 0;
                _vkGetPhysicalDeviceSurfacePresentModesKHR(physicalDevice, _surface, ref count, null).CheckError();
                _presentModes = (VkPresentMode*)Marshal.AllocHGlobal((int)count * sizeof(VkPresentMode));
                _vkGetPhysicalDeviceSurfacePresentModesKHR(physicalDevice, _surface, ref count, _presentModes).CheckError();

                // GetPhysicalDeviceSurfaceCapabilities
                _vkGetPhysicalDeviceMemoryProperties(physicalDevice, out _deviceMemoryProperties);

                // GetPhysicalDeviceSurfaceCapabilities
                _vkGetPhysicalDeviceProperties(_physicalDevice, out _deviceProperties);
            }
        }

        /*
        =============
        CreateDeviceAndQueues
        =============
        */
        private static void CreateDeviceAndQueues()
        {
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
            if (_hasDebugMarker)
            {
                deviceCreateInfo.EnabledExtensionCount = 2;
            }
#endif
            _vkCreateDevice(_physicalDevice, ref deviceCreateInfo, null, out _device).CheckError();

            Memory.Free(deviceExtensions);

            // Get graphics queue
            _vkGetDeviceQueue(_device, _graphicsQueueFamily, 0, out _graphicsQueue);

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
            VkPresentMode presentMode = VkPresentMode.Fifo;
            // Without VSync, prefer immediate to triple buffering
            if (!EnableVSync)
            {
                bool foundImmediate = false;
                bool foundMailbox = false;
                for (int i = 0; i < presentModeCount; ++i)
                {
                    if (presentModes[i] == VkPresentMode.Immediate) foundImmediate = true;
                    if (presentModes[i] == VkPresentMode.Mailbox) foundMailbox = true;
                }
                if (foundMailbox) presentMode = VkPresentMode.Mailbox;
                if (foundImmediate) presentMode = VkPresentMode.Immediate;
            }

            Memory.Free(presentModes);

            Log.Info("Using present mode " + presentMode);

            // Create the sawpchain
            VkSwapchainCreateInfo swapchainCreateInfo = new VkSwapchainCreateInfo
            {
                Type = VkStructureType.SwapchainCreateInfo,
                Next = null,
                Surface = _surface,
                MinImageCount = 2,
                ImageFormat = surfaceFormats[0].Format,
                ImageColorSpace = surfaceFormats[0].ColorSpace,
                ImageExtent = new VkExtent2D(DisplayWidth, DisplayHeight),
                ImageUsage = VkImageUsageFlags.ColorAttachment | VkImageUsageFlags.TransferSrc,
                PreTransform = VkSurfaceTransformFlags.Identity,
                ImageArrayLayers = 1,
                ImageSharingMode = VkSharingMode.Exclusive,
                QueueFamilyIndexCount = 0,
                QueueFamilyIndices = null,
                PresentMode = presentMode,
                Clipped = true,
                CompositeAlpha = VkCompositeAlphaFlags.Opaque,
            };
            // Not all devices support opaque alpha
            if ((_surfaceCapabilities.SupportedCompositeAlpha & VkCompositeAlphaFlags.Opaque) == 0)
            {
                swapchainCreateInfo.CompositeAlpha = VkCompositeAlphaFlags.Inherit;
            }

            _swapchainFormat = surfaceFormats[0].Format;
            Memory.Free(surfaceFormats);

            _vkCreateSwapchainKHR(_device, ref swapchainCreateInfo, null, out _swapchain).CheckError();

            // Create the swapchain images
            _vkGetSwapchainImagesKHR(_device, _swapchain, ref _swapchainImageCount, null).CheckError();

            _swapchainImages = (VkImage*)Marshal.AllocHGlobal((int)_swapchainImageCount * sizeof(VkImage));
            _vkGetSwapchainImagesKHR(_device, _swapchain, ref _swapchainImageCount, _swapchainImages).CheckError();

            VkImageViewCreateInfo imageViewCreateInfo = new VkImageViewCreateInfo
            {
                Type = VkStructureType.ImageViewCreateInfo,
                Next = null,
                Format = _swapchainFormat,
                Components = new VkComponentMapping(VkComponentSwizzle.R, VkComponentSwizzle.G, VkComponentSwizzle.B, VkComponentSwizzle.A),
                SubresourceRange = new VkImageSubresourceRange(VkImageAspectFlags.Color, 0, 1, 0, 1),
                ViewType = VkImageViewType.Type2D,
                Flags = VkImageViewCreateFlags.None,
            };

            VkSemaphoreCreateInfo semaphoreCreateInfo = new VkSemaphoreCreateInfo(VkSemaphoreCreateFlags.None);

            for (int i = 0; i < _swapchainImageCount; ++i)
            {
#if DEBUG
                SetObjectName(_swapchainImages[i], VkDebugReportObjectType.Image, "Swapchain");
#endif
                imageViewCreateInfo.Image = _swapchainImages[i];
                _vkCreateImageView(_device, ref imageViewCreateInfo, null, out _swapchainImageViews[i]).CheckError();
#if DEBUG
                SetObjectName(_swapchainImageViews[i], VkDebugReportObjectType.ImageView, "Swapchain View");
#endif
                _vkCreateSemaphore(_device, ref semaphoreCreateInfo, null, out _imageAcquiredSemaphores[i]).CheckError();
            }
        }

        private static void CreateColorBuffer()
        {
            VkImageCreateInfo imageCreateInfo = new VkImageCreateInfo
            {
                Type = VkStructureType.ImageCreateInfo,
                Next = null,
                ImageType = VkImageType.Image2D,
                Format = _colorFormat,
                Extent = new VkExtent3D(DisplayWidth, DisplayHeight, 1),
                MipLevels = 1,
                ArrayLayers = 1,
                Samples = VkSampleCountFlags.Sample1,
                Tiling = VkImageTiling.Optimal,
                Usage = VkImageUsageFlags.ColorAttachment | VkImageUsageFlags.InputAttachment | VkImageUsageFlags.Sampled | VkImageUsageFlags.Storage
            };

            for (int i = 0; i < ColorBufferCount; ++i)
            {
                _vkCreateImage(_device, ref imageCreateInfo, null, out _colorBuffers[i]).CheckError();
#if DEBUG
                SetObjectName(_colorBuffers[i], VkDebugReportObjectType.Image, "Color Buffer " + i);
#endif
                _vkGetImageMemoryRequirements(_device, _colorBuffers[i], out VkMemoryRequirements memoryRequirements);

                VkMemoryDedicatedAllocateInfo dedicatedAllocationInfo = new VkMemoryDedicatedAllocateInfo
                {
                    Type = VkStructureType.MemoryDedicatedAllocateInfo,
                    Next = null,
                    Image = _colorBuffers[i]
                };

                VkMemoryAllocateInfo memoryAllocateInfo = new VkMemoryAllocateInfo
                {
                    Type = VkStructureType.MemoryAllocateInfo,
                    Next = null,
                    AllocationSize = memoryRequirements.Size,
                    MemoryTypeIndex = memoryRequirements.MemoryTypeBits
                };
            }
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

        /*private static int MemoryTypeFromProperties(uint type, uint flags, uint preferredMask)
        {

        }*/

        /*
        =============
        LoadGetInstanceProcAddrFunction
        =============
        */
        private static void LoadGetInstanceProcAddrFunction()
        {
            IntPtr funcPtr = SDL.VulkanGetVkGetInstanceProcAddr();
            funcPtr.CheckError();
            _vkGetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<VkGetInstanceProcAddr>(funcPtr);
        }

        /*
        =============
        LoadGlobalFunctions
        =============
        */
        private static void LoadGlobalFunctions()
        {
            IntPtr funcPtr;
            using (Text name = new Text("vkCreateInstance"))
            {
                funcPtr = _vkGetInstanceProcAddr(IntPtr.Zero, name.Buffer);
            }
            Assert.IsFalse(funcPtr == IntPtr.Zero, "Could not load Vulkan function vkCreateInstance");

            _vkCreateInstance = Marshal.GetDelegateForFunctionPointer<VkCreateInstance>(funcPtr);
        }

        /*
        =============
        LoadInstanceFunctions
        =============
        */
        private static void LoadInstanceFunctions()
        {
            Assert.IsFalse(_instance == VkInstance.Null, "Vulkan instance must not be null when calling " + nameof(LoadInstanceFunctions));

            string[] names = new string[]
            {
                "vkCreateDevice",
                "vkDestroyInstance",
                "vkGetDeviceProcAddr",
                "vkEnumeratePhysicalDevices",
                "vkGetPhysicalDeviceFeatures",
                "vkGetPhysicalDeviceProperties",
                "vkGetPhysicalDeviceFormatProperties",
                "vkGetPhysicalDeviceMemoryProperties",
                "vkEnumerateDeviceExtensionProperties",
                "vkGetPhysicalDeviceQueueFamilyProperties",

                "vkGetPhysicalDeviceSurfaceFormatsKHR",
                "vkGetPhysicalDeviceSurfaceSupportKHR",
                "vkGetPhysicalDeviceSurfaceCapabilitiesKHR",
                "vkGetPhysicalDeviceSurfacePresentModesKHR",

                "vkCreateDebugReportCallbackEXT",
                "vkDestroyDebugReportCallbackEXT",
            };

            int funcCount = EnableValidation ? names.Length : names.Length - 2;

            IntPtr[] funcPtrs = new IntPtr[funcCount];
            using (TextPool textPool = new TextPool(funcCount, "vkGetPhysicalDeviceSurfacePresentModesKHR".Length))
            {
                for (int i = 0; i < funcCount; ++i)
                {
                    byte* funcName = textPool.Get(names[i]);
                    funcPtrs[i] = _vkGetInstanceProcAddr(_instance, funcName);

                    Assert.IsFalse(funcPtrs[i] == IntPtr.Zero, "Could not load Vulkan function " + Utf8.ToString(funcName));
                }
            }

            int index = 0;
            _vkCreateDevice = Marshal.GetDelegateForFunctionPointer<VkCreateDevice>(funcPtrs[index++]);
            _vkDestroyInstance = Marshal.GetDelegateForFunctionPointer<VkDestroyInstance>(funcPtrs[index++]);
            _vkGetDeviceProcAddr = Marshal.GetDelegateForFunctionPointer<VkGetDeviceProcAddr>(funcPtrs[index++]);
            _vkEnumeratePhysicalDevices = Marshal.GetDelegateForFunctionPointer<VkEnumeratePhysicalDevices>(funcPtrs[index++]);
            _vkGetPhysicalDeviceFeatures = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceFeatures>(funcPtrs[index++]);
            _vkGetPhysicalDeviceProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceProperties>(funcPtrs[index++]);
            _vkGetPhysicalDeviceFormatProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceFormatProperties>(funcPtrs[index++]);
            _vkGetPhysicalDeviceMemoryProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceMemoryProperties>(funcPtrs[index++]);
            _vkEnumerateDeviceExtensionProperties = Marshal.GetDelegateForFunctionPointer<VkEnumerateDeviceExtensionProperties>(funcPtrs[index++]);
            _vkGetPhysicalDeviceQueueFamilyProperties = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceQueueFamilyProperties>(funcPtrs[index++]);

            _vkGetPhysicalDeviceSurfaceFormatsKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfaceFormatsKHR>(funcPtrs[index++]);
            _vkGetPhysicalDeviceSurfaceSupportKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfaceSupportKHR>(funcPtrs[index++]);
            _vkGetPhysicalDeviceSurfaceCapabilitiesKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfaceCapabilitiesKHR>(funcPtrs[index++]);
            _vkGetPhysicalDeviceSurfacePresentModesKHR = Marshal.GetDelegateForFunctionPointer<VkGetPhysicalDeviceSurfacePresentModesKHR>(funcPtrs[index++]);

            if (EnableValidation)
            {
                _vkCreateDebugReportCallbackEXT = Marshal.GetDelegateForFunctionPointer<VkCreateDebugReportCallbackEXT>(funcPtrs[index++]);
                _vkDestroyDebugReportCallbackEXT = Marshal.GetDelegateForFunctionPointer<VkDestroyDebugReportCallbackEXT>(funcPtrs[index++]);
            }
        }

        /*
        =============
        LoadDeviceFunctions
        =============
        */
        private static void LoadDeviceFunctions()
        {
            Assert.IsFalse(_device == VkDevice.Null, "Vulkan device must not be null when calling " + nameof(LoadDeviceFunctions));

            string[] names = new string[]
            {
                "vkCreateFence",
                "vkGetDeviceQueue",
                "vkCreateImageView",
                "vkCreateSemaphore",
                "vkCreateCommandPool",
                "vkAllocateCommandBuffers",

                "vkQueuePresentKHR",
                "vkCreateSwapchainKHR",
                "vkAcquireNextImageKHR",
                "vkDestroySwapchainKHR",
                "vkGetSwapchainImagesKHR",

                "vkDebugMarkerSetObjectNameEXT",
            };

            int funcCount = _hasDebugMarker ? names.Length : names.Length - 1;

            IntPtr[] funcPtrs = new IntPtr[funcCount];
            using (TextPool textPool = new TextPool(funcCount, "vkDebugMarkerSetObjectNameEXT".Length))
            {
                for (int i = 0; i < funcCount; ++i)
                {
                    byte* funcName = textPool.Get(names[i]);
                    funcPtrs[i] = _vkGetDeviceProcAddr(_device, funcName);

                    Assert.IsFalse(funcPtrs[i] == IntPtr.Zero, "Could not load Vulkan function " + Utf8.ToString(funcName));
                }
            }

            int index = 0;
            _vkCreateFence = Marshal.GetDelegateForFunctionPointer<VkCreateFence>(funcPtrs[index++]);
            _vkGetDeviceQueue = Marshal.GetDelegateForFunctionPointer<VkGetDeviceQueue>(funcPtrs[index++]);
            _vkCreateImageView = Marshal.GetDelegateForFunctionPointer<VkCreateImageView>(funcPtrs[index++]);
            _vkCreateSemaphore = Marshal.GetDelegateForFunctionPointer<VkCreateSemaphore>(funcPtrs[index++]);
            _vkCreateCommandPool = Marshal.GetDelegateForFunctionPointer<VkCreateCommandPool>(funcPtrs[index++]);
            _vkAllocateCommandBuffers = Marshal.GetDelegateForFunctionPointer<VkAllocateCommandBuffers>(funcPtrs[index++]);

            _vkQueuePresentKHR = Marshal.GetDelegateForFunctionPointer<VkQueuePresentKHR>(funcPtrs[index++]);
            _vkCreateSwapchainKHR = Marshal.GetDelegateForFunctionPointer<VkCreateSwapchainKHR>(funcPtrs[index++]);
            _vkAcquireNextImageKHR = Marshal.GetDelegateForFunctionPointer<VkAcquireNextImageKHR>(funcPtrs[index++]);
            _vkDestroySwapchainKHR = Marshal.GetDelegateForFunctionPointer<VkDestroySwapchainKHR>(funcPtrs[index++]);
            _vkGetSwapchainImagesKHR = Marshal.GetDelegateForFunctionPointer<VkGetSwapchainImagesKHR>(funcPtrs[index++]);

            if (_hasDebugMarker)
            {
                _vkDebugMarkerSetObjectNameEXT = Marshal.GetDelegateForFunctionPointer<VkDebugMarkerSetObjectNameEXT>(funcPtrs[index++]);
            }
        }

        /*
        =============
        SetObjectName
        =============
        */
        private static void SetObjectName(ulong obj, VkDebugReportObjectType objectType, string name)
        {
            if (_vkDebugMarkerSetObjectNameEXT != null && name != null)
            {
                using (Text namePtr = new Text(name))
                {
                    VkDebugMarkerObjectNameInfo nameInfo = new VkDebugMarkerObjectNameInfo
                    {
                        Type = VkStructureType.DebugMarkerObjectNameInfo,
                        Next = null,
                        ObjectType = objectType,
                        Object = obj,
                        ObjectName = namePtr.Buffer
                    };
                    _vkDebugMarkerSetObjectNameEXT(_device, ref nameInfo).CheckError();
                }
            }
        }

        /*
        =============
        DebugMessageCallback
        =============
        */
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

        /*
        =============
        CreateDebugReportCallback
        =============
        */
        private static void CreateDebugReportCallback()
        {
            VkDebugReportCallbackCreateInfo debugReportCallbackCreateInfo = new VkDebugReportCallbackCreateInfo(
                VkDebugReportFlags.Error | VkDebugReportFlags.Warning | VkDebugReportFlags.PerformanceWarning | VkDebugReportFlags.Debug,
                DebugMessageCallback
            );

            _vkCreateDebugReportCallbackEXT(_instance, ref debugReportCallbackCreateInfo, null, out _debugReportCallback).CheckError();
        }

        /*
        =============
        DestroyDebugReportCallback
        =============
        */
        private static void DestroyDebugReportCallback()
        {
            _vkDestroyDebugReportCallbackEXT(_instance, _debugReportCallback, null);
        }
    }
}