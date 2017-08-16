using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace Vulkan
{

    [Flags]
    public enum BufferCreateFlags : uint
    {
        SparseBinding = 0x00000001,
        SparseResidency = 0x00000002,
        SparseAliased = 0x00000004
    }

    [Flags]
    public enum BufferUsageFlags : uint
    {
        TransferSrc = 0x00000001,
        TransferDst = 0x00000002,
        UniformTexelBuffer = 0x00000004,
        StorageTexelBuffer = 0x00000008,
        UniformBuffer = 0x00000010,
        StorageBuffer = 0x00000020,
        IndexBuffer = 0x00000040,
        VertexBuffer = 0x00000080,
        IndirectBuffer = 0x00000100
    }

    [Flags]
    public enum SurfaceTransformFlags : uint
    {
        Identity = 0x00000001,
        Rotate90 = 0x00000002,
        Rotate180 = 0x00000004,
        Rotate270 = 0x00000008,
        HorizontalMirror = 0x00000010,
        HorizontalMirrorRotate90 = 0x00000020,
        HorizontalMirrorRotate180 = 0x00000040,
        HorizontalMirrorRotate270 = 0x00000080,
        Inherit = 0x00000100,
    }

    [Flags]
    public enum CompositeAlphaFlags : uint
    {
        Opaque = 1 << 0,
        PreMultiplied = 1 << 1,
        PostMultiplied = 1 << 2,
        Inherit = 1 << 3
    }

    public unsafe struct ExtensionProperties
    {
        public fixed byte ExtensionName[(int)Vulkan.MaxExtensionNameSize];
        public uint SpecVersion;
    }

    public unsafe struct LayerProperties
    {
        public fixed byte LayerName[(int)Vulkan.MaxExtensionNameSize];
        public uint SpecVersion;
        public uint ImplementationVersion;
        public fixed byte Description[(int)Vulkan.MaxDescriptionSize];
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ClearColorValue
    {
        [FieldOffset(0)] public fixed float Float32[4];
        [FieldOffset(0)] public fixed int Int32[4];
        [FieldOffset(0)] public fixed uint Uint32[4];
    }

    public struct ClearDepthStencilValue
    {
        public float Depth;
        public uint Stencil;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ClearValue
    {
        [FieldOffset(0)] public ClearColorValue Color;
        [FieldOffset(0)] public ClearDepthStencilValue DepthStencil;
    }
    
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