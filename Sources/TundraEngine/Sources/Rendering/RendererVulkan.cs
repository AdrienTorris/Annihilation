using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TundraEngine.Rendering.Vulkan;
using static TundraEngine.Rendering.Vulkan.Vulkan;

namespace TundraEngine.Rendering
{
    public struct RendererCreateInfo
    {
        public string ApplicationName;
        public bool EnableValidation;
    }

    public unsafe class RendererVulkan : IRenderer
    {
        private Instance instance;

        public RendererVulkan(ref RendererCreateInfo createInfo)
        {
            // Allocate strings
            IntPtr applicationName = Marshal.StringToHGlobalAnsi(createInfo.ApplicationName);
            IntPtr engineName = Marshal.StringToHGlobalAnsi("Tundra Engine");
            IntPtr[] layers = new IntPtr[]
            {
                Marshal.StringToHGlobalAnsi("VK_LAYER_LUNARG_standard_validation")
            };
            IntPtr[] extensions = new IntPtr[]
            {
                Marshal.StringToHGlobalAnsi("VK_KHR_surface"),
                Marshal.StringToHGlobalAnsi("VK_KHR_win32_surface"),
                Marshal.StringToHGlobalAnsi("VK_EXT_debug_report"),
            };

            List<IntPtr> strings = new List<IntPtr>(6);
            strings.Add(applicationName);
            strings.Add(engineName);
            strings.AddRange(layers);
            strings.AddRange(extensions);

            // Application info
            var applicationInfo = new ApplicationInfo
            {
                Type = StructureType.ApplicationInfo,
                ApplicationName = applicationName,
                ApplicationVersion = new Version(1, 0, 0),
                EngineName = engineName,
                EngineVersion = new Version(1, 0, 0),
                ApiVersion = new Version(1, 0, 0)
            };
            
            // Create the instance
            fixed (void* layersPointer = &layers[0])
            fixed (void* extensionsPointer = &extensions[0])
            {
                var instanceCreateInfo = new InstanceCreateInfo
                {
                    Type = StructureType.InstanceCreateInfo,
                    ApplicationInfo = new IntPtr(&applicationInfo),
                    EnabledExtensionCount = (uint)extensions.Length,
                    EnabledExtensionNames = new IntPtr(extensionsPointer),
                };

                if (createInfo.EnableValidation)
                {
                    instanceCreateInfo.EnabledLayerCount = (uint)layers.Length;
                    instanceCreateInfo.EnabledLayerNames = new IntPtr(layersPointer);
                }

                CreateInstance(ref instanceCreateInfo, null, out instance);
            }
            
            // Free memory
            foreach(var pointer in strings)
            {
                Marshal.FreeHGlobal(pointer);
            }
        }

        public void Dispose()
        {
            instance.Destroy();
        }

        public void Render()
        {

        }
    }
}

//using System;
//using System.Runtime.InteropServices;
//using System.Diagnostics;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//using TundraEngine.Windowing;

//namespace TundraEngine.Rendering
//{
//    public class RendererVulkan : IRenderer
//    {
//        private Instance _instance;
//        private Surface _surface;
//        private PhysicalDevice _physicalDevice;
//        private Device _device;
//        private Queue _graphicsQueue;
//        private Queue _computeQueue;
//        private Queue _transferQueue;
//        private Queue _presentQueue;
//        private Swapchain _swapChain;
//        private Extent2D _swapChainExtent;
//        private Image[] _swapChainImages;
//        private ImageView[] _swapChainImageViews;
//        private Framebuffer[] _swapChainFramebuffers;
//        private RenderPass _renderPass;
//        private PipelineLayout _pipelineLayout;
//        private Pipeline _graphicsPipeline;
//        private CommandPool _commandPool;
//        private CommandBuffer[] _commandBuffers;
//        private Semaphore _imageAvailableSemaphore;
//        private Semaphore _renderCompleteSemaphore;

//        // Swapchain details
//        private SurfaceCapabilities _surfaceCapabilities;
//        private SurfaceFormat[] _surfaceFormats;
//        private PresentMode[] _surfacePresentModes;

//        // Queue families
//        private uint _graphicsQueueFamilyIndex = uint.MaxValue;
//        private uint _computeQueueFamilyIndex = uint.MaxValue;
//        private uint _transferQueueFamilyIndex = uint.MaxValue;
//        private uint _presentQueueFamilyIndex = uint.MaxValue;

//        private bool _wasDisposed;

//#if DEBUG
//        private DebugReportCallback _debugCallback;
//#endif
//        private const float DefaultQueuePriority = 1f;

//#if DEBUG
//        private static readonly string[] InstanceLayers = new string[]
//        {
//            "VK_LAYER_LUNARG_standard_validation"
//        };
//#endif
//        private static readonly string[] InstanceExtensions = new string[]
//        {
//                KhrSurface.ExtensionName,
//                Game.Instance.Window.WindowManagerInfo.Type == WindowManagerType.Windows
//                ? KhrWin32Surface.ExtensionName
//                : Game.Instance.Window.WindowManagerInfo.Type == WindowManagerType.X11
//                ? KhrXcbSurface.ExtensionName
//                : Game.Instance.Window.WindowManagerInfo.Type == WindowManagerType.Wayland
//                ? KhrWaylandSurface.ExtensionName
//                : string.Empty,
//#if DEBUG
//                ExtDebugReport.ExtensionName
//#endif
//        };

//        private static readonly string[] DeviceExtensions = new string[]
//        {
//            KhrSwapchain.ExtensionName
//        };

//        private static readonly PhysicalDeviceFeatures DeviceFeatures = new PhysicalDeviceFeatures
//        {
//            MultiDrawIndirect = true,
//            FillModeNonSolid = true
//        };

//        private const Format SurfaceFormat = Format.B8G8R8A8UNorm;
//        private const ColorSpace SurfaceColorSpace = ColorSpace.SrgbNonlinear;
//        private const PresentMode PresentModeType = PresentMode.Fifo;

//        public RendererVulkan()
//        {
//            CreateInstance();
//#if DEBUG
//            CreateDebugCallback();
//#endif
//            CreateSurface();
//            SelectPhysicalDevice();
//            CreateLogicalDeviceAndQueues();
//            CreateSwapChain();
//            CreateImageViews();
//            CreateRenderPass();
//            CreateGraphicsPipeline();
//            CreateFramebuffers();
//            CreateCommandPool();
//            CreateCommandBuffers();
//            CreateSemaphores();

//            // Test allocation library

//        }

//        public void Render()
//        {
//            uint imageIndex = _swapChain.AcquireNextImage(ulong.MaxValue, _imageAvailableSemaphore, null);

//            _graphicsQueue.Submit(new SubmitInfo
//            {
//                WaitSemaphores = new Semaphore[] { _imageAvailableSemaphore },
//                WaitDestinationStageMask = new PipelineStageFlags[] { PipelineStageFlags.ColorAttachmentOutput },
//                CommandBuffers = new CommandBuffer[] { _commandBuffers[(int)imageIndex] },
//                SignalSemaphores = new Semaphore[] { _renderCompleteSemaphore }
//            },
//            null);

//            _presentQueue.Present(new PresentInfo
//            {
//                WaitSemaphores = new Semaphore[] { _renderCompleteSemaphore },
//                Swapchains = new Swapchain[] { _swapChain },
//                ImageIndices = new uint[] { imageIndex },
//                Results = null
//            });

//            _presentQueue.WaitIdle();
//        }

//        private void Dispose(bool disposing)
//        {
//            if (!_wasDisposed)
//            {
//                if (disposing) { }

//                _renderCompleteSemaphore.Dispose();
//                _renderCompleteSemaphore = null;
//                _imageAvailableSemaphore.Dispose();
//                _imageAvailableSemaphore = null;
//                _commandPool.Dispose();
//                _commandPool = null;
//                for (int i = _swapChainFramebuffers.Length - 1; i >= 0; --i)
//                {
//                    _swapChainFramebuffers[i].Dispose();
//                    _swapChainFramebuffers[i] = null;
//                }
//                _graphicsPipeline.Dispose();
//                _graphicsPipeline = null;
//                _pipelineLayout.Dispose();
//                _pipelineLayout = null;
//                _renderPass.Dispose();
//                _renderPass = null;
//                for (int i = _swapChainImageViews.Length - 1; i >= 0; --i)
//                {
//                    _swapChainImageViews[i].Dispose();
//                    _swapChainImageViews[i] = null;
//                }
//                _swapChain.Dispose();
//                _swapChain = null;
//                _device.Dispose();
//                _device = null;
//                _surface.Dispose();
//                _surface = null;
//#if DEBUG
//                _debugCallback.Dispose();
//                _debugCallback = null;
//#endif
//                _instance.Dispose();
//                _instance = null;

//                _wasDisposed = true;
//            }
//        }

//#if DEBUG
//        private static readonly SharpVk.Interop.DebugReportCallbackDelegate DebugReportDelegate = DebugCallback;

//        private static Bool32 DebugCallback(DebugReportFlags flags, DebugReportObjectType objectType, ulong @object, Size location, int messageCode, string layerPrefix, string message, IntPtr userData)
//        {
//            Trace.WriteLine("[Vulkan] " + flags + ": " + message);

//            return false;
//        }
//#endif

//        private void CreateInstance()
//        {
//            // Check if we support the desired extensions
//            ExtensionProperties[] availableExtensions = Instance.EnumerateExtensionProperties(null);
//            HashSet<string> availableExtensionNames = new HashSet<string>(availableExtensions.Length);
//            foreach (var extension in availableExtensions)
//            {
//                availableExtensionNames.Add(extension.ExtensionName);
//            }
//            foreach (string extension in InstanceExtensions)
//            {
//                Assert.IsTrue(availableExtensionNames.Contains(extension), "Extension " + extension + " is not supported.");
//            }

//#if DEBUG
//            // Check validation layer support
//            LayerProperties[] availableLayers = Instance.EnumerateLayerProperties();
//            HashSet<string> availableLayerNames = new HashSet<string>(availableLayers.Length);
//            foreach (var layer in availableLayers)
//            {
//                availableLayerNames.Add(layer.LayerName);
//            }
//            foreach (string layer in InstanceLayers)
//            {
//                Assert.IsTrue(availableLayerNames.Contains(layer), "Validation layer " + layer + " is not supported.");
//            }
//#endif

//            // Create the instance
//            Version version = Game.Instance.Settings.Version;
//            _instance = Instance.Create(new InstanceCreateInfo
//            {
//                ApplicationInfo = new ApplicationInfo
//                {
//                    ApplicationName = Game.Instance.Settings.Name,
//                    ApplicationVersion = new SharpVk.Version(version.Major, version.Minor, version.Patch),
//                    EngineName = "Tundra Engine",
//                    EngineVersion = new SharpVk.Version(1, 0, 0)
//                },
//                EnabledExtensionNames = InstanceExtensions,
//#if DEBUG
//                EnabledLayerNames = Game.Instance.Settings.RendererSettings.VulkanSettings.EnableValidation ? InstanceLayers : null
//#endif
//            });
//            Assert.IsNotNull(_instance, "Could not create Vulkan instance.");
//        }

//#if DEBUG
//        private void CreateDebugCallback()
//        {
//            if (Game.Instance.Settings.RendererSettings.VulkanSettings.EnableValidation == false)
//            {
//                return;
//            }

//            _debugCallback = _instance.CreateDebugReportCallback(new DebugReportCallbackCreateInfo
//            {
//                Flags = Game.Instance.Settings.RendererSettings.VulkanSettings.DebugFlags,
//                PfnCallback = DebugReportDelegate
//            });
//            Assert.IsNotNull(_debugCallback, "Could not create debug callback.");
//        }
//#endif

//        private void CreateSurface()
//        {
//            WindowManagerInfo windowManagerInfo = Game.Instance.Window.WindowManagerInfo;

//            switch (windowManagerInfo.Type)
//            {
//                case WindowManagerType.Windows:
//                _surface = _instance.CreateWin32Surface(new Win32SurfaceCreateInfo
//                {
//                    Hwnd = windowManagerInfo.Windows.HWindow,
//                    Hinstance = windowManagerInfo.Windows.HInstance
//                });
//                break;
//                case WindowManagerType.X11:
//                _surface = _instance.CreateXcbSurface(new XcbSurfaceCreateInfo
//                {
//                    Window = windowManagerInfo.X11.Window,
//                    Connection = windowManagerInfo.X11.Connection
//                });
//                break;
//                case WindowManagerType.Wayland:
//                _surface = _instance.CreateWaylandSurface(new WaylandSurfaceCreateInfo
//                {
//                    Surface = windowManagerInfo.Wayland.Surface,
//                    Display = windowManagerInfo.Wayland.Display
//                });
//                break;
//            }
//            Assert.IsNotNull(_surface, "Could not create surface.");
//        }

//        // TODO: Support multiple GPUs (SLI/Crossfire)
//        private void SelectPhysicalDevice()
//        {
//            PhysicalDevice[] physicalDevices = _instance.EnumeratePhysicalDevices();
//            Assert.IsTrue(physicalDevices.Length > 0, "No GPU found.");

//            // Select the first suitable device
//            foreach (var gpu in physicalDevices)
//            {
//                if (IsGPUSuitable(gpu))
//                {
//                    _physicalDevice = gpu;
//                    break;
//                }
//            }
//            Assert.IsNotNull(_physicalDevice, "Couldn't find a suitable GPU.");

//            // TODO: Don't assume queue family 0 has graphics, present, compute and transfer
//            bool IsGPUSuitable(PhysicalDevice gpu)
//            {
//                if (!AreFeaturesSupported() || !AreExtensionsSupported() || !IsSwapChainSupported())
//                {
//                    return false;
//                }

//                QueueFamilyProperties[] queueFamilies = gpu.GetQueueFamilyProperties();
//                Assert.IsTrue(queueFamilies.Length > 0, "No queue families found.");

//                // The first queue family is the one with graphics + present
//                _graphicsQueueFamilyIndex = 0;
//                _presentQueueFamilyIndex = 0;

//                bool isPresentSurpported = gpu.GetSurfaceSupport(_presentQueueFamilyIndex, _surface);
//                Assert.IsTrue(isPresentSurpported, "Present is not supported on queue family " + _presentQueueFamilyIndex);

//                // Pick the first queue family that has compute and is not the graphics one
//                for (uint i = 1; i < queueFamilies.Length; ++i)
//                {
//                    if (queueFamilies[i].QueueFlags.Has(QueueFlags.Compute))
//                    {
//                        _computeQueueFamilyIndex = i;
//                    }
//                }

//                // If we can't find one (Intel), put compute on first queue family
//                if (_computeQueueFamilyIndex == uint.MaxValue)
//                {
//                    _computeQueueFamilyIndex = 0;
//                }

//                // Transfer queue is different from both graphics and compute queue
//                for (uint i = 1; i < queueFamilies.Length; ++i)
//                {
//                    if (queueFamilies[i].QueueFlags.Has(QueueFlags.Transfer) &&
//                        i != _graphicsQueueFamilyIndex &&
//                        i != _computeQueueFamilyIndex)
//                    {
//                        _transferQueueFamilyIndex = i;
//                    }
//                }

//                // If we can't find one (Intel), put compute on first queue family
//                if (_transferQueueFamilyIndex == uint.MaxValue)
//                {
//                    _transferQueueFamilyIndex = 0;
//                }

//                return
//                    _graphicsQueueFamilyIndex != uint.MaxValue &&
//                    _presentQueueFamilyIndex != uint.MaxValue &&
//                    _computeQueueFamilyIndex != uint.MaxValue &&
//                    _transferQueueFamilyIndex != uint.MaxValue;

//                bool AreFeaturesSupported()
//                {
//                    PhysicalDeviceFeatures availableFeatures = gpu.GetFeatures();
//                    bool ok =
//                        !(DeviceFeatures.AlphaToOne && !availableFeatures.AlphaToOne) &&
//                        !(DeviceFeatures.DepthBiasClamp && !availableFeatures.DepthBiasClamp) &&
//                        !(DeviceFeatures.DepthBounds && !availableFeatures.DepthBounds) &&
//                        !(DeviceFeatures.DepthClamp && !availableFeatures.DepthClamp) &&
//                        !(DeviceFeatures.DrawIndirectFirstInstance && !availableFeatures.DrawIndirectFirstInstance) &&
//                        !(DeviceFeatures.DualSourceBlend && !availableFeatures.DualSourceBlend) &&
//                        !(DeviceFeatures.FillModeNonSolid && !availableFeatures.FillModeNonSolid) &&
//                        !(DeviceFeatures.FragmentStoresAndAtomics && !availableFeatures.FragmentStoresAndAtomics) &&
//                        !(DeviceFeatures.FullDrawIndexUInt32 && !availableFeatures.FullDrawIndexUInt32) &&
//                        !(DeviceFeatures.GeometryShader && !availableFeatures.GeometryShader) &&
//                        !(DeviceFeatures.ImageCubeArray && !availableFeatures.ImageCubeArray) &&
//                        !(DeviceFeatures.IndependentBlend && !availableFeatures.IndependentBlend) &&
//                        !(DeviceFeatures.InheritedQueries && !availableFeatures.InheritedQueries) &&
//                        !(DeviceFeatures.LargePoints && !availableFeatures.LargePoints) &&
//                        !(DeviceFeatures.LogicOp && !availableFeatures.LogicOp) &&
//                        !(DeviceFeatures.MultiDrawIndirect && !availableFeatures.MultiDrawIndirect) &&
//                        !(DeviceFeatures.MultiViewport && !availableFeatures.MultiViewport) &&
//                        !(DeviceFeatures.OcclusionQueryPrecise && !availableFeatures.OcclusionQueryPrecise) &&
//                        !(DeviceFeatures.PipelineStatisticsQuery && !availableFeatures.PipelineStatisticsQuery) &&
//                        !(DeviceFeatures.RobustBufferAccess && !availableFeatures.RobustBufferAccess) &&
//                        !(DeviceFeatures.SamplerAnisotropy && !availableFeatures.SamplerAnisotropy) &&
//                        !(DeviceFeatures.SampleRateShading && !availableFeatures.SampleRateShading) &&
//                        !(DeviceFeatures.ShaderClipDistance && !availableFeatures.ShaderClipDistance) &&
//                        !(DeviceFeatures.ShaderCullDistance && !availableFeatures.ShaderCullDistance) &&
//                        !(DeviceFeatures.ShaderFloat64 && !availableFeatures.ShaderFloat64) &&
//                        !(DeviceFeatures.ShaderImageGatherExtended && !availableFeatures.ShaderImageGatherExtended) &&
//                        !(DeviceFeatures.ShaderInt16 && !availableFeatures.ShaderInt16) &&
//                        !(DeviceFeatures.ShaderInt64 && !availableFeatures.ShaderInt64) &&
//                        !(DeviceFeatures.ShaderResourceMinLod && !availableFeatures.ShaderResourceMinLod) &&
//                        !(DeviceFeatures.ShaderResourceResidency && !availableFeatures.ShaderResourceResidency) &&
//                        !(DeviceFeatures.ShaderSampledImageArrayDynamicIndexing && !availableFeatures.ShaderSampledImageArrayDynamicIndexing) &&
//                        !(DeviceFeatures.ShaderStorageBufferArrayDynamicIndexing && !availableFeatures.ShaderStorageBufferArrayDynamicIndexing) &&
//                        !(DeviceFeatures.ShaderStorageImageArrayDynamicIndexing && !availableFeatures.ShaderStorageImageArrayDynamicIndexing) &&
//                        !(DeviceFeatures.ShaderStorageImageExtendedFormats && !availableFeatures.ShaderStorageImageExtendedFormats) &&
//                        !(DeviceFeatures.ShaderStorageImageMultisample && !availableFeatures.ShaderStorageImageMultisample) &&
//                        !(DeviceFeatures.ShaderStorageImageReadWithoutFormat && !availableFeatures.ShaderStorageImageReadWithoutFormat) &&
//                        !(DeviceFeatures.ShaderStorageImageWriteWithoutFormat && !availableFeatures.ShaderStorageImageWriteWithoutFormat) &&
//                        !(DeviceFeatures.ShaderTessellationAndGeometryPointSize && !availableFeatures.ShaderTessellationAndGeometryPointSize) &&
//                        !(DeviceFeatures.ShaderUniformBufferArrayDynamicIndexing && !availableFeatures.ShaderUniformBufferArrayDynamicIndexing) &&
//                        !(DeviceFeatures.SparseBinding && !availableFeatures.SparseBinding) &&
//                        !(DeviceFeatures.SparseResidency16Samples && !availableFeatures.SparseResidency16Samples) &&
//                        !(DeviceFeatures.SparseResidency2Samples && !availableFeatures.SparseResidency2Samples) &&
//                        !(DeviceFeatures.SparseResidency4Samples && !availableFeatures.SparseResidency4Samples) &&
//                        !(DeviceFeatures.SparseResidency8Samples && !availableFeatures.SparseResidency8Samples) &&
//                        !(DeviceFeatures.SparseResidencyAliased && !availableFeatures.SparseResidencyAliased) &&
//                        !(DeviceFeatures.SparseResidencyBuffer && !availableFeatures.SparseResidencyBuffer) &&
//                        !(DeviceFeatures.SparseResidencyImage2D && !availableFeatures.SparseResidencyImage2D) &&
//                        !(DeviceFeatures.SparseResidencyImage3D && !availableFeatures.SparseResidencyImage3D) &&
//                        !(DeviceFeatures.TessellationShader && !availableFeatures.TessellationShader) &&
//                        !(DeviceFeatures.TextureCompressionASTC_LDR && !availableFeatures.TextureCompressionASTC_LDR) &&
//                        !(DeviceFeatures.TextureCompressionBC && !availableFeatures.TextureCompressionBC) &&
//                        !(DeviceFeatures.TextureCompressionETC2 && !availableFeatures.TextureCompressionETC2) &&
//                        !(DeviceFeatures.VariableMultisampleRate && !availableFeatures.VariableMultisampleRate) &&
//                        !(DeviceFeatures.VertexPipelineStoresAndAtomics && !availableFeatures.VertexPipelineStoresAndAtomics) &&
//                        !(DeviceFeatures.WideLines && !availableFeatures.WideLines);

//                    Assert.IsTrue(ok, "Device features are not supported on this GPU.");
//                    return ok;
//                }

//                bool AreExtensionsSupported()
//                {
//                    ExtensionProperties[] availableExtensions = gpu.EnumerateDeviceExtensionProperties(null);

//                    HashSet<string> extensionSet = new HashSet<string>(DeviceExtensions);

//                    foreach (var extension in availableExtensions)
//                    {
//                        extensionSet.Remove(extension.ExtensionName);
//                    }

//                    bool ok = extensionSet.Count == 0;
//                    Assert.IsTrue(ok, "Device extensions are not supported on this GPU.");
//                    return ok;
//                }

//                bool IsSwapChainSupported()
//                {
//                    _surfaceCapabilities = gpu.GetSurfaceCapabilities(_surface);
//                    _surfaceFormats = gpu.GetSurfaceFormats(_surface);
//                    _surfacePresentModes = gpu.GetSurfacePresentModes(_surface);

//                    bool ok = _surfaceFormats.Length != 0 && _surfacePresentModes.Length != 0;
//                    Assert.IsTrue(ok, "Swapchain is not supported.");

//                    return ok;
//                }
//            }
//        }

//        private void CreateLogicalDeviceAndQueues()
//        {
//            // Queue families
//            QueueFamilyProperties[] queueFamilyProperties = _physicalDevice.GetQueueFamilyProperties();
//            Assert.IsTrue(queueFamilyProperties.Length > 0, "No queue family properties found.");

//            // Queue infos
//            DeviceQueueCreateInfo[] queueCreateInfos = new DeviceQueueCreateInfo[]
//            {
//                // Graphics and present queue
//                new DeviceQueueCreateInfo
//                {
//                    QueueFamilyIndex = _graphicsQueueFamilyIndex,
//                    QueuePriorities = new float[1] { DefaultQueuePriority }
//                },
//                // Compute queue
//                new DeviceQueueCreateInfo
//                {
//                    QueueFamilyIndex = _computeQueueFamilyIndex,
//                    QueuePriorities = new float[1] { DefaultQueuePriority }
//                },
//                // Transfer queue
//                new DeviceQueueCreateInfo
//                {
//                    QueueFamilyIndex = _transferQueueFamilyIndex,
//                    QueuePriorities = new float[1] { DefaultQueuePriority }
//                }
//            };

//            // Create the logical device
//            _device = _physicalDevice.CreateDevice(new DeviceCreateInfo
//            {
//                QueueCreateInfos = queueCreateInfos,
//                EnabledFeatures = DeviceFeatures,
//                EnabledExtensionNames = DeviceExtensions,
//            });
//            Assert.IsNotNull(_device, "Could not create logical device.");

//            // Set the queue handles
//            _graphicsQueue = _device.GetQueue(_graphicsQueueFamilyIndex, 0);
//            _computeQueue = _device.GetQueue(_computeQueueFamilyIndex, 0);
//            _transferQueue = _device.GetQueue(_transferQueueFamilyIndex, 0);
//            _presentQueue = _device.GetQueue(_presentQueueFamilyIndex, 0);
//        }

//        private void CreateSwapChain()
//        {
//            uint imageCount = _surfaceCapabilities.MinImageCount + 1;
//            if (_surfaceCapabilities.MaxImageCount > 0 &&
//                imageCount > _surfaceCapabilities.MaxImageCount)
//            {
//                imageCount = _surfaceCapabilities.MaxImageCount;
//            }

//            ChoosePresentMode(out PresentMode presentMode);
//            ChooseExtent();

//            _swapChain = _device.CreateSwapchain(new SwapchainCreateInfo
//            {
//                Surface = _surface,
//                MinImageCount = imageCount,
//                ImageFormat = SurfaceFormat,
//                ImageColorSpace = SurfaceColorSpace,
//                ImageExtent = _swapChainExtent,
//                ImageArrayLayers = 1,
//                ImageUsage = ImageUsageFlags.ColorAttachment | ImageUsageFlags.TransferSource,
//                ImageSharingMode = SharingMode.Exclusive,
//                PreTransform = _surfaceCapabilities.CurrentTransform,
//                CompositeAlpha = CompositeAlphaFlags.Opaque,
//                PresentMode = presentMode,
//                Clipped = true,
//                OldSwapchain = null,
//            });
//            Assert.IsNotNull(_swapChain, "Could not create swap chain.");

//            _swapChainImages = _swapChain.GetImages();
//            Assert.IsTrue(_swapChainImages.Length > 0, "Could not get any images from the swap chain.");

//            void ChooseExtent()
//            {
//                if (_surfaceCapabilities.CurrentExtent.Width != uint.MaxValue)
//                {
//                    _swapChainExtent = _surfaceCapabilities.CurrentExtent;
//                }
//                else
//                {
//                    _swapChainExtent = new Extent2D(Game.Instance.Window.Width, Game.Instance.Window.Height);
//                    _swapChainExtent.Width = Math.Max(_surfaceCapabilities.MinImageExtent.Width, Math.Min(_surfaceCapabilities.MaxImageExtent.Width, _swapChainExtent.Width));
//                    _swapChainExtent.Height = Math.Max(_surfaceCapabilities.MinImageExtent.Height, Math.Min(_surfaceCapabilities.MaxImageExtent.Height, _swapChainExtent.Height));
//                }
//            }

//            void ChoosePresentMode(out PresentMode mode)
//            {
//                PresentMode desiredMode = Game.Instance.Settings.RendererSettings.VulkanSettings.PresentMode;
//                foreach (var presMode in _surfacePresentModes)
//                {
//                    if (presMode == desiredMode)
//                    {
//                        mode = desiredMode;
//                    }
//                }
//                mode = PresentMode.Fifo;
//            }
//        }

//        private void CreateImageViews()
//        {
//            _swapChainImageViews = new ImageView[_swapChainImages.Length];
//            for (int i = 0; i < _swapChainImages.Length; ++i)
//            {
//                _swapChainImageViews[i] = _device.CreateImageView(new ImageViewCreateInfo
//                {
//                    Image = _swapChainImages[i],
//                    ViewType = ImageViewType.ImageView2d,
//                    Format = SurfaceFormat,
//                    Components = ComponentMapping.Identity,
//                    SubresourceRange = new ImageSubresourceRange
//                    {
//                        AspectMask = ImageAspectFlags.Color,
//                        BaseMipLevel = 0,
//                        LevelCount = 1,
//                        BaseArrayLayer = 0,
//                        LayerCount = 1
//                    }
//                });
//                Assert.IsNotNull(_swapChainImageViews[i], "Could not create image view.");
//            }
//        }

//        private void CreateRenderPass()
//        {
//            AttachmentDescription colorAttachment = new AttachmentDescription
//            {
//                Format = SurfaceFormat,
//                Samples = SampleCountFlags.SampleCount1,
//                LoadOp = AttachmentLoadOp.Clear,
//                StoreOp = AttachmentStoreOp.Store,
//                StencilLoadOp = AttachmentLoadOp.DontCare,
//                StencilStoreOp = AttachmentStoreOp.DontCare,
//                InitialLayout = ImageLayout.Undefined,
//                FinalLayout = ImageLayout.PresentSource
//            };

//            AttachmentReference colorAttachmentRef = new AttachmentReference
//            {
//                Attachment = 0,
//                Layout = ImageLayout.ColorAttachmentOptimal
//            };

//            AttachmentReference depthAttachmentRef = new AttachmentReference
//            {
//                Attachment = SharpVk.Constants.AttachmentUnused,
//            };

//            SubpassDescription subpass = new SubpassDescription
//            {
//                PipelineBindPoint = PipelineBindPoint.Graphics,
//                ColorAttachments = new AttachmentReference[] { colorAttachmentRef },
//                DepthStencilAttachment = depthAttachmentRef
//            };

//            SubpassDependency dependency = new SubpassDependency
//            {
//                SourceSubpass = SharpVk.Constants.SubpassExternal,
//                DestinationSubpass = 0,
//                SourceStageMask = PipelineStageFlags.ColorAttachmentOutput,
//                SourceAccessMask = 0,
//                DestinationStageMask = PipelineStageFlags.ColorAttachmentOutput,
//                DestinationAccessMask = AccessFlags.ColorAttachmentRead | AccessFlags.ColorAttachmentWrite
//            };

//            _renderPass = _device.CreateRenderPass(new RenderPassCreateInfo
//            {
//                Attachments = new AttachmentDescription[] { colorAttachment },
//                Subpasses = new SubpassDescription[] { subpass },
//                Dependencies = new SubpassDependency[] { dependency }
//            });
//            Assert.IsNotNull(_renderPass, "Could not create render pass.");
//        }

//        private void CreateGraphicsPipeline()
//        {
//            ResourceSystem.LoadBinary("Compiled/Shaders/vert.spv", out byte[] vertCode);
//            ResourceSystem.LoadBinary("Compiled/Shaders/frag.spv", out byte[] fragCode);

//            ShaderModule vertShaderModule = CreateShaderModule(vertCode);
//            ShaderModule fragShaderModule = CreateShaderModule(fragCode);

//            PipelineShaderStageCreateInfo vertStageInfo = new PipelineShaderStageCreateInfo
//            {
//                Stage = ShaderStageFlags.Vertex,
//                Module = vertShaderModule,
//                Name = "main"
//            };

//            PipelineShaderStageCreateInfo fragStageInfo = new PipelineShaderStageCreateInfo
//            {
//                Stage = ShaderStageFlags.Fragment,
//                Module = fragShaderModule,
//                Name = "main"
//            };

//            PipelineShaderStageCreateInfo[] shaderStages = { vertStageInfo, fragStageInfo };

//            PipelineVertexInputStateCreateInfo vertexInputInfo = new PipelineVertexInputStateCreateInfo
//            {
//                VertexBindingDescriptions = new VertexInputBindingDescription[] { Vertex.BindingDescription },
//                VertexAttributeDescriptions = Vertex.AttributeDescriptions
//            };

//            PipelineInputAssemblyStateCreateInfo inputAssembly = new PipelineInputAssemblyStateCreateInfo
//            {
//                Topology = PrimitiveTopology.TriangleList,
//                PrimitiveRestartEnable = false
//            };

//            Viewport viewport = new Viewport
//            {
//                X = 0f,
//                Y = 0f,
//                Width = _swapChainExtent.Width,
//                Height = _swapChainExtent.Height,
//                MinDepth = 0f,
//                MaxDepth = 1f
//            };

//            Rect2D scissor = new Rect2D
//            {
//                Offset = new Offset2D(0, 0),
//                Extent = _swapChainExtent
//            };

//            PipelineViewportStateCreateInfo viewportStateInfo = new PipelineViewportStateCreateInfo
//            {
//                Viewports = new Viewport[] { viewport },
//                Scissors = new Rect2D[] { scissor }
//            };

//            PipelineRasterizationStateCreateInfo rasterizerInfo = new PipelineRasterizationStateCreateInfo
//            {
//                DepthClampEnable = false,
//                RasterizerDiscardEnable = false,
//                PolygonMode = PolygonMode.Fill,
//                LineWidth = 1f,
//                CullMode = CullModeFlags.Back,
//                FrontFace = FrontFace.Clockwise,
//                DepthBiasEnable = false,
//                DepthBiasConstantFactor = 0f,
//                DepthBiasClamp = 0f,
//                DepthBiasSlopeFactor = 0f
//            };

//            PipelineMultisampleStateCreateInfo multisamplingInfo = new PipelineMultisampleStateCreateInfo
//            {
//                SampleShadingEnable = false,
//                RasterizationSamples = SampleCountFlags.SampleCount1,
//                MinSampleShading = 1f,
//                SampleMask = null,
//                AlphaToCoverageEnable = false,
//                AlphaToOneEnable = false
//            };

//            PipelineColorBlendAttachmentState colorBlendAttachment = new PipelineColorBlendAttachmentState
//            {
//                ColorWriteMask = ColorComponentFlags.R | ColorComponentFlags.G | ColorComponentFlags.B | ColorComponentFlags.A,
//                BlendEnable = false,
//                SourceColorBlendFactor = BlendFactor.One,
//                DestinationColorBlendFactor = BlendFactor.Zero,
//                ColorBlendOp = BlendOp.Add,
//                SourceAlphaBlendFactor = BlendFactor.One,
//                DestinationAlphaBlendFactor = BlendFactor.Zero,
//                AlphaBlendOp = BlendOp.Add
//            };

//            PipelineColorBlendStateCreateInfo colorBlendInfo = new PipelineColorBlendStateCreateInfo
//            {
//                LogicOpEnable = false,
//                LogicOp = LogicOp.Copy,
//                Attachments = new PipelineColorBlendAttachmentState[] { colorBlendAttachment },
//                BlendConstants = new float[]
//                {
//                    0f, 0f, 0f, 0f
//                }
//            };

//            _pipelineLayout = _device.CreatePipelineLayout(new PipelineLayoutCreateInfo
//            {
//                SetLayouts = null,
//                PushConstantRanges = null
//            });
//            Assert.IsNotNull(_pipelineLayout, "Could not create pipeline layout.");

//            _graphicsPipeline = _device.CreateGraphicsPipelines(null, new GraphicsPipelineCreateInfo
//            {
//                Stages = shaderStages,
//                VertexInputState = vertexInputInfo,
//                InputAssemblyState = inputAssembly,
//                ViewportState = viewportStateInfo,
//                RasterizationState = rasterizerInfo,
//                MultisampleState = multisamplingInfo,
//                DepthStencilState = null,
//                ColorBlendState = colorBlendInfo,
//                DynamicState = null,
//                Layout = _pipelineLayout,
//                RenderPass = _renderPass,
//                Subpass = 0,
//                BasePipelineHandle = null,
//                BasePipelineIndex = -1,
//            })[0];
//            Assert.IsNotNull(_graphicsPipeline, "Could not create graphics pipeline.");

//            vertShaderModule.Destroy();
//            fragShaderModule.Destroy();

//            unsafe ShaderModule CreateShaderModule(byte[] code)
//            {
//                // TODO: Use unsafe code to speed this up.
//                uint[] codeUint = new uint[code.Length / 4];
//                System.Buffer.BlockCopy(code, 0, codeUint, 0, code.Length);

//                ShaderModule shaderModule = _device.CreateShaderModule(new ShaderModuleCreateInfo
//                {
//                    Code = codeUint,
//                    CodeSize = code.Length
//                });
//                Assert.IsNotNull(shaderModule, "Could not create shader module.");
//                return shaderModule;
//            }
//        }

//        private void CreateFramebuffers()
//        {
//            _swapChainFramebuffers = new Framebuffer[_swapChainImageViews.Length];

//            for (int i = 0; i < _swapChainImageViews.Length; ++i)
//            {
//                ImageView[] attachments = new ImageView[] { _swapChainImageViews[i] };

//                _swapChainFramebuffers[i] = _device.CreateFramebuffer(new FramebufferCreateInfo
//                {
//                    RenderPass = _renderPass,
//                    Attachments = attachments,
//                    Width = _swapChainExtent.Width,
//                    Height = _swapChainExtent.Height,
//                    Layers = 1
//                });
//                Assert.IsNotNull(_swapChainFramebuffers[i], "Could not create framebuffer.");
//            }
//        }

//        private void CreateCommandPool()
//        {
//            _commandPool = _device.CreateCommandPool(new CommandPoolCreateInfo
//            {
//                // TODO: Do we need explicit reset?
//                Flags = CommandPoolCreateFlags.ResetCommandBuffer,
//                QueueFamilyIndex = _graphicsQueueFamilyIndex
//            });
//            Assert.IsNotNull(_commandPool, "Could not create command pool.");
//        }

//        private void CreateCommandBuffers()
//        {
//            _commandBuffers = _device.AllocateCommandBuffers(new CommandBufferAllocateInfo
//            {
//                Level = CommandBufferLevel.Primary,
//                CommandPool = _commandPool,
//                CommandBufferCount = (uint)_swapChainFramebuffers.Length
//            });
//            Assert.IsNotNull(_commandBuffers, "Could not create command buffers.");

//            for (int i = 0; i < _commandBuffers.Length; ++i)
//            {
//                _commandBuffers[i].Begin(new CommandBufferBeginInfo
//                {
//                    Flags = CommandBufferUsageFlags.SimultaneousUse,
//                    InheritanceInfo = null
//                });

//                _commandBuffers[i].BeginRenderPass(new RenderPassBeginInfo
//                {
//                    RenderPass = _renderPass,
//                    Framebuffer = _swapChainFramebuffers[i],
//                    RenderArea = new Rect2D
//                    {
//                        Offset = new Offset2D(0, 0),
//                        Extent = _swapChainExtent
//                    },
//                    ClearValues = new ClearValue[]
//                    {
//                        new ClearColorValue(0.2f, 0.1f, 0.4f, 1f)
//                    }
//                },
//                SubpassContents.Inline);

//                _commandBuffers[i].BindPipeline(PipelineBindPoint.Graphics, _graphicsPipeline);

//                _commandBuffers[i].Draw(3, 1, 0, 0);

//                _commandBuffers[i].EndRenderPass();
//                _commandBuffers[i].End();
//            }
//        }

//        private void CreateSemaphores()
//        {
//            _imageAvailableSemaphore = _device.CreateSemaphore(new SemaphoreCreateInfo());
//            _renderCompleteSemaphore = _device.CreateSemaphore(new SemaphoreCreateInfo());
//            Assert.IsTrue(_imageAvailableSemaphore != null && _renderCompleteSemaphore != null, "Could not create semaphore.");
//        }

//        ~RendererVulkan()
//        {
//            Dispose(false);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }

//    internal static class QueueFlagsExtensions
//    {
//        public static bool Has(this QueueFlags variable, QueueFlags flag)
//        {
//            return (variable & flag) != 0;
//        }

//        public static bool HasNot(this QueueFlags variable, QueueFlags flag)
//        {
//            return (variable & flag) == 0;
//        }
//    }
//}