using System;

using CoreVulkan;

namespace Engine.Rendering
{
    public delegate Bool32 DebugCallback(
        DebugReportFlags messageFlags,
        DebugReportObjectType objectType,
        Text layerPrefix,
        Text message,
        IntPtr userData
    );

    public class Instance
    {
        // Members
        private Vulkan.Instance _instance;
        private Text _applicationName;
        private Text _engineName;
        private Vulkan.DebugReportCallback _debugCallbackHandle;
        private DebugCallback _debugCallback;
        private Text[] _enabledExtensions;
        private PhysicalDevice[] _physicalDevices;
        private LayerProperties _globalLayer;
        private LayerProperties[] _supportedLayers;

        // Vulkan functions
        private CreateDebugReportCallback _createDebugReportCallback;
        private DestroyDebugReportCallback _destroyDebugReportCallback;

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

        // Properties
        public int PhysicalDeviceCount => _physicalDevices.Length;
        
        public Instance(Text applicationName, Text engineName, DebugCallback debugCallback, IntPtr debugCallbackData)
        {
            _applicationName = applicationName;
            _engineName = engineName;
            _debugCallback = debugCallback;
            
        }

        public void Init()
        {

        }
    }
}