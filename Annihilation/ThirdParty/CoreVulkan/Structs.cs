using System;
using System.Runtime.InteropServices;

namespace Vulkan
{
    public struct VkBool32 : IEquatable<VkBool32>
    {
        private readonly int _value;

        public VkBool32(bool value) => _value = value ? 1 : 0;

        public VkBool32(int value) => _value = value;

        public bool Equals(VkBool32 other) => _value == other._value;

        public override bool Equals(object obj) => obj is VkBool32 && Equals((VkBool32)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(VkBool32 left, VkBool32 right) => left.Equals(right);

        public static bool operator !=(VkBool32 left, VkBool32 right) => !left.Equals(right);

        public static implicit operator bool(VkBool32 value) => value._value != 0;

        public static implicit operator VkBool32(bool value) => new VkBool32(value);

        public static implicit operator int(VkBool32 value) => value._value;

        public static implicit operator VkBool32(int value) => new VkBool32(value);

        public override string ToString() => ((bool)this).ToString();
    }

    public struct VkDeviceSize : IEquatable<VkDeviceSize>
    {
        private readonly ulong _value;

        public VkDeviceSize(ulong value) => _value = value;

        public bool Equals(VkDeviceSize other) => _value == other._value;

        public override bool Equals(object obj) => obj is VkDeviceSize && Equals((VkDeviceSize)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(VkDeviceSize left, VkDeviceSize right) => left.Equals(right);

        public static bool operator !=(VkDeviceSize left, VkDeviceSize right) => !left.Equals(right);

        public override string ToString() => _value.ToString();

        public static implicit operator ulong(VkDeviceSize VkDeviceSize) => VkDeviceSize._value;

        public static implicit operator VkDeviceSize(ulong value) => new VkDeviceSize(value);
    }

    public struct VkSampleMask : IEquatable<VkSampleMask>
    {
        private uint _value;

        public VkSampleMask(uint value) => _value = value;

        public bool Equals(VkSampleMask other) => _value == other._value;

        public override bool Equals(object obj) => obj is VkSampleMask && Equals((VkSampleMask)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(VkSampleMask left, VkSampleMask right) => left.Equals(right);

        public static bool operator !=(VkSampleMask left, VkSampleMask right) => !left.Equals(right);

        public override string ToString() => _value.ToString();

        public static implicit operator uint(VkSampleMask SampleMask) => SampleMask._value;

        public static implicit operator VkSampleMask(uint value) => new VkSampleMask(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkApplicationInfo
    {
        public VkStructureType Type;
        public void* Next;
        public byte* ApplicationName;
        public Version ApplicationVersion;
        public byte* EngineName;
        public Version EngineVersion;
        public Version ApiVersion;

        public VkApplicationInfo(byte* applicationName, Version applicationVersion, byte* engineName, Version engineVersion, Version apiVersion)
        {
            Type = VkStructureType.ApplicationInfo;
            Next = null;
            ApplicationName = applicationName;
            ApplicationVersion = applicationVersion;
            EngineName = engineName;
            EngineVersion = engineVersion;
            ApiVersion = apiVersion;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkInstanceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkInstanceCreateFlags Flags;
        public VkApplicationInfo* ApplicationInfo;
        public uint EnabledLayerCount;
        public byte** EnabledLayerNames;
        public uint EnabledExtensionCount;
        public byte** EnabledExtensionNames;

        public VkInstanceCreateInfo(VkApplicationInfo* applicationInfo, uint extensionCount, byte** extensionNames)
        {
            Type = VkStructureType.InstanceCreateInfo;
            Next = null;
            Flags = VkInstanceCreateFlags.None;
            ApplicationInfo = applicationInfo;
            EnabledLayerCount = 0;
            EnabledLayerNames = null;
            EnabledExtensionCount = extensionCount;
            EnabledExtensionNames = extensionNames;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VkAllocationCallbacks
    {
        public IntPtr UserData;
        public IntPtr Allocation; // AllocationFunction
        public IntPtr Reallocation; // ReallocationFunction
        public IntPtr Free; // FreeFunction
        public IntPtr InternalAllocation; // InternalAllocationNotification
        public IntPtr InternalFree; // InternalFreeNotification
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceFeatures
    {
        public VkBool32 RobustBufferAccess;
        public VkBool32 FullDrawIndexUint32;
        public VkBool32 ImageCubeArray;
        public VkBool32 IndependentBlend;
        public VkBool32 GeometryShader;
        public VkBool32 TessellationShader;
        public VkBool32 SampleRateShading;
        public VkBool32 DualSrcBlend;
        public VkBool32 LogicOp;
        public VkBool32 MultiDrawIndirect;
        public VkBool32 DrawIndirectFirstInstance;
        public VkBool32 DepthClamp;
        public VkBool32 DepthBiasClamp;
        public VkBool32 FillModeNonSolid;
        public VkBool32 DepthBounds;
        public VkBool32 WideLines;
        public VkBool32 LargePoints;
        public VkBool32 AlphaToOne;
        public VkBool32 MultiViewport;
        public VkBool32 SamplerAnisotropy;
        public VkBool32 TextureCompressionETC2;
        public VkBool32 TextureCompressionASTC_LDR;
        public VkBool32 TextureCompressionBC;
        public VkBool32 OcclusionQueryPrecise;
        public VkBool32 PipelineStatisticsQuery;
        public VkBool32 VertexPipelineStoresAndAtomics;
        public VkBool32 FragmentStoresAndAtomics;
        public VkBool32 ShaderTessellationAndGeometryPointSize;
        public VkBool32 ShaderImageGatherExtended;
        public VkBool32 ShaderStorageImageExtendedFormats;
        public VkBool32 ShaderStorageImageMultisample;
        public VkBool32 ShaderStorageImageReadWithoutFormat;
        public VkBool32 ShaderStorageImageWriteWithoutFormat;
        public VkBool32 ShaderUniformBufferArrayDynamicIndexing;
        public VkBool32 ShaderSampledImageArrayDynamicIndexing;
        public VkBool32 ShaderStorageBufferArrayDynamicIndexing;
        public VkBool32 ShaderStorageImageArrayDynamicIndexing;
        public VkBool32 ShaderClipDistance;
        public VkBool32 ShaderCullDistance;
        public VkBool32 ShaderFloat64;
        public VkBool32 ShaderInt64;
        public VkBool32 ShaderInt16;
        public VkBool32 ShaderResourceResidency;
        public VkBool32 ShaderResourceMinLod;
        public VkBool32 SparseBinding;
        public VkBool32 SparseResidencyBuffer;
        public VkBool32 SparseResidencyImage2D;
        public VkBool32 SparseResidencyImage3D;
        public VkBool32 SparseResidency2Samples;
        public VkBool32 SparseResidency4Samples;
        public VkBool32 SparseResidency8Samples;
        public VkBool32 SparseResidency16Samples;
        public VkBool32 SparseResidencyAliased;
        public VkBool32 VariableMultisampleRate;
        public VkBool32 InheritedQueries;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkFormatProperties
    {
        public VkFormatFeatureFlags LinearTilingFeatures;
        public VkFormatFeatureFlags OptimalTilingFeatures;
        public VkFormatFeatureFlags BufferFeatures;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExtent3D
    {
        public uint Width;
        public uint Height;
        public uint Depth;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageFormatProperties
    {
        public VkExtent3D MaxExtents;
        public uint MaxMipLevels;
        public uint MaxArrayLayers;
        public VkSampleCountFlags SampleCounts;
        public VkDeviceSize MaxResourceSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceLimits
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
        public VkDeviceSize BufferImageGranularity;
        public VkDeviceSize SparseAddressSpaceSize;
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
        public VkDeviceSize MinTexelBufferOffsetAlignment;
        public VkDeviceSize MinUniformBufferOffsetAlignment;
        public VkDeviceSize MinStorageBufferOffsetAlignment;
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
        public VkSampleCountFlags FramebufferColorSampleCounts;
        public VkSampleCountFlags FramebufferDepthSampleCounts;
        public VkSampleCountFlags FramebufferStencilSampleCounts;
        public VkSampleCountFlags FramebufferNoAttachmentsSampleCounts;
        public uint MaxColorAttachments;
        public VkSampleCountFlags SampledImageColorSampleCounts;
        public VkSampleCountFlags SampledImageIntegerSampleCounts;
        public VkSampleCountFlags SampledImageDepthSampleCounts;
        public VkSampleCountFlags SampledImageStencilSampleCounts;
        public VkSampleCountFlags StorageImageSampleCounts;
        public uint MaxSampleMaskWords;
        public VkBool32 TimestampComputeAndGraphics;
        public float TimestampPeriod;
        public uint MaxClipDistances;
        public uint MaxCullDistances;
        public uint MaxCombinedClipAndCullDistances;
        public uint DiscreteQueuePriorities;
        public fixed float PointSizeRange[2];
        public fixed float LineWidthRange[2];
        public float PointSizeGranularity;
        public float LineWidthGranularity;
        public VkBool32 StrictLines;
        public VkBool32 StandardSampleLocations;
        public VkDeviceSize OptimalBufferCopyOffsetAlignment;
        public VkDeviceSize OptimalBufferCopyRowPitchAlignment;
        public VkDeviceSize NonCoherentAtomSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceSparseProperties
    {
        public VkBool32 ResidencyStandard2DBlockShape;
        public VkBool32 ResidencyStandard2DMultisampleBlockShape;
        public VkBool32 ResidencyStandard3DBlockShape;
        public VkBool32 ResidencyAlignedMipSize;
        public VkBool32 ResidencyNonResidentStrict;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceProperties
    {
        public uint ApiVersion;
        public uint DriverVersion;
        public uint VendorId;
        public uint DeviceId;
        public VkPhysicalDeviceType DeviceType;
        public fixed byte DeviceName[Vk.MaxPhysicalDeviceNameSize];
        public fixed byte PipelineCacheUuid[Vk.UUIDSize];
        public VkPhysicalDeviceLimits Limits;
        public VkPhysicalDeviceSparseProperties SparseProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkQueueFamilyProperties
    {
        public VkQueueFlags QueueFlags;
        public uint QueueCount;
        public uint TimestampValidBits;
        public VkExtent3D MinImageTransferGranularity;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryType
    {
        public VkMemoryPropertyFlags PropertyFlags;
        public uint HeapIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryHeap
    {
        public VkDeviceSize Size;
        public VkMemoryHeapFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceMemoryProperties
    {
        public uint MemoryTypeCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Vk.MaxMemoryTypes)]
        public VkMemoryType[] MemoryTypes;
        public uint MemoryHeapCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Vk.MaxMemoryHeaps)]
        public VkMemoryHeap[] MemoryHeaps;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceQueueCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceQueueCreateFlags Flags;
        public uint QueueFamilyIndex;
        public uint QueueCount;
        public float* QueuePriorities;

        public VkDeviceQueueCreateInfo(uint queueFamilyIndex, uint queueCount, float* queuePriorities)
        {
            Type = VkStructureType.DeviceQueueCreateInfo;
            Next = null;
            Flags = VkDeviceQueueCreateFlags.None;
            QueueFamilyIndex = queueFamilyIndex;
            QueueCount = queueCount;
            QueuePriorities = queuePriorities;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceCreateFlags Flags;
        public uint QueueCreateInfoCount;
        public VkDeviceQueueCreateInfo* QueueCreateInfos;
        public uint EnabledLayerCount;
        public byte** EnabledLayerNames;
        public uint EnabledExtensionCount;
        public byte** EnabledExtensionNames;
        public VkPhysicalDeviceFeatures* EnabledFeatures;

        public VkDeviceCreateInfo(VkDeviceQueueCreateInfo queueCreateInfo, uint enabledExtensionCount, byte** enabledExtensionNames, VkPhysicalDeviceFeatures enabledFeatures)
        {
            Type = VkStructureType.DeviceCreateInfo;
            Next = null;
            Flags = VkDeviceCreateFlags.None;
            QueueCreateInfoCount = 1;
            QueueCreateInfos = &queueCreateInfo;
            EnabledLayerCount = 0;
            EnabledLayerNames = null;
            EnabledExtensionCount = enabledExtensionCount;
            EnabledExtensionNames = enabledExtensionNames;
            EnabledFeatures = &enabledFeatures;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExtensionProperties
    {
        public fixed byte ExtensionName[Vk.MaxExtensionNameSize];
        public Version SpecVersion;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkLayerProperties
    {
        public fixed byte LayerName[Vk.MaxExtensionNameSize];
        public Version SpecVersion;
        public Version ImplementationVersion;
        public fixed byte Description[Vk.MaxDescriptionSize];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSubmitInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public VkSemaphore* WaitSemaphores;
        public VkPipelineStageFlags* WaitDstStageMask;
        public uint CommandBufferCount;
        public VkCommandBuffer* CommandBuffers;
        public uint SignalSemaphoreCount;
        public VkSemaphore* SignalSemaphores;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryAllocateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceSize AllocationSize;
        public uint MemoryTypeIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMappedMemoryRange
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceMemory Memory;
        public VkDeviceSize Offset;
        public VkDeviceSize Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryRequirements
    {
        public VkDeviceSize Size;
        public VkDeviceSize Alignment;
        public uint MemoryTypeBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseImageFormatProperties
    {
        public VkImageAspectFlags AspectMask;
        public VkExtent3D ImageGranularity;
        public VkSparseImageFormatFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseImageMemoryRequirements
    {
        public VkSparseImageFormatProperties FormatProperties;
        public uint ImageMipTailFirstLod;
        public VkDeviceSize ImageMipTailSize;
        public VkDeviceSize ImageMipTailOffset;
        public VkDeviceSize ImageMipTailStride;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseMemoryBind
    {
        public VkDeviceSize ResourceOffset;
        public VkDeviceSize Size;
        public VkDeviceMemory Memory;
        public VkDeviceSize MemoryOffset;
        public VkSparseMemoryBindFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseBufferMemoryBindInfo
    {
        public VkBuffer Buffer;
        public uint BindCount;
        public VkSparseMemoryBind* Binds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseImageOpaqueMemoryBindInfo
    {
        public VkImage Image;
        public uint BindCount;
        public VkSparseMemoryBind* Binds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageSubresource
    {
        public VkImageAspectFlags AspectMask;
        public uint MipLevel;
        public uint ArrayLayer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkOffset3D
    {
        public int X;
        public int Y;
        public int Z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseImageMemoryBind
    {
        public VkImageSubresource Subresource;
        public VkOffset3D Offset;
        public VkExtent3D Extent;
        public VkDeviceMemory Memory;
        public VkDeviceSize MemoryOffset;
        public VkSparseMemoryBindFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseImageMemoryBindInfo
    {
        public VkImage Image;
        public uint BindCount;
        public VkSparseImageMemoryBind* Binds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBindSparseInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public VkSemaphore* WaitSemaphores;
        public uint BufferBindCount;
        public VkSparseBufferMemoryBindInfo* BufferBinds;
        public uint ImageOpaqueBindCount;
        public VkSparseImageOpaqueMemoryBindInfo* ImageOpaqueBinds;
        public uint ImageBindCount;
        public VkSparseImageMemoryBindInfo* ImageBinds;
        public uint SignalSemaphoreCount;
        public VkSemaphore* SignalSemaphores;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkFenceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkFenceCreateFlags Flags;

        public VkFenceCreateInfo(VkFenceCreateFlags flags)
        {
            Type = VkStructureType.FenceCreateInfo;
            Next = null;
            Flags = flags;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSemaphoreCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSemaphoreCreateFlags Flags;

        public VkSemaphoreCreateInfo(VkSemaphoreCreateFlags flags = VkSemaphoreCreateFlags.None)
        {
            Type = VkStructureType.SemaphoreCreateInfo;
            Next = null;
            Flags = flags;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkEventCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkEventCreateFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkQueryPoolCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkQueryPoolCreateFlags Flags;
        public VkQueryType QueryType;
        public uint QueryCount;
        public VkQueryPipelineStatisticFlags PipelineStatistics;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBufferCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBufferCreateFlags Flags;
        public VkDeviceSize Size;
        public VkBufferUsageFlags Usage;
        public VkSharingMode SharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBufferViewCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBufferViewCreateFlags Flags;
        public VkBuffer Buffer;
        public VkFormat Format;
        public VkDeviceSize Offset;
        public VkDeviceSize Range;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkImageCreateFlags Flags;
        public VkImageType ImageType;
        public VkFormat Format;
        public VkExtent3D Extent;
        public uint MipLevels;
        public uint ArrayLayers;
        public VkSampleCountFlags Samples;
        public VkImageTiling Tiling;
        public VkImageUsageFlags Usage;
        public VkSharingMode SharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
        public VkImageLayout InitialLayout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSubresourceLayout
    {
        public VkDeviceSize Offset;
        public VkDeviceSize Size;
        public VkDeviceSize RowPitch;
        public VkDeviceSize ArrayPitch;
        public VkDeviceSize DepthPitch;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkComponentMapping
    {
        public VkComponentSwizzle R;
        public VkComponentSwizzle G;
        public VkComponentSwizzle B;
        public VkComponentSwizzle A;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageSubresourceRange
    {
        public VkImageAspectFlags AspectMask;
        public uint BaseMipLevel;
        public uint LevelCount;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageViewCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkImageViewCreateFlags Flags;
        public VkImage Image;
        public VkImageViewType ViewType;
        public VkFormat Format;
        public VkComponentMapping Components;
        public VkImageSubresourceRange SubresourceRange;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkShaderModuleCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkShaderModuleCreateFlags Flags;
        public Size CodeSize;
        public uint* Code;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineCacheCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineCacheCreateFlags Flags;
        public Size InitialDataSize;
        public void* InitialData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSpecializationMapEntry
    {
        public uint antId;
        public uint Offset;
        public Size Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSpecializationInfo
    {
        public uint MapEntryCount;
        public VkSpecializationMapEntry* MapEntries;
        public Size DataSize;
        public void* Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineShaderStageCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineShaderStageCreateFlags Flags;
        public VkShaderStageFlags Stage;
        public VkShaderModule Module;
        public byte* Name;
        public VkSpecializationInfo* SpecializationInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkVertexInputBindingDescription
    {
        public uint Binding;
        public uint Stride;
        public VkVertexInputRate InputRate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkVertexInputAttributeDescription
    {
        public uint Location;
        public uint Binding;
        public VkFormat Format;
        public uint Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineVertexInputStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineVertexInputStateCreateFlags Flags;
        public uint VertexBindingDescriptionCount;
        public VkVertexInputBindingDescription* VertexInputBindingDescriptions;
        public uint VertexAttributeDescriptionCount;
        public VkVertexInputAttributeDescription* VertexAttributeDescriptions;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineInputAssemblyStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineInputAssemblyStateCreateFlags Flags;
        public VkPrimitiveTopology Topology;
        public VkBool32 PrimitiveRestartEnable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineTessellationStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineTessellationStateCreateFlags Flags;
        public uint PatchControlPoints;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkViewport
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float MinDepth;
        public float MaxDepth;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkOffset2D
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExtent2D
    {
        public uint Width;
        public uint Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkRect2D
    {
        public VkOffset2D Offset;
        public VkExtent2D Extent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineViewportStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineViewportStateCreateFlags Flags;
        public uint ViewportCount;
        public VkViewport* Viewports;
        public uint ScissorCount;
        public VkRect2D* Scissors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineRasterizationStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineRasterizationStateCreateFlags Flags;
        public VkBool32 DepthClampEnable;
        public VkBool32 RasterizerDiscardEnable;
        public VkPolygonMode PolygonMode;
        public VkCullModeFlags CullMode;
        public VkFrontFace FrontFace;
        public VkBool32 DepthBiasEnable;
        public float DepthBiasantFactor;
        public float DepthBiasClamp;
        public float DepthBiasSlopeFactor;
        public float LineWidth;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineMultisampleStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineMultisampleStateCreateFlags Flags;
        public VkSampleCountFlags RasterizationSamples;
        public VkBool32 SampleShadingEnable;
        public float MinSampleShading;
        public VkSampleMask* SampleMask;
        public VkBool32 AlphaToCoverageEnable;
        public VkBool32 AlphaToOneEnable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkStencilOpState
    {
        public VkStencilOp FailOp;
        public VkStencilOp PassOp;
        public VkStencilOp DepthFailOp;
        public VkCompareOp CompareOp;
        public uint CompareMask;
        public uint WriteMask;
        public uint Reference;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineDepthStencilStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineDepthStencilStateCreateFlags Flags;
        public VkBool32 DepthTestEnable;
        public VkBool32 DepthWriteEnable;
        public VkCompareOp DepthCompareOp;
        public VkBool32 DepthBoundsTestEnable;
        public VkBool32 StencilTestEnable;
        public VkStencilOpState Front;
        public VkStencilOpState Back;
        public float MinDepthBounds;
        public float MaxDepthBounds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineColorBlendAttachmentState
    {
        public VkBool32 BlendEnable;
        public VkBlendFactor SrcColorBlendFactor;
        public VkBlendFactor DstColorBlendFactor;
        public VkBlendOp ColorBlendOp;
        public VkBlendFactor SrcAlphaBlendFactor;
        public VkBlendFactor DstAlphaBlendFactor;
        public VkBlendOp AlphaBlendOp;
        public VkColorComponentFlags ColorWriteMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineColorBlendStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineColorBlendStateCreateFlags Flags;
        public VkBool32 LogicOpEnable;
        public VkLogicOp LogicOp;
        public uint AttachmentCount;
        public VkPipelineColorBlendAttachmentState* Attachments;
        public fixed float Blendants[4];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineDynamicStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineDynamicStateCreateFlags Flags;
        public uint DynamicStateCount;
        public VkDynamicState* DynamicStates;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkGraphicsPipelineCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineCreateFlags Flags;
        public uint StageCount;
        public VkPipelineShaderStageCreateInfo* Stages;
        public VkPipelineVertexInputStateCreateInfo* VertexInputState;
        public VkPipelineInputAssemblyStateCreateInfo* InputAssemblyState;
        public VkPipelineTessellationStateCreateInfo* TessellationState;
        public VkPipelineViewportStateCreateInfo* ViewportState;
        public VkPipelineRasterizationStateCreateInfo* RasterizationState;
        public VkPipelineMultisampleStateCreateInfo* MultisampleState;
        public VkPipelineDepthStencilStateCreateInfo* DepthStencilState;
        public VkPipelineColorBlendStateCreateInfo* ColorBlendState;
        public VkPipelineDynamicStateCreateInfo* DynamicState;
        public VkPipelineLayout Layout;
        public VkRenderPass RenderPass;
        public uint Subpass;
        public VkPipeline BasePipelineHandle;
        public int BasePipelineIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkComputePipelineCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineCreateFlags Flags;
        public VkPipelineShaderStageCreateInfo Stage;
        public VkPipelineLayout Layout;
        public VkPipeline BasePipelineHandle;
        public int BasePipelineIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPushantRange
    {
        public VkShaderStageFlags StageFlags;
        public uint Offset;
        public uint Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineLayoutCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineLayoutCreateFlags Flags;
        public uint SetLayoutCount;
        public VkDescriptorSetLayout* SetLayouts;
        public uint PushantRangeCount;
        public VkPushantRange* PushantRanges;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSamplerCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSampleCountFlags Flags;
        public VkFilter MagFilter;
        public VkFilter MinFilter;
        public VkSamplerMipmapMode MipmapMode;
        public VkSamplerAddressMode AddressModeU;
        public VkSamplerAddressMode AddressModeV;
        public VkSamplerAddressMode AddressModeW;
        public float MipLodBias;
        public VkBool32 AnisotropyEnable;
        public float MaxAnisotropy;
        public VkBool32 CompareEnable;
        public VkCompareOp CompareOp;
        public float MinLod;
        public float MaxLod;
        public VkBorderColor BorderColor;
        public VkBool32 UnnormalizedCoordinates;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorSetLayoutBinding
    {
        public uint Binding;
        public VkDescriptorType DescriptorType;
        public uint DescriptorCount;
        public VkShaderStageFlags StageFlags;
        public VkSampler* ImmutableSamplers;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorSetLayoutCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDescriptorSetLayoutCreateFlags Flags;
        public uint BindingCount;
        public VkDescriptorSetLayoutBinding* Bindings;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorPoolSize
    {
        public VkDescriptorType Type;
        public uint DescriptorCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorPoolCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDescriptorPoolCreateFlags Flags;
        public uint MaxSets;
        public uint PoolSizeCount;
        public VkDescriptorPoolSize* PoolSizes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorSetAllocateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDescriptorPool DescriptorPool;
        public uint DescriptorSetCount;
        public VkDescriptorSetLayout* SetLayouts;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorImageInfo
    {
        public VkSampler Sampler;
        public VkImageView ImageView;
        public VkImageLayout ImageLayout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorBufferInfo
    {
        public VkBuffer Buffer;
        public VkDeviceSize Offset;
        public VkDeviceSize Range;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkWriteDescriptorSet
    {
        public VkStructureType Type;
        public void* Next;
        public VkDescriptorSet DstSet;
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
        public VkDescriptorType DescriptorType;
        public VkDescriptorImageInfo* ImageInfo;
        public VkDescriptorBufferInfo* BufferInfo;
        public VkBufferView* TexelBufferView;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkCopyDescriptorSet
    {
        public VkStructureType Type;
        public void* Next;
        public VkDescriptorSet SrcSet;
        public uint SrcBinding;
        public uint SrcArrayElement;
        public VkDescriptorSet DstSet;
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkFramebufferCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkFramebufferCreateFlags Flags;
        public VkRenderPass RenderPass;
        public uint AttachmentCount;
        public VkImageView* Attachments;
        public uint Width;
        public uint Height;
        public uint Layers;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkAttachmentDescription
    {
        public VkAttachmentDescriptionFlags Flags;
        public VkFormat Format;
        public VkSampleCountFlags Samples;
        public VkAttachmentLoadOp LoadOp;
        public VkAttachmentStoreOp StoreOp;
        public VkAttachmentLoadOp StencilLoadOp;
        public VkAttachmentStoreOp StencilStoreOp;
        public VkImageLayout InitialLayout;
        public VkImageLayout FinalLayout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkAttachmentReference
    {
        public uint Attachment;
        public VkImageLayout Layout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSubpassDescription
    {
        public VkSubpassDescriptionFlags Flags;
        public VkPipelineBindPoint PipelineBindPoint;
        public uint InputAttachmentCount;
        public VkAttachmentReference* InputAttachments;
        public uint ColorAttachmentCount;
        public VkAttachmentReference* ColorAttachments;
        public VkAttachmentReference* ResolveAttachments;
        public VkAttachmentReference* DepthStencilAttachments;
        public uint PreserveAttachmentCount;
        public uint* PreserveAttachments;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSubpassDependency
    {
        public uint SrcSubpass;
        public uint DstSubpass;
        public VkPipelineStageFlags SrcStageMask;
        public VkPipelineStageFlags DstStageMask;
        public VkAccessFlags SrcAccessMask;
        public VkAccessFlags DstAccessMask;
        public VkDependencyFlags DependencyFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkRenderPassCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkRenderPassCreateFlags Flags;
        public uint AttachmentCount;
        public VkAttachmentDescription* Attachments;
        public uint SubpassCount;
        public VkSubpassDescription* Subpasses;
        public uint DependencyCount;
        public VkSubpassDependency* Dependencies;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkCommandPoolCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkCommandPoolCreateFlags Flags;
        public uint QueueFamilyIndex;

        public VkCommandPoolCreateInfo(VkCommandPoolCreateFlags flags, uint queueFamilyIndex)
        {
            Type = VkStructureType.CommandPoolCreateInfo;
            Next = null;
            Flags = flags;
            QueueFamilyIndex = queueFamilyIndex;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkCommandBufferAllocateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkCommandPool CommandPool;
        public VkCommandBufferLevel Level;
        public uint CommandBufferCount;

        public VkCommandBufferAllocateInfo(VkCommandPool commandPool, uint commandBufferCount)
        {
            Type = VkStructureType.CommandBufferAllocateInfo;
            Next = null;
            CommandPool = commandPool;
            Level = VkCommandBufferLevel.Primary;
            CommandBufferCount = commandBufferCount;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkCommandBufferInheritanceInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkRenderPass RenderPass;
        public uint Subpass;
        public VkFramebuffer Framebuffer;
        public VkBool32 OcclusionQueryEnable;
        public VkQueryControlFlags QueryFlags;
        public VkQueryPipelineStatisticFlags PipelineStatistics;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkCommandBufferBeginInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkCommandBufferUsageFlags Flags;
        public VkCommandBufferInheritanceInfo* InheritanceInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBufferCopy
    {
        public VkDeviceSize SrcOffset;
        public VkDeviceSize DstOffset;
        public VkDeviceSize Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageSubresourceLayers
    {
        public VkImageAspectFlags AspectMask;
        public uint MipLevel;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageCopy
    {
        public VkImageSubresourceLayers SrcSubresource;
        public VkOffset3D SrcOffset;
        public VkImageSubresourceLayers DstSubresource;
        public VkOffset3D DstOffset;
        public VkExtent3D Extent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageBlit
    {
        public VkImageSubresourceLayers SrcSubresource;
        public VkOffset3D* SrcOffsets;
        public VkImageSubresourceLayers DstSubresource;
        public VkOffset3D* DstOffsets;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBufferImageCopy
    {
        public VkDeviceSize BufferOffset;
        public uint BufferRowLength;
        public uint BufferImageHeight;
        public VkImageSubresourceLayers ImageSubresource;
        public VkOffset3D ImageOffset;
        public VkExtent3D ImageExtent;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct VkClearColorValue
    {
        [FieldOffset(0)] public fixed float Float32[4];
        [FieldOffset(0)] public fixed int Int32[4];
        [FieldOffset(0)] public fixed uint Uint32[4];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkClearDepthStencilValue
    {
        public float Depth;
        public uint Stencil;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct VkClearValue
    {
        [FieldOffset(0)] public VkClearColorValue Color;
        [FieldOffset(0)] public VkClearDepthStencilValue DepthStencil;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkClearAttachment
    {
        public VkImageAspectFlags AspectMask;
        public uint ColorAttachment;
        public VkClearValue ClearValue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkClearRect
    {
        public VkRect2D Rect;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageResolve
    {
        public VkImageSubresourceLayers SrcSubresource;
        public VkOffset3D SrcOffset;
        public VkImageSubresourceLayers DstSubresource;
        public VkOffset3D DstOffset;
        public VkExtent3D Extent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryBarrier
    {
        public VkStructureType Type;
        public void* Next;
        public VkAccessFlags SrcAccessMask;
        public VkAccessFlags DstAccessMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBufferMemoryBarrier
    {
        public VkStructureType Type;
        public void* Next;
        public VkAccessFlags SrcAccessMask;
        public VkAccessFlags DstAccessMask;
        public uint SrcQueueFamilyIndex;
        public uint DstQueueFamilyIndex;
        public VkBuffer Buffer;
        public VkDeviceSize Offset;
        public VkDeviceSize Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageMemoryBarrier
    {
        public VkStructureType Type;
        public void* Next;
        public VkAccessFlags SrcAccessMask;
        public VkAccessFlags DstAccessMask;
        public VkImageLayout OldLayout;
        public VkImageLayout NewLayout;
        public uint SrcQueueFamilyIndex;
        public uint DstQueueFamilyIndex;
        public VkImage Image;
        public VkImageSubresourceRange SubresourceRange;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkRenderPassBeginInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkRenderPass RenderPass;
        public VkFramebuffer Framebuffer;
        public VkRect2D RenderArea;
        public uint ClearValueCount;
        public VkClearValue* ClearValues;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDispatchIndirectCommand
    {
        public uint X;
        public uint Y;
        public uint Z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDrawIndexedIndirectCommand
    {
        public uint IndexCount;
        public uint InstanceCount;
        public uint FirstIndex;
        public int VertexOffset;
        public uint FirstInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDrawIndirectCommand
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
    public unsafe struct VkSurfaceCapabilities
    {
        public uint MinImageCount;
        public uint MaxImageCount;
        public VkExtent2D CurrentExtent;
        public VkExtent2D MinImageExtent;
        public VkExtent2D MaxImageExtent;
        public uint MaxImageArrayLayers;
        public VkSurfaceTransformFlags SupportedTransforms;
        public VkSurfaceTransformFlags CurrentTransform;
        public VkCompositeAlphaFlags SupportedCompositeAlpha;
        public VkImageUsageFlags SupportedUsageFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSurfaceFormat
    {
        public VkFormat Format;
        public VkColorSpace ColorSpace;
    }

    //
    // KHR Swapchain
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSwapchainCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSwapchainCreateFlags Flags;
        public VkSurface Surface;
        public uint MinImageCount;
        public VkFormat ImageFormat;
        public VkColorSpace ImageColorSpace;
        public VkExtent2D ImageExtent;
        public uint ImageArrayLayers;
        public VkImageUsageFlags ImageUsage;
        public VkSharingMode ImageSharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
        public VkSurfaceTransformFlags PreTransform;
        public VkCompositeAlphaFlags CompositeAlpha;
        public VkPresentMode PresentMode;
        public VkBool32 Clipped;
        public VkSwapchain OldSwapchain;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPresentInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public VkSemaphore* WaitSemaphores;
        public uint SwapchainCount;
        public VkSwapchain* Swapchains;
        public uint* ImageIndices;
        public VkResult* Results;
    }

    //
    // KHR display
    // 
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayProperties
    {
        public VkDisplay Display;
        public byte* DisplayName;
        public VkExtent2D PhysicalDimensions;
        public VkExtent2D PhysicalResolution;
        public VkSurfaceTransformFlags SupportedTransforms;
        public VkBool32 PlaneReorderPossible;
        public VkBool32 PersistentContent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayModeParameters
    {
        public VkExtent2D VisibleRegion;
        public uint RefreshRate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayModeProperties
    {
        public VkDisplayMode DisplayMode;
        public VkDisplayModeParameters Parameters;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayModeCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDisplayModeCreateFlags Flags;
        public VkDisplayModeParameters Parameters;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayPlaneCapabilities
    {
        public VkDisplayPlaneAlphaFlags SupportedAlpha;
        public VkOffset2D MinSrcPosition;
        public VkOffset2D MaxSrcPosition;
        public VkExtent2D MinSrcExtent;
        public VkExtent2D MaxSrcExtent;
        public VkOffset2D MinDstPosition;
        public VkOffset2D MaxDstPosition;
        public VkExtent2D MinDstExtent;
        public VkExtent2D MaxDstExtent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayPlaneProperties
    {
        public VkDisplay CurrentDisplay;
        public uint CurrentStackIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplaySurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDisplaySurfaceCreateFlags Flags;
        public VkDisplayMode DisplayMode;
        public uint PlaneIndex;
        public uint PlaneStackIndex;
        public VkSurfaceTransformFlags Transform;
        public float GlobalAlpha;
        public VkDisplayPlaneAlphaFlags AlphaMode;
        public VkExtent2D ImageExtent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayPresentInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkRect2D SrcRect;
        public VkRect2D DstRect;
        public VkBool32 Persistent;
    }

    //
    // KHR Platforms
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkXlibSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkXlibSurfaceCreateFlags Flags;
        public IntPtr Dpy;
        public IntPtr Window;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkXcbSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkXcbSurfaceCreateFlags Flags;
        public IntPtr Connection;
        public IntPtr Window;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkWaylandSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkWaylandSurfaceCreateFlags Flags;
        public IntPtr Display;
        public IntPtr Surface;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMirSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkMirSurfaceCreateFlags Flags;
        public IntPtr Connection;
        public IntPtr MirSurface;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkAndroidSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkAndroidSurfaceCreateFlags Flags;
        public IntPtr Window;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkWin32SurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkWin32SurfaceCreateFlags Flags;
        public IntPtr Hinstance;
        public IntPtr Hwnd;

        public VkWin32SurfaceCreateInfo(IntPtr hinstance, IntPtr hwnd)
        {
            Type = VkStructureType.Win32SurfaceCreateInfo;
            Next = null;
            Flags = VkWin32SurfaceCreateFlags.None;
            Hinstance = hinstance;
            Hwnd = hwnd;
        }
    }

    //
    // KHR 2
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceFeatures2
    {
        public VkStructureType Type;
        public void* Next;
        public VkPhysicalDeviceFeatures Features;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceProperties2
    {
        public VkStructureType Type;
        public void* Next;
        public VkPhysicalDeviceProperties Properties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkFormatProperties2
    {
        public VkStructureType Type;
        public void* Next;
        public VkFormatProperties FormatProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageFormatProperties2
    {
        public VkStructureType Type;
        public void* Next;
        public VkImageFormatProperties ImageFormatProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceImageFormatInfo2
    {
        public VkStructureType Type;
        public void* Next;
        public VkFormat Format;
        public VkImageType ImageType;
        public VkImageTiling Tiling;
        public VkImageUsageFlags Usage;
        public VkImageCreateFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkQueueFamilyProperties2
    {
        public VkStructureType Type;
        public void* Next;
        public VkQueueFamilyProperties QueueFamilyProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceMemoryProperties2
    {
        public VkStructureType Type;
        public void* Next;
        public VkPhysicalDeviceMemoryProperties MemoryProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseImageFormatProperties2
    {
        public VkStructureType Type;
        public void* Next;
        public VkSparseImageFormatProperties Properties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceSparseImageFormatInfo2
    {
        public VkStructureType Type;
        public void* Next;
        public VkFormat Format;
        public VkImageType ImageType;
        public VkSampleCountFlags Samples;
        public VkImageUsageFlags Usage;
        public VkImageTiling Tiling;
    }

    //
    // KHR Memory
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalMemoryProperties
    {
        public VkExternalMemoryFeatureFlags ExternalMemoryFeatures;
        public VkExternalMemoryHandleTypeFlags ExportFomImportedHandleTypes;
        public VkExternalMemoryHandleTypeFlags CompatibleHandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceExternalImageFormatInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalImageFormatProperties
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryProperties ExternalMemoryProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceExternalBufferInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBufferCreateFlags Flags;
        public VkBufferUsageFlags Usage;
        public VkExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalBufferProperties
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryProperties ExternalMemoryProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceIDProperties
    {
        public VkStructureType Type;
        public void* Next;
        public fixed byte DeivceUUID[(int)Vk.UUIDSize];
        public fixed byte DriverUUID[(int)Vk.UUIDSize];
        public fixed byte DeviceLUID[Vk.LUIDSize];
        public uint DeviceNodeMask;
        public VkBool32 DeviceLUIDValid;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalMemoryImageCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalMemoryBufferCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportMemoryAllocateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImportMemoryWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportMemoryWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryWin32HandleProperties
    {
        public VkStructureType Type;
        public void* Next;
        public uint MemoryTypeBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryGetWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceMemory Memory;
        public VkExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImportMemoryFdInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlags HandleType;
        public int Fd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryFdProperties
    {
        public VkStructureType Type;
        public void* Next;
        public uint MemoryTypeBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryGetFdInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceMemory Memory;
        public VkExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkWin32KeyedMutexAcquireReleaseInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint AcquireCount;
        public VkDeviceMemory* AcquireSyncs;
        public ulong* AcquireKeys;
        public uint* AcquireTimeouts;
        public uint ReleaseCount;
        public VkDeviceMemory* ReleaseSyncs;
        public ulong* ReleaseKeys;
    }

    //
    // KHR semaphore
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceExternalSemaphoreInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalSemaphoreHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalSemaphoreProperties
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalSemaphoreHandleTypeFlags ExportFromImportedHandleTypes;
        public VkExternalSemaphoreHandleTypeFlags CompatibleHandleTypes;
        public VkExternalSemaphoreFeatureFlags ExternalSemaphoreFeatures;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportSemaphoreCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalSemaphoreHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImportSemaphoreWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSemaphore Semaphore;
        public VkSemaphoreImportFlags Flags;
        public VkExternalSemaphoreHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportSemaphoreWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkD3D12FenceSubmitInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint WaitSemaphoreValuesCount;
        public ulong* WaitSemaphoreValues;
        public uint SignalSemaphoreValuesCount;
        public ulong* SignalSemaphoreValues;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSemaphoreGetWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSemaphore Semaphore;
        public VkExternalSemaphoreHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImportSemaphoreFdInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSemaphore Semaphore;
        public VkSemaphoreImportFlags Flags;
        public VkExternalSemaphoreHandleTypeFlags HandleType;
        public int Fd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSemaphoreGetFdInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSemaphore Semaphore;
        public VkExternalSemaphoreHandleTypeFlags HandleType;
    }

    //
    // KHR misc
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDevicePushDescriptorProperties
    {
        public VkStructureType Type;
        public void* Next;
        public uint MaxPushDescriptors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDevice16BitStorageFeatures
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 StorageBuffer16BitAccess;
        public VkBool32 UniformAndStorageBuffer16BitAccess;
        public VkBool32 StoragePushant16;
        public VkBool32 StorageInputOutput16;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkRectLayer
    {
        public VkOffset2D Offset;
        public VkExtent2D Extent;
        public uint Layer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPresentRegion
    {
        public uint RectangleCount;
        public VkRectLayer* Rectangles;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPresentRegions
    {
        public VkStructureType Type;
        public void* Next;
        public uint SwapchainCount;
        public VkPresentRegion* Regions;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorUpdateTemplateEntry
    {
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
        public VkDescriptorType DescriptorType;
        public Size Offset;
        public Size Stride;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDescriptorUpdateTemplateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDescriptorUpdateTemplateCreateFlags Flags;
        public uint DescriptorUpdateEntryCount;
        public VkDescriptorUpdateTemplateEntry* DescriptorUpdateEntries;
        public VkDescriptorUpdateTemplateType TemplateType;
        public VkDescriptorSetLayout DescriptorSetLayout;
        public VkPipelineBindPoint PipelineBindPoint;
        public VkPipelineLayout PipelineLayout;
        public uint Set;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSharedPresentSurfaceCapabilities
    {
        public VkStructureType Type;
        public void* Next;
        public VkImageUsageFlags SharedPresentSupportedUsageFlags;
    }

    //
    // KHR fence
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceExternalFenceInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalFenceHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalFenceProperties
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalFenceHandleTypeFlags ExportFromImportedHandleTypes;
        public VkExternalFenceHandleTypeFlags CompatibleHandleTypes;
        public VkExternalFenceFeatureFlags ExternalFenceFeatures;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportFenceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalFenceHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImportFenceWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkFence Fence;
        public VkFenceImportFlags Flags;
        public VkExternalFenceHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportFenceWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkFenceGetWin32HandleInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkFence Fence;
        public VkExternalFenceHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImportFenceFdInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkFence Fence;
        public VkFenceImportFlags Flags;
        public VkExternalFenceHandleTypeFlags HandleType;
        public int Fd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkFenceGetFdInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkFence Fence;
        public VkExternalFenceHandleTypeFlags HandleType;
    }

    //
    // KHR 2
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceSurfaceInfo2
    {
        public VkStructureType Type;
        public void* Next;
        public VkSurface Surface;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSurfaceCapabilities2
    {
        public VkStructureType Type;
        public void* Next;
        public VkSurfaceCapabilities SurfaceCapabilities;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSurfaceFormat2
    {
        public VkStructureType Type;
        public void* Next;
        public VkSurfaceFormat SurfaceFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceVariablePointerFeatures
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 VariablePointersStorageBuffer;
        public VkBool32 VariablePointers;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryDedicatedRequirements
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 PrefersDedicatedAllocation;
        public VkBool32 RequiresDedicatedAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryDedicatedAllocateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkImage Image;
        public VkBuffer Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBufferMemoryRequirementsInfo2
    {
        public VkStructureType Type;
        public void* Next;
        public VkBuffer Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageMemoryRequirementsInfo2
    {
        public VkStructureType Type;
        public void* Next;
        public VkImage Image;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageSparseMemoryRequirementsInfo2
    {
        public VkStructureType Type;
        public void* Next;
        public VkImage Image;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryRequirements2
    {
        public VkStructureType Type;
        public void* Next;
        public VkMemoryRequirements MemoryRequirements;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSparseImageMemoryRequirements2
    {
        public VkStructureType Type;
        public void* Next;
        public VkSparseImageMemoryRequirements MemoryRequirements;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDebugReportCallbackCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDebugReportFlags Flags;
        public IntPtr Callback;
        public IntPtr UserData;

        public VkDebugReportCallbackCreateInfo(VkDebugReportFlags flags, DebugReportCallbackEXT callback)
        {
            Type = VkStructureType.DebugReportCallbackCreateInfo;
            Next = null;
            Flags = flags;
            Callback = Marshal.GetFunctionPointerForDelegate(callback);
            UserData = IntPtr.Zero;
        }
    }

    //
    // AMD
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineRasterizationStateRasterizationOrder
    {
        public VkStructureType Type;
        public void* Next;
        public VkRasterizationOrder RasterizationOrder;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDebugMarkerObjectNameInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDebugReportObjectType ObjectType;
        public ulong Object;
        public byte* ObjectName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDebugMarkerObjectTagInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDebugReportObjectType ObjectType;
        public ulong Object;
        public ulong Tagname;
        public Size TagSize;
        public void* Tag;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDebugMarkerMarkerInfo
    {
        public VkStructureType Type;
        public void* Next;
        public byte* MarkerName;
        public fixed float Color[4];
    }

    //
    // AMD
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDedicatedAllocationImageCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 DedicatedAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDedicatedAllocationBufferCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 DedicatedAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDedicatedAllocationMemoryAllocateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkImage Image;
        public VkBuffer Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkTextureLODGatherFormatProperties
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 SupportsTextureGatherLODBias;
    }

    //
    // KHX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkRenderPassMultiviewCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint SubpassCount;
        public uint* ViewMasks;
        public uint DependencyCount;
        public uint* ViewOffsets;
        public uint CorrelationMaskCount;
        public uint* CorrelationMasks;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceMultiviewFeatures
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 Multiview;
        public VkBool32 MultiviewGeometryShader;
        public VkBool32 MultiviewTessellationShader;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceMultiviewProperties
    {
        public VkStructureType Type;
        public void* Next;
        public uint MaxMultiviewViewCount;
        public uint MaxMultiviewInstanceIndex;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalImageFormatPropertiesNV
    {
        public VkImageFormatProperties ImageFormatProperties;
        public VkExternalMemoryFeatureFlagsNV ExternalMemoryFeatures;
        public VkExternalMemoryHandleTypeFlagsNV ExportFromImportedHandleTypes;
        public VkExternalMemoryHandleTypeFlagsNV CompatibleHandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExternalMemoryImageCreateInfoNV
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlagsNV HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportMemoryAllocateInfoNV
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlagsNV HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImportMemoryWin32HandleInfoNV
    {
        public VkStructureType Type;
        public void* Next;
        public VkExternalMemoryHandleTypeFlagsNV HandleType;
        public IntPtr Handle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkExportMemoryWin32HandleInfoNV
    {
        public VkStructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkWin32KeyedMutexAcquireReleaseInfoNV
    {
        public VkStructureType Type;
        public void* Next;
        public uint AcquireCount;
        public VkDeviceMemory* AcquireSyncs;
        public ulong* AcquireKeys;
        public uint* AcquireTimeoutMilliseconds;
        public uint ReleaseCount;
        public VkDeviceMemory* ReleaseSyncs;
        public ulong* ReleaseKeys;
    }

    //
    // KHX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMemoryAllocateFlagsInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkMemoryAllocateFlags Flags;
        public uint DeviceMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBindBufferMemoryInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBuffer Buffer;
        public VkDeviceMemory Memory;
        public VkDeviceSize MemoryOffset;
        public uint DeviceIndexCount;
        public uint* DeviceIndices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBindImageMemoryInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkImage Image;
        public VkDeviceMemory Memory;
        public VkDeviceSize MemoryOffset;
        public uint DeviceIndexCount;
        public uint* DeviceIndices;
        public uint SFRRectCount;
        public VkRect2D* SFRRects;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupRenderPassBeginInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint DeviceMask;
        public uint DeviceRenderAreaCount;
        public VkRect2D* DeviceRenderAreas;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupCommandBufferBeginInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint DeviceMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupSubmitInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public uint* WaitSemaphoreDeviceIndices;
        public uint CommandBufferCount;
        public uint* CommandBufferDeviceMasks;
        public uint SignalSemaphoreCount;
        public uint* SignalSemaphoreDeviceIndices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupBindSparseInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint ResourceDeviceIndex;
        public uint MemoryDeviceIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupPresentCapabilities
    {
        public VkStructureType Type;
        public void* Next;
        public fixed uint PresentMask[Vk.MaxDeviceGroupSize];
        public VkDeviceGroupPresentModeFlags Modes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkImageSwapchainCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSwapchain Swapchain;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkBindImageMemorySwapchainInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSwapchain Swapchain;
        public uint ImageIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkAcquireNextImageInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSwapchain Swapchain;
        public ulong Timeout;
        public VkSemaphore Semaphore;
        public VkFence Fence;
        public uint DeviceMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupPresentInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint SwapchainCount;
        public uint* DeviceMasks;
        public VkDeviceGroupPresentModeFlags Mode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupSwapchainCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceGroupPresentModeFlags Modes;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkValidationFlags
    {
        public VkStructureType Type;
        public void* Next;
        public uint DisabledValidationCheckCount;
        public VkValidationCheck* DisabledValidationChecks;
    }

    //
    // NN
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkViSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkViSurfaceCreateFlags Flags;
        public void* Window;
    }

    //
    // KHX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceGroupProperties
    {
        public VkStructureType Type;
        public void* Next;
        public uint PhysicalDeviceCount;
        public VkPhysicalDevice* PhysicalDevices;
        public VkBool32 SubsetAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGroupDeviceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint PhysicalDeviceCount;
        public VkPhysicalDevice* PhysicalDevices;
    }

    //
    // NVX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGeneratedCommandsFeatures
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 ComputeBindingPointSupport;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceGeneratedCommandsLimits
    {
        public VkStructureType Type;
        public void* Next;
        public uint MaxIndirectCommandsLayoutTokenCount;
        public uint MaxObjectEntryCounts;
        public uint MinSequenceCountBufferOffsetAlignment;
        public uint MinSequenceIndexBufferOffsetAlignment;
        public uint MinCommandsTokenBufferOffsetAlignment;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkIndirectCommandsToken
    {
        public VkIndirectCommandsTokenType TokenType;
        public VkBuffer Buffer;
        public VkDeviceSize Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkIndirectCommandsLayoutToken
    {
        public VkIndirectCommandsTokenType TokenType;
        public uint BindingUnit;
        public uint DynamicCount;
        public uint Divisor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkIndirectCommandsLayoutCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineBindPoint PipelineBindPoint;
        public VkIndirectCommandsLayoutUsageFlags Flags;
        public uint TokenCount;
        public VkIndirectCommandsLayoutToken* Tokens;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkCmdProcessCommandsInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkObjectTable ObjectTable;
        public VkIndirectCommandsLayout IndirectCommandsLayout;
        public uint IndirectCommandsTokenCount;
        public VkIndirectCommandsToken* IndirectCommandsTokens;
        public uint MaxSequencesCount;
        public VkCommandBuffer TargetCommandBuffer;
        public VkBuffer SequencesCountBuffer;
        public VkDeviceSize SequencesCountOffset;
        public VkBuffer SequencesIndexBuffer;
        public VkDeviceSize SequencesIndexOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkCmdReserveSpaceForCommandsInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkObjectTable ObjectTable;
        public VkIndirectCommandsLayout IndirectCommandsLayout;
        public uint MaxSequencesCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkObjectTableCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint ObjectCount;
        public VkObjectEntryType* ObjectEntryTypes;
        public uint* ObjectEntryCounts;
        public VkObjectEntryUsageFlags* pObjectEntryUsageFlags;
        public uint MaxUniformBuffersPerDescriptor;
        public uint MaxStorageBuffersPerDescriptor;
        public uint MaxStorageImagesPerDescriptor;
        public uint MaxSampledImagesPerDescriptor;
        public uint MaxPipelineLayouts;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkObjectTableEntry
    {
        public VkObjectEntryType Type;
        public VkObjectEntryUsageFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkObjectTablePipelineEntry
    {
        public VkObjectEntryType Type;
        public VkObjectEntryUsageFlags Flags;
        public VkPipeline Pipeline;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkObjectTableDescriptorSetEntry
    {
        public VkObjectEntryType Type;
        public VkObjectEntryUsageFlags Flags;
        public VkPipelineLayout PipelineLayout;
        public VkDescriptorSet DescriptorSet;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkObjectTableVertexBufferEntry
    {
        public VkObjectEntryType Type;
        public VkObjectEntryUsageFlags Flags;
        public VkBuffer Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkObjectTableIndexBufferEntry
    {
        public VkObjectEntryType Type;
        public VkObjectEntryUsageFlags Flags;
        public VkBuffer Buffer;
        public VkIndexType IndexType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkObjectTablePushantEntry
    {
        public VkObjectEntryType Type;
        public VkObjectEntryUsageFlags Flags;
        public VkPipelineLayout PipelineLayout;
        public VkShaderStageFlags StageFlags;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkViewportWScaling
    {
        public float XCoeff;
        public float YCoeff;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineViewportWScalingStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 ViewportWScalingEnable;
        public uint ViewportCount;
        public VkViewportWScaling* ViewportWScalings;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSurfaceCapabilities2EXT
    {
        public VkStructureType Type;
        public void* Next;
        public uint MinImageCount;
        public uint MaxImageCount;
        public VkExtent2D CurrentExtent;
        public VkExtent2D MinImageExtent;
        public VkExtent2D MaxImageExtent;
        public uint MaxImageArrayLayers;
        public VkSurfaceTransformFlags SupportedTransforms;
        public VkSurfaceTransformFlags CurrentTransform;
        public VkCompositeAlphaFlags SupportedCompositeAlpha;
        public VkImageUsageFlags SupportedUsageFlags;
        public VkSurfaceCounterFlags SupportedSurfaceCounters;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayPowerInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDisplayPowerState PowerState;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDeviceEventInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDeviceEventType DeviceEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkDisplayEventInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkDisplayEventType DisplayEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSwapchainCounterCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSurfaceCounterFlags SurfaceCounters;
    }

    //
    // GOOGLE
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkRefreshCycleDuration
    {
        public ulong RefreshDuration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPastPresentationTiming
    {
        public uint PresentID;
        public ulong DesiredPrensetTime;
        public ulong ActualPresentTime;
        public ulong EarliestPresentTime;
        public ulong PresentMargin;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPresentTime
    {
        public uint PresentID;
        public ulong DesiredPresentTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPresentTimesInfo
    {
        public VkStructureType Type;
        public void* Next;
        public uint SwapchainCount;
        public VkPresentTime* Times;
    }

    //
    // NVX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceMultiviewPerViewAttributesProperties
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 PerViewPositionAllComponents;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkViewportSwizzle
    {
        public VkViewportCoordinateSwizzle X;
        public VkViewportCoordinateSwizzle Y;
        public VkViewportCoordinateSwizzle Z;
        public VkViewportCoordinateSwizzle W;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineViewportSwizzleStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineViewportSwizzleStateCreateFlags Flags;
        public uint ViewportCount;
        public VkViewportSwizzle* ViewportSwizzles;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceDiscardRectangleProperties
    {
        public VkStructureType Type;
        public void* Next;
        public uint MaxDiscardRectangles;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineDiscardRectangleStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineDiscardRectangleStateCreateFlags Flags;
        public VkDiscardRectangleMode DiscardRectangleMode;
        public uint DiscardRectangleCount;
        public VkRect2D* DiscardRectangles;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkXYColor
    {
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkHdrMetadata
    {
        public VkStructureType Type;
        public void* Next;
        public VkXYColor DisplayPrimaryRed;
        public VkXYColor DisplayPrimaryGreen;
        public VkXYColor DisplayPrimaryBlue;
        public VkXYColor WhitePoint;
        public float MaxLuminance;
        public float MinLuminance;
        public float MaxContentLightLevel;
        public float MaxFrameAverageLightLevel;
    }

    //
    // MVK
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkIOSSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkIOSSurfaceCreateFlags Flags;
        public void* View;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkMacOSSurfaceCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkMacOSSurfaceCreateFlags Flags;
        public void* View;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkSamplerReductionModeCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkSamplerReductionMode ReductionMode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceSamplerFilterMinmaxProperties
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 FilterMinmaxSingleComponentFormats;
        public VkBool32 FilterMinmaxImageComponentMapping;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceBlendOperationAdvancedFeatures
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 AdvancedBlendCoherentOperations;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPhysicalDeviceBlendOperationAdvancedProperties
    {
        public VkStructureType Type;
        public void* Next;
        public uint AdvancedBlendMaxColorAttachments;
        public VkBool32 AdvancedBlendIndependentBlend;
        public VkBool32 AdvancedBlendNonPremultipliedSrcColor;
        public VkBool32 AdvancedBlendNonPremultipliedDstColor;
        public VkBool32 AdvancedBlendCorrelatedOverlap;
        public VkBool32 AdvancedBlendAllOperations;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineColorBlendAdvancedStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkBool32 SrcPremultiplied;
        public VkBool32 DstPremultiplied;
        public VkBlendOverlap BlendOverlap;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineCoverageToColorStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineCoverageToColorStateCreateFlags Flags;
        public VkBool32 CoverageToColorEnable;
        public uint CoverageToColorLocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VkPipelineCoverageModulationStateCreateInfo
    {
        public VkStructureType Type;
        public void* Next;
        public VkPipelineCoverageModulationStateCreateFlags flags;
        public VkCoverageModulationMode CoverageModulationMode;
        public VkBool32 CoverageModulationTableEnable;
        public uint CoverageModulationTableCount;
        public float* CoverageModulationTable;
    }
}