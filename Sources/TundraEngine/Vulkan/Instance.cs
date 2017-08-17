using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Vulkan
{
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

    [Flags] public enum Win32SurfaceCreateFlags : uint { }
    
    public unsafe struct Win32SurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public Win32SurfaceCreateFlags Flags;
        public IntPtr hinstance;
        public IntPtr hwnd;
    }

    public unsafe delegate Bool32 DebugReportCallbackDelegate(
        DebugReportFlags flags,
        DebugReportObjectType objectType,
        ulong objectHandle,
        ulong location,
        int messageCode,
        byte* layerPrefix,
        byte* message,
        void* userData
        );

    public unsafe struct DebugReportCallbackCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DebugReportFlags Flags;
        public DebugReportCallbackDelegate Callback;
        public void* UserData;
    }
    
}