using System;

namespace Vulkan
{
    public enum PipelineCacheHeaderVersion
    {
        One = 1
    }

    public enum Result : int
    {
        Success = 0,
        NotReady = 1,
        Timeout = 2,
        EventSet = 3,
        EventReset = 4,
        Incomplete = 5,
        ErrorOutOfHostMemory = -1,
        ErrorOutOfDeviceMemory = -2,
        ErrorInitializationFailed = -3,
        ErrorDeviceLost = -4,
        ErrorMemoryMapFailed = -5,
        ErrorLayerNotPresent = -6,
        ErrorExtensionNotPresent = -7,
        ErrorFeatureNotPresent = -8,
        ErrorIncompatibleDriver = -9,
        ErrorTooManyObjects = -10,
        ErrorFormatNotSupported = -11,
        ErrorFragmentedPool = -12,
        ErrorSurfaceLost = -1000000000,
        ErrorNativeWindowInUse = -1000000001,
        Suboptimal = 1000001003,
        ErrorOutOfDate = -1000001004,
        ErrorIncompatibleDisplay = -1000003001,
        ErrorValidationFailed = -1000011001,
        ErrorInvalidShader = -1000012000,
        ErrorOutOfPoolMemory = -1000069000,
        ErrorInvalidExternalHandle = -1000072003,
    }

    public enum StructureType
    {
        ApplicationInfo = 0,
        InstanceCreateInfo = 1,
        DeviceQueueCreateInfo = 2,
        DeviceCreateInfo = 3,
        SubmitInfo = 4,
        MemoryAllocateInfo = 5,
        MappedMemoryRange = 6,
        BindSparseInfo = 7,
        FenceCreateInfo = 8,
        SemaphoreCreateInfo = 9,
        EventCreateInfo = 10,
        QueryPoolCreateInfo = 11,
        BufferCreateInfo = 12,
        BufferViewCreateInfo = 13,
        ImageCreateInfo = 14,
        ImageViewCreateInfo = 15,
        ShaderModuleCreateInfo = 16,
        PipelineCacheCreateInfo = 17,
        PipelineShaderStageCreateInfo = 18,
        PipelineVertexInputStateCreateInfo = 19,
        PipelineInputAssemblyStateCreateInfo = 20,
        PipelineTessellationStateCreateInfo = 21,
        PipelineViewportStateCreateInfo = 22,
        PipelineRasterizationStateCreateInfo = 23,
        PipelineMultisampleStateCreateInfo = 24,
        PipelineDepthStencilStateCreateInfo = 25,
        PipelineColorBlendStateCreateInfo = 26,
        PipelineDynamicStateCreateInfo = 27,
        GraphicsPipelineCreateInfo = 28,
        ComputePipelineCreateInfo = 29,
        PipelineLayoutCreateInfo = 30,
        SamplerCreateInfo = 31,
        DescriptorSetLayoutCreateInfo = 32,
        DescriptorPoolCreateInfo = 33,
        DescriptorSetAllocateInfo = 34,
        WriteDescriptorSet = 35,
        CopyDescriptorSet = 36,
        FramebufferCreateInfo = 37,
        RenderPassCreateInfo = 38,
        CommandPoolCreateInfo = 39,
        CommandBufferAllocateInfo = 40,
        CommandBufferInheritanceInfo = 41,
        CommandBufferBeginInfo = 42,
        RenderPassBeginInfo = 43,
        BufferMemoryBarrier = 44,
        ImageMemoryBarrier = 45,
        MemoryBarrier = 46,
        LoaderInstanceCreateInfo = 47,
        LoaderDeviceCreateInfo = 48,
        SwapchainCreateInfo = 1000001000,
        PresentInfo = 1000001001,
        DisplayModeCreateInfo = 1000002000,
        DisplaySurfaceCreateInfo = 1000002001,
        DisplayPresentInfo = 1000003000,
        XlibSurfaceCreateInfo = 1000004000,
        XcbSurfaceCreateInfo = 1000005000,
        WaylandSurfaceCreateInfo = 1000006000,
        MirSurfaceCreateInfo = 1000007000,
        AndroidSurfaceCreateInfo = 1000008000,
        Win32SurfaceCreateInfo = 1000009000,
        DebugReportCallbackCreateInfo = 1000011000,
        PipelineRasterizationStateRasterizationOrder = 1000018000,
        DebugMarkerObjectNameInfo = 1000022000,
        DebugMarkerObjectTagInfo = 1000022001,
        DebugMarkerMarkerInfo = 1000022002,
        DedicatedAllocationImageCreateInfo = 1000026000,
        DedicatedAllocationBufferCreateInfo = 1000026001,
        DedicatedAllocationMemoryAllocateInfo = 1000026002,
        TextureLodGatherFormatProperties = 1000041000,
        RenderPassMultiviewCreateInfo = 1000053000,
        PhysicalDeviceMultiviewFeatures = 1000053001,
        PhysicalDeviceMultiviewProperties = 1000053002,
        ExternalMemoryImageCreateInfoNv = 1000056000,
        ExportMemoryAllocateInfoNv = 1000056001,
        ImportMemoryWin32HandleInfoNv = 1000057000,
        ExportMemoryWin32HandleInfoNv = 1000057001,
        Win32KeyedMutexAcquireReleaseInfoNv = 1000058000,
        PhysicalDeviceFeatures2 = 1000059000,
        PhysicalDeviceProperties2 = 1000059001,
        FormatProperties2 = 1000059002,
        ImageFormatProperties2 = 1000059003,
        PhysicalDeviceImageFormatInfo2 = 1000059004,
        QueueFamilyProperties2 = 1000059005,
        PhysicalDeviceMemoryProperties2 = 1000059006,
        SparseImageFormatProperties2 = 1000059007,
        PhysicalDeviceSparseImageFormatInfo2 = 1000059008,
        MemoryAllocateFlagsInfo = 1000060000,
        BindBufferMemoryInfo = 1000060001,
        BindImageMemoryInfo = 1000060002,
        DeviceGroupRenderPassBeginInfo = 1000060003,
        DeviceGroupCommandBufferBeginInfo = 1000060004,
        DeviceGroupSubmitInfo = 1000060005,
        DeviceGroupBindSparseInfo = 1000060006,
        DeviceGroupPresentCapabilities = 1000060007,
        ImageSwapchainCreateInfo = 1000060008,
        BindImageMemorySwapchainInfo = 1000060009,
        AcquireNextImageInfo = 1000060010,
        DeviceGroupPresentInfo = 1000060011,
        DeviceGroupSwapchainCreateInfo = 1000060012,
        ValidationFlags = 1000061000,
        ViSurfaceCreateInfoNn = 1000062000,
        PhysicalDeviceGroupProperties = 1000070000,
        DeviceGroupDeviceCreateInfo = 1000070001,
        PhysicalDeviceExternalImageFormatInfo = 1000071000,
        ExternalImageFormatProperties = 1000071001,
        PhysicalDeviceExternalBufferInfo = 1000071002,
        ExternalBufferProperties = 1000071003,
        PhysicalDeviceIdProperties = 1000071004,
        ExternalMemoryBufferCreateInfo = 1000072000,
        ExternalMemoryImageCreateInfo = 1000072001,
        ExportMemoryAllocateInfo = 1000072002,
        ImportMemoryWin32HandleInfo = 1000073000,
        ExportMemoryWin32HandleInfo = 1000073001,
        MemoryWin32HandleProperties = 1000073002,
        MemoryGetWin32HandleInfo = 1000073003,
        ImportMemoryFdInfo = 1000074000,
        MemoryFdProperties = 1000074001,
        MemoryGetFdInfo = 1000074002,
        Win32KeyedMutexAcquireReleaseInfo = 1000075000,
        PhysicalDeviceExternalSemaphoreInfo = 1000076000,
        ExternalSemaphoreProperties = 1000076001,
        ExportSemaphoreCreateInfo = 1000077000,
        ImportSemaphoreWin32HandleInfo = 1000078000,
        ExportSemaphoreWin32HandleInfo = 1000078001,
        D3d12FenceSubmitInfo = 1000078002,
        SemaphoreGetWin32HandleInfo = 1000078003,
        ImportSemaphoreFdInfo = 1000079000,
        SemaphoreGetFdInfo = 1000079001,
        PhysicalDevicePushDescriptorProperties = 1000080000,
        PhysicalDevice16bitStorageFeatures = 1000083000,
        PresentRegions = 1000084000,
        DescriptorUpdateTemplateCreateInfo = 1000085000,
        ObjectTableCreateInfo = 1000086000,
        IndirectCommandsLayoutCreateInfo = 1000086001,
        CommandProcessCommandsInfo = 1000086002,
        CommandReserveSpaceForCommandsInfo = 1000086003,
        DeviceGeneratedCommandsLimits = 1000086004,
        DeviceGeneratedCommandsFeatures = 1000086005,
        PipelineViewportWScalingStateCreateInfo = 1000087000,
        SurfaceCapabilities2Ext = 1000090000,
        DisplayPowerInfo = 1000091000,
        DeviceEventInfo = 1000091001,
        DisplayEventInfo = 1000091002,
        SwapchainCounterCreateInfo = 1000091003,
        PresentTimesInfoGoogle = 1000092000,
        PhysicalDeviceMultiviewPerViewAttributesPropertiesNvx = 1000097000,
        PipelineViewportSwizzleStateCreateInfoNv = 1000098000,
        PhysicalDeviceDiscardRectangleProperties = 1000099000,
        PipelineDiscardRectangleStateCreateInfo = 1000099001,
        HdrMetadata = 1000105000,
        SharedPresentSurfaceCapabilities = 1000111000,
        PhysicalDeviceernalFenceInfo = 1000112000,
        ExternalFenceProperties = 1000112001,
        ExportFenceCreateInfo = 1000113000,
        ImportFenceWin32HandleInfo = 1000114000,
        ExportFenceWin32HandleInfo = 1000114001,
        FenceGetWin32HandleInfo = 1000114002,
        ImportFenceFdInfo = 1000115000,
        FenceGetFdInfo = 1000115001,
        PhysicalDeviceSurfaceInfo2 = 1000119000,
        SurfaceCapabilities2 = 1000119001,
        SurfaceFormat2 = 1000119002,
        PhysicalDeviceVariablePointerFeatures = 1000120000,
        IosSurfaceCreateInfoMvk = 1000122000,
        MacosSurfaceCreateInfoMvk = 1000123000,
        MemoryDedicatedRequirements = 1000127000,
        MemoryDedicatedAllocateInfo = 1000127001,
        PhysicalDeviceSamplerFilterMinmaxProperties = 1000130000,
        SamplerReductionModeCreateInfo = 1000130001,
        BufferMemoryRequirementsInfo2 = 1000146000,
        ImageMemoryRequirementsInfo2 = 1000146001,
        ImageSparseMemoryRequirementsInfo2 = 1000146002,
        MemoryRequirements2 = 1000146003,
        SparseImageMemoryRequirements2 = 1000146004,
        PhysicalDeviceBlendOperationAdvancedFeatures = 1000148000,
        PhysicalDeviceBlendOperationAdvancedProperties = 1000148001,
        PipelineColorBlendAdvancedStateCreateInfo = 1000148002,
        PipelineCoverageToColorStateCreateInfoNv = 1000149000,
        PipelineCoverageModulationStateCreateInfoNv = 1000152000,
    }

    public enum SystemAllocationScope
    {
        Command = 0,
        Object = 1,
        Cache = 2,
        Device = 3,
        Instance = 4
    }

    public enum InternalAllocationType
    {
        Executable = 0
    }

    public enum Format
    {
        Undefined = 0,
        R4G4UNormPack8 = 1,
        R4G4B4A4UNormPack16 = 2,
        B4G4R4A4UNormPack16 = 3,
        R5G6B5UNormPack16 = 4,
        B5G6R5UNormPack16 = 5,
        R5G5B5A1UNormPack16 = 6,
        B5G5R5A1UNormPack16 = 7,
        A1R5G5B5UNormPack16 = 8,
        R8UNorm = 9,
        R8SNorm = 10,
        R8UScaled = 11,
        R8SScaled = 12,
        R8UInt = 13,
        R8SInt = 14,
        R8SRGB = 15,
        R8G8UNorm = 16,
        R8G8SNorm = 17,
        R8G8UScaled = 18,
        R8G8SScaled = 19,
        R8G8UInt = 20,
        R8G8SInt = 21,
        R8G8SRGB = 22,
        R8G8B8UNorm = 23,
        R8G8B8SNorm = 24,
        R8G8B8UScaled = 25,
        R8G8B8SScaled = 26,
        R8G8B8UInt = 27,
        R8G8B8SInt = 28,
        R8G8B8SRGB = 29,
        B8G8R8UNorm = 30,
        B8G8R8SNorm = 31,
        B8G8R8UScaled = 32,
        B8G8R8SScaled = 33,
        B8G8R8UInt = 34,
        B8G8R8SInt = 35,
        B8G8R8SRGB = 36,
        R8G8B8A8UNorm = 37,
        R8G8B8A8SNorm = 38,
        R8G8B8A8UScaled = 39,
        R8G8B8A8SScaled = 40,
        R8G8B8A8UInt = 41,
        R8G8B8A8SInt = 42,
        R8G8B8A8SRGB = 43,
        B8G8R8A8UNorm = 44,
        B8G8R8A8SNorm = 45,
        B8G8R8A8UScaled = 46,
        B8G8R8A8SScaled = 47,
        B8G8R8A8UInt = 48,
        B8G8R8A8SInt = 49,
        B8G8R8A8SRGB = 50,
        A8B8G8R8UNormPack32 = 51,
        A8B8G8R8SNormPack32 = 52,
        A8B8G8R8UScaledPack32 = 53,
        A8B8G8R8SScaledPack32 = 54,
        A8B8G8R8UIntPack32 = 55,
        A8B8G8R8SIntPack32 = 56,
        A8B8G8R8SRGBPack32 = 57,
        A2R10G10B10UNormPack32 = 58,
        A2R10G10B10SNormPack32 = 59,
        A2R10G10B10UScaledPack32 = 60,
        A2R10G10B10SScaledPack32 = 61,
        A2R10G10B10UIntPack32 = 62,
        A2R10G10B10SIntPack32 = 63,
        A2B10G10R10UNormPack32 = 64,
        A2B10G10R10SNormPack32 = 65,
        A2B10G10R10UScaledPack32 = 66,
        A2B10G10R10SScaledPack32 = 67,
        A2B10G10R10UIntPack32 = 68,
        A2B10G10R10SIntPack32 = 69,
        R16UNorm = 70,
        R16SNorm = 71,
        R16UScaled = 72,
        R16SScaled = 73,
        R16UInt = 74,
        R16SInt = 75,
        R16SFloat = 76,
        R16G16UNorm = 77,
        R16G16SNorm = 78,
        R16G16UScaled = 79,
        R16G16SScaled = 80,
        R16G16UInt = 81,
        R16G16SInt = 82,
        R16G16SFloat = 83,
        R16G16B16UNorm = 84,
        R16G16B16SNorm = 85,
        R16G16B16UScaled = 86,
        R16G16B16SScaled = 87,
        R16G16B16UInt = 88,
        R16G16B16SInt = 89,
        R16G16B16SFloat = 90,
        R16G16B16A16UNorm = 91,
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
        D16UNorm = 124,
        X8D24UNormPack32 = 125,
        D32SFloat = 126,
        S8UInt = 127,
        D16UNormS8UInt = 128,
        D24UNormS8UInt = 129,
        D32SFloatS8UInt = 130,
        BC1RGBUNormBlock = 131,
        BC1RGBSRGBBlock = 132,
        BC1RGBAUNormBlock = 133,
        BC1RGBASRGBBlock = 134,
        BC2UNormBlock = 135,
        BC2SRGBBlock = 136,
        BC3UNormBlock = 137,
        BC3SRGBBlock = 138,
        BC4UNormBlock = 139,
        BC4SNormBlock = 140,
        BC5UNormBlock = 141,
        BC5SNormBlock = 142,
        BC6HUFloatBlock = 143,
        BC6HSFloatBlock = 144,
        BC7UNormBlock = 145,
        BC7SRGBBlock = 146,
        ETC2R8G8B8UNormBlock = 147,
        ETC2R8G8B8SRGBBlock = 148,
        ETC2R8G8B8A1UNormBlock = 149,
        ETC2R8G8B8A1SRGBBlock = 150,
        ETC2R8G8B8A8UNormBlock = 151,
        ETC2R8G8B8A8SRGBBlock = 152,
        EACR11UNormBlock = 153,
        EACR11SNormBlock = 154,
        EACR11G11UNormBlock = 155,
        EACR11G11SNormBlock = 156,
        ASTC4x4UNormBlock = 157,
        ASTC4x4SRGBBlock = 158,
        ASTC5x4UNormBlock = 159,
        ASTC5x4SRGBBlock = 160,
        ASTC5x5UNormBlock = 161,
        ASTC5x5SRGBBlock = 162,
        ASTC6x5UNormBlock = 163,
        ASTC6x5SRGBBlock = 164,
        ASTC6x6UNormBlock = 165,
        ASTC6x6SRGBBlock = 166,
        ASTC8x5UNormBlock = 167,
        ASTC8x5SRGBBlock = 168,
        ASTC8x6UNormBlock = 169,
        ASTC8x6SRGBBlock = 170,
        ASTC8x8UNormBlock = 171,
        ASTC8x8SRGBBlock = 172,
        ASTC10x5UNormBlock = 173,
        ASTC10x5SRGBBlock = 174,
        ASTC10x6UNormBlock = 175,
        ASTC10x6SRGBBlock = 176,
        ASTC10x8UNormBlock = 177,
        ASTC10x8SRGBBlock = 178,
        ASTC10x10UNormBlock = 179,
        ASTC10x10SRGBBlock = 180,
        ASTC12x10UNormBlock = 181,
        ASTC12x10SRGBBlock = 182,
        ASTC12x12UNormBlock = 183,
        ASTC12x12SRGBBlock = 184,
        PVRTC12BPPUNormBlockImg = 1000054000,
        PVRTC14BPPUNormBlockImg = 1000054001,
        PVRTC22BPPUNormBlockImg = 1000054002,
        PVRTC24BPPUNormBlockImg = 1000054003,
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

    public enum PhysicalDeviceType
    {
        Other = 0,
        IntegratedGPU = 1,
        DiscreteGPU = 2,
        VirtualGPU = 3,
        CPU = 4
    }

    public enum QueryType
    {
        Occlusion = 0,
        PipelineStatistics = 1,
        Timestamp = 2
    }

    public enum SharingMode
    {
        Exclusive = 0,
        Concurrent = 1
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

    public enum ImageViewType
    {
        Type1D = 0,
        Type2D = 1,
        Type3D = 2,
        TypeCube = 3,
        Type1DArray = 4,
        Type2DArray = 5,
        TypeCubeArray = 6
    }

    public enum ComponentSwizzle
    {
        Identity = 0,
        Zero = 1,
        One = 2,
        R = 3,
        G = 4,
        B = 5,
        A = 6
    }

    public enum VertexInputRate
    {
        Vertex = 0,
        Instance = 1
    }

    public enum PrimitiveTopology
    {
        PointList = 0,
        LineList = 1,
        LineStrip = 2,
        TriangleList = 3,
        TriangleStrip = 4,
        TriangleFan = 5,
        LineListWithAdjacency = 6,
        LineStripWithAdjacency = 7,
        TriangleListWithAdjacency = 8,
        TriangleStripWithAdjacency = 9,
        PatchList = 10
    }

    public enum PolygonMode
    {
        Fill = 0,
        Line = 1,
        Point = 2,
        FillTriangle = 1000153000
    }

    public enum FrontFace
    {
        CounterClockwise = 0,
        Clockwise = 1
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

    public enum StencilOp
    {
        Keep = 0,
        Zero = 1,
        Replace = 2,
        IncrementAndClamp = 3,
        DecrementAndClamp = 4,
        Invert = 5,
        IncrementAndWrap = 6,
        DecrementAndWrap = 7,
    }

    public enum LogicOp
    {
        Clear = 0,
        And = 1,
        AndReverse = 2,
        Copy = 3,
        AndInverted = 4,
        NoOp = 5,
        Xor = 6,
        Or = 7,
        Nor = 8,
        Equivalent = 9,
        Invert = 10,
        OrReverse = 11,
        CopyInverted = 12,
        OrInverted = 13,
        Nand = 14,
        Set = 15,
    }

    public enum BlendFactor
    {
        Zero = 0,
        One = 1,
        SrcColor = 2,
        OneMinusSrcColor = 3,
        DstColor = 4,
        OneMinusDstColor = 5,
        SrcAlpha = 6,
        OneMinusSrcAlpha = 7,
        DstAlpha = 8,
        OneMinusDstAlpha = 9,
        ConstantColor = 10,
        OneMinusConstantColor = 11,
        ConstantAlpha = 12,
        OneMinusConstantAlpha = 13,
        SrcAlphaSaturate = 14,
        Src1Color = 15,
        OneMinusSrc1Color = 16,
        Src1Alpha = 17,
        OneMinusSrc1Alpha = 18,
    }

    public enum BlendOp
    {
        Add = 0,
        Subtact = 1,
        ReverseSubtract = 2,
        Min = 3,
        Max = 4,
        Zero = 1000148000,
        Src = 1000148001,
        Dst = 1000148002,
        SrcOver = 1000148003,
        DstOver = 1000148004,
        SrcIn = 1000148005,
        DstIn = 1000148006,
        SrcOut = 1000148007,
        DstOut = 1000148008,
        SrcAtop = 1000148009,
        DstAtop = 1000148010,
        Xor = 1000148011,
        Multiply = 1000148012,
        Screen = 1000148013,
        Overlay = 1000148014,
        Darken = 1000148015,
        Lighten = 1000148016,
        ColorDodge = 1000148017,
        ColorBurn = 1000148018,
        HardLight = 1000148019,
        SoftLight = 1000148020,
        Difference = 1000148021,
        Exclusion = 1000148022,
        Invert = 1000148023,
        InvertRGB = 1000148024,
        LinearDodge = 1000148025,
        LinearBurn = 1000148026,
        VividLight = 1000148027,
        LinearLight = 1000148028,
        PinLight = 1000148029,
        HardMix = 1000148030,
        HSLHue = 1000148031,
        HSLSaturation = 1000148032,
        HSLColor = 1000148033,
        HSLLuminosity = 1000148034,
        Plus = 1000148035,
        PlusClamped = 1000148036,
        PlusClampedAlpha = 1000148037,
        PlusDarker = 1000148038,
        Minus = 1000148039,
        MinusClamped = 1000148040,
        Contrast = 1000148041,
        InvertOVG = 1000148042,
        Red = 1000148043,
        Green = 1000148044,
        Blue = 1000148045,
    }

    public enum DynamicState
    {
        Viewport = 0,
        Scissor = 1,
        LineWidth = 2,
        DepthBias = 3,
        BlendConstants = 4,
        DepthBounds = 5,
        StencilCompareMask = 6,
        StencilWriteMask = 7,
        StencilReference = 8,
        ViewportWScaling = 1000087000,
        DiscardRectangle = 1000099000
    }

    public enum Filter
    {
        Nearest = 0,
        Linear = 1,
        Cubic = 1000015000
    }

    public enum SamplerMipmapMode
    {
        Nearest = 0,
        Linear = 1
    }

    public enum SamplerAddressMode
    {
        Repeat = 0,
        MirroredRepeat = 1,
        ClampToEdge = 2,
        ClampToBorder = 3,
        MirrorClampToEdge = 4
    }

    public enum BorderColor
    {
        FloatTransparentBlack = 0,
        IntTransparentBlack = 1,
        FloatOpaqueBlack = 2,
        IntOpaqueBlack = 3,
        FloatOpaqueWhite = 4,
        IntOpaqueWhite = 5
    }

    public enum DescriptorType
    {
        Sampler = 0,
        CombinedImageSampler = 1,
        SampledImage = 2,
        StorageImage = 3,
        UniformTexelBuffer = 4,
        StorageTexelBuffer = 5,
        UniformBuffer = 6,
        StorageBuffer = 7,
        UniformBufferDynamic = 8,
        StorageBufferDynamic = 9,
        InputAttachment = 10
    }

    public enum AttachmentLoadOp
    {
        Load = 0,
        Clear = 1,
        DontCare = 2
    }

    public enum AttachmentStoreOp
    {
        Store = 0,
        DontCare = 1
    }

    public enum PipelineBindPoint
    {
        Graphics = 0,
        Compute = 1
    }

    public enum CommandBufferLevel
    {
        Primary = 0,
        Secondary = 1
    }

    public enum IndexType
    {
        Uint16 = 0,
        Uint32 = 1
    }

    public enum SubpassContents
    {
        Inline = 0,
        SecondaryCommandBuffers = 1
    }

    public enum ObjectType
    {
        Unkwown = 0,
        Instance = 1,
        PhysicalDevice = 2,
        Device = 3,
        Queue = 4,
        Semaphore = 5,
        CommandBuffer = 6,
        Fence = 7,
        DeviceMemory = 8,
        Buffer = 9,
        Image = 10,
        Event = 11,
        QueryPool = 12,
        BufferView = 13,
        ImageView = 14,
        ShaderModule = 15,
        PipelineCache = 16,
        PipelineLayout = 17,
        RenderPass = 18,
        Pipeline = 19,
        DescriptorSetLayout = 20,
        Sampler = 21,
        DescriptorPool = 22,
        DescriptorSet = 23,
        Framebuffer = 24,
        CommandPool = 25,
        Surface = 1000000000,
        Swapchain = 1000001000,
        Display = 1000002000,
        DisplayMode = 1000002001,
        DebugReportCallback = 1000011000,
        DescriptorUpdateTemplate = 1000085000,
        ObjectTable = 1000086000,
        IndirectCommandsLayout = 1000086001
    }

    [Flags] public enum InstanceCreateFlags : uint { None = 0 }

    [Flags]
    public enum FormatFeatureFlags : uint
    {
        SampledImage = 1 << 0,
        StorageImage = 1 << 1,
        StorageImageAtomic = 1 << 2,
        UniformTexelBuffer = 1 << 3,
        StorageTexelBuffer = 1 << 4,
        StorageTexelBufferAtomic = 1 << 5,
        VertexBuffer = 1 << 6,
        ColorAttachment = 1 << 7,
        ColorAttachmentBlend = 1 << 8,
        DepthStencilAttachment = 1 << 9,
        BlitSrc = 1 << 10,
        BlitDst = 1 << 11,
        SampledImageFilterLinear = 1 << 12,
        SampledImageFilterCubic = 1 << 13,
        TransferSrc = 1 << 14,
        TransferDst = 1 << 15,
        SampledImageFilterMinMax = 1 << 16
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
    public enum QueueFlags : uint
    {
        Graphics = 1 << 0,
        Compute = 1 << 1,
        Transfer = 1 << 2,
        SparseBinding = 1 << 3
    }

    [Flags]
    public enum MemoryPropertyFlags : uint
    {
        DeviceLocal = 0x00000001,
        HostVisible = 0x00000002,
        HostCoherent = 0x00000004,
        HostCached = 0x00000008,
        LazilyAllocated = 0x00000010,
    }

    [Flags]
    public enum MemoryHeapFlags : uint
    {
        DeviceLocal = 0x00000001,
        MultiInstance = 0x00000002,
    }

    [Flags] public enum DeviceCreateFlags : uint { None = 0 }
    [Flags] public enum DeviceQueueCreateFlags : uint { None = 0 }

    [Flags]
    public enum PipelineStageFlags : uint
    {
        TopOfPipe = 1 << 0,
        DrawIndirect = 1 << 1,
        VertexInput = 1 << 2,
        VertexShader = 1 << 3,
        TessellationControlShader = 1 << 4,
        TessellationEvaluationShader = 1 << 5,
        GeometryShader = 1 << 6,
        FragmentShader = 1 << 7,
        EarlyFragmentTests = 1 << 8,
        LateFragmentTests = 1 << 9,
        ColorAttachmentOutput = 1 << 10,
        ComputeShader = 1 << 11,
        Transfer = 1 << 12,
        BottomOfPipe = 1 << 13,
        Host = 1 << 14,
        AllGraphics = 1 << 15,
        AllCommands = 1 << 16,
        CommandProcess = 1 << 17
    }

    [Flags] public enum MemoryMapFlags : uint { None = 0 }

    [Flags]
    public enum ImageAspectFlags : uint
    {
        Color = 1 << 0,
        Depth = 1 << 1,
        Stencil = 1 << 2,
        Metadata = 1 << 3
    }

    [Flags]
    public enum SparseImageFormatFlags : uint
    {
        SingleMiptail = 1 << 0,
        AlignedMipSize = 1 << 1,
        NonstandardBlockSize = 1 << 2
    }

    [Flags]
    public enum SparseMemoryBindFlags : uint
    {
        Metadata = 1 << 0
    }

    [Flags]
    public enum FenceCreateFlags : uint
    {
        None = 0,
        Signaled = 1 << 0
    }

    [Flags] public enum SemaphoreCreateFlags : uint { None = 0 }
    [Flags] public enum EventCreateFlags : uint { None = 0 }
    [Flags] public enum QueryPoolCreateFlags : uint { None = 0 }

    [Flags]
    public enum QueryPipelineStatisticFlags : uint
    {
        InputAssemblyVertices = 1 << 0,
        InputAssemblyPrimitives = 1 << 1,
        VertexShaderInvocations = 1 << 2,
        GeometryShaderInvocations = 1 << 3,
        GeometryShaderPrimitives = 1 << 4,
        ClippingInvocations = 1 << 5,
        ClippingPrimitives = 1 << 6,
        FragmentShaderInvocations = 1 << 7,
        TessellationControlShaderPatches = 1 << 8,
        TessellationEvaluationShaderInvocations = 1 << 9,
        ComputeShaderInvocations = 1 << 10
    }

    [Flags]
    public enum QueryResultFlags : uint
    {
        SixtyFour = 1 << 0,
        Wait = 1 << 1,
        WithAvailability = 1 << 2,
        Partial = 1 << 3
    }

    [Flags]
    public enum BufferCreateFlags : uint
    {
        SparseBinding = 1 << 0,
        SparseResidency = 1 << 1,
        SparseAliased = 1 << 2
    }

    [Flags]
    public enum BufferUsageFlags : uint
    {
        TransferSrc = 1 << 0,
        TransferDst = 1 << 1,
        UniformTexelBuffer = 1 << 2,
        StorageTexelBuffer = 1 << 3,
        UniformBuffer = 1 << 4,
        StorageBuffer = 1 << 5,
        IndexBuffer = 1 << 6,
        VertexBuffer = 1 << 7,
        IndirectBuffer = 1 << 8
    }

    [Flags] public enum BufferViewCreateFlags : uint { None = 0 }
    [Flags] public enum ImageViewCreateFlags : uint { None = 0 }
    [Flags] public enum ShaderModuleCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineCacheCreateFlags : uint { None = 0 }

    [Flags]
    public enum PipelineCreateFlags : uint
    {
        DisableOptimization = 0x00000001,
        AllowDerivatives = 0x00000002,
        Derivative = 0x00000004,
        ViewIndexFromDeviceIndex = 0x00000008,
        DispatchBase = 0x00000010
    }

    [Flags] public enum PipelineShaderStageCreateFlags : uint { None = 0 }

    [Flags]
    public enum ShaderStageFlags : uint
    {
        Vertex = 0x00000001,
        TessellationControl = 0x00000002,
        TessellationEvaluation = 0x00000004,
        Geometry = 0x00000008,
        Fragment = 0x00000010,
        Compute = 0x00000020,
        AllGraphics = 0x0000001F,
        All = 0x7FFFFFFF
    }

    [Flags] public enum PipelineVertexInputStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineInputAssemblyStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineTessellationStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineViewportStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineRasterizationStateCreateFlags : uint { None = 0 }

    [Flags]
    public enum CullModeFlags : uint
    {
        None = 0,
        Front = 0x00000001,
        Back = 0x00000002,
        FrontAndBack = 0x00000003
    }

    [Flags] public enum PipelineMultisampleStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineDepthStencilStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineColorBlendStateCreateFlags : uint { None = 0 }

    [Flags]
    public enum ColorComponentFlags : uint
    {
        R = 0x00000001,
        G = 0x00000002,
        B = 0x00000004,
        A = 0x00000008
    }

    [Flags] public enum PipelineDynamicStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineLayoutCreateFlags : uint { None = 0 }
    [Flags] public enum SamplerCreateFlags : uint { None = 0 }

    [Flags]
    public enum DescriptorSetLayoutCreateFlags : uint
    {
        PushDescriptor = 1 << 0
    }

    [Flags]
    public enum DescriptorPoolCreateFlags : uint
    {
        FreeDescriptorSet = 1 << 0
    }

    [Flags] public enum DescriptorPoolResetFlags : uint { None = 0 }
    [Flags] public enum FramebufferCreateFlags : uint { None = 0 }
    [Flags] public enum RenderPassCreateFlags : uint { None = 0 }

    [Flags]
    public enum AttachmentDescriptionFlags : uint
    {
        MayAlias = 1 << 0
    }

    [Flags]
    public enum SubpassDescriptionFlags : uint
    {
        PerViewAttributes = 1 << 0,
        PerViewPositionXOnly = 1 << 1
    }

    [Flags]
    public enum AccessFlags : uint
    {
        IndirectCommandRead = 1 << 0,
        IndexRead = 1 << 1,
        VertexAttributeRead = 1 << 2,
        UniformRead = 1 << 3,
        InputAttachmentRead = 1 << 4,
        ShaderRead = 1 << 5,
        ShaderWrite = 1 << 6,
        ColorAttachmentRead = 1 << 7,
        ColorAttachmentWrite = 1 << 8,
        DepthStencilAttachmentRead = 1 << 9,
        DepthStencilAttachmentWrite = 1 << 10,
        TransferRead = 1 << 11,
        TransferWrite = 1 << 12,
        HostRead = 1 << 13,
        HostWrite = 1 << 14,
        MemoryRead = 1 << 15,
        MemoryWrite = 1 << 16,
        CommandProcessRead = 1 << 17,
        CommandProcessWrite = 1 << 18,
        ColorAttachmentReadNonCoherent = 1 << 19
    }

    [Flags]
    public enum DependencyFlags : uint
    {
        ByRegion = 1 << 0,
        ViewLocal = 1 << 1,
        DeviceGroup = 1 << 2
    }

    [Flags]
    public enum CommandPoolCreateFlags : uint
    {
        Transient = 1 << 0,
        ResetCommandBuffer = 1 << 1
    }

    [Flags]
    public enum CommandPoolResetFlags : uint
    {
        ReleaseResources = 1 << 0
    }

    [Flags]
    public enum CommandBufferUsageFlags : uint
    {
        OneTimeSubmit = 1 << 0,
        RenderPassContinue = 1 << 1,
        SimultaneousUse = 1 << 2
    }

    [Flags]
    public enum QueryControlFlags : uint
    {
        Precise = 1 << 0
    }

    [Flags]
    public enum CommandBufferResetFlags : uint
    {
        ReleaseResources = 1 << 0
    }

    [Flags]
    public enum StencilFaceFlags : uint
    {
        Front = 1 << 0,
        Back = 1 << 1,
        FrontAndBack = Front | Back
    }

    //
    // Khronos
    //
    public enum ColorSpace
    {
        SrgbNonLinear = 0,
        DisplayP3NonLinear = 1000104001,
        ExtendedSrgbLinear = 1000104002,
        DciP3Linear = 1000104003,
        DciP3NonLinear = 1000104004,
        Bt709Linear = 1000104005,
        Bt709NonLinear = 1000104006,
        Bt2020Linear = 1000104007,
        Hdr10St2084 = 1000104008,
        DolbyVision = 1000104009,
        Hdr10Hlg = 1000104010,
        AdobeRgbLinear = 1000104011,
        AdobeRgbNonLinear = 1000104012,
        PassThrough = 1000104013,
        ExtendedSrgbNonLinear = 1000104014
    }

    public enum PresentMode
    {
        Immediate = 0,
        Mailbox = 1,
        Fifo = 2,
        FifoRelaxed = 3,
        SharedDemandRefresh = 1000111000,
        SharedContinuousRefresh = 1000111001
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

    [Flags]
    public enum SwapchainCreateFlags : uint
    {
        BindSFR = 1 << 0
    }

    [Flags]
    public enum DisplayPlaneAlphaFlags : uint
    {
        Opaque = 1 << 0,
        Global = 1 << 1,
        PerPixel = 1 << 2,
        PerPixelPremultiplied = 1 << 3
    }

    [Flags] public enum DisplayModeCreateFlags : uint { None = 0 }
    [Flags] public enum DisplaySurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum XlibSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum XcbSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum WaylandSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum MirSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum AndroidSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum Win32SurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum CommandPoolTrimFlags : uint { None = 0 }

    [Flags]
    public enum ExternalMemoryHandleTypeFlags : uint
    {
        OpaqueFd = 1 << 0,
        OpaqueWin32 = 1 << 1,
        OpaqueWin32Kmt = 1 << 2,
        D3D11Texture = 1 << 3,
        D3D11TextureKmt = 1 << 4,
        D3D12Heap = 1 << 5,
        D3D12Resource = 1 << 6
    }

    [Flags]
    public enum ExternalMemoryFeatureFlags : uint
    {
        DedicatedOnly = 1 << 0,
        Exportable = 1 << 1,
        Importable = 1 << 2
    }

    [Flags]
    public enum ExternalSemaphoreHandleTypeFlags : uint
    {
        OpaqueFd = 1 << 0,
        OpaqueWin32 = 1 << 1,
        OpaqueWin32Kmt = 1 << 2,
        D3D12Fence = 1 << 3,
        SyncFd = 1 << 4
    }

    [Flags]
    public enum ExternalSemaphoreFeatureFlags : uint
    {
        Exportable = 1 << 0,
        Importable = 1 << 1
    }

    [Flags]
    public enum SemaphoreImportFlags : uint
    {
        Temporary = 1 << 0
    }

    public enum DescriptorUpdateTemplateType
    {
        DescriptorSet = 0,
        PushDescriptors = 1
    }

    [Flags] public enum DescriptorUpdateTemplateCreateFlags : uint { None = 0 }

    [Flags]
    public enum ExternalFenceHandleTypeFlags : uint
    {
        OpaqueFd = 1 << 0,
        OpaqueWin32 = 1 << 1,
        OpaqueWin32Kmt = 1 << 2,
        SyncFd = 1 << 3
    }

    [Flags]
    public enum ExternalFenceFeatureFlags : uint
    {
        Exportable = 1 << 0,
        Importable = 1 << 1
    }

    [Flags]
    public enum FenceImportFlags : uint
    {
        Temporary = 1 << 0
    }

    //
    // Multi-vendor
    //
    public enum DebugReportObjectType
    {
        Unknown = 0,
        Instance = 1,
        PhysicalDevice = 2,
        Device = 3,
        Queue = 4,
        Semaphore = 5,
        CommandBuffer = 6,
        Fence = 7,
        DeviceMemory = 8,
        Buffer = 9,
        Image = 10,
        Event = 11,
        QueryPool = 12,
        BufferView = 13,
        ImageView = 14,
        ShaderModule = 15,
        PipelineCache = 16,
        PipelineLayout = 17,
        RenderPass = 18,
        Pipeline = 19,
        DescriptorSetLayout = 20,
        Sampler = 21,
        DescriptorPool = 22,
        DescriptorSet = 23,
        Framebuffer = 24,
        CommandPool = 25,
        Surface = 26,
        Swapchain = 27,
        DebugReportCallback = 28,
        Display = 29,
        DisplayMode = 30,
        ObjectTable = 31,
        IndirectCommandsLayout = 32,
        DescriptorUpdateTemplate = 1000085000
    }

    [Flags]
    public enum DebugReportFlags : uint
    {
        Information = 0x00000001,
        Warning = 0x00000002,
        PerformanceWarning = 0x00000004,
        Error = 0x00000008,
        Debug = 0x00000010,
    }

    public enum ValidationCheck
    {
        All = 0,
        Shaders = 1
    }

    [Flags]
    public enum SurfaceCounterFlags : uint
    {
        VBlank = 1 << 0
    }

    public enum DisplayPowerState
    {
        Off = 0,
        Suspend = 1,
        On = 2
    }

    public enum DeviceEventType
    {
        DisplayHotplug = 0
    }

    public enum DisplayEventType
    {
        FirstPixelOut = 0
    }

    public enum DiscardRectangleMode
    {
        Inclusive = 0,
        Exclusive = 1
    }

    [Flags] public enum PipelineDiscardRectangleStateCreateFlags : uint { None = 0 }

    public enum SamplerReductionMode
    {
        WeightedAverage = 0,
        Min = 1,
        Max = 2
    }

    public enum BlendOverlap
    {
        Uncorrelated = 0,
        Disjoint = 1,
        Conjoint = 2
    }

    //
    // AMD
    //
    public enum RasterizationOrder
    {
        Strict = 0,
        Relaxed = 1
    }

    //
    // Nvidia
    //
    [Flags]
    public enum ExternalMemoryHandleTypeFlagsNV : uint
    {
        OpaqueWin32 = 1 << 0,
        OpaqueWin32Kmt = 1 << 1,
        D3D11Image = 1 << 2,
        D3D11ImageKmt = 1 << 3
    }

    [Flags]
    public enum ExternalMemoryFeatureFlagsNV : uint
    {
        Dedicated = 1 << 0,
        Exportable = 1 << 1,
        Importable = 1 << 2
    }

    [Flags] public enum PipelineCoverageToColorStateCreateFlags : uint { None = 0 }

    public enum CoverageModulationMode
    {
        None = 0,
        RGB = 1,
        Alpha = 2,
        RGBA = 3
    }

    [Flags] public enum PipelineCoverageModulationStateCreateFlags : uint { None = 0 }


    public enum ViewportCoordinateSwizzle
    {
        PositiveX = 0,
        NegativeX = 1,
        PositiveY = 2,
        NegativeY = 3,
        PositiveZ = 4,
        NegativeZ = 5,
        PositiveW = 6,
        NegativeW = 7
    }

    [Flags] public enum PipelineViewportSwizzleStateCreateFlags : uint { None = 0 }

    //
    // Khronos X
    //
    [Flags]
    public enum PeerMemoryFeatureFlags : uint
    {
        CopySrc = 1 << 0,
        CopyDst = 1 << 1,
        GenericSrc = 1 << 2,
        GenericDst = 1 << 3
    }

    [Flags]
    public enum MemoryAllocateFlags : uint
    {
        DeviceMask = 1 << 0
    }

    [Flags]
    public enum DeviceGroupPresentModeFlags : uint
    {
        Local = 1 << 0,
        Remote = 1 << 1,
        Sum = 1 << 2,
        LocalMultiDevice = 1 << 3
    }

    //
    // Nintendo
    //
    [Flags] public enum ViSurfaceCreateFlags : uint { None = 0 }

    //
    // Nvidia X
    //
    public enum IndirectCommandsTokenType
    {
        Pipeline = 0,
        Descriptor = 1,
        IndexBuffer = 2,
        VertexBuffer = 3,
        PushConstant = 4,
        DrawIndexed = 5,
        Draw = 6,
        Dispatch = 7
    }

    public enum ObjectEntryType
    {
        DescriptorSet = 0,
        Pipeline = 1,
        IndexBuffer = 2,
        VertexBuffer = 3,
        PushConstant = 4
    }

    [Flags]
    public enum IndirectCommandsLayoutUsageFlags : uint
    {
        UnorderedSequences = 1 << 0,
        SparseSequences = 1 << 1,
        EmptyExecutions = 1 << 2,
        IndexedSequences = 1 << 3
    }

    [Flags]
    public enum ObjectEntryUsageFlags : uint
    {
        Graphics = 1 << 0,
        Compute = 1 << 1
    }

    //
    // MoltenVK
    // 
    [Flags] public enum IOSSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum MacOSSurfaceCreateFlags : uint { None = 0 }
}