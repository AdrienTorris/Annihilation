using System;
using SharpVk;

using static SDL.SDL;

namespace TundraEngine
{
    public class Window : IDisposable
    {
        private IntPtr _window;
        private Instance _instance;

        public Window ()
        {
            // Instance
            InstanceCreateInfo instanceInfo = new InstanceCreateInfo
            {
                ApplicationInfo = new ApplicationInfo
                {
                    ApplicationName = "Tundra Engine"
                },
                EnabledExtensionNames = new string[]
                {
                    KhrSurface.ExtensionName,
                    KhrWin32Surface.ExtensionName
                }
            };
            _instance = Instance.Create (instanceInfo);

            // SDL Window
            _window = SDL_CreateWindow (
                "Tundra Engine",
                SDL_WindowPositionUndefined,
                SDL_WindowPositionUndefined,
                1280,
                768,
                SDL_WindowFlags.Shown | SDL_WindowFlags.Vulkan);

            if (_window == IntPtr.Zero)
            {
                SDL_LogError (SDL_LogCategory.Error, SDL_GetErrorString ());
                return;
            }

            if (!SDL_CreateVulkanSurface (_window, _instance, out Surface surface))
            {
                SDL_LogError (SDL_LogCategory.Error, SDL_GetErrorString ());
                return;
            }

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
        }

        public void Dispose ()
        {
            SDL_DestroyWindow (_window);
            _window = IntPtr.Zero;
            _instance.Dispose ();

            GC.SuppressFinalize (this);
        }
    }
}