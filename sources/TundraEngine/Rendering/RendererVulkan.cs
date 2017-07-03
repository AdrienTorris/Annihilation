using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

using TundraEngine.Windowing;
using SharpVk;

namespace TundraEngine.Rendering
{
    public class RendererVulkan : IRenderer
    {
        private Instance _instance;
        private Surface _surface;
        private PhysicalDevice _physicalDevice;
        private Device _device;
        private Swapchain _swapchain;

        private uint _graphicsQueueFamilyIndex = uint.MaxValue;
        private uint _computeQueueFamilyIndex = uint.MaxValue;
        private uint _transferQueueFamilyIndex = uint.MaxValue;
        private uint _presentQueueFamilyIndex = uint.MaxValue;
        private Queue _graphicsQueue;
        private Queue _computeQueue;
        private Queue _transferQueue;
        private Queue _presentQueue;

        private SurfaceCapabilities _surfaceCapabilities;
        private SurfaceFormat[] _surfaceFormats;
        private PresentMode[] _presentModes;

        private bool _wasDisposed;

#if DEBUG
        private DebugReportCallback _debugCallback;
#endif

        private const float DefaultQueuePriority = 1f;

#if DEBUG
        private static readonly string[] InstanceLayers = new string[]
        {
            "VK_LAYER_LUNARG_standard_validation"
        };
#endif
        private static readonly string[] InstanceExtensions = new string[]
        {
                KhrSurface.ExtensionName,
                Application.Window.WindowManagerInfo.Type == WindowManagerType.Windows
                ? KhrWin32Surface.ExtensionName
                : Application.Window.WindowManagerInfo.Type == WindowManagerType.X11
                ? KhrXcbSurface.ExtensionName
                : KhrWaylandSurface.ExtensionName,
#if DEBUG
                ExtDebugReport.ExtensionName
#endif
        };
        private static readonly string[] DeviceExtensions = new string[]
        {
            KhrSwapchain.ExtensionName
        };
        private static readonly PhysicalDeviceFeatures DeviceFeatures = new PhysicalDeviceFeatures
        {
            MultiDrawIndirect = true
        };
        private const Format SurfaceFormat = Format.B8G8R8A8UNorm;
        private const ColorSpace SurfaceColorSpace = ColorSpace.SrgbNonlinear;
        private const Format DepthFormat = Format.D32SFloatS8UInt;

        public RendererVulkan()
        {
            CreateInstance();
            CreateDebugCallback();
            CreateSurface();
            SelectPhysicalDevice();
            CreateLogicalDevice();
            //CreateCommandPool();
            //CreateSwapchain((uint)Application.Settings.RendererSettings.ResolutionX, (uint)Application.Settings.RendererSettings.ResolutionY);
            // TODO: Create command buffers
            // TODO: Setup depth stencil
            // TODO: Setup render pass
            // TODO: Create pipeline cache
            // TODO: Setup framebuffer

            //_graphicsQueue = _device.GetQueue(_graphicsQueueIndex, 0);
        }

        public async Task RenderAsync()
        {
            await Task.Delay(0);
        }

        private void Dispose(bool disposing)
        {
            if (!_wasDisposed)
            {
                if (disposing) { }

                //_swapchain.Dispose();
                //_surface.Dispose();
                //_commandPool.Dispose();
                _device.Dispose();
                _surface.Dispose();
                _debugCallback.Dispose();
                _instance.Dispose();

                _wasDisposed = true;
            }
        }

#if DEBUG
        private static Bool32 DebugCallback(DebugReportFlags flags, DebugReportObjectType objectType, ulong @object, Size location, int messageCode, string layerPrefix, string message, IntPtr userData)
        {
            Debug.WriteLine("Validation Layer: " + message);

            return new Bool32(false);
        }
#endif

        private void CreateInstance()
        {
            // Check if we support the desired extensions
            ExtensionProperties[] availableExtensions = Instance.EnumerateExtensionProperties(null);
            HashSet<string> availableExtensionNames = new HashSet<string>(availableExtensions.Length);
            foreach (var extension in availableExtensions)
            {
                availableExtensionNames.Add(extension.ExtensionName);
            }
            foreach (string extension in InstanceExtensions)
            {
                Assert.IsTrue(availableExtensionNames.Contains(extension), "Extension " + extension + " is not supported.");
            }

#if DEBUG
            // Check validation layer support
            LayerProperties[] availableLayers = Instance.EnumerateLayerProperties();
            HashSet<string> availableLayerNames = new HashSet<string>(availableLayers.Length);
            foreach (var layer in availableLayers)
            {
                availableLayerNames.Add(layer.LayerName);
            }
            foreach (string layer in InstanceLayers)
            {
                Assert.IsTrue(availableLayerNames.Contains(layer), "Validation layer " + layer + " is not supported.");
            }
#endif

            // Create the instance
            Version version = Application.Settings.Version;
            _instance = Instance.Create(new InstanceCreateInfo
            {
                ApplicationInfo = new ApplicationInfo
                {
                    ApplicationName = Application.Settings.Name,
                    ApplicationVersion = new SharpVk.Version(version.Major, version.Minor, version.Patch),
                    EngineName = "Tundra Engine",
                    EngineVersion = new SharpVk.Version(0, 1, 0)
                },
                EnabledExtensionNames = InstanceExtensions,
#if DEBUG
                EnabledLayerNames = InstanceLayers
#endif
            });
            Assert.IsNotNull(_instance, "Could not create Vulkan instance.");
        }

        [Conditional("DEBUG")]
        private void CreateDebugCallback()
        {
            _debugCallback = _instance.CreateDebugReportCallback(new DebugReportCallbackCreateInfo
            {
                Flags = DebugReportFlags.Error | DebugReportFlags.Warning,
                PfnCallback = DebugCallback
            });
            Assert.IsNotNull(_debugCallback, "Could not create debug callback.");
        }

        private void CreateSurface()
        {
            WindowManagerInfo windowManagerInfo = Application.Window.WindowManagerInfo;

            switch (windowManagerInfo.Type)
            {
                case WindowManagerType.Windows:
                _surface = _instance.CreateWin32Surface(new Win32SurfaceCreateInfo
                {
                    Hwnd = windowManagerInfo.Windows.HWindow,
                    Hinstance = windowManagerInfo.Windows.HInstance
                });
                break;
                case WindowManagerType.X11:
                _surface = _instance.CreateXcbSurface(new XcbSurfaceCreateInfo
                {
                    Window = windowManagerInfo.X11.Window,
                    Connection = windowManagerInfo.X11.Connection
                });
                break;
                case WindowManagerType.Wayland:
                _surface = _instance.CreateWaylandSurface(new WaylandSurfaceCreateInfo
                {
                    Surface = windowManagerInfo.Wayland.Surface,
                    Display = windowManagerInfo.Wayland.Display
                });
                break;
            }
            Assert.IsNotNull(_surface, "Could not create surface.");
        }

        private void SelectPhysicalDevice()
        {
            PhysicalDevice[] physicalDevices = _instance.EnumeratePhysicalDevices();
            Assert.IsTrue(physicalDevices.Length > 0, "No GPU found.");
            foreach (var device in physicalDevices)
            {
                if (IsDeviceSuitable(device))
                {
                    _physicalDevice = device;
                    break;
                }
            }
            Assert.IsNotNull(_physicalDevice, "Couldn't find a suitable GPU.");

            bool IsDeviceSuitable(PhysicalDevice device)
            {
                PhysicalDeviceFeatures availableFeatures = device.GetFeatures();
                QueueFamilyProperties[] queueFamilies = device.GetQueueFamilyProperties();

                // The first queue family is the one with graphics + present
                _graphicsQueueFamilyIndex = 0;
                _presentQueueFamilyIndex = 0;

                // Compute is in queue family 1 on AMD and 2 on NVIDIA
                for (uint i = 1; i < queueFamilies.Length; ++i)
                {
                    if (queueFamilies[i].QueueFlags.Has(QueueFlags.Compute))
                    {
                        _computeQueueFamilyIndex = i;
                    }
                }

                // Transfer queue is different from both graphics and compute queue
                for (uint i = 1; i < queueFamilies.Length; ++i)
                {
                    if (queueFamilies[i].QueueFlags.Has(QueueFlags.Transfer) &&
                        i != _graphicsQueueFamilyIndex &&
                        i != _computeQueueFamilyIndex)
                    {
                        _transferQueueFamilyIndex = i;
                    }
                }

                return
                    AreFeaturesSupported() &&
                    AreExtensionsSupported() &&
                    IsSwapChainSupported() &&
                    _graphicsQueueFamilyIndex != uint.MaxValue &&
                    _presentQueueFamilyIndex != uint.MaxValue &&
                    _computeQueueFamilyIndex != uint.MaxValue &&
                    _transferQueueFamilyIndex != uint.MaxValue;
                
                bool AreFeaturesSupported()
                {
                    bool ok =
                        !(DeviceFeatures.AlphaToOne && !availableFeatures.AlphaToOne) &&
                        !(DeviceFeatures.DepthBiasClamp && !availableFeatures.DepthBiasClamp) &&
                        !(DeviceFeatures.DepthBounds && !availableFeatures.DepthBounds) &&
                        !(DeviceFeatures.DepthClamp && !availableFeatures.DepthClamp) &&
                        !(DeviceFeatures.DrawIndirectFirstInstance && !availableFeatures.DrawIndirectFirstInstance) &&
                        !(DeviceFeatures.DualSourceBlend && !availableFeatures.DualSourceBlend) &&
                        !(DeviceFeatures.FillModeNonSolid && !availableFeatures.FillModeNonSolid) &&
                        !(DeviceFeatures.FragmentStoresAndAtomics && !availableFeatures.FragmentStoresAndAtomics) &&
                        !(DeviceFeatures.FullDrawIndexUInt32 && !availableFeatures.FullDrawIndexUInt32) &&
                        !(DeviceFeatures.GeometryShader && !availableFeatures.GeometryShader) &&
                        !(DeviceFeatures.ImageCubeArray && !availableFeatures.ImageCubeArray) &&
                        !(DeviceFeatures.IndependentBlend && !availableFeatures.IndependentBlend) &&
                        !(DeviceFeatures.InheritedQueries && !availableFeatures.InheritedQueries) &&
                        !(DeviceFeatures.LargePoints && !availableFeatures.LargePoints) &&
                        !(DeviceFeatures.LogicOp && !availableFeatures.LogicOp) &&
                        !(DeviceFeatures.MultiDrawIndirect && !availableFeatures.MultiDrawIndirect) &&
                        !(DeviceFeatures.MultiViewport && !availableFeatures.MultiViewport) &&
                        !(DeviceFeatures.OcclusionQueryPrecise && !availableFeatures.OcclusionQueryPrecise) &&
                        !(DeviceFeatures.PipelineStatisticsQuery && !availableFeatures.PipelineStatisticsQuery) &&
                        !(DeviceFeatures.RobustBufferAccess && !availableFeatures.RobustBufferAccess) &&
                        !(DeviceFeatures.SamplerAnisotropy && !availableFeatures.SamplerAnisotropy) &&
                        !(DeviceFeatures.SampleRateShading && !availableFeatures.SampleRateShading) &&
                        !(DeviceFeatures.ShaderClipDistance && !availableFeatures.ShaderClipDistance) &&
                        !(DeviceFeatures.ShaderCullDistance && !availableFeatures.ShaderCullDistance) &&
                        !(DeviceFeatures.ShaderFloat64 && !availableFeatures.ShaderFloat64) &&
                        !(DeviceFeatures.ShaderImageGatherExtended && !availableFeatures.ShaderImageGatherExtended) &&
                        !(DeviceFeatures.ShaderInt16 && !availableFeatures.ShaderInt16) &&
                        !(DeviceFeatures.ShaderInt64 && !availableFeatures.ShaderInt64) &&
                        !(DeviceFeatures.ShaderResourceMinLod && !availableFeatures.ShaderResourceMinLod) &&
                        !(DeviceFeatures.ShaderResourceResidency && !availableFeatures.ShaderResourceResidency) &&
                        !(DeviceFeatures.ShaderSampledImageArrayDynamicIndexing && !availableFeatures.ShaderSampledImageArrayDynamicIndexing) &&
                        !(DeviceFeatures.ShaderStorageBufferArrayDynamicIndexing && !availableFeatures.ShaderStorageBufferArrayDynamicIndexing) &&
                        !(DeviceFeatures.ShaderStorageImageArrayDynamicIndexing && !availableFeatures.ShaderStorageImageArrayDynamicIndexing) &&
                        !(DeviceFeatures.ShaderStorageImageExtendedFormats && !availableFeatures.ShaderStorageImageExtendedFormats) &&
                        !(DeviceFeatures.ShaderStorageImageMultisample && !availableFeatures.ShaderStorageImageMultisample) &&
                        !(DeviceFeatures.ShaderStorageImageReadWithoutFormat && !availableFeatures.ShaderStorageImageReadWithoutFormat) &&
                        !(DeviceFeatures.ShaderStorageImageWriteWithoutFormat && !availableFeatures.ShaderStorageImageWriteWithoutFormat) &&
                        !(DeviceFeatures.ShaderTessellationAndGeometryPointSize && !availableFeatures.ShaderTessellationAndGeometryPointSize) &&
                        !(DeviceFeatures.ShaderUniformBufferArrayDynamicIndexing && !availableFeatures.ShaderUniformBufferArrayDynamicIndexing) &&
                        !(DeviceFeatures.SparseBinding && !availableFeatures.SparseBinding) &&
                        !(DeviceFeatures.SparseResidency16Samples && !availableFeatures.SparseResidency16Samples) &&
                        !(DeviceFeatures.SparseResidency2Samples && !availableFeatures.SparseResidency2Samples) &&
                        !(DeviceFeatures.SparseResidency4Samples && !availableFeatures.SparseResidency4Samples) &&
                        !(DeviceFeatures.SparseResidency8Samples && !availableFeatures.SparseResidency8Samples) &&
                        !(DeviceFeatures.SparseResidencyAliased && !availableFeatures.SparseResidencyAliased) &&
                        !(DeviceFeatures.SparseResidencyBuffer && !availableFeatures.SparseResidencyBuffer) &&
                        !(DeviceFeatures.SparseResidencyImage2D && !availableFeatures.SparseResidencyImage2D) &&
                        !(DeviceFeatures.SparseResidencyImage3D && !availableFeatures.SparseResidencyImage3D) &&
                        !(DeviceFeatures.TessellationShader && !availableFeatures.TessellationShader) &&
                        !(DeviceFeatures.TextureCompressionASTC_LDR && !availableFeatures.TextureCompressionASTC_LDR) &&
                        !(DeviceFeatures.TextureCompressionBC && !availableFeatures.TextureCompressionBC) &&
                        !(DeviceFeatures.TextureCompressionETC2 && !availableFeatures.TextureCompressionETC2) &&
                        !(DeviceFeatures.VariableMultisampleRate && !availableFeatures.VariableMultisampleRate) &&
                        !(DeviceFeatures.VertexPipelineStoresAndAtomics && !availableFeatures.VertexPipelineStoresAndAtomics) &&
                        !(DeviceFeatures.WideLines && !availableFeatures.WideLines);

                    Assert.IsTrue(ok, "Device features are not supported on this GPU.");
                    return ok;
                }

                bool AreExtensionsSupported()
                {
                    ExtensionProperties[] availableExtensions = device.EnumerateDeviceExtensionProperties(null);

                    HashSet<string> extensionSet = new HashSet<string>(DeviceExtensions);
                    
                    foreach (var extension in availableExtensions)
                    {
                        extensionSet.Remove(extension.ExtensionName);
                    }

                    bool ok = extensionSet.Count == 0;
                    Assert.IsTrue(ok, "Device extensions are not supported on this GPU.");

                    return ok;
                }
                
                bool IsSwapChainSupported()
                {
                    _surfaceCapabilities = device.GetSurfaceCapabilities(_surface);
                    _surfaceFormats = device.GetSurfaceFormats(_surface);
                    _presentModes = device.GetSurfacePresentModes(_surface);

                    bool ok = _surfaceFormats.Length != 0 && _presentModes.Length != 0;
                    Assert.IsTrue(ok, "Swapchain format is not supported.");

                    return ok;
                }
            }
        }

        private void CreateLogicalDevice()
        {
            // Queue families
            QueueFamilyProperties[] queueFamilyProperties = _physicalDevice.GetQueueFamilyProperties();
            Assert.IsTrue(queueFamilyProperties.Length > 0, "No queue family properties found.");

            // Queue infos
            DeviceQueueCreateInfo[] queueCreateInfos = new DeviceQueueCreateInfo[3]
            {
                // Graphics and present queue
                new DeviceQueueCreateInfo
                {
                    QueueFamilyIndex = _graphicsQueueFamilyIndex,
                    QueuePriorities = new float[1] { DefaultQueuePriority }
                },
                // Compute queue
                new DeviceQueueCreateInfo
                {
                    QueueFamilyIndex = _computeQueueFamilyIndex,
                    QueuePriorities = new float[1] { DefaultQueuePriority }
                },
                // Transfer queue
                new DeviceQueueCreateInfo
                {
                    QueueFamilyIndex = _transferQueueFamilyIndex,
                    QueuePriorities = new float[1] { DefaultQueuePriority }
                }
            };

            // Create the logical device
            _device = _physicalDevice.CreateDevice(new DeviceCreateInfo
            {
                QueueCreateInfos = queueCreateInfos,
                EnabledFeatures = DeviceFeatures,
                EnabledExtensionNames = DeviceExtensions,

            });
            Assert.IsNotNull(_device, "Could not create logical device.");

            // Set the queue handles
            _graphicsQueue = _device.GetQueue(_graphicsQueueFamilyIndex, 0);
            _computeQueue = _device.GetQueue(_computeQueueFamilyIndex, 0);
            _transferQueue = _device.GetQueue(_transferQueueFamilyIndex, 0);
            _presentQueue = _device.GetQueue(_presentQueueFamilyIndex, 0);
        }

        private void CreateSwapChain()
        {

        }

        private void CreateCommandPool(Device device, uint presentQueueIndex, out CommandPool commandPool)
        {
            commandPool = device.CreateCommandPool(new CommandPoolCreateInfo
            {
                Flags = CommandPoolCreateFlags.ResetCommandBuffer,
                QueueFamilyIndex = presentQueueIndex
            });
            Assert.IsNotNull(commandPool, "Could not create command pool.");
        }
        
        ~RendererVulkan()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public static class QueueFlagsExtensions
    {
        public static bool Has(this QueueFlags variable, QueueFlags flag)
        {
            return (variable & flag) != 0;
        }

        public static bool HasNot(this QueueFlags variable, QueueFlags flag)
        {
            return (variable & flag) == 0;
        }
    }
}