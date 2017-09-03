using System;
using Engine.Config;
using SDL2;
using Vulkan;

namespace Engine
{
    public unsafe class Window : IDisposable
    {
        public const int DefaultWidth = 1280;
        public const int DefaultHeight = 720;

        private static bool _videoSubsystemInitialized = false;

        public Application Application { get; private set; }
        public bool HasFocus { get; set; }
        public IntPtr Handle { get; private set; }
#if PLATFORM_LINUX
        public IntPtr Display { get; private set; }
#endif
        public SDL.Window SdlHandle { get; private set; }
        
        public Window(Application application)
        {
            Application = application;

            // Initialize the video subsystem if needed
            if (_videoSubsystemInitialized == false)
            {
                SDL.InitSubSystem(SDL.InitFlags.Video).CheckError();
                _videoSubsystemInitialized = true;
            }

            // Load the default Vulkan loader library
            SDL.VulkanLoadLibrary(null);

            // Create the window
            SdlHandle = SDL.CreateWindow(
                application.ApplicationSettings.Title,
                SDL.WindowPositionUndefined,
                SDL.WindowPositionUndefined, 
                ConfigManager.GetVar(Application.ConfigResolutionX, 1280), 
                ConfigManager.GetVar(Application.ConfigResolutionY, 720),
                SDL.WindowFlags.Shown | SDL.WindowFlags.Vulkan
            );
            SdlHandle.CheckError();

            // Get the window manager info
            SDL.SysWMInfo sysWMInfo = default(SDL.SysWMInfo);
            SDL.GetVersion(out sysWMInfo.Version);
            SDL.GetWindowWMInfo(SdlHandle, ref sysWMInfo).CheckError();

#if PLATFORM_WINDOWS
            Handle = sysWMInfo.Info.Windows.Window;
#elif PLATFORM_LINUX
            switch(sysWMInfo.SubSystem)
            {
                case SDL.SysWMType.X11:
                    Handle = sysWMInfo.Info.X11.Window;
                    Display = sysWMInfo.Info.X11.Display;
                    break;
                case SDL.SysWMType.Wayland:
                    Handle = sysWMInfo.Info.Wayland.Surface;
                    Display = sysWMInfo.Info.Wayland.Display;
                    break;
                case SDL.SysWMType.Mir:
                    Handle = sysWMInfo.Info.Mir.Connection;
                    Display = sysWMInfo.Info.Mir.Surface;
                    break;
            }
#elif PLATFORM_MACOS
            Handle = sysWMInfo.Info.Cocoa.Window;
#endif
            // Init Vulkan up to the surface creation
            InitVulkanInstance();
        }

        private void InitVulkanInstance()
        {
            // Get the required instance extensions to create a surface
            uint extensionCount = 0;
            SDL.VulkanGetInstanceExtensions(SdlHandle, ref extensionCount, null).CheckError();

            Text[] extensionNames = new Text[extensionCount + 1];
            SDL.VulkanGetInstanceExtensions(SdlHandle, ref extensionCount, extensionNames).CheckError();

            extensionNames[extensionCount] = Vk.ExtDebugReportExtensionName;

            // Create the Vulkan instance
            Vk.ApplicationInfo applicationInfo = new Vk.ApplicationInfo(
                Application.ApplicationSettings.Title,
                Vulkan.Version.One,
                Application.ApplicationSettings.Title,
                Vulkan.Version.One,
                Vulkan.Version.One
            );

            Vk.InstanceCreateInfo instanceCreateInfo = new Vk.InstanceCreateInfo(
                &applicationInfo,
                extensionNames
            );
#if ENABLE_VALIDATION
            Text[] layerNames = new Text[] { "VK_LAYER_LUNARG_standard_validation" };
#endif
        }

        private void Dispose(bool isDisposing)
        {
            if (SdlHandle == IntPtr.Zero)
            {
                return;
            }

            if (isDisposing)
            {
                Application = null;
            }

            SDL.DestroyWindow(SdlHandle);
            SdlHandle = IntPtr.Zero;
            Handle = IntPtr.Zero;
#if PLATFORM_LINUX
            Display = IntPtr.Zero;
#endif
        }

        ~Window()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}