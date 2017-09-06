using System;
using System.Runtime.InteropServices;
using Engine;
using Engine.Platform;

namespace Vulkan
{
    public static partial class Vk
    {
        public static GetInstanceProcAddrDelegate GetInstanceProcAddr { get; set; }
        public static GetDeviceProcAddrDelegate GetDeviceProcAddr { get; set; }

        private static NativeLibrary _library;

        // Constants
        public const float LodClampNone = 1000f;
        public const uint RemainingMipLevels = ~0U;
        public const uint RemainingArrayLayers = ~0U;
        public const ulong WholeSize = ~0UL;
        public const uint AttachmentUnused = ~0U;
        public const uint QueueFamilyIgnored = ~0U;
        public const uint SubpassExternal = ~0U;
        public const int MaxPhysicalDeviceNameSize = 256;
        public const int UUIDSize = 16;
        public const int MaxMemoryTypes = 32;
        public const int MaxMemoryHeaps = 16;
        public const int MaxExtensionNameSize = 256;
        public const int MaxDescriptionSize = 256;
        public const int LUIDSize = 8;
        public const int MaxDeviceGroupSize = 32;

        // Extension names
        public const string SurfaceExtensionName = "VK_KHR_surface";
        public const string SwapchainExtensionName = "VK_KHR_swapchain";
        public const string DisplayExtensionName = "VK_KHR_display";
        public const string DisplaySwapchainExtensionName = "VK_KHR_display_swapchain";
        public const string XlibSurfaceExtensionName = "VK_KHR_xlib_surface";
        public const string XcbSurfaceExtensionName = "VK_KHR_xcb_surface";
        public const string WaylandSurfaceExtensionName = "VK_KHR_wayland_surface";
        public const string MirSurfaceExtensionName = "VK_KHR_mir_surface";
        public const string AndroidSurfaceExtensionName = "VK_KHR_android_surface";
        public const string Win32SurfaceExtensionName = "VK_KHR_win32_surface";
        public const string SamplerMirrorClampToEdgeExtensionName = "VK_KHR_sampler_mirror_clamp_to_edge";
        public const string GetPhysicalDeviceProperties2ExtensionName = "VK_KHR_get_physical_device_properties2";
        public const string ShaderDrawParametersExtensionName = "VK_KHR_shader_draw_parameters";
        public const string Maintenance1ExtensionName = "VK_KHR_maintenance1";
        public const string ExternalMemoryCapabilitiesExtensionName = "VK_KHR_external_memory_capabilities";
        public const string ExternalMemoryExtensionName = "VK_KHR_external_memory";
        public const string ExternalMemoryWin32ExtensionName = "VK_KHR_external_memory_win32";
        public const string ExternalMemoryFdExtensionName = "VK_KHR_external_memory_fd";
        public const string Win32KeyedMutexExtensionName = "VK_KHR_win32_keyed_mutex";
        public const string ExternalSemaphoreCapabilitiesExtensionName = "VK_KHR_external_semaphore_capabilities";
        public const string ExternalSemaphoreExtensionName = "VK_KHR_external_semaphore";
        public const string ExternalSemaphoreWin32ExtensionName = "VK_KHR_external_semaphore_win32";
        public const string ExternalSemaphoreFdExtensionName = "VK_KHR_external_semaphore_fd";
        public const string PushDescriptorExtensionName = "VK_KHR_push_descriptor";
        public const string Khr16BitStorageExtensionName = "VK_KHR_16bit_storage";
        public const string IncrementalPresentExtensionName = "VK_KHR_incremental_present";
        public const string DescriptorUpdateTemplateExtensionName = "VK_KHR_descriptor_update_template";
        public const string SharedPresentableImageExtensionName = "VK_KHR_shared_presentable_image";
        public const string ExternalFenceCapabilitiesExtensionName = "VK_KHR_external_fence_capabilities";
        public const string ExternalFenceExtensionName = "VK_KHR_external_fence";
        public const string ExternalFenceWin32ExtensionName = "VK_KHR_external_fence_win32";
        public const string ExternalFenceFdExtensionName = "VK_KHR_external_fence_fd";
        public const string GetSurfaceCapabilities2ExtensionName = "VK_KHR_get_surface_capabilities2";
        public const string VariablePointersExtensionName = "VK_KHR_variable_pointers";
        public const string DedicatedAllocationExtensionName = "VK_KHR_dedicated_allocation";
        public const string StorageBufferStorageClassExtensionName = "VK_KHR_storage_buffer_storage_class";
        public const string RelaxedBlockLayoutExtensionName = "VK_KHR_relaxed_block_layout";
        public const string GetMemoryRequirements2ExtensionName = "VK_KHR_get_memory_requirements2";
        public const string ExtDebugReportExtensionName = "VK_EXT_debug_report";
        public const string GlslShaderExtensionName = "VK_NV_glsl_shader";
        public const string DepthRangeUnrestrictedExtensionName = "VK_EXT_depth_range_unrestricted";
        public const string FilterCubicExtensionName = "VK_IMG_filter_cubic";
        public const string RasterizationOrderExtensionName = "VK_AMD_rasterization_order";
        public const string ShaderTrinaryMinmaxExtensionName = "VK_AMD_shader_trinary_minmax";
        public const string ShaderExplicitVertexParameterExtensionName = "VK_AMD_shader_explicit_vertex_parameter";
        public const string DebugMarkerExtensionName = "VK_EXT_debug_marker";
        public const string GcnShaderExtensionName = "VK_AMD_gcn_shader";
        public const string NVDedicatedAllocationExtensionName = "VK_NV_dedicated_allocation";
        public const string DrawIndirectCountExtensionName = "VK_AMD_draw_indirect_count";
        public const string NegativeViewportHeightExtensionName = "VK_AMD_negative_viewport_height";
        public const string GpuShaderHalfFloatExtensionName = "VK_AMD_gpu_shader_half_float";
        public const string ShaderBallotExtensionName = "VK_AMD_shader_ballot";
        public const string TextureGatherBiasLodExtensionName = "VK_AMD_texture_gather_bias_lod";
        public const string MultiviewExtensionName = "VK_KHX_multiview";
        public const string FormatPVRTCExtensionName = "VK_IMG_format_pvrtc";
        public const string NVExternalMemoryCapabilitiesExtensionName = "VK_NV_external_memory_capabilities";
        public const string NVExternalMemoryExtensionName = "VK_NV_external_memory";
        public const string NVExternalMemoryWin32ExtensionName = "VK_NV_external_memory_win32";
        public const string NVWin32KeyedMutexExtensionName = "VK_NV_win32_keyed_mutex";
        public const string DeviceGroupExtensionName = "VK_KHX_device_group";
        public const string ValidationFlagsExtensionName = "VK_EXT_validation_flags";
        public const string ViSurfaceExtensionName = "VK_NN_vi_surface";
        public const string ShaderSubgroupBallotExtensionName = "VK_EXT_shader_subgroup_ballot";
        public const string ShaderSubgroupVoteExtensionName = "VK_EXT_shader_subgroup_vote";
        public const string DeviceGroupCreationExtensionName = "VK_KHX_device_group_creation";
        public const string DeviceGeneratedCommandsExtensionName = "VK_NVX_device_generated_commands";
        public const string ClipSpaceWScalingExtensionName = "VK_NV_clip_space_w_scaling";
        public const string DirectModeDisplayExtensionName = "VK_EXT_direct_mode_display";
        public const string AcquireXlibDisplayExtensionName = "VK_EXT_acquire_xlib_display";
        public const string DisplaySurfaceCounterExtensionName = "VK_EXT_display_surface_counter";
        public const string DisplayControlExtensionName = "VK_EXT_display_control";
        public const string DisplayTimingExtensionName = "VK_GOOGLE_display_timing";
        public const string SampleMaskOverrideCoverageExtensionName = "VK_NV_sample_mask_override_coverage";
        public const string GeometryShaderPassthroughExtensionName = "VK_NV_geometry_shader_passthrough";
        public const string ViewportArray2ExtensionName = "VK_NV_viewport_array2";
        public const string MultiviewPerViewAttributesExtensionName = "VK_NVX_multiview_per_view_attributes";
        public const string ViewportSwizzleExtensionName = "VK_NV_viewport_swizzle";
        public const string DiscardRectanglesExtensionName = "VK_EXT_discard_rectangles";
        public const string SwapchainColorSpaceExtensionName = "VK_EXT_swapchain_colorspace";
        public const string HDRMetadataExtensionName = "VK_EXT_hdr_metadata";
        public const string IOSSurfaceExtensionName = "VK_MVK_ios_surface";
        public const string MacOSSurfaceExtensionName = "VK_MVK_macos_surface";
        public const string SamplerFilterMinmaxExtensionName = "VK_EXT_sampler_filter_minmax";
        public const string GPUShaderInt16ExtensionName = "VK_AMD_gpu_shader_int16";
        public const string MixedAttachmentSamplesExtensionName = "VK_AMD_mixed_attachment_samples";
        public const string BlendOperationAdvancedExtensionName = "VK_EXT_blend_operation_advanced";
        public const string FragmentCoverageToColorExtensionName = "VK_NV_fragment_coverage_to_color";
        public const string FramebufferMixedSamplesExtensionName = "VK_NV_framebuffer_mixed_samples";
        public const string FillRectangleExtensionName = "VK_NV_fill_rectangle";
        public const string PostDepthCoverageExtensionName = "VK_EXT_post_depth_coverage";

        public static void LoadLibrary()
        {
            string name;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                name = "vulkan-1.dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                name = "libvulkan.so.1";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                name = "libdylib";
            }
            else
            {
                throw new InvalidOperationException("Unknown Vulkan platform.");
            }

            _library = new NativeLibrary(name);
        }

        public static void LoadGetInstanceProcAddrFunction()
        {
            GetInstanceProcAddr = _library.LoadFunction<GetInstanceProcAddrDelegate>("PFN_vkGetInstanceProcAddr");
        }

        //
        // Methods
        //
        public unsafe static T LoadGlobalFunction<T>() => LoadInstanceFunction<T>(Instance.Null);

        public unsafe static T LoadInstanceFunction<T>(Instance instance)
        {
            string name = "vk" + typeof(T).Name;
            name = name.Replace("Delegate", "");
            byte* nameUtf8 = name.ToUtf8();
            
            IntPtr function = GetInstanceProcAddr(instance, nameUtf8);
            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }
        
        public unsafe static T LoadDeviceFunction<T>(Device device)
        {
            string name = "vk" + typeof(T).Name;
            name = name.Replace("Delegate", "");
            IntPtr function = GetDeviceProcAddr(device, name.ToUtf8());
            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

        //
        // Delegates
        //
        public unsafe delegate void* AllocationFunction(void* userData, Size size, Size alignment, SystemAllocationScope allocationScope);
        public unsafe delegate void* ReallocationFunction(void* userData, void* original, Size size, Size alignment, SystemAllocationScope allocationScope);
        public unsafe delegate void FreeFunction(void* userData, void* memory);
        public unsafe delegate void InternalAllocationNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);
        public unsafe delegate void InternalFreeNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);
        public delegate void VoidFunction();

        //
        // Function delegates
        //
        public unsafe delegate Result CreateInstanceDelegate(ref InstanceCreateInfo createInfo, AllocationCallbacks* allocator, out Instance instance);
        public unsafe delegate void DestroyInstanceDelegate(Instance instance, AllocationCallbacks* allocator);
        public unsafe delegate Result EnumeratePhysicalDevicesDelegate(Instance instance, ref uint physicalDeviceCount, [Out] PhysicalDevice[] physicalDevices);
        public unsafe delegate void GetPhysicalDeviceFeaturesDelegate(PhysicalDevice physicalDevice, out PhysicalDeviceFeatures features);
        public unsafe delegate void GetPhysicalDeviceFormatPropertiesDelegate(PhysicalDevice physicalDevice, Format format, out FormatProperties formatProperties);
        public unsafe delegate Result GetPhysicalDeviceImageFormatPropertiesDelegate(PhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties);
        public unsafe delegate void GetPhysicalDevicePropertiesDelegate(PhysicalDevice physicalDevice, out PhysicalDeviceProperties properties);
        public unsafe delegate void GetPhysicalDeviceQueueFamilyPropertiesDelegate(PhysicalDevice physicalDevice, ref uint queueFamilyPropertyCount, [Out] QueueFamilyProperties[] queueFamilyProperties);
        public unsafe delegate void GetPhysicalDeviceMemoryPropertiesDelegate(PhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties memoryProperties);
        public unsafe delegate IntPtr GetInstanceProcAddrDelegate(Instance instance, byte* name);
        public unsafe delegate IntPtr GetDeviceProcAddrDelegate(Device device, byte* name);
        public unsafe delegate Result CreateDeviceDelegate(PhysicalDevice physicalDevice, ref DeviceCreateInfo createInfo, AllocationCallbacks* allocator, out Device device);
        public unsafe delegate void DestroyDeviceDelegate(Device device, AllocationCallbacks* allocator);
        public unsafe delegate Result EnumerateInstanceExtensionPropertiesDelegate(byte* layerName, ref uint propertyCount, [Out] ExtensionProperties[] properties);
        public unsafe delegate Result EnumerateDeviceExtensionPropertiesDelegate(PhysicalDevice physicalDevice, byte* layerName, ref uint propertyCount, [Out] ExtensionProperties[] properties);
        public unsafe delegate Result EnumerateInstanceLayerPropertiesDelegate(ref uint propertyCount, [Out] LayerProperties[] properties);
        public unsafe delegate Result EnumerateDeviceLayerPropertiesDelegate(PhysicalDevice physicalDevice, ref uint propertyCount, [Out] LayerProperties[] properties);
        public unsafe delegate void GetDeviceQueueDelegate(Device device, uint queueFamilyIndex, uint queueIndex, out Queue queue);
        public unsafe delegate Result QueueSubmitDelegate(Queue queue, uint submitCount, SubmitInfo[] submits, Fence fence);
        public unsafe delegate Result QueueWaitIdleDelegate(Queue queue);
        public unsafe delegate Result DeviceWaitIdleDelegate(Device device);
        public unsafe delegate Result AllocateMemoryDelegate(Device device, ref MemoryAllocateInfo allocateInfo, AllocationCallbacks* allocator, out DeviceMemory memory);
        public unsafe delegate void FreeMemoryDelegate(Device device, DeviceMemory memory, AllocationCallbacks* allocator);
        public unsafe delegate Result MapMemoryDelegate(Device device, DeviceMemory memory, DeviceSize offset, DeviceSize size, MemoryMapFlags flags, void** data);
        public unsafe delegate void UnmapMemoryDelegate(Device device, DeviceMemory memory);
        public unsafe delegate Result FlushMappedMemoryRangesDelegate(Device device, uint memoryRangeCount, MappedMemoryRange[] memoryRanges);
        public unsafe delegate Result InvalidateMappedMemoryRangesDelegate(Device device, uint memoryRangeCount, MappedMemoryRange[] memoryRanges);
        public unsafe delegate void GetDeviceMemoryCommitmentDelegate(Device device, DeviceMemory memory, out DeviceSize committedMemoryInBytes);
        public unsafe delegate Result BindBufferMemoryDelegate(Device device, Buffer buffer, DeviceMemory memory, DeviceSize memoryOffset);
        public unsafe delegate Result BindImageMemoryDelegate(Device device, Image image, DeviceMemory memory, DeviceSize memoryOffset);
        public unsafe delegate void GetBufferMemoryRequirementsDelegate(Device device, Buffer buffer, out MemoryRequirements memoryRequirements);
        public unsafe delegate void GetImageMemoryRequirementsDelegate(Device device, Image image, out MemoryRequirements memoryRequirements);
        public unsafe delegate void GetImageSparseMemoryRequirementsDelegate(Device device, Image image, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements[] sparseMemoryRequirements);
        public unsafe delegate void GetPhysicalDeviceSparseImageFormatPropertiesDelegate(PhysicalDevice physicalDevice, Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, out uint propertyCount, [Out] SparseImageFormatProperties[] properties);
        public unsafe delegate Result QueueBindSparseDelegate(Queue queue, uint bindInfoCount, ref BindSparseInfo bindInfo, Fence fence);
        public unsafe delegate Result CreateFenceDelegate(Device device, ref FenceCreateInfo createInfo, AllocationCallbacks* allocator, out Fence fence);
        public unsafe delegate void DestroyFenceDelegate(Device device, Fence fence, AllocationCallbacks* allocator);
        public unsafe delegate Result ResetFencesDelegate(Device device, uint fenceCount, Fence[] fences);
        public unsafe delegate Result GetFenceStatusDelegate(Device device, Fence fence);
        public unsafe delegate Result WaitForFencesDelegate(Device device, uint fenceCount, Fence[] fences, Bool32 waitAll, ulong timeout);
        public unsafe delegate Result CreateSemaphoreDelegate(Device device, ref SemaphoreCreateInfo createInfo, AllocationCallbacks* allocator, out Semaphore semaphore);
        public unsafe delegate void DestroySemaphoreDelegate(Device device, Semaphore semaphore, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateEventDelegate(Device device, ref EventCreateInfo createInfo, AllocationCallbacks* allocator, out Event evnt);
        public unsafe delegate void DestroyEventDelegate(Device device, Event evt, AllocationCallbacks* allocator);
        public unsafe delegate Result GetEventStatusDelegate(Device device, Event evt);
        public unsafe delegate Result SetEventDelegate(Device device, Event evt);
        public unsafe delegate Result ResetEventDelegate(Device device, Event evt);
        public unsafe delegate Result CreateQueryPoolDelegate(Device device, ref QueryPoolCreateInfo createInfo, AllocationCallbacks* allocator, out QueryPool queryPool);
        public unsafe delegate void DestroyQueryPoolDelegate(Device device, QueryPool queryPool, AllocationCallbacks* allocator);
        public unsafe delegate Result GetQueryPoolResultsDelegate(Device device, QueryPool queryPool, uint firstQuery, uint queryCount, Size dataSize, void* data, DeviceSize stride, QueryResultFlags flags);
        public unsafe delegate Result CreateBufferDelegate(Device device, ref BufferCreateInfo createInfo, AllocationCallbacks* allocator, out Buffer buffer);
        public unsafe delegate void DestroyBufferDelegate(Device device, Buffer buffer, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateBufferViewDelegate(Device device, ref BufferViewCreateInfo createInfo, AllocationCallbacks* allocator, out BufferView view);
        public unsafe delegate void DestroyBufferViewDelegate(Device device, BufferView bufferView, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateImageDelegate(Device device, ref ImageCreateInfo createInfo, AllocationCallbacks* allocator, out Image image);
        public unsafe delegate void DestroyImageDelegate(Device device, Image image, AllocationCallbacks* allocator);
        public unsafe delegate void GetImageSubresourceLayoutDelegate(Device device, Image image, ref ImageSubresource subresource, out SubresourceLayout layout);
        public unsafe delegate Result CreateImageViewDelegate(Device device, ref ImageViewCreateInfo createInfo, AllocationCallbacks* allocator, out ImageView view);
        public unsafe delegate void DestroyImageViewDelegate(Device device, ImageView imageView, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateShaderModuleDelegate(Device device, ref ShaderModuleCreateInfo createInfo, AllocationCallbacks* allocator, out ShaderModule shaderModule);
        public unsafe delegate void DestroyShaderModuleDelegate(Device device, ShaderModule shaderModule, AllocationCallbacks* allocator);
        public unsafe delegate Result CreatePipelineCacheDelegate(Device device, ref PipelineCacheCreateInfo createInfo, AllocationCallbacks* allocator, out PipelineCache pipelineCache);
        public unsafe delegate void DestroyPipelineCacheDelegate(Device device, PipelineCache pipelineCache, AllocationCallbacks* allocator);
        public unsafe delegate Result GetPipelineCacheDataDelegate(Device device, PipelineCache pipelineCache, out Size dataSize, void* data);
        public unsafe delegate Result MergePipelineCachesDelegate(Device device, PipelineCache dstCache, uint srcCacheCount, ref PipelineCache[] srcCaches);
        public unsafe delegate Result CreateGraphicsPipelinesDelegate(Device device, PipelineCache pipelineCache, uint createInfoCount, GraphicsPipelineCreateInfo[] createInfos, AllocationCallbacks* allocator, Pipeline[] pipelines);
        public unsafe delegate Result CreateComputePipelinesDelegate(Device device, PipelineCache pipelineCache, uint createInfoCount, ComputePipelineCreateInfo[] createInfos, AllocationCallbacks* allocator, Pipeline[] pipelines);
        public unsafe delegate void DestroyPipelineDelegate(Device device, Pipeline pipeline, AllocationCallbacks* allocator);
        public unsafe delegate Result CreatePipelineLayoutDelegate(Device device, ref PipelineLayoutCreateInfo createInfo, AllocationCallbacks* allocator, out PipelineLayout pipelineLayout);
        public unsafe delegate void DestroyPipelineLayoutDelegate(Device device, PipelineLayout pipelineLayout, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateSamplerDelegate(Device device, ref SamplerCreateInfo createInfo, AllocationCallbacks* allocator, out Sampler sampler);
        public unsafe delegate void DestroySamplerDelegate(Device device, Sampler sampler, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateDescriptorSetLayoutDelegate(Device device, ref DescriptorSetLayoutCreateInfo createInfo, AllocationCallbacks* allocator, out DescriptorSetLayout setLayout);
        public unsafe delegate void DestroyDescriptorSetLayoutDelegate(Device device, DescriptorSetLayout descriptorSetLayout, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateDescriptorPoolDelegate(Device device, ref DescriptorPoolCreateInfo createInfo, AllocationCallbacks* allocator, out DescriptorPool descriptorPool);
        public unsafe delegate void DestroyDescriptorPoolDelegate(Device device, DescriptorPool descriptorPool, AllocationCallbacks* allocator);
        public unsafe delegate Result ResetDescriptorPoolDelegate(Device device, DescriptorPool descriptorPool, DescriptorPoolResetFlags flags);
        public unsafe delegate Result AllocateDescriptorSetsDelegate(Device device, ref DescriptorSetAllocateInfo allocateInfo, DescriptorSet[] descriptorSets);
        public unsafe delegate Result FreeDescriptorSetsDelegate(Device device, DescriptorPool descriptorPool, uint descriptorSetCount, DescriptorSet[] descriptorSets);
        public unsafe delegate void UpdateDescriptorSetsDelegate(Device device, uint descriptorWriteCount, WriteDescriptorSet[] descriptorWrites, uint descriptorCopyCount, CopyDescriptorSet[] descriptorCopies);
        public unsafe delegate Result CreateFramebufferDelegate(Device device, ref FramebufferCreateInfo createInfo, AllocationCallbacks* allocator, out Framebuffer framebuffer);
        public unsafe delegate void DestroyFramebufferDelegate(Device device, Framebuffer framebuffer, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateRenderPassDelegate(Device device, ref RenderPassCreateInfo createInfo, AllocationCallbacks* allocator, out RenderPass renderPass);
        public unsafe delegate void DestroyRenderPassDelegate(Device device, RenderPass renderPass, AllocationCallbacks* allocator);
        public unsafe delegate void GetRenderAreaGranularityDelegate(Device device, RenderPass renderPass, out Extent2D granularity);
        public unsafe delegate Result CreateCommandPoolDelegate(Device device, ref CommandPoolCreateInfo createInfo, AllocationCallbacks* allocator, out CommandPool commandPool);
        public unsafe delegate void DestroyCommandPoolDelegate(Device device, CommandPool commandPool, AllocationCallbacks* allocator);
        public unsafe delegate Result ResetCommandPoolDelegate(Device device, CommandPool commandPool, CommandPoolResetFlags flags);
        public unsafe delegate Result AllocateCommandBuffersDelegate(Device device, ref CommandBufferAllocateInfo allocateInfo, CommandBuffer[] commandBuffers);
        public unsafe delegate void FreeCommandBuffersDelegate(Device device, CommandPool commandPool, uint commandBufferCount, CommandBuffer[] commandBuffers);
        public unsafe delegate Result BeginCommandBufferDelegate(CommandBuffer commandBuffer, ref CommandBufferBeginInfo beginInfo);
        public unsafe delegate Result EndCommandBufferDelegate(CommandBuffer commandBuffer);
        public unsafe delegate Result ResetCommandBufferDelegate(CommandBuffer commandBuffer, CommandBufferResetFlags flags);
        public unsafe delegate void CmdBindPipelineDelegate(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, Pipeline pipeline);
        public unsafe delegate void CmdSetViewportDelegate(CommandBuffer commandBuffer, uint firstViewport, uint viewportCount, Viewport[] viewports);
        public unsafe delegate void CmdSetScissorDelegate(CommandBuffer commandBuffer, uint firstScissor, uint scissorCount, Rect2D[] scissors);
        public unsafe delegate void CmdSetLineWidthDelegate(CommandBuffer commandBuffer, float lineWidth);
        public unsafe delegate void CmdSetDepthBiasDelegate(CommandBuffer commandBuffer, float depthBiasrefantFactor, float depthBiasClamp, float depthBiasSlopeFactor);
        public unsafe delegate void CmdSetBlendConstantsDelegate(CommandBuffer commandBuffer, float[] blendConstants);
        public unsafe delegate void CmdSetDepthBoundsDelegate(CommandBuffer commandBuffer, float minDepthBounds, float maxDepthBounds);
        public unsafe delegate void CmdSetStencilCompareMaskDelegate(CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint compareMask);
        public unsafe delegate void CmdSetStencilWriteMaskDelegate(CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint writeMask);
        public unsafe delegate void CmdSetStencilReferenceDelegate(CommandBuffer commandBuffer, StencilFaceFlags faceMask, uint reference);
        public unsafe delegate void CmdBindDescriptorSetsDelegate(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineLayout layout, uint firstSet, uint descriptorSetCount, DescriptorSet[] descriptorSets, uint dynamicOffsetCount, uint[] dynamicOffsets);
        public unsafe delegate void CmdBindIndexBufferDelegate(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, IndexType indexType);
        public unsafe delegate void CmdBindVertexBuffersDelegate(CommandBuffer commandBuffer, uint firstBinding, uint bindingCount, Buffer[] buffers, DeviceSize[] offsets);
        public unsafe delegate void CmdDrawDelegate(CommandBuffer commandBuffer, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);
        public unsafe delegate void CmdDrawIndexedDelegate(CommandBuffer commandBuffer, uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset, uint firstInstance);
        public unsafe delegate void CmdDrawIndirectDelegate(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, uint drawCount, uint stride);
        public unsafe delegate void CmdDrawIndexedIndirectDelegate(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, uint drawCount, uint stride);
        public unsafe delegate void CmdDispatchDelegate(CommandBuffer commandBuffer, uint groupCountX, uint groupCountY, uint groupCountZ);
        public unsafe delegate void CmdDispatchIndirectDelegate(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset);
        public unsafe delegate void CmdCopyBufferDelegate(CommandBuffer commandBuffer, Buffer srcBuffer, Buffer dstBuffer, uint regionCount, BufferCopy[] regions);
        public unsafe delegate void CmdCopyImageDelegate(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageCopy[] regions);
        public unsafe delegate void CmdBlitImageDelegate(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageBlit[] regions, Filter filter);
        public unsafe delegate void CmdCopyBufferToImageDelegate(CommandBuffer commandBuffer, Buffer srcBuffer, Image dstImage, ImageLayout dstImageLayout, uint regionCount, BufferImageCopy[] regions);
        public unsafe delegate void CmdCopyImageToBufferDelegate(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Buffer dstBuffer, uint regionCount, BufferImageCopy[] regions);
        public unsafe delegate void CmdUpdateBufferDelegate(CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize dataSize, void* data);
        public unsafe delegate void CmdFillBufferDelegate(CommandBuffer commandBuffer, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize size, uint data);
        public unsafe delegate void CmdClearColorImageDelegate(CommandBuffer commandBuffer, Image image, ImageLayout imageLayout, ref ClearColorValue color, uint rangeCount, ImageSubresourceRange[] ranges);
        public unsafe delegate void CmdClearDepthStencilImageDelegate(CommandBuffer commandBuffer, Image image, ImageLayout imageLayout, ref ClearDepthStencilValue depthStencil, uint rangeCount, ImageSubresourceRange[] ranges);
        public unsafe delegate void CmdClearAttachmentsDelegate(CommandBuffer commandBuffer, uint attachmentCount, ClearAttachment[] attachments, uint rectCount, ClearRect[] rects);
        public unsafe delegate void CmdResolveImageDelegate(CommandBuffer commandBuffer, Image srcImage, ImageLayout srcImageLayout, Image dstImage, ImageLayout dstImageLayout, uint regionCount, ImageResolve[] regions);
        public unsafe delegate void CmdSetEventDelegate(CommandBuffer commandBuffer, Event evt, PipelineStageFlags stageMask);
        public unsafe delegate void CmdResetEventDelegate(CommandBuffer commandBuffer, Event evt, PipelineStageFlags stageMask);
        public unsafe delegate void CmdWaitEventsDelegate(CommandBuffer commandBuffer, uint eventCount, Event[] events, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, uint memoryBarrierCount, MemoryBarrier[] memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier[] bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier[] imageMemoryBarriers);
        public unsafe delegate void CmdPipelineBarrierDelegate(CommandBuffer commandBuffer, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, uint memoryBarrierCount, MemoryBarrier[] memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier[] bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier[] imageMemoryBarriers);
        public unsafe delegate void CmdBeginQueryDelegate(CommandBuffer commandBuffer, QueryPool queryPool, uint query, QueryControlFlags flags);
        public unsafe delegate void CmdEndQueryDelegate(CommandBuffer commandBuffer, QueryPool queryPool, uint query);
        public unsafe delegate void CmdResetQueryPoolDelegate(CommandBuffer commandBuffer, QueryPool queryPool, uint firstQuery, uint queryCount);
        public unsafe delegate void CmdWriteTimestampDelegate(CommandBuffer commandBuffer, PipelineStageFlags pipelineStage, QueryPool queryPool, uint query);
        public unsafe delegate void CmdCopyQueryPoolResultsDelegate(CommandBuffer commandBuffer, QueryPool queryPool, uint firstQuery, uint queryCount, Buffer dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags);
        public unsafe delegate void CmdPushConstantsDelegate(CommandBuffer commandBuffer, PipelineLayout layout, ShaderStageFlags stageFlags, uint offset, uint size, void* values);
        public unsafe delegate void CmdBeginRenderPassDelegate(CommandBuffer commandBuffer, ref RenderPassBeginInfo renderPassBegin, SubpassContents contents);
        public unsafe delegate void CmdNextSubpassDelegate(CommandBuffer commandBuffer, SubpassContents contents);
        public unsafe delegate void CmdEndRenderPassDelegate(CommandBuffer commandBuffer);
        public unsafe delegate void CmdExecuteCommandsDelegate(CommandBuffer commandBuffer, uint commandBufferCount, CommandBuffer[] commandBuffers);
        // Khronos
        public unsafe delegate void DestroySurfaceKHRDelegate(Instance instance, Surface surface, AllocationCallbacks* allocator);
        public unsafe delegate Result GetPhysicalDeviceSurfaceSupportKHRDelegate(PhysicalDevice physicalDevice, uint queueFamilyIndex, Surface surface, out Bool32 supported);
        public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate(PhysicalDevice physicalDevice, Surface surface, out SurfaceCapabilities surfaceCapabilities);
        public unsafe delegate Result GetPhysicalDeviceSurfaceFormatsKHRDelegate(PhysicalDevice physicalDevice, Surface surface, out uint surfaceFormatCount, [Out] SurfaceFormat[] surfaceFormats);
        public unsafe delegate Result GetPhysicalDeviceSurfacePresentModesKHRDelegate(PhysicalDevice physicalDevice, Surface surface, out uint presentModeCount, [Out] PresentMode[] presentModes);
        public unsafe delegate Result CreateSwapchainKHRDelegate(Device device, ref SwapchainCreateInfo createInfo, AllocationCallbacks* allocator, out Swapchain swapchain);
        public unsafe delegate void DestroySwapchainKHRDelegate(Device device, Swapchain swapchain, AllocationCallbacks* allocator);
        public unsafe delegate Result GetSwapchainImagesKHRDelegate(Device device, Swapchain swapchain, out uint swapchainImageCount, [Out] Image[] swapchainImages);
        public unsafe delegate Result AcquireNextImageKHRDelegate(Device device, Swapchain swapchain, ulong timeout, Semaphore semaphore, Fence fence, out uint imageIndex);
        public unsafe delegate Result QueuePresentKHRDelegate(Queue queue, ref PresentInfo presentInfo);
        public unsafe delegate Result GetPhysicalDeviceDisplayPropertiesKHRDelegate(PhysicalDevice physicalDevice, out uint propertyCount, [Out] DisplayProperties[] properties);
        public unsafe delegate Result GetPhysicalDeviceDisplayPlanePropertiesKHRDelegate(PhysicalDevice physicalDevice, out uint propertyCount, [Out] DisplayPlaneProperties[] properties);
        public unsafe delegate Result GetDisplayPlaneSupportedDisplaysKHRDelegate(PhysicalDevice physicalDevice, uint planeIndex, out uint displayCount, [Out] Display[] displays);
        public unsafe delegate Result GetDisplayModePropertiesKHRDelegate(PhysicalDevice physicalDevice, Display display, out uint propertyCount, [Out] DisplayModeProperties[] properties);
        public unsafe delegate Result CreateDisplayModeKHRDelegate(PhysicalDevice physicalDevice, Display display, ref DisplayModeCreateInfo createInfo, AllocationCallbacks* allocator, out DisplayMode mode);
        public unsafe delegate Result GetDisplayPlaneCapabilitiesKHRDelegate(PhysicalDevice physicalDevice, DisplayMode mode, uint planeIndex, out DisplayPlaneCapabilities capabilities);
        public unsafe delegate Result CreateDisplayPlaneSurfaceKHRDelegate(Instance instance, ref DisplaySurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Result CreateSharedSwapchainsKHRDelegate(Device device, uint swapchainCount, SwapchainCreateInfo[] createInfos, AllocationCallbacks* allocator, Swapchain[] swapchains);
        public unsafe delegate Result CreateXlibSurfaceKHRDelegate(Instance instance, ref XlibSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Bool32 GetPhysicalDeviceXlibPresentationSupportKHRDelegate(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr dpy, IntPtr visualID);
        public unsafe delegate Result CreateXcbSurfaceKHRDelegate(Instance instance, ref XcbSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Bool32 GetPhysicalDeviceXcbPresentationSupportKHRDelegate(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection, IntPtr visualId);
        public unsafe delegate Result CreateWaylandSurfaceKHRDelegate(Instance instance, ref WaylandSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Bool32 GetPhysicalDeviceWaylandPresentationSupportKHRDelegate(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr display);
        public unsafe delegate Result CreateMirSurfaceKHRDelegate(Instance instance, ref MirSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Bool32 GetPhysicalDeviceMirPresentationSupportKHRDelegate(PhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection);
        public unsafe delegate Result CreateAndroidSurfaceKHRDelegate(Instance instance, ref AndroidSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Result CreateWin32SurfaceKHRDelegate(Instance instance, ref Win32SurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Bool32 GetPhysicalDeviceWin32PresentationSupportKHRDelegate(PhysicalDevice physicalDevice, uint queueFamilyIndex);
        public unsafe delegate void GetPhysicalDeviceFeatures2KHRDelegate(PhysicalDevice physicalDevice, out PhysicalDeviceFeatures2 features);
        public unsafe delegate void GetPhysicalDeviceProperties2KHRDelegate(PhysicalDevice physicalDevice, out PhysicalDeviceProperties2 properties);
        public unsafe delegate void GetPhysicalDeviceFormatProperties2KHRDelegate(PhysicalDevice physicalDevice, Format format, out FormatProperties2 formatProperties);
        public unsafe delegate Result GetPhysicalDeviceImageFormatProperties2KHRDelegate(PhysicalDevice physicalDevice, ref PhysicalDeviceImageFormatInfo2 imageFormatInfo, out ImageFormatProperties2 imageFormatProperties);
        public unsafe delegate void GetPhysicalDeviceQueueFamilyProperties2KHRDelegate(PhysicalDevice physicalDevice, out uint queueFamilyPropertyCount, [Out] QueueFamilyProperties2[] queueFamilyProperties);
        public unsafe delegate void GetPhysicalDeviceMemoryProperties2KHRDelegate(PhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties2 memoryProperties);
        public unsafe delegate void GetPhysicalDeviceSparseImageFormatProperties2KHRDelegate(PhysicalDevice physicalDevice, ref PhysicalDeviceSparseImageFormatInfo2 formatInfo, out uint propertyCount, [Out] SparseImageFormatProperties2[] properties);
        public unsafe delegate void TrimCommandPoolKHRDelegate(Device device, CommandPool commandPool, CommandPoolTrimFlags flags);
        public unsafe delegate void GetPhysicalDeviceExternalBufferPropertiesKHRDelegate(PhysicalDevice physicalDevice, ref PhysicalDeviceExternalBufferInfo externalBufferInfo, out ExternalBufferProperties externalBufferProperties);
        public unsafe delegate Result GetMemoryWin32HandleKHRDelegate(Device device, ref MemoryGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
        public unsafe delegate Result GetMemoryWin32HandlePropertiesKHRDelegate(Device device, ExternalMemoryHandleTypeFlags handleType, IntPtr handle, out MemoryWin32HandleProperties memoryWin32HandleProperties);
        public unsafe delegate Result GetMemoryFdKHRDelegate(Device device, ref MemoryGetFdInfo getFdInfo, out int fd);
        public unsafe delegate Result GetMemoryFdPropertiesKHRDelegate(Device device, ExternalMemoryHandleTypeFlags handleType, int fd, out MemoryFdProperties pMemoryFdProperties);
        public unsafe delegate void GetPhysicalDeviceExternalSemaphorePropertiesKHRDelegate(PhysicalDevice physicalDevice, ref PhysicalDeviceExternalSemaphoreInfo externalSemaphoreInfo, out ExternalSemaphoreProperties externalSemaphoreProperties);
        public unsafe delegate Result ImportSemaphoreWin32HandleKHRDelegate(Device device, ref ImportSemaphoreWin32HandleInfo importSemaphoreWin32HandleInfo);
        public unsafe delegate Result GetSemaphoreWin32HandleKHRDelegate(Device device, ref SemaphoreGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
        public unsafe delegate Result ImportSemaphoreFdKHRDelegate(Device device, ref ImportSemaphoreFdInfo importSemaphoreFdInfo);
        public unsafe delegate Result GetSemaphoreFdKHRDelegate(Device device, ref SemaphoreGetFdInfo getFdInfo, out int fd);
        public unsafe delegate void CmdPushDescriptorSetKHRDelegate(CommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineLayout layout, uint set, uint descriptorWriteCount, WriteDescriptorSet[] descriptorWrites);
        public unsafe delegate Result CreateDescriptorUpdateTemplateKHRDelegate(Device device, ref DescriptorUpdateTemplateCreateInfo createInfo, AllocationCallbacks* allocator, out DescriptorUpdateTemplate descriptorUpdateTemplate);
        public unsafe delegate void DestroyDescriptorUpdateTemplateKHRDelegate(Device device, DescriptorUpdateTemplate descriptorUpdateTemplate, AllocationCallbacks* allocator);
        public unsafe delegate void UpdateDescriptorSetWithTemplateKHRDelegate(Device device, DescriptorSet descriptorSet, DescriptorUpdateTemplate descriptorUpdateTemplate, void* data);
        public unsafe delegate void CmdPushDescriptorSetWithTemplateKHRDelegate(CommandBuffer commandBuffer, DescriptorUpdateTemplate descriptorUpdateTemplate, PipelineLayout layout, uint set, void* data);
        public unsafe delegate Result GetSwapchainStatusKHRDelegate(Device device, Swapchain swapchain);
        public unsafe delegate void GetPhysicalDeviceExternalFencePropertiesKHRDelegate(PhysicalDevice physicalDevice, ref PhysicalDeviceExternalFenceInfo externalFenceInfo, out ExternalFenceProperties externalFenceProperties);
        public unsafe delegate Result ImportFenceWin32HandleKHRDelegate(Device device, ref ImportFenceWin32HandleInfo importFenceWin32HandleInfo);
        public unsafe delegate Result GetFenceWin32HandleKHRDelegate(Device device, ref FenceGetWin32HandleInfo getWin32HandleInfo, IntPtr pHandle);
        public unsafe delegate Result ImportFenceFdKHRDelegate(Device device, ref ImportFenceFdInfo importFenceFdInfo);
        public unsafe delegate Result GetFenceFdKHRDelegate(Device device, ref FenceGetFdInfo getFdInfo, out int fd);
        public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2KHRDelegate(PhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out SurfaceCapabilities2 surfaceCapabilities);
        public unsafe delegate Result GetPhysicalDeviceSurfaceFormats2KHRDelegate(PhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out uint surfaceFormatCount, [Out] SurfaceFormat2[] surfaceFormats);
        public unsafe delegate void GetImageMemoryRequirements2KHRDelegate(Device device, ref ImageMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
        public unsafe delegate void GetBufferMemoryRequirements2KHRDelegate(Device device, ref BufferMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
        public unsafe delegate void GetImageSparseMemoryRequirements2KHRDelegate(Device device, ref ImageSparseMemoryRequirementsInfo2 info, out uint sparseMemoryRequirementCount, [Out] SparseImageMemoryRequirements2[] sparseMemoryRequirements);
        // Khronos X
        public unsafe delegate void GetDeviceGroupPeerMemoryFeaturesKHXDelegate(Device device, uint heapIndex, uint localDeviceIndex, uint remoteDeviceIndex, out PeerMemoryFeatureFlags peerMemoryFeatures);
        public unsafe delegate Result BindBufferMemory2KHXDelegate(Device device, uint bindInfoCount, BindBufferMemoryInfo[] bindInfos);
        public unsafe delegate Result BindImageMemory2KHXDelegate(Device device, uint bindInfoCount, BindImageMemoryInfo[] bindInfos);
        public unsafe delegate void CmdSetDeviceMaskKHXDelegate(CommandBuffer commandBuffer, uint deviceMask);
        public unsafe delegate Result GetDeviceGroupPresentCapabilitiesKHXDelegate(Device device, out DeviceGroupPresentCapabilities deviceGroupPresentCapabilities);
        public unsafe delegate Result GetDeviceGroupSurfacePresentModesKHXDelegate(Device device, Surface surface, out DeviceGroupPresentModeFlags modes);
        public unsafe delegate Result AcquireNextImage2KHXDelegate(Device device, ref AcquireNextImageInfo acquireInfo, out uint imageIndex);
        public unsafe delegate void CmdDispatchBaseKHXDelegate(CommandBuffer commandBuffer, uint baseGroupX, uint baseGroupY, uint baseGroupZ, uint groupCountX, uint groupCountY, uint groupCountZ);
        public unsafe delegate Result GetPhysicalDevicePresentRectanglesKHXDelegate(PhysicalDevice physicalDevice, Surface surface, ref uint rectCount, Rect2D[] rects);
        public unsafe delegate Result EnumeratePhysicalDeviceGroupsKHXDelegate(Instance instance, ref uint physicalDeviceGroupCount, PhysicalDeviceGroupProperties[] physicalDeviceGroupProperties);
        // Multi-vendor
        public unsafe delegate Bool32 DebugReportCallbackEXTDelegate(DebugReportFlags flags, DebugReportObjectType objectType, ulong objectHandle, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData);
        public unsafe delegate Result CreateDebugReportCallbackEXTDelegate(Instance instance, ref DebugReportCallbackCreateInfo createInfo, AllocationCallbacks* allocator, out DebugReportCallback callback);
        public unsafe delegate void DestroyDebugReportCallbackEXTDelegate(Instance instance, DebugReportCallback callback, AllocationCallbacks* allocator);
        public unsafe delegate void DebugReportMessageEXTDelegate(Instance instance, DebugReportFlags flags, DebugReportObjectType objectType, ulong obj, Size location, int messageCode, byte* layerPrefix, byte* message);
        public unsafe delegate Result DebugMarkerSetObjectTagEXTDelegate(Device device, ref DebugMarkerObjectTagInfo tagInfo);
        public unsafe delegate Result DebugMarkerSetObjectNameEXTDelegate(Device device, ref DebugMarkerObjectNameInfo nameInfo);
        public unsafe delegate void CmdDebugMarkerBeginEXTDelegate(CommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
        public unsafe delegate void CmdDebugMarkerEndEXTDelegate(CommandBuffer commandBuffer);
        public unsafe delegate void CmdDebugMarkerInsertEXTDelegate(CommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
        public unsafe delegate Result ReleaseDisplayEXTDelegate(PhysicalDevice physicalDevice, Display display);
        public unsafe delegate Result AcquireXlibDisplayEXTDelegate(PhysicalDevice physicalDevice, IntPtr dpy, Display display);
        public unsafe delegate Result GetRandROutputDisplayEXTDelegate(PhysicalDevice physicalDevice, IntPtr dpy, IntPtr rrOutput, out Display display);
        public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2EXTDelegate(PhysicalDevice physicalDevice, Surface surface, out SurfaceCapabilities2EXT surfaceCapabilities);
        public unsafe delegate Result DisplayPowerControlEXTDelegate(Device device, Display display, ref DisplayPowerInfo displayPowerInfo);
        public unsafe delegate Result RegisterDeviceEventEXTDelegate(Device device, ref DeviceEventInfo deviceEventInfo, AllocationCallbacks* allocator, out Fence fence);
        public unsafe delegate Result RegisterDisplayEventEXTDelegate(Device device, Display display, ref DisplayEventInfo displayEventInfo, AllocationCallbacks* allocator, out Fence fence);
        public unsafe delegate Result GetSwapchainCounterEXTDelegate(Device device, Swapchain swapchain, SurfaceCounterFlags counter, out ulong counterValue);
        public unsafe delegate void CmdSetDiscardRectangleEXTDelegate(CommandBuffer commandBuffer, uint firstDiscardRectangle, uint discardRectangleCount, Rect2D[] discardRectangles);
        public unsafe delegate void SetHdrMetadataEXTDelegate(Device device, uint swapchainCount, Swapchain[] swapchains, HdrMetadata[] metadata);
        // AMD
        public unsafe delegate void CmdDrawIndirectCountAMDDelegate(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, Buffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);
        public unsafe delegate void CmdDrawIndexedIndirectCountAMDDelegate(CommandBuffer commandBuffer, Buffer buffer, DeviceSize offset, Buffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);
        // Nvidia
        public unsafe delegate Result GetPhysicalDeviceExternalImageFormatPropertiesNVDelegate(PhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, ExternalMemoryHandleTypeFlagsNV ExternalHandleType, out ExternalImageFormatPropertiesNV externalImageFormatProperties);
        public unsafe delegate Result GetMemoryWin32HandleNVDelegate(Device device, DeviceMemory memory, ExternalMemoryHandleTypeFlagsNV handleType, IntPtr handle);
        public unsafe delegate void CmdSetViewportWScalingNVDelegate(CommandBuffer commandBuffer, uint firstViewport, uint viewportCount, ViewportWScaling[] viewportWScalings);
        // Nvidia X
        public unsafe delegate void CmdProcessCommandsNVXDelegate(CommandBuffer commandBuffer, ref CmdProcessCommandsInfo processCommandsInfo);
        public unsafe delegate void CmdReserveSpaceForCommandsNVXDelegate(CommandBuffer commandBuffer, ref CmdReserveSpaceForCommandsInfo reserveSpaceInfo);
        public unsafe delegate Result CreateIndirectCommandsLayoutNVXDelegate(Device device, ref IndirectCommandsLayoutCreateInfo createInfo, AllocationCallbacks* allocator, out IndirectCommandsLayout indirectCommandsLayout);
        public unsafe delegate void DestroyIndirectCommandsLayoutNVXDelegate(Device device, IndirectCommandsLayout indirectCommandsLayout, AllocationCallbacks* allocator);
        public unsafe delegate Result CreateObjectTableNVXDelegate(Device device, ref ObjectTableCreateInfo createInfo, AllocationCallbacks* allocator, out ObjectTable objectTable);
        public unsafe delegate void DestroyObjectTableNVXDelegate(Device device, ObjectTable objectTable, AllocationCallbacks* allocator);
        public unsafe delegate Result RegisterObjectsNVXDelegate(Device device, ObjectTable objectTable, uint objectCount, ref ObjectTableEntry[] objectTableEntries, uint[] objectIndices);
        public unsafe delegate Result UnregisterObjectsNVXDelegate(Device device, ObjectTable objectTable, uint objectCount, ObjectEntryType[] objectEntryTypes, uint[] objectIndices);
        public unsafe delegate void GetPhysicalDeviceGeneratedCommandsPropertiesNVXDelegate(PhysicalDevice physicalDevice, out DeviceGeneratedCommandsFeatures features, out DeviceGeneratedCommandsLimits limits);
        // Nintendo
        public unsafe delegate Result CreateViSurfaceNNDelegate(Instance instance, ref ViSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        // Google
        public unsafe delegate Result GetRefreshCycleDurationGOOGLEDelegate(Device device, Swapchain swapchain, ref RefreshCycleDuration displayTimingProperties);
        public unsafe delegate Result GetPastPresentationTimingGOOGLEDelegate(Device device, Swapchain swapchain, ref uint presentationTimingCount, [Out] PastPresentationTiming[] presentationTimings);
        // MoltenVK
        public unsafe delegate Result CreateIOSSurfaceMVKDelegate(Instance instance, ref IOSSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
        public unsafe delegate Result CreateMacOSSurfaceMVKDelegate(Instance instance, ref MacOSSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out Surface surface);
    }
}