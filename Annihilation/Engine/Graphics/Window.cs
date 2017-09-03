using System;
using System.Runtime.InteropServices;
using Engine.Config;
using SDL2;
using Vulkan;

namespace Engine.Graphics
{
    public unsafe class Window : IDisposable
    {
        public const int DefaultWidth = 1280;
        public const int DefaultHeight = 720;

        public static bool VideoSubSystemInitialized { get; private set; }

        private static Vk.CreateInstanceDelegate vkCreateInstance;
        private static Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate vkGetPhysicalDeviceSurfaceSupport;
        private static Vk.GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate vkGetPhysicalDeviceSurfaceCapabilities;
        private static Vk.GetPhysicalDeviceSurfaceFormatsKHRDelegate vkGetPhysicalDeviceSurfaceFormats;
        private static Vk.GetPhysicalDeviceSurfacePresentModesKHRDelegate vkGetPhysicalDeviceSurfacePresentModes;
        private static Vk.CreateSwapchainKHRDelegate vkCreateSwapchain;
        private static Vk.DestroySwapchainKHRDelegate vkDestroySwapchain;
        private static Vk.GetSwapchainImagesKHRDelegate vkGetSwapchainImages;
        private static Vk.AcquireNextImageKHRDelegate vkAcquireNextImage;
        private static Vk.QueuePresentKHRDelegate vkQueuePresent;
#if DEBUG
        private static Vk.CreateDebugReportCallbackEXTDelegate vkCreateDebugReportCallback;
        private static Vk.DestroyDebugReportCallbackEXTDelegate vkDestroyDebugReportCallback;
        private static Vk.DebugMarkerObjectNameInfo vkDebugMarkerObjectName;
#endif

        public bool HasFocus { get; set; }

        private Application _application;
        private IntPtr _handle;
#if PLATFORM_LINUX
        private IntPtr _display;
#endif
        private SDL.Window _sdlWindow;
        private Vk.Instance _vulkanInstance;
        private Vk.Surface _vulkanSurface;
        private uint _currentSwapchainBuffer;
#if DEBUG
        private Vk.DebugReportCallback _debugReportCallback;

        private static Vk.Bool32 DebugMessageCallback(Vk.DebugReportFlags flags, Vk.DebugReportObjectType objectType, ulong @object, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData)
        {
            if (objectType == Vk.DebugReportObjectType.DebugReportCallback && messageCode == 1)
            {
                return false;
            }

            Text layerPrefixText = new Text(layerPrefix, 32);
            Text messageText = new Text(message, 64);
            string output = "[" + layerPrefixText + "] Code " + messageCode + ": " + messageText;

            switch (flags)
            {
                case Vk.DebugReportFlags.Information: Log.Info(output); return false;
                case Vk.DebugReportFlags.Warning: Log.Warning(output); return false;
                case Vk.DebugReportFlags.PerformanceWarning: Log.Performance(output); return false;
                case Vk.DebugReportFlags.Error: Log.Error(output); return true;
                case Vk.DebugReportFlags.Debug: Log.Info(output); return false;
                default: return false;
            }
        }
#endif

        public Window(Application application)
        {
            _application = application;
            
            Log.Info("Initializing SDL video subsystem.");
            if (VideoSubSystemInitialized == false)
            {
                SDL.InitSubSystem(SDL.InitFlags.Video).CheckError();
                VideoSubSystemInitialized = true;
            }
            
            Log.Info("Loading Vulkan loader library.");
            SDL.VulkanLoadLibrary(null);
            
            Log.Info("Creating SDL window.");
            _sdlWindow = SDL.CreateWindow(
                application.ApplicationSettings.Title,
                SDL.WindowPositionUndefined,
                SDL.WindowPositionUndefined, 
                ConfigManager.GetVar(Application.ConfigResolutionX, 1280), 
                ConfigManager.GetVar(Application.ConfigResolutionY, 720),
                SDL.WindowFlags.Shown | SDL.WindowFlags.Vulkan
            );
            _sdlWindow.CheckError();

            Log.Info("Getting window manager info.");
            SDL.SysWMInfo sysWMInfo = default(SDL.SysWMInfo);
            SDL.GetVersion(out sysWMInfo.Version);
            SDL.GetWindowWMInfo(_sdlWindow, ref sysWMInfo).CheckError();

#if PLATFORM_WINDOWS
            _handle = sysWMInfo.Info.Windows.Window;
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
            Log.Info("Initializing Vulkan.");
            InitInstance();
            InitDevice();
            InitCommandBuffers();
            CreateSwapchain();
            CreateColorBuffer();
            CreateDepthBuffer();
            CreateRenderPasses();
            CreateFramebuffers();
            InitStagingBuffers();
            CreateDescriptorSetLayouts();
            CreateDescriptorPool();
            InitDynamicBuffers();
            InitSamplers();
            CreatePipelineLayouts();
            CreatePipelines();
            CreateDescriptorSets();
        }

        private void InitInstance()
        {
            Log.Info("Loading Vulkan getInstanceProcAddr function.");
            Vk.GetInstanceProcAddr = Vk.GetInstanceProcAddr ?? Marshal.GetDelegateForFunctionPointer<Vk.GetInstanceProcAddrDelegate>(SDL.VulkanGetVkGetInstanceProcAddr());

            Log.Info("Getting required Vulkan instance extensions for surface.");
            uint extensionCount = 0;
            SDL.VulkanGetInstanceExtensions(_sdlWindow, ref extensionCount, null).CheckError();

            byte*[] extensionNames = new byte*[extensionCount + 1];
            SDL.VulkanGetInstanceExtensions(_sdlWindow, ref extensionCount, extensionNames).CheckError();

#if DEBUG
            bool enableValidation = ConfigManager.GetVar(Application.ConfigEnableVulkanValidation, true);
            if (enableValidation)
            {
                extensionNames[extensionCount] = new Text(Vk.ExtDebugReportExtensionName);
            }
#endif
            Log.Info("Loading Vulkan createInstance function.");
            vkCreateInstance = vkCreateInstance ?? Vk.LoadGlobalFunction<Vk.CreateInstanceDelegate>();

            Log.Info("Creating Vulkan instance.");
            Vk.ApplicationInfo applicationInfo = new Vk.ApplicationInfo(
                _application.ApplicationSettings.Title,
                Vulkan.Version.One,
                _application.ApplicationSettings.Title,
                Vulkan.Version.One,
                Vulkan.Version.One
            );

            Vk.InstanceCreateInfo instanceCreateInfo = new Vk.InstanceCreateInfo(
                &applicationInfo,
                extensionCount,
                extensionNames
            );
#if DEBUG
            if (enableValidation)
            {
                byte*[] layerNames = new byte*[] { "VK_LAYER_LUNARG_standard_validation".ToText() };

                instanceCreateInfo.EnabledExtensionCount = extensionCount + 1;
                instanceCreateInfo.EnabledLayerCount = 1;
                fixed (byte** ptr = &layerNames[0])
                {
                    instanceCreateInfo.EnabledLayerNames = ptr;
                }
            }
#endif
            vkCreateInstance(ref instanceCreateInfo, null, out _vulkanInstance).CheckError();

            Log.Info("Creating Vulkan surface.");
            SDL.VulkanCreateSurface(_sdlWindow, _vulkanInstance, out _vulkanSurface).CheckError();

            Log.Info("Loading Vulkan getDeviceProcAddr function.");
            Vk.GetDeviceProcAddr = Vk.LoadInstanceFunction<Vk.GetDeviceProcAddrDelegate>(_vulkanInstance);

            Log.Info("Loading other Vulkan instance functions.");
            vkGetPhysicalDeviceSurfaceSupport = Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceSurfaceCapabilities = Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceSurfaceFormats = Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfaceFormatsKHRDelegate>(_vulkanInstance);
            vkGetPhysicalDeviceSurfacePresentModes = Vk.LoadInstanceFunction<Vk.GetPhysicalDeviceSurfacePresentModesKHRDelegate>(_vulkanInstance);
            vkGetSwapchainImages = Vk.LoadInstanceFunction<Vk.GetSwapchainImagesKHRDelegate>(_vulkanInstance);

#if DEBUG
            if (enableValidation)
            {
                vkCreateDebugReportCallback = Vk.LoadInstanceFunction<Vk.CreateDebugReportCallbackEXTDelegate>(_vulkanInstance);
                vkDestroyDebugReportCallback = Vk.LoadInstanceFunction<Vk.DestroyDebugReportCallbackEXTDelegate>(_vulkanInstance);

                Log.Info("Creating Vulkan debug report callback.");
                Vk.DebugReportCallbackCreateInfo debugReportCallbackCreateInfo = new Vk.DebugReportCallbackCreateInfo(
                    Vk.DebugReportFlags.Error | Vk.DebugReportFlags.Warning | Vk.DebugReportFlags.PerformanceWarning,
                    DebugMessageCallback
                );

                vkCreateDebugReportCallback(_vulkanInstance, ref debugReportCallbackCreateInfo, null, out _debugReportCallback).CheckError();
            }
#endif

            // TODO: Free allocated Texts?
        }

        private void InitDevice()
        {

        }

        private void InitCommandBuffers()
        {

        }

        private void CreateSwapchain()
        {

        }

        private void CreateColorBuffer()
        {

        }

        private void CreateDepthBuffer()
        {

        }

        private void CreateRenderPasses()
        {

        }

        private void CreateFramebuffers()
        {

        }

        private void InitStagingBuffers()
        {

        }

        private void CreateDescriptorSetLayouts()
        {

        }

        private void CreateDescriptorPool()
        {

        }

        private void InitDynamicBuffers()
        {

        }

        private void InitSamplers()
        {

        }

        private void CreatePipelineLayouts()
        {

        }

        private void CreatePipelines()
        {

        }

        private void CreateDescriptorSets()
        {

        }

        // TODO: Dispose of Vulkan stuff
        private void Dispose(bool isDisposing)
        {
            if (_sdlWindow == IntPtr.Zero)
            {
                return;
            }

            if (isDisposing)
            {
                _application = null;
            }

            SDL.DestroyWindow(_sdlWindow);
            _sdlWindow = IntPtr.Zero;
            _handle = IntPtr.Zero;
#if PLATFORM_LINUX
            _display = IntPtr.Zero;
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