using System;
using System.Runtime.InteropServices;

namespace Annihilation.Vulkan
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

        public void CreateDevice(ref DeviceCreateInfo createInfo, out DeviceHandle device)
        {
            _createDevice = _createDevice ?? Instance.GetProcAddr<CreateDeviceDelegate>(FunctionName.CreateDevice);

            _createDevice(Handle, ref createInfo, null, out device).CheckError();
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

        public void GetDisplayPlanePropertiesKHR(out DisplayPlaneProperties[] displayPlaneProperties)
        {
            _getPhysicalDeviceDisplayPlanePropertiesKHR = _getPhysicalDeviceDisplayPlanePropertiesKHR ?? Instance.GetProcAddr<GetPhysicalDeviceDisplayPlanePropertiesKHRDelegate>(FunctionName.GetPhysicalDeviceDisplayPlanePropertiesKHR);

            uint count = 0;
            _getPhysicalDeviceDisplayPlanePropertiesKHR(Handle, ref count, null).CheckError();
            DisplayPlaneProperties* properties = (DisplayPlaneProperties*)Marshal.AllocHGlobal((int)count * sizeof(DisplayPlaneProperties));
            _getPhysicalDeviceDisplayPlanePropertiesKHR(Handle, ref count, properties).CheckError();

            displayPlaneProperties = new DisplayPlaneProperties[count];
            for (int i = 0; i < count; ++i)
            {
                displayPlaneProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void GetDisplayPlaneSupportedDisplaysKHR(uint planeIndex, out Display[] displays)
        {
            _getDisplayPlaneSupportedDisplaysKHR = _getDisplayPlaneSupportedDisplaysKHR ?? Instance.GetProcAddr<GetDisplayPlaneSupportedDisplaysKHRDelegate>(FunctionName.GetDisplayPlaneSupportedDisplaysKHR);

            uint count = 0;
            _getDisplayPlaneSupportedDisplaysKHR(Handle, planeIndex, ref count, null).CheckError();
            DisplayHandle* disps = (DisplayHandle*)Marshal.AllocHGlobal((int)count * sizeof(DisplayHandle));
            _getDisplayPlaneSupportedDisplaysKHR(Handle, planeIndex, ref count, disps).CheckError();

            displays = new Display[count];
            for (int i = 0; i < count; ++i)
            {
                displays[i] = new Display(disps[i], this);
            }

            Marshal.FreeHGlobal(new IntPtr(disps));
        }

        public void GetDisplayModePropertiesKHR(DisplayHandle displayHandle, out DisplayModeProperties[] displayModeProperties)
        {
            _getDisplayModePropertiesKHR = _getDisplayModePropertiesKHR ?? Instance.GetProcAddr<GetDisplayModePropertiesKHRDelegate>(FunctionName.GetDisplayModePropertiesKHR);

            uint count = 0;
            _getDisplayModePropertiesKHR(Handle, displayHandle, ref count, null).CheckError();
            DisplayModeProperties* properties = (DisplayModeProperties*)Marshal.AllocHGlobal((int)count * sizeof(DisplayModeProperties));
            _getDisplayModePropertiesKHR(Handle, displayHandle, ref count, properties).CheckError();

            displayModeProperties = new DisplayModeProperties[count];
            for (int i = 0; i < count; ++i)
            {
                displayModeProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void CreateDisplayModeKHR(DisplayHandle displayHandle, ref DisplayModeCreateInfo createInfo, out DisplayModeHandle displayModeHandle)
        {
            _createDisplayModeKHR = _createDisplayModeKHR ?? Instance.GetProcAddr<CreateDisplayModeKHRDelegate>(FunctionName.CreateDisplayModeKHR);

            _createDisplayModeKHR(Handle, displayHandle, ref createInfo, null, out displayModeHandle).CheckError();
        }

        public void GetDisplayPlaneCapabilitiesKHR(DisplayModeHandle displayModeHandle, uint planeIndex, out DisplayPlaneCapabilities displayPlaneCapabilities)
        {
            _getDisplayPlaneCapabilitiesKHR = _getDisplayPlaneCapabilitiesKHR ?? Instance.GetProcAddr<GetDisplayPlaneCapabilitiesKHRDelegate>(FunctionName.GetDisplayPlaneCapabilitiesKHR);

            _getDisplayPlaneCapabilitiesKHR(Handle, displayModeHandle, planeIndex, out displayPlaneCapabilities).CheckError();
        }

        public bool GetMirPresentationSupportKHR(uint queueFamilyIndex, IntPtr connection)
        {
            _getPhysicalDeviceMirPresentationSupportKHR = _getPhysicalDeviceMirPresentationSupportKHR ?? Instance.GetProcAddr<GetPhysicalDeviceMirPresentationSupportKHRDelegate>(FunctionName.GetPhysicalDeviceMirPresentationSupportKHR);

            return _getPhysicalDeviceMirPresentationSupportKHR(Handle, queueFamilyIndex, connection);
        }

        public bool GetSurfaceSupportKHR(uint queueFamilyIndex, SurfaceHandle surfaceHandle)
        {
            _getPhysicalDeviceSurfaceSupportKHR = _getPhysicalDeviceSurfaceSupportKHR ?? Instance.GetProcAddr<GetPhysicalDeviceSurfaceSupportKHRDelegate>(FunctionName.GetPhysicalDeviceSurfaceSupportKHR);

            _getPhysicalDeviceSurfaceSupportKHR(Handle, queueFamilyIndex, surfaceHandle, out Bool32 supported).CheckError();
            return supported;
        }

        public void GetSurfaceCapabilitiesKHR(SurfaceHandle surfaceHandle, out SurfaceCapabilities surfaceCapabilities)
        {
            _getPhysicalDeviceSurfaceCapabilitiesKHR = _getPhysicalDeviceSurfaceCapabilitiesKHR ?? Instance.GetProcAddr<GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate>(FunctionName.GetPhysicalDeviceSurfaceCapabilitiesKHR);

            _getPhysicalDeviceSurfaceCapabilitiesKHR(Handle, surfaceHandle, out surfaceCapabilities).CheckError();
        }

        public void GetSurfaceFormatsKHR(SurfaceHandle surfaceHandle, out SurfaceFormat[] surfaceFormats)
        {
            _getPhysicalDeviceSurfaceFormatsKHR = _getPhysicalDeviceSurfaceFormatsKHR ?? Instance.GetProcAddr<GetPhysicalDeviceSurfaceFormatsKHRDelegate>(FunctionName.GetPhysicalDeviceSurfaceFormatsKHR);

            uint count = 0;
            _getPhysicalDeviceSurfaceFormatsKHR(Handle, surfaceHandle, ref count, null).CheckError();
            SurfaceFormat* formats = (SurfaceFormat*)Marshal.AllocHGlobal((int)count * sizeof(SurfaceFormat));
            _getPhysicalDeviceSurfaceFormatsKHR(Handle, surfaceHandle, ref count, formats).CheckError();

            surfaceFormats = new SurfaceFormat[count];
            for (int i = 0; i < count; ++i)
            {
                surfaceFormats[i] = formats[i];
            }

            Marshal.FreeHGlobal(new IntPtr(formats));
        }

        public void GetSurfacePresentModesKHR(SurfaceHandle surfaceHandle, out PresentMode[] presentModes)
        {
            _getPhysicalDeviceSurfacePresentModesKHR = _getPhysicalDeviceSurfacePresentModesKHR ?? Instance.GetProcAddr<GetPhysicalDeviceSurfacePresentModesKHRDelegate>(FunctionName.GetPhysicalDeviceSurfacePresentModesKHR);

            uint count = 0;
            _getPhysicalDeviceSurfacePresentModesKHR(Handle, surfaceHandle, ref count, null).CheckError();
            PresentMode* modes = (PresentMode*)Marshal.AllocHGlobal((int)count * sizeof(PresentMode));
            _getPhysicalDeviceSurfacePresentModesKHR(Handle, surfaceHandle, ref count, modes).CheckError();

            presentModes = new PresentMode[count];
            for (int i = 0; i < count; ++i)
            {
                presentModes[i] = modes[i];
            }

            Marshal.FreeHGlobal(new IntPtr(modes));
        }

        public bool GetWaylandPresentationSupportKHR(uint queueFamilyIndex, IntPtr display)
        {
            _getPhysicalDeviceWaylandPresentationSupportKHR = _getPhysicalDeviceWaylandPresentationSupportKHR ?? Instance.GetProcAddr<GetPhysicalDeviceWaylandPresentationSupportKHRDelegate>(FunctionName.GetPhysicalDeviceWaylandPresentationSupportKHR);

            return _getPhysicalDeviceWaylandPresentationSupportKHR(Handle, queueFamilyIndex, display);
        }

        public bool GetWin32PresentationSupportKHR(uint queueFamilyIndex)
        {
            _getPhysicalDeviceWin32PresentationSupportKHR = _getPhysicalDeviceWin32PresentationSupportKHR ?? Instance.GetProcAddr<GetPhysicalDeviceWin32PresentationSupportKHRDelegate>(FunctionName.GetPhysicalDeviceWin32PresentationSupportKHR);

            return _getPhysicalDeviceWin32PresentationSupportKHR(Handle, queueFamilyIndex);
        }

        public bool GetXlibPresentationSupport(uint queueFamilyIndex, IntPtr display, IntPtr visualID)
        {
            _getPhysicalDeviceXlibPresentationSupportKHR = _getPhysicalDeviceXlibPresentationSupportKHR ?? Instance.GetProcAddr<GetPhysicalDeviceXlibPresentationSupportKHRDelegate>(FunctionName.GetPhysicalDeviceXlibPresentationSupportKHR);

            return _getPhysicalDeviceXlibPresentationSupportKHR(Handle, queueFamilyIndex, display, visualID);
        }

        public bool GetXcbPresentationSupport(uint queueFamilyIndex, IntPtr connection, IntPtr visualID)
        {
            _getPhysicalDeviceXcbPresentationSupportKHR = _getPhysicalDeviceXcbPresentationSupportKHR ?? Instance.GetProcAddr<GetPhysicalDeviceXcbPresentationSupportKHRDelegate>(FunctionName.GetPhysicalDeviceXcbPresentationSupportKHR);

            return _getPhysicalDeviceXcbPresentationSupportKHR(Handle, queueFamilyIndex, connection, visualID);
        }

        public void GetExternalImageFormatPropertiesNV(Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, ExternalMemoryHandleTypeFlagsNV ExternalHandleType, out ExternalImageFormatPropertiesNV externalImageFormatProperties)
        {
            _getPhysicalDeviceExternalImageFormatPropertiesNV = _getPhysicalDeviceExternalImageFormatPropertiesNV ?? Instance.GetProcAddr<GetPhysicalDeviceExternalImageFormatPropertiesNVDelegate>(FunctionName.GetPhysicalDeviceExternalImageFormatPropertiesNV);

            _getPhysicalDeviceExternalImageFormatPropertiesNV(Handle, format, type, tiling, usage, flags, ExternalHandleType, out externalImageFormatProperties).CheckError();
        }

        public void GetGeneratedCommandsPropertiesNVX(out DeviceGeneratedCommandsFeatures deviceGeneratedCommandsFeatures, out DeviceGeneratedCommandsLimits deviceGeneratedCommandsLimits)
        {
            _getPhysicalDeviceGeneratedCommandsPropertiesNVX = _getPhysicalDeviceGeneratedCommandsPropertiesNVX ?? Instance.GetProcAddr<GetPhysicalDeviceGeneratedCommandsPropertiesNVXDelegate>(FunctionName.GetPhysicalDeviceGeneratedCommandsPropertiesNVX);

            _getPhysicalDeviceGeneratedCommandsPropertiesNVX(Handle, out deviceGeneratedCommandsFeatures, out deviceGeneratedCommandsLimits);
        }

        public void GetFeatures2KHR(out PhysicalDeviceFeatures2 physicalDeviceFeatures2)
        {
            _getPhysicalDeviceFeatures2KHR = _getPhysicalDeviceFeatures2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceFeatures2KHRDelegate>(FunctionName.GetPhysicalDeviceFeatures2KHR);

            _getPhysicalDeviceFeatures2KHR(Handle, out physicalDeviceFeatures2);
        }

        public void GetProperties2KHR(out PhysicalDeviceProperties2 physicalDeviceProperties2)
        {
            _getPhysicalDeviceProperties2KHR = _getPhysicalDeviceProperties2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceProperties2KHRDelegate>(FunctionName.GetPhysicalDeviceProperties2KHR);

            _getPhysicalDeviceProperties2KHR(Handle, out physicalDeviceProperties2);
        }

        public void GetFormatProperties2KHR(Format format, out FormatProperties2 formatProperties2)
        {
            _getPhysicalDeviceFormatProperties2KHR = _getPhysicalDeviceFormatProperties2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceFormatProperties2KHRDelegate>(FunctionName.GetPhysicalDeviceFormatProperties2KHR);

            _getPhysicalDeviceFormatProperties2KHR(Handle, format, out formatProperties2);
        }

        public void GetImageFormatProperties2KHR(ref PhysicalDeviceImageFormatInfo2 imageFormatInfo, out ImageFormatProperties2 imageFormatProperties)
        {
            _getPhysicalDeviceImageFormatProperties2KHR = _getPhysicalDeviceImageFormatProperties2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceImageFormatProperties2KHRDelegate>(FunctionName.GetPhysicalDeviceImageFormatProperties2KHR);

            _getPhysicalDeviceImageFormatProperties2KHR(Handle, ref imageFormatInfo, out imageFormatProperties).CheckError();
        }

        public void GetQueueFamilyProperties2KHR(out QueueFamilyProperties2[] queueFamilyProperties2)
        {
            _getPhysicalDeviceQueueFamilyProperties2KHR = _getPhysicalDeviceQueueFamilyProperties2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceQueueFamilyProperties2KHRDelegate>(FunctionName.GetPhysicalDeviceQueueFamilyProperties2KHR);

            uint count = 0;
            _getPhysicalDeviceQueueFamilyProperties2KHR(Handle, ref count, null);
            QueueFamilyProperties2* properties = (QueueFamilyProperties2*)Marshal.AllocHGlobal((int)count * sizeof(QueueFamilyProperties2));
            _getPhysicalDeviceQueueFamilyProperties2KHR(Handle, ref count, properties);

            queueFamilyProperties2 = new QueueFamilyProperties2[count];
            for (int i = 0; i < count; ++i)
            {
                queueFamilyProperties2[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void GetMemoryProperties2KHR(out PhysicalDeviceMemoryProperties2 physicalDeviceMemoryProperties2)
        {
            _getPhysicalDeviceMemoryProperties2KHR = _getPhysicalDeviceMemoryProperties2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceMemoryProperties2KHRDelegate>(FunctionName.GetPhysicalDeviceMemoryProperties2KHR);

            _getPhysicalDeviceMemoryProperties2KHR(Handle, out physicalDeviceMemoryProperties2);
        }

        public void GetSparseImageFormatProperties2KHR(ref PhysicalDeviceSparseImageFormatInfo2 physicalDeviceSparseImageFormatInfo2, out SparseImageFormatProperties2[] sparseImageFormatProperties2)
        {
            _getPhysicalDeviceSparseImageFormatProperties2KHR = _getPhysicalDeviceSparseImageFormatProperties2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceSparseImageFormatProperties2KHRDelegate>(FunctionName.GetPhysicalDeviceSparseImageFormatProperties2KHR);

            uint count = 0;
            _getPhysicalDeviceSparseImageFormatProperties2KHR(Handle, ref physicalDeviceSparseImageFormatInfo2, ref count, null);
            SparseImageFormatProperties2* properties = (SparseImageFormatProperties2*)Marshal.AllocHGlobal((int)count * sizeof(SparseImageFormatProperties2));
            _getPhysicalDeviceSparseImageFormatProperties2KHR(Handle, ref physicalDeviceSparseImageFormatInfo2, ref count, properties);

            sparseImageFormatProperties2 = new SparseImageFormatProperties2[count];
            for (int i = 0; i < count; ++i)
            {
                sparseImageFormatProperties2[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void GetExternalBufferPropertiesKHR(ref PhysicalDeviceExternalBufferInfo externalBufferInfo, out ExternalBufferProperties externalBufferProperties)
        {
            _getPhysicalDeviceExternalBufferPropertiesKHR = _getPhysicalDeviceExternalBufferPropertiesKHR ?? Instance.GetProcAddr<GetPhysicalDeviceExternalBufferPropertiesKHRDelegate>(FunctionName.GetPhysicalDeviceExternalBufferPropertiesKHR);

            _getPhysicalDeviceExternalBufferPropertiesKHR(Handle, ref externalBufferInfo, out externalBufferProperties);
        }

        public void GetExternalSemaphorePropertiesKHR(ref PhysicalDeviceExternalSemaphoreInfo externalSemaphoreInfo, out ExternalSemaphoreProperties externalSemaphoreProperties)
        {
            _getPhysicalDeviceExternalSemaphorePropertiesKHR = _getPhysicalDeviceExternalSemaphorePropertiesKHR ?? Instance.GetProcAddr<GetPhysicalDeviceExternalSemaphorePropertiesKHRDelegate>(FunctionName.GetPhysicalDeviceExternalSemaphorePropertiesKHR);

            _getPhysicalDeviceExternalSemaphorePropertiesKHR(Handle, ref externalSemaphoreInfo, out externalSemaphoreProperties);
        }

        public void GetExternalFencePropertiesKHR(ref PhysicalDeviceExternalFenceInfo externalFenceInfo, out ExternalFenceProperties externalFenceProperties)
        {
            _getPhysicalDeviceExternalFencePropertiesKHR = _getPhysicalDeviceExternalFencePropertiesKHR ?? Instance.GetProcAddr<GetPhysicalDeviceExternalFencePropertiesKHRDelegate>(FunctionName.GetPhysicalDeviceExternalFencePropertiesKHR);

            _getPhysicalDeviceExternalFencePropertiesKHR(Handle, ref externalFenceInfo, out externalFenceProperties);
        }

        public void ReleaseDisplayEXT(DisplayHandle displayHandle)
        {
            _releaseDisplayEXT = _releaseDisplayEXT ?? Instance.GetProcAddr<ReleaseDisplayEXTDelegate>(FunctionName.ReleaseDisplayEXT);

            _releaseDisplayEXT(Handle, displayHandle).CheckError();
        }

        public void AcquireXlibDisplayEXT(IntPtr dpy, DisplayHandle display)
        {
            _acquireXlibDisplayEXT = _acquireXlibDisplayEXT ?? Instance.GetProcAddr<AcquireXlibDisplayEXTDelegate>(FunctionName.AcquireXlibDisplayEXT);

            _acquireXlibDisplayEXT(Handle, dpy, display).CheckError();
        }

        public void GetRandROutputDisplayEXT(IntPtr dpy, IntPtr rrOutput, out DisplayHandle displayHandle)
        {
            _getRandROutputDisplayEXT = _getRandROutputDisplayEXT ?? Instance.GetProcAddr<GetRandROutputDisplayEXTDelegate>(FunctionName.GetRandROutputDisplayEXT);

            _getRandROutputDisplayEXT(Handle, dpy, rrOutput, out displayHandle).CheckError();
        }
        
        public void GetSurfaceCapabilities2EXT(SurfaceHandle surfaceHandle, out SurfaceCapabilities2EXT surfaceCapabilities2EXT)
        {
            _getPhysicalDeviceSurfaceCapabilities2EXT = _getPhysicalDeviceSurfaceCapabilities2EXT ?? Instance.GetProcAddr<GetPhysicalDeviceSurfaceCapabilities2EXTDelegate>(FunctionName.GetPhysicalDeviceSurfaceCapabilities2EXT);

            _getPhysicalDeviceSurfaceCapabilities2EXT(Handle, surfaceHandle, out surfaceCapabilities2EXT).CheckError();
        }

        public void GetPresentRectanglesKHX(SurfaceHandle surfaceHandle, out Rect2D[] rects)
        {
            _getPhysicalDevicePresentRectanglesKHX = _getPhysicalDevicePresentRectanglesKHX ?? Instance.GetProcAddr<GetPhysicalDevicePresentRectanglesKHXDelegate>(FunctionName.GetPhysicalDevicePresentRectanglesKHX);

            uint count = 0;
            _getPhysicalDevicePresentRectanglesKHX(Handle, surfaceHandle, ref count, null).CheckError();
            Rect2D* rcts = (Rect2D*)Marshal.AllocHGlobal((int)count * sizeof(Rect2D));
            _getPhysicalDevicePresentRectanglesKHX(Handle, surfaceHandle, ref count, rcts).CheckError();

            rects = new Rect2D[count];
            for (int i = 0; i < count; ++i)
            {
                rects[i] = rcts[i];
            }

            Marshal.FreeHGlobal(new IntPtr(rcts));
        }

        public void GetSurfaceCapabilities2KHR(ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out SurfaceCapabilities2 surfaceCapabilities)
        {
            _getPhysicalDeviceSurfaceCapabilities2KHR = _getPhysicalDeviceSurfaceCapabilities2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceSurfaceCapabilities2KHRDelegate>(FunctionName.GetPhysicalDeviceSurfaceCapabilities2KHR);

            _getPhysicalDeviceSurfaceCapabilities2KHR(Handle, ref surfaceInfo, out surfaceCapabilities).CheckError();
        }

        public void GetSurfaceFormats2KHR(ref PhysicalDeviceSurfaceInfo2 physicalDeviceSurfaceInfo2, out SurfaceFormat2[] surfaceFormats)
        {
            _getPhysicalDeviceSurfaceFormats2KHR = _getPhysicalDeviceSurfaceFormats2KHR ?? Instance.GetProcAddr<GetPhysicalDeviceSurfaceFormats2KHRDelegate>(FunctionName.GetPhysicalDeviceSurfaceFormats2KHR);

            uint count = 0;
            _getPhysicalDeviceSurfaceFormats2KHR(Handle, ref physicalDeviceSurfaceInfo2, ref count, null).CheckError();
            SurfaceFormat2* formats = (SurfaceFormat2*)Marshal.AllocHGlobal((int)count * sizeof(SurfaceFormat2));
            _getPhysicalDeviceSurfaceFormats2KHR(Handle, ref physicalDeviceSurfaceInfo2, ref count, formats).CheckError();

            surfaceFormats = new SurfaceFormat2[count];
            for (int i = 0; i < count; ++i)
            {
                surfaceFormats[i] = formats[i];
            }

            Marshal.FreeHGlobal(new IntPtr(formats));
        }
    }
}