using System;
using SDL2;
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
            // Extensions and layers
            LayerProperties[] layers = Instance.EnumerateLayerProperties ();
            foreach (var layer in layers)
            {
                SDL.Log (layer.LayerName);
            }

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

            // SDL Window
            IntPtr windowPtr = SDL.CreateWindow (
                "Tundra Engine",
                SDL.WindowPositionUndefined,
                SDL.WindowPositionUndefined,
                1280,
                768,
                SDL.WindowFlags.Shown | SDL.WindowFlags.Vulkan);

            if (windowPtr == IntPtr.Zero)
            {
                SDL.LogError (SDL.LogCategory.Error, SDL.GetError ());
                return;
            }

            SDL.CreateVulkanSurface (windowPtr, _instance, out Surface surface);

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