using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    public struct Size
    {
        public ulong Handle;
    }

    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct Version
    {
        private readonly uint _value;

        public Version(uint major, uint minor, uint patch)
        {
            _value = major << 22 | minor << 12 | patch;
        }

        public uint Major => _value >> 22;

        public uint Minor => (_value >> 12) & 0x3ff;

        public uint Patch => (_value >> 22) & 0xfff;

        public static implicit operator uint(Version version)
        {
            return version._value;
        }
    }

    public unsafe struct ApplicationInfo
    {
        public StructureType Type;
        public void* Next;
        public Text ApplicationName;
        public Version ApplicationVersion;
        public Text EngineName;
        public Version EngineVersion;
        public Version ApiVersion;
    }

    public unsafe struct InstanceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public InstanceCreateFlags Flags;
        public ApplicationInfo* ApplicationInfo;
        public uint EnabledLayerCount;
        public Text* EnabledLayerNames;
        public uint EnabledExtensionCount;
        public Text* EnabledExtensionNames;
    }

    public unsafe struct AllocationCallbacks
    {
        public void* UserData;
        public AllocationFunction Allocation;
        public ReallocationFunction Reallocation;
        public FreeFunction Free;
        public InternalAllocationNotification InternalAllocation;
        public InternalFreeNotification InternalFree;
    }

    public struct PhysicalDeviceFeatures
    {
        public Bool32 RobustBufferAccess;
        public Bool32 FullDrawIndexUint32;
        public Bool32 ImageCubeArray;
        public Bool32 IndependentBlend;
        public Bool32 GeometryShader;
        public Bool32 TessellationShader;
        public Bool32 SampleRateShading;
        public Bool32 DualSrcBlend;
        public Bool32 LogicOp;
        public Bool32 MultiDrawIndirect;
        public Bool32 DrawIndirectFirstInstance;
        public Bool32 DepthClamp;
        public Bool32 DepthBiasClamp;
        public Bool32 FillModeNonSolid;
        public Bool32 DepthBounds;
        public Bool32 WideLines;
        public Bool32 LargePoints;
        public Bool32 AlphaToOne;
        public Bool32 MultiViewport;
        public Bool32 SamplerAnisotropy;
        public Bool32 TextureCompressionETC2;
        public Bool32 TextureCompressionASTC_LDR;
        public Bool32 TextureCompressionBC;
        public Bool32 OcclusionQueryPrecise;
        public Bool32 PipelineStatisticsQuery;
        public Bool32 VertexPipelineStoresAndAtomics;
        public Bool32 FragmentStoresAndAtomics;
        public Bool32 ShaderTessellationAndGeometryPointSize;
        public Bool32 ShaderImageGatherExtended;
        public Bool32 ShaderStorageImageExtendedFormats;
        public Bool32 ShaderStorageImageMultisample;
        public Bool32 ShaderStorageImageReadWithoutFormat;
        public Bool32 ShaderStorageImageWriteWithoutFormat;
        public Bool32 ShaderUniformBufferArrayDynamicIndexing;
        public Bool32 ShaderSampledImageArrayDynamicIndexing;
        public Bool32 ShaderStorageBufferArrayDynamicIndexing;
        public Bool32 ShaderStorageImageArrayDynamicIndexing;
        public Bool32 ShaderClipDistance;
        public Bool32 ShaderCullDistance;
        public Bool32 ShaderFloat64;
        public Bool32 ShaderInt64;
        public Bool32 ShaderInt16;
        public Bool32 ShaderResourceResidency;
        public Bool32 ShaderResourceMinLod;
        public Bool32 SparseBinding;
        public Bool32 SparseResidencyBuffer;
        public Bool32 SparseResidencyImage2D;
        public Bool32 SparseResidencyImage3D;
        public Bool32 SparseResidency2Samples;
        public Bool32 SparseResidency4Samples;
        public Bool32 SparseResidency8Samples;
        public Bool32 SparseResidency16Samples;
        public Bool32 SparseResidencyAliased;
        public Bool32 VariableMultisampleRate;
        public Bool32 InheritedQueries;
    }

    public struct FormatProperties
    {
        public FormatFeatureFlags LinearTilingFeatures;
        public FormatFeatureFlags OptimalTilingFeatures;
        public FormatFeatureFlags BufferFeatures;
    }

    public struct Extent3D
    {
        public uint Width;
        public uint Height;
        public uint Depth;
    }

    public struct ImageFormatProperties
    {
        public Extent3D MaxExtents;
        public uint MaxMipLevels;
        public uint MaxArrayLayers;
        public SampleCountFlags SampleCounts;
        public DeviceSize MaxResourceSize;
    }

    public unsafe struct PhysicalDeviceLimits
    {
        public uint MaxImageDimension1D;
        public uint MaxImageDimension2D;
        public uint MaxImageDimension3D;
        public uint MaxImageDimensionCube;
        public uint MaxImageArrayLayers;
        public uint MaxTexelBufferElements;
        public uint MaxUniformBufferRange;
        public uint MaxStorageBufferRange;
        public uint MaxPushConstantsSize;
        public uint MaxMemoryAllocationCount;
        public uint MaxSamplerAllocationCount;
        public DeviceSize BufferImageGranularity;
        public DeviceSize SparseAddressSpaceSize;
        public uint MaxBoundDescriptorSets;
        public uint MaxPerStageDescriptorSamplers;
        public uint MaxPerStageDescriptorUniformBuffers;
        public uint MaxPerStageDescriptorStorageBuffers;
        public uint MaxPerStageDescriptorSampledImages;
        public uint MaxPerStageDescriptorStorageImages;
        public uint MaxPerStageDescriptorInputAttachments;
        public uint MaxPerStageResources;
        public uint MaxDescriptorSetSamplers;
        public uint MaxDescriptorSetUniformBuffers;
        public uint MaxDescriptorSetUniformBuffersDynamic;
        public uint MaxDescriptorSetStorageBuffers;
        public uint MaxDescriptorSetStorageBuffersDynamic;
        public uint MaxDescriptorSetSampledImages;
        public uint MaxDescriptorSetStorageImages;
        public uint MaxDescriptorSetInputAttachments;
        public uint MaxVertexInputAttributes;
        public uint MaxVertexInputBindings;
        public uint MaxVertexInputAttributeOffset;
        public uint MaxVertexInputBindingStride;
        public uint MaxVertexOutputComponents;
        public uint MaxTessellationGenerationLevel;
        public uint MaxTessellationPatchSize;
        public uint MaxTessellationControlPerVertexInputComponents;
        public uint MaxTessellationControlPerVertexOutputComponents;
        public uint MaxTessellationControlPerPatchOutputComponents;
        public uint MaxTessellationControlTotalOutputComponents;
        public uint MaxTessellationEvaluationInputComponents;
        public uint MaxTessellationEvaluationOutputComponents;
        public uint MaxGeometryShaderInvocations;
        public uint MaxGeometryInputComponents;
        public uint MaxGeometryOutputComponents;
        public uint MaxGeometryOutputVertices;
        public uint MaxGeometryTotalOutputComponents;
        public uint MaxFragmentInputComponents;
        public uint MaxFragmentOutputAttachments;
        public uint MaxFragmentDualSrcAttachments;
        public uint MaxFragmentCombinedOutputResources;
        public uint MaxComputeSharedMemorySize;
        public fixed uint MaxComputeWorkGroupCount[3];
        public uint MaxComputeWorkGroupInvocations;
        public fixed uint MaxComputeWorkGroupSize[3];
        public uint SubPixelPrecisionBits;
        public uint SubTexelPrecisionBits;
        public uint MipmapPrecisionBits;
        public uint MaxDrawIndexedIndexValue;
        public uint MaxDrawIndirectCount;
        public float MaxSamplerLodBias;
        public float MaxSamplerAnisotropy;
        public uint MaxViewports;
        public fixed uint MaxViewportDimensions[2];
        public fixed float ViewportBoundsRange[2];
        public uint ViewportSubPixelBits;
        public ulong MinMemoryMapAlignment;
        public DeviceSize MinTexelBufferOffsetAlignment;
        public DeviceSize MinUniformBufferOffsetAlignment;
        public DeviceSize MinStorageBufferOffsetAlignment;
        public int MinTexelOffset;
        public uint MaxTexelOffset;
        public int MinTexelGatherOffset;
        public uint MaxTexelGatherOffset;
        public float MinInterpolationOffset;
        public float MaxInterpolationOffset;
        public uint SubPixelInterpolationOffsetBits;
        public uint MaxFramebufferWidth;
        public uint MaxFramebufferHeight;
        public uint MaxFramebufferLayers;
        public SampleCountFlags FramebufferColorSampleCounts;
        public SampleCountFlags FramebufferDepthSampleCounts;
        public SampleCountFlags FramebufferStencilSampleCounts;
        public SampleCountFlags FramebufferNoAttachmentsSampleCounts;
        public uint MaxColorAttachments;
        public SampleCountFlags SampledImageColorSampleCounts;
        public SampleCountFlags SampledImageIntegerSampleCounts;
        public SampleCountFlags SampledImageDepthSampleCounts;
        public SampleCountFlags SampledImageStencilSampleCounts;
        public SampleCountFlags StorageImageSampleCounts;
        public uint MaxSampleMaskWords;
        public Bool32 TimestampComputeAndGraphics;
        public float TimestampPeriod;
        public uint MaxClipDistances;
        public uint MaxCullDistances;
        public uint MaxCombinedClipAndCullDistances;
        public uint DiscreteQueuePriorities;
        public fixed float PointSizeRange[2];
        public fixed float LineWidthRange[2];
        public float PointSizeGranularity;
        public float LineWidthGranularity;
        public Bool32 StrictLines;
        public Bool32 StandardSampleLocations;
        public DeviceSize OptimalBufferCopyOffsetAlignment;
        public DeviceSize OptimalBufferCopyRowPitchAlignment;
        public DeviceSize NonCoherentAtomSize;
    }

    public struct PhysicalDeviceSparseProperties
    {
        public Bool32 ResidencyStandard2DBlockShape;
        public Bool32 ResidencyStandard2DMultisampleBlockShape;
        public Bool32 ResidencyStandard3DBlockShape;
        public Bool32 ResidencyAlignedMipSize;
        public Bool32 ResidencyNonResidentStrict;
    }

    public unsafe struct PhysicalDeviceProperties
    {
        public uint ApiVersion;
        public uint DriverVersion;
        public uint VendorId;
        public uint DeviceId;
        public PhysicalDeviceType DeviceType;
        public fixed byte DeviceName[(int)Vulkan.MaxPhysicalDeviceNameSize];
        public fixed byte PipelineCacheUuid[(int)Vulkan.UuidSize];
        public PhysicalDeviceLimits Limits;
        public PhysicalDeviceSparseProperties SparseProperties;
    }

    public struct QueueFamilyProperties
    {
        public QueueFlags QueueFlags;
        public uint QueueCount;
        public uint TimestampValidBits;
        public Extent3D MinIMageTransferGranularity;
    }

    public struct MemoryType
    {
        public MemoryPropertyFlags PropertyFlags;
        public uint HeapIndex;
    }

    public struct MemoryHeap
    {
        public DeviceSize Size;
        public MemoryHeapFlags Flags;
    }

    public struct PhysicalDeviceMemoryProperties
    {
        public uint MemoryTypeCount;
        /// <summary>
        /// <see cref="MemoryType"/>[<see cref="Vulkan.MaxMemoryTypes"/>]
        /// </summary>
        public IntPtr MemoryTypes;
        public uint MemoryHeapCount;
        /// <summary>
        /// <see cref="MemoryHeap"/>[<see cref="Vulkan.MaxMemoryHeaps"/>]
        /// </summary>
        public IntPtr MemoryHeaps;
    }

    public unsafe struct DeviceQueueCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceQueueCreateFlags Flags;
        public uint QueueFamilyIndex;
        public uint QueueCount;
        public float* QueuePriorities;
    }

    public unsafe struct DeviceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceCreateFlags Flags;
        public uint QueueCreateInfoCount;
        public DeviceQueueCreateInfo* QueueCreateInfos;
        public uint EnabledLayerCount;
        public Text* EnabledLayerNames;
        public uint EnabledExtensionCount;
        public Text* EnabledExtensionNames;
        public PhysicalDeviceFeatures* EnabledFeatures;
    }

    public unsafe struct ExtensionProperties
    {
        public fixed byte ExtensionName[(int)Vulkan.MaxExtensionNameSize];
        public Version SpecVersion;
    }

    public unsafe struct LayerProperties
    {
        public fixed byte LayerName[(int)Vulkan.MaxExtensionNameSize];
        public Version SpecVersion;
        public Version ImplementationVersion;
        public fixed byte Description[(int)Vulkan.MaxDescriptionSize];
    }

    public unsafe struct SubmitInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public Semaphore* WaitSemaphores;
        public PipelineStageFlags* WaitDstStageMask;
        public uint CommandBufferCount;
        public CommandBuffer* CommandBuffers;
        public uint SignalSemaphoreCount;
        public Semaphore* SignalSemaphores;
    }

    public unsafe struct MemoryAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceSize AllocationSize;
        public uint MemoryTypeIndex;
    }

    public unsafe struct MappedMemoryRange
    {
        public StructureType Type;
        public void* Next;
        public DeviceMemory Memory;
        public DeviceSize Offset;
        public DeviceSize Size;
    }

    public struct MemoryRequirements
    {
        public DeviceSize Size;
        public DeviceSize Alignment;
        public uint MemoryTypeBits;
    }

    public struct SparseImageFormatProperties
    {
        public ImageAspectFlags AspectMask;
        public Extent3D ImageGranularity;
        public SparseImageFormatFlags Flags;
    }

    public struct SparseImageMemoryRequirements
    {
        public SparseImageFormatProperties FormatProperties;
        public uint ImageMipTailFirstLod;
        public DeviceSize ImageMipTailSize;
        public DeviceSize ImageMipTailOffset;
        public DeviceSize ImageMipTailStride;
    }

    public struct SparseMemoryBind
    {
        public DeviceSize ResourceOffset;
        public DeviceSize Size;
        public DeviceMemory Memory;
        public DeviceSize MemoryOffset;
        public SparseMemoryBindFlags Flags;
    }

    public unsafe struct SparseBufferMemoryBindInfo
    {
        public Buffer Buffer;
        public uint BindCount;
        public SparseMemoryBind* Binds;
    }

    public unsafe struct SparseImageOpaqueMemoryBindInfo
    {
        public Image Image;
        public uint BindCount;
        public SparseMemoryBind* Binds;
    }

    public struct ImageSubresource
    {
        public ImageAspectFlags AspectMask;
        public uint MipLevel;
        public uint ArrayLayer;
    }

    public struct Offset3D
    {
        public int X;
        public int Y;
        public int Z;
    }

    public struct SparseImageMemoryBind
    {
        public ImageSubresource Subresource;
        public Offset3D Offset;
        public Extent3D Extent;
        public DeviceMemory Memory;
        public DeviceSize MemoryOffset;
        public SparseMemoryBindFlags Flags;
    }

    public unsafe struct SparseImageMemoryBindInfo
    {
        public Image Image;
        public uint BindCount;
        public SparseImageMemoryBind* Binds;
    }

    public unsafe struct BindSparseInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public Semaphore* WaitSemaphores;
        public uint BufferBindCount;
        public SparseBufferMemoryBindInfo* BufferBinds;
        public uint ImageOpaqueBindCount;
        public SparseImageOpaqueMemoryBindInfo* ImageOpaqueBinds;
        public uint ImageBindCount;
        public SparseImageMemoryBindInfo* ImageBinds;
        public uint SignalSemaphoreCount;
        public Semaphore* SignalSemaphores;
    }

    public unsafe struct FenceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public FenceCreateFlags Flags;
    }

    public unsafe struct SemaphoreCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SemaphoreCreateFlags Flags;
    }

    public unsafe struct EventCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public EventCreateFlags Flags;
    }

    public unsafe struct QueryPoolCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public QueryPoolCreateFlags Flags;
        public QueryType QueryType;
        public uint QueryCount;
        public QueryPipelineStatisticFlags PipelineStatistics;
    }

    public unsafe struct BufferCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public BufferCreateFlags Flags;
        public DeviceSize Size;
        public BufferUsageFlags Usage;
        public SharingMode SharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
    }

    public unsafe struct BufferViewCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public BufferViewCreateFlags Flags;
        public Buffer Buffer;
        public Format Format;
        public DeviceSize Offset;
        public DeviceSize Range;
    }

    public unsafe struct ImageCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ImageCreateFlags Flags;
        public ImageType ImageType;
        public Format Format;
        public Extent3D Extent;
        public uint MipLevels;
        public uint ArrayLayers;
        public SampleCountFlags Samples;
        public ImageTiling Tiling;
        public ImageUsageFlags Usage;
        public SharingMode SharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
        public ImageLayout InitialLayout;
    }

    public struct SubresourceLayout
    {
        public DeviceSize Offset;
        public DeviceSize Size;
        public DeviceSize RowPitch;
        public DeviceSize ArrayPitch;
        public DeviceSize DepthPitch;
    }

    public struct ComponentMapping
    {
        public ComponentSwizzle R;
        public ComponentSwizzle G;
        public ComponentSwizzle B;
        public ComponentSwizzle A;
    }

    public struct ImageSubresourceRange
    {
        public ImageAspectFlags AspectMask;
        public uint BaseMipLevel;
        public uint LevelCount;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    public unsafe struct ImageViewCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ImageViewCreateFlags Flags;
        public Image Image;
        public ImageViewType ViewType;
        public Format Format;
        public ComponentMapping Components;
        public ImageSubresourceRange SubresourceRange;
    }

    public unsafe struct ShaderModuleCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ShaderModuleCreateFlags Flags;
        public Size CodeSize;
        public uint* Code;
    }

    public unsafe struct PipelineCacheCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCacheCreateFlags Flags;
        public Size InitialDataSize;
        public void* InitialData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SpecializationMapEntry
    {
        public uint ConstantId;
        public uint Offset;
        public Size Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpecializationInfo
    {
        public uint MapEntryCount;
        public SpecializationMapEntry* MapEntries;
        public Size DataSize;
        public void* Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineShaderStageCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineShaderStageCreateFlags Flags;
        public ShaderStageFlags Stage;
        public ShaderModule Module;
        public Text Name;
        public SpecializationInfo* SpecializationInfo;
    }

    public struct VertexInputBindingDescription
    {
        public uint Binding;
        public uint Stride;
        public VertexInputRate InputRate;
    }

    public struct VertexInputAttributeDescription
    {
        public uint Location;
        public uint Binding;
        public Format Format;
        public uint Offset;
    }

    public unsafe struct PipelineVertexInputStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineVertexInputStateCreateFlags Flags;
        public uint VertexBindingDescriptionCount;
        public VertexInputBindingDescription* VertexInputBindingDescriptions;
        public uint VertexAttributeDescriptionCount;
        public VertexInputAttributeDescription* VertexAttributeDescriptions;
    }

    public unsafe struct PipelineInputAssemblyStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineInputAssemblyStateCreateFlags Flags;
        public PrimitiveTopology Topology;
        public Bool32 PrimitiveRestartEnable;
    }

    public unsafe struct PipelineTessellationStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineTessellationStateCreateFlags Flags;
        public uint PatchControlPoints;
    }

    public struct Viewport
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float MinDepth;
        public float MaxDepth;
    }

    public struct Offset2D
    {
        public int X;
        public int Y;
    }

    public struct Extent2D
    {
        public uint Width;
        public uint Height;
    }

    public struct Rect2D
    {
        public Offset2D Offset;
        public Extent2D Extent;
    }

    public unsafe struct PipelineViewportStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineViewportStateCreateFlags Flags;
        public uint ViewportCount;
        public Viewport* Viewports;
        public uint ScissorCount;
        public Rect2D* Scissors;
    }

    public unsafe struct PipelineRasterizationStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineRasterizationStateCreateFlags Flags;
        public Bool32 DepthClampEnable;
        public Bool32 RasterizerDiscardEnable;
        public PolygonMode PolygonMode;
        public CullModeFlags CullMode;
        public FrontFace FrontFace;
        public Bool32 DepthBiasEnable;
        public float DepthBiasConstantFactor;
        public float DepthBiasClamp;
        public float DepthBiasSlopeFactor;
        public float LineWidth;
    }

    public unsafe struct PipelineMultisampleStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineMultisampleStateCreateFlags Flags;
        public SampleCountFlags RasterizationSamples;
        public Bool32 SampleShadingEnable;
        public float MinSampleShading;
        public SampleMask* SampleMask;
        public Bool32 AlphaToCoverageEnable;
        public Bool32 AlphaToOneEnable;
    }

    public struct StencilOpState
    {
        public StencilOp FailOp;
        public StencilOp PassOp;
        public StencilOp DepthFailOp;
        public CompareOp CompareOp;
        public uint CompareMask;
        public uint WriteMask;
        public uint Reference;
    }

    public unsafe struct PipelineDepthStencilStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineDepthStencilStateCreateFlags Flags;
        public Bool32 DepthTestEnable;
        public Bool32 DepthWriteEnable;
        public CompareOp DepthCompareOp;
        public Bool32 DepthBoundsTestEnable;
        public Bool32 StencilTestEnable;
        public StencilOpState Front;
        public StencilOpState Back;
        public float MinDepthBounds;
        public float MaxDepthBounds;
    }

    public struct PipelineColorBlendAttachmentState
    {
        public Bool32 BlendEnable;
        public BlendFactor SrcColorBlendFactor;
        public BlendFactor DstColorBlendFactor;
        public BlendOp ColorBlendOp;
        public BlendFactor SrcAlphaBlendFactor;
        public BlendFactor DstAlphaBlendFactor;
        public BlendOp AlphaBlendOp;
        public ColorComponentFlags ColorWriteMask;
    }

    public unsafe struct PipelineColorBlendStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineColorBlendStateCreateFlags Flags;
        public Bool32 LogicOpEnable;
        public LogicOp LogicOp;
        public uint AttachmentCount;
        public PipelineColorBlendAttachmentState* Attachments;
        public fixed float BlendConstants[4];
    }

    public unsafe struct PipelineDynamicStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineDynamicStateCreateFlags Flags;
        public uint DynamicStateCount;
        public DynamicState* DynamicStates;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GraphicsPipelineCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCreateFlags Flags;
        public uint StageCount;
        public PipelineShaderStageCreateInfo* Stages;
        public PipelineVertexInputStateCreateInfo* VertexInputState;
        public PipelineInputAssemblyStateCreateInfo* InputAssemblyState;
        public PipelineTessellationStateCreateInfo* TessellationState;
        public PipelineViewportStateCreateInfo* ViewportState;
        public PipelineRasterizationStateCreateInfo* RasterizationState;
        public PipelineMultisampleStateCreateInfo* MultisampleState;
        public PipelineDepthStencilStateCreateInfo* DepthStencilState;
        public PipelineColorBlendStateCreateInfo* ColorBlendState;
        public PipelineDynamicStateCreateInfo* DynamicState;
        public PipelineLayout Layout;
        public RenderPass RenderPass;
        public uint Subpass;
        public Pipeline BasePipelineHandle;
        public int BasePipelineIndex;
    }

    public unsafe struct ComputePipelineCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCreateFlags Flags;
        public PipelineShaderStageCreateInfo Stage;
        public PipelineLayout Layout;
        public Pipeline BasePipelineHandle;
        public int BasePipelineIndex;
    }

    public struct PushConstantRange
    {
        public ShaderStageFlags stageFlags;
        public uint Offset;
        public uint Size;
    }

    public unsafe struct PipelineLayoutCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineLayoutCreateFlags Flags;
        public uint SetLayoutCount;
        public DescriptorSetLayout* SetLayouts;
        public uint PushConstantRangeCount;
        public PushConstantRange* PushConstantRanges;
    }

    public unsafe struct SamplerCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SampleCountFlags Flags;
        public Filter MagFilter;
        public Filter MinFilter;
        public SamplerMipmapMode MipmapMode;
        public SamplerAddressMode AddressModeU;
        public SamplerAddressMode AddressModeV;
        public SamplerAddressMode AddressModeW;
        public float MipLodBias;
        public Bool32 AnisotropyEnable;
        public float MaxAnisotropy;
        public Bool32 CompareEnable;
        public CompareOp CompareOp;
        public float MinLod;
        public float MaxLod;
        public BorderColor BorderColor;
        public Bool32 UnnormalizedCoordinates;
    }

    public unsafe struct DescriptorSetLayoutBinding
    {
        public uint Binding;
        public DescriptorType DescriptorType;
        public uint DescriptorCount;
        public ShaderStageFlags StageFlags;
        public Sampler* ImmutableSamplers;
    }

    public unsafe struct DescriptorSetLayoutCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorSetLayoutCreateFlags Flags;
        public uint BindingCount;
        public DescriptorSetLayoutBinding* Bindings;
    }

    public struct DescriptorPoolSize
    {
        public DescriptorType Type;
        public uint DescriptorCount;
    }

    public unsafe struct DescriptorPoolCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorPoolCreateFlags Flags;
        public uint MaxSets;
        public uint PoolSizeCount;
        public DescriptorPoolSize* PoolSizes;
    }

    public unsafe struct DescriptorSetAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorPool DescriptorPool;
        public uint DescriptorSetCount;
        public DescriptorSetLayout* SetLayouts;
    }

    public struct DescriptorImageInfo
    {
        public Sampler Sampler;
        public ImageView ImageView;
        public ImageLayout imageLayout;
    }

    public struct DescriptorBufferInfo
    {
        public Buffer Buffer;
        public DeviceSize Offset;
        public DeviceSize Range;
    }

    public unsafe struct WriteDescriptorSet
    {
        public StructureType Type;
        public void* Next;
        public DescriptorSet DstSet;
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
        public DescriptorType DescriptorType;
        public DescriptorImageInfo* ImageInfo;
        public DescriptorBufferInfo* BufferInfo;
        public BufferView* TexelBufferView;
    }

    public unsafe struct CopyDescriptorSet
    {
        public StructureType Type;
        public void* Next;
        public DescriptorSet SrcSet;
        public uint SrcBinding;
        public uint SrcArrayElement;
        public DescriptorSet DstSet;
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
    }

    public unsafe struct FramebufferCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public FramebufferCreateFlags Flags;
        public RenderPass RenderPass;
        public uint AttachmentCount;
        public ImageView* Attachments;
        public uint Width;
        public uint Height;
        public uint Layers;
    }

    public struct AttachmentDescription
    {
        public AttachmentDescriptionFlags Flags;
        public Format Format;
        public SampleCountFlags Samples;
        public AttachmentLoadOp LoadOp;
        public AttachmentStoreOp StoreOp;
        public AttachmentLoadOp StencilLoadOp;
        public AttachmentStoreOp StencilStoreOp;
        public ImageLayout InitialLayout;
        public ImageLayout FinalLayout;
    }

    public struct AttachmentReference
    {
        public uint Attachment;
        public ImageLayout Layout;
    }

    public unsafe struct SubpassDescription
    {
        public SubpassDescriptionFlags Flags;
        public PipelineBindPoint PipelineBindPoint;
        public uint InputAttachmentCount;
        public AttachmentReference* InputAttachments;
        public uint ColorAttachmentCount;
        public AttachmentReference* ColorAttachments;
        public AttachmentReference* ResolveAttachments;
        public AttachmentReference* DepthStencilAttachments;
        public uint PreserveAttachmentCount;
        public uint* PreserveAttachments;
    }

    public struct SubpassDependency
    {
        public uint SrcSubpass;
        public uint DstSubpass;
        public PipelineStageFlags SrcStageMask;
        public PipelineStageFlags DstStageMask;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
        public DependencyFlags DependencyFlags;
    }

    public unsafe struct RenderPassCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public RenderPassCreateFlags Flags;
        public uint AttachmentCount;
        public AttachmentDescription* Attachments;
        public uint SubpassCount;
        public SubpassDescription* Subpasses;
        public uint DependencyCount;
        public SubpassDependency* Dependencies;
    }

    public unsafe struct CommandPoolCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public CommandPoolCreateFlags Flags;
        public uint QueueFamilyIndex;
    }

    public unsafe struct CommandBufferAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public CommandPool CommandPool;
        public CommandBufferLevel Level;
        public uint CommandBufferCount;
    }

    public unsafe struct CommandBufferInheritanceInfo
    {
        public StructureType Type;
        public void* Next;
        public RenderPass RenderPass;
        public uint Subpass;
        public Framebuffer Framebuffer;
        public Bool32 OcclusionQueryEnable;
        public QueryControlFlags QueryFlags;
        public QueryPipelineStatisticFlags PipelineStatistics;
    }

    public unsafe struct CommandBufferBeginInfo
    {
        public StructureType Type;
        public void* Next;
        public CommandBufferUsageFlags Flags;
        public CommandBufferInheritanceInfo* InheritanceInfo;
    }

    public struct BufferCopy
    {
        public DeviceSize SrcOffset;
        public DeviceSize DstOffset;
        public DeviceSize Size;
    }

    public struct ImageSubresourceLayers
    {
        public ImageAspectFlags AspectMask;
        public uint MipLevel;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    public struct ImageCopy
    {
        public ImageSubresourceLayers SrcSubresource;
        public Offset3D SrcOffset;
        public ImageSubresourceLayers DstSubresource;
        public Offset3D DstOffset;
        public Extent3D Extent;
    }

    public unsafe struct ImageBlit
    {
        public ImageSubresourceLayers SrcSubresource;
        /// <summary>
        /// <see cref="Offset3D"/>[2]
        /// </summary>
        public IntPtr SrcOffsets;
        public ImageSubresourceLayers DstSubresource;
        /// <summary>
        /// <see cref="Offset3D"/>[2]
        /// </summary>
        public IntPtr DstOffsets;
    }

    public struct BufferImageCopy
    {
        public DeviceSize BufferOffset;
        public uint BufferRowLength;
        public uint BufferImageHeight;
        public ImageSubresourceLayers ImageSubresource;
        public Offset3D ImageOffset;
        public Extent3D ImageExtent;
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

    public struct ClearAttachment
    {
        public ImageAspectFlags AspectMask;
        public uint ColorAttachment;
        public ClearValue ClearValue;
    }

    public struct ClearRect
    {
        public Rect2D Rect;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    public struct ImageResolve
    {
        public ImageSubresourceLayers SrcSubresource;
        public Offset3D SrcOffset;
        public ImageSubresourceLayers DstSubresource;
        public Offset3D DstOffset;
        public Extent3D Extent;
    }

    public unsafe struct MemoryBarrier
    {
        public StructureType Type;
        public void* Next;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
    }

    public unsafe struct BufferMemoryBarrier
    {
        public StructureType Type;
        public void* Next;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
        public uint SrcQueueFamilyIndex;
        public uint DstQueueFamilyIndex;
        public Buffer Buffer;
        public DeviceSize Offset;
        public DeviceSize Size;
    }

    public unsafe struct ImageMemoryBarrier
    {
        public StructureType Type;
        public void* Next;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
        public ImageLayout OldLayout;
        public ImageLayout NewLayout;
        public uint SrcQueueFamilyIndex;
        public uint DstQueueFamilyIndex;
        public Image Image;
        public ImageSubresourceRange SubresourceRange;
    }

    public unsafe struct RenderPassBeginInfo
    {
        public StructureType Type;
        public void* Next;
        public RenderPass RenderPass;
        public Framebuffer Framebuffer;
        public Rect2D RenderArea;
        public uint ClearValueCount;
        public ClearValue* ClearValues;
    }

    public struct DispatchIndirectCommand
    {
        public uint X;
        public uint Y;
        public uint Z;
    }

    public struct DrawIndexedIndirectCommand
    {
        public uint IndexCount;
        public uint InstanceCount;
        public uint FirstIndex;
        public int VertexOffset;
        public uint FirstInstance;
    }

    public struct DrawIndirectCommand
    {
        public uint VertexCount;
        public uint InstanceCount;
        public uint FirstVertex;
        public uint FirstInstance;
    }

    //
    // KHR surface
    //
    public struct SurfaceCapabilities
    {
        public uint MinImageCount;
        public uint MaxImageCount;
        public Extent2D CurrentExtent;
        public Extent2D MinImageExtent;
        public Extent2D MaxImageExtent;
        public uint MaxImageArrayLayers;
        public SurfaceTransformFlags SupportedTransforms;
        public SurfaceTransformFlags CurrentTransform;
        public CompositeAlphaFlags SupportedCompositeAlpha;
        public ImageUsageFlags SupportedUsageFlags;
    }

    public struct SurfaceFormat
    {
        public Format Format;
        public ColorSpace ColorSpace;
    }

    //
    // KHR swapchain
    //
    public unsafe struct SwapchainCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SwapchainCreateFlags Flags;
        public Surface Surface;
        public uint MinImageCount;
        public Format ImageFormat;
        public ColorSpace ImageColorSpace;
        public Extent2D ImageExtent;
        public uint ImageArrayLayers;
        public ImageUsageFlags ImageUsage;
        public SharingMode ImageSharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
        public SurfaceTransformFlags PreTransform;
        public CompositeAlphaFlags CompositeAlpha;
        public PresentMode PresentMode;
        public Bool32 Clipped;
        public Swapchain OldSwapchain;
    }

    public unsafe struct PresentInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public Semaphore* WaitSemaphores;
        public uint SwapchainCount;
        public Swapchain* Swapchains;
        public uint* ImageIndices;
        public Result* Results;
    }

    //
    // KHR display
    // 
    public unsafe struct DisplayProperties
    {
        public Display Display;
        public Text DisplayName;
        public Extent2D PhysicalDimensions;
        public Extent2D PhysicalResolution;
        public SurfaceTransformFlags SupportedTransforms;
        public Bool32 PlaneReorderPossible;
        public Bool32 PersistentContent;
    }

    public struct DisplayModeParameters
    {
        public Extent2D VisibleRegion;
        public uint RefreshRate;
    }

    public struct DisplayModeProperties
    {
        public DisplayMode DisplayMode;
        public DisplayModeParameters Parameters;
    }

    public unsafe struct DisplayModeCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DisplayModeCreateFlags Flags;
        public DisplayModeParameters Parameters;
    }

    public struct DisplayPlaneCapabilities
    {
        public DisplayPlaneAlphaFlags SupportedAlpha;
        public Offset2D MinSrcPosition;
        public Offset2D MaxSrcPosition;
        public Extent2D MinSrcExtent;
        public Extent2D MaxSrcExtent;
        public Offset2D MinDstPosition;
        public Offset2D MaxDstPosition;
        public Extent2D MinDstExtent;
        public Extent2D MaxDstExtent;
    }

    public struct DisplayPlaneProperties
    {
        public Display CurrentDisplay;
        public uint CurrentStackIndex;
    }

    public unsafe struct DisplaySurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DisplaySurfaceCreateFlags Flags;
        public DisplayMode DisplayMode;
        public uint PlaneIndex;
        public uint PlaneStackIndex;
        public SurfaceTransformFlags Transform;
        public float GlobalAlpha;
        public DisplayPlaneAlphaFlags AlphaMode;
        public Extent2D ImageExtent;
    }

    public unsafe struct DisplayPresentInfo
    {
        public StructureType Type;
        public void* Next;
        public Rect2D SrcRect;
        public Rect2D DstRect;
        public Bool32 Persistent;
    }

    //
    // KHR Platforms
    //
    public unsafe struct XlibSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public XlibSurfaceCreateFlags Flags;
        public IntPtr Dpy;
        public IntPtr Window;
    }

    public unsafe struct XcbSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public XcbSurfaceCreateFlags Flags;
        public IntPtr Connection;
        public IntPtr Window;
    }

    public unsafe struct WaylandSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public WaylandSurfaceCreateFlags Flags;
        public IntPtr Display;
        public IntPtr Surface;
    }

    public unsafe struct MirSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public MirSurfaceCreateFlags Flags;
        public IntPtr Connection;
        public IntPtr MirSurface;
    }

    public unsafe struct AndroidSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public AndroidSurfaceCreateFlags Flags;
        public IntPtr Window;
    }

    public unsafe struct Win32SurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public Win32SurfaceCreateFlags Flags;
        public IntPtr Hinstance;
        public IntPtr Hwnd;
    }

    //
    // KHR 2
    //
    public unsafe struct PhysicalDeviceFeatures2
    {
        public StructureType Type;
        public void* Next;
        public PhysicalDeviceFeatures Features;
    }

    public unsafe struct PhysicalDeviceProperties2
    {
        public StructureType Type;
        public void* Next;
        public PhysicalDeviceProperties Properties;
    }

    public unsafe struct FormatProperties2
    {
        public StructureType Type;
        public void* Next;
        public FormatProperties FormatProperties;
    }

    public unsafe struct ImageFormatProperties2
    {
        public StructureType Type;
        public void* Next;
        public ImageFormatProperties ImageFormatProperties;
    }

    public unsafe struct PhysicalDeviceImageFormatInfo2
    {
        public StructureType Type;
        public void* Next;
        public Format Format;
        public ImageType ImageType;
        public ImageTiling Tiling;
        public ImageUsageFlags Usage;
        public ImageCreateFlags Flags;
    }

    public unsafe struct QueueFamilyProperties2
    {
        public StructureType Type;
        public void* Next;
        public QueueFamilyProperties QueueFamilyProperties;
    }

    public unsafe struct PhysicalDeviceMemoryProperties2
    {
        public StructureType Type;
        public void* Next;
        public PhysicalDeviceMemoryProperties MemoryProperties;
    }

    public unsafe struct SparseImageFormatProperties2
    {
        public StructureType Type;
        public void* Next;
        public SparseImageFormatProperties Properties;
    }

    public unsafe struct PhysicalDeviceSparseImageFormatInfo2
    {
        public StructureType Type;
        public void* Next;
        public Format Format;
        public ImageType ImageType;
        public SampleCountFlags Samples;
        public ImageUsageFlags Usage;
        public ImageTiling Tiling;
    }

    //
    // KHR memory
    //
    public struct ExternalMemoryProperties
    {
        public ExternalMemoryFeatureFlags ExternalMemoryFeatures;
        public ExternalMemoryHandleTypeFlags ExportFomImportedHandleTypes;
        public ExternalMemoryHandleTypeFlags CompatibleHandleTypes;
    }

    public unsafe struct PhysicalDeviceExternalImageFormatInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    public unsafe struct ExternalImageFormatProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryProperties ExternalMemoryProperties;
    }

    public unsafe struct PhysicalDeviceExternalBufferInfo
    {
        public StructureType Type;
        public void* Next;
        public BufferCreateFlags Flags;
        public BufferUsageFlags Usage;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    public unsafe struct ExternalBufferProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryProperties ExternalMemoryProperties;
    }

    public unsafe struct PhysicalDeviceIDProperties
    {
        public StructureType Type;
        public void* Next;
        public fixed byte DeivceUUID[(int)Vulkan.UuidSize];
        public fixed byte DriverUUID[(int)Vulkan.UuidSize];
        public fixed byte DeviceLUID[Vulkan.LuidSize];
        public uint DeviceNodeMask;
        public Bool32 DeviceLUIDValid;
    }

    public unsafe struct ExternalMemoryImageCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleTypes;
    }

    public unsafe struct ExternalMemoryBufferCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleTypes;
    }

    public unsafe struct ExportMemoryAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleTypes;
    }

    public unsafe struct ImportMemoryWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    public unsafe struct ExportMemoryWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    public unsafe struct MemoryWin32HandleProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MemoryTypeBits;
    }

    public unsafe struct MemoryGetWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceMemory Memory;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    public unsafe struct ImportMemoryFdInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleType;
        public int Fd;
    }

    public unsafe struct MemoryFdProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MemoryTypeBits;
    }

    public unsafe struct MemoryGetFdInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceMemory Memory;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    public unsafe struct Win32KeyedMutexAcquireReleaseInfo
    {
        public StructureType Type;
        public void* Next;
        public uint AcquireCount;
        public DeviceMemory* AcquireSyncs;
        public ulong* AcquireKeys;
        public uint* AcquireTimeouts;
        public uint ReleaseCount;
        public DeviceMemory* ReleaseSyncs;
        public ulong* ReleaseKeys;
    }

    //
    // KHR semaphore
    //
    public unsafe struct PhysicalDeviceExternalSemaphoreInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalSemaphoreHandleTypeFlags HandleType;
    }

    public unsafe struct ExternalSemaphoreProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalSemaphoreHandleTypeFlags ExportFromImportedHandleTypes;
        public ExternalSemaphoreHandleTypeFlags CompatibleHandleTypes;
        public ExternalSemaphoreFeatureFlags ExternalSemaphoreFeatures;
    }

    public unsafe struct ExportSemaphoreCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalSemaphoreHandleTypeFlags HandleTypes;
    }

    public unsafe struct ImportSemaphoreWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public Semaphore Semaphore;
        public SemaphoreImportFlags Flags;
        public ExternalSemaphoreHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    public unsafe struct ExportSemaphoreWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    public unsafe struct D3D12FenceSubmitInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreValuesCount;
        public ulong* WaitSemaphoreValues;
        public uint SignalSemaphoreValuesCount;
        public ulong* SignalSemaphoreValues;
    }

    public unsafe struct SemaphoreGetWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public Semaphore Semaphore;
        public ExternalSemaphoreHandleTypeFlags HandleType;
    }

    public unsafe struct ImportSemaphoreFdInfo
    {
        public StructureType Type;
        public void* Next;
        public Semaphore Semaphore;
        public SemaphoreImportFlags Flags;
        public ExternalSemaphoreHandleTypeFlags HandleType;
        public int Fd;
    }

    public unsafe struct SemaphoreGetFdInfo
    {
        public StructureType Type;
        public void* Next;
        public Semaphore Semaphore;
        public ExternalSemaphoreHandleTypeFlags HandleType;
    }

    //
    // KHR misc
    //
    public unsafe struct PhysicalDevicePushDescriptorProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MaxPushDescriptors;
    }

    public unsafe struct PhysicalDevice16BitStorageFeatures
    {
        public StructureType Type;
        public void* Next;
        public Bool32 StorageBuffer16BitAccess;
        public Bool32 UniformAndStorageBuffer16BitAccess;
        public Bool32 StoragePushConstant16;
        public Bool32 StorageInputOutput16;
    }

    public struct RectLayer
    {
        public Offset2D Offset;
        public Extent2D Extent;
        public uint Layer;
    }

    public unsafe struct PresentRegion
    {
        public uint RectangleCount;
        public RectLayer* Rectangles;
    }

    public unsafe struct PresentRegions
    {
        public StructureType Type;
        public void* Next;
        public uint SwapchainCount;
        public PresentRegion* Regions;
    }

    public struct DescriptorUpdateTemplateEntry
    {
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
        public DescriptorType DescriptorType;
        public Size Offset;
        public Size Stride;
    }

    public unsafe struct DescriptorUpdateTemplateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorUpdateTemplateCreateFlags Flags;
        public uint DescriptorUpdateEntryCount;
        public DescriptorUpdateTemplateEntry* DescriptorUpdateEntries;
        public DescriptorUpdateTemplateType TemplateType;
        public DescriptorSetLayout DescriptorSetLayout;
        public PipelineBindPoint PipelineBindPoint;
        public PipelineLayout PipelineLayout;
        public uint Set;
    }

    public unsafe struct SharedPresentSurfaceCapabilities
    {
        public StructureType Type;
        public void* Next;
        public ImageUsageFlags SharedPresentSupportedUsageFlags;
    }

    //
    // KHR fence
    //
    public unsafe struct PhysicalDeviceExternalFenceInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalFenceHandleTypeFlags HandleType;
    }

    public unsafe struct ExternalFenceProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalFenceHandleTypeFlags ExportFromImportedHandleTypes;
        public ExternalFenceHandleTypeFlags CompatibleHandleTypes;
        public ExternalFenceFeatureFlags ExternalFenceFeatures;
    }

    public unsafe struct ExportFenceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalFenceHandleTypeFlags HandleTypes;
    }

    public unsafe struct ImportFenceWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public Fence Fence;
        public FenceImportFlags Flags;
        public ExternalFenceHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    public unsafe struct ExportFenceWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    public unsafe struct FenceGetWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public Fence Fence;
        public ExternalFenceHandleTypeFlags HandleType;
    }

    public unsafe struct ImportFenceFdInfo
    {
        public StructureType Type;
        public void* Next;
        public Fence Fence;
        public FenceImportFlags Flags;
        public ExternalFenceHandleTypeFlags HandleType;
        public int Fd;
    }

    public unsafe struct FenceGetFdInfo
    {
        public StructureType Type;
        public void* Next;
        public Fence Fence;
        public ExternalFenceHandleTypeFlags HandleType;
    }

    //
    // KHR 2
    //
    public unsafe struct PhysicalDeviceSurfaceInfo2
    {
        public StructureType Type;
        public void* Next;
        public Surface Surface;
    }

    public unsafe struct SurfaceCapabilities2
    {
        public StructureType Type;
        public void* Next;
        public SurfaceCapabilities SurfaceCapabilities;
    }

    public unsafe struct SurfaceFormat2
    {
        public StructureType Type;
        public void* Next;
        public SurfaceFormat SurfaceFormat;
    }

    public unsafe struct PhysicalDeviceVariablePointerFeatures
    {
        public StructureType Type;
        public void* Next;
        public Bool32 VariablePointersStorageBuffer;
        public Bool32 VariablePointers;
    }

    public unsafe struct MemoryDedicatedRequirements
    {
        public StructureType Type;
        public void* Next;
        public Bool32 PrefersDedicatedAllocation;
        public Bool32 RequiresDedicatedAllocation;
    }

    public unsafe struct MemoryDedicatedAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public Image image;
        public Buffer Buffer;
    }

    public unsafe struct BufferMemoryRequirementsInfo2
    {
        public StructureType Type;
        public void* Next;
        public Buffer Buffer;
    }

    public unsafe struct ImageMemoryRequirementsInfo2
    {
        public StructureType Type;
        public void* Next;
        public Image Image;
    }

    public unsafe struct ImageSparseMemoryRequirementsInfo2
    {
        public StructureType Type;
        public void* Next;
        public Image Image;
    }

    public unsafe struct MemoryRequirements2
    {
        public StructureType Type;
        public void* Next;
        public MemoryRequirements MemoryRequirements;
    }

    public unsafe struct SparseImageMemoryRequirements2
    {
        public StructureType Type;
        public void* Next;
        public SparseImageMemoryRequirements MemoryRequirements;
    }

    //
    // EXT
    //
    public unsafe struct DebugReportCallbackCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DebugReportFlags Flags;
        public DebugReportCallbackDelegate Callback;
        public void* UserData;
    }
}