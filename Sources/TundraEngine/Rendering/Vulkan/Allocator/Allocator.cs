using System;

namespace TundraEngine.Rendering.Vulkan.Allocator
{
    public class Allocator
    {
        public UIntPtr Handle;

        public Allocator(ref AllocatorCreateInfo createInfo)
        {
            MemoryAllocatorNative.CreateAllocator(createInfo, out Handle);
        }

        public void Destroy()
        {
            MemoryAllocatorNative.DestroyAllocator(Handle);
        }

        public void CreateImage(
            ref ImageCreateInfo createInfo, 
            ref MemoryRequirements memoryRequirements, 
            out Image image)
        {
            UIntPtr allocation = UIntPtr.Zero;

            MemoryAllocatorNative.CreateImage(Handle, createInfo, memoryRequirements, out Image imageRaw, allocation, UIntPtr.Zero);
            
        }
    }
}