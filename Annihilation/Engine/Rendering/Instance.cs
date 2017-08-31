using System;
using Vulkan;

using Version = Vulkan.Version;

namespace Engine.Rendering
{
    public unsafe class Instance : IDisposable
    {
        // Members
        private Vk.Instance _instance;
        private bool _isDisposed = false;

#if ENABLE_VALIDATION
        public DebugReport DebugReport { get; set; }
#endif

        // Vulkan global functions
        private static EnumerateInstanceExtensionProperties EnumerateInstanceExtensionProperties;
        private static EnumerateInstanceLayerProperties EnumerateInstanceLayerProperties;
        private static CreateInstance CreateInstance;

        // Vulkan instance functions
        public static DestroyInstance DestroyInstance;
        
        public Instance(Text applicationName, Text engineName)
        {
            // Load global functions
            EnumerateInstanceExtensionProperties = Vk.LoadGlobalFunction<EnumerateInstanceExtensionProperties>();
            EnumerateInstanceLayerProperties = Vk.LoadGlobalFunction<EnumerateInstanceLayerProperties>();
            CreateInstance = Vk.LoadGlobalFunction<CreateInstance>();

            // Find available instance extensions
            EnumerateInstanceExtensionProperties(Text.Null, out uint extensionCount, null).CheckError();
            ExtensionProperties[] availableExtensions = new ExtensionProperties[(int)extensionCount];
            EnumerateInstanceExtensionProperties(Text.Null, out extensionCount, availableExtensions).CheckError();

            // Check if the desired instance extensions are available
            Text[] desiredExtensions = new Text[]
            {
                Vk.SurfaceExtensionName,
                global::Game.WindowType == WindowType.Win32 ? Vk.Win32SurfaceExtensionName :
                global::Game.WindowType == WindowType.Xlib ? Vk.XlibSurfaceExtensionName :
                global::Game.WindowType == WindowType.Xcb ? Vk.XcbSurfaceExtensionName :
                global::Game.WindowType == WindowType.Mir ? Vk.MirSurfaceExtensionName :
                global::Game.WindowType == WindowType.Wayland ? Vk.WaylandSurfaceExtensionName :
                global::Game.WindowType == WindowType.Android ? Vk.AndroidSurfaceExtensionName :
                global::Game.WindowType == WindowType.IOS ? Vk.IOSSurfaceExtensionName :
                global::Game.WindowType == WindowType.MacOS ? Vk.MacOSSurfaceExtensionName :
                global::Game.WindowType == WindowType.Switch ? Vk.ViSurfaceExtensionName :
                throw new PlatformNotSupportedException(),
#if ENABLE_VALIDATION
                Vk.DebugReportExtensionName
#endif
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

#if ENABLE_VALIDATION
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
#endif

            // Create the instance
            ApplicationInfo applicationInfo = new ApplicationInfo(
                applicationName, 
                Version.One, 
                engineName, 
                Version.One, 
                Version.One
            );
            InstanceCreateInfo instanceCreateInfo = new InstanceCreateInfo(
                &applicationInfo,
#if ENABLE_VALIDATION
                desiredLayers,
#endif
                desiredExtensions
            );

            CreateInstance(ref instanceCreateInfo, ref AllocationCallbacks.Null, out _instance).CheckError();

#if ENABLE_VALIDATION
            DebugReport = new DebugReport(_instance);
#endif

            // Load instance functions
            DestroyInstance = Vk.LoadInstanceFunction<DestroyInstance>(_instance);
        }
        
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
#if ENABLE_VALIDATION
                    DebugReport.Dispose();
#endif
                }

                DestroyInstance(_instance, ref AllocationCallbacks.Null);

                EnumerateInstanceExtensionProperties = null;
                EnumerateInstanceLayerProperties = null;
                CreateInstance = null;
                DestroyInstance = null;

                _isDisposed = true;

                Log.Info("Vulkan instance destroyed");
            }
        }
        
        ~Instance()
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