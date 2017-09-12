//using System;
//using System.Runtime.InteropServices;
//using SDL2;
//using Vulkan;

//namespace Engine.Graphics
//{
//    public unsafe class Window : IDisposable
//    {
//        public const int DefaultWidth = 1280;
//        public const int DefaultHeight = 720;
//        private const VkFormatFeatureFlags RequiredColorBufferFeatures =
//            VkFormatFeatureFlags.ColorAttachment |
//            VkFormatFeatureFlags.ColorAttachmentBlend |
//            VkFormatFeatureFlags.SampledImage |
//            VkFormatFeatureFlags.StorageImage |
//            VkFormatFeatureFlags.SampledImageFilterLinear;
//        private const int CommandBufferCount = 2;
//        private const int ColorBufferCount = 2;
//        private const int MaxSwapchainImages = 8;

//        public static bool VideoSubSystemInitialized { get; private set; }

//        private static Vk.CreateInstanceDelegate vkCreateInstance;
//        private static Vk.DestroyInstanceDelegate vkDestroyInstance;

//        private static Vk.EnumeratePhysicalDevicesDelegate vkEnumeratePhysicalDevices;
//        private static Vk.EnumerateDeviceExtensionPropertiesDelegate vkEnumerateDeviceExtensionProperties;

//        private static Vk.GetPhysicalDevicePropertiesDelegate vkGetPhysicalDeviceProperties;
//        private static Vk.GetPhysicalDeviceMemoryPropertiesDelegate vkGetPhysicalDeviceMemoryProperties;
//        private static Vk.GetPhysicalDeviceQueueFamilyPropertiesDelegate vkGetPhysicalDeviceQueueFamilyProperties;
//        private static Vk.GetPhysicalDeviceFeaturesDelegate vkGetPhysicalDeviceFeatures;
//        private static Vk.GetPhysicalDeviceFormatPropertiesDelegate vkGetPhysicalDeviceFormatProperties;

//        private static Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate vkGetPhysicalDeviceSurfaceSupport;
//        private static Vk.GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate vkGetPhysicalDeviceSurfaceCapabilities;
//        private static Vk.GetPhysicalDeviceSurfaceFormatsKHRDelegate vkGetPhysicalDeviceSurfaceFormats;
//        private static Vk.GetPhysicalDeviceSurfacePresentModesKHRDelegate vkGetPhysicalDeviceSurfacePresentModes;

//        private static Vk.CreateDeviceDelegate vkCreateDevice;

//        private static Vk.GetDeviceQueueDelegate vkGetDeviceQueue;
//        private static Vk.CreateCommandPoolDelegate vkCreateCommandPool;
//        private static Vk.AllocateCommandBuffersDelegate vkAllocateCommandBuffers;
//        private static Vk.CreateFenceDelegate vkCreateFence;
//        private static Vk.CreateSemaphoreDelegate vkCreateSemaphore;

//        private static Vk.CreateSwapchainKHRDelegate vkCreateSwapchain;
//        private static Vk.DestroySwapchainKHRDelegate vkDestroySwapchain;
//        private static Vk.GetSwapchainImagesKHRDelegate vkGetSwapchainImages;
//        private static Vk.AcquireNextImageKHRDelegate vkAcquireNextImage;
//        private static Vk.QueuePresentKHRDelegate vkQueuePresent;
//#if DEBUG
//        private static Vk.CreateDebugReportCallbackEXTDelegate vkCreateDebugReportCallback;
//        private static Vk.DestroyDebugReportCallbackEXTDelegate vkDestroyDebugReportCallback;
//        private static Vk.DebugMarkerSetObjectNameEXTDelegate vkDebugMarkerSetObjectName;
//#endif

//        public bool HasFocus { get; set; }
//        public int Width { get; private set; }
//        public int Height { get; private set; }

//        private IntPtr _handle;
//#if PLATFORM_LINUX
//        private IntPtr _display;
//#endif
//        private SDL.Window _sdlWindow;

//        public Window(byte* title)
//        {
//            // Initializing SDL video subsystem.
//            if (VideoSubSystemInitialized == false)
//            {
//                SDL.InitSubSystem(SDL.InitFlags.Video).CheckError();
//                VideoSubSystemInitialized = true;
//            }

//            // Loading Vulkan loader library.
//            SDL.VulkanLoadLibrary(null).CheckError();

//            // Creating SDL window.
//            _sdlWindow = SDL.CreateWindow(
//                title,
//                SDL.WindowPositionCentered,
//                SDL.WindowPositionCentered,
//                GraphicsSystem.DisplayWidth,
//                GraphicsSystem.DisplayHeight,
//                SDL.WindowFlags.Shown | SDL.WindowFlags.Vulkan
//            );
//            _sdlWindow.CheckError();

//            // Getting window manager info.
//            SDL.SysWMInfo sysWMInfo = default(SDL.SysWMInfo);
//            SDL.GetVersion(out sysWMInfo.Version);
//            SDL.GetWindowWMInfo(_sdlWindow, ref sysWMInfo).CheckError();

//#if PLATFORM_WINDOWS
//            _handle = sysWMInfo.Info.Windows.Window;
//#elif PLATFORM_LINUX
//            switch(sysWMInfo.SubSystem)
//            {
//                case SDL.SysWMType.X11:
//                    Handle = sysWMInfo.Info.X11.Window;
//                    Display = sysWMInfo.Info.X11.Display;
//                    break;
//                case SDL.SysWMType.Wayland:
//                    Handle = sysWMInfo.Info.Wayland.Surface;
//                    Display = sysWMInfo.Info.Wayland.Display;
//                    break;
//                case SDL.SysWMType.Mir:
//                    Handle = sysWMInfo.Info.Mir.Connection;
//                    Display = sysWMInfo.Info.Mir.Surface;
//                    break;
//            }
//#elif PLATFORM_MACOS
//            Handle = sysWMInfo.Info.Cocoa.Window;
//#endif
//            // Initializing Vulkan.
//        }

//        public void Show()
//        {
//            SDL.ShowWindow(_sdlWindow);
//        }

//        public void SetMode(int width, int height, int refreshRate, int bpp, bool fullscreen)
//        {
//            // Creating SDL window.
//            if (_sdlWindow.Handle == IntPtr.Zero)
//            {
//                SDL.WindowFlags flags = SDL.WindowFlags.Hidden | SDL.WindowFlags.Vulkan;

//                //if (VariableManager.GetVar(""))
//            }
//        }

//        private void GetCurrentSize(out int width, out int height)
//        {
//            SDL.VulkanGetDrawableSize(_sdlWindow, out width, out height);
//        }

//        private int GetCurrentWidth()
//        {
//            SDL.VulkanGetDrawableSize(_sdlWindow, out int width, out int height);
//            return width;
//        }

//        private int GetCurrentHeight()
//        {
//            SDL.VulkanGetDrawableSize(_sdlWindow, out int width, out int height);
//            return height;
//        }
        

//        // TODO: Dispose of Vulkan stuff
//        private void Dispose(bool isDisposing)
//        {
//            if (_sdlWindow == IntPtr.Zero)
//            {
//                return;
//            }

//            SDL.DestroyWindow(_sdlWindow);
//            _sdlWindow = IntPtr.Zero;
//            _handle = IntPtr.Zero;
//#if PLATFORM_LINUX
//            _display = IntPtr.Zero;
//#endif
//        }

//        ~Window()
//        {
//            Dispose(false);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}