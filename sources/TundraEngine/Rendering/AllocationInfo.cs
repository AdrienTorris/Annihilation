using System;

using SharpVk;

namespace TundraEngine.Rendering
{
    /// <summary>
    /// Parameters of <see cref="Allocation"/> objects, that can be retrieved using function <see cref="Allocator.GetAllocationInfo"/>.
    /// </summary>
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
        /// <para/> It can change after call to <see cref="Allocator.Defragment"/> if this allocation is passed to the function.
        /// </summary>
        public DeviceMemory DeviceMemory;
        /// <summary>
        /// Offset into <see cref="DeviceMemory"/> object to the beginning of this allocation, in bytes. (<see cref="DeviceMemory"/>, <see cref="Offset"/>) pair is unique to this allocation.
        /// <para/> It can change after call to <see cref="Allocator.Defragment"/> if this allocation is passed to the function.
        /// </summary>
        public DeviceSize Offset;
        /// <summary>
        /// Size of the allocation, in bytes.
        /// <para/> It never changes.
        /// </summary>
        public DeviceSize Size;
        /// <summary>
        /// Pointer to the beginning of this allocation as mapped data. <see cref="IntPtr.Zero"/> if this alloaction is not persistently mapped.
        /// <para/> It can change after call to <see cref="Allocator.UnmapPersistentlyMappedMemory"/>, <see cref="Allocator.MapPersistentlyMappedMemory"/>.
        /// <para/> It can also change after call to <see cref="Allocator.Defragment"/> if this allocation is passed to the function.
        /// </summary>
        public IntPtr MappedData;
        /// <summary>
        /// Custom general-purpose pointer that was passed as <see cref="MemoryRequirements.UserData"/> or set using <see cref="Allocator.SetAllocationUserData"/>.
        /// <para/> It can change after call to <see cref="Allocator.SetAllocationUserData"/> for this allocation.
        /// </summary>
        public IntPtr UserData;
    }
}