using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.Vulkan
{
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

    public struct CommandBuffer
    {
        public IntPtr NativeHandle;
    }
}