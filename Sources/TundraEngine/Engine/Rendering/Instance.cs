using System;
using CoreVulkan;

using Version = CoreVulkan.Version;

namespace Engine.Rendering
{
    public unsafe class Instance
    {
        // Members
        private Vulkan.Instance _instance;

        public DebugReport DebugReport { get; set; }

        // Vulkan global functions
        private EnumerateInstanceExtensionProperties EnumerateInstanceExtensionProperties;
        private EnumerateInstanceLayerProperties EnumerateInstanceLayerProperties;
        private CreateInstance CreateInstance;

        // Vulkan instance functions
        private DestroySurface _destroySurface;
        private GetPhysicalDeviceSurfaceCapabilities _getPhysicalDeviceSurfaceCapabilities;
        private GetPhysicalDeviceSurfaceFormats _getPhysicalDeviceSurfaceFormats;
        private GetPhysicalDeviceSurfacePresentModes _getPhysicalDeviceSurfacePresentModes;
        private GetPhysicalDeviceSurfaceSupport _getPhysicalDeviceSurfaceSupport;
        private GetPhysicalDeviceFeatures2 _getPhysicalDeviceFeatures2;
        private GetPhysicalDeviceFormatProperties2 _getPhysicalDeviceFormatProperties2;
        private GetPhysicalDeviceImageFormatProperties2 _getPhysicalDeviceImageFormatProperties2;
        private GetPhysicalDeviceMemoryProperties2 _getPhysicalDeviceMemoryProperties2;
        private GetPhysicalDeviceProperties2 _getPhysicalDeviceProperties2;
        private GetPhysicalDeviceQueueFamilyProperties2 _getPhysicalDeviceQueueFamilyProperties2;
        private GetPhysicalDeviceSparseImageFormatProperties2 _getPhysicalDeviceSparseImageFormatProperties2;
        private CreateWin32Surface _createWin32Surface;
        private GetPhysicalDeviceWin32PresentationSupport _getPhysicalDeviceWin32PresentationSupport;
        private CreateXcbSurface _createXcbSurface;
        private GetPhysicalDeviceXcbPresentationSupport _getPhysicalDeviceXcbPresentationSupport;
        // TODO: Other platform functions here
        
        public void Create(Text applicationName, Text engineName, Text[] layers, Text[] extensions)
        {
            // Load global functions
            EnumerateInstanceExtensionProperties = Vulkan.LoadGlobalFunction<EnumerateInstanceExtensionProperties>();
            EnumerateInstanceLayerProperties = Vulkan.LoadGlobalFunction<EnumerateInstanceLayerProperties>();
            CreateInstance = Vulkan.LoadGlobalFunction<CreateInstance>();

            // Find available instance extensions
            EnumerateInstanceExtensionProperties(Text.Null, out uint extensionCount, null).CheckError();
            ExtensionProperties[] availableExtensions = new ExtensionProperties[(int)extensionCount];
            EnumerateInstanceExtensionProperties(Text.Null, out extensionCount, availableExtensions).CheckError();

            // Check if the desired instance extensions are available
            Text[] desiredExtensions = new Text[]
            {
                Vulkan.SurfaceExtensionName,
                Game.WindowType == WindowType.Win32 ? Vulkan.Win32SurfaceExtensionName :
                Game.WindowType == WindowType.Xlib ? Vulkan.XlibSurfaceExtensionName :
                Game.WindowType == WindowType.Xcb ? Vulkan.XcbSurfaceExtensionName :
                Game.WindowType == WindowType.Mir ? Vulkan.MirSurfaceExtensionName :
                Game.WindowType == WindowType.Wayland ? Vulkan.WaylandSurfaceExtensionName :
                Game.WindowType == WindowType.Android ? Vulkan.AndroidSurfaceExtensionName :
                Game.WindowType == WindowType.IOS ? Vulkan.IOSSurfaceExtensionName :
                Game.WindowType == WindowType.MacOS ? Vulkan.MacOSSurfaceExtensionName :
                Game.WindowType == WindowType.Switch ? Vulkan.ViSurfaceExtensionName :
                throw new PlatformNotSupportedException(),
                Game.Settings.RendererSettings.EnableDebugReport ? Vulkan.DebugReportExtensionName : string.Empty
            };

            foreach (string desiredExtension in desiredExtensions)
            {
                foreach (ExtensionProperties availableExtension in availableExtensions)
                {
                    if (!availableExtension.ExtensionName.Compare(desiredExtension))
                    {
                        throw new InvalidOperationException("Extension " + desiredExtension + " is not supported");
                    }
                }
            }

            if (Game.Settings.RendererSettings.EnableValidation)
            {
                // Find available instance layers
                EnumerateInstanceLayerProperties(out uint layerCount, null).CheckError();
                LayerProperties[] availableLayers = new LayerProperties[layerCount];
                EnumerateInstanceLayerProperties(out layerCount, availableLayers).CheckError();

                // Check if the desired instance layers are available
                Text[] desiredLayers = new Text[]
                {
                    "VK_LAYER_LUNARG_standard_validation"
                };

                foreach (string desiredLayer in desiredLayers)
                {
                    foreach (LayerProperties availableLayer in availableLayers)
                    {
                        if (!availableLayer.LayerName.Compare(desiredLayer))
                        {
                            throw new InvalidOperationException("Layer " + desiredLayer + " is not supported");
                        }
                    }
                }
            }

            // Create the instance
            ApplicationInfo applicationInfo = new ApplicationInfo(
                _applicationName, 
                Version.One, 
                _engineName, 
                Version.One, 
                Version.One
            );
            InstanceCreateInfo instanceCreateInfo = new InstanceCreateInfo(
                &applicationInfo, 
                desiredLayers, 
                desiredExtensions
            );
            CreateInstance(ref instanceCreateInfo, ref AllocationCallbacks.Null, out _instance).CheckError();
        }
    }
}