using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Vulkan
{
    public unsafe partial struct PhysicalDevice
    {
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
        
        public void LoadFunctions(Instance instance)
        {
            Trace.Assert(instance != Instance.Null);

            _getPhysicalDeviceProperties = Vk.LoadInstanceFunction<GetPhysicalDevicePropertiesDelegate>(instance, FunctionName.GetPhysicalDeviceProperties);
            _getPhysicalDeviceMemoryProperties = Vk.LoadInstanceFunction<GetPhysicalDeviceMemoryPropertiesDelegate>(instance, FunctionName.GetPhysicalDeviceMemoryProperties);
            _getPhysicalDeviceQueueFamilyProperties = Vk.LoadInstanceFunction<GetPhysicalDeviceQueueFamilyPropertiesDelegate>(instance, FunctionName.GetPhysicalDeviceQueueFamilyProperties);
            _getPhysicalDeviceFeatures = Vk.LoadInstanceFunction<GetPhysicalDeviceFeaturesDelegate>(instance, FunctionName.GetPhysicalDeviceFeatures);
            _getPhysicalDeviceFormatProperties = Vk.LoadInstanceFunction<GetPhysicalDeviceFormatPropertiesDelegate>(instance, FunctionName.GetPhysicalDeviceFormatProperties);
            _getPhysicalDeviceImageFormatProperties = Vk.LoadInstanceFunction<GetPhysicalDeviceImageFormatPropertiesDelegate>(instance, FunctionName.GetPhysicalDeviceImageFormatProperties);
            _getPhysicalDeviceSurfaceSupportKHR = Vk.LoadInstanceFunction<GetPhysicalDeviceSurfaceSupportKHRDelegate>(instance, FunctionName.GetPhysicalDeviceSurfaceSupportKHR);
            _getPhysicalDeviceSurfaceCapabilitiesKHR = Vk.LoadInstanceFunction<GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate>(instance, FunctionName.GetPhysicalDeviceSurfaceCapabilitiesKHR);
            _getPhysicalDeviceSurfaceFormatsKHR = Vk.LoadInstanceFunction<GetPhysicalDeviceSurfaceFormatsKHRDelegate>(instance, FunctionName.GetPhysicalDeviceSurfaceFormatsKHR);
            _getPhysicalDeviceSurfacePresentModesKHR = Vk.LoadInstanceFunction<GetPhysicalDeviceSurfacePresentModesKHRDelegate>(instance, FunctionName.GetPhysicalDeviceSurfacePresentModesKHR);
        }

        public void GetFeatures(out PhysicalDeviceFeatures features)
        {
            Trace.Assert(_getPhysicalDeviceFeatures != null);

            _getPhysicalDeviceFeatures(this, out features);
        }

        public void GetFormatProperties(Format format, out FormatProperties formatProperties)
        {
            Trace.Assert(_getPhysicalDeviceFormatProperties != null);

            _getPhysicalDeviceFormatProperties(this, format, out formatProperties);
        }

        public void GetImageFormatProperties(Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties)
        {
            Trace.Assert(_getPhysicalDeviceImageFormatProperties != null);

            _getPhysicalDeviceImageFormatProperties(this, format, type, tiling, usage, flags, out imageFormatProperties).CheckError();
        }

        public void GetProperties(out PhysicalDeviceProperties properties)
        {
            Trace.Assert(_getPhysicalDeviceProperties != null);

            _getPhysicalDeviceProperties(this, out properties);
        }

        public void GetQueueFamilyProperties(out uint count, out QueueFamilyProperties* properties)
        {
            Trace.Assert(_getPhysicalDeviceQueueFamilyProperties != null);

            count = 0;
            _getPhysicalDeviceQueueFamilyProperties(this, ref count, null);
            properties = (QueueFamilyProperties*)Marshal.AllocHGlobal((int)count * sizeof(QueueFamilyProperties));
            _getPhysicalDeviceQueueFamilyProperties(this, ref count, properties);
        }

        public void GetMemoryProperties(out PhysicalDeviceMemoryProperties memoryProperties)
        {
            Trace.Assert(_getPhysicalDeviceMemoryProperties != null);

            _getPhysicalDeviceMemoryProperties(this, out memoryProperties);
        }

        public void CreateDevice(ref DeviceCreateInfo createInfo, out Device device)
        {
            Trace.Assert(_createDevice != null);

            _createDevice(this, ref createInfo, null, out device).CheckError();
        }

        public void EnumerateDeviceExtensionProperties(out uint count, out ExtensionProperties* properties)
        {
            Trace.Assert(_enumerateDeviceExtensionProperties != null);

            count = 0;
            _enumerateDeviceExtensionProperties(this, null, ref count, null).CheckError();
            properties = (ExtensionProperties*)Marshal.AllocHGlobal((int)count * sizeof(ExtensionProperties));
            _enumerateDeviceExtensionProperties(this, null, ref count, properties).CheckError();
        }

        /*public void GetSparseImageFormatProperties(Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, ImageCreateFlags flags, out uint count, out SparseImageFormatProperties* properties)
        {
            _gPDS(this, format, type, tiling, usage, flags, out imageFormatProperties).CheckError();
        }*/
        
    }
}