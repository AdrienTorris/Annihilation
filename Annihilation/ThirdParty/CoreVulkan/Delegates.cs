namespace Vulkan
{
    public unsafe delegate void* VkAllocationFunction(void* userData, Size size, Size alignment, VkSystemAllocationScope VkAllocationScope);
    public unsafe delegate void* VkReallocationFunction(void* userData, void* original, Size size, Size alignment, VkSystemAllocationScope VkAllocationScope);
    public unsafe delegate void VkFreeFunction(void* userData, void* memory);
    public unsafe delegate void VkInternalAllocationNotification(void* userData, Size size, VkInternalAllocationType VkAllocationType, VkSystemAllocationScope VkAllocationScope);
    public unsafe delegate void VkInternalFreeNotification(void* userData, Size size, VkInternalAllocationType VkAllocationType, VkSystemAllocationScope VkAllocationScope);
    public unsafe delegate void VkVoidFunction();
}