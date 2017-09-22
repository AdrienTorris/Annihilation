using System;
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
        private const FormatFeatureFlags RequiredColorBufferFeatures =
            FormatFeatureFlags.ColorAttachment |
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
        public static IntVar DisplayAdapter = new IntVar("DisplayAdapter", 0);
        public static IntVar DisplayMode = new IntVar("DisplayMode", (int)Graphics.DisplayMode.FullscreenBorderless, 0, (int)Graphics.DisplayMode.Count);
        public static IntVar Monitor = new IntVar("Monitor", 0);

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
            // Find instance extensions
            uint requiredExtensionCount = 0;
            SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, null).CheckError();

            byte*[] additionalExtensions = null;
#if DEBUG
            additionalExtensions = new byte*[]
            {
                ExtensionName.ExtDebugReport
            };
#endif
            uint additionalExtensionCount = additionalExtensions != null ? (uint)additionalExtensions.Length : 0;
            uint extensionCount = requiredExtensionCount + additionalExtensionCount;

            byte** extensionNames = Memory.AllocatePointers(extensionCount);

            SDL.VulkanGetInstanceExtensions(window, ref requiredExtensionCount, extensionNames).CheckError();

            for (int i = 0; i < additionalExtensionCount; ++i)
            {
                extensionNames[i + requiredExtensionCount] = additionalExtensions[i];
            }

            // Create instance
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
                EnabledExtensionCount = extensionCount,
                EnabledExtensionNames = extensionNames,
            };
            
#if DEBUG
            Text layerName = new Text("VK_LAYER_LUNARG_standard_validation");
            instanceCreateInfo.EnabledLayerCount = 1;
            instanceCreateInfo.EnabledLayerNames = layerName.BufferPtr;
#endif

            Instance instance = new Instance(ref instanceCreateInfo);

            Memory.Free(extensionNames);

#if DEBUG
            // Create debug report callback
            DebugReportCallbackCreateInfo debugReportCallbackCreateInfo = new DebugReportCallbackCreateInfo(
                DebugReportFlags.Error | DebugReportFlags.PerformanceWarning | DebugReportFlags.Warning,
                DebugMessageCallback
            );

            DebugReportCallback debugReportCallback = new DebugReportCallback(instance, ref debugReportCallbackCreateInfo);

            layerName.Free();
#endif

            // Create surface
            SDL.VulkanCreateSurface(window, instance.Handle, out ulong surfaceHandle).CheckError();
            Surface surface = new Surface(surfaceHandle, instance);

            // Select physical device
            instance.EnumeratePhysicalDevices(out PhysicalDevice[] physicalDevices);

            PhysicalDevice physicalDevice = physicalDevices[0];

            // Get queue families
            physicalDevice.GetQueueFamilyProperties(out QueueFamilyProperties[] queueFamilyProperties);

            // Find a queue family that supports both graphics and present
            uint queueCreateInfoCount = 0;

            uint graphicsPresentQueueFamily = uint.MaxValue;
            for (uint i = 0; i < queueFamilyProperties.Length; ++i)
            {
                if (!physicalDevice.GetSurfaceSupportKHR(i, surface.Handle))
                {
                    continue;
                }

                if ((queueFamilyProperties[i].QueueFlags & QueueFlags.Graphics) == 0)
                {
                    continue;
                }

                graphicsPresentQueueFamily = i;
                queueCreateInfoCount++;
                break;
            }

            if (graphicsPresentQueueFamily == uint.MaxValue)
            {
                throw new Exception("Could not find a queue family with graphics and present support.");
            }

            // If possible, find a different queue family for compute
            uint computeQueueFamily = graphicsPresentQueueFamily;
            for (uint i = 0; i < queueFamilyProperties.Length; ++i)
            {
                if (i == graphicsPresentQueueFamily)
                {
                    continue;
                }

                if (!physicalDevice.GetSurfaceSupportKHR(i, surface.Handle))
                {
                    continue;
                }

                if ((queueFamilyProperties[i].QueueFlags & QueueFlags.Compute) == 0)
                {
                    continue;
                }

                computeQueueFamily = i;
                queueCreateInfoCount++;
                break;
            }

            DeviceQueueCreateInfo* queueCreateInfos = (DeviceQueueCreateInfo*)Memory.Allocate<DeviceQueueCreateInfo>(queueCreateInfoCount);
            queueCreateInfos[0] = new DeviceQueueCreateInfo
            {
                Type = StructureType.DeviceQueueCreateInfo,
                QueueFamilyIndex = graphicsPresentQueueFamily,
                QueueCount = 1,
                QueuePriorities = null
            };

            if (computeQueueFamily != graphicsPresentQueueFamily)
            {
                queueCreateInfos[1] = new DeviceQueueCreateInfo
                {
                    Type = StructureType.DeviceQueueCreateInfo,
                    QueueFamilyIndex = computeQueueFamily,
                    QueueCount = 1,
                    QueuePriorities = null
                };
            }

            // Get the device features
            physicalDevice.GetFeatures(out PhysicalDeviceFeatures physicalDeviceFeatures);

            PhysicalDeviceFeatures features = new PhysicalDeviceFeatures
            {
                GeometryShader = physicalDeviceFeatures.GeometryShader,
                TessellationShader = physicalDeviceFeatures.TessellationShader,
                MultiDrawIndirect = physicalDeviceFeatures.MultiDrawIndirect,
                ShaderStorageImageExtendedFormats = physicalDeviceFeatures.ShaderStorageImageExtendedFormats,
                SamplerAnisotropy = physicalDeviceFeatures.SamplerAnisotropy
            };

            // Allocate extension names
            uint deviceExtensionCount = 1;
#if DEBUG
            deviceExtensionCount = 2;
#endif 
            byte** deviceExtensions = Memory.AllocatePointers(deviceExtensionCount);
            deviceExtensions[0] = ExtensionName.Swapchain;
#if DEBUG
            deviceExtensions[1] = ExtensionName.DebugMarker;
#endif

            DeviceCreateInfo deviceCreateInfo = new DeviceCreateInfo
            {
                Type = StructureType.DeviceCreateInfo,
                QueueCreateInfoCount = queueCreateInfoCount,
                QueueCreateInfos = queueCreateInfos,
                EnabledLayerCount = 0,
                EnabledLayerNames = null,
                EnabledExtensionCount = deviceExtensionCount,
                EnabledExtensionNames = deviceExtensions,
                EnabledFeatures = &features
            };

            Device device = new Device(physicalDevice, ref deviceCreateInfo);

            Memory.Free(queueCreateInfos);
            Memory.Free(deviceExtensions);

            // Get graphics and present queue
            device.GetQueue(graphicsPresentQueueFamily, 0, out Queue graphicsPresentQueue);

            // Get compute queue
            device.GetQueue(computeQueueFamily, 0, out Queue computeQueue);

            // Find color buffer format
            FormatProperties formatProperties;
            Format colorFormat = Format.R8G8B8A8UNorm;

            if (physicalDeviceFeatures.ShaderStorageImageExtendedFormats)
            {
                physicalDevice.GetFormatProperties(Format.A2B10G10R10UNormPack32, out formatProperties);
                bool a2b10g10r10Support = (formatProperties.OptimalTilingFeatures & RequiredColorBufferFeatures) == RequiredColorBufferFeatures;

                if (a2b10g10r10Support)
                {
                    colorFormat = Format.A2B10G10R10UNormPack32;
                }
            }

            // Find depth buffer format
            physicalDevice.GetFormatProperties(Format.X8D24UNormPack32, out formatProperties);
            bool x8d24Support = (formatProperties.OptimalTilingFeatures & FormatFeatureFlags.DepthStencilAttachment) != 0;
            physicalDevice.GetFormatProperties(Format.D32SFloat, out formatProperties);
            bool d32Support = (formatProperties.OptimalTilingFeatures & FormatFeatureFlags.DepthStencilAttachment) != 0;

            Format depthFormat = Format.D16UNorm;
            if (x8d24Support)
            {
                depthFormat = Format.X8D24UNormPack32;
            }
            else if (d32Support)
            {
                depthFormat = Format.D32SFloat;
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
        CreateDeviceAndQueues
        =============
        */
        private static void CreateDeviceAndQueues()
        {
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