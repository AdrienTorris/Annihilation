namespace Vulkan.MemoryAllocator
{
    public struct AllocatorStatInfo
    {
        public uint AllocationCount;
        public uint SuballocationCount;
        public uint UnusedRangeCount;
        public DeviceSize UsedBytes;
        public DeviceSize UnusedBytes;
        public DeviceSize SuballocationSizeMin;
        public DeviceSize SuballocationSizeAvg;
        public DeviceSize SuballocationSizeMax;
        public DeviceSize UnusedRangeSizeMin;
        public DeviceSize UnusedRangeSizeAvg;
        public DeviceSize UnusedRangeSizeMax;
    }

    /// <summary>
    /// General statistics from current state of <see cref="MemoryAllocator"/>.
    /// </summary>
    public struct AllocatorStats
    {
        public AllocatorStatInfo[] MemoryType;
        public AllocatorStatInfo[] MemoryHeap;
        public AllocatorStatInfo Total;
    }
}