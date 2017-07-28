using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.Rendering.Vulkan
{
    public enum Format
    {
        Undefined = 0,
        R4G4UnormPack8 = 1,
        R4G4B4A4UnormPack16 = 2,
        B4G4R4A4UnormPack16 = 3,
        R5G6B5UnormPack16 = 4,
        B5G6R5UnormPack16 = 5,
        R5G5B5A1UnormPack16 = 6,
        B5G5R5A1UnormPack16 = 7,
        A1R5G5B5UnormPack16 = 8,
        R8Unorm = 9,
        R8SNorm = 10,
        R8UScaled = 11,
        R8SScaled = 12,
        R8UInt = 13,
        R8SInt = 14,
        R8SRGB = 15,
        R8G8Unorm = 16,
        R8G8SNorm = 17,
        R8G8UScaled = 18,
        R8G8SScaled = 19,
        R8G8UInt = 20,
        R8G8SInt = 21,
        R8G8SRGB = 22,
        R8G8B8Unorm = 23,
        R8G8B8SNorm = 24,
        R8G8B8UScaled = 25,
        R8G8B8SScaled = 26,
        R8G8B8UInt = 27,
        R8G8B8SInt = 28,
        R8G8B8SRGB = 29,
        B8G8R8Unorm = 30,
        B8G8R8SNorm = 31,
        B8G8R8UScaled = 32,
        B8G8R8SScaled = 33,
        B8G8R8UInt = 34,
        B8G8R8SInt = 35,
        B8G8R8SRGB = 36,
        R8G8B8A8Unorm = 37,
        R8G8B8A8SNorm = 38,
        R8G8B8A8UScaled = 39,
        R8G8B8A8SScaled = 40,
        R8G8B8A8UInt = 41,
        R8G8B8A8SInt = 42,
        R8G8B8A8SRGB = 43,
        B8G8R8A8Unorm = 44,
        B8G8R8A8SNorm = 45,
        B8G8R8A8UScaled = 46,
        B8G8R8A8SScaled = 47,
        B8G8R8A8UInt = 48,
        B8G8R8A8SInt = 49,
        B8G8R8A8SRGB = 50,
        A8B8G8R8UnormPack32 = 51,
        A8B8G8R8SNormPack32 = 52,
        A8B8G8R8UScaledPack32 = 53,
        A8B8G8R8SScaledPack32 = 54,
        A8B8G8R8UIntPack32 = 55,
        A8B8G8R8SIntPack32 = 56,
        A8B8G8R8SRGBPack32 = 57,
        A2R10G10B10UnormPack32 = 58,
        A2R10G10B10SNormPack32 = 59,
        A2R10G10B10UScaledPack32 = 60,
        A2R10G10B10SScaledPack32 = 61,
        A2R10G10B10UIntPack32 = 62,
        A2R10G10B10SIntPack32 = 63,
        A2B10G10R10UnormPack32 = 64,
        A2B10G10R10SNormPack32 = 65,
        A2B10G10R10UScaledPack32 = 66,
        A2B10G10R10SScaledPack32 = 67,
        A2B10G10R10UIntPack32 = 68,
        A2B10G10R10SIntPack32 = 69,
        R16Unorm = 70,
        R16SNorm = 71,
        R16UScaled = 72,
        R16SScaled = 73,
        R16UInt = 74,
        R16SInt = 75,
        R16SFloat = 76,
        R16G16Unorm = 77,
        R16G16SNorm = 78,
        R16G16UScaled = 79,
        R16G16SScaled = 80,
        R16G16UInt = 81,
        R16G16SInt = 82,
        R16G16SFloat = 83,
        R16G16B16Unorm = 84,
        R16G16B16SNorm = 85,
        R16G16B16UScaled = 86,
        R16G16B16SScaled = 87,
        R16G16B16UInt = 88,
        R16G16B16SInt = 89,
        R16G16B16SFloat = 90,
        R16G16B16A16Unorm = 91,
        R16G16B16A16SNorm = 92,
        R16G16B16A16UScaled = 93,
        R16G16B16A16SScaled = 94,
        R16G16B16A16UInt = 95,
        R16G16B16A16SInt = 96,
        R16G16B16A16SFloat = 97,
        R32UInt = 98,
        R32SInt = 99,
        R32SFloat = 100,
        R32G32UInt = 101,
        R32G32SInt = 102,
        R32G32SFloat = 103,
        R32G32B32UInt = 104,
        R32G32B32SInt = 105,
        R32G32B32SFloat = 106,
        R32G32B32A32UInt = 107,
        R32G32B32A32SInt = 108,
        R32G32B32A32SFloat = 109,
        R64UInt = 110,
        R64SInt = 111,
        R64SFloat = 112,
        R64G64UInt = 113,
        R64G64SInt = 114,
        R64G64SFloat = 115,
        R64G64B64UInt = 116,
        R64G64B64SInt = 117,
        R64G64B64SFloat = 118,
        R64G64B64A64UInt = 119,
        R64G64B64A64SInt = 120,
        R64G64B64A64SFloat = 121,
        B10G11R11UFloatPack32 = 122,
        E5B9G9R9UFloatPack32 = 123,
        D16Unorm = 124,
        X8D24UnormPack32 = 125,
        D32SFloat = 126,
        S8UInt = 127,
        D16UnormS8UInt = 128,
        D24UnormS8UInt = 129,
        D32SFloatS8UInt = 130,
        BC1RGBUnormBlock = 131,
        BC1RGBSRGBBlock = 132,
        BC1RGBAUnormBlock = 133,
        BC1RGBASRGBBlock = 134,
        BC2UnormBlock = 135,
        BC2SRGBBlock = 136,
        BC3UnormBlock = 137,
        BC3SRGBBlock = 138,
        BC4UnormBlock = 139,
        BC4SNormBlock = 140,
        BC5UnormBlock = 141,
        BC5SNormBlock = 142,
        BC6HUFloatBlock = 143,
        BC6HSFloatBlock = 144,
        BC7UnormBlock = 145,
        BC7SRGBBlock = 146,
        ETC2R8G8B8UnormBlock = 147,
        ETC2R8G8B8SRGBBlock = 148,
        ETC2R8G8B8A1UnormBlock = 149,
        ETC2R8G8B8A1SRGBBlock = 150,
        ETC2R8G8B8A8UnormBlock = 151,
        ETC2R8G8B8A8SRGBBlock = 152,
        EACR11UnormBlock = 153,
        EACR11SNormBlock = 154,
        EACR11G11UnormBlock = 155,
        EACR11G11SNormBlock = 156,
        ASTC4x4UnormBlock = 157,
        ASTC4x4SRGBBlock = 158,
        ASTC5x4UnormBlock = 159,
        ASTC5x4SRGBBlock = 160,
        ASTC5x5UnormBlock = 161,
        ASTC5x5SRGBBlock = 162,
        ASTC6x5UnormBlock = 163,
        ASTC6x5SRGBBlock = 164,
        ASTC6x6UnormBlock = 165,
        ASTC6x6SRGBBlock = 166,
        ASTC8x5UnormBlock = 167,
        ASTC8x5SRGBBlock = 168,
        ASTC8x6UnormBlock = 169,
        ASTC8x6SRGBBlock = 170,
        ASTC8x8UnormBlock = 171,
        ASTC8x8SRGBBlock = 172,
        ASTC10x5UnormBlock = 173,
        ASTC10x5SRGBBlock = 174,
        ASTC10x6UnormBlock = 175,
        ASTC10x6SRGBBlock = 176,
        ASTC10x8UnormBlock = 177,
        ASTC10x8SRGBBlock = 178,
        ASTC10x10UnormBlock = 179,
        ASTC10x10SRGBBlock = 180,
        ASTC12x10UnormBlock = 181,
        ASTC12x10SRGBBlock = 182,
        ASTC12x12UnormBlock = 183,
        ASTC12x12SRGBBlock = 184,
        PVRTC12BPPUnormBlockImg = 1000054000,
        PVRTC14BPPUnormBlockImg = 1000054001,
        PVRTC22BPPUnormBlockImg = 1000054002,
        PVRTC24BPPUnormBlockImg = 1000054003,
        PVRTC12BPPSRGBBlockImg = 1000054004,
        PVRTC14BPPSRGBBlockImg = 1000054005,
        PVRTC22BPPSRGBBlockImg = 1000054006,
        PVRTC24BPPSRGBBlockImg = 1000054007,
    }

    public enum ImageType
    {
        Image1D = 0,
        Image2D = 1,
        Image3D = 2,
    }

    public enum ImageTiling
    {
        Optimal = 0,
        Linear = 1,
    }

    public enum SharingMode
    {
        Exclusive = 0,
        Concurrent = 1,
    }

    public enum ImageLayout
    {
        Undefined = 0,
        General = 1,
        ColorAttachmentOptimal = 2,
        DepthStencilAttachmentOptimal = 3,
        DepthStencilReadOnlyOptimal = 4,
        ShaderReadOnlyOptimal = 5,
        TransferSrcOptimal = 6,
        TransferDstOptimal = 7,
        Preinitialized = 8,
        PresentSrc = 1000001002,
        SharedPresent = 1000111000,
    }

    public enum CompareOp
    {
        Never = 0,
        Less = 1,
        Equal = 2,
        LessOrEqual = 3,
        Greater = 4,
        NotEqual = 5,
        GreaterOrEqual = 6,
        Always = 7
    }

    [Flags]
    public enum SampleCountFlags : uint
    {
        Sample1 = 0x00000001,
        Sample2 = 0x00000002,
        Sample4 = 0x00000004,
        Sample8 = 0x00000008,
        Sample16 = 0x00000010,
        Sample32 = 0x00000020,
        Sample64 = 0x00000040
    }

    [Flags]
    public enum ImageCreateFlags : uint
    {
        SparseBinding = 0x00000001,
        SparseResidency = 0x00000002,
        SparseAliased = 0x00000004,
        MutableFormat = 0x00000008,
        CubeCompatible = 0x00000010,
        BindSfr = 0x00000040,
        Image2DArrayCompatible = 0x00000020,
    }

    [Flags]
    public enum ImageUsageFlags : uint
    {
        TransferSrc = 0x00000001,
        TransferDst = 0x00000002,
        Sampled = 0x00000004,
        Storage = 0x00000008,
        ColorAttachment = 0x00000010,
        DepthStencilAttachment = 0x00000020,
        TransientAttachment = 0x00000040,
        InputAttachment = 0x00000080,
    }

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
        // Constants
        internal const string LibraryName = "vulkan-1.dll";
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
        public static readonly EnumerateInstanceExtensionPropertiesDelegate EnumerateInstanceExtensionProperties;
        public static readonly EnumerateInstanceLayerPropertiesDelegate EnumerateInstanceLayerProperties;
        public static readonly CreateInstanceDelegate CreateInstance;

        // Instance functions
        public static readonly DestroyInstanceDelegate DestroyInstance;
        
        unsafe static Vulkan()
        {
            // Global functions
            EnumerateInstanceExtensionProperties = LoadGlobalFunction<EnumerateInstanceExtensionPropertiesDelegate>();
            EnumerateInstanceLayerProperties = LoadGlobalFunction<EnumerateInstanceLayerPropertiesDelegate>();
            CreateInstance = LoadGlobalFunction<CreateInstanceDelegate>();

            // Create instance

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