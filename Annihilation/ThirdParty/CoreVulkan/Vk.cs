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
            // TODO: Don't VkAllocate string here
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
            // TODO: Don't VkAllocate string here
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
        public delegate void* VkAllocationFunction(void* userData, Size size, Size alignment, VkSystemAllocationScope VkAllocationScope);
        public delegate void* ReallocationFunction(void* userData, void* original, Size size, Size alignment, VkSystemAllocationScope VkAllocationScope);
        public delegate void FreeFunction(void* userData, void* memory);
        public delegate void InternalAllocationNotification(void* userData, Size size, VkInternalAllocationType VkAllocationType, VkSystemAllocationScope VkAllocationScope);
        public delegate void InternalFreeNotification(void* userData, Size size, VkInternalAllocationType VkAllocationType, VkSystemAllocationScope VkAllocationScope);
        public delegate void VoidFunction();

        //
        // Function delegates
        //
        public delegate VkResult CreateInstanceDelegate(ref VkInstanceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkInstance instance);
        public delegate void DestroyInstanceDelegate(VkInstance instance, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult EnumeratePhysicalDevicesDelegate(VkInstance instance, ref uint physicalDeviceCount, VkPhysicalDevice* physicalDevices);
        public delegate void GetPhysicalDeviceFeaturesDelegate(VkPhysicalDevice physicalDevice, out VkPhysicalDeviceFeatures features);
        public delegate void GetPhysicalDeviceFormatPropertiesDelegate(VkPhysicalDevice physicalDevice, VkFormat format, out VkFormatProperties formatProperties);
        public delegate VkResult GetPhysicalDeviceImageFormatPropertiesDelegate(VkPhysicalDevice physicalDevice, VkFormat format, VkImageType type, VkImageTiling tiling, VkImageUsageFlags usage, VkImageCreateFlags flags, out VkImageFormatProperties imageFormatProperties);
        public delegate void GetPhysicalDevicePropertiesDelegate(VkPhysicalDevice physicalDevice, out VkPhysicalDeviceProperties properties);
        public delegate void GetPhysicalDeviceQueueFamilyPropertiesDelegate(VkPhysicalDevice physicalDevice, ref uint queueFamilyPropertyCount, VkQueueFamilyProperties* queueFamilyProperties);
        public delegate void GetPhysicalDeviceMemoryPropertiesDelegate(VkPhysicalDevice physicalDevice, out VkPhysicalDeviceMemoryProperties memoryProperties);
        public delegate IntPtr GetInstanceProcAddrDelegate(VkInstance instance, byte* name);
        public delegate IntPtr GetDeviceProcAddrDelegate(VkDevice device, byte* name);
        public delegate VkResult CreateDeviceDelegate(VkPhysicalDevice physicalDevice, ref VkDeviceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkDevice device);
        public delegate void DestroyDeviceDelegate(VkDevice device, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult EnumerateInstanceExtensionPropertiesDelegate(byte* layerName, ref uint propertyCount, VkExtensionProperties* properties);
        public delegate VkResult EnumerateDeviceExtensionPropertiesDelegate(VkPhysicalDevice physicalDevice, byte* layerName, ref uint propertyCount, VkExtensionProperties* properties);
        public delegate VkResult EnumerateInstanceLayerPropertiesDelegate(ref uint propertyCount, VkLayerProperties* properties);
        public delegate VkResult EnumerateDeviceLayerPropertiesDelegate(VkPhysicalDevice physicalDevice, ref uint propertyCount, VkLayerProperties* properties);
        public delegate void GetDeviceQueueDelegate(VkDevice device, uint queueFamilyIndex, uint queueIndex, out VkQueue queue);
        public delegate VkResult QueueSubmitDelegate(VkQueue queue, uint submitCount, VkSubmitInfo* submits, VkFence fence);
        public delegate VkResult QueueWaitIdleDelegate(VkQueue queue);
        public delegate VkResult DeviceWaitIdleDelegate(VkDevice device);
        public delegate VkResult VkAllocateMemoryDelegate(VkDevice device, ref VkMemoryAllocateInfo VkAllocateInfo, VkAllocationCallbacks* VkAllocator, out VkDeviceMemory memory);
        public delegate void FreeMemoryDelegate(VkDevice device, VkDeviceMemory memory, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult MapMemoryDelegate(VkDevice device, VkDeviceMemory memory, VkDeviceSize offset, VkDeviceSize size, VkMemoryMapFlags flags, void** data);
        public delegate void UnmapMemoryDelegate(VkDevice device, VkDeviceMemory memory);
        public delegate VkResult FlushMappedMemoryRangesDelegate(VkDevice device, uint memoryRangeCount, VkMappedMemoryRange* memoryRanges);
        public delegate VkResult InvalidateMappedMemoryRangesDelegate(VkDevice device, uint memoryRangeCount, VkMappedMemoryRange* memoryRanges);
        public delegate void GetDeviceMemoryCommitmentDelegate(VkDevice device, VkDeviceMemory memory, out VkDeviceSize committedMemoryInBytes);
        public delegate VkResult BindBufferMemoryDelegate(VkDevice device, VkBuffer buffer, VkDeviceMemory memory, VkDeviceSize memoryOffset);
        public delegate VkResult BindImageMemoryDelegate(VkDevice device, VkImage image, VkDeviceMemory memory, VkDeviceSize memoryOffset);
        public delegate void GetBufferMemoryRequirementsDelegate(VkDevice device, VkBuffer buffer, out VkImageMemoryRequirementsInfo2 memoryRequirements);
        public delegate void GetImageMemoryRequirementsDelegate(VkDevice device, VkImage image, out VkMemoryRequirements memoryRequirements);
        public delegate void GetImageSparseMemoryRequirementsDelegate(VkDevice device, VkImage image, out uint sparseMemoryRequirementCount, VkSparseImageMemoryRequirements* sparseMemoryRequirements);
        public delegate void GetPhysicalDeviceSparseImageFormatPropertiesDelegate(VkPhysicalDevice physicalDevice, VkFormat format, VkImageType type, VkSampleCountFlags samples, VkImageUsageFlags usage, VkImageTiling tiling, out uint propertyCount, VkSparseImageFormatProperties* properties);
        public delegate VkResult QueueBindSparseDelegate(VkQueue queue, uint bindInfoCount, ref VkBindSparseInfo bindInfo, VkFence fence);
        public delegate VkResult CreateFenceDelegate(VkDevice device, ref VkFenceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkFence fence);
        public delegate void DestroyFenceDelegate(VkDevice device, VkFence fence, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult ResetFencesDelegate(VkDevice device, uint fenceCount, VkFence* fences);
        public delegate VkResult GetFenceStatusDelegate(VkDevice device, VkFence fence);
        public delegate VkResult WaitForFencesDelegate(VkDevice device, uint fenceCount, VkFence* fences, VkBool32 waitAll, ulong timeout);
        public delegate VkResult CreateSemaphoreDelegate(VkDevice device, ref VkSemaphoreCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSemaphore semaphore);
        public delegate void DestroySemaphoreDelegate(VkDevice device, VkSemaphore semaphore, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateEventDelegate(VkDevice device, ref VkEventCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkEvent evnt);
        public delegate void DestroyEventDelegate(VkDevice device, VkEvent evt, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult GetEventStatusDelegate(VkDevice device, VkEvent evt);
        public delegate VkResult SetEventDelegate(VkDevice device, VkEvent evt);
        public delegate VkResult ResetEventDelegate(VkDevice device, VkEvent evt);
        public delegate VkResult CreateQueryPoolDelegate(VkDevice device, ref VkQueryPoolCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkQueryPool queryPool);
        public delegate void DestroyQueryPoolDelegate(VkDevice device, VkQueryPool queryPool, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult GetQueryPoolVkResultsDelegate(VkDevice device, VkQueryPool queryPool, uint firstQuery, uint queryCount, Size dataSize, void* data, VkDeviceSize stride, VkQueryResultFlags flags);
        public delegate VkResult CreateBufferDelegate(VkDevice device, ref VkBufferCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkBuffer buffer);
        public delegate void DestroyBufferDelegate(VkDevice device, VkBuffer buffer, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateBufferViewDelegate(VkDevice device, ref VkBufferViewCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkBufferView view);
        public delegate void DestroyBufferViewDelegate(VkDevice device, VkBufferView bufferView, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateImageDelegate(VkDevice device, ref VkImageCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkImage image);
        public delegate void DestroyImageDelegate(VkDevice device, VkImage image, VkAllocationCallbacks* VkAllocator);
        public delegate void GetImageSubresourceLayoutDelegate(VkDevice device, VkImage image, ref VkImageSubresource subresource, out VkSubresourceLayout layout);
        public delegate VkResult CreateImageViewDelegate(VkDevice device, ref VkImageViewCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkImageView view);
        public delegate void DestroyImageViewDelegate(VkDevice device, VkImageView imageView, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateShaderModuleDelegate(VkDevice device, ref VkShaderModuleCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkShaderModule shaderModule);
        public delegate void DestroyShaderModuleDelegate(VkDevice device, VkShaderModule shaderModule, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreatePipelineCacheDelegate(VkDevice device, ref VkPipelineCacheCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkPipelineCache pipelineCache);
        public delegate void DestroyPipelineCacheDelegate(VkDevice device, VkPipelineCache pipelineCache, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult GetPipelineCacheDataDelegate(VkDevice device, VkPipelineCache pipelineCache, out Size dataSize, void* data);
        public delegate VkResult MergePipelineCachesDelegate(VkDevice device, VkPipelineCache dstCache, uint srcCacheCount, ref VkPipelineCache* srcCaches);
        public delegate VkResult CreateGraphicsPipelinesDelegate(VkDevice device, VkPipelineCache pipelineCache, uint createInfoCount, VkGraphicsPipelineCreateInfo* createInfos, VkAllocationCallbacks* VkAllocator, VkPipeline* pipelines);
        public delegate VkResult CreateComputePipelinesDelegate(VkDevice device, VkPipelineCache pipelineCache, uint createInfoCount, VkComputePipelineCreateInfo* createInfos, VkAllocationCallbacks* VkAllocator, VkPipeline* pipelines);
        public delegate void DestroyPipelineDelegate(VkDevice device, VkPipeline pipeline, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreatePipelineLayoutDelegate(VkDevice device, ref VkPipelineLayoutCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkPipelineLayout pipelineLayout);
        public delegate void DestroyPipelineLayoutDelegate(VkDevice device, VkPipelineLayout pipelineLayout, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateSamplerDelegate(VkDevice device, ref VkSamplerCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSampler sampler);
        public delegate void DestroySamplerDelegate(VkDevice device, VkSampler sampler, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateDescriptorSetLayoutDelegate(VkDevice device, ref VkDescriptorSetLayoutCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkDescriptorSetLayout setLayout);
        public delegate void DestroyDescriptorSetLayoutDelegate(VkDevice device, VkDescriptorSetLayout descriptorSetLayout, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateDescriptorPoolDelegate(VkDevice device, ref VkDescriptorPoolCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkDescriptorPool descriptorPool);
        public delegate void DestroyDescriptorPoolDelegate(VkDevice device, VkDescriptorPool descriptorPool, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult ResetDescriptorPoolDelegate(VkDevice device, VkDescriptorPool descriptorPool, VkDescriptorPoolResetFlags flags);
        public delegate VkResult VkAllocateDescriptorSetsDelegate(VkDevice device, ref VkDescriptorSetAllocateInfo VkAllocateInfo, VkDescriptorSet* descriptorSets);
        public delegate VkResult FreeDescriptorSetsDelegate(VkDevice device, VkDescriptorPool descriptorPool, uint descriptorSetCount, VkDescriptorSet* descriptorSets);
        public delegate void UpdateDescriptorSetsDelegate(VkDevice device, uint descriptorWriteCount, VkWriteDescriptorSet* descriptorWrites, uint descriptorCopyCount, VkCopyDescriptorSet* descriptorCopies);
        public delegate VkResult CreateFramebufferDelegate(VkDevice device, ref VkFramebufferCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkFramebuffer framebuffer);
        public delegate void DestroyFramebufferDelegate(VkDevice device, VkFramebuffer framebuffer, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateRenderPassDelegate(VkDevice device, ref VkRenderPassCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkRenderPass renderPass);
        public delegate void DestroyRenderPassDelegate(VkDevice device, VkRenderPass renderPass, VkAllocationCallbacks* VkAllocator);
        public delegate void GetRenderAreaGranularityDelegate(VkDevice device, VkRenderPass renderPass, out VkExtent2D granularity);
        public delegate VkResult CreateCommandPoolDelegate(VkDevice device, ref VkCommandPoolCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkCommandPool commandPool);
        public delegate void DestroyCommandPoolDelegate(VkDevice device, VkCommandPool commandPool, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult ResetCommandPoolDelegate(VkDevice device, VkCommandPool commandPool, VkCommandPoolResetFlags flags);
        public delegate VkResult AllocateCommandBuffersDelegate(VkDevice device, ref VkCommandBufferAllocateInfo VkAllocateInfo, VkCommandBuffer* commandBuffers);
        public delegate void FreeCommandBuffersDelegate(VkDevice device, VkCommandPool commandPool, uint commandBufferCount, VkCommandBuffer* commandBuffers);
        public delegate VkResult BeginCommandBufferDelegate(VkCommandBuffer commandBuffer, ref VkCommandBufferBeginInfo beginInfo);
        public delegate VkResult EndCommandBufferDelegate(VkCommandBuffer commandBuffer);
        public delegate VkResult ResetCommandBufferDelegate(VkCommandBuffer commandBuffer, VkCommandBufferResetFlags flags);
        public delegate void CmdBindPipelineDelegate(VkCommandBuffer commandBuffer, VkPipelineBindPoint pipelineBindPoint, VkPipeline pipeline);
        public delegate void CmdSetViewportDelegate(VkCommandBuffer commandBuffer, uint firstViewport, uint viewportCount, VkViewport* viewports);
        public delegate void CmdSetScissorDelegate(VkCommandBuffer commandBuffer, uint firstScissor, uint scissorCount, VkRect2D* scissors);
        public delegate void CmdSetLineWidthDelegate(VkCommandBuffer commandBuffer, float lineWidth);
        public delegate void CmdSetDepthBiasDelegate(VkCommandBuffer commandBuffer, float depthBiasrefantFactor, float depthBiasClamp, float depthBiasSlopeFactor);
        public delegate void CmdSetBlendConstantsDelegate(VkCommandBuffer commandBuffer, float* blendConstants);
        public delegate void CmdSetDepthBoundsDelegate(VkCommandBuffer commandBuffer, float minDepthBounds, float maxDepthBounds);
        public delegate void CmdSetStencilCompareMaskDelegate(VkCommandBuffer commandBuffer, VkStencilFaceFlags faceMask, uint compareMask);
        public delegate void CmdSetStencilWriteMaskDelegate(VkCommandBuffer commandBuffer, VkStencilFaceFlags faceMask, uint writeMask);
        public delegate void CmdSetStencilReferenceDelegate(VkCommandBuffer commandBuffer, VkStencilFaceFlags faceMask, uint reference);
        public delegate void CmdBindDescriptorSetsDelegate(VkCommandBuffer commandBuffer, VkPipelineBindPoint pipelineBindPoint, VkPipelineLayout layout, uint firstSet, uint descriptorSetCount, VkDescriptorSet* descriptorSets, uint dynamicOffsetCount, uint* dynamicOffsets);
        public delegate void CmdBindIndexBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, VkDeviceSize offset, VkIndexType indexType);
        public delegate void CmdBindVertexBuffersDelegate(VkCommandBuffer commandBuffer, uint firstBinding, uint bindingCount, VkBuffer* buffers, VkDeviceSize* offsets);
        public delegate void CmdDrawDelegate(VkCommandBuffer commandBuffer, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);
        public delegate void CmdDrawIndexedDelegate(VkCommandBuffer commandBuffer, uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset, uint firstInstance);
        public delegate void CmdDrawIndirectDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, VkDeviceSize offset, uint drawCount, uint stride);
        public delegate void CmdDrawIndexedIndirectDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, VkDeviceSize offset, uint drawCount, uint stride);
        public delegate void CmdDispatchDelegate(VkCommandBuffer commandBuffer, uint groupCountX, uint groupCountY, uint groupCountZ);
        public delegate void CmdDispatchIndirectDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, VkDeviceSize offset);
        public delegate void CmdCopyBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer srcBuffer, VkBuffer dstBuffer, uint regionCount, VkBufferCopy* regions);
        public delegate void CmdCopyImageDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, VkImageLayout srcImageLayout, VkImage dstImage, VkImageLayout dstImageLayout, uint regionCount, VkImageCopy* regions);
        public delegate void CmdBlitImageDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, VkImageLayout srcImageLayout, VkImage dstImage, VkImageLayout dstImageLayout, uint regionCount, VkImageBlit* regions, VkFilter filter);
        public delegate void CmdCopyBufferToImageDelegate(VkCommandBuffer commandBuffer, VkBuffer srcBuffer, VkImage dstImage, VkImageLayout dstImageLayout, uint regionCount, VkBufferImageCopy* regions);
        public delegate void CmdCopyImageToBufferDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, VkImageLayout srcImageLayout, VkBuffer dstBuffer, uint regionCount, VkBufferImageCopy* regions);
        public delegate void CmdUpdateBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer dstBuffer, VkDeviceSize dstOffset, VkDeviceSize dataSize, void* data);
        public delegate void CmdFillBufferDelegate(VkCommandBuffer commandBuffer, VkBuffer dstBuffer, VkDeviceSize dstOffset, VkDeviceSize size, uint data);
        public delegate void CmdClearColorImageDelegate(VkCommandBuffer commandBuffer, VkImage image, VkImageLayout imageLayout, ref VkClearColorValue color, uint rangeCount, VkImageSubresourceRange* ranges);
        public delegate void CmdClearDepthStencilImageDelegate(VkCommandBuffer commandBuffer, VkImage image, VkImageLayout imageLayout, ref VkClearDepthStencilValue depthStencil, uint rangeCount, VkImageSubresourceRange* ranges);
        public delegate void CmdClearAttachmentsDelegate(VkCommandBuffer commandBuffer, uint attachmentCount, VkClearAttachment* attachments, uint rectCount, VkClearRect* rects);
        public delegate void CmdResolveImageDelegate(VkCommandBuffer commandBuffer, VkImage srcImage, VkImageLayout srcImageLayout, VkImage dstImage, VkImageLayout dstImageLayout, uint regionCount, VkImageResolve* regions);
        public delegate void CmdSetEventDelegate(VkCommandBuffer commandBuffer, VkEvent evt, VkPipelineStageFlags stageMask);
        public delegate void CmdResetEventDelegate(VkCommandBuffer commandBuffer, VkEvent evt, VkPipelineStageFlags stageMask);
        public delegate void CmdWaitEventsDelegate(VkCommandBuffer commandBuffer, uint eventCount, VkEvent* events, VkPipelineStageFlags srcStageMask, VkPipelineStageFlags dstStageMask, uint memoryBarrierCount, VkMemoryBarrier* memoryBarriers, uint bufferMemoryBarrierCount, VkBufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount, VkImageMemoryBarrier* imageMemoryBarriers);
        public delegate void CmdPipelineBarrierDelegate(VkCommandBuffer commandBuffer, VkPipelineStageFlags srcStageMask, VkPipelineStageFlags dstStageMask, VkDependencyFlags dependencyFlags, uint memoryBarrierCount, VkMemoryBarrier* memoryBarriers, uint bufferMemoryBarrierCount, VkBufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount, VkImageMemoryBarrier* imageMemoryBarriers);
        public delegate void CmdBeginQueryDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint query, VkQueryControlFlags flags);
        public delegate void CmdEndQueryDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint query);
        public delegate void CmdResetQueryPoolDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint firstQuery, uint queryCount);
        public delegate void CmdWriteTimestampDelegate(VkCommandBuffer commandBuffer, VkPipelineStageFlags pipelineStage, VkQueryPool queryPool, uint query);
        public delegate void CmdCopyQueryPoolVkResultsDelegate(VkCommandBuffer commandBuffer, VkQueryPool queryPool, uint firstQuery, uint queryCount, VkBuffer dstBuffer, VkDeviceSize dstOffset, VkDeviceSize stride, VkQueryResultFlags flags);
        public delegate void CmdPushConstantsDelegate(VkCommandBuffer commandBuffer, VkPipelineLayout layout, VkShaderStageFlags stageFlags, uint offset, uint size, void* values);
        public delegate void CmdBeginRenderPassDelegate(VkCommandBuffer commandBuffer, ref VkRenderPassBeginInfo renderPassBegin, VkSubpassContents contents);
        public delegate void CmdNextSubpassDelegate(VkCommandBuffer commandBuffer, VkSubpassContents contents);
        public delegate void CmdEndRenderPassDelegate(VkCommandBuffer commandBuffer);
        public delegate void CmdExecuteCommandsDelegate(VkCommandBuffer commandBuffer, uint commandBufferCount, VkCommandBuffer* commandBuffers);

        // Khronos
        public delegate void DestroySurfaceKHRDelegate(VkInstance instance, VkSurface surface, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult GetPhysicalDeviceSurfaceSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, VkSurface surface, out VkBool32 supported);
        public delegate VkResult GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out VkSurfaceCapabilities surfaceCapabilities);
        public delegate VkResult GetPhysicalDeviceSurfaceFormatsKHRDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out uint surfaceFormatCount, VkSurfaceFormat* surfaceFormats);
        public delegate VkResult GetPhysicalDeviceSurfacePresentModesKHRDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out uint presentModeCount, VkPresentMode* presentModes);
        public delegate VkResult CreateSwapchainKHRDelegate(VkDevice device, ref VkSwapchainCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSwapchain swapchain);
        public delegate void DestroySwapchainKHRDelegate(VkDevice device, VkSwapchain swapchain, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult GetSwapchainImagesKHRDelegate(VkDevice device, VkSwapchain swapchain, out uint swapchainImageCount, VkImage* swapchainImages);
        public delegate VkResult AcquireNextImageKHRDelegate(VkDevice device, VkSwapchain swapchain, ulong timeout, VkSemaphore semaphore, VkFence fence, out uint imageIndex);
        public delegate VkResult QueuePresentKHRDelegate(VkQueue queue, ref VkPresentInfo presentInfo);
        public delegate VkResult GetPhysicalDeviceDisplayPropertiesKHRDelegate(VkPhysicalDevice physicalDevice, out uint propertyCount, VkDisplayProperties* properties);
        public delegate VkResult GetPhysicalDeviceDisplayPlanePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, out uint propertyCount, VkDisplayPlaneProperties* properties);
        public delegate VkResult GetDisplayPlaneSupportedDisplaysKHRDelegate(VkPhysicalDevice physicalDevice, uint planeIndex, out uint displayCount, VkDisplay* displays);
        public delegate VkResult GetDisplayModePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, VkDisplay display, out uint propertyCount, VkDisplayModeProperties* properties);
        public delegate VkResult CreateDisplayModeKHRDelegate(VkPhysicalDevice physicalDevice, VkDisplay display, ref VkDisplayModeCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkDisplayMode mode);
        public delegate VkResult GetDisplayPlaneCapabilitiesKHRDelegate(VkPhysicalDevice physicalDevice, VkDisplayMode mode, uint planeIndex, out VkDisplayPlaneCapabilities capabilities);
        public delegate VkResult CreateDisplayPlaneSurfaceKHRDelegate(VkInstance instance, ref VkDisplaySurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkResult CreateSharedSwapchainsKHRDelegate(VkDevice device, uint swapchainCount, VkSwapchainCreateInfo* createInfos, VkAllocationCallbacks* VkAllocator, VkSwapchain* swapchains);
        public delegate VkResult CreateXlibSurfaceKHRDelegate(VkInstance instance, ref VkXlibSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkBool32 GetPhysicalDeviceXlibPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr dpy, IntPtr visualID);
        public delegate VkResult CreateXcbSurfaceKHRDelegate(VkInstance instance, ref VkXcbSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkBool32 GetPhysicalDeviceXcbPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection, IntPtr visualId);
        public delegate VkResult CreateWaylandSurfaceKHRDelegate(VkInstance instance, ref VkWaylandSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkBool32 GetPhysicalDeviceWaylandPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr display);
        public delegate VkResult CreateMirSurfaceKHRDelegate(VkInstance instance, ref VkMirSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkBool32 GetPhysicalDeviceMirPresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex, IntPtr connection);
        public delegate VkResult CreateAndroidSurfaceKHRDelegate(VkInstance instance, ref VkAndroidSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkResult CreateWin32SurfaceKHRDelegate(VkInstance instance, ref VkWin32SurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkBool32 GetPhysicalDeviceWin32PresentationSupportKHRDelegate(VkPhysicalDevice physicalDevice, uint queueFamilyIndex);
        public delegate void GetPhysicalDeviceFeatures2KHRDelegate(VkPhysicalDevice physicalDevice, out VkPhysicalDeviceFeatures2 features);
        public delegate void GetPhysicalDeviceProperties2KHRDelegate(VkPhysicalDevice physicalDevice, out VkPhysicalDeviceProperties2 properties);
        public delegate void GetPhysicalDeviceFormatProperties2KHRDelegate(VkPhysicalDevice physicalDevice, VkFormat format, out VkFormatProperties2 formatProperties);
        public delegate VkResult GetPhysicalDeviceImageFormatProperties2KHRDelegate(VkPhysicalDevice physicalDevice, ref VkPhysicalDeviceImageFormatInfo2 imageFormatInfo, out VkImageFormatProperties2 imageFormatProperties);
        public delegate void GetPhysicalDeviceQueueFamilyProperties2KHRDelegate(VkPhysicalDevice physicalDevice, out uint queueFamilyPropertyCount, VkQueueFamilyProperties2* queueFamilyProperties);
        public delegate void GetPhysicalDeviceMemoryProperties2KHRDelegate(VkPhysicalDevice physicalDevice, out VkPhysicalDeviceMemoryProperties2 memoryProperties);
        public delegate void GetPhysicalDeviceSparseImageFormatProperties2KHRDelegate(VkPhysicalDevice physicalDevice, ref VkPhysicalDeviceSparseImageFormatInfo2 formatInfo, out uint propertyCount, VkSparseImageFormatProperties2* properties);
        public delegate void TrimCommandPoolKHRDelegate(VkDevice device, VkCommandPool commandPool, VkCommandPoolTrimFlags flags);
        public delegate void GetPhysicalDeviceExternalBufferPropertiesKHRDelegate(VkPhysicalDevice physicalDevice, ref VkPhysicalDeviceExternalBufferInfo externalBufferInfo, out VkExternalBufferProperties externalBufferProperties);
        public delegate VkResult GetMemoryWin32HandleKHRDelegate(VkDevice device, ref VkMemoryGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
        public delegate VkResult GetMemoryWin32HandlePropertiesKHRDelegate(VkDevice device, VkExternalMemoryHandleTypeFlags handleType, IntPtr handle, out VkMemoryWin32HandleProperties memoryWin32HandleProperties);
        public delegate VkResult GetMemoryFdKHRDelegate(VkDevice device, ref VkMemoryGetFdInfo getFdInfo, out int fd);
        public delegate VkResult GetMemoryFdPropertiesKHRDelegate(VkDevice device, VkExternalMemoryHandleTypeFlags handleType, int fd, out VkMemoryFdProperties pMemoryFdProperties);
        public delegate void GetPhysicalDeviceExternalSemaphorePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, ref VkPhysicalDeviceExternalSemaphoreInfo externalSemaphoreInfo, out VkExternalSemaphoreProperties externalSemaphoreProperties);
        public delegate VkResult ImportSemaphoreWin32HandleKHRDelegate(VkDevice device, ref VkImportSemaphoreWin32HandleInfo importSemaphoreWin32HandleInfo);
        public delegate VkResult GetSemaphoreWin32HandleKHRDelegate(VkDevice device, ref VkSemaphoreGetWin32HandleInfo getWin32HandleInfo, IntPtr handle);
        public delegate VkResult ImportSemaphoreFdKHRDelegate(VkDevice device, ref VkImportSemaphoreFdInfo importSemaphoreFdInfo);
        public delegate VkResult GetSemaphoreFdKHRDelegate(VkDevice device, ref VkSemaphoreGetFdInfo getFdInfo, out int fd);
        public delegate void CmdPushDescriptorSetKHRDelegate(VkCommandBuffer commandBuffer, VkPipelineBindPoint pipelineBindPoint, VkPipelineLayout layout, uint set, uint descriptorWriteCount, VkWriteDescriptorSet* descriptorWrites);
        public delegate VkResult CreateDescriptorUpdateTemplateKHRDelegate(VkDevice device, ref VkDescriptorUpdateTemplateCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkDescriptorUpdateTemplate descriptorUpdateTemplate);
        public delegate void DestroyDescriptorUpdateTemplateKHRDelegate(VkDevice device, VkDescriptorUpdateTemplate descriptorUpdateTemplate, VkAllocationCallbacks* VkAllocator);
        public delegate void UpdateDescriptorSetWithTemplateKHRDelegate(VkDevice device, VkDescriptorSet descriptorSet, VkDescriptorUpdateTemplate descriptorUpdateTemplate, void* data);
        public delegate void CmdPushDescriptorSetWithTemplateKHRDelegate(VkCommandBuffer commandBuffer, VkDescriptorUpdateTemplate descriptorUpdateTemplate, VkPipelineLayout layout, uint set, void* data);
        public delegate VkResult GetSwapchainStatusKHRDelegate(VkDevice device, VkSwapchain swapchain);
        public delegate void GetPhysicalDeviceExternalFencePropertiesKHRDelegate(VkPhysicalDevice physicalDevice, ref VkPhysicalDeviceExternalFenceInfo externalFenceInfo, out VkExternalFenceProperties externalFenceProperties);
        public delegate VkResult ImportFenceWin32HandleKHRDelegate(VkDevice device, ref VkImportFenceWin32HandleInfo importFenceWin32HandleInfo);
        public delegate VkResult GetFenceWin32HandleKHRDelegate(VkDevice device, ref VkFenceGetWin32HandleInfo getWin32HandleInfo, IntPtr pHandle);
        public delegate VkResult ImportFenceFdKHRDelegate(VkDevice device, ref VkImportFenceFdInfo importFenceFdInfo);
        public delegate VkResult GetFenceFdKHRDelegate(VkDevice device, ref VkFenceGetFdInfo getFdInfo, out int fd);
        public delegate VkResult GetPhysicalDeviceSurfaceCapabilities2KHRDelegate(VkPhysicalDevice physicalDevice, ref VkPhysicalDeviceSurfaceInfo2 surfaceInfo, out VkSurfaceCapabilities2 surfaceCapabilities);
        public delegate VkResult GetPhysicalDeviceSurfaceFormats2KHRDelegate(VkPhysicalDevice physicalDevice, ref VkPhysicalDeviceSurfaceInfo2 surfaceInfo, out uint surfaceFormatCount, VkSurfaceFormat2* surfaceFormats);
        public delegate void GetImageMemoryRequirements2KHRDelegate(VkDevice device, ref VkImageMemoryRequirementsInfo2 info, out VkMemoryRequirements2 memoryRequirements);
        public delegate void GetBufferMemoryRequirements2KHRDelegate(VkDevice device, ref VkBufferMemoryRequirementsInfo2 info, out VkMemoryRequirements2 memoryRequirements);
        public delegate void GetImageSparseMemoryRequirements2KHRDelegate(VkDevice device, ref VkImageSparseMemoryRequirementsInfo2 info, out uint sparseMemoryRequirementCount, VkSparseImageMemoryRequirements2* sparseMemoryRequirements);

        // Khronos X
        public delegate void GetDeviceGroupPeerMemoryFeaturesKHXDelegate(VkDevice device, uint heapIndex, uint localDeviceIndex, uint remoteDeviceIndex, out VkPeerMemoryFeatureFlags peerMemoryFeatures);
        public delegate VkResult BindBufferMemory2KHXDelegate(VkDevice device, uint bindInfoCount, VkBindBufferMemoryInfo* bindInfos);
        public delegate VkResult BindImageMemory2KHXDelegate(VkDevice device, uint bindInfoCount, VkBindImageMemoryInfo* bindInfos);
        public delegate void CmdSetDeviceMaskKHXDelegate(VkCommandBuffer commandBuffer, uint deviceMask);
        public delegate VkResult GetDeviceGroupPresentCapabilitiesKHXDelegate(VkDevice device, out VkDeviceGroupPresentCapabilities deviceGroupPresentCapabilities);
        public delegate VkResult GetDeviceGroupSurfacePresentModesKHXDelegate(VkDevice device, VkSurface surface, out VkDeviceGroupPresentModeFlags modes);
        public delegate VkResult AcquireNextImage2KHXDelegate(VkDevice device, ref VkAcquireNextImageInfo acquireInfo, out uint imageIndex);
        public delegate void CmdDispatchBaseKHXDelegate(VkCommandBuffer commandBuffer, uint baseGroupX, uint baseGroupY, uint baseGroupZ, uint groupCountX, uint groupCountY, uint groupCountZ);
        public delegate VkResult GetPhysicalDevicePresentRectanglesKHXDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, ref uint rectCount, VkRect2D* rects);
        public delegate VkResult EnumeratePhysicalDeviceGroupsKHXDelegate(VkInstance instance, ref uint physicalDeviceGroupCount, VkPhysicalDeviceGroupProperties* physicalDeviceGroupProperties);

        // Multi-vendor
        public delegate VkBool32 DebugReportCallbackEXTDelegate(VkDebugReportFlags flags, VkDebugReportObjectType objectType, ulong objectHandle, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData);
        public delegate VkResult CreateDebugReportCallbackEXTDelegate(VkInstance instance, ref VkDebugReportCallbackCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkDebugReportCallback callback);
        public delegate void DestroyDebugReportCallbackEXTDelegate(VkInstance instance, VkDebugReportCallback callback, VkAllocationCallbacks* VkAllocator);
        public delegate void DebugReportMessageEXTDelegate(VkInstance instance, VkDebugReportFlags flags, VkDebugReportObjectType objectType, ulong obj, Size location, int messageCode, byte* layerPrefix, byte* message);
        public delegate VkResult DebugMarkerSetObjectTagEXTDelegate(VkDevice device, ref VkDebugMarkerObjectTagInfo tagInfo);
        public delegate VkResult DebugMarkerSetObjectNameEXTDelegate(VkDevice device, ref VkDebugMarkerObjectNameInfo nameInfo);
        public delegate void CmdDebugMarkerBeginEXTDelegate(VkCommandBuffer commandBuffer, ref VkDebugMarkerMarkerInfo markerInfo);
        public delegate void CmdDebugMarkerEndEXTDelegate(VkCommandBuffer commandBuffer);
        public delegate void CmdDebugMarkerInsertEXTDelegate(VkCommandBuffer commandBuffer, ref VkDebugMarkerMarkerInfo markerInfo);
        public delegate VkResult ReleaseDisplayEXTDelegate(VkPhysicalDevice physicalDevice, VkDisplay display);
        public delegate VkResult AcquireXlibDisplayEXTDelegate(VkPhysicalDevice physicalDevice, IntPtr dpy, VkDisplay display);
        public delegate VkResult GetRandROutputDisplayEXTDelegate(VkPhysicalDevice physicalDevice, IntPtr dpy, IntPtr rrOutput, out VkDisplay display);
        public delegate VkResult GetPhysicalDeviceSurfaceCapabilities2EXTDelegate(VkPhysicalDevice physicalDevice, VkSurface surface, out VkSurfaceCapabilities2EXT surfaceCapabilities);
        public delegate VkResult DisplayPowerControlEXTDelegate(VkDevice device, VkDisplay display, ref VkDisplayPowerInfo displayPowerInfo);
        public delegate VkResult RegisterDeviceEventEXTDelegate(VkDevice device, ref VkDeviceEventInfo deviceEventInfo, VkAllocationCallbacks* VkAllocator, out VkFence fence);
        public delegate VkResult RegisterDisplayEventEXTDelegate(VkDevice device, VkDisplay display, ref VkDisplayEventInfo displayEventInfo, VkAllocationCallbacks* VkAllocator, out VkFence fence);
        public delegate VkResult GetSwapchainCounterEXTDelegate(VkDevice device, VkSwapchain swapchain, VkSurfaceCounterFlags counter, out ulong counterValue);
        public delegate void CmdSetDiscardRectangleEXTDelegate(VkCommandBuffer commandBuffer, uint firstDiscardRectangle, uint discardRectangleCount, VkRect2D* discardRectangles);
        public delegate void SetHdrMetadataEXTDelegate(VkDevice device, uint swapchainCount, VkSwapchain* swapchains, VkHdrMetadata* metadata);

        // AMD
        public delegate void CmdDrawIndirectCountAMDDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, VkDeviceSize offset, VkBuffer countBuffer, VkDeviceSize countBufferOffset, uint maxDrawCount, uint stride);
        public delegate void CmdDrawIndexedIndirectCountAMDDelegate(VkCommandBuffer commandBuffer, VkBuffer buffer, VkDeviceSize offset, VkBuffer countBuffer, VkDeviceSize countBufferOffset, uint maxDrawCount, uint stride);

        // Nvidia
        public delegate VkResult GetPhysicalDeviceExternalImageFormatPropertiesNVDelegate(VkPhysicalDevice physicalDevice, VkFormat format, VkImageType type, VkImageTiling tiling, VkImageUsageFlags usage, VkImageCreateFlags flags, VkExternalMemoryHandleTypeFlagsNV ExternalHandleType, out VkExternalImageFormatPropertiesNV externalImageFormatProperties);
        public delegate VkResult GetMemoryWin32HandleNVDelegate(VkDevice device, VkDeviceMemory memory, VkExternalMemoryHandleTypeFlagsNV handleType, IntPtr handle);
        public delegate void CmdSetViewportWScalingNVDelegate(VkCommandBuffer commandBuffer, uint firstViewport, uint viewportCount, VkViewportWScaling* viewportWScalings);

        // Nvidia X
        public delegate void CmdProcessCommandsNVXDelegate(VkCommandBuffer commandBuffer, ref VkCmdProcessCommandsInfo processCommandsInfo);
        public delegate void CmdReserveSpaceForCommandsNVXDelegate(VkCommandBuffer commandBuffer, ref VkCmdReserveSpaceForCommandsInfo reserveSpaceInfo);
        public delegate VkResult CreateIndirectCommandsLayoutNVXDelegate(VkDevice device, ref VkIndirectCommandsLayoutCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkIndirectCommandsLayout indirectCommandsLayout);
        public delegate void DestroyIndirectCommandsLayoutNVXDelegate(VkDevice device, VkIndirectCommandsLayout indirectCommandsLayout, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult CreateObjectTableNVXDelegate(VkDevice device, ref VkObjectTableCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkObjectTable objectTable);
        public delegate void DestroyObjectTableNVXDelegate(VkDevice device, VkObjectTable objectTable, VkAllocationCallbacks* VkAllocator);
        public delegate VkResult RegisterObjectsNVXDelegate(VkDevice device, VkObjectTable objectTable, uint objectCount, ref VkObjectTableEntry* objectTableEntries, uint* objectIndices);
        public delegate VkResult UnregisterObjectsNVXDelegate(VkDevice device, VkObjectTable objectTable, uint objectCount, VkObjectEntryType* objectEntryTypes, uint* objectIndices);
        public delegate void GetPhysicalDeviceGeneratedCommandsPropertiesNVXDelegate(VkPhysicalDevice physicalDevice, out VkDeviceGeneratedCommandsFeatures features, out VkDeviceGeneratedCommandsLimits limits);

        // Nintendo
        public delegate VkResult CreateViSurfaceNNDelegate(VkInstance instance, ref VkViSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);

        // Google
        public delegate VkResult GetRefreshCycleDurationGOOGLEDelegate(VkDevice device, VkSwapchain swapchain, ref VkRefreshCycleDuration displayTimingProperties);
        public delegate VkResult GetPastPresentationTimingGOOGLEDelegate(VkDevice device, VkSwapchain swapchain, ref uint presentationTimingCount, VkPastPresentationTiming* presentationTimings);

        // MoltenVK
        public delegate VkResult CreateIOSSurfaceMVKDelegate(VkInstance instance, ref VkIOSSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
        public delegate VkResult CreateMacOSSurfaceMVKDelegate(VkInstance instance, ref VkMacOSSurfaceCreateInfo createInfo, VkAllocationCallbacks* VkAllocator, out VkSurface surface);
    }
}