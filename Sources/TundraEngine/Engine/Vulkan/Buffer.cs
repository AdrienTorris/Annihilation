using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Engine.Vulkan
{
    public unsafe struct BufferCreateInfo
    {
        public StructureType type;
        public void* Next;
        public BufferCreateFlags Flags;
        public DeviceSize Size;
        public BufferUsageFlags Usage;
        public SharingMode SharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
    }

    public struct Buffer
    {
        internal ulong NativeHandle;
    }
}