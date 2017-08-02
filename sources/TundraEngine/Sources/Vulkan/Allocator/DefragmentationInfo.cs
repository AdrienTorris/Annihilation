namespace TundraEngine.Vulkan.Allocator
{
    /// <summary>
    /// Optional configuration parameters to be passed to function <see cref="Allocator.Defragment"/>.
    /// </summary>
    public struct DefragmentationInfo
    {
        /// <summary>
        /// Maximum total numbers of bytes that can be copied while moving allocations to different places.
        /// <para/> Default is <see cref="Vulkan.WholeSize"/>, which means no limit.
        /// </summary>
        public DeviceSize MaxBytesToMove;
        /// <summary>
        /// Maximum number of allocations that can be moved to different place.
        /// <para/> Default is <see cref="uint.MaxValue"/>, which means no limit.
        /// </summary>
        public uint MaxAllocationsToMove;
    }
}