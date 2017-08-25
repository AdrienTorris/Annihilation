using System;
using System.Runtime.InteropServices;

namespace Vulkan.MemoryAllocator
{
    public static partial class Vma
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DeviceMemoryCallbacks
        {
            public IntPtr Allocate; // AllocateDeviceMemoryFunction
            public IntPtr Free; // FreeDeviceMemoryFunction
        }

        /// <summary>
        /// Description of a <see cref="Allocator"/> to be created.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AllocatorCreateInfo
        {
            /// <summary>
            /// Flags for created allocator.
            /// </summary>
            public AllocatorFlags Flags;
            /// <summary>
            /// Vulkan physical device.
            /// <para/> It must be valid throughout whole lifetime of created Allocator.
            /// </summary>
            public Vk.PhysicalDevice PhysicalDevice;
            /// <summary>
            /// Vulkan device.
            /// <para/> It must be valid throughout whole lifetime of created Allocator.
            /// </summary>
            public Vk.Device Device;
            /// <summary>
            /// Size of a single memory block to allocate for resources.
            /// <para/> Set to 0 to use default, which is currently 256 MB.
            /// </summary>
            public Vk.DeviceSize PreferredLargeHeapBlockSize;
            /// <summary>
            /// Size of a single memory block to allocate for resources from a small heap &lt 512 MB.
            /// <para/> Set to 0 to use default, which is currently 64 MB.
            /// </summary>
            public Vk.DeviceSize PreferredSmallHeapBlockSize;
            /// <summary>
            /// Custom allocation callbacks.
            /// <para/> Optional, can be null. When specified, will also be used for all CPU-side memory allocations.
            /// </summary>
            public Vk.AllocationCallbacks* AllocationCallbacks;
            /// <summary>
            /// Informative callbacks for <see cref="AllocateMemory"/>, <see cref="FreeMemory"/>
            /// <para/> Optional, can be null.
            /// </summary>
            public DeviceMemoryCallbacks* DeviceMemoryCallbacks;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StatInfo
        {
            public uint AllocationCount;
            public uint SuballocationCount;
            public uint UnusedRangeCount;
            public Vk.DeviceSize UsedBytes;
            public Vk.DeviceSize UnusedBytes;
            public Vk.DeviceSize SuballocationSizeMin, SuballocationSizeAvg, SuballocationSizeMax;
            public Vk.DeviceSize UnusedRangeSizeMin, UnusedRangeSizeAvg, UnusedRangeSizeMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Stats
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Vk.MaxMemoryTypes)]
            public StatInfo[] MemoryType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Vk.MaxMemoryTypes)]
            public StatInfo[] MemoryHeap;
            public StatInfo Total;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryRequirements
        {
            public MemoryRequirementFlags Flags;
            public MemoryUsage Usage;
            public Vk.MemoryPropertyFlags RequiredFlags;
            public Vk.MemoryPropertyFlags PreferredFlags;
            public IntPtr UserData;
        }

        /// <summary>
        /// Parameters of <see cref="Allocation"/> objects, that can be retrieved using function <see cref="MemoryAllocator.GetAllocationInfo"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct AllocationInfo
        {
            /// <summary>
            /// Memory type index that this allocation was allocated from.
            /// <para/> It never changes.
            /// </summary>
            public uint MemoryType;
            /// <summary>
            /// Handle to Vulkan memory object.
            /// <para/> Same memory object can be shared by multiple allocations.
            /// <para/> It can change after call to <see cref="MemoryAllocator.Defragment"/> if this allocation is passed to the function.
            /// </summary>
            public Vk.DeviceMemory DeviceMemory;
            /// <summary>
            /// Offset into <see cref="DeviceMemory"/> object to the beginning of this allocation, in bytes. (<see cref="DeviceMemory"/>, <see cref="Offset"/>) pair is unique to this allocation.
            /// <para/> It can change after call to <see cref="MemoryAllocator.Defragment"/> if this allocation is passed to the function.
            /// </summary>
            public Vk.DeviceSize Offset;
            /// <summary>
            /// Size of the allocation, in bytes.
            /// <para/> It never changes.
            /// </summary>
            public Vk.DeviceSize Size;
            /// <summary>
            /// Pointer to the beginning of this allocation as mapped data. <see cref="IntPtr.Zero"/> if this alloaction is not persistently mapped.
            /// <para/> It can change after call to <see cref="MemoryAllocator.UnmapPersistentlyMappedMemory"/>, <see cref="MemoryAllocator.MapPersistentlyMappedMemory"/>.
            /// <para/> It can also change after call to <see cref="MemoryAllocator.Defragment"/> if this allocation is passed to the function.
            /// </summary>
            public IntPtr MappedData;
            /// <summary>
            /// Custom general-purpose pointer that was passed as <see cref="MemoryRequirements.UserData"/> or set using <see cref="MemoryAllocator.SetAllocationUserData"/>.
            /// <para/> It can change after call to <see cref="MemoryAllocator.SetAllocationUserData"/> for this allocation.
            /// </summary>
            public IntPtr UserData;
        }

        /// <summary>
        /// Optional configuration parameters to be passed to function <see cref="MemoryAllocator.Defragment"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DefragmentationInfo
        {
            /// <summary>
            /// Maximum total numbers of bytes that can be copied while moving allocations to different places.
            /// <para/> Default is <see cref="Vk.WholeSize"/>, which means no limit.
            /// </summary>
            public Vk.DeviceSize MaxBytesToMove;
            /// <summary>
            /// Maximum number of allocations that can be moved to different place.
            /// <para/> Default is <see cref="uint.MaxValue"/>, which means no limit.
            /// </summary>
            public uint MaxAllocationsToMove;
        }

        /// <summary>
        /// Statistics returned by function <see cref="MemoryAllocator.Defragment"/>
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DefragmentationStats
        {
            /// <summary>
            /// Total number of bytes that have been copied while moving allocations to different places.
            /// </summary>
            public Vk.DeviceSize BytesMoved;
            /// <summary>
            /// Total number of bytes that have been released to the system by freeing empty <see cref="DeviceMemory"/> objects.
            /// </summary>
            public Vk.DeviceSize BytesFreed;
            /// <summary>
            /// Number of allocations that have been moved to different places.
            /// </summary>
            public uint AllocationsMoved;
            /// <summary>
            /// Number of empty <see cref="DeviceMemory"/> objects that have been released to the system.
            /// </summary>
            public uint DeviceMemoryBlocksFreed;
        }
    }
}