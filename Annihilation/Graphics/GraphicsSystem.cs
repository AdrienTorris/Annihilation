﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Annihilation.Config;
using Annihilation.Core;
using Annihilation.Vulkan;
using SDL2;

using Version = Annihilation.Vulkan.Version;

namespace Annihilation.Graphics
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
        private const FormatFeatureFlags RequiredColorBufferFeatures = FormatFeatureFlags.ColorAttachment |
                                                                         FormatFeatureFlags.ColorAttachmentBlend |
                                                                         FormatFeatureFlags.SampledImage |
                                                                         FormatFeatureFlags.StorageImage |
                                                                         FormatFeatureFlags.SampledImageFilterLinear;

        /*
        =============
        Config variables
        =============
        */
        public static EnumVar<WindowMode> WindowMode = new EnumVar<WindowMode>("WindowMode", Config.WindowMode.Windowed);
        public static FloatVar TargetFramerate = new FloatVar("TargetFramerate", 144f);
        public static IntVar DisplayWidth = new IntVar("DisplayWidth", 1280);
        public static IntVar DisplayHeight = new IntVar("DisplayHeight", 720);
        public static BoolVar EnableVSync = new BoolVar("VSync", false);
        public static BoolVar EnableValidation = new BoolVar("Validation", true);
        public static IntVar DisplayAdapter = new IntVar("DisplayAdapter", 0);
        public static IntVar DisplayMode = new IntVar("DisplayMode", (int)Graphics.DisplayMode.FullscreenBorderless, 0, (int)Graphics.DisplayMode.Count);
        public static IntVar Monitor = new IntVar("Monitor", 0);

        private static InstanceHandle _instance;
        private static SurfaceHandle _surface;
#if DEBUG
        private static DebugReportCallbackHandle _debugReportCallback;
#endif

        private static PhysicalDeviceHandle _physicalDevice;
        private static PhysicalDeviceProperties _physicalDeviceProperties;
        private static PhysicalDeviceMemoryProperties _physicalDeviceMemoryProperties;
        private static PhysicalDeviceFeatures _physicalDeviceFeatures;
        private static SurfaceCapabilities _surfaceCapabilities;
        private static SurfaceFormat* _surfaceFormats;
        private static PresentMode* _presentModes;
        private static QueueFamilyProperties* _queueFamilyProperties;
        private static ExtensionProperties* _extensionProperties;

        private static Format _swapchainFormat;
        private static SwapchainHandle _swapchain;
        private static uint _swapchainImageCount;
        private static ImageHandle* _swapchainImages;
        private static ImageViewHandle[] _swapchainImageViews = new ImageViewHandle[MaxSwapchainImages];
        private static SemaphoreHandle[] _imageAcquiredSemaphores = new SemaphoreHandle[MaxSwapchainImages];

        private static CommandPoolHandle _commandPool;
        private static CommandPoolHandle _transientCommandPool;
        private static CommandBufferHandle* _commandBuffers;
        private static FenceHandle[] _commandBufferFences = new FenceHandle[CommandBufferCount];
        private static SemaphoreHandle[] _drawCompleteSemaphores = new SemaphoreHandle[CommandBufferCount];

        private static ImageHandle[] _colorBuffers = new ImageHandle[ColorBufferCount];

        private static bool[] _isCommandBufferSubmitted = new bool[CommandBufferCount];
        private static uint _currentSwapchainBuffer;
        private static DeviceHandle _device;
        private static uint _graphicsQueueFamily;
        private static uint _computeQueueFamily;
        private static uint _transferQueueFamily;
        private static QueueHandle _graphicsQueue;
        private static QueueHandle _computeQueue;
        private static QueueHandle _transferQueue;
        private static CommandBufferHandle _commandBuffer;
        private static PhysicalDeviceProperties _deviceProperties;
        private static PhysicalDeviceMemoryProperties _deviceMemoryProperties;
        private static Format _colorFormat;
        private static Format _depthFormat;
        private static SampleCountFlags _sampleCount;
        private static bool _supersampling;

        private static bool _hasDedicatedAllocation;
        private static bool _hasDebugMarker;

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
        public static void Initialize(Text title, SDL.Window window)
        {
            Debug.Assert(_swapchain == SwapchainHandle.Null, "Graphics system has already been initialized.");

            FindInstanceExtensions(out uint instanceExtensionCount, out byte** instanceExtensions);

            CreateInstance(out Instance instance);

            void FindInstanceExtensions(out uint extensionCount, out byte** extensionNames)
            {
                // Get count of required extensions
                uint requiredExtensionCount = 0;
                SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, null).CheckError();

                // Declare additional extensions
                byte*[] additionalExtensions = null;
                if (EnableValidation)
                {
                    additionalExtensions = new byte*[]
                    {
                        ExtensionName.ExtDebugReport
                    };
                }
                uint additionalExtensionCount = additionalExtensions != null ? (uint)additionalExtensions.Length : 0;

                // Get count of required + additional extensions
                extensionCount = requiredExtensionCount + additionalExtensionCount;

                // Allocate extension names
                extensionNames = Memory.AllocatePointers(extensionCount);

                // Get names of required extensions
                SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, extensionNames).CheckError();

                // Copy additional extensions after required extensions
                for (int i = 0; i < additionalExtensionCount; ++i)
                {
                    extensionNames[i + requiredExtensionCount] = additionalExtensions[i];
                }
            }

            void CreateInstance(out Instance instance)
            {
                ApplicationInfo applicationInfo = new ApplicationInfo
                {
                    Type = StructureType.ApplicationInfo,
                    ApplicationName = title,
                    ApplicationVersion = Version.One,
                    EngineName = title,
                    EngineVersion = Version.One,
                    ApiVersion = new Version(1, 0, Vulkan.Vulkan.HeaderVersion)
                };

                InstanceCreateInfo instanceCreateInfo = new InstanceCreateInfo
                {
                    Type = StructureType.InstanceCreateInfo,
                    ApplicationInfo = &applicationInfo,
                    EnabledExtensionCount = instanceExtensionCount,
                    EnabledExtensionNames = instanceExtensions,
                };

                Text layerName = new Text();
                if (EnableValidation)
                {
                    layerName = new Text("VK_LAYER_LUNARG_standard_validation");
                    instanceCreateInfo.EnabledLayerCount = 1;
                    instanceCreateInfo.EnabledLayerNames = layerName.BufferPtr;
                }

                instance = new Instance(ref instanceCreateInfo);

                Memory.Free(extensionNames);
                layerName.Free();

                if (EnableValidation)
                {
                    instance.CreateDebugReportCallback();
                }
            }
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
            // Get count of required extensions
            uint requiredExtensionCount = 0;
            SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, null).CheckError();

            // Declare additional extensions
            byte*[] additionalExtensions = null;
            if (EnableValidation)
            {
                additionalExtensions = new byte*[]
                {
                    ExtensionName.ExtDebugReport
                };
            }
            uint additionalExtensionCount = additionalExtensions != null ? (uint)additionalExtensions.Length : 0;

            // Get count of required + additional extensions
            extensionCount = requiredExtensionCount + additionalExtensionCount;

            // Allocate extension names
            extensionNames = Memory.AllocatePointers(extensionCount);

            // Get names of required extensions
            SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, extensionNames).CheckError();

            // Copy additional extensions after required extensions
            for (int i = 0; i < additionalExtensionCount; ++i)
            {
                extensionNames[i + requiredExtensionCount] = additionalExtensions[i];
            }
        }

        /*
        =============
        CreateInstance
        =============
        */
        private static void CreateInstance(ref byte* title, ref SDL.Window window)
        {
            ApplicationInfo applicationInfo = new ApplicationInfo
            {
                Type = StructureType.ApplicationInfo,
                ApplicationName = title,
                ApplicationVersion = Version.One,
                EngineName = title,
                EngineVersion = Version.One,
                ApiVersion = new Version(1, 0, Vulkan.Vulkan.HeaderVersion)
            };

            FindInstanceExtensions(ref window, out uint extensionCount, out byte** extensionNames);

            InstanceCreateInfo instanceCreateInfo = new InstanceCreateInfo
            {
                Type = StructureType.InstanceCreateInfo,
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

            Instance instance = new Instance(ref instanceCreateInfo);

            Memory.Free(extensionNames);
            layerName.Free();

            if (EnableValidation)
            {
                instance.CreateDebugReportCallback();
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
            _surface = new SurfaceHandle { Handle = surfaceHandle };
        }

        /*
        =============
        SelectPhysicalDevice
        =============
        */
        private static void SelectPhysicalDevice()
        {
            uint physicalDeviceCount = 0;
            _EnumeratePhysicalDevices(_instance, ref physicalDeviceCount, null).CheckError();
            PhysicalDeviceHandle* physicalDevices = (PhysicalDeviceHandle*)Marshal.AllocHGlobal((int)physicalDeviceCount * sizeof(PhysicalDeviceHandle));
            _EnumeratePhysicalDevices(_instance, ref physicalDeviceCount, physicalDevices).CheckError();

            for (int i = 0; i < physicalDeviceCount; ++i)
            {
                PhysicalDeviceHandle physicalDevice = physicalDevices[i];

                // Check for extension support
                {

                    if (!SupportsRequiredExtensions()) continue;

                    bool SupportsRequiredExtensions()
                    {
                        string[] requiredExtensions = new string[]
                        {
                            .SwapchainExtensionName
                        };

                        using (TextPool textPool = new TextPool(requiredExtensions.Length, .SwapchainExtensionName.Length))
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
                _GetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref familyCount, null);
                _queueFamilyProperties = (QueueFamilyProperties*)Marshal.AllocHGlobal((int)familyCount * sizeof(QueueFamilyProperties));
                _GetPhysicalDeviceQueueFamilyProperties(physicalDevice, ref familyCount, _queueFamilyProperties);

                _GetPhysicalDeviceSurfaceCapabilitiesKHR(physicalDevice, _surface, out _surfaceCapabilities).CheckError();

                count = 0;
                _GetPhysicalDeviceSurfaceFormatsKHR(physicalDevice, _surface, ref count, null).CheckError();
                _surfaceFormats = (SurfaceFormat*)Marshal.AllocHGlobal((int)count * sizeof(SurfaceFormat));
                _GetPhysicalDeviceSurfaceFormatsKHR(physicalDevice, _surface, ref count, _surfaceFormats).CheckError();

                // GetPhysicalDeviceSurfaceCapabilities
                count = 0;
                _GetPhysicalDeviceSurfacePresentModesKHR(physicalDevice, _surface, ref count, null).CheckError();
                _presentModes = (PresentMode*)Marshal.AllocHGlobal((int)count * sizeof(PresentMode));
                _GetPhysicalDeviceSurfacePresentModesKHR(physicalDevice, _surface, ref count, _presentModes).CheckError();

                // GetPhysicalDeviceSurfaceCapabilities
                _GetPhysicalDeviceMemoryProperties(physicalDevice, out _deviceMemoryProperties);

                // GetPhysicalDeviceSurfaceCapabilities
                _GetPhysicalDeviceProperties(_physicalDevice, out _deviceProperties);
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
            Bool32[] queueSupportsPresent = new Bool32[queueFamilyCount];
            for (uint i = 0; i < queueFamilyCount; ++i)
            {
                _GetPhysicalDeviceSurfaceSupportKHR(_physicalDevice, i, _surface, out queueSupportsPresent[i]);
            }

            // Finding queue with graphics and present support.
            bool foundGraphicsQueue = false;

            for (uint i = 0; i < queueFamilyCount; ++i)
            {
                if ((queueFamilyProperties[i].QueueFlags & QueueFlags.Graphics) != 0 && queueSupportsPresent[i])
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
            _GetPhysicalDeviceFeatures(_physicalDevice, out _physicalDeviceFeatures);

            PhysicalDeviceFeatures physicalDeviceFeatures = new PhysicalDeviceFeatures
            {
                ShaderStorageImageExtendedFormats = _physicalDeviceFeatures.ShaderStorageImageExtendedFormats,
                SamplerAnisotropy = _physicalDeviceFeatures.SamplerAnisotropy
            };

            // Creating Vulkan device.
            float queuePriority = 0f;
            DeviceQueueCreateInfo deviceQueueCreateInfo = new DeviceQueueCreateInfo(
                _graphicsQueueFamily,
                1,
                &queuePriority
            );

            byte** deviceExtensions = (byte**)Memory.AllocatePointers(2);
            deviceExtensions[0] = Utf8.AllocateFromAsciiString(.SwapchainExtensionName);
            deviceExtensions[1] = Utf8.AllocateFromAsciiString(.DebugMarkerExtensionName);

            DeviceCreateInfo deviceCreateInfo = new DeviceCreateInfo(
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
            _CreateDevice(_physicalDevice, ref deviceCreateInfo, null, out _device).CheckError();

            Memory.Free(deviceExtensions);

            // Get graphics queue
            _GetDeviceQueue(_device, _graphicsQueueFamily, 0, out _graphicsQueue);

            // Find color buffer format
            FormatProperties formatProperties;
            _colorFormat = Format.R8G8B8A8UNorm;

            if (_physicalDeviceFeatures.ShaderStorageImageExtendedFormats)
            {
                _GetPhysicalDeviceFormatProperties(_physicalDevice, Format.A2B10G10R10UNormPack32, out formatProperties);
                bool a2b10g10r10Support = (formatProperties.OptimalTilingFeatures & RequiredColorBufferFeatures) == RequiredColorBufferFeatures;

                if (a2b10g10r10Support)
                {
                    _colorFormat = Format.A2B10G10R10UNormPack32;
                }
            }

            // Find depth buffer format
            _GetPhysicalDeviceFormatProperties(_physicalDevice, Format.X8D24UNormPack32, out formatProperties);
            bool x8d24Support = (formatProperties.OptimalTilingFeatures & FormatFeatureFlags.DepthStencilAttachment) != 0;
            _GetPhysicalDeviceFormatProperties(_physicalDevice, Format.D32SFloat, out formatProperties);
            bool d32Support = (formatProperties.OptimalTilingFeatures & FormatFeatureFlags.DepthStencilAttachment) != 0;

            _depthFormat = Format.D16UNorm;
            if (x8d24Support)
            {
                _depthFormat = Format.X8D24UNormPack32;
            }
            else if (d32Support)
            {
                _depthFormat = Format.D32SFloat;
            }
        }

        /*
        =============
        InitCommandBuffers
        =============
        */
        private static void InitCommandBuffers()
        {
            // Create Vulkan command pool
            CommandPoolCreateInfo commandPoolCreateInfo = new CommandPoolCreateInfo(
                CommandPoolCreateFlags.ResetCommandBuffer,
                _graphicsQueueFamily
            );

            _CreateCommandPool(_device, ref commandPoolCreateInfo, null, out _commandPool).CheckError();

            commandPoolCreateInfo.Flags = CommandPoolCreateFlags.Transient;
            _CreateCommandPool(_device, ref commandPoolCreateInfo, null, out _transientCommandPool).CheckError();

            // Create Vulkan command buffers
            CommandBufferAllocateInfo commandBufferAllocateInfo = new CommandBufferAllocateInfo(_commandPool, CommandBufferCount);
            _commandBuffers = (CommandBufferHandle*)Marshal.AllocHGlobal(CommandBufferCount * sizeof(CommandBufferHandle));
            _AllocateCommandBuffers(_device, ref commandBufferAllocateInfo, _commandBuffers).CheckError();

            // Create Vulkan command buffer fences and semaphores
            FenceCreateInfo fenceCreateInfo = new FenceCreateInfo(FenceCreateFlags.None);

            for (int i = 0; i < CommandBufferCount; ++i)
            {
                _CreateFence(_device, ref fenceCreateInfo, null, out _commandBufferFences[i]).CheckError();

                SemaphoreCreateInfo semaphoreCreateInfo = new SemaphoreCreateInfo(SemaphoreCreateFlags.None);

                _CreateSemaphore(_device, ref semaphoreCreateInfo, null, out _drawCompleteSemaphores[i]).CheckError();
            }
        }

        /*
        =============
        CreateSwapchain
        =============
        */
        private static void CreateSwapchain()
        {
            PresentMode presentMode = PresentMode.Fifo;
            // Without VSync, prefer immediate to triple buffering
            if (!EnableVSync)
            {
                bool foundImmediate = false;
                bool foundMailbox = false;
                for (int i = 0; i < presentModeCount; ++i)
                {
                    if (presentModes[i] == PresentMode.Immediate) foundImmediate = true;
                    if (presentModes[i] == PresentMode.Mailbox) foundMailbox = true;
                }
                if (foundMailbox) presentMode = PresentMode.Mailbox;
                if (foundImmediate) presentMode = PresentMode.Immediate;
            }

            Memory.Free(presentModes);

            Log.Info("Using present mode " + presentMode);

            // Create the sawpchain
            SwapchainCreateInfo swapchainCreateInfo = new SwapchainCreateInfo
            {
                Type = StructureType.SwapchainCreateInfo,
                Next = null,
                Surface = _surface,
                MinImageCount = 2,
                ImageFormat = surfaceFormats[0].Format,
                ImageColorSpace = surfaceFormats[0].ColorSpace,
                ImageExtent = new Extent2D(DisplayWidth, DisplayHeight),
                ImageUsage = ImageUsageFlags.ColorAttachment | ImageUsageFlags.TransferSrc,
                PreTransform = SurfaceTransformFlags.Identity,
                ImageArrayLayers = 1,
                ImageSharingMode = SharingMode.Exclusive,
                QueueFamilyIndexCount = 0,
                QueueFamilyIndices = null,
                PresentMode = presentMode,
                Clipped = true,
                CompositeAlpha = CompositeAlphaFlags.Opaque,
            };
            // Not all devices support opaque alpha
            if ((_surfaceCapabilities.SupportedCompositeAlpha & CompositeAlphaFlags.Opaque) == 0)
            {
                swapchainCreateInfo.CompositeAlpha = CompositeAlphaFlags.Inherit;
            }

            _swapchainFormat = surfaceFormats[0].Format;
            Memory.Free(surfaceFormats);

            _CreateSwapchainKHR(_device, ref swapchainCreateInfo, null, out _swapchain).CheckError();

            // Create the swapchain images
            _GetSwapchainImagesKHR(_device, _swapchain, ref _swapchainImageCount, null).CheckError();

            _swapchainImages = (ImageHandle*)Marshal.AllocHGlobal((int)_swapchainImageCount * sizeof(ImageHandle));
            _GetSwapchainImagesKHR(_device, _swapchain, ref _swapchainImageCount, _swapchainImages).CheckError();

            ImageViewCreateInfo imageViewCreateInfo = new ImageViewCreateInfo
            {
                Type = StructureType.ImageViewCreateInfo,
                Next = null,
                Format = _swapchainFormat,
                Components = new ComponentMapping(ComponentSwizzle.R, ComponentSwizzle.G, ComponentSwizzle.B, ComponentSwizzle.A),
                SubresourceRange = new ImageSubresourceRange(ImageAspectFlags.Color, 0, 1, 0, 1),
                ViewType = ImageViewType.Type2D,
                Flags = ImageViewCreateFlags.None,
            };

            SemaphoreCreateInfo semaphoreCreateInfo = new SemaphoreCreateInfo(SemaphoreCreateFlags.None);

            for (int i = 0; i < _swapchainImageCount; ++i)
            {
#if DEBUG
                SetObjectName(_swapchainImages[i], DebugReportObjectType.Image, "Swapchain");
#endif
                imageViewCreateInfo.Image = _swapchainImages[i];
                _CreateImageView(_device, ref imageViewCreateInfo, null, out _swapchainImageViews[i]).CheckError();
#if DEBUG
                SetObjectName(_swapchainImageViews[i], DebugReportObjectType.ImageView, "Swapchain View");
#endif
                _CreateSemaphore(_device, ref semaphoreCreateInfo, null, out _imageAcquiredSemaphores[i]).CheckError();
            }
        }

        /*
        =============
        CreateColorBuffer
        =============
        */
        private static void CreateColorBuffer()
        {
            ImageCreateInfo imageCreateInfo = new ImageCreateInfo
            {
                Type = StructureType.ImageCreateInfo,
                Next = null,
                ImageType = ImageType.Image2D,
                Format = _colorFormat,
                Extent = new Extent3D(DisplayWidth, DisplayHeight, 1),
                MipLevels = 1,
                ArrayLayers = 1,
                Samples = SampleCountFlags.Sample1,
                Tiling = ImageTiling.Optimal,
                Usage = ImageUsageFlags.ColorAttachment | ImageUsageFlags.InputAttachment | ImageUsageFlags.Sampled | ImageUsageFlags.Storage
            };

            for (int i = 0; i < ColorBufferCount; ++i)
            {
                _CreateImage(_device, ref imageCreateInfo, null, out _colorBuffers[i]).CheckError();
#if DEBUG
                SetObjectName(_colorBuffers[i], DebugReportObjectType.Image, "Color Buffer " + i);
#endif
                _GetImageMemoryRequirements(_device, _colorBuffers[i], out MemoryRequirements memoryRequirements);

                MemoryDedicatedAllocateInfo dedicatedAllocationInfo = new MemoryDedicatedAllocateInfo
                {
                    Type = StructureType.MemoryDedicatedAllocateInfo,
                    Next = null,
                    Image = _colorBuffers[i]
                };

                MemoryAllocateInfo memoryAllocateInfo = new MemoryAllocateInfo
                {
                    Type = StructureType.MemoryAllocateInfo,
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

        /*
        =============
        SetObjectName
        =============
        */
        private static void SetObjectName(ulong obj, DebugReportObjectType objectType, string name)
        {
            if (_DebugMarkerSetObjectNameEXT != null && name != null)
            {
                using (Text namePtr = new Text(name))
                {
                    DebugMarkerObjectNameInfo nameInfo = new DebugMarkerObjectNameInfo
                    {
                        Type = StructureType.DebugMarkerObjectNameInfo,
                        Next = null,
                        ObjectType = objectType,
                        Object = obj,
                        ObjectName = namePtr.Buffer
                    };
                    _DebugMarkerSetObjectNameEXT(_device, ref nameInfo).CheckError();
                }
            }
        }

        /*
        =============
        DebugMessageCallback
        =============
        */
        private static Bool32 DebugMessageCallback(DebugReportFlags flags, DebugReportObjectType objectType, ulong @object, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData)
        {
            if (objectType == DebugReportObjectType.DebugReportCallback && messageCode == 1)
            {
                return false;
            }

            string output = "[" + Utf8.ToString(layerPrefix) + "] Code " + messageCode + ": " + Utf8.ToString(message);

            switch (flags)
            {
                case DebugReportFlags.Information: Trace.TraceInformation(output); return false;
                case DebugReportFlags.Warning: Trace.TraceWarning(output); return false;
                case DebugReportFlags.PerformanceWarning: Trace.TraceWarning(output); return false;
                case DebugReportFlags.Error: Trace.TraceError(output); return true;
                case DebugReportFlags.Debug: Trace.TraceInformation(output); return false;
                default: return false;
            }
        }
    }
}