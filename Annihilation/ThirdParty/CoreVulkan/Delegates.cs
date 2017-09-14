namespace Vulkan
{
    public unsafe delegate void* AllocationFunction(void* userData, Size size, Size alignment, SystemAllocationScope AllocationScope);
    public unsafe delegate void* ReallocationFunction(void* userData, void* original, Size size, Size alignment, SystemAllocationScope AllocationScope);
    public unsafe delegate void FreeFunction(void* userData, void* memory);
    public unsafe delegate void InternalAllocationNotification(void* userData, Size size, InternalAllocationType AllocationType, SystemAllocationScope AllocationScope);
    public unsafe delegate void InternalFreeNotification(void* userData, Size size, InternalAllocationType AllocationType, SystemAllocationScope AllocationScope);
    public unsafe delegate void VoidFunction();
}