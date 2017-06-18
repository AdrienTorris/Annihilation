using System;
using System.Collections.Generic;
using SharpVk;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Graphics
{
    internal static class Vulkan
    {
        private const float DefaultQueuePriority = 0f;

        public static Instance CreateInstance (string applicationName, SysWMType windowSubsystem)
        {
            // Find proper extensions depending on system
            string surfaceExtension = string.Empty;
            switch (windowSubsystem)
            {
                case SysWMType.Windows:
                    surfaceExtension = KhrWin32Surface.ExtensionName;
                    break;
                case SysWMType.X11:
                    surfaceExtension = KhrXcbSurface.ExtensionName;
                    break;
                case SysWMType.Wayland:
                    surfaceExtension = KhrWaylandSurface.ExtensionName;
                    break;
            }
            Assert.IsFalse (string.IsNullOrEmpty (surfaceExtension), "Windowing subsystem \"" + windowSubsystem + "\" not supported.");

            // Create the instance
            Instance instance = Instance.Create (new InstanceCreateInfo
            {
                ApplicationInfo = new ApplicationInfo
                {
                    ApplicationName = applicationName,
                    EngineName = "Tundra Engine"
                },
                EnabledExtensionNames = new string[]
                {
                    KhrSurface.ExtensionName,
                    surfaceExtension,
                }
            });
            Assert.IsNotNull (instance, "Could not create Vulkan instance.");
            return instance;
        }

        public static PhysicalDevice GetPhysicalDevice (Instance instance)
        {
            PhysicalDevice[] physicalDevices = instance.EnumeratePhysicalDevices ();
            Assert.IsTrue (physicalDevices.Length > 0, "No physical devices found.");
            PhysicalDevice physicalDevice = physicalDevices[0];
            Assert.IsNotNull (physicalDevice, "Physical device is null.");
            return physicalDevice;
        }

        public static (Device, CommandPool) CreateDeviceAndCommandPool (PhysicalDevice physicalDevice, QueueFlags queueTypes, out QueueFamilyIndices queueFamilyIndices, Func<PhysicalDeviceFeatures> getFeatures)
        {
            // Physical device features and properties
            PhysicalDeviceProperties properties = physicalDevice.GetProperties ();
            PhysicalDeviceFeatures features = physicalDevice.GetFeatures ();
            PhysicalDeviceMemoryProperties memoryProperties = physicalDevice.GetMemoryProperties ();

            // Queue families
            QueueFamilyProperties[] queueFamilyProperties = physicalDevice.GetQueueFamilyProperties ();
            Assert.IsTrue (queueFamilyProperties.Length > 0, "No queue family properties found.");

            // Extensions
            ExtensionProperties[] extensionProperties = physicalDevice.EnumerateDeviceExtensionProperties (null);

            // Queue infos
            List<DeviceQueueCreateInfo> queueCreateInfos = new List<DeviceQueueCreateInfo> ();
            
            // Graphics queue
            if (queueTypes.Has (QueueFlags.Graphics))
            {
                queueFamilyIndices.Graphics = GetQueueFamilyIndex (QueueFlags.Graphics);
                queueCreateInfos.Add (new DeviceQueueCreateInfo
                {
                    QueueFamilyIndex = queueFamilyIndices.Graphics,
                    QueuePriorities = new float[1] { DefaultQueuePriority }
                });
            }
            else
            {
                queueFamilyIndices.Graphics = 0;
            }

            // Dedicated compute queue
            if (queueTypes.Has (QueueFlags.Compute))
            {
                queueFamilyIndices.Compute = GetQueueFamilyIndex (QueueFlags.Compute);
                if (queueFamilyIndices.Compute != queueFamilyIndices.Graphics)
                {
                    // If compute family index differs, we need an additional queue create info for the compute queue
                    queueCreateInfos.Add (new DeviceQueueCreateInfo
                    {
                        QueueFamilyIndex = queueFamilyIndices.Compute,
                        QueuePriorities = new float[1] { DefaultQueuePriority }
                    });
                }
            }
            else
            {
                // Use the same queue as graphics
                queueFamilyIndices.Compute = queueFamilyIndices.Graphics;
            }

            // Dedicated transfer queue
            if (queueTypes.Has (QueueFlags.Transfer))
            {
                queueFamilyIndices.Transfer = GetQueueFamilyIndex (QueueFlags.Transfer);
                if (queueFamilyIndices.Transfer != queueFamilyIndices.Graphics &&
                    queueFamilyIndices.Transfer != queueFamilyIndices.Compute)
                {
                    queueCreateInfos.Add (new DeviceQueueCreateInfo
                    {
                        QueueFamilyIndex = queueFamilyIndices.Transfer,
                        QueuePriorities = new float[1] { DefaultQueuePriority }
                    });
                }
            }
            else
            {
                // Use the same queue as graphics
                queueFamilyIndices.Transfer = queueFamilyIndices.Graphics;
            }

            // Create the logical device
            Device device = physicalDevice.CreateDevice (new DeviceCreateInfo
            {
                QueueCreateInfos = queueCreateInfos.ToArray (),
                EnabledFeatures = getFeatures (),
                EnabledExtensionNames = new string[]
                {
                    KhrSwapchain.ExtensionName
                }
            });
            Assert.IsNotNull (device, "Could not create logical device.");

            // Create a default command pool for graphics command buffers
            CommandPool commandPool = device.CreateCommandPool (new CommandPoolCreateInfo
            {
                Flags = CommandPoolCreateFlags.ResetCommandBuffer,
                QueueFamilyIndex = queueFamilyIndices.Graphics
            });
            Assert.IsNotNull (commandPool, "Could not create command pool.");

            return (device, commandPool);

            // Get the index of a queue family that supports the requested queue flags
            uint GetQueueFamilyIndex (QueueFlags queueFlags)
            {
                // Dedicated queue for compute
                // Try to find a queue family index that supports compute but not graphics
                if (queueFlags.Has (QueueFlags.Compute))
                {
                    for (uint i = 0; i < queueFamilyProperties.Length; ++i)
                    {
                        if (queueFamilyProperties[i].QueueFlags.Has (queueFlags) &&
                            queueFamilyProperties[i].QueueFlags.HasNot (QueueFlags.Graphics))
                        {
                            return i;
                        }
                    }
                }

                // Dedicated queue for transfer
                // Try to find a queue family index that supports transfer but not graphics and compute
                if ((queueFlags & QueueFlags.Transfer) != 0)
                {
                    for (uint i = 0; i < queueFamilyProperties.Length; ++i)
                    {
                        if (queueFamilyProperties[i].QueueFlags.Has (queueFlags) &&
                            queueFamilyProperties[i].QueueFlags.HasNot (QueueFlags.Graphics) &&
                            queueFamilyProperties[i].QueueFlags.HasNot (QueueFlags.Compute))
                        {
                            return i;
                        }
                    }
                }

                // For other queue types or if no separate compute queue is present, return the first one to support the requested flags
                for (uint i = 0; i < queueFamilyProperties.Length; ++i)
                {
                    if (queueFamilyProperties[i].QueueFlags.Has (queueFlags))
                    {
                        return i;
                    }
                }

                throw new Exception ("Could not find a matching queue family index.");
            }
        }

        public static Surface CreateSurface (Instance instance, SysWMInfo windowManagerInfo)
        {
            Surface surface = null;
            switch (windowManagerInfo.SubSystem)
            {
                case SysWMType.Windows:
                    surface = instance.CreateWin32Surface (new Win32SurfaceCreateInfo
                    {
                        Hwnd = windowManagerInfo.Info.Windows.Window,
                        Hinstance = windowManagerInfo.Info.Windows.HInstance
                    });
                    break;
                case SysWMType.X11:
                    surface = instance.CreateXcbSurface (new XcbSurfaceCreateInfo
                    {
                        Window = windowManagerInfo.Info.X11.Window,
                        Connection = windowManagerInfo.Info.X11.Display
                    });
                    break;
                case SysWMType.Wayland:
                    surface = instance.CreateWaylandSurface (new WaylandSurfaceCreateInfo
                    {
                        Surface = windowManagerInfo.Info.Wayland.Surface,
                        Display = windowManagerInfo.Info.Wayland.Display
                    });
                    break;
            }
            Assert.IsNotNull (surface, "Could not create surface.");

            return surface;
        }
    }
}