﻿using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace Vulkan
{
    
    public unsafe delegate void* GetInstanceProcAddrDelegate(
        IntPtr instance,
        byte* name
        );

    public unsafe delegate Result EnumerateInstanceExtensionPropertiesDelegate(
        byte* layerName,
        uint* propertyCount,
        ExtensionProperties* properties
        );

    public unsafe delegate Result EnumerateInstanceLayerPropertiesDelegate(
        uint* propertyCount,
        LayerProperties* properties 
        );

    public unsafe delegate Result CreateInstanceDelegate(
        InstanceCreateInfo* createInfo,
        AllocationCallbacks* allocator,
        Instance* instance
        );
    
    public unsafe delegate void DestroyInstanceDelegate(
        Instance instance,
        AllocationCallbacks* allocator
        );

    public unsafe delegate Result EnumeratePhysicalDevicesDelegate(
        Instance instance,
        uint* physicalDeviceCount,
        PhysicalDevice* physicalDevices
        );

    public unsafe delegate Result CreateDebugReportCallbackDelegate(
        Instance instance,
        IntPtr createInfo,
        AllocationCallbacks* allocator,
        IntPtr callback
        );

    public unsafe delegate void DestroyDebugReportCallbackDelegate(
        Instance instance,
        DebugReportCallbackDelegate callback,
        AllocationCallbacks* allocator
        );

    public unsafe delegate void DebugReportMessageDelegate(
        Instance instance,
        DebugReportFlags flags,
        DebugReportObjectType objectType,
        ulong objectHandle,
        ulong location,
        int messageCode,
        byte* layerPrefix,
        byte* message
        );

    public unsafe delegate Result CreateWin32SurfaceDelegate(
        Instance instance,
        Win32SurfaceCreateInfo* createInfo,
        AllocationCallbacks* allocator,
        Surface* surface
        );

    public unsafe delegate Result CreateDisplayPlaneSurfaceDelegate(
        Instance instance,
        DisplaySurfaceCreateInfo* createInfo,
        AllocationCallbacks* allocator,
        Surface* surface
        );

    [SuppressUnmanagedCodeSecurity]
    public static class Vulkan
    {
        //
        // Constants
        //
        public const string LibraryName = "vulkan-1.dll";
        public const float LodClampNone = 1000f;
        public const uint RemainingMipLevels = ~0U;
        public const uint RemainingArrayLayers = ~0U;
        public const ulong WholeSize = ~0UL;
        public const uint AttachmentUnused = ~0U;
        public const uint QueueFamilyIgnored = ~0U;
        public const uint SubpassExternal = ~0U;
        public const uint MaxPhysicalDeviceNameSize = 256;
        public const uint UuidSize = 16;
        public const uint MaxMemoryTypes = 32;
        public const uint MaxMemoryHeaps = 16;
        public const uint MaxExtensionNameSize = 256;
        public const uint MaxDescriptionSize = 256;
        
        // Global functions
        public static readonly EnumerateInstanceExtensionPropertiesDelegate EnumerateInstanceExtensionProperties = LoadGlobalFunction<EnumerateInstanceExtensionPropertiesDelegate>();
        public static readonly EnumerateInstanceLayerPropertiesDelegate EnumerateInstanceLayerProperties = LoadGlobalFunction<EnumerateInstanceLayerPropertiesDelegate>();
        public static readonly CreateInstanceDelegate CreateInstance = LoadGlobalFunction<CreateInstanceDelegate>();

        // Instance functions
        public static readonly DestroyInstanceDelegate DestroyInstance = LoadInstanceFunction<DestroyInstanceDelegate>(Windowing.Window);
        
        public static unsafe void Initialize()
        {
            // Instance functions
            DestroyInstance = LoadInstanceFunction<DestroyInstanceDelegate>();
        }

        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        private static extern unsafe IntPtr vkGetInstanceProcAddr(Instance instance, byte* name);

        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        private static extern unsafe IntPtr vkGetInstanceProcAddr(IntPtr instance, byte* name);

        public unsafe static T LoadGlobalFunction<T>()
        {
            IntPtr function = IntPtr.Zero;
            string name = typeof(T).Name;
            name = ("vk" + name).Replace("Delegate", string.Empty);
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            fixed (byte* namePtr = &nameBytes[0])
            {
                function = vkGetInstanceProcAddr(IntPtr.Zero, namePtr);
            }
            Assert.IsTrue(function != IntPtr.Zero, "Could not load function " + name);

            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

        public unsafe static T LoadInstanceFunction<T>(Instance instance)
        {
            IntPtr function = IntPtr.Zero;
            string name = typeof(T).Name;
            name = ("vk" + name).Replace("Delegate", string.Empty);
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            fixed (byte* namePtr = &nameBytes[0])
            {
                function = vkGetInstanceProcAddr(instance, namePtr);
            }
            Assert.IsTrue(function != IntPtr.Zero, "Could not load function " + name);

            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }
    }
}