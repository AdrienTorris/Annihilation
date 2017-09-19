using System;
using System.Runtime.InteropServices;

namespace Annihilation.Vulkan
{
    public unsafe struct Instance
    {
        private static DestroyInstanceDelegate _destroyInstance;
        private static EnumeratePhysicalDevicesDelegate _enumeratePhysicalDevices;
        private static GetInstanceProcAddrDelegate _getInstanceProcAddr;
        private static CreateAndroidSurfaceKHRDelegate _createAndroidSurfaceKHR;
        private static CreateDisplayPlaneSurfaceKHRDelegate _createDisplayPlaneSurfaceKHR;
        private static CreateMirSurfaceKHRDelegate _createMirSurfaceKHR;
        private static DestroySurfaceKHRDelegate _destroySurfaceKHR;
        private static CreateViSurfaceNNDelegate _createViSurfaceNN;
        private static CreateWaylandSurfaceKHRDelegate _createWaylandSurfaceKHR;
        private static CreateWin32SurfaceKHRDelegate _createWin32SurfaceKHR;
        private static CreateXlibSurfaceKHRDelegate _createXlibSurfaceKHR;
        private static CreateXcbSurfaceKHRDelegate _createXcbSurfaceKHR;
        private static CreateDebugReportCallbackEXTDelegate _createDebugReportCallbackEXT;
        private static DestroyDebugReportCallbackEXTDelegate _destroyDebugReportCallbackEXT;
        private static EnumeratePhysicalDeviceGroupsKHXDelegate _enumeratePhysicalDeviceGroupsKHX;
        private static CreateIOSSurfaceMVKDelegate _createIOSSurfaceMVK;
        private static CreateMacOSSurfaceMVKDelegate _createMacOSSurfaceMVK;

        public InstanceHandle Handle;

        public bool IsNull => Handle.Handle == IntPtr.Zero;

        public Instance(InstanceHandle handle)
        {
            Handle = handle;
        }

        public Instance(ref InstanceCreateInfo createInfo)
        {
            Vulkan.CreateInstance(ref createInfo, out this);
        }

        public void Destroy()
        {
            _destroyInstance = _destroyInstance ?? GetProcAddr<DestroyInstanceDelegate>(FunctionName.DestroyInstance);

            _destroyInstance(Handle, null);
            Handle = InstanceHandle.Null;
        }

        public void EnumeratePhysicalDevices(out PhysicalDevice[] physicalDevices)
        {
            _enumeratePhysicalDevices = _enumeratePhysicalDevices ?? GetProcAddr<EnumeratePhysicalDevicesDelegate>(FunctionName.EnumeratePhysicalDevices);

            uint count = 0;
            _enumeratePhysicalDevices(Handle, ref count, null).CheckError();
            PhysicalDeviceHandle* handles = (PhysicalDeviceHandle*)Marshal.AllocHGlobal((int)count * sizeof(PhysicalDeviceHandle));
            _enumeratePhysicalDevices(Handle, ref count, handles).CheckError();

            physicalDevices = new PhysicalDevice[count];
            for (int i = 0; i < count; ++i)
            {
                physicalDevices[i] = new PhysicalDevice(handles[i], this);
            }

            Marshal.FreeHGlobal(new IntPtr(handles));
        }

        public T GetProcAddr<T>(byte* functionName)
        {
            _getInstanceProcAddr = _getInstanceProcAddr ?? Vulkan.LoadGlobalFunction<GetInstanceProcAddrDelegate>(FunctionName.GetInstanceProcAddr);

            IntPtr func = _getInstanceProcAddr(Handle, functionName);
            if (func == IntPtr.Zero) throw new Exception("Could not load Vulkan function " + Annihilation.Utf8.ToString(functionName));
            return Marshal.GetDelegateForFunctionPointer<T>(func);
        }

        public void CreateAndroidSurface(ref AndroidSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createAndroidSurfaceKHR = _createAndroidSurfaceKHR ?? GetProcAddr<CreateAndroidSurfaceKHRDelegate>(FunctionName.CreateAndroidSurfaceKHR);

            _createAndroidSurfaceKHR(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateDisplayPlaneSurface(ref DisplaySurfaceCreateInfo createInfo, out Surface surface)
        {
            _createDisplayPlaneSurfaceKHR = _createDisplayPlaneSurfaceKHR ?? GetProcAddr<CreateDisplayPlaneSurfaceKHRDelegate>(FunctionName.CreateDisplayPlaneSurfaceKHR);

            _createDisplayPlaneSurfaceKHR(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateMirSurface(ref MirSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createMirSurfaceKHR = _createMirSurfaceKHR ?? GetProcAddr<CreateMirSurfaceKHRDelegate>(FunctionName.CreateMirSurfaceKHR);

            _createMirSurfaceKHR(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void DestroySurface(SurfaceHandle surfaceHandle)
        {
            _destroySurfaceKHR = _destroySurfaceKHR ?? GetProcAddr<DestroySurfaceKHRDelegate>(FunctionName.DestroySurfaceKHR);

            _destroySurfaceKHR(Handle, surfaceHandle, null);
        }

        public void CreateViSurface(ref ViSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createViSurfaceNN = _createViSurfaceNN ?? GetProcAddr<CreateViSurfaceNNDelegate>(FunctionName.CreateViSurfaceNN);

            _createViSurfaceNN(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateWaylandSurface(ref WaylandSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createWaylandSurfaceKHR = _createWaylandSurfaceKHR ?? GetProcAddr<CreateWaylandSurfaceKHRDelegate>(FunctionName.CreateWaylandSurfaceKHR);

            _createWaylandSurfaceKHR(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateWin32Surface(ref Win32SurfaceCreateInfo createInfo, out Surface surface)
        {
            _createWin32SurfaceKHR = _createWin32SurfaceKHR ?? GetProcAddr<CreateWin32SurfaceKHRDelegate>(FunctionName.CreateWin32SurfaceKHR);

            _createWin32SurfaceKHR(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateXlibSurface(ref XlibSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createXlibSurfaceKHR = _createXlibSurfaceKHR ?? GetProcAddr<CreateXlibSurfaceKHRDelegate>(FunctionName.CreateXlibSurfaceKHR);

            _createXlibSurfaceKHR(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }
        
        public void CreateXcbSurface(ref XcbSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createXcbSurfaceKHR = _createXcbSurfaceKHR ?? GetProcAddr<CreateXcbSurfaceKHRDelegate>(FunctionName.CreateXcbSurfaceKHR);

            _createXcbSurfaceKHR(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateDebugReportCallback(ref DebugReportCallbackCreateInfo createInfo, out DebugReportCallback debugReportCallback)
        {
            _createDebugReportCallbackEXT = _createDebugReportCallbackEXT ?? GetProcAddr<CreateDebugReportCallbackEXTDelegate>(FunctionName.CreateDebugReportCallbackEXT);

            _createDebugReportCallbackEXT(Handle, ref createInfo, null, out DebugReportCallbackHandle handle).CheckError();

            debugReportCallback = new DebugReportCallback(handle, this);
        }

        public void DestroyDebugReportCallback(DebugReportCallbackHandle handle)
        {
            _destroyDebugReportCallbackEXT = _destroyDebugReportCallbackEXT ?? GetProcAddr<DestroyDebugReportCallbackEXTDelegate>(FunctionName.DestroyDebugReportCallbackEXT);

            _destroyDebugReportCallbackEXT(Handle, handle, null);
        }

        public void EnumeratePhysicalDeviceGroups(PhysicalDeviceGroupProperties[] groupProperties)
        {
            _enumeratePhysicalDeviceGroupsKHX = _enumeratePhysicalDeviceGroupsKHX ?? GetProcAddr<EnumeratePhysicalDeviceGroupsKHXDelegate>(FunctionName.EnumeratePhysicalDeviceGroupsKHX);

            uint count = 0;
            _enumeratePhysicalDeviceGroupsKHX(Handle, ref count, null).CheckError();
            PhysicalDeviceGroupProperties* properties = (PhysicalDeviceGroupProperties*)Marshal.AllocHGlobal((int)count * sizeof(PhysicalDeviceGroupProperties));
            _enumeratePhysicalDeviceGroupsKHX(Handle, ref count, properties).CheckError();

            groupProperties = new PhysicalDeviceGroupProperties[count];
            for (int i = 0; i < count; ++i)
            {
                groupProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public void CreateIOSSurface(ref IOSSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createIOSSurfaceMVK = _createIOSSurfaceMVK ?? GetProcAddr<CreateIOSSurfaceMVKDelegate>(FunctionName.CreateIOSSurfaceMVK);

            _createIOSSurfaceMVK(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateMacOSSurface(ref MacOSSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createMacOSSurfaceMVK = _createMacOSSurfaceMVK ?? GetProcAddr<CreateMacOSSurfaceMVKDelegate>(FunctionName.CreateMacOSSurfaceMVK);

            _createMacOSSurfaceMVK(Handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }
    }
}