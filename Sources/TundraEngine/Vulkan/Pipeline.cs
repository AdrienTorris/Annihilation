using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Vulkan
{

    [StructLayout(LayoutKind.Sequential)]
    public struct SpecializationMapEntry
    {
        public uint ConstantId;
        public uint Offset;
        public ulong Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpecializationInfo
    {
        public uint MapEntryCount;
        public SpecializationMapEntry* MapEntries;
        public ulong DataSize;
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
        public byte* Name;
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
    
    public struct Pipeline
    {
        internal ulong NativeHandle;
    }
}
