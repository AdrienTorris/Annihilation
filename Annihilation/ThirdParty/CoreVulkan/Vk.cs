using System;
using System.Runtime.InteropServices;
using SDL2;

namespace Vulkan
{
    public static unsafe partial class Vk
    {
        public static GetInstanceProcAddrDelegate GetInstanceProcAddr;
        public static GetDeviceProcAddrDelegate GetDeviceProcAddr;
        
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
        
        //
        // Methods
        //
        public static void LoadLibrary()
        {
            if ((SDL.WasInit(SDL.InitFlags.Video) & SDL.InitFlags.Video) == 0)
            {
                SDL.InitSubSystem(SDL.InitFlags.Video);
            }
            SDL.VulkanLoadLibrary(null).CheckError();
        }

        public static void UnloadLibrary()
        {
            SDL.VulkanUnloadLibrary();
        }

        public static void LoadGetInstanceProcAddrFunction()
        {
            GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddrDelegate>(SDL.VulkanGetVkGetInstanceProcAddr());
        }
        public static T LoadGlobalFunction<T>() => LoadInstanceFunction<T>(VkInstance.Null);

        public static T LoadInstanceFunction<T>(VkInstance instance)
        {
            // TODO: Don't allocate string here
            string name = "vk" + typeof(T).Name;
            name = name.Replace("Delegate", "");
            byte* nameUtf8 = Utf8.AllocateFromString(name);
            
            IntPtr function = GetInstanceProcAddr(instance, nameUtf8);

            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }

            Utf8.Free(nameUtf8);

            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }
        
        public static T LoadDeviceFunction<T>(VkDevice device)
        {
            // TODO: Don't allocate string here
            string name = "vk" + typeof(T).Name;
            name = name.Replace("Delegate", "");
            byte* nameUtf8 = Utf8.AllocateFromString(name);

            IntPtr function = GetDeviceProcAddr(device, nameUtf8);

            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }

            Utf8.Free(nameUtf8);
            
            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

        //
        // Delegates
        //
        public delegate void* AllocationFunction(void* userData, Size size, Size alignment, SystemAllocationScope allocationScope);
        public delegate void* ReallocationFunction(void* userData, void* original, Size size, Size alignment, SystemAllocationScope allocationScope);
        public delegate void FreeFunction(void* userData, void* memory);
        public delegate void InternalAllocationNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);
        public delegate void InternalFreeNotification(void* userData, Size size, InternalAllocationType allocationType, SystemAllocationScope allocationScope);
        public delegate void VoidFunction();

        //
        // Function delegates
        //
        public delegate Result CreateInstanceDelegate(ref InstanceCreateInfo createInfo, AllocationCallbacks* allocator, out VkInstance instance);
        public delegate void DestroyInstanceDelegate(VkInstance instance, AllocationCallbacks* allocator);
        public delegate Result EnumeratePhysicalDevicesDelegate(VkInstance instance, ref uint physicalDeviceCount, VkPhysicalDevice* physicalDevices);
        public delegate void GetPhysicalDeviceFeaturesDelegate(VkPhysicalDevice physicalDevice, out PhysicalDeviceFeatures features);
        public delegate void GetPhysicalDeviceFormatPropertiesDelegate(VkPhysicalDevice physicalDevice, Format format, out FormatProperties formatProperties);
        public delegate Result GetPhysicalDeviceImageFormatPropertiesDelegate(VkPhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties);
        public delegate void GetPhysicalDevicePropertiesDelegate(VkPhysicalDevice physicalDevice, out PhysicalDeviceProperties properties);
        public delegate void GetPhysicalDeviceQueueFamilyPropertiesDelegate(VkPhysicalDevice physicalDevice, ref uint queueFamilyPropertyCount, QueueFamilyProperties* queueFamilyProperties);
        public delegate void GetPhysicalDeviceMemoryPropertiesDelegate(VkPhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties memoryProperties);
        public delegate IntPtr GetInstanceProcAddrDelegate(VkInstance instance, byte* name);
        public delegate IntPtr GetDeviceProcAddrDelegate(VkDevice device, byte* name);
        public delegate Result CreateDeviceDelegate(VkPhysicalDevice physicalDevice, ref DeviceCreateInfo createInfo, AllocationCallbacks* allocator, out VkDevice device);
        public delegate void DestroyDeviceDelegate(VkDevice device, AllocationCallbacks* allocator);
        public delegate Result EnumerateInstanceExtensionPropertiesDelegate(byte* layerName, ref uint propertyCount, ExtensionProperties* properties);
        public delegate Result EnumerateDeviceExtensionPropertiesDelegate(VkPhysicalDevice physicalDevice, byte* layerName, ref uint propertyCount, ExtensionProperties* properties);
        public delegate Result EnumerateInstanceLayerPropertiesDelegate(ref uint propertyCount, LayerProperties* properties);
        public delegate Result EnumerateDeviceLayerPropertiesDelegate(VkPhysicalDevice physicalDevice, ref uint propertyCount, LayerProperties* properties);
        public delegate void GetDeviceQueueDelegate(VkDevice device, uint queueFamilyIndex, uint queueIndex, out VkQueue queue);
        public delegate Result QueueSubmitDelegate(VkQueue queue, uint submitCount, SubmitInfo* submits, VkFence fence);
        public delegate Result QueueWaitIdleDelegate(VkQueue queue);
        public delegate Result DeviceWaitIdleDelegate(VkDevice device);
        public delegate Result AllocateMemoryDelegate(VkDevice device, ref MemoryAllocateInfo allocateInfo, AllocationCallbacks* allocator, out VkDeviceMemory memory);
        public delegate void FreeMemoryDelegate(VkDevice device, VkDeviceMemory memory, AllocationCallbacks* allocator);
        public delegate Result MapMemoryDelegate(VkDevice device, VkDeviceMemory memory, DeviceSize offset, DeviceSize size, MemoryMapFlags flags, void** data);
        public delegate void UnmapMemoryDelegate(VkDevice device, VkDeviceMemory memory);
        public delegate Result FlushMappedMemoryRangesDelegate(VkDevice device, uint memoryRangeCount, MappedMemoryRange* memoryRanges);
        public delegate Result InvalidateMappedMemoryRangesDelegate(VkDevice device, uint memoryRangeCount, MappedMemoryRange* memoryRanges);
        public delegate void GetDeviceMemoryCommitmentDelegate(VkDevice device, VkDeviceMemory memory, out DeviceSize committedMemoryInBytes);
        public delegate Result BindBufferMemoryDelegate(VkDevice device, VkBuffer buffer, VkDeviceMemory memory, DeviceSize memoryOffset);
        public delegate Result BindImageMemoryDelegate(VkDevice device, VkImage image, VkDeviceMemory memory, DeviceSize memoryOffset);
        public delegate void GetBufferMemoryRequirementsDelegate(VkDevice device, VkBuffer buffer, out MemoryRequirements memoryRequirements);
        public delegate void GetImageMemoryRequirementsDelegate(VkDevice device, VkImage image, out MemoryRequirements memoryRequirements);
        public delegate void GetImageSparseMemoryRequirementsDelegate(VkDevice device, VkImage image, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements* sparseMemoryRequirements);
        public delegate void GetPhysicalDeviceSparseImageFormatPropertiesDelegate(VkPhysicalDevice physicalDevice, Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, out uint propertyCount, SparseImageFormatProperties* properties);
        public delegate Result QueueBindSparseDelegate(VkQueue queue, uint bindInfoCount, ref BindSparseInfo bindInfo, VkFence fence);
        public delegate Result CreateFenceDelegate(VkDevice device, ref FenceCreateInfo createInfo, AllocationCallbacks* allocator, out VkFence fence);
        public delegate void DestroyFenceDelegate(VkDevice device, VkFence fence, AllocationCallbacks* allocator);
        public delegate Result ResetFencesDelegate(VkDevice device, uint fenceCount, VkFence* fences);
        public delegate Result GetFenceStatusDelegate(VkDevice device, VkFence fence);
        public delegate Result WaitForFencesDelegate(VkDevice device, uint fenceCount, VkFence* fences, Bool32 waitAll, ulong timeout);
        public delegate Result CreateSemaphoreDelegate(VkDevice device, ref SemaphoreCreateInfo createInfo, AllocationCallbacks* allocator, out VkSemaphore semaphore);
        public delegate void DestroySemaphoreDelegate(VkDevice device, VkSemaphore semaphore, AllocationCallbacks* allocator);
        public delegate Result CreateEventDelegate(VkDevice device, ref EventCreateInfo createInfo, AllocationCallbacks* allocator, out VkEvent evnt);
        public delegate void DestroyEventDelegate(VkDevice device, VkEvent evt, AllocationCallbacks* allocator);
        public delegate Result GetEventStatusDelegate(VkDevice device, VkEvent evt);
        public delegate Result SetEventDelegate(VkDevice device, VkEvent evt);
        public delegate Result ResetEventDelegate(VkDevice device, VkEvent evt);
        public delegate Result CreateQueryPoolDelegate(VkDevice device, ref QueryPoolCreateInfo createInfo, AllocationCallbacks* allocator, out VkQueryPool queryPool);
        public delegate void DestroyQueryPoolDelegate(VkDevice device, VkQueryPool queryPool, AllocationCallbacks* allocator);
        public delegate Result GetQueryPoolResultsDelegate(VkDevice device, VkQueryPool queryPool, uint firstQuery, uint queryCount, Size dataSize, void* data, DeviceSize stride, QueryResultFlags flags);
        public delegate Result CreateBufferDelegate(VkDevice device, ref BufferCreateInfo createInfo, AllocationCallbacks* allocator, out VkBuffer buffer);
        public delegate void DestroyBufferDelegate(VkDevice device, VkBuffer buffer, AllocationCallbacks* allocator);
        public delegate Result CreateBufferViewDelegate(VkDevice device, ref BufferViewCreateInfo createInfo, AllocationCallbacks* allocator, out VkBufferView view);
        public delegate void DestroyBufferViewDelegate(VkDevice device, VkBufferView bufferView, AllocationCallbacks* allocator);
        public delegate Result CreateImageDelegate(VkDevice device, ref ImageCreateInfo createInfo, AllocationCallbacks* allocator, out VkImage image);
        public delegate void DestroyImageDelegate(VkDevice device, VkImage image, AllocationCallbacks* allocator);
        public delegate void GetImageSubresourceLayoutDelegate(VkDevice device, VkImage image, ref ImageSubresource subresource, out SubresourceLayout layout);
        public delegate Result CreateImageViewDelegate(VkDevice device, ref ImageViewCreateInfo createInfo, AllocationCallbacks* allocator, out VkImageView view);
        public delegate void DestroyImageViewDelegate(VkDevice device, VkImageView imageView, AllocationCallbacks* allocator);
        public delegate Result CreateShaderModuleDelegate(VkDevice device, ref ShaderModuleCreateInfo createInfo, AllocationCallbacks* allocator, out VkShaderModule shaderModule);
        public delegate void DestroyShaderModuleDelegate(VkDevice device, VkShaderModule shaderModule, AllocationCallbacks* allocator);
        public delegate Result CreatePipelineCacheDelegate(VkDevice device, ref PipelineCacheCreateInfo createInfo, AllocationCallbacks* allocator, out VkPipelineCache pipelineCache);
        public delegate void DestroyPipelineCacheDelegate(VkDevice device, VkPipelineCache pipelineCache, AllocationCallbacks* allocator);
        public delegate Result GetPipelineCacheDataDelegate(VkDevice device, VkPipelineCache pipelineCache, out Size dataSize, void* data);
        public delegate Result MergePipelineCachesDelegate(VkDevice device, VkPipelineCache dstCache, uint srcCacheCount, ref VkPipelineCache* srcCaches);
        public delegate Result CreateGraphicsPipelinesDelegate(VkDevice device, VkPipelineCache pipelineCache, uint createInfoCount, GraphicsPipelineCreateInfo* createInfos, AllocationCallbacks* allocator, VkPipeline* pipelines);
        public delegate Result CreateComputePipelinesDelegate(VkDevice device, VkPipelineCache pipelineCache, uint createInfoCount, ComputePipelineCreateInfo* createInfos, AllocationCallbacks* allocator, VkPipeline* pipelines);
        public delegate void DestroyPipelineDelegate(VkDevice device, VkPipeline pipeline, AllocationCallbacks* allocator);
        public delegate Result CreatePipelineLayoutDelegate(VkDevice device, ref PipelineLayoutCreateInfo createInfo, AllocationCallbacks* allocator, out VkPipelineLayout pipelineLayout);
        public delegate void DestroyPipelineLayoutDelegate(VkDevice device, VkPipelineLayout pipelineLayout, AllocationCallbacks* allocator);
        public delegate Result CreateSamplerDelegate(VkDevice device, ref SamplerCreateInfo createInfo, AllocationCallbacks* allocator, out VkSampler sampler);
        public delegate void DestroySamplerDelegate(VkDevice device, VkSampler sampler, AllocationCallbacks* allocator);
        public delegate Result CreateDescriptorSetLayoutDelegate(VkDevice device, ref DescriptorSetLayoutCreateInfo createInfo, AllocationCallbacks* allocator, out VkDescriptorSetLayout setLayout);
        public delegate void DestroyDescriptorSetLayoutDelegate(VkDevice device, VkDescriptorSetLayout descriptorSetLayout, AllocationCallbacks* allocator);
        public delegate Result CreateDescriptorPoolDelegate(VkDevice device, ref DescriptorPoolCreateInfo createInfo, AllocationCallbacks* allocator, out VkDescriptorPool descriptorPool);
        public delegate void DestroyDescriptorPoolDelegate(VkDevice device, VkDescriptorPool descriptorPool, AllocationCallbacks* allocator);
        public delegate Result ResetDescriptorPoolDelegate(VkDevice device, VkDescriptorPool descriptorPool, DescriptorPoolResetFlags flags);
        public delegate Result AllocateDescriptorSetsDelegate(VkDevice device, ref DescriptorSetAllocateInfo allocateInfo, VkDescriptorSet* descriptorSets);
        public delegate Result FreeDescriptorSetsDelegate(VkDevice device, VkDescriptorPool descriptorPool, uint descriptorSetCount, VkDescriptorSet* descriptorSets);
        public delegate void UpdateDescriptorSetsDelegate(VkDevice device, uint descriptorWriteCount, WriteDescriptorSet* descriptorWrites, uint descriptorCopyCount, CopyDescriptorSet* descriptorCopies);
        public delegate Result CreateFramebufferDelegate(VkDevice device, ref FramebufferCreateInfo createInfo, AllocationCallbacks* allocator, out VkFramebuffer framebuffer);
        public delegate void DestroyFramebufferDelegate(VkDevice device, VkFramebuffer framebuffer, AllocationCallbacks* allocator);
        public delegate Result CreateRenderPassDelegate(VkDevice device, ref RenderPassCreateInfo createInfo, AllocationCallbacks* allocator, out VkRenderPass renderPass);
        public delegate void DestroyRenderPassDelegate(VkDevice device, VkRenderPass renderPass, AllocationCallbacks* allocator);
        public delegate void GetRenderAreaGranularityDelegate(VkDevice device, VkRenderPass renderPass, out Extent2D granularity);
        public delegate Result CreateCommandPoolDelegate(VkDevice device, ref CommandPoolCreateInfo createInfo, AllocationCallbacks* allocator, out VkCommandPool commandPool);
        public delegate void DestroyCommandPoolDelegate(VkDevice device, VkCommandPool commandPool, AllocationCallbacks* allocator);
        public delegate Result ResetCommandPoolDelegate(VkDevice device, VkCommandPool commandPool, CommandPoolResetFlags flags);
        public delegate Result AllocateCommandBuffersDelegate(VkDevice device, ref CommandBufferAllocateInfo allocateInfo, VkCommandBuffer* commandBuffers);
        public delegate void FreeCommandBuffersDelegate(VkDevice device, VkCommandPool commandPool, uint commandBufferCount, VkCommandBuffer* commandBuffers);
        public delegate Result BeginCommandBufferDelegate(VkCommandBuffer commandBuffer, ref CommandBufferBeginInfo beginInfo);
        public delegate Result EndCommandBufferDelegate(VkCommandBuffer commandBuffer);
        public delegate Result ResetCommandBufferDelegate(VkCommandBuffer commandBuffer, CommandBufferResetFlags flags);
        public delegate void CmdBindPipelineDelegate(VkCommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, VkPipeline pipeline);
        public delegate void CmdSetViewportDelegate(VkCommandBuffer commandBuffer, uint firstViewport, uint viewportCount, Viewport* viewports);
        public delegate void CmdSetScissorDelegate(VkCommandBuffer commandBuffer, uint firstScissor, uint scissorCount, Rect2D* scissors);
        public delegate void CmdSetLineWidthDelegate(VkCommandBuffer commandBuffer, float lineWidth);
        public delegate void CmdSetDepthBiasDelegate(VkCommandBuffer commandBuffer, float depthBiasrefantFactor, float depthBiasClamp, float depthBiasSlopeFactor);
        public delegate void CmdSetBlendConstantsDelegate(VkCommandBuffer commandBuffer, float* blendConstants);
        public delegate void CmdSetDepthBoundsDelegate(VkCommandBuffer commandBuffer, float minDepthBounds, float maxDepthBounds);
        public delegate void CmdSetStencilCompareMaskDelegate(VkCommandBuffer commandBuffer, StencilFaceFlags faceMask, uint compareMask);
        public delegate void CmdSetStencilWriteMaskDelegate(VkCommandBuffer commandBuffer, StencilFaceFlags faceMask, uint writeMask);
        public delegate void CmdSetStencilReferenceDelegate(VkCommandBuffer commandBuffer, StencilFaceFlags faceMask, uint reference);
        public delegate void CmdBindDescriptorSetsDelegate(VkCommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, VkPipelineLayout layout, uint firstSet, uint descriptorSetCount, VkDescriptorSet* descriptorSets, uint dynamicOffsetCount, uint* dynamicOffsets);
        public delegate void CmdBindIndexBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, DeviceSize offset, IndexType indexType);
        public delegate void CmdBindVertexBuffersDelegate(VkCommandBuffer commandBuffer, uint firstBinding, uint bindingCount, VkBuffer* buffers, DeviceSize* offsets);
        public delegate void CmdDrawDelegate(VkCommandBuffer commandBuffer, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);
        public delegate void CmdDrawIndexedDelegate(VkCommandBuffer commandBuffer, uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset, uint firstInstance);
        public delegate void CmdDrawIndirectDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, DeviceSize offset, uint drawCount, uint stride);
        public delegate void CmdDrawIndexedIndirectDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, DeviceSize offset, uint drawCount, uint stride);
        public delegate void CmdDispatchDelegate(VkCommandBuffer commandBuffer, uint groupCountX, uint groupCountY, uint groupCountZ);
        public delegate void CmdDispatchIndirectDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, DeviceSize offset);
        public delegate void CmdCopyBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer srcBuffer, VkBuffer dstBuffer, uint regionCount, BufferCopy* regions);
        public delegate void CmdCopyImageDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, ImageLayout srcImageLayout, VkImage dstImage, ImageLayout dstImageLayout, uint regionCount, ImageCopy* regions);
        public delegate void CmdBlitImageDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, ImageLayout srcImageLayout, VkImage dstImage, ImageLayout dstImageLayout, uint regionCount, ImageBlit* regions, Filter filter);
        public delegate void CmdCopyBufferToImageDelegate(VkCommandBuffer commandBuffer, VkBuffer srcBuffer, VkImage dstImage, ImageLayout dstImageLayout, uint regionCount, BufferImageCopy* regions);
        public delegate void CmdCopyImageToBufferDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, ImageLayout srcImageLayout, VkBuffer dstBuffer, uint regionCount, BufferImageCopy* regions);
        public delegate void CmdUpdateBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer dstBuffer, DeviceSize dstOffset, DeviceSize dataSize, void* data);
        public delegate void CmdFillBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer dstBuffer, DeviceSize dstOffset, DeviceSize size, uint data);
        public delegate void CmdClearColorImageDelegate(VkCommandBuffer commandBuffer, VkImage image, ImageLayout imageLayout, ref ClearColorValue color, uint rangeCount, ImageSubresourceRange* ranges);
        public delegate void CmdClearDepthStencilImageDelegate(VkCommandBuffer commandBuffer, VkImage image, ImageLayout imageLayout, ref ClearDepthStencilValue depthStencil, uint rangeCount, ImageSubresourceRange* ranges);
        public delegate void CmdClearAttachmentsDelegate(VkCommandBuffer commandBuffer, uint attachmentCount, ClearAttachment* attachments, uint rectCount, ClearRect* rects);
        public delegate void CmdResolveImageDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, ImageLayout srcImageLayout, VkImage dstImage, ImageLayout dstImageLayout, uint regionCount, ImageResolve* regions);
        public delegate void CmdSetEventDelegate(VkCommandBuffer commandBuffer, VkEvent evt, PipelineStageFlags stageMask);
        public delegate void CmdResetEventDelegate(VkCommandBuffer commandBuffer, VkEvent evt, PipelineStageFlags stageMask);
        public delegate void CmdWaitEventsDelegate(VkCommandBuffer commandBuffer, uint eventCount, VkEvent* events, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, uint memoryBarrierCount, MemoryBarrier* memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier* imageMemoryBarriers);
        public delegate void CmdPipelineBarrierDelegate(VkCommandBuffer commandBuffer, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, uint memoryBarrierCount, MemoryBarrier* memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier* imageMemoryBarriers);
        public delegate void CmdBeginQueryDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint query, QueryControlFlags flags);
        public delegate void CmdEndQueryDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint query);
        public delegate void CmdResetQueryPoolDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint firstQuery, uint queryCount);
        public delegate void CmdWriteTimestampDelegate(VkCommandBuffer commandBuffer, PipelineStageFlags pipelineStage, VkQueryPool queryPool, uint query);
        public delegate void CmdCopyQueryPoolResultsDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint firstQuery, uint queryCount, VkBuffer dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags);
        public delegate void CmdPushConstantsDelegate(VkCommandBuffer commandBuffer, VkPipelineLayout layout, ShaderStageFlags stageFlags, uint offset, uint size, void* values);
        public delegate void CmdBeginRenderPassDelegate(VkCommandBuffer commandBuffer, ref RenderPassBeginInfo renderPassBegin, SubpassContents contents);
        public delegate void CmdNextSubpassDelegate(VkCommandBuffer commandBuffer, SubpassContents contents);
        public delegate void CmdEndRenderPassDelegate(VkCommandBuffer commandBuffer);
        public delegate void CmdExecuteCommandsDelegate(VkCommandBuffer commandBuffer, uint commandBufferCount, VkCommandBuffer* commandBuffers);

        // Khronos
        public delegate void DestroySurfaceKHRDelegate(VkInstance instance, VkSurface surface, AllocationCallbacks* allocator);
        public delegate Result GetPhysicalDeviceSurfaceSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, VkSurface surface, out Bool32 supported);
        public delegate Result GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out SurfaceCapabilities surfaceCapabilities);
        public delegate Result GetPhysicalDeviceSurfaceFormatsKHRDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out uint surfaceFormatCount, SurfaceFormat* surfaceFormats);
        public delegate Result GetPhysicalDeviceSurfacePresentModesKHRDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out uint presentModeCount, PresentMode* presentModes);
        public delegate Result CreateSwapchainKHRDelegate(VkDevice device, ref SwapchainCreateInfo createInfo, AllocationCallbacks* allocator, out VkSwapchain swapchain);
        public delegate void DestroySwapchainKHRDelegate(VkDevice device, VkSwapchain swapchain, AllocationCallbacks* allocator);
        public delegate Result GetSwapchainImagesKHRDelegate(VkDevice device, VkSwapchain swapchain, out uint swapchainImageCount, VkImage* swapchainImages);
        public delegate Result AcquireNextImageKHRDelegate(VkDevice device, VkSwapchain swapchain, ulong timeout, VkSemaphore semaphore, VkFence fence, out uint imageIndex);
        public delegate Result QueuePresentKHRDelegate(VkQueue queue, ref PresentInfo presentInfo);
        public delegate Result GetPhysicalDeviceDisplayPropertiesKHRDelegate(VkPhysicalDevice physicalDevice, out uint propertyCount, DisplayProperties* properties);
        public delegate Result GetPhysicalDeviceDisplayPlanePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, out uint propertyCount, DisplayPlaneProperties* properties);
        public delegate Result GetDisplayPlaneSupportedDisplaysKHRDelegate(VkPhysicalDevice physicalDevice, uint planeIndex, out uint displayCount, VkDisplay* displays);
        public delegate Result GetDisplayModePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, VkDisplay display, out uint propertyCount, DisplayModeProperties* properties);
        public delegate Result CreateDisplayModeKHRDelegate(VkPhysicalDevice physicalDevice, VkDisplay display, ref DisplayModeCreateInfo createInfo, AllocationCallbacks* allocator, out VkDisplayMode mode);
        public delegate Result GetDisplayPlaneCapabilitiesKHRDelegate(VkPhysicalDevice physicalDevice, VkDisplayMode mode, uint planeIndex, out DisplayPlaneCapabilities capabilities);
        public delegate Result CreateDisplayPlaneSurfaceKHRDelegate(VkInstance instance, ref DisplaySurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Result CreateSharedSwapchainsKHRDelegate(VkDevice device, uint swapchainCount, SwapchainCreateInfo* createInfos, AllocationCallbacks* allocator, VkSwapchain* swapchains);
        public delegate Result CreateXlibSurfaceKHRDelegate(VkInstance instance, ref XlibSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Bool32 GetPhysicalDeviceXlibPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr dpy, IntPtr visualID);
        public delegate Result CreateXcbSurfaceKHRDelegate(VkInstance instance, ref XcbSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Bool32 GetPhysicalDeviceXcbPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection, IntPtr visualId);
        public delegate Result CreateWaylandSurfaceKHRDelegate(VkInstance instance, ref WaylandSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Bool32 GetPhysicalDeviceWaylandPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr display);
        public delegate Result CreateMirSurfaceKHRDelegate(VkInstance instance, ref MirSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Bool32 GetPhysicalDeviceMirPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection);
        public delegate Result CreateAndroidSurfaceKHRDelegate(VkInstance instance, ref AndroidSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Result CreateWin32SurfaceKHRDelegate(VkInstance instance, ref Win32SurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Bool32 GetPhysicalDeviceWin32PresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex);
        public delegate void GetPhysicalDeviceFeatures2KHRDelegate(VkPhysicalDevice physicalDevice, out PhysicalDeviceFeatures2 features);
        public delegate void GetPhysicalDeviceProperties2KHRDelegate(VkPhysicalDevice physicalDevice, out PhysicalDeviceProperties2 properties);
        public delegate void GetPhysicalDeviceFormatProperties2KHRDelegate(VkPhysicalDevice physicalDevice, Format format, out FormatProperties2 formatProperties);
        public delegate Result GetPhysicalDeviceImageFormatProperties2KHRDelegate(VkPhysicalDevice physicalDevice, ref PhysicalDeviceImageFormatInfo2 imageFormatInfo, out ImageFormatProperties2 imageFormatProperties);
        public delegate void GetPhysicalDeviceQueueFamilyProperties2KHRDelegate(VkPhysicalDevice physicalDevice, out uint queueFamilyPropertyCount, QueueFamilyProperties2* queueFamilyProperties);
        public delegate void GetPhysicalDeviceMemoryProperties2KHRDelegate(VkPhysicalDevice physicalDevice, out PhysicalDeviceMemoryProperties2 memoryProperties);
        public delegate void GetPhysicalDeviceSparseImageFormatProperties2KHRDelegate(VkPhysicalDevice physicalDevice, ref PhysicalDeviceSparseImageFormatInfo2 formatInfo, out uint propertyCount, SparseImageFormatProperties2* properties);
        public delegate void TrimCommandPoolKHRDelegate(VkDevice device, VkCommandPool commandPool, CommandPoolTrimFlags flags);
        public delegate void GetPhysicalDeviceExternalBufferPropertiesKHRDelegate(VkPhysicalDevice physicalDevice, ref PhysicalDeviceExternalBufferInfo externalBufferInfo, out ExternalBufferProperties externalBufferProperties);
        public delegate Result GetMemoryWin32HandleKHRDelegate(VkDevice device, ref MemoryGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
        public delegate Result GetMemoryWin32HandlePropertiesKHRDelegate(VkDevice device, ExternalMemoryHandleTypeFlags handleType, IntPtr handle, out MemoryWin32HandleProperties memoryWin32HandleProperties);
        public delegate Result GetMemoryFdKHRDelegate(VkDevice device, ref MemoryGetFdInfo getFdInfo, out int fd);
        public delegate Result GetMemoryFdPropertiesKHRDelegate(VkDevice device, ExternalMemoryHandleTypeFlags handleType, int fd, out MemoryFdProperties pMemoryFdProperties);
        public delegate void GetPhysicalDeviceExternalSemaphorePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, ref PhysicalDeviceExternalSemaphoreInfo externalSemaphoreInfo, out ExternalSemaphoreProperties externalSemaphoreProperties);
        public delegate Result ImportSemaphoreWin32HandleKHRDelegate(VkDevice device, ref ImportSemaphoreWin32HandleInfo importSemaphoreWin32HandleInfo);
        public delegate Result GetSemaphoreWin32HandleKHRDelegate(VkDevice device, ref SemaphoreGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
        public delegate Result ImportSemaphoreFdKHRDelegate(VkDevice device, ref ImportSemaphoreFdInfo importSemaphoreFdInfo);
        public delegate Result GetSemaphoreFdKHRDelegate(VkDevice device, ref SemaphoreGetFdInfo getFdInfo, out int fd);
        public delegate void CmdPushDescriptorSetKHRDelegate(VkCommandBuffer commandBuffer, PipelineBindPoint pipelineBindPoint, VkPipelineLayout layout, uint set, uint descriptorWriteCount, WriteDescriptorSet* descriptorWrites);
        public delegate Result CreateDescriptorUpdateTemplateKHRDelegate(VkDevice device, ref DescriptorUpdateTemplateCreateInfo createInfo, AllocationCallbacks* allocator, out VkDescriptorUpdateTemplate descriptorUpdateTemplate);
        public delegate void DestroyDescriptorUpdateTemplateKHRDelegate(VkDevice device, VkDescriptorUpdateTemplate descriptorUpdateTemplate, AllocationCallbacks* allocator);
        public delegate void UpdateDescriptorSetWithTemplateKHRDelegate(VkDevice device, VkDescriptorSet descriptorSet, VkDescriptorUpdateTemplate descriptorUpdateTemplate, void* data);
        public delegate void CmdPushDescriptorSetWithTemplateKHRDelegate(VkCommandBuffer commandBuffer, VkDescriptorUpdateTemplate descriptorUpdateTemplate, VkPipelineLayout layout, uint set, void* data);
        public delegate Result GetSwapchainStatusKHRDelegate(VkDevice device, VkSwapchain swapchain);
        public delegate void GetPhysicalDeviceExternalFencePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, ref PhysicalDeviceExternalFenceInfo externalFenceInfo, out ExternalFenceProperties externalFenceProperties);
        public delegate Result ImportFenceWin32HandleKHRDelegate(VkDevice device, ref ImportFenceWin32HandleInfo importFenceWin32HandleInfo);
        public delegate Result GetFenceWin32HandleKHRDelegate(VkDevice device, ref FenceGetWin32HandleInfo getWin32HandleInfo, IntPtr pHandle);
        public delegate Result ImportFenceFdKHRDelegate(VkDevice device, ref ImportFenceFdInfo importFenceFdInfo);
        public delegate Result GetFenceFdKHRDelegate(VkDevice device, ref FenceGetFdInfo getFdInfo, out int fd);
        public delegate Result GetPhysicalDeviceSurfaceCapabilities2KHRDelegate(VkPhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out SurfaceCapabilities2 surfaceCapabilities);
        public delegate Result GetPhysicalDeviceSurfaceFormats2KHRDelegate(VkPhysicalDevice physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out uint surfaceFormatCount, SurfaceFormat2* surfaceFormats);
        public delegate void GetImageMemoryRequirements2KHRDelegate(VkDevice device, ref ImageMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
        public delegate void GetBufferMemoryRequirements2KHRDelegate(VkDevice device, ref BufferMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
        public delegate void GetImageSparseMemoryRequirements2KHRDelegate(VkDevice device, ref ImageSparseMemoryRequirementsInfo2 info, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements2* sparseMemoryRequirements);

        // Khronos X
        public delegate void GetDeviceGroupPeerMemoryFeaturesKHXDelegate(VkDevice device, uint heapIndex, uint localDeviceIndex, uint remoteDeviceIndex, out PeerMemoryFeatureFlags peerMemoryFeatures);
        public delegate Result BindBufferMemory2KHXDelegate(VkDevice device, uint bindInfoCount, BindBufferMemoryInfo* bindInfos);
        public delegate Result BindImageMemory2KHXDelegate(VkDevice device, uint bindInfoCount, BindImageMemoryInfo* bindInfos);
        public delegate void CmdSetDeviceMaskKHXDelegate(VkCommandBuffer commandBuffer, uint deviceMask);
        public delegate Result GetDeviceGroupPresentCapabilitiesKHXDelegate(VkDevice device, out DeviceGroupPresentCapabilities deviceGroupPresentCapabilities);
        public delegate Result GetDeviceGroupSurfacePresentModesKHXDelegate(VkDevice device, VkSurface surface, out DeviceGroupPresentModeFlags modes);
        public delegate Result AcquireNextImage2KHXDelegate(VkDevice device, ref AcquireNextImageInfo acquireInfo, out uint imageIndex);
        public delegate void CmdDispatchBaseKHXDelegate(VkCommandBuffer commandBuffer, uint baseGroupX, uint baseGroupY, uint baseGroupZ, uint groupCountX, uint groupCountY, uint groupCountZ);
        public delegate Result GetPhysicalDevicePresentRectanglesKHXDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, ref uint rectCount, Rect2D* rects);
        public delegate Result EnumeratePhysicalDeviceGroupsKHXDelegate(VkInstance instance, ref uint physicalDeviceGroupCount, PhysicalDeviceGroupProperties* physicalDeviceGroupProperties);

        // Multi-vendor
        public delegate Bool32 DebugReportCallbackEXTDelegate(DebugReportFlags flags, DebugReportObjectType objectType, ulong objectHandle, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData);
        public delegate Result CreateDebugReportCallbackEXTDelegate(VkInstance instance, ref DebugReportCallbackCreateInfo createInfo, AllocationCallbacks* allocator, out VkDebugReportCallback callback);
        public delegate void DestroyDebugReportCallbackEXTDelegate(VkInstance instance, VkDebugReportCallback callback, AllocationCallbacks* allocator);
        public delegate void DebugReportMessageEXTDelegate(VkInstance instance, DebugReportFlags flags, DebugReportObjectType objectType, ulong obj, Size location, int messageCode, byte* layerPrefix, byte* message);
        public delegate Result DebugMarkerSetObjectTagEXTDelegate(VkDevice device, ref DebugMarkerObjectTagInfo tagInfo);
        public delegate Result DebugMarkerSetObjectNameEXTDelegate(VkDevice device, ref DebugMarkerObjectNameInfo nameInfo);
        public delegate void CmdDebugMarkerBeginEXTDelegate(VkCommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
        public delegate void CmdDebugMarkerEndEXTDelegate(VkCommandBuffer commandBuffer);
        public delegate void CmdDebugMarkerInsertEXTDelegate(VkCommandBuffer commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
        public delegate Result ReleaseDisplayEXTDelegate(VkPhysicalDevice physicalDevice, VkDisplay display);
        public delegate Result AcquireXlibDisplayEXTDelegate(VkPhysicalDevice physicalDevice, IntPtr dpy, VkDisplay display);
        public delegate Result GetRandROutputDisplayEXTDelegate(VkPhysicalDevice physicalDevice, IntPtr dpy, IntPtr rrOutput, out VkDisplay display);
        public delegate Result GetPhysicalDeviceSurfaceCapabilities2EXTDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out SurfaceCapabilities2EXT surfaceCapabilities);
        public delegate Result DisplayPowerControlEXTDelegate(VkDevice device, VkDisplay display, ref DisplayPowerInfo displayPowerInfo);
        public delegate Result RegisterDeviceEventEXTDelegate(VkDevice device, ref DeviceEventInfo deviceEventInfo, AllocationCallbacks* allocator, out VkFence fence);
        public delegate Result RegisterDisplayEventEXTDelegate(VkDevice device, VkDisplay display, ref DisplayEventInfo displayEventInfo, AllocationCallbacks* allocator, out VkFence fence);
        public delegate Result GetSwapchainCounterEXTDelegate(VkDevice device, VkSwapchain swapchain, SurfaceCounterFlags counter, out ulong counterValue);
        public delegate void CmdSetDiscardRectangleEXTDelegate(VkCommandBuffer commandBuffer, uint firstDiscardRectangle, uint discardRectangleCount, Rect2D* discardRectangles);
        public delegate void SetHdrMetadataEXTDelegate(VkDevice device, uint swapchainCount, VkSwapchain* swapchains, HdrMetadata* metadata);

        // AMD
        public delegate void CmdDrawIndirectCountAMDDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, DeviceSize offset, VkBuffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);
        public delegate void CmdDrawIndexedIndirectCountAMDDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, DeviceSize offset, VkBuffer countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);

        // Nvidia
        public delegate Result GetPhysicalDeviceExternalImageFormatPropertiesNVDelegate(VkPhysicalDevice physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, ExternalMemoryHandleTypeFlagsNV ExternalHandleType, out ExternalImageFormatPropertiesNV externalImageFormatProperties);
        public delegate Result GetMemoryWin32HandleNVDelegate(VkDevice device, VkDeviceMemory memory, ExternalMemoryHandleTypeFlagsNV handleType, IntPtr handle);
        public delegate void CmdSetViewportWScalingNVDelegate(VkCommandBuffer commandBuffer, uint firstViewport, uint viewportCount, ViewportWScaling* viewportWScalings);

        // Nvidia X
        public delegate void CmdProcessCommandsNVXDelegate(VkCommandBuffer commandBuffer, ref CmdProcessCommandsInfo processCommandsInfo);
        public delegate void CmdReserveSpaceForCommandsNVXDelegate(VkCommandBuffer commandBuffer, ref CmdReserveSpaceForCommandsInfo reserveSpaceInfo);
        public delegate Result CreateIndirectCommandsLayoutNVXDelegate(VkDevice device, ref IndirectCommandsLayoutCreateInfo createInfo, AllocationCallbacks* allocator, out VkIndirectCommandsLayout indirectCommandsLayout);
        public delegate void DestroyIndirectCommandsLayoutNVXDelegate(VkDevice device, VkIndirectCommandsLayout indirectCommandsLayout, AllocationCallbacks* allocator);
        public delegate Result CreateObjectTableNVXDelegate(VkDevice device, ref ObjectTableCreateInfo createInfo, AllocationCallbacks* allocator, out VkObjectTable objectTable);
        public delegate void DestroyObjectTableNVXDelegate(VkDevice device, VkObjectTable objectTable, AllocationCallbacks* allocator);
        public delegate Result RegisterObjectsNVXDelegate(VkDevice device, VkObjectTable objectTable, uint objectCount, ref ObjectTableEntry* objectTableEntries, uint* objectIndices);
        public delegate Result UnregisterObjectsNVXDelegate(VkDevice device, VkObjectTable objectTable, uint objectCount, ObjectEntryType* objectEntryTypes, uint* objectIndices);
        public delegate void GetPhysicalDeviceGeneratedCommandsPropertiesNVXDelegate(VkPhysicalDevice physicalDevice, out DeviceGeneratedCommandsFeatures features, out DeviceGeneratedCommandsLimits limits);

        // Nintendo
        public delegate Result CreateViSurfaceNNDelegate(VkInstance instance, ref ViSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);

        // Google
        public delegate Result GetRefreshCycleDurationGOOGLEDelegate(VkDevice device, VkSwapchain swapchain, ref RefreshCycleDuration displayTimingProperties);
        public delegate Result GetPastPresentationTimingGOOGLEDelegate(VkDevice device, VkSwapchain swapchain, ref uint presentationTimingCount, PastPresentationTiming* presentationTimings);

        // MoltenVK
        public delegate Result CreateIOSSurfaceMVKDelegate(VkInstance instance, ref IOSSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
        public delegate Result CreateMacOSSurfaceMVKDelegate(VkInstance instance, ref MacOSSurfaceCreateInfo createInfo, AllocationCallbacks* allocator, out VkSurface surface);
    }
}