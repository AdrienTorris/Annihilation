using System;
using System.Runtime.InteropServices;
using SDL2;

namespace Vulkan
{
    public unsafe struct FunctionName
    {
        private const int MaxFunctionNameLength = 64;

        public fixed byte Name[MaxFunctionNameLength];

        private static byte* FromString(string str)
        {
            FunctionName name = new FunctionName();
            for (int i = 0; i < str.Length; ++i)
            {
                name.Name[i] = (byte)str[i];
            }
            name.Name[str.Length] = 0;
            return name.Name;
        }

        public static byte* CreateInstance => FromString("vkCreateInstance");
        public static byte* DestroyInstance => FromString("vkDestroyInstance");
        public static byte* EnumeratePhysicalDevices => FromString("vkEnumeratePhysicalDevices");
        public static byte* GetPhysicalDeviceFeatures => FromString("vkGetPhysicalDeviceFeatures");
        public static byte* GetPhysicalDeviceFormatProperties => FromString("vkGetPhysicalDeviceFormatProperties");
        public static byte* GetPhysicalDeviceImageFormatProperties => FromString("vkGetPhysicalDeviceImageFormatProperties");
        public static byte* GetPhysicalDeviceProperties => FromString("vkGetPhysicalDeviceProperties");
        public static byte* GetPhysicalDeviceQueueFamilyProperties => FromString("vkGetPhysicalDeviceQueueFamilyProperties");
        public static byte* GetPhysicalDeviceMemoryProperties => FromString("vkGetPhysicalDeviceMemoryProperties");
        public static byte* GetInstanceProcAddr => FromString("vkGetInstanceProcAddr");
        public static byte* GetDeviceProcAddr => FromString("vkGetDeviceProcAddr");
        public static byte* CreateDevice => FromString("vkCreateDevice");
        public static byte* DestroyDevice => FromString("vkDestroyDevice");
        public static byte* EnumerateInstanceExtensionProperties => FromString("vkEnumerateInstanceExtensionProperties");
        public static byte* EnumerateDeviceExtensionProperties => FromString("vkEnumerateDeviceExtensionProperties");
        public static byte* EnumerateInstanceLayerProperties => FromString("vkEnumerateInstanceLayerProperties");
        public static byte* EnumerateDeviceLayerProperties => FromString("vkEnumerateDeviceLayerProperties");
        public static byte* GetDeviceQueue => FromString("vkGetDeviceQueue");
        public static byte* QueueSubmit => FromString("vkQueueSubmit");
        public static byte* QueueWaitIdle => FromString("vkQueueWaitIdle");
        public static byte* DeviceWaitIdle => FromString("vkDeviceWaitIdle");
        public static byte* AllocateMemory => FromString("vkAllocateMemory");
        public static byte* FreeMemory => FromString("vkFreeMemory");
        public static byte* MapMemory => FromString("vkMapMemory");
        public static byte* UnmapMemory => FromString("vkUnmapMemory");
        public static byte* FlushMappedMemoryRanges => FromString("vkFlushMappedMemoryRanges");
        public static byte* InvalidateMappedMemoryRanges => FromString("vkInvalidateMappedMemoryRanges");
        public static byte* GetDeviceMemoryCommitment => FromString("vkGetDeviceMemoryCommitment");
        public static byte* BindBufferMemory => FromString("vkBindBufferMemory");
        public static byte* BindImageMemory => FromString("vkBindImageMemory");
        public static byte* GetBufferMemoryRequirements => FromString("vkGetBufferMemoryRequirements");
        public static byte* GetImageMemoryRequirements => FromString("vkGetImageMemoryRequirements");
        public static byte* GetImageSparseMemoryRequirements => FromString("vkGetImageSparseMemoryRequirements");
        public static byte* GetPhysicalDeviceSparseImageFormatProperties => FromString("vkGetPhysicalDeviceSparseImageFormatProperties");
        public static byte* QueueBindSparse => FromString("vkQueueBindSparse");
        public static byte* CreateFence => FromString("vkCreateFence");
        public static byte* DestroyFence => FromString("vkDestroyFence");
        public static byte* ResetFences => FromString("vkResetFences");
        public static byte* GetFenceStatus => FromString("vkGetFenceStatus");
        public static byte* WaitForFences => FromString("vkWaitForFences");
        public static byte* CreateSemaphore => FromString("vkCreateSemaphore");
        public static byte* DestroySemaphore => FromString("vkDestroySemaphore");
        public static byte* CreateEvent => FromString("vkCreateEvent");
        public static byte* DestroyEvent => FromString("vkDestroyEvent");
        public static byte* GetEventStatus => FromString("vkGetEventStatus");
        public static byte* SetEvent => FromString("vkSetEvent");
        public static byte* ResetEvent => FromString("vkResetEvent");
        public static byte* CreateQueryPool => FromString("vkCreateQueryPool");
        public static byte* DestroyQueryPool => FromString("vkDestroyQueryPool");
        public static byte* GetQueryPoolResults => FromString("vkGetQueryPoolResults");
        public static byte* CreateBuffer => FromString("vkCreateBuffer");
        public static byte* DestroyBuffer => FromString("vkDestroyBuffer");
        public static byte* CreateBufferView => FromString("vkCreateBufferView");
        public static byte* DestroyBufferView => FromString("vkDestroyBufferView");
        public static byte* CreateImage => FromString("vkCreateImage");
        public static byte* DestroyImage => FromString("vkDestroyImage");
        public static byte* GetImageSubresourceLayout => FromString("vkGetImageSubresourceLayout");
        public static byte* CreateImageView => FromString("vkCreateImageView");
        public static byte* DestroyImageView => FromString("vkDestroyImageView");
        public static byte* CreateShaderModule => FromString("vkCreateShaderModule");
        public static byte* DestroyShaderModule => FromString("vkDestroyShaderModule");
        public static byte* CreatePipelineCache => FromString("vkCreatePipelineCache");
        public static byte* DestroyPipelineCache => FromString("vkDestroyPipelineCache");
        public static byte* GetPipelineCacheData => FromString("vkGetPipelineCacheData");
        public static byte* MergePipelineCaches => FromString("vkMergePipelineCaches");
        public static byte* CreateGraphicsPipelines => FromString("vkCreateGraphicsPipelines");
        public static byte* CreateComputePipelines => FromString("vkCreateComputePipelines");
        public static byte* DestroyPipeline => FromString("vkDestroyPipeline");
        public static byte* CreatePipelineLayout => FromString("vkCreatePipelineLayout");
        public static byte* DestroyPipelineLayout => FromString("vkDestroyPipelineLayout");
        public static byte* CreateSampler => FromString("vkCreateSampler");
        public static byte* DestroySampler => FromString("vkDestroySampler");
        public static byte* CreateDescriptorSetLayout => FromString("vkCreateDescriptorSetLayout");
        public static byte* DestroyDescriptorSetLayout => FromString("vkDestroyDescriptorSetLayout");
        public static byte* CreateDescriptorPool => FromString("vkCreateDescriptorPool");
        public static byte* DestroyDescriptorPool => FromString("vkDestroyDescriptorPool");
        public static byte* ResetDescriptorPool => FromString("vkResetDescriptorPool");
        public static byte* AllocateDescriptorSets => FromString("vkAllocateDescriptorSets");
        public static byte* FreeDescriptorSets => FromString("vkFreeDescriptorSets");
        public static byte* UpdateDescriptorSets => FromString("vkUpdateDescriptorSets");
        public static byte* CreateFramebuffer => FromString("vkCreateFramebuffer");
        public static byte* DestroyFramebuffer => FromString("vkDestroyFramebuffer");
        public static byte* CreateRenderPass => FromString("vkCreateRenderPass");
        public static byte* DestroyRenderPass => FromString("vkDestroyRenderPass");
        public static byte* GetRenderAreaGranularity => FromString("vkGetRenderAreaGranularity");
        public static byte* CreateCommandPool => FromString("vkCreateCommandPool");
        public static byte* DestroyCommandPool => FromString("vkDestroyCommandPool");
        public static byte* ResetCommandPool => FromString("vkResetCommandPool");
        public static byte* AllocateCommandBuffers => FromString("vkAllocateCommandBuffers");
        public static byte* FreeCommandBuffers => FromString("vkFreeCommandBuffers");
        public static byte* BeginCommandBuffer => FromString("vkBeginCommandBuffer");
        public static byte* EndCommandBuffer => FromString("vkEndCommandBuffer");
        public static byte* ResetCommandBuffer => FromString("vkResetCommandBuffer");
        public static byte* CmdBindPipeline => FromString("vkCmdBindPipeline");
        public static byte* CmdSetViewport => FromString("vkCmdSetViewport");
        public static byte* CmdSetScissor => FromString("vkCmdSetScissor");
        public static byte* CmdSetLineWidth => FromString("vkCmdSetLineWidth");
        public static byte* CmdSetDepthBias => FromString("vkCmdSetDepthBias");
        public static byte* CmdSetBlendConstants => FromString("vkCmdSetBlendConstants");
        public static byte* CmdSetDepthBounds => FromString("vkCmdSetDepthBounds");
        public static byte* CmdSetStencilCompareMask => FromString("vkCmdSetStencilCompareMask");
        public static byte* CmdSetStencilWriteMask => FromString("vkCmdSetStencilWriteMask");
        public static byte* CmdSetStencilReference => FromString("vkCmdSetStencilReference");
        public static byte* CmdBindDescriptorSets => FromString("vkCmdBindDescriptorSets");
        public static byte* CmdBindIndexBuffer => FromString("vkCmdBindIndexBuffer");
        public static byte* CmdBindVertexBuffers => FromString("vkCmdBindVertexBuffers");
        public static byte* CmdDraw => FromString("vkCmdDraw");
        public static byte* CmdDrawIndexed => FromString("vkCmdDrawIndexed");
        public static byte* CmdDrawIndirect => FromString("vkCmdDrawIndirect");
        public static byte* CmdDrawIndexedIndirect => FromString("vkCmdDrawIndexedIndirect");
        public static byte* CmdDispatch => FromString("vkCmdDispatch");
        public static byte* CmdDispatchIndirect => FromString("vkCmdDispatchIndirect");
        public static byte* CmdCopyBuffer => FromString("vkCmdCopyBuffer");
        public static byte* CmdCopyImage => FromString("vkCmdCopyImage");
        public static byte* CmdBlitImage => FromString("vkCmdBlitImage");
        public static byte* CmdCopyBufferToImage => FromString("vkCmdCopyBufferToImage");
        public static byte* CmdCopyImageToBuffer => FromString("vkCmdCopyImageToBuffer");
        public static byte* CmdUpdateBuffer => FromString("vkCmdUpdateBuffer");
        public static byte* CmdFillBuffer => FromString("vkCmdFillBuffer");
        public static byte* CmdClearColorImage => FromString("vkCmdClearColorImage");
        public static byte* CmdClearDepthStencilImage => FromString("vkCmdClearDepthStencilImage");
        public static byte* CmdClearAttachments => FromString("vkCmdClearAttachments");
        public static byte* CmdResolveImage => FromString("vkCmdResolveImage");
        public static byte* CmdSetEvent => FromString("vkCmdSetEvent");
        public static byte* CmdResetEvent => FromString("vkCmdResetEvent");
        public static byte* CmdWaitEvents => FromString("vkCmdWaitEvents");
        public static byte* CmdPipelineBarrier => FromString("vkCmdPipelineBarrier");
        public static byte* CmdBeginQuery => FromString("vkCmdBeginQuery");
        public static byte* CmdEndQuery => FromString("vkCmdEndQuery");
        public static byte* CmdResetQueryPool => FromString("vkCmdResetQueryPool");
        public static byte* CmdWriteTimestamp => FromString("vkCmdWriteTimestamp");
        public static byte* CmdCopyQueryPoolResults => FromString("vkCmdCopyQueryPoolResults");
        public static byte* CmdPushConstants => FromString("vkCmdPushConstants");
        public static byte* CmdBeginRenderPass => FromString("vkCmdBeginRenderPass");
        public static byte* CmdNextSubpass => FromString("vkCmdNextSubpass");
        public static byte* CmdEndRenderPass => FromString("vkCmdEndRenderPass");
        public static byte* CmdExecuteCommands => FromString("vkCmdExecuteCommands");

        public static byte* DestroySurfaceKHR => FromString("vkDrestroySurface");
        public static byte* GetPhysicalDeviceSurfaceSupportKHR => FromString("vkGetPhysicalDeviceSurfaceSupportKHR");
        public static byte* GetPhysicalDeviceSurfaceCapabilitiesKHR => FromString("vkGetPhysicalDeviceSurfaceCapabilitiesKHR");
        public static byte* GetPhysicalDeviceSurfaceFormatsKHR => FromString("vkGetPhysicalDeviceSurfaceFormatsKHR");
        public static byte* GetPhysicalDeviceSurfacePresentModesKHR => FromString("vkGetPhysicalDeviceSurfacePresentModesKHR");

        public static byte* CreateSwapchainKHR => FromString("vkCreateSwapchainKHR");
        public static byte* DestroySwapchainKHR => FromString("vkDestroySwapchainKHR");
        public static byte* GetSwapchainImagesKHR => FromString("vkGetSwapchainImagesKHR");
        public static byte* AcquireNextImageKHR => FromString("vkAcquireNextImageKHR");
        public static byte* QueuePresentKHR => FromString("vkQueuePresentKHR");

        public static byte* GetPhysicalDeviceDisplayPropertiesKHR => FromString("vkGetPhysicalDeviceDisplayPropertiesKHR");
        public static byte* GetPhysicalDeviceDisplayPlanePropertiesKHR => FromString("vkGetPhysicalDeviceDisplayPlanePropertiesKHR");
        public static byte* GetDisplayPlaneSupportedDisplaysKHR => FromString("vkGetDisplayPlaneSupportedDisplaysKHR");
        public static byte* GetDisplayModePropertiesKHR => FromString("vkGetDisplayModePropertiesKHR");
        public static byte* CreateDisplayModeKHR => FromString("vkCreateDisplayModeKHR");
        public static byte* GetDisplayPlaneCapabilitiesKHR => FromString("vkGetDisplayPlaneCapabilitiesKHR");
        public static byte* CreateDisplayPlaneSurfaceKHR => FromString("vkCreateDisplayPlaneSurfaceKHR");

        public static byte* CreateSharedSwapchainsKHR => FromString("vkCreateSharedSwapchainsKHR");

        public static byte* CreateDebugReportCallbackEXT => FromString("vkCreateDebugReportCallbackEXT");
        public static byte* DestroyDebugReportCallbackEXT => FromString("vkDestroyDebugReportCallbackEXT");
        public static byte* DebugReportMessageEXT => FromString("vkDebugReportMessageEXT");

        public static byte* DebugMarkerSetObjectTagEXT => FromString("vkDebugMarkerSetObjectTagEXT");
        public static byte* DebugMarkerSetObjectNameEXT => FromString("vkDebugMarkerSetObjectNameEXT");
        public static byte* CmdDebugMarkerBeginEXT => FromString("vkCmdDebugMarkerBeginEXT");
        public static byte* CmdDebugMarkerEndEXT => FromString("vkCmdDebugMarkerEndEXT");
        public static byte* CmdDebugMarkerInsertEXT => FromString("vkCmdDebugMarkerInsertEXT");
    }

    public static unsafe class Vk
    {
        // Constants
        public const uint HeaderVersion = 57;
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

        public static readonly GetInstanceProcAddrDelegate GetInstanceProcAddr;
        public static readonly GetDeviceProcAddrDelegate GetDeviceProcAddr;

        static Vk()
        {
            SDL.VulkanLoadLibrary(null);

            IntPtr func = SDL.VulkanGetVkGetInstanceProcAddr();
            if (func == IntPtr.Zero) throw new Exception(Utf8.ToString(SDL.GetError()));
            GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddrDelegate>(func);
        }

        public static T LoadGlobalFunction<T>(byte* name) => LoadInstanceFunction<T>(Instance.Null, name);

        public static T LoadInstanceFunction<T>(Instance instance, byte* name)
        {
            IntPtr func = GetInstanceProcAddr(instance, name);
            if (func == IntPtr.Zero) throw new Exception("Could not load Vulkan function " + Utf8.ToString(name));
            return Marshal.GetDelegateForFunctionPointer<T>(func);
        }

        public static T LoadDeviceFunction<T>(Device device, byte* name)
        {
            IntPtr func = GetDeviceProcAddr(device, name);
            if (func == IntPtr.Zero) throw new Exception("Could not load Vulkan function " + Utf8.ToString(name));
            return Marshal.GetDelegateForFunctionPointer<T>(func);
        }

        private const int ExtensionCount = 90;
        private const int TotalExtensionCharCount = 1955;
        private const int MaxExtensionCharCount = 34;
        private const int AvgExtensionCharCount = 22;

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
    }
}