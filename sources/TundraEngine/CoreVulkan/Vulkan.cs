﻿using System;
using System.IO;
using System.Security;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CoreVulkan
{
    public static class Vulkan
    {
        public readonly static GetInstanceProcAddr GetInstanceProcAddr;

        private static IntPtr _library;

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

        // Extensions
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
        public const string DebugReportExtensionName = "VK_EXT_debug_report";
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

        static Vulkan()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _library = Win32.LoadLibrary("vulkan-1.dll");
                if (_library == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan library");
                }
                IntPtr getProcAddr = Win32.GetProcAddress(_library, "PFN_vkGetInstanceProcAddr");
                if (getProcAddr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan loading function");
                }
                GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddr>(getProcAddr);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Linux.dlerror();
                _library = Linux.dlopen("libvulkan.so.1", Linux.RTLD_NOW);
                if (_library == IntPtr.Zero && !Path.IsPathRooted("libvulkan.so.1"))
                {
                    string localPath = Path.Combine(AppContext.BaseDirectory, "libvulkan.so.1");
                    _library = Linux.dlopen(localPath, Linux.RTLD_NOW);
                }
                if (_library == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan library");
                }
                IntPtr getProcAddr = Linux.dlsym(_library, "PFN_vkGetInstanceProcAddr");
                if (getProcAddr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan loading function");
                }
                GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddr>(getProcAddr);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        //
        // Methods
        //
        public unsafe static T LoadGlobalFunction<T>() => LoadInstanceFunction<T>(Instance.Null);

        public unsafe static T LoadInstanceFunction<T>(Instance instance)
        {
            string name = "vk" + typeof(T).Name;
            IntPtr function = GetInstanceProcAddr(instance, name);
            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

        public unsafe static T LoadInstanceFunctionFromExtension<T>(Instance instance, Text extension, Text[] enabledExtensions)
        {
            foreach (Text enabledExtension in enabledExtensions)
            {
                if (enabledExtension == extension)
                {
                    return LoadInstanceFunction<T>(instance);
                }
            }
            return default(T);
        }

        //
        // Handles
        //
        public struct Instance : IEquatable<Instance>
        {
            public IntPtr Handle;

            public readonly static Instance Null = new Instance();

            public override bool Equals(object obj)
            {
                return obj is Instance && this == (Instance)obj;
            }

            public static bool operator ==(Instance left, Instance right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Instance left, Instance right)
            {
                return !left.Equals(right);
            }

            public bool Equals(Instance other)
            {
                return Handle == other.Handle;
            }

            public override int GetHashCode()
            {
                return Handle.GetHashCode();
            }

            public override string ToString()
            {
                return Handle.ToString();
            }
        }

        public struct PhysicalDevice : IEquatable<PhysicalDevice>
        {
            public IntPtr Handle;

            public readonly static PhysicalDevice Null = new PhysicalDevice();

            public override bool Equals(object obj)
            {
                return obj is PhysicalDevice && this == (PhysicalDevice)obj;
            }

            public static bool operator ==(PhysicalDevice left, PhysicalDevice right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(PhysicalDevice left, PhysicalDevice right)
            {
                return !left.Equals(right);
            }

            public bool Equals(PhysicalDevice other)
            {
                return Handle == other.Handle;
            }

            public override int GetHashCode()
            {
                return Handle.GetHashCode();
            }

            public override string ToString()
            {
                return Handle.ToString();
            }
        }

        public struct Device : IEquatable<Device>
        {
            public IntPtr Handle;

            public readonly static Device Null = new Device();

            public override bool Equals(object obj)
            {
                return obj is Device && this == (Device)obj;
            }

            public static bool operator ==(Device left, Device right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Device left, Device right)
            {
                return !left.Equals(right);
            }

            public bool Equals(Device other)
            {
                return Handle == other.Handle;
            }

            public override int GetHashCode()
            {
                return Handle.GetHashCode();
            }

            public override string ToString()
            {
                return Handle.ToString();
            }
        }

        public struct Queue : IEquatable<Queue>
        {
            public IntPtr Handle;

            public readonly static Queue Null = new Queue();

            public override bool Equals(object obj)
            {
                return obj is Queue && this == (Queue)obj;
            }

            public static bool operator ==(Queue left, Queue right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Queue left, Queue right)
            {
                return !left.Equals(right);
            }

            public bool Equals(Queue other)
            {
                return Handle == other.Handle;
            }

            public override int GetHashCode()
            {
                return Handle.GetHashCode();
            }

            public override string ToString()
            {
                return Handle.ToString();
            }
        }

        public struct CommandBuffer : IEquatable<CommandBuffer>
        {
            public IntPtr Handle;

            public readonly static CommandBuffer Null = new CommandBuffer();

            public override bool Equals(object obj)
            {
                return obj is CommandBuffer && this == (CommandBuffer)obj;
            }

            public static bool operator ==(CommandBuffer left, CommandBuffer right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(CommandBuffer left, CommandBuffer right)
            {
                return !left.Equals(right);
            }

            public bool Equals(CommandBuffer other)
            {
                return Handle == other.Handle;
            }

            public override int GetHashCode()
            {
                return Handle.GetHashCode();
            }

            public override string ToString()
            {
                return Handle.ToString();
            }
        }

        //
        // Non dispatchable handles
        //
        public struct Semaphore
        {
            public ulong Handle;
        }

        public struct Fence
        {
            public ulong Handle;
        }

        public struct DeviceMemory
        {
            public ulong Handle;
        }

        public struct Buffer
        {
            public ulong Handle;
        }

        public struct Image
        {
            public ulong Handle;
        }

        public struct Event
        {
            public ulong Handle;
        }

        public struct QueryPool
        {
            public ulong Handle;
        }

        public struct BufferView
        {
            public ulong Handle;
        }

        public struct ImageView
        {
            public ulong Handle;
        }

        public struct ShaderModule
        {
            public ulong Handle;
        }

        public struct PipelineCache
        {
            public ulong Handle;
        }

        public struct PipelineLayout
        {
            public ulong Handle;
        }

        public struct RenderPass
        {
            public ulong Handle;
        }

        public struct Pipeline
        {
            public ulong Handle;
        }

        public struct DescriptorSetLayout
        {
            public ulong Handle;
        }

        public struct Sampler
        {
            public ulong Handle;
        }

        public struct DescriptorPool
        {
            public ulong Handle;
        }

        public struct DescriptorSet
        {
            public ulong Handle;
        }

        public struct Framebuffer
        {
            public ulong Handle;
        }

        public struct CommandPool
        {
            public ulong Handle;
        }
        
        // Khronos
        public struct Surface
        {
            public ulong Handle;
        }

        public struct Swapchain
        {
            public ulong Handle;
        }

        public struct Display
        {
            public ulong Handle;
        }

        public struct DisplayMode
        {
            public ulong Handle;
        }

        public struct DescriptorUpdateTemplate
        {
            public ulong Handle;
        }
        
        // Multi-vendor
        public struct DebugReportCallback
        {
            public ulong Handle;
        }
        
        // Nvidia
        public struct ObjectTable
        {
            public ulong Handle;
        }

        public struct IndirectCommandsLayout
        {
            public ulong Handle;
        }

        //
        // Platforms
        //
        [SuppressUnmanagedCodeSecurity]
        private static class Win32
        {
            [DllImport("kernel32")]
            public static extern IntPtr LoadLibrary(string fileName);

            [DllImport("kernel32")]
            public static extern IntPtr GetProcAddress(IntPtr module, string procName);

            [DllImport("kernel32")]
            public static extern int FreeLibrary(IntPtr module);
        }

        [SuppressUnmanagedCodeSecurity]
        private static class Linux
        {
            public const int RTLD_NOW = 0x002;

            [DllImport("libdl.so")]
            public static extern IntPtr dlopen(string fileName, int flags);

            [DllImport("libdl.so")]
            public static extern IntPtr dlsym(IntPtr handle, string name);

            [DllImport("libdl.so")]
            public static extern int dlclose(IntPtr handle);

            [DllImport("libdl.so")]
            public static extern string dlerror();
        }
    }
}