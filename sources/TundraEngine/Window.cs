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

            // System WM
            SysWMInfo wmInfo = new SysWMInfo ();
            FillVersion (out wmInfo.Version);
            GetWindowWMInfo (_window, ref wmInfo);

            Surface surface;
            switch (wmInfo.SubSystem)
            {
                case SysWMType.Windows:
                    surface = _instance.CreateWin32Surface (new Win32SurfaceCreateInfo
                    {
                        Hwnd = wmInfo.Info.Windows.Window,
                        Hinstance = wmInfo.Info.Windows.HInstance
                    });
                    break;
                case SysWMType.X11:
                    surface = _instance.CreateXcbSurface (new XcbSurfaceCreateInfo
                    {
                        Connection = wmInfo.Info.X11.Display,
                        Window = wmInfo.Info.X11.Window
                    });
                    break;
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

        ~Window ()
        {
            Dispose (false);
        }

        private void Dispose (bool isDisposing)
        {
            SDL_DestroyWindow (_window);
            _window = IntPtr.Zero;
            _instance.Dispose ();

            GC.SuppressFinalize (this);
        }

        public void Dispose ()
        {
            Dispose (true);
        }
    }
}