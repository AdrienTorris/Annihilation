using System;
using System.Runtime.InteropServices;

namespace CoreVulkan
{
    public unsafe struct PhysicalDevice
    {
        public PhysicalDeviceHandle Handle { get; private set; }
        public Instance Instance { get; private set; }

        private static GetPhysicalDevicePropertiesDelegate _getPhysicalDeviceProperties;
        private static GetPhysicalDeviceMemoryPropertiesDelegate _getPhysicalDeviceMemoryProperties;
        private static GetPhysicalDeviceQueueFamilyPropertiesDelegate _getPhysicalDeviceQueueFamilyProperties;
        private static GetPhysicalDeviceFeaturesDelegate _getPhysicalDeviceFeatures;
        private static GetPhysicalDeviceFormatPropertiesDelegate _getPhysicalDeviceFormatProperties;
        private static GetPhysicalDeviceImageFormatPropertiesDelegate _getPhysicalDeviceImageFormatProperties;
        private static GetPhysicalDeviceSurfaceSupportKHRDelegate _getPhysicalDeviceSurfaceSupportKHR;
        private static GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate _getPhysicalDeviceSurfaceCapabilitiesKHR;
        private static GetPhysicalDeviceSurfaceFormatsKHRDelegate _getPhysicalDeviceSurfaceFormatsKHR;
        private static GetPhysicalDeviceSurfacePresentModesKHRDelegate _getPhysicalDeviceSurfacePresentModesKHR;
        
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

        public void GetFeatures(out PhysicalDeviceFeatures features)
        {

            _getPhysicalDeviceFeatures(Handle, out features);
        }

        public void GetFormatProperties(Format format, out FormatProperties formatProperties)
        {

            _getPhysicalDeviceFormatProperties(Handle, format, out formatProperties);
        }

        public void GetImageFormatProperties(Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties)
        {

            _getPhysicalDeviceImageFormatProperties(Handle, format, type, tiling, usage, flags, out imageFormatProperties).CheckError();
        }

        public void GetQueueFamilyProperties(out uint count, out QueueFamilyProperties* properties)
        {

            count = 0;
            _getPhysicalDeviceQueueFamilyProperties(Handle, ref count, null);
            properties = (QueueFamilyProperties*)Marshal.AllocHGlobal((int)count * sizeof(QueueFamilyProperties));
            _getPhysicalDeviceQueueFamilyProperties(Handle, ref count, properties);
        }

        public void GetMemoryProperties(out PhysicalDeviceMemoryProperties memoryProperties)
        {

            _getPhysicalDeviceMemoryProperties(Handle, out memoryProperties);
        }

        public void CreateDevice(ref DeviceCreateInfo createInfo, out DeviceHandle device)
        {

            _createDevice(Handle, ref createInfo, null, out device).CheckError();
        }

        public void EnumerateDeviceExtensionProperties(out uint count, out ExtensionProperties* properties)
        {

            count = 0;
            _enumerateDeviceExtensionProperties(Handle, null, ref count, null).CheckError();
            properties = (ExtensionProperties*)Marshal.AllocHGlobal((int)count * sizeof(ExtensionProperties));
            _enumerateDeviceExtensionProperties(Handle, null, ref count, properties).CheckError();
        }

        /*public void GetSparseImageFormatProperties(Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, ImageCreateFlags flags, out uint count, out SparseImageFormatProperties* properties)
        {
            _gPDS(this, format, type, tiling, usage, flags, out imageFormatProperties).CheckError();
        }*/
        
    }
}