using System;
using System.Runtime.InteropServices;
using Engine;

namespace Vulkan
{
    public static partial class Vk
    {
        public struct Bool32 : IEquatable<Bool32>
        {
            private readonly int _value;

            public Bool32(bool value) => _value = value ? 1 : 0;

            public Bool32(int value) => _value = value;

            public bool Equals(Bool32 other) => _value == other._value;

            public override bool Equals(object obj) => obj is Bool32 && Equals((Bool32)obj);

            public override int GetHashCode() => _value.GetHashCode();

            public static bool operator ==(Bool32 left, Bool32 right) => left.Equals(right);

            public static bool operator !=(Bool32 left, Bool32 right) => !left.Equals(right);

            public static implicit operator bool(Bool32 value) => value._value != 0;

            public static implicit operator Bool32(bool value) => new Bool32(value);

            public static implicit operator int(Bool32 value) => value._value;

            public static implicit operator Bool32(int value) => new Bool32(value);

            public override string ToString() => ((bool)this).ToString();
        }

        public struct DeviceSize : IEquatable<DeviceSize>
        {
            private readonly ulong _value;

            public DeviceSize(ulong value) => _value = value;

            public bool Equals(DeviceSize other) => _value == other._value;

            public override bool Equals(object obj) => obj is DeviceSize && Equals((DeviceSize)obj);

            public override int GetHashCode() => _value.GetHashCode();

            public static bool operator ==(DeviceSize left, DeviceSize right) => left.Equals(right);

            public static bool operator !=(DeviceSize left, DeviceSize right) => !left.Equals(right);

            public override string ToString() => _value.ToString();

            public static implicit operator ulong(DeviceSize deviceSize) => deviceSize._value;

            public static implicit operator DeviceSize(ulong value) => new DeviceSize(value);
        }

        public struct SampleMask : IEquatable<SampleMask>
        {
            private uint _value;

            public SampleMask(uint value) => _value = value;

            public bool Equals(SampleMask other) => _value == other._value;

            public override bool Equals(object obj) => obj is SampleMask && Equals((SampleMask)obj);

            public override int GetHashCode() => _value.GetHashCode();

            public static bool operator ==(SampleMask left, SampleMask right) => left.Equals(right);

            public static bool operator !=(SampleMask left, SampleMask right) => !left.Equals(right);

            public override string ToString() => _value.ToString();

            public static implicit operator uint(SampleMask SampleMask) => SampleMask._value;

            public static implicit operator SampleMask(uint value) => new SampleMask(value);
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ApplicationInfo
        {
            public StructureType Type;
            public void* Next;
            public Text ApplicationName;
            public Version ApplicationVersion;
            public Text EngineName;
            public Version EngineVersion;
            public Version ApiVersion;

            public ApplicationInfo(Text applicationName, Version applicationVersion, Text engineName, Version engineVersion, Version apiVersion)
            {
                Type = StructureType.ApplicationInfo;
                Next = null;
                ApplicationName = applicationName;
                ApplicationVersion = applicationVersion;
                EngineName = engineName;
                EngineVersion = engineVersion;
                ApiVersion = apiVersion;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
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

            public InstanceCreateInfo(ApplicationInfo* applicationInfo, Text[] extensionNames)
            {
                Type = StructureType.InstanceCreateInfo;
                Next = null;
                Flags = InstanceCreateFlags.None;
                ApplicationInfo = applicationInfo;
                EnabledLayerCount = 0;
                EnabledLayerNames = null;
                EnabledExtensionCount = (uint)extensionNames.Length;
                fixed (Text* ptr = &extensionNames[0])
                {
                    EnabledExtensionNames = ptr;
                }
            }

            public InstanceCreateInfo(ApplicationInfo* applicationInfo, Text[] layerNames, Text[] extensionNames)
            {
                Type = StructureType.InstanceCreateInfo;
                Next = null;
                Flags = InstanceCreateFlags.None;
                ApplicationInfo = applicationInfo;
                if (layerNames == null)
                {
                    EnabledLayerCount = 0;
                    EnabledLayerNames = null;
                }
                else
                {
                    EnabledLayerCount = (uint)layerNames.Length;
                    fixed (Text* ptr = &layerNames[0])
                    {
                        EnabledLayerNames = ptr;
                    }
                }
                EnabledExtensionCount = (uint)extensionNames.Length;
                fixed (Text* ptr = &extensionNames[0])
                {
                    EnabledExtensionNames = ptr;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AllocationCallbacks
        {
            public IntPtr UserData;
            public IntPtr Allocation; // AllocationFunction
            public IntPtr Reallocation; // ReallocationFunction
            public IntPtr Free; // FreeFunction
            public IntPtr InternalAllocation; // InternalAllocationNotification
            public IntPtr InternalFree; // InternalFreeNotification

            public static AllocationCallbacks Null = new AllocationCallbacks();
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceFeatures
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct FormatProperties
        {
            public FormatFeatureFlags LinearTilingFeatures;
            public FormatFeatureFlags OptimalTilingFeatures;
            public FormatFeatureFlags BufferFeatures;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Extent3D
        {
            public uint Width;
            public uint Height;
            public uint Depth;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageFormatProperties
        {
            public Extent3D MaxExtents;
            public uint MaxMipLevels;
            public uint MaxArrayLayers;
            public SampleCountFlags SampleCounts;
            public DeviceSize MaxResourceSize;
        }

        [StructLayout(LayoutKind.Sequential)]
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
            public uint MaxPushantsSize;
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceSparseProperties
        {
            public Bool32 ResidencyStandard2DBlockShape;
            public Bool32 ResidencyStandard2DMultisampleBlockShape;
            public Bool32 ResidencyStandard3DBlockShape;
            public Bool32 ResidencyAlignedMipSize;
            public Bool32 ResidencyNonResidentStrict;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceProperties
        {
            public uint ApiVersion;
            public uint DriverVersion;
            public uint VendorId;
            public uint DeviceId;
            public PhysicalDeviceType DeviceType;
            public fixed byte DeviceName[(int)MaxPhysicalDeviceNameSize];
            public fixed byte PipelineCacheUuid[(int)UUIDSize];
            public PhysicalDeviceLimits Limits;
            public PhysicalDeviceSparseProperties SparseProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct QueueFamilyProperties
        {
            public QueueFlags QueueFlags;
            public uint QueueCount;
            public uint TimestampValidBits;
            public Extent3D MinImageTransferGranularity;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryType
        {
            public MemoryPropertyFlags PropertyFlags;
            public uint HeapIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryHeap
        {
            public DeviceSize Size;
            public MemoryHeapFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceMemoryProperties
        {
            public uint MemoryTypeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxMemoryTypes)]
            public MemoryType[] MemoryTypes;
            public uint MemoryHeapCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxMemoryHeaps)]
            public MemoryHeap[] MemoryHeaps;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceQueueCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public DeviceQueueCreateFlags Flags;
            public uint QueueFamilyIndex;
            public uint QueueCount;
            public float* QueuePriorities;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExtensionProperties
        {
            public ExtensionName ExtensionName;
            public Version SpecVersion;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct LayerProperties
        {
            public ExtensionName LayerName;
            public Version SpecVersion;
            public Version ImplementationVersion;
            public fixed byte Description[MaxDescriptionSize];
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryAllocateInfo
        {
            public StructureType Type;
            public void* Next;
            public DeviceSize AllocationSize;
            public uint MemoryTypeIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MappedMemoryRange
        {
            public StructureType Type;
            public void* Next;
            public DeviceMemory Memory;
            public DeviceSize Offset;
            public DeviceSize Size;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryRequirements
        {
            public DeviceSize Size;
            public DeviceSize Alignment;
            public uint MemoryTypeBits;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseImageFormatProperties
        {
            public ImageAspectFlags AspectMask;
            public Extent3D ImageGranularity;
            public SparseImageFormatFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseImageMemoryRequirements
        {
            public SparseImageFormatProperties FormatProperties;
            public uint ImageMipTailFirstLod;
            public DeviceSize ImageMipTailSize;
            public DeviceSize ImageMipTailOffset;
            public DeviceSize ImageMipTailStride;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseMemoryBind
        {
            public DeviceSize ResourceOffset;
            public DeviceSize Size;
            public DeviceMemory Memory;
            public DeviceSize MemoryOffset;
            public SparseMemoryBindFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseBufferMemoryBindInfo
        {
            public Buffer Buffer;
            public uint BindCount;
            public SparseMemoryBind* Binds;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseImageOpaqueMemoryBindInfo
        {
            public Image Image;
            public uint BindCount;
            public SparseMemoryBind* Binds;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageSubresource
        {
            public ImageAspectFlags AspectMask;
            public uint MipLevel;
            public uint ArrayLayer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Offset3D
        {
            public int X;
            public int Y;
            public int Z;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseImageMemoryBind
        {
            public ImageSubresource Subresource;
            public Offset3D Offset;
            public Extent3D Extent;
            public DeviceMemory Memory;
            public DeviceSize MemoryOffset;
            public SparseMemoryBindFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseImageMemoryBindInfo
        {
            public Image Image;
            public uint BindCount;
            public SparseImageMemoryBind* Binds;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct FenceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public FenceCreateFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SemaphoreCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public SemaphoreCreateFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct EventCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public EventCreateFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct QueryPoolCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public QueryPoolCreateFlags Flags;
            public QueryType QueryType;
            public uint QueryCount;
            public QueryPipelineStatisticFlags PipelineStatistics;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SubresourceLayout
        {
            public DeviceSize Offset;
            public DeviceSize Size;
            public DeviceSize RowPitch;
            public DeviceSize ArrayPitch;
            public DeviceSize DepthPitch;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ComponentMapping
        {
            public ComponentSwizzle R;
            public ComponentSwizzle G;
            public ComponentSwizzle B;
            public ComponentSwizzle A;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageSubresourceRange
        {
            public ImageAspectFlags AspectMask;
            public uint BaseMipLevel;
            public uint LevelCount;
            public uint BaseArrayLayer;
            public uint LayerCount;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ShaderModuleCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public ShaderModuleCreateFlags Flags;
            public Size CodeSize;
            public uint* Code;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineCacheCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineCacheCreateFlags Flags;
            public Size InitialDataSize;
            public void* InitialData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SpecializationMapEntry
        {
            public uint antId;
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct VertexInputBindingDescription
        {
            public uint Binding;
            public uint Stride;
            public VertexInputRate InputRate;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct VertexInputAttributeDescription
        {
            public uint Location;
            public uint Binding;
            public Format Format;
            public uint Offset;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineInputAssemblyStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineInputAssemblyStateCreateFlags Flags;
            public PrimitiveTopology Topology;
            public Bool32 PrimitiveRestartEnable;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineTessellationStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineTessellationStateCreateFlags Flags;
            public uint PatchControlPoints;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Viewport
        {
            public float X;
            public float Y;
            public float Width;
            public float Height;
            public float MinDepth;
            public float MaxDepth;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Offset2D
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Extent2D
        {
            public uint Width;
            public uint Height;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Rect2D
        {
            public Offset2D Offset;
            public Extent2D Extent;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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
            public float DepthBiasantFactor;
            public float DepthBiasClamp;
            public float DepthBiasSlopeFactor;
            public float LineWidth;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct StencilOpState
        {
            public StencilOp FailOp;
            public StencilOp PassOp;
            public StencilOp DepthFailOp;
            public CompareOp CompareOp;
            public uint CompareMask;
            public uint WriteMask;
            public uint Reference;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineColorBlendAttachmentState
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineColorBlendStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineColorBlendStateCreateFlags Flags;
            public Bool32 LogicOpEnable;
            public LogicOp LogicOp;
            public uint AttachmentCount;
            public PipelineColorBlendAttachmentState* Attachments;
            public fixed float Blendants[4];
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PushantRange
        {
            public ShaderStageFlags StageFlags;
            public uint Offset;
            public uint Size;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineLayoutCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineLayoutCreateFlags Flags;
            public uint SetLayoutCount;
            public DescriptorSetLayout* SetLayouts;
            public uint PushantRangeCount;
            public PushantRange* PushantRanges;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorSetLayoutBinding
        {
            public uint Binding;
            public DescriptorType DescriptorType;
            public uint DescriptorCount;
            public ShaderStageFlags StageFlags;
            public Sampler* ImmutableSamplers;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorSetLayoutCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public DescriptorSetLayoutCreateFlags Flags;
            public uint BindingCount;
            public DescriptorSetLayoutBinding* Bindings;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorPoolSize
        {
            public DescriptorType Type;
            public uint DescriptorCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorPoolCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public DescriptorPoolCreateFlags Flags;
            public uint MaxSets;
            public uint PoolSizeCount;
            public DescriptorPoolSize* PoolSizes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorSetAllocateInfo
        {
            public StructureType Type;
            public void* Next;
            public DescriptorPool DescriptorPool;
            public uint DescriptorSetCount;
            public DescriptorSetLayout* SetLayouts;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorImageInfo
        {
            public Sampler Sampler;
            public ImageView ImageView;
            public ImageLayout ImageLayout;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorBufferInfo
        {
            public Buffer Buffer;
            public DeviceSize Offset;
            public DeviceSize Range;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AttachmentDescription
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AttachmentReference
        {
            public uint Attachment;
            public ImageLayout Layout;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SubpassDependency
        {
            public uint SrcSubpass;
            public uint DstSubpass;
            public PipelineStageFlags SrcStageMask;
            public PipelineStageFlags DstStageMask;
            public AccessFlags SrcAccessMask;
            public AccessFlags DstAccessMask;
            public DependencyFlags DependencyFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct CommandPoolCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public CommandPoolCreateFlags Flags;
            public uint QueueFamilyIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct CommandBufferAllocateInfo
        {
            public StructureType Type;
            public void* Next;
            public CommandPool CommandPool;
            public CommandBufferLevel Level;
            public uint CommandBufferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct CommandBufferBeginInfo
        {
            public StructureType Type;
            public void* Next;
            public CommandBufferUsageFlags Flags;
            public CommandBufferInheritanceInfo* InheritanceInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct BufferCopy
        {
            public DeviceSize SrcOffset;
            public DeviceSize DstOffset;
            public DeviceSize Size;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageSubresourceLayers
        {
            public ImageAspectFlags AspectMask;
            public uint MipLevel;
            public uint BaseArrayLayer;
            public uint LayerCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageCopy
        {
            public ImageSubresourceLayers SrcSubresource;
            public Offset3D SrcOffset;
            public ImageSubresourceLayers DstSubresource;
            public Offset3D DstOffset;
            public Extent3D Extent;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageBlit
        {
            public ImageSubresourceLayers SrcSubresource;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Offset3D[] SrcOffsets;
            public ImageSubresourceLayers DstSubresource;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Offset3D[] DstOffsets;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct BufferImageCopy
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ClearDepthStencilValue
        {
            public float Depth;
            public uint Stencil;
        }

        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct ClearValue
        {
            [FieldOffset(0)] public ClearColorValue Color;
            [FieldOffset(0)] public ClearDepthStencilValue DepthStencil;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ClearAttachment
        {
            public ImageAspectFlags AspectMask;
            public uint ColorAttachment;
            public ClearValue ClearValue;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ClearRect
        {
            public Rect2D Rect;
            public uint BaseArrayLayer;
            public uint LayerCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageResolve
        {
            public ImageSubresourceLayers SrcSubresource;
            public Offset3D SrcOffset;
            public ImageSubresourceLayers DstSubresource;
            public Offset3D DstOffset;
            public Extent3D Extent;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryBarrier
        {
            public StructureType Type;
            public void* Next;
            public AccessFlags SrcAccessMask;
            public AccessFlags DstAccessMask;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DispatchIndirectCommand
        {
            public uint X;
            public uint Y;
            public uint Z;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DrawIndexedIndirectCommand
        {
            public uint IndexCount;
            public uint InstanceCount;
            public uint FirstIndex;
            public int VertexOffset;
            public uint FirstInstance;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DrawIndirectCommand
        {
            public uint VertexCount;
            public uint InstanceCount;
            public uint FirstVertex;
            public uint FirstInstance;
        }

        //
        // KHR surface
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SurfaceCapabilities
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SurfaceFormat
        {
            public Format Format;
            public ColorSpace ColorSpace;
        }

        //
        // KHR Swapchain
        //
        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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
        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DisplayModeParameters
        {
            public Extent2D VisibleRegion;
            public uint RefreshRate;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DisplayModeProperties
        {
            public DisplayMode DisplayMode;
            public DisplayModeParameters Parameters;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DisplayModeCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public DisplayModeCreateFlags Flags;
            public DisplayModeParameters Parameters;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DisplayPlaneCapabilities
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DisplayPlaneProperties
        {
            public Display CurrentDisplay;
            public uint CurrentStackIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
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
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct XlibSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public XlibSurfaceCreateFlags Flags;
            public IntPtr Dpy;
            public IntPtr Window;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct XcbSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public XcbSurfaceCreateFlags Flags;
            public IntPtr Connection;
            public IntPtr Window;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct WaylandSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public WaylandSurfaceCreateFlags Flags;
            public IntPtr Display;
            public IntPtr Surface;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MirSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public MirSurfaceCreateFlags Flags;
            public IntPtr Connection;
            public IntPtr MirSurface;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AndroidSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public AndroidSurfaceCreateFlags Flags;
            public IntPtr Window;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Win32SurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public Win32SurfaceCreateFlags Flags;
            public IntPtr Hinstance;
            public IntPtr Hwnd;

            public Win32SurfaceCreateInfo(IntPtr hinstance, IntPtr hwnd)
            {
                Type = StructureType.Win32SurfaceCreateInfo;
                Next = null;
                Flags = Win32SurfaceCreateFlags.None;
                Hinstance = hinstance;
                Hwnd = hwnd;
            }
        }

        //
        // KHR 2
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceFeatures2
        {
            public StructureType Type;
            public void* Next;
            public PhysicalDeviceFeatures Features;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceProperties2
        {
            public StructureType Type;
            public void* Next;
            public PhysicalDeviceProperties Properties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct FormatProperties2
        {
            public StructureType Type;
            public void* Next;
            public FormatProperties FormatProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageFormatProperties2
        {
            public StructureType Type;
            public void* Next;
            public ImageFormatProperties ImageFormatProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct QueueFamilyProperties2
        {
            public StructureType Type;
            public void* Next;
            public QueueFamilyProperties QueueFamilyProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceMemoryProperties2
        {
            public StructureType Type;
            public void* Next;
            public PhysicalDeviceMemoryProperties MemoryProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseImageFormatProperties2
        {
            public StructureType Type;
            public void* Next;
            public SparseImageFormatProperties Properties;
        }

        [StructLayout(LayoutKind.Sequential)]
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
        // KHR Memory
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalMemoryProperties
        {
            public ExternalMemoryFeatureFlags ExternalMemoryFeatures;
            public ExternalMemoryHandleTypeFlags ExportFomImportedHandleTypes;
            public ExternalMemoryHandleTypeFlags CompatibleHandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceExternalImageFormatInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalImageFormatProperties
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryProperties ExternalMemoryProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceExternalBufferInfo
        {
            public StructureType Type;
            public void* Next;
            public BufferCreateFlags Flags;
            public BufferUsageFlags Usage;
            public ExternalMemoryHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalBufferProperties
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryProperties ExternalMemoryProperties;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceIDProperties
        {
            public StructureType Type;
            public void* Next;
            public fixed byte DeivceUUID[(int)UUIDSize];
            public fixed byte DriverUUID[(int)UUIDSize];
            public fixed byte DeviceLUID[LUIDSize];
            public uint DeviceNodeMask;
            public Bool32 DeviceLUIDValid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalMemoryImageCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlags HandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalMemoryBufferCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlags HandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportMemoryAllocateInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlags HandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImportMemoryWin32HandleInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlags HandleType;
            public IntPtr Handle;
            public char* Name;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportMemoryWin32HandleInfo
        {
            public StructureType Type;
            public void* Next;
            public IntPtr Attributes;
            public IntPtr DwAccess;
            public char* Name;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryWin32HandleProperties
        {
            public StructureType Type;
            public void* Next;
            public uint MemoryTypeBits;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryGetWin32HandleInfo
        {
            public StructureType Type;
            public void* Next;
            public DeviceMemory Memory;
            public ExternalMemoryHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImportMemoryFdInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlags HandleType;
            public int Fd;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryFdProperties
        {
            public StructureType Type;
            public void* Next;
            public uint MemoryTypeBits;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryGetFdInfo
        {
            public StructureType Type;
            public void* Next;
            public DeviceMemory Memory;
            public ExternalMemoryHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
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
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceExternalSemaphoreInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalSemaphoreHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalSemaphoreProperties
        {
            public StructureType Type;
            public void* Next;
            public ExternalSemaphoreHandleTypeFlags ExportFromImportedHandleTypes;
            public ExternalSemaphoreHandleTypeFlags CompatibleHandleTypes;
            public ExternalSemaphoreFeatureFlags ExternalSemaphoreFeatures;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportSemaphoreCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalSemaphoreHandleTypeFlags HandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportSemaphoreWin32HandleInfo
        {
            public StructureType Type;
            public void* Next;
            public IntPtr Attributes;
            public IntPtr DwAccess;
            public char* Name;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct D3D12FenceSubmitInfo
        {
            public StructureType Type;
            public void* Next;
            public uint WaitSemaphoreValuesCount;
            public ulong* WaitSemaphoreValues;
            public uint SignalSemaphoreValuesCount;
            public ulong* SignalSemaphoreValues;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SemaphoreGetWin32HandleInfo
        {
            public StructureType Type;
            public void* Next;
            public Semaphore Semaphore;
            public ExternalSemaphoreHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImportSemaphoreFdInfo
        {
            public StructureType Type;
            public void* Next;
            public Semaphore Semaphore;
            public SemaphoreImportFlags Flags;
            public ExternalSemaphoreHandleTypeFlags HandleType;
            public int Fd;
        }

        [StructLayout(LayoutKind.Sequential)]
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
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDevicePushDescriptorProperties
        {
            public StructureType Type;
            public void* Next;
            public uint MaxPushDescriptors;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDevice16BitStorageFeatures
        {
            public StructureType Type;
            public void* Next;
            public Bool32 StorageBuffer16BitAccess;
            public Bool32 UniformAndStorageBuffer16BitAccess;
            public Bool32 StoragePushant16;
            public Bool32 StorageInputOutput16;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct RectLayer
        {
            public Offset2D Offset;
            public Extent2D Extent;
            public uint Layer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PresentRegion
        {
            public uint RectangleCount;
            public RectLayer* Rectangles;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PresentRegions
        {
            public StructureType Type;
            public void* Next;
            public uint SwapchainCount;
            public PresentRegion* Regions;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DescriptorUpdateTemplateEntry
        {
            public uint DstBinding;
            public uint DstArrayElement;
            public uint DescriptorCount;
            public DescriptorType DescriptorType;
            public Size Offset;
            public Size Stride;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SharedPresentSurfaceCapabilities
        {
            public StructureType Type;
            public void* Next;
            public ImageUsageFlags SharedPresentSupportedUsageFlags;
        }

        //
        // KHR fence
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceExternalFenceInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalFenceHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalFenceProperties
        {
            public StructureType Type;
            public void* Next;
            public ExternalFenceHandleTypeFlags ExportFromImportedHandleTypes;
            public ExternalFenceHandleTypeFlags CompatibleHandleTypes;
            public ExternalFenceFeatureFlags ExternalFenceFeatures;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportFenceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public ExternalFenceHandleTypeFlags HandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
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

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportFenceWin32HandleInfo
        {
            public StructureType Type;
            public void* Next;
            public IntPtr Attributes;
            public IntPtr DwAccess;
            public char* Name;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct FenceGetWin32HandleInfo
        {
            public StructureType Type;
            public void* Next;
            public Fence Fence;
            public ExternalFenceHandleTypeFlags HandleType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImportFenceFdInfo
        {
            public StructureType Type;
            public void* Next;
            public Fence Fence;
            public FenceImportFlags Flags;
            public ExternalFenceHandleTypeFlags HandleType;
            public int Fd;
        }

        [StructLayout(LayoutKind.Sequential)]
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
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceSurfaceInfo2
        {
            public StructureType Type;
            public void* Next;
            public Surface Surface;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SurfaceCapabilities2
        {
            public StructureType Type;
            public void* Next;
            public SurfaceCapabilities SurfaceCapabilities;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SurfaceFormat2
        {
            public StructureType Type;
            public void* Next;
            public SurfaceFormat SurfaceFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceVariablePointerFeatures
        {
            public StructureType Type;
            public void* Next;
            public Bool32 VariablePointersStorageBuffer;
            public Bool32 VariablePointers;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryDedicatedRequirements
        {
            public StructureType Type;
            public void* Next;
            public Bool32 PrefersDedicatedAllocation;
            public Bool32 RequiresDedicatedAllocation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryDedicatedAllocateInfo
        {
            public StructureType Type;
            public void* Next;
            public Image Image;
            public Buffer Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct BufferMemoryRequirementsInfo2
        {
            public StructureType Type;
            public void* Next;
            public Buffer Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageMemoryRequirementsInfo2
        {
            public StructureType Type;
            public void* Next;
            public Image Image;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageSparseMemoryRequirementsInfo2
        {
            public StructureType Type;
            public void* Next;
            public Image Image;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryRequirements2
        {
            public StructureType Type;
            public void* Next;
            public MemoryRequirements MemoryRequirements;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SparseImageMemoryRequirements2
        {
            public StructureType Type;
            public void* Next;
            public SparseImageMemoryRequirements MemoryRequirements;
        }

        //
        // EXT
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DebugReportCallbackCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public DebugReportFlags Flags;
            public DebugReportCallback Callback;
            public void* UserData;

            public DebugReportCallbackCreateInfo(DebugReportFlags flags, DebugReportCallback callback)
            {
                Type = StructureType.DebugReportCallbackCreateInfo;
                Next = null;
                Flags = flags;
                Callback = callback;
                UserData = null;
            }
        }

        //
        // AMD
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineRasterizationStateRasterizationOrder
        {
            public StructureType Type;
            public void* Next;
            public RasterizationOrder RasterizationOrder;
        }

        //
        // EXT
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DebugMarkerObjectNameInfo
        {
            public StructureType Type;
            public void* Next;
            public DebugReportObjectType ObjectType;
            public ulong Object;
            public Text ObjectName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DebugMarkerObjectTagInfo
        {
            public StructureType Type;
            public void* Next;
            public DebugReportObjectType ObjectType;
            public ulong Object;
            public ulong Tagname;
            public Size TagSize;
            public void* Tag;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DebugMarkerMarkerInfo
        {
            public StructureType Type;
            public void* Next;
            public Text MarkerName;
            public fixed float Color[4];
        }

        //
        // AMD
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DedicatedAllocationImageCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public Bool32 DedicatedAllocation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DedicatedAllocationBufferCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public Bool32 DedicatedAllocation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DedicatedAllocationMemoryAllocateInfo
        {
            public StructureType Type;
            public void* Next;
            public Image Image;
            public Buffer Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct TextureLODGatherFormatProperties
        {
            public StructureType Type;
            public void* Next;
            public Bool32 SupportsTextureGatherLODBias;
        }

        //
        // KHX
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct RenderPassMultiviewCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public uint SubpassCount;
            public uint* ViewMasks;
            public uint DependencyCount;
            public uint* ViewOffsets;
            public uint CorrelationMaskCount;
            public uint* CorrelationMasks;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceMultiviewFeatures
        {
            public StructureType Type;
            public void* Next;
            public Bool32 Multiview;
            public Bool32 MultiviewGeometryShader;
            public Bool32 MultiviewTessellationShader;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceMultiviewProperties
        {
            public StructureType Type;
            public void* Next;
            public uint MaxMultiviewViewCount;
            public uint MaxMultiviewInstanceIndex;
        }

        //
        // NV
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalImageFormatPropertiesNV
        {
            public ImageFormatProperties ImageFormatProperties;
            public ExternalMemoryFeatureFlagsNV ExternalMemoryFeatures;
            public ExternalMemoryHandleTypeFlagsNV ExportFromImportedHandleTypes;
            public ExternalMemoryHandleTypeFlagsNV CompatibleHandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExternalMemoryImageCreateInfoNV
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlagsNV HandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportMemoryAllocateInfoNV
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlagsNV HandleTypes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImportMemoryWin32HandleInfoNV
        {
            public StructureType Type;
            public void* Next;
            public ExternalMemoryHandleTypeFlagsNV HandleType;
            public IntPtr Handle;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ExportMemoryWin32HandleInfoNV
        {
            public StructureType Type;
            public void* Next;
            public IntPtr Attributes;
            public IntPtr DwAccess;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct Win32KeyedMutexAcquireReleaseInfoNV
        {
            public StructureType Type;
            public void* Next;
            public uint AcquireCount;
            public DeviceMemory* AcquireSyncs;
            public ulong* AcquireKeys;
            public uint* AcquireTimeoutMilliseconds;
            public uint ReleaseCount;
            public DeviceMemory* ReleaseSyncs;
            public ulong* ReleaseKeys;
        }

        //
        // KHX
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MemoryAllocateFlagsInfo
        {
            public StructureType Type;
            public void* Next;
            public MemoryAllocateFlags Flags;
            public uint DeviceMask;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct BindBufferMemoryInfo
        {
            public StructureType Type;
            public void* Next;
            public Buffer Buffer;
            public DeviceMemory Memory;
            public DeviceSize MemoryOffset;
            public uint DeviceIndexCount;
            public uint* DeviceIndices;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct BindImageMemoryInfo
        {
            public StructureType Type;
            public void* Next;
            public Image Image;
            public DeviceMemory Memory;
            public DeviceSize MemoryOffset;
            public uint DeviceIndexCount;
            public uint* DeviceIndices;
            public uint SFRRectCount;
            public Rect2D* SFRRects;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupRenderPassBeginInfo
        {
            public StructureType Type;
            public void* Next;
            public uint DeviceMask;
            public uint DeviceRenderAreaCount;
            public Rect2D* DeviceRenderAreas;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupCommandBufferBeginInfo
        {
            public StructureType Type;
            public void* Next;
            public uint DeviceMask;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupSubmitInfo
        {
            public StructureType Type;
            public void* Next;
            public uint WaitSemaphoreCount;
            public uint* WaitSemaphoreDeviceIndices;
            public uint CommandBufferCount;
            public uint* CommandBufferDeviceMasks;
            public uint SignalSemaphoreCount;
            public uint* SignalSemaphoreDeviceIndices;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupBindSparseInfo
        {
            public StructureType Type;
            public void* Next;
            public uint ResourceDeviceIndex;
            public uint MemoryDeviceIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupPresentCapabilities
        {
            public StructureType Type;
            public void* Next;
            public fixed uint PresentMask[MaxDeviceGroupSize];
            public DeviceGroupPresentModeFlags Modes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ImageSwapchainCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public Swapchain Swapchain;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct BindImageMemorySwapchainInfo
        {
            public StructureType Type;
            public void* Next;
            public Swapchain Swapchain;
            public uint ImageIndex;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AcquireNextImageInfo
        {
            public StructureType Type;
            public void* Next;
            public Swapchain Swapchain;
            public ulong Timeout;
            public Semaphore Semaphore;
            public Fence Fence;
            public uint DeviceMask;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupPresentInfo
        {
            public StructureType Type;
            public void* Next;
            public uint SwapchainCount;
            public uint* DeviceMasks;
            public DeviceGroupPresentModeFlags Mode;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupSwapchainCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public DeviceGroupPresentModeFlags Modes;
        }

        //
        // EXT
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ValidationFlags
        {
            public StructureType Type;
            public void* Next;
            public uint DisabledValidationCheckCount;
            public ValidationCheck* DisabledValidationChecks;
        }

        //
        // NN
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ViSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public ViSurfaceCreateFlags Flags;
            public void* Window;
        }

        //
        // KHX
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceGroupProperties
        {
            public StructureType Type;
            public void* Next;
            public uint PhysicalDeviceCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MaxDeviceGroupSize)]
            public PhysicalDevice[] PhysicalDevices;
            public Bool32 SubsetAllocation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGroupDeviceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public uint PhysicalDeviceCount;
            public PhysicalDevice* PhysicalDevices;
        }

        //
        // NVX
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGeneratedCommandsFeatures
        {
            public StructureType Type;
            public void* Next;
            public Bool32 ComputeBindingPointSupport;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceGeneratedCommandsLimits
        {
            public StructureType Type;
            public void* Next;
            public uint MaxIndirectCommandsLayoutTokenCount;
            public uint MaxObjectEntryCounts;
            public uint MinSequenceCountBufferOffsetAlignment;
            public uint MinSequenceIndexBufferOffsetAlignment;
            public uint MinCommandsTokenBufferOffsetAlignment;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct IndirectCommandsToken
        {
            public IndirectCommandsTokenType TokenType;
            public Buffer Buffer;
            public DeviceSize Offset;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct IndirectCommandsLayoutToken
        {
            public IndirectCommandsTokenType TokenType;
            public uint BindingUnit;
            public uint DynamicCount;
            public uint Divisor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct IndirectCommandsLayoutCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineBindPoint PipelineBindPoint;
            public IndirectCommandsLayoutUsageFlags Flags;
            public uint TokenCount;
            public IndirectCommandsLayoutToken* Tokens;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct CmdProcessCommandsInfo
        {
            public StructureType Type;
            public void* Next;
            public ObjectTable ObjectTable;
            public IndirectCommandsLayout IndirectCommandsLayout;
            public uint IndirectCommandsTokenCount;
            public IndirectCommandsToken* IndirectCommandsTokens;
            public uint MaxSequencesCount;
            public CommandBuffer TargetCommandBuffer;
            public Buffer SequencesCountBuffer;
            public DeviceSize SequencesCountOffset;
            public Buffer SequencesIndexBuffer;
            public DeviceSize SequencesIndexOffset;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct CmdReserveSpaceForCommandsInfo
        {
            public StructureType Type;
            public void* Next;
            public ObjectTable ObjectTable;
            public IndirectCommandsLayout IndirectCommandsLayout;
            public uint MaxSequencesCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ObjectTableCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public uint ObjectCount;
            public ObjectEntryType* ObjectEntryTypes;
            public uint* ObjectEntryCounts;
            public ObjectEntryUsageFlags* pObjectEntryUsageFlags;
            public uint MaxUniformBuffersPerDescriptor;
            public uint MaxStorageBuffersPerDescriptor;
            public uint MaxStorageImagesPerDescriptor;
            public uint MaxSampledImagesPerDescriptor;
            public uint MaxPipelineLayouts;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ObjectTableEntry
        {
            public ObjectEntryType Type;
            public ObjectEntryUsageFlags Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ObjectTablePipelineEntry
        {
            public ObjectEntryType Type;
            public ObjectEntryUsageFlags Flags;
            public Pipeline Pipeline;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ObjectTableDescriptorSetEntry
        {
            public ObjectEntryType Type;
            public ObjectEntryUsageFlags Flags;
            public PipelineLayout PipelineLayout;
            public DescriptorSet DescriptorSet;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ObjectTableVertexBufferEntry
        {
            public ObjectEntryType Type;
            public ObjectEntryUsageFlags Flags;
            public Buffer Buffer;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ObjectTableIndexBufferEntry
        {
            public ObjectEntryType Type;
            public ObjectEntryUsageFlags Flags;
            public Buffer Buffer;
            public IndexType IndexType;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ObjectTablePushantEntry
        {
            public ObjectEntryType Type;
            public ObjectEntryUsageFlags Flags;
            public PipelineLayout PipelineLayout;
            public ShaderStageFlags StageFlags;
        }

        //
        // NV
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ViewportWScaling
        {
            public float XCoeff;
            public float YCoeff;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineViewportWScalingStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public Bool32 ViewportWScalingEnable;
            public uint ViewportCount;
            public ViewportWScaling* ViewportWScalings;
        }

        //
        // EXT
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SurfaceCapabilities2EXT
        {
            public StructureType Type;
            public void* Next;
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
            public SurfaceCounterFlags SupportedSurfaceCounters;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DisplayPowerInfo
        {
            public StructureType Type;
            public void* Next;
            public DisplayPowerState PowerState;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DeviceEventInfo
        {
            public StructureType Type;
            public void* Next;
            public DeviceEventType DeviceEvent;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct DisplayEventInfo
        {
            public StructureType Type;
            public void* Next;
            public DisplayEventType DisplayEvent;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SwapchainCounterCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public SurfaceCounterFlags SurfaceCounters;
        }

        //
        // GOOGLE
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct RefreshCycleDuration
        {
            public ulong RefreshDuration;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PastPresentationTiming
        {
            public uint PresentID;
            public ulong DesiredPrensetTime;
            public ulong ActualPresentTime;
            public ulong EarliestPresentTime;
            public ulong PresentMargin;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PresentTime
        {
            public uint PresentID;
            public ulong DesiredPresentTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PresentTimesInfo
        {
            public StructureType Type;
            public void* Next;
            public uint SwapchainCount;
            public PresentTime* Times;
        }

        //
        // NVX
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceMultiviewPerViewAttributesProperties
        {
            public StructureType Type;
            public void* Next;
            public Bool32 PerViewPositionAllComponents;
        }

        //
        // NV
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct ViewportSwizzle
        {
            public ViewportCoordinateSwizzle X;
            public ViewportCoordinateSwizzle Y;
            public ViewportCoordinateSwizzle Z;
            public ViewportCoordinateSwizzle W;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineViewportSwizzleStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineViewportSwizzleStateCreateFlags Flags;
            public uint ViewportCount;
            public ViewportSwizzle* ViewportSwizzles;
        }

        //
        // EXT
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceDiscardRectangleProperties
        {
            public StructureType Type;
            public void* Next;
            public uint MaxDiscardRectangles;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineDiscardRectangleStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineDiscardRectangleStateCreateFlags Flags;
            public DiscardRectangleMode DiscardRectangleMode;
            public uint DiscardRectangleCount;
            public Rect2D* DiscardRectangles;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct XYColor
        {
            public float X;
            public float Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HdrMetadata
        {
            public StructureType Type;
            public void* Next;
            public XYColor DisplayPrimaryRed;
            public XYColor DisplayPrimaryGreen;
            public XYColor DisplayPrimaryBlue;
            public XYColor WhitePoint;
            public float MaxLuminance;
            public float MinLuminance;
            public float MaxContentLightLevel;
            public float MaxFrameAverageLightLevel;
        }

        //
        // MVK
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct IOSSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public IOSSurfaceCreateFlags Flags;
            public void* View;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MacOSSurfaceCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public MacOSSurfaceCreateFlags Flags;
            public void* View;
        }

        //
        // EXT
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SamplerReductionModeCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public SamplerReductionMode ReductionMode;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceSamplerFilterMinmaxProperties
        {
            public StructureType Type;
            public void* Next;
            public Bool32 FilterMinmaxSingleComponentFormats;
            public Bool32 FilterMinmaxImageComponentMapping;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceBlendOperationAdvancedFeatures
        {
            public StructureType Type;
            public void* Next;
            public Bool32 AdvancedBlendCoherentOperations;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PhysicalDeviceBlendOperationAdvancedProperties
        {
            public StructureType Type;
            public void* Next;
            public uint AdvancedBlendMaxColorAttachments;
            public Bool32 AdvancedBlendIndependentBlend;
            public Bool32 AdvancedBlendNonPremultipliedSrcColor;
            public Bool32 AdvancedBlendNonPremultipliedDstColor;
            public Bool32 AdvancedBlendCorrelatedOverlap;
            public Bool32 AdvancedBlendAllOperations;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineColorBlendAdvancedStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public Bool32 SrcPremultiplied;
            public Bool32 DstPremultiplied;
            public BlendOverlap BlendOverlap;
        }

        //
        // NV
        //
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineCoverageToColorStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineCoverageToColorStateCreateFlags Flags;
            public Bool32 CoverageToColorEnable;
            public uint CoverageToColorLocation;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PipelineCoverageModulationStateCreateInfo
        {
            public StructureType Type;
            public void* Next;
            public PipelineCoverageModulationStateCreateFlags flags;
            public CoverageModulationMode CoverageModulationMode;
            public Bool32 CoverageModulationTableEnable;
            public uint CoverageModulationTableCount;
            public float* CoverageModulationTable;
        }
    }
}