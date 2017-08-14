using System;

namespace Engine.Vulkan.Allocator
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

            MemoryAllocatorNative.CreateImage(Handle, createInfo, memoryRequirements, out image, allocation, UIntPtr.Zero).CheckError();
        }
    }
}