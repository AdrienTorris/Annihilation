using System;

namespace CoreVulkan.Allocator
{
    /// <summary>
    /// Flags for created Allocator
    /// </summary>
    [Flags]
    public enum AllocatorFlags : uint
    {
        /// <summary>
        /// Allocator and all objects created from it will not be synchronized internally, so you must guarantee they are used from only one thread at a time or synchronized externally by you.
        /// <para/> Using this flag may increase performance because internal mutexes are not used.
        /// </summary>
        ExternallySynchronized = 0x00000001,
        MaxEnum = 0x7FFFFFFF
    }

    /// <summary>
    /// Description of a <see cref="Allocator"/> to be created.
    /// </summary>
    public struct AllocatorCreateInfo
    {
        /// <summary>
        /// Flags for created allocator.
        /// </summary>
        public AllocatorFlags Flags;
        /// <summary>
        /// Vulkan physical device.
        /// <para/> It must be valid throughout whole lifetime of created Allocator.
        /// </summary>
        public PhysicalDevice PhysicalDevice;
        /// <summary>
        /// Vulkan device.
        /// <para/> It must be valid throughout whole lifetime of created Allocator.
        /// </summary>
        public Device Device;
        /// <summary>
        /// Size of a single memory block to allocate for resources.
        /// <para/> Set to 0 to use default, which is currently 256 MB.
        /// </summary>
        public DeviceSize PreferredLargeHeapBlockSize;
        /// <summary>
        /// Size of a single memory block to allocate for resources from a small heap &lt 512 MB.
        /// <para/> Set to 0 to use default, which is currently 64 MB.
        /// </summary>
        public DeviceSize PreferredSmallHeapBlockSize;
        /// <summary>
        /// Custom allocation callbacks.
        /// <para/> Optional, can be null. When specified, will also be used for all CPU-side memory allocations.
        /// </summary>
        public AllocationCallbacks AllocationCallbacks;
        /// <summary>
        /// Informative callbacks for <see cref="AllocateMemory"/>, <see cref="FreeMemory"/>
        /// <para/> Optional, can be null.
        /// </summary>
        public DeviceMemoryCallbacks DeviceMemoryCallbacks;
    }
}