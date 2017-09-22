using System;
using System.Runtime.InteropServices;

namespace Annihilation.Vulkan
{
    public unsafe class Instance : IDisposable
    {
        private static DestroyInstanceDelegate _destroyInstance;
        private static EnumeratePhysicalDevicesDelegate _enumeratePhysicalDevices;
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

        private InstanceHandle _handle;

        public InstanceHandle Handle => _handle;
        public bool IsNull => _handle.Handle == IntPtr.Zero;

        public Instance(InstanceHandle handle)
        {
            _handle = handle;
        }

        public Instance(ref InstanceCreateInfo createInfo)
        {
            Vulkan.CreateInstance(ref createInfo, out _handle);
        }

        public void Destroy()
        {
            _destroyInstance = _destroyInstance ?? GetProcAddr<DestroyInstanceDelegate>(FunctionName.DestroyInstance);

            _destroyInstance(_handle, null);
            _handle = InstanceHandle.Null;
        }

        public void EnumeratePhysicalDevices(out PhysicalDevice[] physicalDevices)
        {
            _enumeratePhysicalDevices = _enumeratePhysicalDevices ?? GetProcAddr<EnumeratePhysicalDevicesDelegate>(FunctionName.EnumeratePhysicalDevices);

            uint count = 0;
            _enumeratePhysicalDevices(_handle, ref count, null).CheckError();
            PhysicalDeviceHandle* handles = (PhysicalDeviceHandle*)Marshal.AllocHGlobal((int)count * sizeof(PhysicalDeviceHandle));
            _enumeratePhysicalDevices(_handle, ref count, handles).CheckError();

            physicalDevices = new PhysicalDevice[count];
            for (int i = 0; i < count; ++i)
            {
                physicalDevices[i] = new PhysicalDevice(handles[i], this);
            }

            Marshal.FreeHGlobal(new IntPtr(handles));
        }

        public T GetProcAddr<T>(byte* functionName)
        {
            IntPtr func = Vulkan.GetInstanceProcAddr(_handle, functionName);
            if (func == IntPtr.Zero) throw new Exception("Could not load Vulkan function " + Annihilation.Utf8.ToString(functionName));
            return Marshal.GetDelegateForFunctionPointer<T>(func);
        }

        public void CreateAndroidSurface(ref AndroidSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createAndroidSurfaceKHR = _createAndroidSurfaceKHR ?? GetProcAddr<CreateAndroidSurfaceKHRDelegate>(FunctionName.CreateAndroidSurfaceKHR);

            _createAndroidSurfaceKHR(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateDisplayPlaneSurface(ref DisplaySurfaceCreateInfo createInfo, out Surface surface)
        {
            _createDisplayPlaneSurfaceKHR = _createDisplayPlaneSurfaceKHR ?? GetProcAddr<CreateDisplayPlaneSurfaceKHRDelegate>(FunctionName.CreateDisplayPlaneSurfaceKHR);

            _createDisplayPlaneSurfaceKHR(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateMirSurface(ref MirSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createMirSurfaceKHR = _createMirSurfaceKHR ?? GetProcAddr<CreateMirSurfaceKHRDelegate>(FunctionName.CreateMirSurfaceKHR);

            _createMirSurfaceKHR(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void DestroySurface(SurfaceHandle surfaceHandle)
        {
            _destroySurfaceKHR = _destroySurfaceKHR ?? GetProcAddr<DestroySurfaceKHRDelegate>(FunctionName.DestroySurfaceKHR);

            _destroySurfaceKHR(_handle, surfaceHandle, null);
        }

        public void CreateViSurface(ref ViSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createViSurfaceNN = _createViSurfaceNN ?? GetProcAddr<CreateViSurfaceNNDelegate>(FunctionName.CreateViSurfaceNN);

            _createViSurfaceNN(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateWaylandSurface(ref WaylandSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createWaylandSurfaceKHR = _createWaylandSurfaceKHR ?? GetProcAddr<CreateWaylandSurfaceKHRDelegate>(FunctionName.CreateWaylandSurfaceKHR);

            _createWaylandSurfaceKHR(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateWin32Surface(ref Win32SurfaceCreateInfo createInfo, out Surface surface)
        {
            _createWin32SurfaceKHR = _createWin32SurfaceKHR ?? GetProcAddr<CreateWin32SurfaceKHRDelegate>(FunctionName.CreateWin32SurfaceKHR);

            _createWin32SurfaceKHR(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateXlibSurface(ref XlibSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createXlibSurfaceKHR = _createXlibSurfaceKHR ?? GetProcAddr<CreateXlibSurfaceKHRDelegate>(FunctionName.CreateXlibSurfaceKHR);

            _createXlibSurfaceKHR(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }
        
        public void CreateXcbSurface(ref XcbSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createXcbSurfaceKHR = _createXcbSurfaceKHR ?? GetProcAddr<CreateXcbSurfaceKHRDelegate>(FunctionName.CreateXcbSurfaceKHR);

            _createXcbSurfaceKHR(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateDebugReportCallback(ref DebugReportCallbackCreateInfo createInfo, out DebugReportCallbackHandle debugReportCallback)
        {
            _createDebugReportCallbackEXT = _createDebugReportCallbackEXT ?? GetProcAddr<CreateDebugReportCallbackEXTDelegate>(FunctionName.CreateDebugReportCallbackEXT);

            _createDebugReportCallbackEXT(_handle, ref createInfo, null, out debugReportCallback).CheckError();
        }

        public void DestroyDebugReportCallback(DebugReportCallbackHandle handle)
        {
            _destroyDebugReportCallbackEXT = _destroyDebugReportCallbackEXT ?? GetProcAddr<DestroyDebugReportCallbackEXTDelegate>(FunctionName.DestroyDebugReportCallbackEXT);

            _destroyDebugReportCallbackEXT(_handle, handle, null);
        }

        public void EnumeratePhysicalDeviceGroups(PhysicalDeviceGroupProperties[] groupProperties)
        {
            _enumeratePhysicalDeviceGroupsKHX = _enumeratePhysicalDeviceGroupsKHX ?? GetProcAddr<EnumeratePhysicalDeviceGroupsKHXDelegate>(FunctionName.EnumeratePhysicalDeviceGroupsKHX);

            uint count = 0;
            _enumeratePhysicalDeviceGroupsKHX(_handle, ref count, null).CheckError();
            PhysicalDeviceGroupProperties* properties = (PhysicalDeviceGroupProperties*)Marshal.AllocHGlobal((int)count * sizeof(PhysicalDeviceGroupProperties));
            _enumeratePhysicalDeviceGroupsKHX(_handle, ref count, properties).CheckError();

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

            _createIOSSurfaceMVK(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        public void CreateMacOSSurface(ref MacOSSurfaceCreateInfo createInfo, out Surface surface)
        {
            _createMacOSSurfaceMVK = _createMacOSSurfaceMVK ?? GetProcAddr<CreateMacOSSurfaceMVKDelegate>(FunctionName.CreateMacOSSurfaceMVK);

            _createMacOSSurfaceMVK(_handle, ref createInfo, null, out SurfaceHandle handle).CheckError();

            surface = new Surface(handle, this);
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (IsNull) return;
            
            Destroy();
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
        #endregion
    }
}