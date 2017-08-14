using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Engine.Vulkan
{
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

    public struct Image
    {
        internal ulong NativeHandle;
    }
}
