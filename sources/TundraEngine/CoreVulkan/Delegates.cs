using System;
using System.Runtime.InteropServices;

using Buffer = CoreVulkan.Handle.Buffer;

namespace CoreVulkan
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void* AllocationFunction(void* userData, Size size, Size alignment, SystemAllocationScope allocationScope);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void* ReallocationFunction(void* userData, void* original, Size size, Size alignment, SystemAllocationScope allocationScope);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void FreeFunction(void* userData, void* memory);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void InternalAllocationNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void InternalFreeNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void VoidFunction();
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateInstance(ref InstanceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Instance instance);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyInstance(Vulkan.Instance instance, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumeratePhysicalDevices(Vulkan.Instance instance, out uint physicalDeviceCount, Vulkan.PhysicalDevice[] physicalDevices);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFeatures(Vulkan.PhysicalDevice physicalDevice, out PhysicalDeviceFeatures features);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFormatProperties(Vulkan.PhysicalDevice physicalDevice, Format format, out FormatProperties formatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceImageFormatProperties(Vulkan.PhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceProperties(Vulkan.PhysicalDevice physicalDevice, out PhysicalDeviceProperties properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceQueueFamilyProperties(Vulkan.PhysicalDevice physicalDevice, out uint queueFamilyPropertyCount, QueueFamilyProperties[] queueFamilyProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceMemoryProperties(Vulkan.PhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties memoryProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate IntPtr GetInstanceProcAddr(Vulkan.Instance instance, Text name);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate IntPtr GetDeviceProcAddr(Vulkan.Device device, Text name);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDevice(Vulkan.PhysicalDevice physicalDevice, ref DeviceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Device device);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDevice(Vulkan.Device device, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateInstanceExtensionProperties(Text layerName, out uint propertyCount, ExtensionProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateDeviceExtensionProperties(Vulkan.PhysicalDevice physicalDevice, Text layerName, out uint propertyCount, ExtensionProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateInstanceLayerProperties(out uint propertyCount, LayerProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateDeviceLayerProperties(Vulkan.PhysicalDevice physicalDevice, out uint propertyCount, LayerProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetDeviceQueue(Vulkan.Device device, uint queueFamilyIndex, uint queueIndex, out Vulkan.Queue queue);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueueSubmit(Vulkan.Queue queue, uint submitCount, SubmitInfo[] submits, Vulkan.Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueueWaitIdle(Vulkan.Queue queue);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DeviceWaitIdle(Vulkan.Device device);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AllocateMemory(Vulkan.Device device, ref MemoryAllocateInfo allocateInfo, ref AllocationCallbacks allocator, out Vulkan.DeviceMemory memory);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void FreeMemory(Vulkan.Device device, Vulkan.DeviceMemory memory, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result MapMemory(Vulkan.Device device, Vulkan.DeviceMemory memory, DeviceSize offset, DeviceSize size, MemoryMapFlags flags, void** data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void UnmapMemory(Vulkan.Device device, Vulkan.DeviceMemory memory);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result FlushMappedMemoryRanges(Vulkan.Device device, uint memoryRangeCount, MappedMemoryRange[] memoryRanges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result InvalidateMappedMemoryRanges(Vulkan.Device device, uint memoryRangeCount, MappedMemoryRange[] memoryRanges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetDeviceMemoryCommitment(Vulkan.Device device, Vulkan.DeviceMemory memory, out DeviceSize committedMemoryInBytes);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindBufferMemory(Vulkan.Device device, Buffer buffer, Vulkan.DeviceMemory memory, DeviceSize memoryOffset);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindImageMemory(Vulkan.Device device, Vulkan.Image image, Vulkan.DeviceMemory memory, DeviceSize memoryOffset);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetBufferMemoryRequirements(Vulkan.Device device, Buffer buffer, out MemoryRequirements memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageMemoryRequirements(Vulkan.Device device, Vulkan.Image image, out MemoryRequirements memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageSparseMemoryRequirements(Vulkan.Device device, Vulkan.Image image, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements[] sparseMemoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceSparseImageFormatProperties(Vulkan.PhysicalDevice physicalDevice, Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, out uint propertyCount, SparseImageFormatProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueueBindSparse(Vulkan.Queue queue, uint bindInfoCount, ref BindSparseInfo bindInfo, Vulkan.Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateFence(Vulkan.Device device, ref FenceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyFence(Vulkan.Device device, Vulkan.Fence fence, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetFences(Vulkan.Device device, uint fenceCount, Vulkan.Fence[] fences);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetFenceStatus(Vulkan.Device device, Vulkan.Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result WaitForFences(Vulkan.Device device, uint fenceCount, Vulkan.Fence[] fences, Bool32 waitAll, ulong timeout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSemaphore(Vulkan.Device device, ref SemaphoreCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Semaphore semaphore);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySemaphore(Vulkan.Device device, Vulkan.Semaphore semaphore, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateEvent(Vulkan.Device device, ref EventCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Event evnt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyEvent(Vulkan.Device device, Vulkan.Event evt, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetEventStatus(Vulkan.Device device, Vulkan.Event evt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result SetEvent(Vulkan.Device device, Vulkan.Event evt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetEvent(Vulkan.Device device, Vulkan.Event evt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateQueryPool(Vulkan.Device device, ref QueryPoolCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.QueryPool queryPool);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyQueryPool(Vulkan.Device device, Vulkan.QueryPool queryPool, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetQueryPoolResults(Vulkan.Device device, Vulkan.QueryPool queryPool, uint firstQuery, uint queryCount, Size dataSize, void* data, DeviceSize stride, QueryResultFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateBuffer(Vulkan.Device device, ref BufferCreateInfo createInfo, ref AllocationCallbacks allocator, out Buffer buffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyBuffer(Vulkan.Device device, Buffer buffer, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateBufferView(Vulkan.Device device, ref BufferViewCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.BufferView view);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyBufferView(Vulkan.Device device, Vulkan.BufferView bufferView, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateImage(Vulkan.Device device, ref ImageCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Image image);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyImage(Vulkan.Device device, Vulkan.Image image, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageSubresourceLayout(Vulkan.Device device, Vulkan.Image image, ref ImageSubresource subresource, out SubresourceLayout layout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateImageView(Vulkan.Device device, ref ImageViewCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.ImageView view);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyImageView(Vulkan.Device device, Vulkan.ImageView imageView, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateShaderModule(Vulkan.Device device, ref ShaderModuleCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.ShaderModule shaderModule);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyShaderModule(Vulkan.Device device, Vulkan.ShaderModule shaderModule, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreatePipelineCache(Vulkan.Device device, ref PipelineCacheCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.PipelineCache pipelineCache);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyPipelineCache(Vulkan.Device device, Vulkan.PipelineCache pipelineCache, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPipelineCacheData(Vulkan.Device device, Vulkan.PipelineCache pipelineCache, out Size dataSize, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result MergePipelineCaches(Vulkan.Device device, Vulkan.PipelineCache dstCache, uint srcCacheCount, ref Vulkan.PipelineCache[] srcCaches);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateGraphicsPipelines(Vulkan.Device device, Vulkan.PipelineCache pipelineCache, uint createInfoCount, GraphicsPipelineCreateInfo[] createInfos, ref AllocationCallbacks allocator, Vulkan.Pipeline[] pipelines);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateComputePipelines(Vulkan.Device device, Vulkan.PipelineCache pipelineCache, uint createInfoCount, ComputePipelineCreateInfo[] createInfos, ref AllocationCallbacks allocator, Vulkan.Pipeline[] pipelines);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyPipeline(Vulkan.Device device, Vulkan.Pipeline pipeline, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreatePipelineLayout(Vulkan.Device device, ref PipelineLayoutCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.PipelineLayout pipelineLayout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyPipelineLayout(Vulkan.Device device, Vulkan.PipelineLayout pipelineLayout, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSampler(Vulkan.Device device, ref SamplerCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Sampler sampler);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySampler(Vulkan.Device device, Vulkan.Sampler sampler, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDescriptorSetLayout(Vulkan.Device device, ref DescriptorSetLayoutCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.DescriptorSetLayout setLayout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDescriptorSetLayout(Vulkan.Device device, Vulkan.DescriptorSetLayout descriptorSetLayout, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDescriptorPool(Vulkan.Device device, ref DescriptorPoolCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.DescriptorPool descriptorPool);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDescriptorPool(Vulkan.Device device, Vulkan.DescriptorPool descriptorPool, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetDescriptorPool(Vulkan.Device device, Vulkan.DescriptorPool descriptorPool, DescriptorPoolResetFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AllocateDescriptorSets(Vulkan.Device device, ref DescriptorSetAllocateInfo allocateInfo, Vulkan.DescriptorSet[] descriptorSets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result FreeDescriptorSets(Vulkan.Device device, Vulkan.DescriptorPool descriptorPool, uint descriptorSetCount, Vulkan.DescriptorSet[] descriptorSets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void UpdateDescriptorSets(Vulkan.Device device, uint descriptorWriteCount, WriteDescriptorSet[] descriptorWrites, uint descriptorCopyCount, CopyDescriptorSet[] descriptorCopies);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateFramebuffer(Vulkan.Device device, ref FramebufferCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Framebuffer framebuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyFramebuffer(Vulkan.Device device, Vulkan.Framebuffer framebuffer, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateRenderPass(Vulkan.Device device, ref RenderPassCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.RenderPass renderPass);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyRenderPass(Vulkan.Device device, Vulkan.RenderPass renderPass, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetRenderAreaGranularity(Vulkan.Device device, Vulkan.RenderPass renderPass, out Extent2D granularity);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateCommandPool(Vulkan.Device device, ref CommandPoolCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.CommandPool commandPool);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyCommandPool(Vulkan.Device device, Vulkan.CommandPool commandPool, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetCommandPool(Vulkan.Device device, Vulkan.CommandPool commandPool, CommandPoolResetFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AllocateCommandBuffers(Vulkan.Device device, ref CommandBufferAllocateInfo allocateInfo, Vulkan.CommandBuffer[] commandBuffers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void FreeCommandBuffers(Vulkan.Device device, Vulkan.CommandPool commandPool, uint commandBufferCount, Vulkan.CommandBuffer[] commandBuffers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BeginCommandBuffer(Vulkan.CommandBuffer commandBuffer, ref CommandBufferBeginInfo beginInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EndCommandBuffer(Vulkan.CommandBuffer commandBuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetCommandBuffer(Vulkan.CommandBuffer commandBuffer, CommandBufferResetFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindPipeline(Vulkan.CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, Vulkan.Pipeline pipeline);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetViewport(Vulkan.CommandBuffer commandBuffer, uint firstViewport, uint viewportCount, Viewport[] viewports);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetScissor(Vulkan.CommandBuffer commandBuffer, uint firstScissor, uint scissorCount, Rect2D[] scissors);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetLineWidth(Vulkan.CommandBuffer commandBuffer, float lineWidth);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDepthBias(Vulkan.CommandBuffer commandBuffer, float depthBiasrefantFactor, float depthBiasClamp, float depthBiasSlopeFactor);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetBlendConstants(Vulkan.CommandBuffer commandBuffer, float[] blendConstants);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDepthBounds(Vulkan.CommandBuffer commandBuffer, float minDepthBounds, float maxDepthBounds);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetStencilCompareMask(Vulkan.CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint compareMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetStencilWriteMask(Vulkan.CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint writeMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetStencilReference(Vulkan.CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint reference);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindDescriptorSets(Vulkan.CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, Vulkan.PipelineLayout layout, uint firstSet, uint descriptorSetCount, Vulkan.DescriptorSet[] descriptorSets, uint dynamicOffsetCount, uint[] dynamicOffsets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindIndexBuffer(Vulkan.CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, IndexType indexType);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindVertexBuffers(Vulkan.CommandBuffer commandBuffer, uint firstBinding, uint bindingCount, Buffer[] buffers, DeviceSize[] offsets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDraw(Vulkan.CommandBuffer commandBuffer, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndexed(Vulkan.CommandBuffer commandBuffer, uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset, uint firstInstance);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndirect(Vulkan.CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, uint drawCount, uint stride);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndexedIndirect(Vulkan.CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, uint drawCount, uint stride);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDispatch(Vulkan.CommandBuffer commandBuffer, uint groupCountX, uint groupCountY, uint groupCountZ);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDispatchIndirect(Vulkan.CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyBuffer(Vulkan.CommandBuffer commandBuffer, Buffer srcBuffer, Buffer dstBuffer, uint regionCount, BufferCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyImage(Vulkan.CommandBuffer commandBuffer, Vulkan.Image srcImage, ImageLayout srcImageLayout, Vulkan.Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBlitImage(Vulkan.CommandBuffer commandBuffer, Vulkan.Image srcImage, ImageLayout srcImageLayout, Vulkan.Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageBlit[] regions, Filter filter);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyBufferToImage(Vulkan.CommandBuffer commandBuffer, Buffer srcBuffer, Vulkan.Image dstImage, ImageLayout dstImageLayout, uint regionCount, BufferImageCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyImageToBuffer(Vulkan.CommandBuffer commandBuffer, Vulkan.Image srcImage, ImageLayout srcImageLayout, Buffer dstBuffer, uint regionCount, BufferImageCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdUpdateBuffer(Vulkan.CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize dataSize, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdFillBuffer(Vulkan.CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize size, uint data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdClearColorImage(Vulkan.CommandBuffer commandBuffer, Vulkan.Image image, ImageLayout imageLayout, ref ClearColorValue color, uint rangeCount, ImageSubresourceRange[] ranges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdClearDepthStencilImage(Vulkan.CommandBuffer commandBuffer, Vulkan.Image image, ImageLayout imageLayout, ref ClearDepthStencilValue depthStencil, uint rangeCount, ImageSubresourceRange[] ranges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdClearAttachments(Vulkan.CommandBuffer commandBuffer, uint attachmentCount, ClearAttachment[] attachments, uint rectCount, ClearRect[] rects);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdResolveImage(Vulkan.CommandBuffer commandBuffer, Vulkan.Image srcImage, ImageLayout srcImageLayout, Vulkan.Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageResolve[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetEvent(Vulkan.CommandBuffer commandBuffer, Vulkan.Event evt, PipelineStageFlags stageMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdResetEvent(Vulkan.CommandBuffer commandBuffer, Vulkan.Event evt, PipelineStageFlags stageMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdWaitEvents(Vulkan.CommandBuffer commandBuffer, uint eventCount, Vulkan.Event[] events, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, uint memoryBarrierCount, MemoryBarrier[] memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier[] bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier[] imageMemoryBarriers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPipelineBarrier(Vulkan.CommandBuffer commandBuffer, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, uint memoryBarrierCount, MemoryBarrier[] memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier[] bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier[] imageMemoryBarriers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBeginQuery(Vulkan.CommandBuffer commandBuffer, Vulkan.QueryPool queryPool, uint query, QueryControlFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdEndQuery(Vulkan.CommandBuffer commandBuffer, Vulkan.QueryPool queryPool, uint query);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdResetQueryPool(Vulkan.CommandBuffer commandBuffer, Vulkan.QueryPool queryPool, uint firstQuery, uint queryCount);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdWriteTimestamp(Vulkan.CommandBuffer commandBuffer, PipelineStageFlags pipelineStage, Vulkan.QueryPool queryPool, uint query);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyQueryPoolResults(Vulkan.CommandBuffer commandBuffer, Vulkan.QueryPool queryPool, uint firstQuery, uint queryCount, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPushConstants(Vulkan.CommandBuffer commandBuffer, Vulkan.PipelineLayout layout, ShaderStageFlags stageFlags, uint offset, uint size, void* values);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBeginRenderPass(Vulkan.CommandBuffer commandBuffer, ref RenderPassBeginInfo renderPassBegin, SubpassContents contents);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdNextSubpass(Vulkan.CommandBuffer commandBuffer, SubpassContents contents);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdEndRenderPass(Vulkan.CommandBuffer commandBuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdExecuteCommands(Vulkan.CommandBuffer commandBuffer, uint commandBufferCount, Vulkan.CommandBuffer[] commandBuffers);

    //
    // Khronos
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySurface(Vulkan.Instance instance, Vulkan.Surface surface, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceSupport(Vulkan.PhysicalDevice physicalDevice, uint queueFamilyIndex, Vulkan.Surface surface, out Bool32 supported);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities(Vulkan.PhysicalDevice physicalDevice, Vulkan.Surface surface, SurfaceCapabilities[] surfaceCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceFormats(Vulkan.PhysicalDevice physicalDevice, Vulkan.Surface surface, out uint surfaceFormatCount, SurfaceFormat[] surfaceFormats);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfacePresentModes(Vulkan.PhysicalDevice physicalDevice, Vulkan.Surface surface, out uint presentModeCount, PresentMode[] presentModes);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSwapchain(Vulkan.Device device, ref SwapchainCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Swapchain swapchain);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySwapchain(Vulkan.Device device, Vulkan.Swapchain swapchain, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSwapchainImages(Vulkan.Device device, Vulkan.Swapchain swapchain, out uint swapchainImageCount, Vulkan.Image[] swapchainImages);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AcquireNextImage(Vulkan.Device device, Vulkan.Swapchain swapchain, ulong timeout, Vulkan.Semaphore semaphore, Vulkan.Fence fence, out uint imageIndex);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueuePresent(Vulkan.Queue queue, ref PresentInfo presentInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceDisplayProperties(Vulkan.PhysicalDevice physicalDevice, out uint propertyCount, DisplayProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceDisplayPlaneProperties(Vulkan.PhysicalDevice physicalDevice, out uint propertyCount, DisplayPlaneProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDisplayPlaneSupportedDisplays(Vulkan.PhysicalDevice physicalDevice, uint planeIndex, out uint displayCount, Vulkan.Display[] displays);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDisplayModeProperties(Vulkan.PhysicalDevice physicalDevice, Vulkan.Display display, out uint propertyCount, DisplayModeProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDisplayMode(Vulkan.PhysicalDevice physicalDevice, Vulkan.Display display, ref DisplayModeCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.DisplayMode mode);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDisplayPlaneCapabilities(Vulkan.PhysicalDevice physicalDevice, Vulkan.DisplayMode mode, uint planeIndex, out DisplayPlaneCapabilities capabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDisplayPlaneSurface(Vulkan.Instance instance, ref DisplaySurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSharedSwapchains(Vulkan.Device device, uint swapchainCount, SwapchainCreateInfo[] createInfos, ref AllocationCallbacks allocator, Vulkan.Swapchain[] swapchains);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateXlibSurface(Vulkan.Instance instance, ref XlibSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceXlibPresentationSupport(Vulkan.PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr dpy, IntPtr visualID);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateXcbSurface(Vulkan.Instance instance, ref XcbSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceXcbPresentationSupport(Vulkan.PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection, IntPtr visualId);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateWaylandSurface(Vulkan.Instance instance, ref WaylandSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceWaylandPresentationSupport(Vulkan.PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateMirSurface(Vulkan.Instance instance, ref MirSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceMirPresentationSupport(Vulkan.PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateAndroidSurface(Vulkan.Instance instance, ref AndroidSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateWin32Surface(Vulkan.Instance instance, ref Win32SurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceWin32PresentationSupport(Vulkan.PhysicalDevice physicalDevice, uint queueFamilyIndex);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFeatures2(Vulkan.PhysicalDevice physicalDevice, out PhysicalDeviceFeatures2 features);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceProperties2(Vulkan.PhysicalDevice physicalDevice, out PhysicalDeviceProperties2 properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFormatProperties2(Vulkan.PhysicalDevice physicalDevice, Format format, out FormatProperties2 formatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceImageFormatProperties2(Vulkan.PhysicalDevice physicalDevice, ref PhysicalDeviceImageFormatInfo2 imageFormatInfo, out ImageFormatProperties2 imageFormatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceQueueFamilyProperties2(Vulkan.PhysicalDevice physicalDevice, out uint queueFamilyPropertyCount, QueueFamilyProperties2[] queueFamilyProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceMemoryProperties2(Vulkan.PhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties2 memoryProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceSparseImageFormatProperties2(Vulkan.PhysicalDevice physicalDevice, ref PhysicalDeviceSparseImageFormatInfo2 formatInfo, out uint propertyCount, SparseImageFormatProperties2[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void TrimCommandPool(Vulkan.Device device, Vulkan.CommandPool commandPool, CommandPoolTrimFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceExternalBufferProperties(Vulkan.PhysicalDevice physicalDevice, ref PhysicalDeviceExternalBufferInfo externalBufferInfo, out ExternalBufferProperties externalBufferProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryWin32Handle(Vulkan.Device device, ref MemoryGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryWin32HandleProperties(Vulkan.Device device, ExternalMemoryHandleTypeFlags handleType, IntPtr handle, out MemoryWin32HandleProperties memoryWin32HandleProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryFd(Vulkan.Device device, ref MemoryGetFdInfo getFdInfo, out int fd);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryFdProperties(Vulkan.Device device, ExternalMemoryHandleTypeFlags handleType, int fd, out MemoryFdProperties pMemoryFdProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceExternalSemaphoreProperties(Vulkan.PhysicalDevice physicalDevice, ref PhysicalDeviceExternalSemaphoreInfo externalSemaphoreInfo, out ExternalSemaphoreProperties externalSemaphoreProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportSemaphoreWin32Handle(Vulkan.Device device, ref ImportSemaphoreWin32HandleInfo importSemaphoreWin32HandleInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSemaphoreWin32Handle(Vulkan.Device device, ref SemaphoreGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportSemaphoreFd(Vulkan.Device device, ref ImportSemaphoreFdInfo importSemaphoreFdInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSemaphoreFd(Vulkan.Device device, ref SemaphoreGetFdInfo getFdInfo, out int fd);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPushDescriptorSet(Vulkan.CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, Vulkan.PipelineLayout layout, uint set, uint descriptorWriteCount, WriteDescriptorSet[] descriptorWrites);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDescriptorUpdateTemplate(Vulkan.Device device, ref DescriptorUpdateTemplateCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.DescriptorUpdateTemplate descriptorUpdateTemplate);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDescriptorUpdateTemplate(Vulkan.Device device, Vulkan.DescriptorUpdateTemplate descriptorUpdateTemplate, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void UpdateDescriptorSetWithTemplate(Vulkan.Device device, Vulkan.DescriptorSet descriptorSet, Vulkan.DescriptorUpdateTemplate descriptorUpdateTemplate, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPushDescriptorSetWithTemplate(Vulkan.CommandBuffer commandBuffer, Vulkan.DescriptorUpdateTemplate descriptorUpdateTemplate, Vulkan.PipelineLayout layout, uint set, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSwapchainStatus(Vulkan.Device device, Vulkan.Swapchain swapchain);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceExternalFenceProperties(Vulkan.PhysicalDevice physicalDevice, ref PhysicalDeviceExternalFenceInfo externalFenceInfo, out ExternalFenceProperties externalFenceProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportFenceWin32Handle(Vulkan.Device device, ref ImportFenceWin32HandleInfo importFenceWin32HandleInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetFenceWin32Handle(Vulkan.Device device, ref FenceGetWin32HandleInfo getWin32HandleInfo, IntPtr pHandle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportFenceFd(Vulkan.Device device, ref ImportFenceFdInfo importFenceFdInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetFenceFd(Vulkan.Device device, ref FenceGetFdInfo getFdInfo, out int fd);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2(Vulkan.PhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out SurfaceCapabilities2 surfaceCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceFormats2(Vulkan.PhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out uint surfaceFormatCount, SurfaceFormat2[] surfaceFormats);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageMemoryRequirements2(Vulkan.Device device, ref ImageMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetBufferMemoryRequirements2(Vulkan.Device device, ref BufferMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageSparseMemoryRequirements2(Vulkan.Device device, ref ImageSparseMemoryRequirementsInfo2 info, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements2[] sparseMemoryRequirements);

    //
    // Khronos X
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetDeviceGroupPeerMemoryFeatures(Vulkan.Device device, uint heapIndex, uint localDeviceIndex, uint remoteDeviceIndex, out PeerMemoryFeatureFlags peerMemoryFeatures);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindBufferMemory2(Vulkan.Device device, uint bindInfoCount, BindBufferMemoryInfo[] bindInfos);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindImageMemory2(Vulkan.Device device, uint bindInfoCount, BindImageMemoryInfo[] bindInfos);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDeviceMask(Vulkan.CommandBuffer commandBuffer, uint deviceMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDeviceGroupPresentCapabilities(Vulkan.Device device, out DeviceGroupPresentCapabilities deviceGroupPresentCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDeviceGroupSurfacePresentModes(Vulkan.Device device, Vulkan.Surface surface, out DeviceGroupPresentModeFlags modes);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AcquireNextImage2(Vulkan.Device device, ref AcquireNextImageInfo acquireInfo, out uint imageIndex);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDispatchBase(Vulkan.CommandBuffer commandBuffer, uint baseGroupX, uint baseGroupY, uint baseGroupZ, uint groupCountX, uint groupCountY, uint groupCountZ);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDevicePresentRectangles(Vulkan.PhysicalDevice physicalDevice, Vulkan.Surface surface, ref uint rectCount, Rect2D[] rects);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumeratePhysicalDeviceGroups(Vulkan.Instance instance, ref uint physicalDeviceGroupCount, PhysicalDeviceGroupProperties[] physicalDeviceGroupProperties);

    //
    // Multi-vendor
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 DebugReportCallback(DebugReportFlags flags, DebugReportObjectType objectType, ulong objectHandle, ulong location, int messageCode, Text layerPrefix, Text message, void* userData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDebugReportCallback(Vulkan.Instance instance, ref DebugReportCallbackCreateInfo createInfo, ref AllocationCallbacks allocator, DebugReportCallback callback);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDebugReportCallback(Vulkan.Instance instance, DebugReportCallback callback, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DebugReportMessage(Vulkan.Instance instance, DebugReportFlags flags, DebugReportObjectType objectType, ulong obj, Size location, int messageCode, Text layerPrefix, Text message);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DebugMarkerSetObjectTag(Vulkan.Device device, ref DebugMarkerObjectTagInfo tagInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DebugMarkerSetObjectName(Vulkan.Device device, ref DebugMarkerObjectNameInfo nameInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDebugMarkerBegin(Vulkan.CommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDebugMarkerEnd(Vulkan.CommandBuffer commandBuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDebugMarkerInsert(Vulkan.CommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ReleaseDisplay(Vulkan.PhysicalDevice physicalDevice, Vulkan.Display display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AcquireXlibDisplay(Vulkan.PhysicalDevice physicalDevice, IntPtr dpy, Vulkan.Display display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetRandROutputDisplay(Vulkan.PhysicalDevice physicalDevice, IntPtr dpy, IntPtr rrOutput, out Vulkan.Display display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2EXT(Vulkan.PhysicalDevice physicalDevice, Vulkan.Surface surface, out SurfaceCapabilities2EXT surfaceCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DisplayPowerControl(Vulkan.Device device, Vulkan.Display display, ref DisplayPowerInfo displayPowerInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result RegisterDeviceEvent(Vulkan.Device device, ref DeviceEventInfo deviceEventInfo, ref AllocationCallbacks allocator, out Vulkan.Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result RegisterDisplayEvent(Vulkan.Device device, Vulkan.Display display, ref DisplayEventInfo displayEventInfo, ref AllocationCallbacks allocator, out Vulkan.Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSwapchainCounter(Vulkan.Device device, Vulkan.Swapchain swapchain, SurfaceCounterFlags counter, out ulong counterValue);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDiscardRectangle(Vulkan.CommandBuffer commandBuffer, uint firstDiscardRectangle, uint discardRectangleCount, Rect2D[] discardRectangles);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void SetHdrMetadata(Vulkan.Device device, uint swapchainCount, Vulkan.Swapchain[] swapchains, HdrMetadata[] metadata);

    //
    // AMD
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndirectCount(Vulkan.CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, Buffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndexedIndirectCount(Vulkan.CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, Buffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);

    //
    // Nvidia
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceExternalImageFormatProperties(Vulkan.PhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, ExternalMemoryHandleTypeFlagsNV ExternalHandleType, out ExternalImageFormatPropertiesNV externalImageFormatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryWin32HandleNV(Vulkan.Device device, Vulkan.DeviceMemory memory, ExternalMemoryHandleTypeFlagsNV handleType, IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetViewportWScaling(Vulkan.CommandBuffer commandBuffer, uint firstViewport, uint viewportCount, ViewportWScaling[] viewportWScalings);

    //
    // Nvidia X
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdProcessCommands(Vulkan.CommandBuffer commandBuffer, ref CmdProcessCommandsInfo processCommandsInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdReserveSpaceForCommands(Vulkan.CommandBuffer commandBuffer, ref CmdReserveSpaceForCommandsInfo reserveSpaceInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateIndirectCommandsLayout(Vulkan.Device device, ref IndirectCommandsLayoutCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.IndirectCommandsLayout indirectCommandsLayout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyIndirectCommandsLayout(Vulkan.Device device, Vulkan.IndirectCommandsLayout indirectCommandsLayout, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateObjectTable(Vulkan.Device device, ref ObjectTableCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.ObjectTable objectTable);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyObjectTable(Vulkan.Device device, Vulkan.ObjectTable objectTable, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result RegisterObjects(Vulkan.Device device, Vulkan.ObjectTable objectTable, uint objectCount, ref ObjectTableEntry[] objectTableEntries, uint[] objectIndices);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result UnregisterObjects(Vulkan.Device device, Vulkan.ObjectTable objectTable, uint objectCount, ObjectEntryType[] objectEntryTypes, uint[] objectIndices);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceGeneratedCommandsProperties(Vulkan.PhysicalDevice physicalDevice, out DeviceGeneratedCommandsFeatures features, out DeviceGeneratedCommandsLimits limits);

    //
    // Nintendo
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateViSurface(Vulkan.Instance instance, ref ViSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);

    //
    // Google
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetRefreshCycleDuration(Vulkan.Device device, Vulkan.Swapchain swapchain, ref RefreshCycleDuration displayTimingProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPastPresentationTiming(Vulkan.Device device, Vulkan.Swapchain swapchain, ref uint presentationTimingCount, PastPresentationTiming[] presentationTimings);

    //
    // MoltenVK
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateIOSSurface(Vulkan.Instance instance, ref IOSSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateMacOSSurface(Vulkan.Instance instance, ref MacOSSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Vulkan.Surface surface);
}