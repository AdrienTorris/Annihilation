using System;

namespace Vulkan.MemoryAllocator
{
    public enum MemoryUsage : uint
    {
        /// <summary>
        /// No intended memory usage specified.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Memory will be used on device only, no need to be mapped on host.
        /// </summary>
        GpuOnly = 1,
        /// <summary>
        /// Memory will be mapped on host. Could be used for transfer to device.
        /// <para/> Guarantees to be <see cref="MemoryPropertyFlags.HostVisible"/> and <see cref="MemoryPropertyFlags.HostCoherent"/>.
        /// </summary>
        CpuOnly = 2,
        /// <summary>
        /// Memory will be used for frequent (dynamic) updates from host and reads on device.
        /// <para/> Guarantees to be <see cref="MemoryPropertyFlags.HostVisible"/>.
        /// </summary>
        CpuToGpu = 3,
        /// <summary>
        /// Memory will be used for writing on device and readback on host.
        /// <para/> Guarantees to be <see cref="MemoryPropertyFlags.HostVisible"/>.
        /// </summary>
        GpuToCpu = 4,
        MaxEnum = 0x7FFFFFFF
    }

    /// <summary>
    /// Flags to be passed as <see cref="MemoryRequirements.Flags"/>.
    /// </summary>
    [Flags]
    public enum MemoryRequirementFlags : uint
    {
        /// <summary>
        /// Set this flag if the allocation should have its own memory block.
        /// <para/> Use it for special, big resources, like fullscreen images used as attachments.
        /// <para/> This flag must also be used for host visible resources that you want to map simultaneously because otherwise they might end up as regions of the same <see cref="DeviceMemory"/>, while mapping same <see cref="DeviceMemory"/> multiple times is illegal.
        /// </summary>
        OwnMemory = 0x00000001,
        /// <summary>
        /// Set this flag to only try to allocate from existing <see cref="DeviceMemory"/> blocks and never create new such block.
        /// <para/> If new allocation cannot be placed in any of the existing blocks, allocation fails with <see cref="Result.ErrorOutOfDeviceMemory"/> error.
        /// <para/> It makes no sense to set <see cref="OwnMemory"/> and <see cref="NeverAllocate"/> at the same time.
        /// </summary>
        NeverAllocate = 0x00000002,
        /// <summary>
        /// Set to use a memory that will be persistently mapped and retrieve pointer to it.
        /// <para/> Pointer to mapped memory will be returned through <see cref="AllocationInfo.MappedData"/>. You cannot map the memory on your own as multiple maps of a single <see cref="DeviceMemory"/> are illegal.
        /// </summary>
        PersistentMap = 0x00000004,
        MaxEnum = 0x7FFFFFFF
    }

    public struct MemoryRequirements
    {
        public MemoryRequirementFlags Flags;
        /// <summary>
        /// Intended usage of memory.
        /// <para/> Leave <see cref="MemoryUsage.Unknown"/> if you specify <see cref="RequiredFlags"/>. You can also use both.
        /// </summary>
        public MemoryUsage Usage;
        /// <summary>
        /// Flags that must be set in a Memory Type chosen for an allocation.
        /// <para/> Leave 0 if you specify requirement via usage.
        /// </summary>
        public MemoryPropertyFlags RequiredFlags;
        /// <summary>
        /// Flags that preferably should be set in a Memory Type chosen for an allocation.
        /// <para/> Set to 0 if no additional flags are prefered and only <see cref="RequiredFlags"/> should be used. If not 0, it must be a superset or equal to <see cref="RequiredFlags"/>.
        /// </summary>
        public MemoryPropertyFlags PreferredFlags;
        /// <summary>
        /// Custom general-purpose pointer that will be stored in <see cref="Allocation"/>, can be read as <see cref="AllocationInfo.UserData"/> and changed using <see cref="MemoryAllocator.SetAllocationUserData"/>.
        /// </summary>
        public IntPtr UserData;
    }
}