using System;
using System.Diagnostics;

namespace Vulkan.MemoryAllocator
{
    public static partial class Vma
    {
        //
        // Handles
        //
        public class Allocator
        {
            public IntPtr Handle;
        }

        //
        // Objects
        //
        public struct Suballocation
        {
            public Vk.DeviceSize Offset;
            public Vk.DeviceSize Size;
        }

        public unsafe class Block
        {
            public uint MemoryTypeIndex;
            public BlockVectorType BlockVectorType;
            public Vk.DeviceMemory Memory;
            public Vk.DeviceSize Size;
            public bool PersistentMap;
            public void* MappedData;
            public uint FreeCount;
            public Vk.DeviceSize SumFreeSize;
            public 
        }

        public class AllocatorType
        {
            public bool UseMutex;
            public Vk.Device Device;
            public bool AllocationCallbackSpecified;
            public Vk.AllocationCallbacks AllocationCallbacks;
            public DeviceMemoryCallbacks DeviceMemoryCallbacks;
            public Vk.DeviceSize PreferredLargeHeapBlockSize;
            public Vk.DeviceSize PreferredSmallHeapBlockSize;

            public uint UnmapPersistentlyMappedMemoryCounter;

            public Vk.PhysicalDeviceProperties PhysicalDeviceProperties;
            public Vk.PhysicalDeviceMemoryProperties MemProps;

            public BlockVector
        }

        public static unsafe Vk.Result CreateAllocator(ref AllocatorCreateInfo createInfo, out Allocator allocator)
        {
            allocator = 
        }
    }
}