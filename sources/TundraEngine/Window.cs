using System;
using SharpVk;

namespace TundraEngine
{
    public class Window
    {
        private Instance _instance;
        private PhysicalDevice _physicalDevice;
        private Device _device;

        public Window ()
        {
            // Instance
            InstanceCreateInfo instanceInfo = new InstanceCreateInfo
            {
                ApplicationInfo = new ApplicationInfo
                {
                    ApplicationName = "Tundra Engine"
                },
                EnabledLayerNames = new string[]
                {
                    "VK_LAYER_LUNARG_core_validation",
                    "VK_LAYER_LUNARG_object_tracker"
                },
                EnabledExtensionNames = new string[]
                {
                    KhrSurface.ExtensionName,
                    KhrWin32Surface.ExtensionName
                }
            };
            _instance = Instance.Create (instanceInfo);

            // Device
            PhysicalDevice[] physicalDevices = _instance.EnumeratePhysicalDevices ();
            foreach (var physicalDevice in physicalDevices)
            {
                QueueFamilyProperties[] queueProperties = physicalDevice.GetQueueFamilyProperties ();
                PhysicalDeviceMemoryProperties memoryProperties = physicalDevice.GetMemoryProperties ();
                PhysicalDeviceProperties physicalDeviceProperties = physicalDevice.GetProperties ();

                DeviceCreateInfo deviceInfo = new DeviceCreateInfo
                {
                    
                };
                Device device = physicalDevice.CreateDevice (deviceInfo);
            }

            Win32SurfaceCreateInfo surfaceInfo = new Win32SurfaceCreateInfo
            {
                
            };
            //KhrWin32Surface win32Surface = Create
        }
    }
}