using System;
using System.Runtime.InteropServices;

namespace CoreVulkan
{
    public unsafe struct PhysicalDevice
    {
        public PhysicalDeviceHandle Handle { get; private set; }
        public Instance Instance { get; private set; }

        private static GetPhysicalDevicePropertiesDelegate _getPhysicalDeviceProperties;
        private static GetPhysicalDeviceQueueFamilyPropertiesDelegate _getPhysicalDeviceQueueFamilyProperties;
        private static GetPhysicalDeviceMemoryPropertiesDelegate _getPhysicalDeviceMemoryProperties;
        private static GetPhysicalDeviceFeaturesDelegate _getPhysicalDeviceFeatures;
        private static GetPhysicalDeviceFormatPropertiesDelegate _getPhysicalDeviceFormatProperties;
        private static GetPhysicalDeviceImageFormatPropertiesDelegate _getPhysicalDeviceImageFormatProperties;
        private static CreateDeviceDelegate _createDevice;
        private static EnumerateDeviceExtensionPropertiesDelegate _enumerateDeviceExtensionProperties;
        private static GetPhysicalDeviceSparseImageFormatPropertiesDelegate _getPhysicalDeviceSparseImageFormatProperties;
        private static GetPhysicalDeviceDisplayPropertiesKHRDelegate _getPhysicalDeviceDisplayPropertiesKHR;
        private static GetPhysicalDeviceDisplayPlanePropertiesKHRDelegate _getPhysicalDeviceDisplayPlanePropertiesKHR;
        private static GetDisplayPlaneSupportedDisplaysKHRDelegate _getDisplayPlaneSupportedDisplaysKHR;
        private static GetDisplayModePropertiesKHRDelegate _getDisplayModePropertiesKHR;
        private static CreateDisplayModeKHRDelegate _createDisplayModeKHR;
        private static GetDisplayPlaneCapabilitiesKHRDelegate _getDisplayPlaneCapabilitiesKHR;
        private static GetPhysicalDeviceMirPresentationSupportKHRDelegate _getPhysicalDeviceMirPresentationSupportKHR;
        private static GetPhysicalDeviceSurfaceSupportKHRDelegate _getPhysicalDeviceSurfaceSupportKHR;
        private static GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate _getPhysicalDeviceSurfaceCapabilitiesKHR;
        private static GetPhysicalDeviceSurfaceFormatsKHRDelegate _getPhysicalDeviceSurfaceFormatsKHR;
        private static GetPhysicalDeviceSurfacePresentModesKHRDelegate _getPhysicalDeviceSurfacePresentModesKHR;
        private static GetPhysicalDeviceWaylandPresentationSupportKHRDelegate _getPhysicalDeviceWaylandPresentationSupportKHR;
        private static GetPhysicalDeviceWin32PresentationSupportKHRDelegate _getPhysicalDeviceWin32PresentationSupportKHR;
        private static GetPhysicalDeviceXlibPresentationSupportKHRDelegate _getPhysicalDeviceXlibPresentationSupportKHR;
        private static GetPhysicalDeviceXcbPresentationSupportKHRDelegate _getPhysicalDeviceXcbPresentationSupportKHR;
        private static GetPhysicalDeviceExternalImageFormatPropertiesNVDelegate _getPhysicalDeviceExternalImageFormatPropertiesNV;
        private static GetPhysicalDeviceGeneratedCommandsPropertiesNVXDelegate _getPhysicalDeviceGeneratedCommandsPropertiesNVX;
        private static GetPhysicalDeviceFeatures2KHRDelegate _getPhysicalDeviceFeatures2KHR;
        private static GetPhysicalDeviceProperties2KHRDelegate _getPhysicalDeviceProperties2KHR;
        private static GetPhysicalDeviceFormatProperties2KHRDelegate _getPhysicalDeviceFormatProperties2KHR;
        private static GetPhysicalDeviceImageFormatProperties2KHRDelegate _getPhysicalDeviceImageFormatProperties2KHR;
        private static GetPhysicalDeviceQueueFamilyProperties2KHRDelegate _getPhysicalDeviceQueueFamilyProperties2KHR;
        private static GetPhysicalDeviceMemoryProperties2KHRDelegate _getPhysicalDeviceMemoryProperties2KHR;
        private static GetPhysicalDeviceSparseImageFormatProperties2KHRDelegate _getPhysicalDeviceSparseImageFormatProperties2KHR;
        private static GetPhysicalDeviceExternalBufferPropertiesKHRDelegate _getPhysicalDeviceExternalBufferPropertiesKHR;
        private static GetPhysicalDeviceExternalSemaphorePropertiesKHRDelegate _getPhysicalDeviceExternalSemaphorePropertiesKHR;
        private static GetPhysicalDeviceExternalFencePropertiesKHRDelegate _getPhysicalDeviceExternalFencePropertiesKHR;
        private static ReleaseDisplayEXTDelegate _releaseDisplayEXT;
        private static AcquireXlibDisplayEXTDelegate _acquireXlibDisplayEXT;
        private static GetRandROutputDisplayEXTDelegate _getRandROutputDisplayEXT;
        private static GetPhysicalDeviceSurfaceCapabilities2EXTDelegate _getPhysicalDeviceSurfaceCapabilities2EXT;
        private static GetPhysicalDevicePresentRectanglesKHXDelegate _getPhysicalDevicePresentRectanglesKHX;
        private static GetPhysicalDeviceSurfaceCapabilities2KHRDelegate _getPhysicalDeviceSurfaceCapabilities2KHR;
        private static GetPhysicalDeviceSurfaceFormats2KHRDelegate _getPhysicalDeviceSurfaceFormats2KHR;

        public PhysicalDevice(PhysicalDeviceHandle handle, Instance instance)
        {
            Handle = handle;
            Instance = instance;
        }

        public void GetProperties(out PhysicalDeviceProperties properties)
        {
            _getPhysicalDeviceProperties = _getPhysicalDeviceProperties ?? Instance.GetProcAddr<GetPhysicalDevicePropertiesDelegate>(FunctionName.GetPhysicalDeviceProperties);

            _getPhysicalDeviceProperties(Handle, out properties);
        }

        public void GetQueueFamilyProperties(out QueueFamilyProperties[] familyProperties)
        {
            _getPhysicalDeviceQueueFamilyProperties = _getPhysicalDeviceQueueFamilyProperties ?? Instance.GetProcAddr<GetPhysicalDeviceQueueFamilyPropertiesDelegate>(FunctionName.GetPhysicalDeviceQueueFamilyProperties);

            uint count = 0;
            _getPhysicalDeviceQueueFamilyProperties(Handle, ref count, null);
            QueueFamilyProperties* properties = (QueueFamilyProperties*)Marshal.AllocHGlobal((int)count * sizeof(QueueFamilyProperties));
            _getPhysicalDeviceQueueFamilyProperties(Handle, ref count, properties);

            familyProperties = new QueueFamilyProperties[count];
            for (int i = 0; i < count; ++i)
            {
                familyProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void GetMemoryProperties(out PhysicalDeviceMemoryProperties memoryProperties)
        {
            _getPhysicalDeviceMemoryProperties = _getPhysicalDeviceMemoryProperties ?? Instance.GetProcAddr<GetPhysicalDeviceMemoryPropertiesDelegate>(FunctionName.GetPhysicalDeviceMemoryProperties);

            _getPhysicalDeviceMemoryProperties(Handle, out memoryProperties);
        }

        public void GetFeatures(out PhysicalDeviceFeatures features)
        {
            _getPhysicalDeviceFeatures = _getPhysicalDeviceFeatures ?? Instance.GetProcAddr<GetPhysicalDeviceFeaturesDelegate>(FunctionName.GetPhysicalDeviceFeatures);

            _getPhysicalDeviceFeatures(Handle, out features);
        }

        public void GetFormatProperties(Format format, out FormatProperties formatProperties)
        {
            _getPhysicalDeviceFormatProperties = _getPhysicalDeviceFormatProperties ?? Instance.GetProcAddr<GetPhysicalDeviceFormatPropertiesDelegate>(FunctionName.GetPhysicalDeviceFormatProperties);

            _getPhysicalDeviceFormatProperties(Handle, format, out formatProperties);
        }

        public void GetImageFormatProperties(Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties)
        {
            _getPhysicalDeviceImageFormatProperties = _getPhysicalDeviceImageFormatProperties ?? Instance.GetProcAddr<GetPhysicalDeviceImageFormatPropertiesDelegate>(FunctionName.GetPhysicalDeviceImageFormatProperties);

            _getPhysicalDeviceImageFormatProperties(Handle, format, type, tiling, usage, flags, out imageFormatProperties).CheckError();
        }

        public void CreateDevice(ref DeviceCreateInfo createInfo, out Device device)
        {
            _createDevice = _createDevice ?? Instance.GetProcAddr<CreateDeviceDelegate>(FunctionName.CreateDevice);

            _createDevice(Handle, ref createInfo, null, out DeviceHandle handle).CheckError();

            device = new Device(handle, this);
        }

        public void EnumerateDeviceExtensionProperties(out ExtensionProperties[] extensionProperties)
        {
            _enumerateDeviceExtensionProperties = _enumerateDeviceExtensionProperties ?? Instance.GetProcAddr<EnumerateDeviceExtensionPropertiesDelegate>(FunctionName.EnumerateDeviceExtensionProperties);

            uint count = 0;
            _enumerateDeviceExtensionProperties(Handle, null, ref count, null).CheckError();
            ExtensionProperties* properties = (ExtensionProperties*)Marshal.AllocHGlobal((int)count * sizeof(ExtensionProperties));
            _enumerateDeviceExtensionProperties(Handle, null, ref count, properties).CheckError();

            extensionProperties = new ExtensionProperties[count];
            for (int i = 0; i < count; ++i)
            {
                extensionProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void GetSparseImageFormatProperties(Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, ImageCreateFlags flags, out SparseImageFormatProperties[] formatProperties)
        {
            _getPhysicalDeviceSparseImageFormatProperties = _getPhysicalDeviceSparseImageFormatProperties ?? Instance.GetProcAddr<GetPhysicalDeviceSparseImageFormatPropertiesDelegate>(FunctionName.GetPhysicalDeviceSparseImageFormatProperties);

            uint count = 0;
            _getPhysicalDeviceSparseImageFormatProperties(Handle, format, type, samples, usage, tiling, ref count, null);
            SparseImageFormatProperties* properties = (SparseImageFormatProperties*)Marshal.AllocHGlobal((int)count * sizeof(SparseImageFormatProperties));
            _getPhysicalDeviceSparseImageFormatProperties(Handle, format, type, samples, usage, tiling, ref count, properties);

            formatProperties = new SparseImageFormatProperties[count];
            for (int i = 0; i < count; ++i)
            {
                formatProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }
        
        public void GetDisplayPropertiesKHR(out DisplayProperties[] displayProperties)
        {
            _getPhysicalDeviceDisplayPropertiesKHR = _getPhysicalDeviceDisplayPropertiesKHR ?? Instance.GetProcAddr<GetPhysicalDeviceDisplayPropertiesKHRDelegate>(FunctionName.GetPhysicalDeviceDisplayPropertiesKHR);

            uint count = 0;
            _getPhysicalDeviceDisplayPropertiesKHR(Handle, ref count, null).CheckError();
            DisplayProperties* properties = (DisplayProperties*)Marshal.AllocHGlobal((int)count * sizeof(DisplayProperties));
            _getPhysicalDeviceDisplayPropertiesKHR(Handle, ref count, properties).CheckError();

            displayProperties = new DisplayProperties[count];
            for (int i = 0; i < count; ++i)
            {
                displayProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void GetDisplayPlanePropertiesKHR()
        {

        }

        public void GetDisplayPlaneSupportedDisplaysKHR()
        {

        }

        public void GetDisplayModePropertiesKHR()
        {

        }

        public void CreateDisplayModeKHR()
        {

        }

        public void GetDisplayPlaneCapabilitiesKHR()
        {

        }

        public void GetMirPresentationSupportKHR()
        {

        }

        public void GetSurfaceSupportKHR()
        {

        }

        public void GetSurfaceCapabilitiesKHR()
        {

        }

        public void GetSurfaceFormatsKHR()
        {

        }

        public void GetSurfacePresentModesKHR()
        {

        }

        public void GetWaylandPresentationSupportKHR()
        {

        }

        public void GetWin32PresentationSupportKHR()
        {

        }

        public void GetXlibPresentationSupport()
        {

        }

        public void GetXcbPresentationSupport()
        {

        }

        public void GetExternalImageFormatPropertiesNV()
        {

        }

        public void GetGeneratedCommandsPropertiesNVX()
        {

        }

        public void GetFeatures2KHR()
        {

        }

        public void GetProperties2KHR()
        {

        }

        public void GetFormatProperties2KHR()
        {

        }

        public void GetImageFormatProperties2KHR()
        {

        }

        public void GetQueueFamilyProperties2KHR()
        {

        }

        public void GetMemoryProperties2KHR()
        {

        }

        public void GetSparseImageFormatProperties2KHR()
        {

        }

        public void GetExternalBufferPropertiesKHR()
        {

        }

        public void GetExternalSemaphorePropertiesKHR()
        {

        }

        public void GetExternalFencePropertiesKHR()
        {

        }

        public void ReleaseDisplayEXT()
        {

        }

        public void AcquireXlibDisplayEXT()
        {

        }

        public void GetRandROutputDisplayEXT()
        {

        }

        public void GetSurfaceCapabilities2EXT()
        {

        }

        public void GetPresentRectanglesKHX()
        {

        }

        public void GetSurfaceCapabilities2KHR()
        {

        }

        public void GetSurfaceFormats2KHR()
        {

        }
    }
}