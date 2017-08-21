using System;
using System.Runtime.InteropServices;

using Vulkan.Handle;

using Buffer = Vulkan.Handle.Buffer;

namespace Vulkan
{
    //
    // Function pointers
    //
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
    public unsafe delegate Result CreateInstance(ref InstanceCreateInfo createInfo, ref AllocationCallbacks allocator, out Instance instance);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyInstance(Instance instance, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumeratePhysicalDevices(Instance instance, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFeatures(PhysicalDevice physicalDevice, out PhysicalDeviceFeatures features);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFormatProperties(PhysicalDevice physicalDevice, Format format, out FormatProperties formatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceImageFormatProperties(PhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceProperties(PhysicalDevice physicalDevice, out PhysicalDeviceProperties properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceQueueFamilyProperties(PhysicalDevice physicalDevice, out uint queueFamilyPropertyCount, QueueFamilyProperties[] queueFamilyProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceMemoryProperties(PhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties memoryProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate IntPtr GetInstanceProcAddr(Instance instance, Text name);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate IntPtr GetDeviceProcAddr(Device device, Text name);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDevice(PhysicalDevice physicalDevice, ref DeviceCreateInfo createInfo, ref AllocationCallbacks allocator, out Device device);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDevice(Device device, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateInstanceExtensionProperties(Text layerName, out uint propertyCount, ExtensionProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateDeviceensionProperties(PhysicalDevice physicalDevice, Text layerName, out uint propertyCount, ExtensionProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateInstanceLayerProperties(out uint propertyCount, LayerProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumerateDeviceLayerProperties(PhysicalDevice physicalDevice, out uint propertyCount, LayerProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetDeviceQueue(Device device, uint queueFamilyIndex, uint queueIndex, out Queue queue);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueueSubmit(Queue queue, uint submitCount, SubmitInfo[] submits, Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueueWaitIdle(Queue queue);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DeviceWaitIdle(Device device);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AllocateMemory(Device device, ref MemoryAllocateInfo allocateInfo, ref AllocationCallbacks allocator, out DeviceMemory memory);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void FreeMemory(Device device, DeviceMemory memory, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result MapMemory(Device device, DeviceMemory memory, DeviceSize offset, DeviceSize size, MemoryMapFlags flags, void** data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void UnmapMemory(Device device, DeviceMemory memory);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result FlushMappedMemoryRanges(Device device, uint memoryRangeCount, MappedMemoryRange[] memoryRanges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result InvalidateMappedMemoryRanges(Device device, uint memoryRangeCount, MappedMemoryRange[] memoryRanges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetDeviceMemoryCommitment(Device device, DeviceMemory memory, out DeviceSize committedMemoryInBytes);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindBufferMemory(Device device, Buffer buffer, DeviceMemory memory, DeviceSize memoryOffset);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindImageMemory(Device device, Image image, DeviceMemory memory, DeviceSize memoryOffset);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetBufferMemoryRequirements(Device device, Buffer buffer, out MemoryRequirements memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageMemoryRequirements(Device device, Image image, out MemoryRequirements memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageSparseMemoryRequirements(Device device, Image image, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements[] sparseMemoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceSparseImageFormatProperties(PhysicalDevice physicalDevice, Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, out uint propertyCount, SparseImageFormatProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueueBindSparse(Queue queue, uint bindInfoCount, ref BindSparseInfo bindInfo, Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateFence(Device device, ref FenceCreateInfo createInfo, ref AllocationCallbacks allocator, out Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyFence(Device device, Fence fence, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetFences(Device device, uint fenceCount, Fence[] fences);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetFenceStatus(Device device, Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result WaitForFences(Device device, uint fenceCount, Fence[] fences, Bool32 waitAll, ulong timeout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSemaphore(Device device, ref SemaphoreCreateInfo createInfo, ref AllocationCallbacks allocator, out Semaphore semaphore);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySemaphore(Device device, Semaphore semaphore, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateEvent(Device device, ref EventCreateInfo createInfo, ref AllocationCallbacks allocator, out Event evnt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyEvent(Device device, Event evt, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetEventStatus(Device device, Event evt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result SetEvent(Device device, Event evt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetEvent(Device device, Event evt);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateQueryPool(Device device, ref QueryPoolCreateInfo createInfo, ref AllocationCallbacks allocator, out QueryPool queryPool);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyQueryPool(Device device, QueryPool queryPool, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetQueryPoolResults(Device device, QueryPool queryPool, uint firstQuery, uint queryCount, Size dataSize, void* data, DeviceSize stride, QueryResultFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateBuffer(Device device, ref BufferCreateInfo createInfo, ref AllocationCallbacks allocator, out Buffer buffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyBuffer(Device device, Buffer buffer, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateBufferView(Device device, ref BufferViewCreateInfo createInfo, ref AllocationCallbacks allocator, out BufferView view);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyBufferView(Device device, BufferView bufferView, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateImage(Device device, ref ImageCreateInfo createInfo, ref AllocationCallbacks allocator, out Image image);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyImage(Device device, Image image, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageSubresourceLayout(Device device, Image image, ref ImageSubresource subresource, out SubresourceLayout layout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateImageView(Device device, ref ImageViewCreateInfo createInfo, ref AllocationCallbacks allocator, out ImageView view);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyImageView(Device device, ImageView imageView, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateShaderModule(Device device, ref ShaderModuleCreateInfo createInfo, ref AllocationCallbacks allocator, out ShaderModule shaderModule);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyShaderModule(Device device, ShaderModule shaderModule, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreatePipelineCache(Device device, ref PipelineCacheCreateInfo createInfo, ref AllocationCallbacks allocator, out PipelineCache pipelineCache);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyPipelineCache(Device device, PipelineCache pipelineCache, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPipelineCacheData(Device device, PipelineCache pipelineCache, out Size dataSize, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result MergePipelineCaches(Device device, PipelineCache dstCache, uint srcCacheCount, ref PipelineCache[] srcCaches);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateGraphicsPipelines(Device device, PipelineCache pipelineCache, uint createInfoCount, GraphicsPipelineCreateInfo[] createInfos, ref AllocationCallbacks allocator, Pipeline[] pipelines);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateComputePipelines(Device device, PipelineCache pipelineCache, uint createInfoCount, ComputePipelineCreateInfo[] createInfos, ref AllocationCallbacks allocator, Pipeline[] pipelines);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyPipeline(Device device, Pipeline pipeline, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreatePipelineLayout(Device device, ref PipelineLayoutCreateInfo createInfo, ref AllocationCallbacks allocator, out PipelineLayout pipelineLayout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyPipelineLayout(Device device, PipelineLayout pipelineLayout, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSampler(Device device, ref SamplerCreateInfo createInfo, ref AllocationCallbacks allocator, out Sampler sampler);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySampler(Device device, Sampler sampler, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDescriptorSetLayout(Device device, ref DescriptorSetLayoutCreateInfo createInfo, ref AllocationCallbacks allocator, out DescriptorSetLayout setLayout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDescriptorSetLayout(Device device, DescriptorSetLayout descriptorSetLayout, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDescriptorPool(Device device, ref DescriptorPoolCreateInfo createInfo, ref AllocationCallbacks allocator, out DescriptorPool descriptorPool);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDescriptorPool(Device device, DescriptorPool descriptorPool, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetDescriptorPool(Device device, DescriptorPool descriptorPool, DescriptorPoolResetFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AllocateDescriptorSets(Device device, ref DescriptorSetAllocateInfo allocateInfo, DescriptorSet[] descriptorSets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result FreeDescriptorSets(Device device, DescriptorPool descriptorPool, uint descriptorSetCount, DescriptorSet[] descriptorSets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void UpdateDescriptorSets(Device device, uint descriptorWriteCount, WriteDescriptorSet[] descriptorWrites, uint descriptorCopyCount, CopyDescriptorSet[] descriptorCopies);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateFramebuffer(Device device, ref FramebufferCreateInfo createInfo, ref AllocationCallbacks allocator, out Framebuffer framebuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyFramebuffer(Device device, Framebuffer framebuffer, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateRenderPass(Device device, ref RenderPassCreateInfo createInfo, ref AllocationCallbacks allocator, out RenderPass renderPass);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyRenderPass(Device device, RenderPass renderPass, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetRenderAreaGranularity(Device device, RenderPass renderPass, out Extent2D granularity);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateCommandPool(Device device, ref CommandPoolCreateInfo createInfo, ref AllocationCallbacks allocator, out CommandPool commandPool);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyCommandPool(Device device, CommandPool commandPool, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetCommandPool(Device device, CommandPool commandPool, CommandPoolResetFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AllocateCommandBuffers(Device device, ref CommandBufferAllocateInfo allocateInfo, CommandBuffer[] commandBuffers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void FreeCommandBuffers(Device device, CommandPool commandPool, uint commandBufferCount, CommandBuffer[] commandBuffers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BeginCommandBuffer(CommandBuffer commandBuffer, ref CommandBufferBeginInfo beginInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EndCommandBuffer(CommandBuffer commandBuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ResetCommandBuffer(CommandBuffer commandBuffer, CommandBufferResetFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindPipeline(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, Pipeline pipeline);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetViewport(CommandBuffer commandBuffer, uint firstViewport, uint viewportCount, Viewport[] viewports);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetScissor(CommandBuffer commandBuffer, uint firstScissor, uint scissorCount, Rect2D[] scissors);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetLineWidth(CommandBuffer commandBuffer, float lineWidth);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDepthBias(CommandBuffer commandBuffer, float depthBiasrefantFactor, float depthBiasClamp, float depthBiasSlopeFactor);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetBlendConstants(CommandBuffer commandBuffer, float[] blendConstants);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDepthBounds(CommandBuffer commandBuffer, float minDepthBounds, float maxDepthBounds);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetStencilCompareMask(CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint compareMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetStencilWriteMask(CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint writeMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetStencilReference(CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint reference);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindDescriptorSets(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineLayout layout, uint firstSet, uint descriptorSetCount, DescriptorSet[] descriptorSets, uint dynamicOffsetCount, uint[] dynamicOffsets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindIndexBuffer(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, IndexType indexType);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBindVertexBuffers(CommandBuffer commandBuffer, uint firstBinding, uint bindingCount, Buffer[] buffers, DeviceSize[] offsets);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDraw(CommandBuffer commandBuffer, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndexed(CommandBuffer commandBuffer, uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset, uint firstInstance);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndirect(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, uint drawCount, uint stride);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndexedIndirect(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, uint drawCount, uint stride);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDispatch(CommandBuffer commandBuffer, uint groupCountX, uint groupCountY, uint groupCountZ);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDispatchIndirect(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyBuffer(CommandBuffer commandBuffer, Buffer srcBuffer, Buffer dstBuffer, uint regionCount, BufferCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyImage(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBlitImage(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageBlit[] regions, Filter filter);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyBufferToImage(CommandBuffer commandBuffer, Buffer srcBuffer, Image dstImage, ImageLayout dstImageLayout, uint regionCount, BufferImageCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyImageToBuffer(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Buffer dstBuffer, uint regionCount, BufferImageCopy[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdUpdateBuffer(CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize dataSize, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdFillBuffer(CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize size, uint data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdClearColorImage(CommandBuffer commandBuffer, Image image, ImageLayout imageLayout, ref ClearColorValue color, uint rangeCount, ImageSubresourceRange[] ranges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdClearDepthStencilImage(CommandBuffer commandBuffer, Image image, ImageLayout imageLayout, ref ClearDepthStencilValue depthStencil, uint rangeCount, ImageSubresourceRange[] ranges);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdClearAttachments(CommandBuffer commandBuffer, uint attachmentCount, ClearAttachment[] attachments, uint rectCount, ClearRect[] rects);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdResolveImage(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageResolve[] regions);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetEvent(CommandBuffer commandBuffer, Event evt, PipelineStageFlags stageMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdResetEvent(CommandBuffer commandBuffer, Event evt, PipelineStageFlags stageMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdWaitEvents(CommandBuffer commandBuffer, uint eventCount, Event[] events, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, uint memoryBarrierCount, MemoryBarrier[] memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier[] bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier[] imageMemoryBarriers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPipelineBarrier(CommandBuffer commandBuffer, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, uint memoryBarrierCount, MemoryBarrier[] memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier[] bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier[] imageMemoryBarriers);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBeginQuery(CommandBuffer commandBuffer, QueryPool queryPool, uint query, QueryControlFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdEndQuery(CommandBuffer commandBuffer, QueryPool queryPool, uint query);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdResetQueryPool(CommandBuffer commandBuffer, QueryPool queryPool, uint firstQuery, uint queryCount);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdWriteTimestamp(CommandBuffer commandBuffer, PipelineStageFlags pipelineStage, QueryPool queryPool, uint query);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdCopyQueryPoolResults(CommandBuffer commandBuffer, QueryPool queryPool, uint firstQuery, uint queryCount, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPushConstants(CommandBuffer commandBuffer, PipelineLayout layout, ShaderStageFlags stageFlags, uint offset, uint size, void* values);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdBeginRenderPass(CommandBuffer commandBuffer, ref RenderPassBeginInfo renderPassBegin, SubpassContents contents);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdNSubpass(CommandBuffer commandBuffer, SubpassContents contents);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdEndRenderPass(CommandBuffer commandBuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdExecuteCommands(CommandBuffer commandBuffer, uint commandBufferCount, CommandBuffer[] commandBuffers);

    //
    // Khronos
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySurface(Instance instance, Surface surface, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceSupport(PhysicalDevice physicalDevice, uint queueFamilyIndex, Surface surface, out Bool32 supported);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities(PhysicalDevice physicalDevice, Surface surface, SurfaceCapabilities[] surfaceCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceFormats(PhysicalDevice physicalDevice, Surface surface, out uint surfaceFormatCount, SurfaceFormat[] surfaceFormats);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfacePresentModes(PhysicalDevice physicalDevice, Surface surface, out uint presentModeCount, PresentMode[] presentModes);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSwapchain(Device device, ref SwapchainCreateInfo createInfo, ref AllocationCallbacks allocator, out Swapchain swapchain);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroySwapchain(Device device, Swapchain swapchain, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSwapchainImages(Device device, Swapchain swapchain, out uint swapchainImageCount, Image[] swapchainImages);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AcquireNImage(Device device, Swapchain swapchain, ulong timeout, Semaphore semaphore, Fence fence, out uint imageIndex);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result QueuePresent(Queue queue, ref PresentInfo presentInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceDisplayProperties(PhysicalDevice physicalDevice, out uint propertyCount, DisplayProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceDisplayPlaneProperties(PhysicalDevice physicalDevice, out uint propertyCount, DisplayPlaneProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDisplayPlaneSupportedDisplays(PhysicalDevice physicalDevice, uint planeIndex, out uint displayCount, Display[] displays);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDisplayModeProperties(PhysicalDevice physicalDevice, Display display, out uint propertyCount, DisplayModeProperties[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDisplayMode(PhysicalDevice physicalDevice, Display display, ref DisplayModeCreateInfo createInfo, ref AllocationCallbacks allocator, out DisplayMode mode);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDisplayPlaneCapabilities(PhysicalDevice physicalDevice, DisplayMode mode, uint planeIndex, out DisplayPlaneCapabilities capabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDisplayPlaneSurface(Instance instance, ref DisplaySurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateSharedSwapchains(Device device, uint swapchainCount, SwapchainCreateInfo[] createInfos, ref AllocationCallbacks allocator, Swapchain[] swapchains);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateXlibSurface(Instance instance, ref XlibSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceXlibPresentationSupport(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr dpy, IntPtr visualID);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateXcbSurface(Instance instance, ref XcbSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceXcbPresentationSupport(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection, IntPtr visualId);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateWaylandSurface(Instance instance, ref WaylandSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceWaylandPresentationSupport(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateMirSurface(Instance instance, ref MirSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceMirPresentationSupport(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateAndroidSurface(Instance instance, ref AndroidSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateWin32Surface(Instance instance, ref Win32SurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 GetPhysicalDeviceWin32PresentationSupport(PhysicalDevice physicalDevice, uint queueFamilyIndex);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFeatures2(PhysicalDevice physicalDevice, out PhysicalDeviceFeatures2 features);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceProperties2(PhysicalDevice physicalDevice, out PhysicalDeviceProperties2 properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceFormatProperties2(PhysicalDevice physicalDevice, Format format, out FormatProperties2 formatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceImageFormatProperties2(PhysicalDevice physicalDevice, ref PhysicalDeviceImageFormatInfo2 imageFormatInfo, out ImageFormatProperties2 imageFormatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceQueueFamilyProperties2(PhysicalDevice physicalDevice, out uint queueFamilyPropertyCount, QueueFamilyProperties2[] queueFamilyProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceMemoryProperties2(PhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties2 memoryProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceSparseImageFormatProperties2(PhysicalDevice physicalDevice, ref PhysicalDeviceSparseImageFormatInfo2 formatInfo, out uint propertyCount, SparseImageFormatProperties2[] properties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void TrimCommandPool(Device device, CommandPool commandPool, CommandPoolTrimFlags flags);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceExternalBufferProperties(PhysicalDevice physicalDevice, ref PhysicalDeviceExternalBufferInfo externalBufferInfo, out ExternalBufferProperties externalBufferProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryWin32Handle(Device device, ref MemoryGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryWin32HandleProperties(Device device, ExternalMemoryHandleTypeFlags handleType, IntPtr handle, out MemoryWin32HandleProperties memoryWin32HandleProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryFd(Device device, ref MemoryGetFdInfo getFdInfo, out int fd);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryFdProperties(Device device, ExternalMemoryHandleTypeFlags handleType, int fd, out MemoryFdProperties pMemoryFdProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceExternalSemaphoreProperties(PhysicalDevice physicalDevice, ref PhysicalDeviceExternalSemaphoreInfo externalSemaphoreInfo, out ExternalSemaphoreProperties externalSemaphoreProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportSemaphoreWin32Handle(Device device, ref ImportSemaphoreWin32HandleInfo importSemaphoreWin32HandleInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSemaphoreWin32Handle(Device device, ref SemaphoreGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportSemaphoreFd(Device device, ref ImportSemaphoreFdInfo importSemaphoreFdInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSemaphoreFd(Device device, ref SemaphoreGetFdInfo getFdInfo, out int fd);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPushDescriptorSet(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineLayout layout, uint set, uint descriptorWriteCount, WriteDescriptorSet[] descriptorWrites);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDescriptorUpdateTemplate(Device device, ref DescriptorUpdateTemplateCreateInfo createInfo, ref AllocationCallbacks allocator, out DescriptorUpdateTemplate descriptorUpdateTemplate);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDescriptorUpdateTemplate(Device device, DescriptorUpdateTemplate descriptorUpdateTemplate, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void UpdateDescriptorSetWithTemplate(Device device, DescriptorSet descriptorSet, DescriptorUpdateTemplate descriptorUpdateTemplate, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdPushDescriptorSetWithTemplate(CommandBuffer commandBuffer, DescriptorUpdateTemplate descriptorUpdateTemplate, PipelineLayout layout, uint set, void* data);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSwapchainStatus(Device device, Swapchain swapchain);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceExternalFenceProperties(PhysicalDevice physicalDevice, ref PhysicalDeviceExternalFenceInfo externalFenceInfo, out ExternalFenceProperties externalFenceProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportFenceWin32Handle(Device device, ref ImportFenceWin32HandleInfo importFenceWin32HandleInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetFenceWin32Handle(Device device, ref FenceGetWin32HandleInfo getWin32HandleInfo, IntPtr pHandle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ImportFenceFd(Device device, ref ImportFenceFdInfo importFenceFdInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetFenceFd(Device device, ref FenceGetFdInfo getFdInfo, out int fd);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2(PhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out SurfaceCapabilities2 surfaceCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceFormats2(PhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out uint surfaceFormatCount, SurfaceFormat2[] surfaceFormats);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageMemoryRequirements2(Device device, ref ImageMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetBufferMemoryRequirements2(Device device, ref BufferMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetImageSparseMemoryRequirements2(Device device, ref ImageSparseMemoryRequirementsInfo2 info, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements2[] sparseMemoryRequirements);

    //
    // Khronos X
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetDeviceGroupPeerMemoryFeatures(Device device, uint heapIndex, uint localDeviceIndex, uint remoteDeviceIndex, out PeerMemoryFeatureFlags peerMemoryFeatures);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindBufferMemory2(Device device, uint bindInfoCount, BindBufferMemoryInfo[] bindInfos);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result BindImageMemory2(Device device, uint bindInfoCount, BindImageMemoryInfo[] bindInfos);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDeviceMask(CommandBuffer commandBuffer, uint deviceMask);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDeviceGroupPresentCapabilities(Device device, out DeviceGroupPresentCapabilities deviceGroupPresentCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetDeviceGroupSurfacePresentModes(Device device, Surface surface, out DeviceGroupPresentModeFlags modes);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AcquireNextImage2(Device device, ref AcquireNextImageInfo acquireInfo, out uint imageIndex);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDispatchBase(CommandBuffer commandBuffer, uint baseGroupX, uint baseGroupY, uint baseGroupZ, uint groupCountX, uint groupCountY, uint groupCountZ);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDevicePresentRectangles(PhysicalDevice physicalDevice, Surface surface, ref uint rectCount, Rect2D[] rects);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result EnumeratePhysicalDeviceGroups(Instance instance, ref uint physicalDeviceGroupCount, PhysicalDeviceGroupProperties[] physicalDeviceGroupProperties);

    //
    // Multi-vendor
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Bool32 DebugReportCallback(DebugReportFlags flags, DebugReportObjectType objectType, ulong objectHandle, ulong location, int messageCode, Text layerPrefix, Text message, void* userData);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateDebugReportCallback(Instance instance, ref DebugReportCallbackCreateInfo createInfo, ref AllocationCallbacks allocator, DebugReportCallback callback);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyDebugReportCallback(Instance instance, DebugReportCallback callback, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DebugReportMessage(Instance instance, DebugReportFlags flags, DebugReportObjectType objectType, ulong obj, Size location, int messageCode, Text layerPrefix, Text message);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DebugMarkerSetObjectTag(Device device, ref DebugMarkerObjectTagInfo tagInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DebugMarkerSetObjectName(Device device, ref DebugMarkerObjectNameInfo nameInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDebugMarkerBegin(CommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDebugMarkerEnd(CommandBuffer commandBuffer);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDebugMarkerInsert(CommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result ReleaseDisplay(PhysicalDevice physicalDevice, Display display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result AcquireXlibDisplay(PhysicalDevice physicalDevice, IntPtr dpy, Display display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetRandROutputDisplay(PhysicalDevice physicalDevice, IntPtr dpy, IntPtr rrOutput, out Display display);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2EXT(PhysicalDevice physicalDevice, Surface surface, out SurfaceCapabilities2EXT surfaceCapabilities);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result DisplayPowerControl(Device device, Display display, ref DisplayPowerInfo displayPowerInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result RegisterDeviceEvent(Device device, ref DeviceEventInfo deviceEventInfo, ref AllocationCallbacks allocator, out Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result RegisterDisplayEvent(Device device, Display display, ref DisplayEventInfo displayEventInfo, ref AllocationCallbacks allocator, out Fence fence);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetSwapchainCounter(Device device, Swapchain swapchain, SurfaceCounterFlags counter, out ulong counterValue);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetDiscardRectangle(CommandBuffer commandBuffer, uint firstDiscardRectangle, uint discardRectangleCount, Rect2D[] discardRectangles);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void SetHdrMetadata(Device device, uint swapchainCount, Swapchain[] swapchains, HdrMetadata[] metadata);

    //
    // AMD
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndirectCount(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, Buffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdDrawIndexedIndirectCount(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, Buffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);

    //
    // Nvidia
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPhysicalDeviceExternalImageFormatProperties(PhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, ExternalMemoryHandleTypeFlagsNV ExternalHandleType, out ExternalImageFormatPropertiesNV externalImageFormatProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetMemoryWin32HandleNV(Device device, DeviceMemory memory, ExternalMemoryHandleTypeFlagsNV handleType, IntPtr handle);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdSetViewportWScaling(CommandBuffer commandBuffer, uint firstViewport, uint viewportCount, ViewportWScaling[] viewportWScalings);

    //
    // Nvidia X
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdProcessCommands(CommandBuffer commandBuffer, ref CmdProcessCommandsInfo processCommandsInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void CmdReserveSpaceForCommands(CommandBuffer commandBuffer, ref CmdReserveSpaceForCommandsInfo reserveSpaceInfo);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateIndirectCommandsLayout(Device device, ref IndirectCommandsLayoutCreateInfo createInfo, ref AllocationCallbacks allocator, out IndirectCommandsLayout indirectCommandsLayout);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyIndirectCommandsLayout(Device device, IndirectCommandsLayout indirectCommandsLayout, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateObjectTable(Device device, ref ObjectTableCreateInfo createInfo, ref AllocationCallbacks allocator, out ObjectTable objectTable);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void DestroyObjectTable(Device device, ObjectTable objectTable, ref AllocationCallbacks allocator);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result RegisterObjects(Device device, ObjectTable objectTable, uint objectCount, ref ObjectTableEntry[] objectTableEntries, uint[] objectIndices);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result UnregisterObjects(Device device, ObjectTable objectTable, uint objectCount, ObjectEntryType[] objectEntryTypes, uint[] objectIndices);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void GetPhysicalDeviceGeneratedCommandsProperties(PhysicalDevice physicalDevice, out DeviceGeneratedCommandsFeatures features, out DeviceGeneratedCommandsLimits limits);

    //
    // Nintendo
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateViSurface(Instance instance, ref ViSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);

    //
    // Google
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetRefreshCycleDuration(Device device, Swapchain swapchain, ref RefreshCycleDuration displayTimingProperties);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result GetPastPresentationTiming(Device device, Swapchain swapchain, ref uint presentationTimingCount, PastPresentationTiming[] presentationTimings);

    //
    // MoltenVK
    //
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateIOSSurface(Instance instance, ref IOSSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate Result CreateMacOSSurface(Instance instance, ref MacOSSurfaceCreateInfo createInfo, ref AllocationCallbacks allocator, out Surface surface);
}