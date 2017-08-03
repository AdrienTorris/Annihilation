using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Engine.Vulkan
{
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

    [Flags]
    public enum PipelineCreateFlags : uint
    {
        DisableOptimization = 0x00000001,
        AllowDerivatives = 0x00000002,
        Derivative = 0x00000004,
        ViewIndexFromDeviceIndex = 0x00000008,
        DispatchBase = 0x00000010
    }

    [Flags] public enum PipelineShaderStageCreateFlags : uint { }

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

    [Flags] public enum PipelineVertexInputStateCreateFlags : uint { }
    [Flags] public enum PipelineInputAssemblyStateCreateFlags : uint { }
    [Flags] public enum PipelineTessellationStateCreateFlags : uint { }
    [Flags] public enum PipelineViewportStateCreateFlags : uint { }
    [Flags] public enum PipelineRasterizationStateCreateFlags : uint { }
    
    [Flags]
    public enum CullModeFlags : uint
    {
        None = 0,
        Front = 0x00000001,
        Back = 0x00000002,
        FrontAndBack = 0x00000003
    }

    [Flags] public enum PipelineMultisampleStateCreateFlags : uint { }
    [Flags] public enum PipelineDepthStencilStateCreateFlags : uint { }
    [Flags] public enum PipelineColorBlendStateCreateFlags : uint { }

    [Flags]
    public enum ColorComponentFlags : uint
    {
        R = 0x00000001,
        G = 0x00000002,
        B = 0x00000004,
        A = 0x00000008
    }

    [Flags] public enum PipelineDynamicStateCreateFlags : uint { }
    [Flags] public enum PipelineLayoutCreateFlags : uint { }

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
