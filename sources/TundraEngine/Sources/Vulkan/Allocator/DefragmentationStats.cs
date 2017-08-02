namespace TundraEngine.Vulkan.Allocator
{
    /// <summary>
    /// Statistics returned by function <see cref="Allocator.Defragment"/>
    /// </summary>
    public struct DefragmentationStats
    {
        /// <summary>
        /// Total number of bytes that have been copied while moving allocations to different places.
        /// </summary>
        public DeviceSize BytesMoved;
        /// <summary>
        /// Total number of bytes that have been released to the system by freeing empty <see cref="DeviceMemory"/> objects.
        /// </summary>
        public DeviceSize BytesFreed;
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