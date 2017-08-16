using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Vulkan
{

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

    public unsafe struct PhysicalDeviceMemoryProperties
    {
        public uint MemoryTypeCount;
        /// <summary>
        /// Buffer of size <see cref="Vulkan.MaxMemoryTypes"/>.
        /// </summary>
        public IntPtr MemoryTypes;
        public uint MemoryHeapCount;
        /// <summary>
        /// Buffer of size <see cref="Vulkan.MaxMemoryHeaps"/>.
        /// </summary>
        public IntPtr MemoryHeaps;
    }
}