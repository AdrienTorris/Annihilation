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
        private bool _wasDisposed;
        private Instance _instance;
#if DEBUG
        private DebugReportCallback _debugCallback;
#endif
        private Surface _surface;
        private PhysicalDevice _physicalDevice;
        private uint _graphicsQueueIndex = uint.MaxValue;
        private uint _computeQueueIndex = uint.MaxValue;
        private uint _transferQueueIndex = uint.MaxValue;
        private uint _presentQueueIndex = uint.MaxValue;
        private Device _device;
        private Queue _graphicsQueue;
        private Queue _computeQueue;
        private Queue _transferQueue;
        private Queue _presentQueue;
        private CommandPool _commandPool;
        private Swapchain _swapchain;
        
        private const float DefaultQueuePriority = 1f;
        private static readonly PhysicalDeviceFeatures Features = new PhysicalDeviceFeatures
        {
            MultiDrawIndirect = true
        };
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
            WindowManagerType windowManagerType = Application.Window.WindowManagerInfo.Type;

            // Get desired extensions depending on system
            string[] desiredExtensions = new string[]
            {
                KhrSurface.ExtensionName,
                windowManagerType == WindowManagerType.Windows
                ? KhrWin32Surface.ExtensionName
                : windowManagerType == WindowManagerType.X11
                ? KhrXcbSurface.ExtensionName
                : KhrWaylandSurface.ExtensionName,
#if DEBUG
                ExtDebugReport.ExtensionName
#endif
            };

            // Check if we support the desired extensions
            ExtensionProperties[] availableExtensions = Instance.EnumerateExtensionProperties(null);
            HashSet<string> availableExtensionNames = new HashSet<string>(availableExtensions.Length);
            foreach (var extension in availableExtensions)
            {
                availableExtensionNames.Add(extension.ExtensionName);
            }
            foreach (string extension in desiredExtensions)
            {
                Assert.IsTrue(availableExtensionNames.Contains(extension), "Extension " + extension + " is not supported.");
            }

#if DEBUG
            // Enable standard validation in Debug mode
            string[] desiredValidationLayers = new string[]
            {
                "VK_LAYER_LUNARG_standard_validation",
            };

            // Check validation layer support
            LayerProperties[] availableLayers = Instance.EnumerateLayerProperties();
            HashSet<string> availableLayerNames = new HashSet<string>(availableLayers.Length);
            foreach (var layer in availableLayers)
            {
                availableLayerNames.Add(layer.LayerName);
            }
            foreach (string layer in desiredValidationLayers)
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
                EnabledExtensionNames = desiredExtensions,
#if DEBUG
                EnabledLayerNames = desiredValidationLayers
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
                PhysicalDeviceFeatures features = device.GetFeatures();
                QueueFamilyProperties[] queueFamilies = device.GetQueueFamilyProperties();

                _graphicsQueueIndex = GetQueueFamilyIndex(QueueFlags.Graphics);
                _computeQueueIndex = GetQueueFamilyIndex(QueueFlags.Compute);
                _transferQueueIndex = GetQueueFamilyIndex(QueueFlags.Transfer);
                _presentQueueIndex = GetPresentQueueFamilyIndex();

                return features.MultiDrawIndirect &&
                    _graphicsQueueIndex != uint.MaxValue &&
                    _computeQueueIndex != uint.MaxValue &&
                    _transferQueueIndex != uint.MaxValue &&
                    _presentQueueIndex != uint.MaxValue;

                // Get the index of a queue family that supports the requested queue flags
                uint GetQueueFamilyIndex(QueueFlags queueFlags)
                {
                    // Dedicated queue for compute
                    // Try to find a queue family index that supports compute but not graphics
                    if (queueFlags.Has(QueueFlags.Compute))
                    {
                        for (uint i = 0; i < queueFamilies.Length; ++i)
                        {
                            Assert.IsTrue(queueFamilies[i].QueueCount > 0, "No queues in this queue family.");

                            if (queueFamilies[i].QueueFlags.Has(queueFlags) &&
                                queueFamilies[i].QueueFlags.HasNot(QueueFlags.Graphics))
                            {
                                return i;
                            }
                        }
                    }

                    // Dedicated queue for transfer
                    // Try to find a queue family index that supports transfer but not graphics and compute
                    if (queueFlags.Has(QueueFlags.Transfer))
                    {
                        for (uint i = 0; i < queueFamilies.Length; ++i)
                        {
                            Assert.IsTrue(queueFamilies[i].QueueCount > 0, "No queues in this queue family.");

                            if (queueFamilies[i].QueueFlags.Has(queueFlags) &&
                                queueFamilies[i].QueueFlags.HasNot(QueueFlags.Graphics) &&
                                queueFamilies[i].QueueFlags.HasNot(QueueFlags.Compute))
                            {
                                return i;
                            }
                        }
                    }

                    // For other queue types or if no separate compute queue is present, return the first one to support the requested flags
                    for (uint i = 0; i < queueFamilies.Length; ++i)
                    {
                        Assert.IsTrue(queueFamilies[i].QueueCount > 0, "No queues in this queue family.");

                        if (queueFamilies[i].QueueFlags.Has(queueFlags))
                        {
                            return i;
                        }
                    }

                    throw new Exception("Could not find a queue family index for " + queueFlags);
                }

                uint GetPresentQueueFamilyIndex()
                {
                    // Try to find a dedicated present queue
                    // TODO: Check what's the most efficient pattern. Graphics + present in same queue?
                    for (uint i = 0; i < queueFamilies.Length; ++i)
                    {
                        Assert.IsTrue(queueFamilies[i].QueueCount > 0, "No queues in this queue family.");
                        
                        if (i != _graphicsQueueIndex &&
                            i != _computeQueueIndex &&
                            i != _transferQueueIndex &&
                            _physicalDevice.GetSurfaceSupport(i, _surface))
                        {
                            return i;
                        }
                    }

                    // Else just use the first available
                    for (uint i = 0; i < queueFamilies.Length; ++i)
                    {
                        Assert.IsTrue(queueFamilies[i].QueueCount > 0, "No queues in this queue family.");

                        if (_physicalDevice.GetSurfaceSupport(i, _surface))
                        {
                            return i;
                        }
                    }
                    throw new Exception("Could not find a present queue family index.");
                }
            }
        }

        private void CreateLogicalDevice()
        {
            // Queue families
            QueueFamilyProperties[] queueFamilyProperties = _physicalDevice.GetQueueFamilyProperties();
            Assert.IsTrue(queueFamilyProperties.Length > 0, "No queue family properties found.");

            // Queue infos
            List<DeviceQueueCreateInfo> queueCreateInfos = new List<DeviceQueueCreateInfo>(3)
            {
                // Graphics queue
                new DeviceQueueCreateInfo
                {
                    QueueFamilyIndex = _graphicsQueueIndex,
                    QueuePriorities = new float[1] { DefaultQueuePriority }
                }
            };

            // Dedicated compute queue, if any
            if (_computeQueueIndex != _graphicsQueueIndex)
            {
                // If compute family index differs, we need an additional queue create info for the compute queue
                queueCreateInfos.Add(new DeviceQueueCreateInfo
                {
                    QueueFamilyIndex = _computeQueueIndex,
                    QueuePriorities = new float[1] { DefaultQueuePriority }
                });
            }

            // Dedicated transfer queue, if any
            if (_transferQueueIndex != _graphicsQueueIndex &&
                _transferQueueIndex != _computeQueueIndex)
            {
                // If graphics and compute family indices differ, we need an additional queue create info for the transfer queue
                queueCreateInfos.Add(new DeviceQueueCreateInfo
                {
                    QueueFamilyIndex = _transferQueueIndex,
                    QueuePriorities = new float[1] { DefaultQueuePriority }
                });
            }

            // Create the logical device
            _device = _physicalDevice.CreateDevice(new DeviceCreateInfo
            {
                QueueCreateInfos = queueCreateInfos.ToArray(),
                EnabledFeatures = Features,
                EnabledExtensionNames = new string[]
                {
                    KhrSwapchain.ExtensionName
                },
                
            });
            Assert.IsNotNull(_device, "Could not create logical device.");

            // Set the queue handles
            // TODO: Check if queue index is 0 for all families
            _graphicsQueue = _device.GetQueue(_graphicsQueueIndex, 0);
            _computeQueue = _device.GetQueue(_computeQueueIndex, 0);
            _transferQueue = _device.GetQueue(_transferQueueIndex, 0);
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

        private void CreateSwapchain(Device device, uint width, uint height, Swapchain oldSwapchain, out Swapchain swapchain)
        {
            swapchain = null;
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