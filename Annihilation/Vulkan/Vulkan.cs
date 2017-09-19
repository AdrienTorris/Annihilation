using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using SDL2;

namespace Engine.Vk
{
    #region Vulkan
    public static unsafe class Vulkan
    {
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

        public static GetInstanceProcAddrDelegate GetInstanceProcAddr;
        
        private static CreateInstanceDelegate _createInstance;
        private static EnumerateInstanceExtensionPropertiesDelegate _enumerateInstanceExtensionProperties;
        private static EnumerateInstanceLayerPropertiesDelegate _enumerateInstanceLayerProperties;

        static Vulkan()
        {
            SDL.VulkanLoadLibrary(null);

            GetInstanceProcAddr = LoadGetInstanceProcAddrFunction();
        }

        public static void CreateInstance(ref InstanceCreateInfo createInfo, out Instance instance)
        {
            _createInstance = _createInstance ?? LoadGlobalFunction<CreateInstanceDelegate>(FunctionName.CreateInstance);

            _createInstance(ref createInfo, null, out InstanceHandle handle).CheckError();

            instance = new Instance(handle);
        }

        public static void EnumerateInstanceExtensionProperties(byte* layerName, out ExtensionProperties[] extensionProperties)
        {
            _enumerateInstanceExtensionProperties = _enumerateInstanceExtensionProperties ?? LoadGlobalFunction<EnumerateInstanceExtensionPropertiesDelegate>(FunctionName.EnumerateInstanceExtensionProperties);

            uint count = 0;
            _enumerateInstanceExtensionProperties(layerName, ref count, null).CheckError();
            ExtensionProperties* properties = (ExtensionProperties*)Marshal.AllocHGlobal((int)count * sizeof(ExtensionProperties));
            _enumerateInstanceExtensionProperties(layerName, ref count, properties).CheckError();

            extensionProperties = new ExtensionProperties[count];
            for (int i = 0; i < count; ++i)
            {
                extensionProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public static void EnumerateInstanceLayerProperties(out LayerProperties[] layerProperties)
        {
            _enumerateInstanceLayerProperties = _enumerateInstanceLayerProperties ?? LoadGlobalFunction<EnumerateInstanceLayerPropertiesDelegate>(FunctionName.EnumerateInstanceLayerProperties);

            uint count = 0;
            _enumerateInstanceLayerProperties(ref count, null).CheckError();
            LayerProperties* properties = (LayerProperties*)Marshal.AllocHGlobal((int)count * sizeof(LayerProperties));
            _enumerateInstanceLayerProperties(ref count, properties).CheckError();

            layerProperties = new LayerProperties[count];
            for (int i = 0; i < count; ++i)
            {
                layerProperties[i] = properties[i];
            }

            Marshal.FreeHGlobal(new IntPtr(properties));
        }

        public static GetInstanceProcAddrDelegate LoadGetInstanceProcAddrFunction()
        {
            IntPtr func = SDL.VulkanGetVkGetInstanceProcAddr();
            if (func == IntPtr.Zero) throw new Exception(Utf8.ToString(SDL.GetError()));
            return Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddrDelegate>(func);
        }

        public static T LoadGlobalFunction<T>(byte* name) => LoadInstanceFunction<T>(InstanceHandle.Null, name);

        public static T LoadInstanceFunction<T>(InstanceHandle instanceHandle, byte* name)
        {
            IntPtr func = GetInstanceProcAddr(instanceHandle, name);
            if (func == IntPtr.Zero) throw new Exception("Could not load Vulkan function " + Utf8.ToString(name));
            return Marshal.GetDelegateForFunctionPointer<T>(func);
        }
    }
    #endregion

    #region Names
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

        public static byte* CreateXlibSurfaceKHR => FromString("vkCreateXlibSurfaceKHR");
        public static byte* GetPhysicalDeviceXlibPresentationSupportKHR => FromString("vkGetPhysicalDeviceXlibPresentationSupportKHR");
        public static byte* CreateXcbSurfaceKHR => FromString("vkCreateXcbSurfaceKHR");
        public static byte* GetPhysicalDeviceXcbPresentationSupportKHR => FromString("vkGetPhysicalDeviceXcbPresentationSupportKHR");
        public static byte* CreateWaylandSurfaceKHR => FromString("vkCreateWaylandSurfaceKHR");
        public static byte* GetPhysicalDeviceWaylandPresentationSupportKHR => FromString("vkGetPhysicalDeviceWaylandPresentationSupportKHR");
        public static byte* CreateMirSurfaceKHR => FromString("vkCreateMirSurfaceKHR");
        public static byte* GetPhysicalDeviceMirPresentationSupportKHR => FromString("vkGetPhysicalDeviceMirPresentationSupportKHR");
        public static byte* CreateAndroidSurfaceKHR => FromString("vkCreateAndroidSurfaceKHR");
        public static byte* CreateWin32SurfaceKHR => FromString("vkCreateWin32SurfaceKHR");
        public static byte* GetPhysicalDeviceWin32PresentationSupportKHR => FromString("vkGetPhysicalDeviceWin32PresentationSupportKHR");

        public static byte* GetPhysicalDeviceFeatures2KHR => FromString("vkGetPhysicalDeviceFeatures2KHR");
        public static byte* GetPhysicalDeviceProperties2KHR => FromString("vkGetPhysicalDeviceProperties2KHR");
        public static byte* GetPhysicalDeviceFormatProperties2KHR => FromString("vkGetPhysicalDeviceFormatProperties2KHR");
        public static byte* GetPhysicalDeviceImageFormatProperties2KHR => FromString("vkGetPhysicalDeviceImageFormatProperties2KHR");
        public static byte* GetPhysicalDeviceQueueFamilyProperties2KHR => FromString("vkGetPhysicalDeviceQueueFamilyProperties2KHR");
        public static byte* GetPhysicalDeviceMemoryProperties2KHR => FromString("vkGetPhysicalDeviceMemoryProperties2KHR");
        public static byte* GetPhysicalDeviceSparseImageFormatProperties2KHR => FromString("vkGetPhysicalDeviceSparseImageFormatProperties2KHR");

        public static byte* TrimCommandPoolKHR => FromString("vkTrimCommandPoolKHR");

        public static byte* GetPhysicalDeviceExternalBufferPropertiesKHR => FromString("vkGetPhysicalDeviceExternalBufferPropertiesKHR");

        public static byte* GetMemoryWin32HandleKHR => FromString("vkGetMemoryWin32HandleKHR");
        public static byte* GetMemoryWin32HandlePropertiesKHR => FromString("vkGetMemoryWin32HandlePropertiesKHR");

        public static byte* GetMemoryFdKHR => FromString("vkGetMemoryFdKHR");
        public static byte* GetMemoryFdPropertiesKHR => FromString("vkGetMemoryFdPropertiesKHR");

        public static byte* GetPhysicalDeviceExternalSemaphorePropertiesKHR => FromString("vkGetPhysicalDeviceExternalSemaphorePropertiesKHR");

        public static byte* ImportSemaphoreWin32HandleKHR => FromString("vkImportSemaphoreWin32HandleKHR");
        public static byte* GetSemaphoreWin32HandleKHR => FromString("vkGetSemaphoreWin32HandleKHR");

        public static byte* ImportSemaphoreFdKHR => FromString("vkImportSemaphoreFdKHR");
        public static byte* GetSemaphoreFdKHR => FromString("vkGetSemaphoreFdKHR");

        public static byte* CmdPushDescriptorSetKHR => FromString("vkCmdPushDescriptorSetKHR");

        public static byte* CreateDescriptorUpdateTemplateKHR => FromString("vkCreateDescriptorUpdateTemplateKHR");
        public static byte* DestroyDescriptorUpdateTemplateKHR => FromString("vkDestroyDescriptorUpdateTemplateKHR");
        public static byte* UpdateDescriptorSetWithTemplateKHR => FromString("vkUpdateDescriptorSetWithTemplateKHR");
        public static byte* CmdPushDescriptorSetWithTemplateKHR => FromString("vkCmdPushDescriptorSetWithTemplateKHR");

        public static byte* GetSwapchainStatusKHR => FromString("vkGetSwapchainStatusKHR");

        public static byte* GetPhysicalDeviceExternalFencePropertiesKHR => FromString("vkGetPhysicalDeviceExternalFencePropertiesKHR");

        public static byte* ImportFenceWin32HandleKHR => FromString("vkImportFenceWin32HandleKHR");
        public static byte* GetFenceWin32HandleKHR => FromString("vkGetFenceWin32HandleKHR");

        public static byte* ImportFenceFdKHR => FromString("vkImportFenceFdKHR");
        public static byte* GetFenceFdKHR => FromString("vkGetFenceFdKHR");

        public static byte* GetPhysicalDeviceSurfaceCapabilities2KHR => FromString("vkGetPhysicalDeviceSurfaceCapabilities2KHR");
        public static byte* GetPhysicalDeviceSurfaceFormats2KHR => FromString("vkGetPhysicalDeviceSurfaceFormats2KHR");

        public static byte* GetImageMemoryRequirements2KHR => FromString("vkGetImageMemoryRequirements2KHR");
        public static byte* GetBufferMemoryRequirements2KHR => FromString("vkGetBufferMemoryRequirements2KHR");
        public static byte* GetImageSparseMemoryRequirements2KHR => FromString("vkGetImageSparseMemoryRequirements2KHR");
        
        public static byte* CreateDebugReportCallbackEXT => FromString("vkCreateDebugReportCallbackEXT");
        public static byte* DestroyDebugReportCallbackEXT => FromString("vkDestroyDebugReportCallbackEXT");
        public static byte* DebugReportMessageEXT => FromString("vkDebugReportMessageEXT");

        public static byte* DebugMarkerSetObjectTagEXT => FromString("vkDebugMarkerSetObjectTagEXT");
        public static byte* DebugMarkerSetObjectNameEXT => FromString("vkDebugMarkerSetObjectNameEXT");
        public static byte* CmdDebugMarkerBeginEXT => FromString("vkCmdDebugMarkerBeginEXT");
        public static byte* CmdDebugMarkerEndEXT => FromString("vkCmdDebugMarkerEndEXT");
        public static byte* CmdDebugMarkerInsertEXT => FromString("vkCmdDebugMarkerInsertEXT");

        public static byte* CmdDrawIndirectCountAMD => FromString("vkCmdDrawIndirectCountAMD");
        public static byte* CmdDrawIndexedIndirectCountAMD => FromString("vkCmdDrawIndexedIndirectCountAMD");

        public static byte* GetPhysicalDeviceExternalImageFormatPropertiesNV => FromString("vkGetPhysicalDeviceExternalImageFormatPropertiesNV");

        public static byte* GetMemoryWin32HandleNV => FromString("vkGetMemoryWin32HandleNV");

        public static byte* GetDeviceGroupPeerMemoryFeaturesKHX => FromString("vkGetDeviceGroupPeerMemoryFeaturesKHX");
        public static byte* BindBufferMemory2KHX => FromString("vkBindBufferMemory2KHX");
        public static byte* BindImageMemory2KHX => FromString("vkBindImageMemory2KHX");
        public static byte* CmdSetDeviceMaskKHX => FromString("vkCmdSetDeviceMaskKHX");
        public static byte* GetDeviceGroupPresentCapabilitiesKHX => FromString("vkGetDeviceGroupPresentCapabilitiesKHX");
        public static byte* GetDeviceGroupSurfacePresentModesKHX => FromString("vkGetDeviceGroupSurfacePresentModesKHX");
        public static byte* AcquireNextImage2KHX => FromString("vkAcquireNextImage2KHX");
        public static byte* CmdDispatchBaseKHX => FromString("vkCmdDispatchBaseKHX");
        public static byte* GetPhysicalDevicePresentRectanglesKHX => FromString("vkGetPhysicalDevicePresentRectanglesKHX");

        public static byte* CreateViSurfaceNN => FromString("vkCreateViSurfaceNN");

        public static byte* EnumeratePhysicalDeviceGroupsKHX => FromString("vkEnumeratePhysicalDeviceGroupsKHX");

        public static byte* CmdProcessCommandsNVX => FromString("vkCmdProcessCommandsNVX");
        public static byte* CmdReserveSpaceForCommandsNVX => FromString("vkCmdReserveSpaceForCommandsNVX");
        public static byte* CreateIndirectCommandsLayoutNVX => FromString("vkCreateIndirectCommandsLayoutNVX");
        public static byte* DestroyIndirectCommandsLayoutNVX => FromString("vkDestroyIndirectCommandsLayoutNVX");
        public static byte* CreateObjectTableNVX => FromString("vkCreateObjectTableNVX");
        public static byte* DestroyObjectTableNVX => FromString("vkDestroyObjectTableNVX");
        public static byte* RegisterObjectsNVX => FromString("vkRegisterObjectsNVX");
        public static byte* UnregisterObjectsNVX => FromString("vkUnregisterObjectsNVX");
        public static byte* GetPhysicalDeviceGeneratedCommandsPropertiesNVX => FromString("vkGetPhysicalDeviceGeneratedCommandsPropertiesNVX");

        public static byte* CmdSetViewportWScalingNV => FromString("vkCmdSetViewportWScalingNV");

        public static byte* ReleaseDisplayEXT => FromString("vkReleaseDisplayEXT");

        public static byte* AcquireXlibDisplayEXT => FromString("vkAcquireXlibDisplayEXT");
        public static byte* GetRandROutputDisplayEXT => FromString("vkGetRandROutputDisplayEXT");

        public static byte* GetPhysicalDeviceSurfaceCapabilities2EXT => FromString("vkGetPhysicalDeviceSurfaceCapabilities2EXT");

        public static byte* DisplayPowerControlEXT => FromString("vkDisplayPowerControlEXT");
        public static byte* RegisterDeviceEventEXT => FromString("vkRegisterDeviceEventEXT");
        public static byte* RegisterDisplayEventEXT => FromString("vkRegisterDisplayEventEXT");
        public static byte* GetSwapchainCounterEXT => FromString("vkGetSwapchainCounterEXT");

        public static byte* GetRefreshCycleDurationGOOGLE => FromString("vkGetRefreshCycleDurationGOOGLE");
        public static byte* GetPastPresentationTimingGOOGLE => FromString("vkGetPastPresentationTimingGOOGLE");

        public static byte* CmdSetDiscardRectangleEXT => FromString("vkCmdSetDiscardRectangleEXT");

        public static byte* SetHdrMetadataEXT => FromString("vkSetHdrMetadataEXT");

        public static byte* CreateIOSSurfaceMVK => FromString("vkCreateIOSSurfaceMVK");
        public static byte* CreateMacOSSurfaceMVK => FromString("vkCreateMacOSSurfaceMVK");
    }

    public unsafe struct ExtensionName
    {
        private const int MaxExtensionNameLength = 48;

        public fixed byte Name[MaxExtensionNameLength];

        private static byte* FromString(string str)
        {
            ExtensionName name = new ExtensionName();
            for (int i = 0; i < str.Length; ++i)
            {
                name.Name[i] = (byte)str[i];
            }
            name.Name[str.Length] = 0;
            return name.Name;
        }

        public static byte* Surface => FromString("VK_KHR_surface");
        public static byte* Swapchain => FromString("VK_KHR_swapchain");
        public static byte* Display => FromString("VK_KHR_display");
        public static byte* DisplaySwapchain => FromString("VK_KHR_display_swapchain");
        public static byte* XlibSurface => FromString("VK_KHR_xlib_surface");
        public static byte* XcbSurface => FromString("VK_KHR_xcb_surface");
        public static byte* WaylandSurface => FromString("VK_KHR_wayland_surface");
        public static byte* MirSurface => FromString("VK_KHR_mir_surface");
        public static byte* AndroidSurface => FromString("VK_KHR_android_surface");
        public static byte* Win32Surface => FromString("VK_KHR_win32_surface");
        public static byte* SamplerMirrorClampToEdge => FromString("VK_KHR_sampler_mirror_clamp_to_edge");
        public static byte* GetPhysicalDeviceProperties2 => FromString("VK_KHR_get_physical_device_properties2");
        public static byte* ShaderDrawParameters => FromString("VK_KHR_shader_draw_parameters");
        public static byte* Maintenance1 => FromString("VK_KHR_maintenance1");
        public static byte* ExternalMemoryCapabilities => FromString("VK_KHR_external_memory_capabilities");
        public static byte* ExternalMemory => FromString("VK_KHR_external_memory");
        public static byte* ExternalMemoryWin32 => FromString("VK_KHR_external_memory_win32");
        public static byte* ExternalMemoryFd => FromString("VK_KHR_external_memory_fd");
        public static byte* Win32KeyedMutex => FromString("VK_KHR_win32_keyed_mutex");
        public static byte* ExternalSemaphoreCapabilities => FromString("VK_KHR_external_semaphore_capabilities");
        public static byte* ExternalSemaphore => FromString("VK_KHR_external_semaphore");
        public static byte* ExternalSemaphoreWin32 => FromString("VK_KHR_external_semaphore_win32");
        public static byte* ExternalSemaphoreFd => FromString("VK_KHR_external_semaphore_fd");
        public static byte* PushDescriptor => FromString("VK_KHR_push_descriptor");
        public static byte* Khr16BitStorage => FromString("VK_KHR_16bit_storage");
        public static byte* IncrementalPresent => FromString("VK_KHR_incremental_present");
        public static byte* DescriptorUpdateTemplate => FromString("VK_KHR_descriptor_update_template");
        public static byte* SharedPresentableImage => FromString("VK_KHR_shared_presentable_image");
        public static byte* ExternalFenceCapabilities => FromString("VK_KHR_external_fence_capabilities");
        public static byte* ExternalFence => FromString("VK_KHR_external_fence");
        public static byte* ExternalFenceWin32 => FromString("VK_KHR_external_fence_win32");
        public static byte* ExternalFenceFd => FromString("VK_KHR_external_fence_fd");
        public static byte* GetSurfaceCapabilities2 => FromString("VK_KHR_get_surface_capabilities2");
        public static byte* VariablePointers => FromString("VK_KHR_variable_pointers");
        public static byte* DedicatedAllocation => FromString("VK_KHR_dedicated_allocation");
        public static byte* StorageBufferStorageClass => FromString("VK_KHR_storage_buffer_storage_class");
        public static byte* RelaxedBlockLayout => FromString("VK_KHR_relaxed_block_layout");
        public static byte* GetMemoryRequirements2 => FromString("VK_KHR_get_memory_requirements2");
        public static byte* ExtDebugReport => FromString("VK_EXT_debug_report");
        public static byte* GlslShader => FromString("VK_NV_glsl_shader");
        public static byte* DepthRangeUnrestricted => FromString("VK_EXT_depth_range_unrestricted");
        public static byte* FilterCubic => FromString("VK_IMG_filter_cubic");
        public static byte* RasterizationOrder => FromString("VK_AMD_rasterization_order");
        public static byte* ShaderTrinaryMinmax => FromString("VK_AMD_shader_trinary_minmax");
        public static byte* ShaderExplicitVertexParameter => FromString("VK_AMD_shader_explicit_vertex_parameter");
        public static byte* DebugMarker => FromString("VK_EXT_debug_marker");
        public static byte* GcnShader => FromString("VK_AMD_gcn_shader");
        public static byte* NVDedicatedAllocation => FromString("VK_NV_dedicated_allocation");
        public static byte* DrawIndirectCount => FromString("VK_AMD_draw_indirect_count");
        public static byte* NegativeViewportHeight => FromString("VK_AMD_negative_viewport_height");
        public static byte* GpuShaderHalfFloat => FromString("VK_AMD_gpu_shader_half_float");
        public static byte* ShaderBallot => FromString("VK_AMD_shader_ballot");
        public static byte* TextureGatherBiasLod => FromString("VK_AMD_texture_gather_bias_lod");
        public static byte* Multiview => FromString("VK_KHX_multiview");
        public static byte* FormatPVRTC => FromString("VK_IMG_format_pvrtc");
        public static byte* NVExternalMemoryCapabilities => FromString("VK_NV_external_memory_capabilities");
        public static byte* NVExternalMemory => FromString("VK_NV_external_memory");
        public static byte* NVExternalMemoryWin32 => FromString("VK_NV_external_memory_win32");
        public static byte* NVWin32KeyedMutex => FromString("VK_NV_win32_keyed_mutex");
        public static byte* DeviceGroup => FromString("VK_KHX_device_group");
        public static byte* ValidationFlags => FromString("VK_EXT_validation_flags");
        public static byte* ViSurface => FromString("VK_NN_vi_surface");
        public static byte* ShaderSubgroupBallot => FromString("VK_EXT_shader_subgroup_ballot");
        public static byte* ShaderSubgroupVote => FromString("VK_EXT_shader_subgroup_vote");
        public static byte* DeviceGroupCreation => FromString("VK_KHX_device_group_creation");
        public static byte* DeviceGeneratedCommands => FromString("VK_NVX_device_generated_commands");
        public static byte* ClipSpaceWScaling => FromString("VK_NV_clip_space_w_scaling");
        public static byte* DirectModeDisplay => FromString("VK_EXT_direct_mode_display");
        public static byte* AcquireXlibDisplay => FromString("VK_EXT_acquire_xlib_display");
        public static byte* DisplaySurfaceCounter => FromString("VK_EXT_display_surface_counter");
        public static byte* DisplayControl => FromString("VK_EXT_display_control");
        public static byte* DisplayTiming => FromString("VK_GOOGLE_display_timing");
        public static byte* SampleMaskOverrideCoverage => FromString("VK_NV_sample_mask_override_coverage");
        public static byte* GeometryShaderPassthrough => FromString("VK_NV_geometry_shader_passthrough");
        public static byte* ViewportArray2 => FromString("VK_NV_viewport_array2");
        public static byte* MultiviewPerViewAttributes => FromString("VK_NVX_multiview_per_view_attributes");
        public static byte* ViewportSwizzle => FromString("VK_NV_viewport_swizzle");
        public static byte* DiscardRectangles => FromString("VK_EXT_discard_rectangles");
        public static byte* SwapchainColorSpace => FromString("VK_EXT_swapchain_colorspace");
        public static byte* HDRMetadata => FromString("VK_EXT_hdr_metadata");
        public static byte* IOSSurface => FromString("VK_MVK_ios_surface");
        public static byte* MacOSSurface => FromString("VK_MVK_macos_surface");
        public static byte* SamplerFilterMinmax => FromString("VK_EXT_sampler_filter_minmax");
        public static byte* GPUShaderInt16 => FromString("VK_AMD_gpu_shader_int16");
        public static byte* MixedAttachmentSamples => FromString("VK_AMD_mixed_attachment_samples");
        public static byte* BlendOperationAdvanced => FromString("VK_EXT_blend_operation_advanced");
        public static byte* FragmentCoverageToColor => FromString("VK_NV_fragment_coverage_to_color");
        public static byte* FramebufferMixedSamples => FromString("VK_NV_framebuffer_mixed_samples");
        public static byte* FillRectangle => FromString("VK_NV_fill_rectangle");
        public static byte* PostDepthCoverage => FromString("VK_EXT_post_depth_coverage");
    }

    public unsafe struct LayerName
    {
        private const int MaxLayerNameLength = 48;

        public fixed byte Name[MaxLayerNameLength];

        private static byte* FromString(string str)
        {
            LayerName name = new LayerName();
            for (int i = 0; i < str.Length; ++i)
            {
                name.Name[i] = (byte)str[i];
            }
            name.Name[str.Length] = 0;
            return name.Name;
        }

        public static byte* LunarGStandardValidation => FromString("VK_LAYER_LUNARG_standard_validation");
        public static byte* LunarGApiDump => FromString("VK_LAYER_LUNARG_api_dump");
        public static byte* LunarGMonitor => FromString("VK_LAYER_LUNARG_monitor");
        public static byte* GoogleUniqueObjects => FromString("VK_LAYER_GOOGLE_unique_objects");
        public static byte* LunarGCoreValidation => FromString("VK_LAYER_LUNARG_core_validation");
        public static byte* LunarGObjectTracker => FromString("VK_LAYER_LUNARG_object_tracker");
        public static byte* LunarGParametervalidation => FromString("VK_LAYER_LUNARG_parameter_validation");
        public static byte* LunarGScreenshot => FromString("VK_LAYER_LUNARG_screenshot");
        public static byte* GoogleThreading => FromString("VK_LAYER_GOOGLE_threading");
        public static byte* LunarGDeviceSimulation => FromString("VK_LAYER_LUNARG_device_simulation");
    }
    #endregion

    #region Delegates
    public unsafe delegate void* AllocationFunction(void* userData, Size size, Size alignment, SystemAllocationScope AllocationScope);
    public unsafe delegate void* ReallocationFunction(void* userData, void* original, Size size, Size alignment, SystemAllocationScope AllocationScope);
    public unsafe delegate void FreeFunction(void* userData, void* memory);
    public unsafe delegate void InternalAllocationNotification(void* userData, Size size, InternalAllocationType AllocationType, SystemAllocationScope AllocationScope);
    public unsafe delegate void InternalFreeNotification(void* userData, Size size, InternalAllocationType AllocationType, SystemAllocationScope AllocationScope);
    public unsafe delegate void VoidFunction();
    #endregion

    #region Enums
    public enum PipelineCacheHeaderVersion
    {
        One = 1
    }

    public enum Result : int
    {
        Success = 0,
        NotReady = 1,
        Timeout = 2,
        EventSet = 3,
        EventReset = 4,
        Incomplete = 5,
        ErrorOutOfHostMemory = -1,
        ErrorOutOfDeviceMemory = -2,
        ErrorInitializationFailed = -3,
        ErrorDeviceLost = -4,
        ErrorMemoryMapFailed = -5,
        ErrorLayerNotPresent = -6,
        ErrorExtensionNotPresent = -7,
        ErrorFeatureNotPresent = -8,
        ErrorIncompatibleDriver = -9,
        ErrorTooManyObjects = -10,
        ErrorFormatNotSupported = -11,
        ErrorFragmentedPool = -12,
        ErrorSurfaceLost = -1000000000,
        ErrorNativeWindowInUse = -1000000001,
        Suboptimal = 1000001003,
        ErrorOutOfDate = -1000001004,
        ErrorIncompatibleDisplay = -1000003001,
        ErrorValidationFailed = -1000011001,
        ErrorInvalidShader = -1000012000,
        ErrorOutOfPoolMemory = -1000069000,
        ErrorInvalidExternalHandle = -1000072003,
    }

    public enum StructureType
    {
        ApplicationInfo = 0,
        InstanceCreateInfo = 1,
        DeviceQueueCreateInfo = 2,
        DeviceCreateInfo = 3,
        SubmitInfo = 4,
        MemoryAllocateInfo = 5,
        MappedMemoryRange = 6,
        BindSparseInfo = 7,
        FenceCreateInfo = 8,
        SemaphoreCreateInfo = 9,
        EventCreateInfo = 10,
        QueryPoolCreateInfo = 11,
        BufferCreateInfo = 12,
        BufferViewCreateInfo = 13,
        ImageCreateInfo = 14,
        ImageViewCreateInfo = 15,
        ShaderModuleCreateInfo = 16,
        PipelineCacheCreateInfo = 17,
        PipelineShaderStageCreateInfo = 18,
        PipelineVertexInputStateCreateInfo = 19,
        PipelineInputAssemblyStateCreateInfo = 20,
        PipelineTessellationStateCreateInfo = 21,
        PipelineViewportStateCreateInfo = 22,
        PipelineRasterizationStateCreateInfo = 23,
        PipelineMultisampleStateCreateInfo = 24,
        PipelineDepthStencilStateCreateInfo = 25,
        PipelineColorBlendStateCreateInfo = 26,
        PipelineDynamicStateCreateInfo = 27,
        GraphicsPipelineCreateInfo = 28,
        ComputePipelineCreateInfo = 29,
        PipelineLayoutCreateInfo = 30,
        SamplerCreateInfo = 31,
        DescriptorSetLayoutCreateInfo = 32,
        DescriptorPoolCreateInfo = 33,
        DescriptorSetAllocateInfo = 34,
        WriteDescriptorSet = 35,
        CopyDescriptorSet = 36,
        FramebufferCreateInfo = 37,
        RenderPassCreateInfo = 38,
        CommandPoolCreateInfo = 39,
        CommandBufferAllocateInfo = 40,
        CommandBufferInheritanceInfo = 41,
        CommandBufferBeginInfo = 42,
        RenderPassBeginInfo = 43,
        BufferMemoryBarrier = 44,
        ImageMemoryBarrier = 45,
        MemoryBarrier = 46,
        LoaderInstanceCreateInfo = 47,
        LoaderDeviceCreateInfo = 48,
        SwapchainCreateInfo = 1000001000,
        PresentInfo = 1000001001,
        DisplayModeCreateInfo = 1000002000,
        DisplaySurfaceCreateInfo = 1000002001,
        DisplayPresentInfo = 1000003000,
        XlibSurfaceCreateInfo = 1000004000,
        XcbSurfaceCreateInfo = 1000005000,
        WaylandSurfaceCreateInfo = 1000006000,
        MirSurfaceCreateInfo = 1000007000,
        AndroidSurfaceCreateInfo = 1000008000,
        Win32SurfaceCreateInfo = 1000009000,
        DebugReportCallbackCreateInfo = 1000011000,
        PipelineRasterizationStateRasterizationOrder = 1000018000,
        DebugMarkerObjectNameInfo = 1000022000,
        DebugMarkerObjectTagInfo = 1000022001,
        DebugMarkerMarkerInfo = 1000022002,
        DedicatedAllocationImageCreateInfo = 1000026000,
        DedicatedAllocationBufferCreateInfo = 1000026001,
        DedicatedAllocationMemoryAllocateInfo = 1000026002,
        TextureLodGatherFormatProperties = 1000041000,
        RenderPassMultiviewCreateInfo = 1000053000,
        PhysicalDeviceMultiviewFeatures = 1000053001,
        PhysicalDeviceMultiviewProperties = 1000053002,
        ExternalMemoryImageCreateInfoNv = 1000056000,
        ExportMemoryAllocateInfoNv = 1000056001,
        ImportMemoryWin32HandleInfoNv = 1000057000,
        ExportMemoryWin32HandleInfoNv = 1000057001,
        Win32KeyedMutexAcquireReleaseInfoNv = 1000058000,
        PhysicalDeviceFeatures2 = 1000059000,
        PhysicalDeviceProperties2 = 1000059001,
        FormatProperties2 = 1000059002,
        ImageFormatProperties2 = 1000059003,
        PhysicalDeviceImageFormatInfo2 = 1000059004,
        QueueFamilyProperties2 = 1000059005,
        PhysicalDeviceMemoryProperties2 = 1000059006,
        SparseImageFormatProperties2 = 1000059007,
        PhysicalDeviceSparseImageFormatInfo2 = 1000059008,
        MemoryAllocateFlagsInfo = 1000060000,
        BindBufferMemoryInfo = 1000060001,
        BindImageMemoryInfo = 1000060002,
        DeviceGroupRenderPassBeginInfo = 1000060003,
        DeviceGroupCommandBufferBeginInfo = 1000060004,
        DeviceGroupSubmitInfo = 1000060005,
        DeviceGroupBindSparseInfo = 1000060006,
        DeviceGroupPresentCapabilities = 1000060007,
        ImageSwapchainCreateInfo = 1000060008,
        BindImageMemorySwapchainInfo = 1000060009,
        AcquireNextImageInfo = 1000060010,
        DeviceGroupPresentInfo = 1000060011,
        DeviceGroupSwapchainCreateInfo = 1000060012,
        ValidationFlags = 1000061000,
        ViSurfaceCreateInfoNn = 1000062000,
        PhysicalDeviceGroupProperties = 1000070000,
        DeviceGroupDeviceCreateInfo = 1000070001,
        PhysicalDeviceExternalImageFormatInfo = 1000071000,
        ExternalImageFormatProperties = 1000071001,
        PhysicalDeviceExternalBufferInfo = 1000071002,
        ExternalBufferProperties = 1000071003,
        PhysicalDeviceIdProperties = 1000071004,
        ExternalMemoryBufferCreateInfo = 1000072000,
        ExternalMemoryImageCreateInfo = 1000072001,
        ExportMemoryAllocateInfo = 1000072002,
        ImportMemoryWin32HandleInfo = 1000073000,
        ExportMemoryWin32HandleInfo = 1000073001,
        MemoryWin32HandleProperties = 1000073002,
        MemoryGetWin32HandleInfo = 1000073003,
        ImportMemoryFdInfo = 1000074000,
        MemoryFdProperties = 1000074001,
        MemoryGetFdInfo = 1000074002,
        Win32KeyedMutexAcquireReleaseInfo = 1000075000,
        PhysicalDeviceExternalSemaphoreInfo = 1000076000,
        ExternalSemaphoreProperties = 1000076001,
        ExportSemaphoreCreateInfo = 1000077000,
        ImportSemaphoreWin32HandleInfo = 1000078000,
        ExportSemaphoreWin32HandleInfo = 1000078001,
        D3d12FenceSubmitInfo = 1000078002,
        SemaphoreGetWin32HandleInfo = 1000078003,
        ImportSemaphoreFdInfo = 1000079000,
        SemaphoreGetFdInfo = 1000079001,
        PhysicalDevicePushDescriptorProperties = 1000080000,
        PhysicalDevice16bitStorageFeatures = 1000083000,
        PresentRegions = 1000084000,
        DescriptorUpdateTemplateCreateInfo = 1000085000,
        ObjectTableCreateInfo = 1000086000,
        IndirectCommandsLayoutCreateInfo = 1000086001,
        CommandProcessCommandsInfo = 1000086002,
        CommandReserveSpaceForCommandsInfo = 1000086003,
        DeviceGeneratedCommandsLimits = 1000086004,
        DeviceGeneratedCommandsFeatures = 1000086005,
        PipelineViewportWScalingStateCreateInfo = 1000087000,
        SurfaceCapabilities2Ext = 1000090000,
        DisplayPowerInfo = 1000091000,
        DeviceEventInfo = 1000091001,
        DisplayEventInfo = 1000091002,
        SwapchainCounterCreateInfo = 1000091003,
        PresentTimesInfoGoogle = 1000092000,
        PhysicalDeviceMultiviewPerViewAttributesPropertiesNvx = 1000097000,
        PipelineViewportSwizzleStateCreateInfoNv = 1000098000,
        PhysicalDeviceDiscardRectangleProperties = 1000099000,
        PipelineDiscardRectangleStateCreateInfo = 1000099001,
        HdrMetadata = 1000105000,
        SharedPresentSurfaceCapabilities = 1000111000,
        PhysicalDeviceernalFenceInfo = 1000112000,
        ExternalFenceProperties = 1000112001,
        ExportFenceCreateInfo = 1000113000,
        ImportFenceWin32HandleInfo = 1000114000,
        ExportFenceWin32HandleInfo = 1000114001,
        FenceGetWin32HandleInfo = 1000114002,
        ImportFenceFdInfo = 1000115000,
        FenceGetFdInfo = 1000115001,
        PhysicalDeviceSurfaceInfo2 = 1000119000,
        SurfaceCapabilities2 = 1000119001,
        SurfaceFormat2 = 1000119002,
        PhysicalDeviceVariablePointerFeatures = 1000120000,
        IosSurfaceCreateInfoMvk = 1000122000,
        MacosSurfaceCreateInfoMvk = 1000123000,
        MemoryDedicatedRequirements = 1000127000,
        MemoryDedicatedAllocateInfo = 1000127001,
        PhysicalDeviceSamplerFilterMinmaxProperties = 1000130000,
        SamplerReductionModeCreateInfo = 1000130001,
        BufferMemoryRequirementsInfo2 = 1000146000,
        ImageMemoryRequirementsInfo2 = 1000146001,
        ImageSparseMemoryRequirementsInfo2 = 1000146002,
        MemoryRequirements2 = 1000146003,
        SparseImageMemoryRequirements2 = 1000146004,
        PhysicalDeviceBlendOperationAdvancedFeatures = 1000148000,
        PhysicalDeviceBlendOperationAdvancedProperties = 1000148001,
        PipelineColorBlendAdvancedStateCreateInfo = 1000148002,
        PipelineCoverageToColorStateCreateInfoNv = 1000149000,
        PipelineCoverageModulationStateCreateInfoNv = 1000152000,
    }

    public enum SystemAllocationScope
    {
        Command = 0,
        Object = 1,
        Cache = 2,
        Device = 3,
        Instance = 4
    }

    public enum InternalAllocationType
    {
        Executable = 0
    }

    public enum Format
    {
        Undefined = 0,
        R4G4UNormPack8 = 1,
        R4G4B4A4UNormPack16 = 2,
        B4G4R4A4UNormPack16 = 3,
        R5G6B5UNormPack16 = 4,
        B5G6R5UNormPack16 = 5,
        R5G5B5A1UNormPack16 = 6,
        B5G5R5A1UNormPack16 = 7,
        A1R5G5B5UNormPack16 = 8,
        R8UNorm = 9,
        R8SNorm = 10,
        R8UScaled = 11,
        R8SScaled = 12,
        R8UInt = 13,
        R8SInt = 14,
        R8SRGB = 15,
        R8G8UNorm = 16,
        R8G8SNorm = 17,
        R8G8UScaled = 18,
        R8G8SScaled = 19,
        R8G8UInt = 20,
        R8G8SInt = 21,
        R8G8SRGB = 22,
        R8G8B8UNorm = 23,
        R8G8B8SNorm = 24,
        R8G8B8UScaled = 25,
        R8G8B8SScaled = 26,
        R8G8B8UInt = 27,
        R8G8B8SInt = 28,
        R8G8B8SRGB = 29,
        B8G8R8UNorm = 30,
        B8G8R8SNorm = 31,
        B8G8R8UScaled = 32,
        B8G8R8SScaled = 33,
        B8G8R8UInt = 34,
        B8G8R8SInt = 35,
        B8G8R8SRGB = 36,
        R8G8B8A8UNorm = 37,
        R8G8B8A8SNorm = 38,
        R8G8B8A8UScaled = 39,
        R8G8B8A8SScaled = 40,
        R8G8B8A8UInt = 41,
        R8G8B8A8SInt = 42,
        R8G8B8A8SRGB = 43,
        B8G8R8A8UNorm = 44,
        B8G8R8A8SNorm = 45,
        B8G8R8A8UScaled = 46,
        B8G8R8A8SScaled = 47,
        B8G8R8A8UInt = 48,
        B8G8R8A8SInt = 49,
        B8G8R8A8SRGB = 50,
        A8B8G8R8UNormPack32 = 51,
        A8B8G8R8SNormPack32 = 52,
        A8B8G8R8UScaledPack32 = 53,
        A8B8G8R8SScaledPack32 = 54,
        A8B8G8R8UIntPack32 = 55,
        A8B8G8R8SIntPack32 = 56,
        A8B8G8R8SRGBPack32 = 57,
        A2R10G10B10UNormPack32 = 58,
        A2R10G10B10SNormPack32 = 59,
        A2R10G10B10UScaledPack32 = 60,
        A2R10G10B10SScaledPack32 = 61,
        A2R10G10B10UIntPack32 = 62,
        A2R10G10B10SIntPack32 = 63,
        A2B10G10R10UNormPack32 = 64,
        A2B10G10R10SNormPack32 = 65,
        A2B10G10R10UScaledPack32 = 66,
        A2B10G10R10SScaledPack32 = 67,
        A2B10G10R10UIntPack32 = 68,
        A2B10G10R10SIntPack32 = 69,
        R16UNorm = 70,
        R16SNorm = 71,
        R16UScaled = 72,
        R16SScaled = 73,
        R16UInt = 74,
        R16SInt = 75,
        R16SFloat = 76,
        R16G16UNorm = 77,
        R16G16SNorm = 78,
        R16G16UScaled = 79,
        R16G16SScaled = 80,
        R16G16UInt = 81,
        R16G16SInt = 82,
        R16G16SFloat = 83,
        R16G16B16UNorm = 84,
        R16G16B16SNorm = 85,
        R16G16B16UScaled = 86,
        R16G16B16SScaled = 87,
        R16G16B16UInt = 88,
        R16G16B16SInt = 89,
        R16G16B16SFloat = 90,
        R16G16B16A16UNorm = 91,
        R16G16B16A16SNorm = 92,
        R16G16B16A16UScaled = 93,
        R16G16B16A16SScaled = 94,
        R16G16B16A16UInt = 95,
        R16G16B16A16SInt = 96,
        R16G16B16A16SFloat = 97,
        R32UInt = 98,
        R32SInt = 99,
        R32SFloat = 100,
        R32G32UInt = 101,
        R32G32SInt = 102,
        R32G32SFloat = 103,
        R32G32B32UInt = 104,
        R32G32B32SInt = 105,
        R32G32B32SFloat = 106,
        R32G32B32A32UInt = 107,
        R32G32B32A32SInt = 108,
        R32G32B32A32SFloat = 109,
        R64UInt = 110,
        R64SInt = 111,
        R64SFloat = 112,
        R64G64UInt = 113,
        R64G64SInt = 114,
        R64G64SFloat = 115,
        R64G64B64UInt = 116,
        R64G64B64SInt = 117,
        R64G64B64SFloat = 118,
        R64G64B64A64UInt = 119,
        R64G64B64A64SInt = 120,
        R64G64B64A64SFloat = 121,
        B10G11R11UFloatPack32 = 122,
        E5B9G9R9UFloatPack32 = 123,
        D16UNorm = 124,
        X8D24UNormPack32 = 125,
        D32SFloat = 126,
        S8UInt = 127,
        D16UNormS8UInt = 128,
        D24UNormS8UInt = 129,
        D32SFloatS8UInt = 130,
        BC1RGBUNormBlock = 131,
        BC1RGBSRGBBlock = 132,
        BC1RGBAUNormBlock = 133,
        BC1RGBASRGBBlock = 134,
        BC2UNormBlock = 135,
        BC2SRGBBlock = 136,
        BC3UNormBlock = 137,
        BC3SRGBBlock = 138,
        BC4UNormBlock = 139,
        BC4SNormBlock = 140,
        BC5UNormBlock = 141,
        BC5SNormBlock = 142,
        BC6HUFloatBlock = 143,
        BC6HSFloatBlock = 144,
        BC7UNormBlock = 145,
        BC7SRGBBlock = 146,
        ETC2R8G8B8UNormBlock = 147,
        ETC2R8G8B8SRGBBlock = 148,
        ETC2R8G8B8A1UNormBlock = 149,
        ETC2R8G8B8A1SRGBBlock = 150,
        ETC2R8G8B8A8UNormBlock = 151,
        ETC2R8G8B8A8SRGBBlock = 152,
        EACR11UNormBlock = 153,
        EACR11SNormBlock = 154,
        EACR11G11UNormBlock = 155,
        EACR11G11SNormBlock = 156,
        ASTC4x4UNormBlock = 157,
        ASTC4x4SRGBBlock = 158,
        ASTC5x4UNormBlock = 159,
        ASTC5x4SRGBBlock = 160,
        ASTC5x5UNormBlock = 161,
        ASTC5x5SRGBBlock = 162,
        ASTC6x5UNormBlock = 163,
        ASTC6x5SRGBBlock = 164,
        ASTC6x6UNormBlock = 165,
        ASTC6x6SRGBBlock = 166,
        ASTC8x5UNormBlock = 167,
        ASTC8x5SRGBBlock = 168,
        ASTC8x6UNormBlock = 169,
        ASTC8x6SRGBBlock = 170,
        ASTC8x8UNormBlock = 171,
        ASTC8x8SRGBBlock = 172,
        ASTC10x5UNormBlock = 173,
        ASTC10x5SRGBBlock = 174,
        ASTC10x6UNormBlock = 175,
        ASTC10x6SRGBBlock = 176,
        ASTC10x8UNormBlock = 177,
        ASTC10x8SRGBBlock = 178,
        ASTC10x10UNormBlock = 179,
        ASTC10x10SRGBBlock = 180,
        ASTC12x10UNormBlock = 181,
        ASTC12x10SRGBBlock = 182,
        ASTC12x12UNormBlock = 183,
        ASTC12x12SRGBBlock = 184,
        PVRTC12BPPUNormBlockImg = 1000054000,
        PVRTC14BPPUNormBlockImg = 1000054001,
        PVRTC22BPPUNormBlockImg = 1000054002,
        PVRTC24BPPUNormBlockImg = 1000054003,
        PVRTC12BPPSRGBBlockImg = 1000054004,
        PVRTC14BPPSRGBBlockImg = 1000054005,
        PVRTC22BPPSRGBBlockImg = 1000054006,
        PVRTC24BPPSRGBBlockImg = 1000054007,
    }

    public enum ImageType
    {
        Image1D = 0,
        Image2D = 1,
        Image3D = 2,
    }

    public enum ImageTiling
    {
        Optimal = 0,
        Linear = 1,
    }

    public enum PhysicalDeviceType
    {
        Other = 0,
        IntegratedGPU = 1,
        DiscreteGPU = 2,
        VirtualGPU = 3,
        CPU = 4
    }

    public enum QueryType
    {
        Occlusion = 0,
        PipelineStatistics = 1,
        Timestamp = 2
    }

    public enum SharingMode
    {
        Exclusive = 0,
        Concurrent = 1
    }

    public enum ImageLayout
    {
        Undefined = 0,
        General = 1,
        ColorAttachmentOptimal = 2,
        DepthStencilAttachmentOptimal = 3,
        DepthStencilReadOnlyOptimal = 4,
        ShaderReadOnlyOptimal = 5,
        TransferSrcOptimal = 6,
        TransferDstOptimal = 7,
        Preinitialized = 8,
        PresentSrc = 1000001002,
        SharedPresent = 1000111000,
    }

    public enum ImageViewType
    {
        Type1D = 0,
        Type2D = 1,
        Type3D = 2,
        TypeCube = 3,
        Type1DArray = 4,
        Type2DArray = 5,
        TypeCubeArray = 6
    }

    public enum ComponentSwizzle
    {
        Identity = 0,
        Zero = 1,
        One = 2,
        R = 3,
        G = 4,
        B = 5,
        A = 6
    }

    public enum VertexInputRate
    {
        Vertex = 0,
        Instance = 1
    }

    public enum PrimitiveTopology
    {
        PointList = 0,
        LineList = 1,
        LineStrip = 2,
        TriangleList = 3,
        TriangleStrip = 4,
        TriangleFan = 5,
        LineListWithAdjacency = 6,
        LineStripWithAdjacency = 7,
        TriangleListWithAdjacency = 8,
        TriangleStripWithAdjacency = 9,
        PatchList = 10
    }

    public enum PolygonMode
    {
        Fill = 0,
        Line = 1,
        Point = 2,
        FillTriangle = 1000153000
    }

    public enum FrontFace
    {
        CounterClockwise = 0,
        Clockwise = 1
    }

    public enum CompareOp
    {
        Never = 0,
        Less = 1,
        Equal = 2,
        LessOrEqual = 3,
        Greater = 4,
        NotEqual = 5,
        GreaterOrEqual = 6,
        Always = 7
    }

    public enum StencilOp
    {
        Keep = 0,
        Zero = 1,
        Replace = 2,
        IncrementAndClamp = 3,
        DecrementAndClamp = 4,
        Invert = 5,
        IncrementAndWrap = 6,
        DecrementAndWrap = 7,
    }

    public enum LogicOp
    {
        Clear = 0,
        And = 1,
        AndReverse = 2,
        Copy = 3,
        AndInverted = 4,
        NoOp = 5,
        Xor = 6,
        Or = 7,
        Nor = 8,
        Equivalent = 9,
        Invert = 10,
        OrReverse = 11,
        CopyInverted = 12,
        OrInverted = 13,
        Nand = 14,
        Set = 15,
    }

    public enum BlendFactor
    {
        Zero = 0,
        One = 1,
        SrcColor = 2,
        OneMinusSrcColor = 3,
        DstColor = 4,
        OneMinusDstColor = 5,
        SrcAlpha = 6,
        OneMinusSrcAlpha = 7,
        DstAlpha = 8,
        OneMinusDstAlpha = 9,
        ConstantColor = 10,
        OneMinusConstantColor = 11,
        ConstantAlpha = 12,
        OneMinusConstantAlpha = 13,
        SrcAlphaSaturate = 14,
        Src1Color = 15,
        OneMinusSrc1Color = 16,
        Src1Alpha = 17,
        OneMinusSrc1Alpha = 18,
    }

    public enum BlendOp
    {
        Add = 0,
        Subtact = 1,
        ReverseSubtract = 2,
        Min = 3,
        Max = 4,
        Zero = 1000148000,
        Src = 1000148001,
        Dst = 1000148002,
        SrcOver = 1000148003,
        DstOver = 1000148004,
        SrcIn = 1000148005,
        DstIn = 1000148006,
        SrcOut = 1000148007,
        DstOut = 1000148008,
        SrcAtop = 1000148009,
        DstAtop = 1000148010,
        Xor = 1000148011,
        Multiply = 1000148012,
        Screen = 1000148013,
        Overlay = 1000148014,
        Darken = 1000148015,
        Lighten = 1000148016,
        ColorDodge = 1000148017,
        ColorBurn = 1000148018,
        HardLight = 1000148019,
        SoftLight = 1000148020,
        Difference = 1000148021,
        Exclusion = 1000148022,
        Invert = 1000148023,
        InvertRGB = 1000148024,
        LinearDodge = 1000148025,
        LinearBurn = 1000148026,
        VividLight = 1000148027,
        LinearLight = 1000148028,
        PinLight = 1000148029,
        HardMix = 1000148030,
        HSLHue = 1000148031,
        HSLSaturation = 1000148032,
        HSLColor = 1000148033,
        HSLLuminosity = 1000148034,
        Plus = 1000148035,
        PlusClamped = 1000148036,
        PlusClampedAlpha = 1000148037,
        PlusDarker = 1000148038,
        Minus = 1000148039,
        MinusClamped = 1000148040,
        Contrast = 1000148041,
        InvertOVG = 1000148042,
        Red = 1000148043,
        Green = 1000148044,
        Blue = 1000148045,
    }

    public enum DynamicState
    {
        Viewport = 0,
        Scissor = 1,
        LineWidth = 2,
        DepthBias = 3,
        BlendConstants = 4,
        DepthBounds = 5,
        StencilCompareMask = 6,
        StencilWriteMask = 7,
        StencilReference = 8,
        ViewportWScaling = 1000087000,
        DiscardRectangle = 1000099000
    }

    public enum Filter
    {
        Nearest = 0,
        Linear = 1,
        Cubic = 1000015000
    }

    public enum SamplerMipmapMode
    {
        Nearest = 0,
        Linear = 1
    }

    public enum SamplerAddressMode
    {
        Repeat = 0,
        MirroredRepeat = 1,
        ClampToEdge = 2,
        ClampToBorder = 3,
        MirrorClampToEdge = 4
    }

    public enum BorderColor
    {
        FloatTransparentBlack = 0,
        IntTransparentBlack = 1,
        FloatOpaqueBlack = 2,
        IntOpaqueBlack = 3,
        FloatOpaqueWhite = 4,
        IntOpaqueWhite = 5
    }

    public enum DescriptorType
    {
        Sampler = 0,
        CombinedImageSampler = 1,
        SampledImage = 2,
        StorageImage = 3,
        UniformTexelBuffer = 4,
        StorageTexelBuffer = 5,
        UniformBuffer = 6,
        StorageBuffer = 7,
        UniformBufferDynamic = 8,
        StorageBufferDynamic = 9,
        InputAttachment = 10
    }

    public enum AttachmentLoadOp
    {
        Load = 0,
        Clear = 1,
        DontCare = 2
    }

    public enum AttachmentStoreOp
    {
        Store = 0,
        DontCare = 1
    }

    public enum PipelineBindPoint
    {
        Graphics = 0,
        Compute = 1
    }

    public enum CommandBufferLevel
    {
        Primary = 0,
        Secondary = 1
    }

    public enum IndexType
    {
        Uint16 = 0,
        Uint32 = 1
    }

    public enum SubpassContents
    {
        Inline = 0,
        SecondaryCommandBuffers = 1
    }

    public enum ObjectType
    {
        Unkwown = 0,
        Instance = 1,
        PhysicalDevice = 2,
        Device = 3,
        Queue = 4,
        Semaphore = 5,
        CommandBuffer = 6,
        Fence = 7,
        DeviceMemory = 8,
        Buffer = 9,
        Image = 10,
        Event = 11,
        QueryPool = 12,
        BufferView = 13,
        ImageView = 14,
        ShaderModule = 15,
        PipelineCache = 16,
        PipelineLayout = 17,
        RenderPass = 18,
        Pipeline = 19,
        DescriptorSetLayout = 20,
        Sampler = 21,
        DescriptorPool = 22,
        DescriptorSet = 23,
        Framebuffer = 24,
        CommandPool = 25,
        Surface = 1000000000,
        Swapchain = 1000001000,
        Display = 1000002000,
        DisplayMode = 1000002001,
        DebugReportCallback = 1000011000,
        DescriptorUpdateTemplate = 1000085000,
        ObjectTable = 1000086000,
        IndirectCommandsLayout = 1000086001
    }

    [Flags] public enum InstanceCreateFlags : uint { None = 0 }

    [Flags]
    public enum FormatFeatureFlags : uint
    {
        SampledImage = 1 << 0,
        StorageImage = 1 << 1,
        StorageImageAtomic = 1 << 2,
        UniformTexelBuffer = 1 << 3,
        StorageTexelBuffer = 1 << 4,
        StorageTexelBufferAtomic = 1 << 5,
        VertexBuffer = 1 << 6,
        ColorAttachment = 1 << 7,
        ColorAttachmentBlend = 1 << 8,
        DepthStencilAttachment = 1 << 9,
        BlitSrc = 1 << 10,
        BlitDst = 1 << 11,
        SampledImageFilterLinear = 1 << 12,
        SampledImageFilterCubic = 1 << 13,
        TransferSrc = 1 << 14,
        TransferDst = 1 << 15,
        SampledImageFilterMinMax = 1 << 16
    }

    [Flags]
    public enum ImageUsageFlags : uint
    {
        TransferSrc = 0x00000001,
        TransferDst = 0x00000002,
        Sampled = 0x00000004,
        Storage = 0x00000008,
        ColorAttachment = 0x00000010,
        DepthStencilAttachment = 0x00000020,
        TransientAttachment = 0x00000040,
        InputAttachment = 0x00000080,
    }

    [Flags]
    public enum ImageCreateFlags : uint
    {
        SparseBinding = 0x00000001,
        SparseResidency = 0x00000002,
        SparseAliased = 0x00000004,
        MutableFormat = 0x00000008,
        CubeCompatible = 0x00000010,
        BindSfr = 0x00000040,
        Image2DArrayCompatible = 0x00000020,
    }

    [Flags]
    public enum SampleCountFlags : uint
    {
        Sample1 = 0x00000001,
        Sample2 = 0x00000002,
        Sample4 = 0x00000004,
        Sample8 = 0x00000008,
        Sample16 = 0x00000010,
        Sample32 = 0x00000020,
        Sample64 = 0x00000040
    }

    [Flags]
    public enum QueueFlags : uint
    {
        Graphics = 1 << 0,
        Compute = 1 << 1,
        Transfer = 1 << 2,
        SparseBinding = 1 << 3
    }

    [Flags]
    public enum MemoryPropertyFlags : uint
    {
        DeviceLocal = 0x00000001,
        HostVisible = 0x00000002,
        HostCoherent = 0x00000004,
        HostCached = 0x00000008,
        LazilyAllocated = 0x00000010,
    }

    [Flags]
    public enum MemoryHeapFlags : uint
    {
        DeviceLocal = 0x00000001,
        MultiInstance = 0x00000002,
    }

    [Flags] public enum DeviceCreateFlags : uint { None = 0 }
    [Flags] public enum DeviceQueueCreateFlags : uint { None = 0 }

    [Flags]
    public enum PipelineStageFlags : uint
    {
        TopOfPipe = 1 << 0,
        DrawIndirect = 1 << 1,
        VertexInput = 1 << 2,
        VertexShader = 1 << 3,
        TessellationControlShader = 1 << 4,
        TessellationEvaluationShader = 1 << 5,
        GeometryShader = 1 << 6,
        FragmentShader = 1 << 7,
        EarlyFragmentTests = 1 << 8,
        LateFragmentTests = 1 << 9,
        ColorAttachmentOutput = 1 << 10,
        ComputeShader = 1 << 11,
        Transfer = 1 << 12,
        BottomOfPipe = 1 << 13,
        Host = 1 << 14,
        AllGraphics = 1 << 15,
        AllCommands = 1 << 16,
        CommandProcess = 1 << 17
    }

    [Flags] public enum MemoryMapFlags : uint { None = 0 }

    [Flags]
    public enum ImageAspectFlags : uint
    {
        Color = 1 << 0,
        Depth = 1 << 1,
        Stencil = 1 << 2,
        Metadata = 1 << 3
    }

    [Flags]
    public enum SparseImageFormatFlags : uint
    {
        SingleMiptail = 1 << 0,
        AlignedMipSize = 1 << 1,
        NonstandardBlockSize = 1 << 2
    }

    [Flags]
    public enum SparseMemoryBindFlags : uint
    {
        Metadata = 1 << 0
    }

    [Flags]
    public enum FenceCreateFlags : uint
    {
        None = 0,
        Signaled = 1 << 0
    }

    [Flags] public enum SemaphoreCreateFlags : uint { None = 0 }
    [Flags] public enum EventCreateFlags : uint { None = 0 }
    [Flags] public enum QueryPoolCreateFlags : uint { None = 0 }

    [Flags]
    public enum QueryPipelineStatisticFlags : uint
    {
        InputAssemblyVertices = 1 << 0,
        InputAssemblyPrimitives = 1 << 1,
        VertexShaderInvocations = 1 << 2,
        GeometryShaderInvocations = 1 << 3,
        GeometryShaderPrimitives = 1 << 4,
        ClippingInvocations = 1 << 5,
        ClippingPrimitives = 1 << 6,
        FragmentShaderInvocations = 1 << 7,
        TessellationControlShaderPatches = 1 << 8,
        TessellationEvaluationShaderInvocations = 1 << 9,
        ComputeShaderInvocations = 1 << 10
    }

    [Flags]
    public enum QueryResultFlags : uint
    {
        SixtyFour = 1 << 0,
        Wait = 1 << 1,
        WithAvailability = 1 << 2,
        Partial = 1 << 3
    }

    [Flags]
    public enum BufferCreateFlags : uint
    {
        SparseBinding = 1 << 0,
        SparseResidency = 1 << 1,
        SparseAliased = 1 << 2
    }

    [Flags]
    public enum BufferUsageFlags : uint
    {
        TransferSrc = 1 << 0,
        TransferDst = 1 << 1,
        UniformTexelBuffer = 1 << 2,
        StorageTexelBuffer = 1 << 3,
        UniformBuffer = 1 << 4,
        StorageBuffer = 1 << 5,
        IndexBuffer = 1 << 6,
        VertexBuffer = 1 << 7,
        IndirectBuffer = 1 << 8
    }

    [Flags] public enum BufferViewCreateFlags : uint { None = 0 }
    [Flags] public enum ImageViewCreateFlags : uint { None = 0 }
    [Flags] public enum ShaderModuleCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineCacheCreateFlags : uint { None = 0 }

    [Flags]
    public enum PipelineCreateFlags : uint
    {
        DisableOptimization = 0x00000001,
        AllowDerivatives = 0x00000002,
        Derivative = 0x00000004,
        ViewIndexFromDeviceIndex = 0x00000008,
        DispatchBase = 0x00000010
    }

    [Flags] public enum PipelineShaderStageCreateFlags : uint { None = 0 }

    [Flags]
    public enum ShaderStageFlags : uint
    {
        Vertex = 0x00000001,
        TessellationControl = 0x00000002,
        TessellationEvaluation = 0x00000004,
        Geometry = 0x00000008,
        Fragment = 0x00000010,
        Compute = 0x00000020,
        AllGraphics = 0x0000001F,
        All = 0x7FFFFFFF
    }

    [Flags] public enum PipelineVertexInputStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineInputAssemblyStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineTessellationStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineViewportStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineRasterizationStateCreateFlags : uint { None = 0 }

    [Flags]
    public enum CullModeFlags : uint
    {
        None = 0,
        Front = 0x00000001,
        Back = 0x00000002,
        FrontAndBack = 0x00000003
    }

    [Flags] public enum PipelineMultisampleStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineDepthStencilStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineColorBlendStateCreateFlags : uint { None = 0 }

    [Flags]
    public enum ColorComponentFlags : uint
    {
        R = 0x00000001,
        G = 0x00000002,
        B = 0x00000004,
        A = 0x00000008
    }

    [Flags] public enum PipelineDynamicStateCreateFlags : uint { None = 0 }
    [Flags] public enum PipelineLayoutCreateFlags : uint { None = 0 }
    [Flags] public enum SamplerCreateFlags : uint { None = 0 }

    [Flags]
    public enum DescriptorSetLayoutCreateFlags : uint
    {
        PushDescriptor = 1 << 0
    }

    [Flags]
    public enum DescriptorPoolCreateFlags : uint
    {
        FreeDescriptorSet = 1 << 0
    }

    [Flags] public enum DescriptorPoolResetFlags : uint { None = 0 }
    [Flags] public enum FramebufferCreateFlags : uint { None = 0 }
    [Flags] public enum RenderPassCreateFlags : uint { None = 0 }

    [Flags]
    public enum AttachmentDescriptionFlags : uint
    {
        MayAlias = 1 << 0
    }

    [Flags]
    public enum SubpassDescriptionFlags : uint
    {
        PerViewAttributes = 1 << 0,
        PerViewPositionXOnly = 1 << 1
    }

    [Flags]
    public enum AccessFlags : uint
    {
        IndirectCommandRead = 1 << 0,
        IndexRead = 1 << 1,
        VertexAttributeRead = 1 << 2,
        UniformRead = 1 << 3,
        InputAttachmentRead = 1 << 4,
        ShaderRead = 1 << 5,
        ShaderWrite = 1 << 6,
        ColorAttachmentRead = 1 << 7,
        ColorAttachmentWrite = 1 << 8,
        DepthStencilAttachmentRead = 1 << 9,
        DepthStencilAttachmentWrite = 1 << 10,
        TransferRead = 1 << 11,
        TransferWrite = 1 << 12,
        HostRead = 1 << 13,
        HostWrite = 1 << 14,
        MemoryRead = 1 << 15,
        MemoryWrite = 1 << 16,
        CommandProcessRead = 1 << 17,
        CommandProcessWrite = 1 << 18,
        ColorAttachmentReadNonCoherent = 1 << 19
    }

    [Flags]
    public enum DependencyFlags : uint
    {
        ByRegion = 1 << 0,
        ViewLocal = 1 << 1,
        DeviceGroup = 1 << 2
    }

    [Flags]
    public enum CommandPoolCreateFlags : uint
    {
        Transient = 1 << 0,
        ResetCommandBuffer = 1 << 1
    }

    [Flags]
    public enum CommandPoolResetFlags : uint
    {
        ReleaseResources = 1 << 0
    }

    [Flags]
    public enum CommandBufferUsageFlags : uint
    {
        OneTimeSubmit = 1 << 0,
        RenderPassContinue = 1 << 1,
        SimultaneousUse = 1 << 2
    }

    [Flags]
    public enum QueryControlFlags : uint
    {
        Precise = 1 << 0
    }

    [Flags]
    public enum CommandBufferResetFlags : uint
    {
        ReleaseResources = 1 << 0
    }

    [Flags]
    public enum StencilFaceFlags : uint
    {
        Front = 1 << 0,
        Back = 1 << 1,
        FrontAndBack = Front | Back
    }

    //
    // Khronos
    //
    public enum ColorSpace
    {
        SrgbNonLinear = 0,
        DisplayP3NonLinear = 1000104001,
        ExtendedSrgbLinear = 1000104002,
        DciP3Linear = 1000104003,
        DciP3NonLinear = 1000104004,
        Bt709Linear = 1000104005,
        Bt709NonLinear = 1000104006,
        Bt2020Linear = 1000104007,
        Hdr10St2084 = 1000104008,
        DolbyVision = 1000104009,
        Hdr10Hlg = 1000104010,
        AdobeRgbLinear = 1000104011,
        AdobeRgbNonLinear = 1000104012,
        PassThrough = 1000104013,
        ExtendedSrgbNonLinear = 1000104014
    }

    public enum PresentMode
    {
        Immediate = 0,
        Mailbox = 1,
        Fifo = 2,
        FifoRelaxed = 3,
        SharedDemandRefresh = 1000111000,
        SharedContinuousRefresh = 1000111001
    }

    [Flags]
    public enum SurfaceTransformFlags : uint
    {
        Identity = 0x00000001,
        Rotate90 = 0x00000002,
        Rotate180 = 0x00000004,
        Rotate270 = 0x00000008,
        HorizontalMirror = 0x00000010,
        HorizontalMirrorRotate90 = 0x00000020,
        HorizontalMirrorRotate180 = 0x00000040,
        HorizontalMirrorRotate270 = 0x00000080,
        Inherit = 0x00000100,
    }

    [Flags]
    public enum CompositeAlphaFlags : uint
    {
        Opaque = 1 << 0,
        PreMultiplied = 1 << 1,
        PostMultiplied = 1 << 2,
        Inherit = 1 << 3
    }

    [Flags]
    public enum SwapchainCreateFlags : uint
    {
        BindSFR = 1 << 0
    }

    [Flags]
    public enum DisplayPlaneAlphaFlags : uint
    {
        Opaque = 1 << 0,
        Global = 1 << 1,
        PerPixel = 1 << 2,
        PerPixelPremultiplied = 1 << 3
    }

    [Flags] public enum DisplayModeCreateFlags : uint { None = 0 }
    [Flags] public enum DisplaySurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum XlibSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum XcbSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum WaylandSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum MirSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum AndroidSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum Win32SurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum CommandPoolTrimFlags : uint { None = 0 }

    [Flags]
    public enum ExternalMemoryHandleTypeFlags : uint
    {
        OpaqueFd = 1 << 0,
        OpaqueWin32 = 1 << 1,
        OpaqueWin32Kmt = 1 << 2,
        D3D11Texture = 1 << 3,
        D3D11TextureKmt = 1 << 4,
        D3D12Heap = 1 << 5,
        D3D12Resource = 1 << 6
    }

    [Flags]
    public enum ExternalMemoryFeatureFlags : uint
    {
        DedicatedOnly = 1 << 0,
        Exportable = 1 << 1,
        Importable = 1 << 2
    }

    [Flags]
    public enum ExternalSemaphoreHandleTypeFlags : uint
    {
        OpaqueFd = 1 << 0,
        OpaqueWin32 = 1 << 1,
        OpaqueWin32Kmt = 1 << 2,
        D3D12Fence = 1 << 3,
        SyncFd = 1 << 4
    }

    [Flags]
    public enum ExternalSemaphoreFeatureFlags : uint
    {
        Exportable = 1 << 0,
        Importable = 1 << 1
    }

    [Flags]
    public enum SemaphoreImportFlags : uint
    {
        Temporary = 1 << 0
    }

    public enum DescriptorUpdateTemplateType
    {
        DescriptorSet = 0,
        PushDescriptors = 1
    }

    [Flags] public enum DescriptorUpdateTemplateCreateFlags : uint { None = 0 }

    [Flags]
    public enum ExternalFenceHandleTypeFlags : uint
    {
        OpaqueFd = 1 << 0,
        OpaqueWin32 = 1 << 1,
        OpaqueWin32Kmt = 1 << 2,
        SyncFd = 1 << 3
    }

    [Flags]
    public enum ExternalFenceFeatureFlags : uint
    {
        Exportable = 1 << 0,
        Importable = 1 << 1
    }

    [Flags]
    public enum FenceImportFlags : uint
    {
        Temporary = 1 << 0
    }

    //
    // Multi-vendor
    //
    public enum DebugReportObjectType
    {
        Unknown = 0,
        Instance = 1,
        PhysicalDevice = 2,
        Device = 3,
        Queue = 4,
        Semaphore = 5,
        CommandBuffer = 6,
        Fence = 7,
        DeviceMemory = 8,
        Buffer = 9,
        Image = 10,
        Event = 11,
        QueryPool = 12,
        BufferView = 13,
        ImageView = 14,
        ShaderModule = 15,
        PipelineCache = 16,
        PipelineLayout = 17,
        RenderPass = 18,
        Pipeline = 19,
        DescriptorSetLayout = 20,
        Sampler = 21,
        DescriptorPool = 22,
        DescriptorSet = 23,
        Framebuffer = 24,
        CommandPool = 25,
        Surface = 26,
        Swapchain = 27,
        DebugReportCallback = 28,
        Display = 29,
        DisplayMode = 30,
        ObjectTable = 31,
        IndirectCommandsLayout = 32,
        DescriptorUpdateTemplate = 1000085000
    }

    [Flags]
    public enum DebugReportFlags : uint
    {
        Information = 0x00000001,
        Warning = 0x00000002,
        PerformanceWarning = 0x00000004,
        Error = 0x00000008,
        Debug = 0x00000010,
    }

    public enum ValidationCheck
    {
        All = 0,
        Shaders = 1
    }

    [Flags]
    public enum SurfaceCounterFlags : uint
    {
        VBlank = 1 << 0
    }

    public enum DisplayPowerState
    {
        Off = 0,
        Suspend = 1,
        On = 2
    }

    public enum DeviceEventType
    {
        DisplayHotplug = 0
    }

    public enum DisplayEventType
    {
        FirstPixelOut = 0
    }

    public enum DiscardRectangleMode
    {
        Inclusive = 0,
        Exclusive = 1
    }

    [Flags] public enum PipelineDiscardRectangleStateCreateFlags : uint { None = 0 }

    public enum SamplerReductionMode
    {
        WeightedAverage = 0,
        Min = 1,
        Max = 2
    }

    public enum BlendOverlap
    {
        Uncorrelated = 0,
        Disjoint = 1,
        Conjoint = 2
    }

    //
    // AMD
    //
    public enum RasterizationOrder
    {
        Strict = 0,
        Relaxed = 1
    }

    //
    // Nvidia
    //
    [Flags]
    public enum ExternalMemoryHandleTypeFlagsNV : uint
    {
        OpaqueWin32 = 1 << 0,
        OpaqueWin32Kmt = 1 << 1,
        D3D11Image = 1 << 2,
        D3D11ImageKmt = 1 << 3
    }

    [Flags]
    public enum ExternalMemoryFeatureFlagsNV : uint
    {
        Dedicated = 1 << 0,
        Exportable = 1 << 1,
        Importable = 1 << 2
    }

    [Flags] public enum PipelineCoverageToColorStateCreateFlags : uint { None = 0 }

    public enum CoverageModulationMode
    {
        None = 0,
        RGB = 1,
        Alpha = 2,
        RGBA = 3
    }

    [Flags] public enum PipelineCoverageModulationStateCreateFlags : uint { None = 0 }


    public enum ViewportCoordinateSwizzle
    {
        PositiveX = 0,
        NegativeX = 1,
        PositiveY = 2,
        NegativeY = 3,
        PositiveZ = 4,
        NegativeZ = 5,
        PositiveW = 6,
        NegativeW = 7
    }

    [Flags] public enum PipelineViewportSwizzleStateCreateFlags : uint { None = 0 }

    //
    // Khronos X
    //
    [Flags]
    public enum PeerMemoryFeatureFlags : uint
    {
        CopySrc = 1 << 0,
        CopyDst = 1 << 1,
        GenericSrc = 1 << 2,
        GenericDst = 1 << 3
    }

    [Flags]
    public enum MemoryAllocateFlags : uint
    {
        DeviceMask = 1 << 0
    }

    [Flags]
    public enum DeviceGroupPresentModeFlags : uint
    {
        Local = 1 << 0,
        Remote = 1 << 1,
        Sum = 1 << 2,
        LocalMultiDevice = 1 << 3
    }

    //
    // Nintendo
    //
    [Flags] public enum ViSurfaceCreateFlags : uint { None = 0 }

    //
    // Nvidia X
    //
    public enum IndirectCommandsTokenType
    {
        Pipeline = 0,
        Descriptor = 1,
        IndexBuffer = 2,
        VertexBuffer = 3,
        PushConstant = 4,
        DrawIndexed = 5,
        Draw = 6,
        Dispatch = 7
    }

    public enum ObjectEntryType
    {
        DescriptorSet = 0,
        Pipeline = 1,
        IndexBuffer = 2,
        VertexBuffer = 3,
        PushConstant = 4
    }

    [Flags]
    public enum IndirectCommandsLayoutUsageFlags : uint
    {
        UnorderedSequences = 1 << 0,
        SparseSequences = 1 << 1,
        EmptyExecutions = 1 << 2,
        IndexedSequences = 1 << 3
    }

    [Flags]
    public enum ObjectEntryUsageFlags : uint
    {
        Graphics = 1 << 0,
        Compute = 1 << 1
    }

    //
    // MoltenVK
    // 
    [Flags] public enum IOSSurfaceCreateFlags : uint { None = 0 }
    [Flags] public enum MacOSSurfaceCreateFlags : uint { None = 0 }
    #endregion

    #region Handles
    public struct InstanceHandle : IEquatable<InstanceHandle>
    {
        public IntPtr Handle;

        public static readonly InstanceHandle Null;

        public bool Equals(InstanceHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is InstanceHandle && this == (InstanceHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(InstanceHandle left, InstanceHandle right) => left.Equals(right);
        public static bool operator !=(InstanceHandle left, InstanceHandle right) => !left.Equals(right);

        public static implicit operator IntPtr(InstanceHandle instance) => instance.Handle;
        public static implicit operator InstanceHandle(IntPtr handle) => new InstanceHandle { Handle = handle };
        public static implicit operator SDL.VkInstance(InstanceHandle instance) => new SDL.VkInstance { Handle = instance.Handle };
        public static implicit operator InstanceHandle(SDL.VkInstance instance) => new InstanceHandle { Handle = instance.Handle };
    }
    
    public struct PhysicalDeviceHandle : IEquatable<PhysicalDeviceHandle>
    {
        public IntPtr Handle;

        public readonly static PhysicalDeviceHandle Null;

        public bool Equals(PhysicalDeviceHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is PhysicalDeviceHandle && this == (PhysicalDeviceHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(PhysicalDeviceHandle left, PhysicalDeviceHandle right) => left.Equals(right);
        public static bool operator !=(PhysicalDeviceHandle left, PhysicalDeviceHandle right) => !left.Equals(right);

        public static implicit operator IntPtr(PhysicalDeviceHandle physicalDevice) => physicalDevice.Handle;
        public static implicit operator PhysicalDeviceHandle(IntPtr handle) => new PhysicalDeviceHandle { Handle = handle };
    }

    public struct DeviceHandle : IEquatable<DeviceHandle>
    {
        public IntPtr Handle;

        public static readonly DeviceHandle Null;

        public bool Equals(DeviceHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DeviceHandle && this == (DeviceHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DeviceHandle left, DeviceHandle right) => left.Equals(right);
        public static bool operator !=(DeviceHandle left, DeviceHandle right) => !left.Equals(right);

        public static implicit operator IntPtr(DeviceHandle device) => device.Handle;
        public static implicit operator DeviceHandle(IntPtr handle) => new DeviceHandle { Handle = handle };
    }

    public struct QueueHandle : IEquatable<QueueHandle>
    {
        public IntPtr Handle;

        public static readonly QueueHandle Null;

        public bool Equals(QueueHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is QueueHandle && this == (QueueHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(QueueHandle left, QueueHandle right) => left.Equals(right);
        public static bool operator !=(QueueHandle left, QueueHandle right) => !left.Equals(right);

        public static implicit operator IntPtr(QueueHandle queue) => queue.Handle;
        public static implicit operator QueueHandle(IntPtr handle) => new QueueHandle { Handle = handle };
    }

    public struct CommandBufferHandle : IEquatable<CommandBufferHandle>
    {
        public IntPtr Handle;

        public static readonly CommandBufferHandle Null;

        public bool Equals(CommandBufferHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is CommandBufferHandle && this == (CommandBufferHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(CommandBufferHandle left, CommandBufferHandle right) => left.Equals(right);
        public static bool operator !=(CommandBufferHandle left, CommandBufferHandle right) => !left.Equals(right);

        public static implicit operator IntPtr(CommandBufferHandle commandBuffer) => commandBuffer.Handle;
        public static implicit operator CommandBufferHandle(IntPtr handle) => new CommandBufferHandle { Handle = handle };
    }

    //
    // Non dispatchable handles
    //
    public struct SemaphoreHandle : IEquatable<SemaphoreHandle>
    {
        public ulong Handle;

        public static readonly SemaphoreHandle Null;

        public bool Equals(SemaphoreHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is SemaphoreHandle && this == (SemaphoreHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(SemaphoreHandle left, SemaphoreHandle right) => left.Equals(right);
        public static bool operator !=(SemaphoreHandle left, SemaphoreHandle right) => !left.Equals(right);

        public static implicit operator ulong(SemaphoreHandle semaphore) => semaphore.Handle;
        public static implicit operator SemaphoreHandle(ulong handle) => new SemaphoreHandle { Handle = handle };
    }

    public struct FenceHandle : IEquatable<FenceHandle>
    {
        public ulong Handle;

        public static readonly FenceHandle Null;

        public bool Equals(FenceHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is FenceHandle && this == (FenceHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(FenceHandle left, FenceHandle right) => left.Equals(right);
        public static bool operator !=(FenceHandle left, FenceHandle right) => !left.Equals(right);

        public static implicit operator ulong(FenceHandle fence) => fence.Handle;
        public static implicit operator FenceHandle(ulong handle) => new FenceHandle { Handle = handle };
    }

    public struct DeviceMemoryHandle : IEquatable<DeviceMemoryHandle>
    {
        public ulong Handle;

        public static readonly DeviceMemoryHandle Null;

        public bool Equals(DeviceMemoryHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DeviceMemoryHandle && this == (DeviceMemoryHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DeviceMemoryHandle left, DeviceMemoryHandle right) => left.Equals(right);
        public static bool operator !=(DeviceMemoryHandle left, DeviceMemoryHandle right) => !left.Equals(right);

        public static implicit operator ulong(DeviceMemoryHandle deviceMemory) => deviceMemory.Handle;
        public static implicit operator DeviceMemoryHandle(ulong handle) => new DeviceMemoryHandle { Handle = handle };
    }

    public struct BufferHandle : IEquatable<BufferHandle>
    {
        public ulong Handle;

        public static readonly BufferHandle Null;

        public bool Equals(BufferHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is BufferHandle && this == (BufferHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(BufferHandle left, BufferHandle right) => left.Equals(right);
        public static bool operator !=(BufferHandle left, BufferHandle right) => !left.Equals(right);

        public static implicit operator ulong(BufferHandle buffer) => buffer.Handle;
        public static implicit operator BufferHandle(ulong handle) => new BufferHandle { Handle = handle };
    }

    public struct ImageHandle : IEquatable<ImageHandle>
    {
        public ulong Handle;

        public static readonly ImageHandle Null;

        public bool Equals(ImageHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is ImageHandle && this == (ImageHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(ImageHandle left, ImageHandle right) => left.Equals(right);
        public static bool operator !=(ImageHandle left, ImageHandle right) => !left.Equals(right);

        public static implicit operator ulong(ImageHandle image) => image.Handle;
        public static implicit operator ImageHandle(ulong handle) => new ImageHandle { Handle = handle };
    }

    public struct EventHandle : IEquatable<EventHandle>
    {
        public ulong Handle;

        public static readonly EventHandle Null;

        public bool Equals(EventHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is EventHandle && this == (EventHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(EventHandle left, EventHandle right) => left.Equals(right);
        public static bool operator !=(EventHandle left, EventHandle right) => !left.Equals(right);

        public static implicit operator ulong(EventHandle @event) => @event.Handle;
        public static implicit operator EventHandle(ulong handle) => new EventHandle { Handle = handle };
    }

    public struct QueryPoolHandle : IEquatable<QueryPoolHandle>
    {
        public ulong Handle;

        public static readonly QueryPoolHandle Null;

        public bool Equals(QueryPoolHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is QueryPoolHandle && this == (QueryPoolHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(QueryPoolHandle left, QueryPoolHandle right) => left.Equals(right);
        public static bool operator !=(QueryPoolHandle left, QueryPoolHandle right) => !left.Equals(right);

        public static implicit operator ulong(QueryPoolHandle queryPool) => queryPool.Handle;
        public static implicit operator QueryPoolHandle(ulong handle) => new QueryPoolHandle { Handle = handle };
    }

    public struct BufferViewHandle : IEquatable<BufferViewHandle>
    {
        public ulong Handle;

        public static readonly BufferViewHandle Null;

        public bool Equals(BufferViewHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is BufferViewHandle && this == (BufferViewHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(BufferViewHandle left, BufferViewHandle right) => left.Equals(right);
        public static bool operator !=(BufferViewHandle left, BufferViewHandle right) => !left.Equals(right);

        public static implicit operator ulong(BufferViewHandle bufferView) => bufferView.Handle;
        public static implicit operator BufferViewHandle(ulong handle) => new BufferViewHandle { Handle = handle };
    }

    public struct ImageViewHandle : IEquatable<ImageViewHandle>
    {
        public ulong Handle;

        public static readonly ImageViewHandle Null;

        public bool Equals(ImageViewHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is ImageViewHandle && this == (ImageViewHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(ImageViewHandle left, ImageViewHandle right) => left.Equals(right);
        public static bool operator !=(ImageViewHandle left, ImageViewHandle right) => !left.Equals(right);

        public static implicit operator ulong(ImageViewHandle imageView) => imageView.Handle;
        public static implicit operator ImageViewHandle(ulong handle) => new ImageViewHandle { Handle = handle };
    }

    public struct ShaderModuleHandle : IEquatable<ShaderModuleHandle>
    {
        public ulong Handle;

        public static readonly ShaderModuleHandle Null;

        public bool Equals(ShaderModuleHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is ShaderModuleHandle && this == (ShaderModuleHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(ShaderModuleHandle left, ShaderModuleHandle right) => left.Equals(right);
        public static bool operator !=(ShaderModuleHandle left, ShaderModuleHandle right) => !left.Equals(right);

        public static implicit operator ulong(ShaderModuleHandle shaderModule) => shaderModule.Handle;
        public static implicit operator ShaderModuleHandle(ulong handle) => new ShaderModuleHandle { Handle = handle };
    }

    public struct PipelineCacheHandle : IEquatable<PipelineCacheHandle>
    {
        public ulong Handle;

        public static readonly PipelineCacheHandle Null;

        public bool Equals(PipelineCacheHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is PipelineCacheHandle && this == (PipelineCacheHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(PipelineCacheHandle left, PipelineCacheHandle right) => left.Equals(right);
        public static bool operator !=(PipelineCacheHandle left, PipelineCacheHandle right) => !left.Equals(right);

        public static implicit operator ulong(PipelineCacheHandle pipelineCache) => pipelineCache.Handle;
        public static implicit operator PipelineCacheHandle(ulong handle) => new PipelineCacheHandle { Handle = handle };
    }

    public struct PipelineLayoutHandle : IEquatable<PipelineLayoutHandle>
    {
        public ulong Handle;

        public static readonly PipelineLayoutHandle Null;

        public bool Equals(PipelineLayoutHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is PipelineLayoutHandle && this == (PipelineLayoutHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(PipelineLayoutHandle left, PipelineLayoutHandle right) => left.Equals(right);
        public static bool operator !=(PipelineLayoutHandle left, PipelineLayoutHandle right) => !left.Equals(right);

        public static implicit operator ulong(PipelineLayoutHandle pipelineLayout) => pipelineLayout.Handle;
        public static implicit operator PipelineLayoutHandle(ulong handle) => new PipelineLayoutHandle { Handle = handle };
    }

    public struct RenderPassHandle : IEquatable<RenderPassHandle>
    {
        public ulong Handle;

        public static readonly RenderPassHandle Null;

        public bool Equals(RenderPassHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is RenderPassHandle && this == (RenderPassHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(RenderPassHandle left, RenderPassHandle right) => left.Equals(right);
        public static bool operator !=(RenderPassHandle left, RenderPassHandle right) => !left.Equals(right);

        public static implicit operator ulong(RenderPassHandle renderPass) => renderPass.Handle;
        public static implicit operator RenderPassHandle(ulong handle) => new RenderPassHandle { Handle = handle };
    }

    public struct PipelineHandle : IEquatable<PipelineHandle>
    {
        public ulong Handle;

        public static readonly PipelineHandle Null;

        public bool Equals(PipelineHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is PipelineHandle && this == (PipelineHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(PipelineHandle left, PipelineHandle right) => left.Equals(right);
        public static bool operator !=(PipelineHandle left, PipelineHandle right) => !left.Equals(right);

        public static implicit operator ulong(PipelineHandle pipeline) => pipeline.Handle;
        public static implicit operator PipelineHandle(ulong handle) => new PipelineHandle { Handle = handle };
    }

    public struct DescriptorSetLayoutHandle : IEquatable<DescriptorSetLayoutHandle>
    {
        public ulong Handle;

        public static readonly DescriptorSetLayoutHandle Null;

        public bool Equals(DescriptorSetLayoutHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorSetLayoutHandle && this == (DescriptorSetLayoutHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorSetLayoutHandle left, DescriptorSetLayoutHandle right) => left.Equals(right);
        public static bool operator !=(DescriptorSetLayoutHandle left, DescriptorSetLayoutHandle right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorSetLayoutHandle descriptorSetLayout) => descriptorSetLayout.Handle;
        public static implicit operator DescriptorSetLayoutHandle(ulong handle) => new DescriptorSetLayoutHandle { Handle = handle };
    }

    public struct SamplerHandle : IEquatable<SamplerHandle>
    {
        public ulong Handle;

        public static readonly SamplerHandle Null;

        public bool Equals(SamplerHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is SamplerHandle && this == (SamplerHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(SamplerHandle left, SamplerHandle right) => left.Equals(right);
        public static bool operator !=(SamplerHandle left, SamplerHandle right) => !left.Equals(right);

        public static implicit operator ulong(SamplerHandle sampler) => sampler.Handle;
        public static implicit operator SamplerHandle(ulong handle) => new SamplerHandle { Handle = handle };
    }

    public struct DescriptorPoolHandle : IEquatable<DescriptorPoolHandle>
    {
        public ulong Handle;

        public static readonly DescriptorPoolHandle Null;

        public bool Equals(DescriptorPoolHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorPoolHandle && this == (DescriptorPoolHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorPoolHandle left, DescriptorPoolHandle right) => left.Equals(right);
        public static bool operator !=(DescriptorPoolHandle left, DescriptorPoolHandle right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorPoolHandle descriptorPool) => descriptorPool.Handle;
        public static implicit operator DescriptorPoolHandle(ulong handle) => new DescriptorPoolHandle { Handle = handle };
    }

    public struct DescriptorSetHandle : IEquatable<DescriptorSetHandle>
    {
        public ulong Handle;

        public static readonly DescriptorSetHandle Null;

        public bool Equals(DescriptorSetHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorSetHandle && this == (DescriptorSetHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorSetHandle left, DescriptorSetHandle right) => left.Equals(right);
        public static bool operator !=(DescriptorSetHandle left, DescriptorSetHandle right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorSetHandle descriptorSet) => descriptorSet.Handle;
        public static implicit operator DescriptorSetHandle(ulong handle) => new DescriptorSetHandle { Handle = handle };
    }

    public struct FramebufferHandle : IEquatable<FramebufferHandle>
    {
        public ulong Handle;

        public static readonly FramebufferHandle Null;

        public bool Equals(FramebufferHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is FramebufferHandle && this == (FramebufferHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(FramebufferHandle left, FramebufferHandle right) => left.Equals(right);
        public static bool operator !=(FramebufferHandle left, FramebufferHandle right) => !left.Equals(right);

        public static implicit operator ulong(FramebufferHandle framebuffer) => framebuffer.Handle;
        public static implicit operator FramebufferHandle(ulong handle) => new FramebufferHandle { Handle = handle };
    }

    public struct CommandPoolHandle : IEquatable<CommandPoolHandle>
    {
        public ulong Handle;

        public static readonly CommandPoolHandle Null;

        public bool Equals(CommandPoolHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is CommandPoolHandle && this == (CommandPoolHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(CommandPoolHandle left, CommandPoolHandle right) => left.Equals(right);
        public static bool operator !=(CommandPoolHandle left, CommandPoolHandle right) => !left.Equals(right);

        public static implicit operator ulong(CommandPoolHandle commandPool) => commandPool.Handle;
        public static implicit operator CommandPoolHandle(ulong handle) => new CommandPoolHandle { Handle = handle };
    }

    // Khronos
    public struct SurfaceHandle : IEquatable<SurfaceHandle>
    {
        public ulong Handle;

        public static readonly SurfaceHandle Null;

        public bool Equals(SurfaceHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is SurfaceHandle && this == (SurfaceHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(SurfaceHandle left, SurfaceHandle right) => left.Equals(right);
        public static bool operator !=(SurfaceHandle left, SurfaceHandle right) => !left.Equals(right);

        public static implicit operator ulong(SurfaceHandle surface) => surface.Handle;
        public static implicit operator SurfaceHandle(ulong handle) => new SurfaceHandle { Handle = handle };
        public static implicit operator SDL.VkSurfaceKHR(SurfaceHandle surface) => new SDL.VkSurfaceKHR { Handle = surface.Handle };
        public static implicit operator SurfaceHandle(SDL.VkSurfaceKHR surface) => new SurfaceHandle { Handle = surface.Handle };
    }

    public struct SwapchainHandle : IEquatable<SwapchainHandle>
    {
        public ulong Handle;

        public static readonly SwapchainHandle Null;

        public bool Equals(SwapchainHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is SwapchainHandle && this == (SwapchainHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(SwapchainHandle left, SwapchainHandle right) => left.Equals(right);
        public static bool operator !=(SwapchainHandle left, SwapchainHandle right) => !left.Equals(right);

        public static implicit operator ulong(SwapchainHandle swapchain) => swapchain.Handle;
        public static implicit operator SwapchainHandle(ulong handle) => new SwapchainHandle { Handle = handle };
    }

    public struct DisplayHandle : IEquatable<DisplayHandle>
    {
        public ulong Handle;

        public static readonly DisplayHandle Null;

        public bool Equals(DisplayHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DisplayHandle && this == (DisplayHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DisplayHandle left, DisplayHandle right) => left.Equals(right);
        public static bool operator !=(DisplayHandle left, DisplayHandle right) => !left.Equals(right);

        public static implicit operator ulong(DisplayHandle display) => display.Handle;
        public static implicit operator DisplayHandle(ulong handle) => new DisplayHandle { Handle = handle };
    }

    public struct DisplayModeHandle : IEquatable<DisplayModeHandle>
    {
        public ulong Handle;

        public static readonly DisplayModeHandle Null;

        public bool Equals(DisplayModeHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DisplayModeHandle && this == (DisplayModeHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DisplayModeHandle left, DisplayModeHandle right) => left.Equals(right);
        public static bool operator !=(DisplayModeHandle left, DisplayModeHandle right) => !left.Equals(right);

        public static implicit operator ulong(DisplayModeHandle displayMode) => displayMode.Handle;
        public static implicit operator DisplayModeHandle(ulong handle) => new DisplayModeHandle { Handle = handle };
    }

    public struct DescriptorUpdateTemplateHandle : IEquatable<DescriptorUpdateTemplateHandle>
    {
        public ulong Handle;

        public static readonly DescriptorUpdateTemplateHandle Null;

        public bool Equals(DescriptorUpdateTemplateHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorUpdateTemplateHandle && this == (DescriptorUpdateTemplateHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorUpdateTemplateHandle left, DescriptorUpdateTemplateHandle right) => left.Equals(right);
        public static bool operator !=(DescriptorUpdateTemplateHandle left, DescriptorUpdateTemplateHandle right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorUpdateTemplateHandle descriptorUpdateTemplate) => descriptorUpdateTemplate.Handle;
        public static implicit operator DescriptorUpdateTemplateHandle(ulong handle) => new DescriptorUpdateTemplateHandle { Handle = handle };
    }

    // Multi-vendor
    public struct DebugReportCallbackHandle : IEquatable<DebugReportCallbackHandle>
    {
        public ulong Handle;

        public static readonly DebugReportCallbackHandle Null;

        public bool Equals(DebugReportCallbackHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DebugReportCallbackHandle && this == (DebugReportCallbackHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DebugReportCallbackHandle left, DebugReportCallbackHandle right) => left.Equals(right);
        public static bool operator !=(DebugReportCallbackHandle left, DebugReportCallbackHandle right) => !left.Equals(right);

        public static implicit operator ulong(DebugReportCallbackHandle debugReportCallback) => debugReportCallback.Handle;
        public static implicit operator DebugReportCallbackHandle(ulong handle) => new DebugReportCallbackHandle { Handle = handle };
    }

    // Nvidia
    public struct ObjectTableHandle : IEquatable<ObjectTableHandle>
    {
        public ulong Handle;

        public static readonly ObjectTableHandle Null;

        public bool Equals(ObjectTableHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is ObjectTableHandle && this == (ObjectTableHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(ObjectTableHandle left, ObjectTableHandle right) => left.Equals(right);
        public static bool operator !=(ObjectTableHandle left, ObjectTableHandle right) => !left.Equals(right);

        public static implicit operator ulong(ObjectTableHandle objectTable) => objectTable.Handle;
        public static implicit operator ObjectTableHandle(ulong handle) => new ObjectTableHandle { Handle = handle };
    }

    public struct IndirectCommandsLayoutHandle : IEquatable<IndirectCommandsLayoutHandle>
    {
        public ulong Handle;

        public static readonly IndirectCommandsLayoutHandle Null;

        public bool Equals(IndirectCommandsLayoutHandle other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is IndirectCommandsLayoutHandle && this == (IndirectCommandsLayoutHandle)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(IndirectCommandsLayoutHandle left, IndirectCommandsLayoutHandle right) => left.Equals(right);
        public static bool operator !=(IndirectCommandsLayoutHandle left, IndirectCommandsLayoutHandle right) => !left.Equals(right);

        public static implicit operator ulong(IndirectCommandsLayoutHandle indirectCommandsLayout) => indirectCommandsLayout.Handle;
        public static implicit operator IndirectCommandsLayoutHandle(ulong handle) => new IndirectCommandsLayoutHandle { Handle = handle };
    }
    #endregion

    #region Structs
    public struct Size
    {
#pragma warning disable 649
        private ulong _value;
#pragma warning restore 649

        public static implicit operator ulong(Size size) => size._value;
    }

    public struct Version : IEquatable<Version>, IComparable<Version>
    {
        private uint _value;

        public uint Major => _value >> 22;

        public uint Minor => (_value >> 12) & 0x03FF;

        public uint Patch => _value & 0x0FFF;

        public static Version Zero => new Version(0, 0, 0);
        public static Version One => new Version(1, 0, 0);

        public Version(uint major, uint Minor, uint patch) => _value = major << 22 | Minor << 12 | patch;

        public bool Equals(Version other) => _value == other._value;

        public int CompareTo(Version other) => _value.CompareTo(other._value);

        public static implicit operator uint(Version version) => version._value;

        public override string ToString() => $"{Major}.{Minor}.{Patch}";
    }

    public struct Bool32 : IEquatable<Bool32>
    {
        private readonly int _value;

        public Bool32(bool value) => _value = value ? 1 : 0;

        public Bool32(int value) => _value = value;

        public bool Equals(Bool32 other) => _value == other._value;

        public override bool Equals(object obj) => obj is Bool32 && Equals((Bool32)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Bool32 left, Bool32 right) => left.Equals(right);

        public static bool operator !=(Bool32 left, Bool32 right) => !left.Equals(right);

        public static implicit operator bool(Bool32 value) => value._value != 0;

        public static implicit operator Bool32(bool value) => new Bool32(value);

        public static implicit operator int(Bool32 value) => value._value;

        public static implicit operator Bool32(int value) => new Bool32(value);

        public override string ToString() => ((bool)this).ToString();
    }

    public struct DeviceSize : IEquatable<DeviceSize>
    {
        private readonly ulong _value;

        public DeviceSize(ulong value) => _value = value;

        public bool Equals(DeviceSize other) => _value == other._value;

        public override bool Equals(object obj) => obj is DeviceSize && Equals((DeviceSize)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(DeviceSize left, DeviceSize right) => left.Equals(right);

        public static bool operator !=(DeviceSize left, DeviceSize right) => !left.Equals(right);

        public override string ToString() => _value.ToString();

        public static implicit operator ulong(DeviceSize DeviceSize) => DeviceSize._value;

        public static implicit operator DeviceSize(ulong value) => new DeviceSize(value);
    }

    public struct SampleMask : IEquatable<SampleMask>
    {
        private uint _value;

        public SampleMask(uint value) => _value = value;

        public bool Equals(SampleMask other) => _value == other._value;

        public override bool Equals(object obj) => obj is SampleMask && Equals((SampleMask)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(SampleMask left, SampleMask right) => left.Equals(right);

        public static bool operator !=(SampleMask left, SampleMask right) => !left.Equals(right);

        public override string ToString() => _value.ToString();

        public static implicit operator uint(SampleMask SampleMask) => SampleMask._value;

        public static implicit operator SampleMask(uint value) => new SampleMask(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ApplicationInfo
    {
        public StructureType Type;
        public void* Next;
        public byte* ApplicationName;
        public Version ApplicationVersion;
        public byte* EngineName;
        public Version EngineVersion;
        public Version ApiVersion;

        public ApplicationInfo(byte* applicationName, Version applicationVersion, byte* engineName, Version engineVersion, Version apiVersion)
        {
            Type = StructureType.ApplicationInfo;
            Next = null;
            ApplicationName = applicationName;
            ApplicationVersion = applicationVersion;
            EngineName = engineName;
            EngineVersion = engineVersion;
            ApiVersion = apiVersion;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct InstanceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public InstanceCreateFlags Flags;
        public ApplicationInfo* ApplicationInfo;
        public uint EnabledLayerCount;
        public byte** EnabledLayerNames;
        public uint EnabledExtensionCount;
        public byte** EnabledExtensionNames;

        public InstanceCreateInfo(ApplicationInfo* applicationInfo, uint extensionCount, byte** extensionNames)
        {
            Type = StructureType.InstanceCreateInfo;
            Next = null;
            Flags = InstanceCreateFlags.None;
            ApplicationInfo = applicationInfo;
            EnabledLayerCount = 0;
            EnabledLayerNames = null;
            EnabledExtensionCount = extensionCount;
            EnabledExtensionNames = extensionNames;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AllocationCallbacks
    {
        public IntPtr UserData;
        public IntPtr Allocation; // AllocationFunction
        public IntPtr Reallocation; // ReallocationFunction
        public IntPtr Free; // FreeFunction
        public IntPtr InternalAllocation; // InternalAllocationNotification
        public IntPtr InternalFree; // InternalFreeNotification
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceFeatures
    {
        public Bool32 RobustBufferAccess;
        public Bool32 FullDrawIndexUint32;
        public Bool32 ImageCubeArray;
        public Bool32 IndependentBlend;
        public Bool32 GeometryShader;
        public Bool32 TessellationShader;
        public Bool32 SampleRateShading;
        public Bool32 DualSrcBlend;
        public Bool32 LogicOp;
        public Bool32 MultiDrawIndirect;
        public Bool32 DrawIndirectFirstInstance;
        public Bool32 DepthClamp;
        public Bool32 DepthBiasClamp;
        public Bool32 FillModeNonSolid;
        public Bool32 DepthBounds;
        public Bool32 WideLines;
        public Bool32 LargePoints;
        public Bool32 AlphaToOne;
        public Bool32 MultiViewport;
        public Bool32 SamplerAnisotropy;
        public Bool32 TextureCompressionETC2;
        public Bool32 TextureCompressionASTC_LDR;
        public Bool32 TextureCompressionBC;
        public Bool32 OcclusionQueryPrecise;
        public Bool32 PipelineStatisticsQuery;
        public Bool32 VertexPipelineStoresAndAtomics;
        public Bool32 FragmentStoresAndAtomics;
        public Bool32 ShaderTessellationAndGeometryPointSize;
        public Bool32 ShaderImageGatherExtended;
        public Bool32 ShaderStorageImageExtendedFormats;
        public Bool32 ShaderStorageImageMultisample;
        public Bool32 ShaderStorageImageReadWithoutFormat;
        public Bool32 ShaderStorageImageWriteWithoutFormat;
        public Bool32 ShaderUniformBufferArrayDynamicIndexing;
        public Bool32 ShaderSampledImageArrayDynamicIndexing;
        public Bool32 ShaderStorageBufferArrayDynamicIndexing;
        public Bool32 ShaderStorageImageArrayDynamicIndexing;
        public Bool32 ShaderClipDistance;
        public Bool32 ShaderCullDistance;
        public Bool32 ShaderFloat64;
        public Bool32 ShaderInt64;
        public Bool32 ShaderInt16;
        public Bool32 ShaderResourceResidency;
        public Bool32 ShaderResourceMinLod;
        public Bool32 SparseBinding;
        public Bool32 SparseResidencyBuffer;
        public Bool32 SparseResidencyImage2D;
        public Bool32 SparseResidencyImage3D;
        public Bool32 SparseResidency2Samples;
        public Bool32 SparseResidency4Samples;
        public Bool32 SparseResidency8Samples;
        public Bool32 SparseResidency16Samples;
        public Bool32 SparseResidencyAliased;
        public Bool32 VariableMultisampleRate;
        public Bool32 InheritedQueries;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FormatProperties
    {
        public FormatFeatureFlags LinearTilingFeatures;
        public FormatFeatureFlags OptimalTilingFeatures;
        public FormatFeatureFlags BufferFeatures;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Extent3D
    {
        public uint Width;
        public uint Height;
        public uint Depth;

        public Extent3D(int width, int height, int depth)
        {
            Width = (uint)width;
            Height = (uint)height;
            Depth = (uint)depth;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageFormatProperties
    {
        public Extent3D MaxExtents;
        public uint MaxMipLevels;
        public uint MaxArrayLayers;
        public SampleCountFlags SampleCounts;
        public DeviceSize MaxResourceSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceLimits
    {
        public uint MaxImageDimension1D;
        public uint MaxImageDimension2D;
        public uint MaxImageDimension3D;
        public uint MaxImageDimensionCube;
        public uint MaxImageArrayLayers;
        public uint MaxTexelBufferElements;
        public uint MaxUniformBufferRange;
        public uint MaxStorageBufferRange;
        public uint MaxPushantsSize;
        public uint MaxMemoryAllocationCount;
        public uint MaxSamplerAllocationCount;
        public DeviceSize BufferImageGranularity;
        public DeviceSize SparseAddressSpaceSize;
        public uint MaxBoundDescriptorSets;
        public uint MaxPerStageDescriptorSamplers;
        public uint MaxPerStageDescriptorUniformBuffers;
        public uint MaxPerStageDescriptorStorageBuffers;
        public uint MaxPerStageDescriptorSampledImages;
        public uint MaxPerStageDescriptorStorageImages;
        public uint MaxPerStageDescriptorInputAttachments;
        public uint MaxPerStageResources;
        public uint MaxDescriptorSetSamplers;
        public uint MaxDescriptorSetUniformBuffers;
        public uint MaxDescriptorSetUniformBuffersDynamic;
        public uint MaxDescriptorSetStorageBuffers;
        public uint MaxDescriptorSetStorageBuffersDynamic;
        public uint MaxDescriptorSetSampledImages;
        public uint MaxDescriptorSetStorageImages;
        public uint MaxDescriptorSetInputAttachments;
        public uint MaxVertexInputAttributes;
        public uint MaxVertexInputBindings;
        public uint MaxVertexInputAttributeOffset;
        public uint MaxVertexInputBindingStride;
        public uint MaxVertexOutputComponents;
        public uint MaxTessellationGenerationLevel;
        public uint MaxTessellationPatchSize;
        public uint MaxTessellationControlPerVertexInputComponents;
        public uint MaxTessellationControlPerVertexOutputComponents;
        public uint MaxTessellationControlPerPatchOutputComponents;
        public uint MaxTessellationControlTotalOutputComponents;
        public uint MaxTessellationEvaluationInputComponents;
        public uint MaxTessellationEvaluationOutputComponents;
        public uint MaxGeometryShaderInvocations;
        public uint MaxGeometryInputComponents;
        public uint MaxGeometryOutputComponents;
        public uint MaxGeometryOutputVertices;
        public uint MaxGeometryTotalOutputComponents;
        public uint MaxFragmentInputComponents;
        public uint MaxFragmentOutputAttachments;
        public uint MaxFragmentDualSrcAttachments;
        public uint MaxFragmentCombinedOutputResources;
        public uint MaxComputeSharedMemorySize;
        public fixed uint MaxComputeWorkGroupCount[3];
        public uint MaxComputeWorkGroupInvocations;
        public fixed uint MaxComputeWorkGroupSize[3];
        public uint SubPixelPrecisionBits;
        public uint SubTexelPrecisionBits;
        public uint MipmapPrecisionBits;
        public uint MaxDrawIndexedIndexValue;
        public uint MaxDrawIndirectCount;
        public float MaxSamplerLodBias;
        public float MaxSamplerAnisotropy;
        public uint MaxViewports;
        public fixed uint MaxViewportDimensions[2];
        public fixed float ViewportBoundsRange[2];
        public uint ViewportSubPixelBits;
        public ulong MinMemoryMapAlignment;
        public DeviceSize MinTexelBufferOffsetAlignment;
        public DeviceSize MinUniformBufferOffsetAlignment;
        public DeviceSize MinStorageBufferOffsetAlignment;
        public int MinTexelOffset;
        public uint MaxTexelOffset;
        public int MinTexelGatherOffset;
        public uint MaxTexelGatherOffset;
        public float MinInterpolationOffset;
        public float MaxInterpolationOffset;
        public uint SubPixelInterpolationOffsetBits;
        public uint MaxFramebufferWidth;
        public uint MaxFramebufferHeight;
        public uint MaxFramebufferLayers;
        public SampleCountFlags FramebufferColorSampleCounts;
        public SampleCountFlags FramebufferDepthSampleCounts;
        public SampleCountFlags FramebufferStencilSampleCounts;
        public SampleCountFlags FramebufferNoAttachmentsSampleCounts;
        public uint MaxColorAttachments;
        public SampleCountFlags SampledImageColorSampleCounts;
        public SampleCountFlags SampledImageIntegerSampleCounts;
        public SampleCountFlags SampledImageDepthSampleCounts;
        public SampleCountFlags SampledImageStencilSampleCounts;
        public SampleCountFlags StorageImageSampleCounts;
        public uint MaxSampleMaskWords;
        public Bool32 TimestampComputeAndGraphics;
        public float TimestampPeriod;
        public uint MaxClipDistances;
        public uint MaxCullDistances;
        public uint MaxCombinedClipAndCullDistances;
        public uint DiscreteQueuePriorities;
        public fixed float PointSizeRange[2];
        public fixed float LineWidthRange[2];
        public float PointSizeGranularity;
        public float LineWidthGranularity;
        public Bool32 StrictLines;
        public Bool32 StandardSampleLocations;
        public DeviceSize OptimalBufferCopyOffsetAlignment;
        public DeviceSize OptimalBufferCopyRowPitchAlignment;
        public DeviceSize NonCoherentAtomSize;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceSparseProperties
    {
        public Bool32 ResidencyStandard2DBlockShape;
        public Bool32 ResidencyStandard2DMultisampleBlockShape;
        public Bool32 ResidencyStandard3DBlockShape;
        public Bool32 ResidencyAlignedMipSize;
        public Bool32 ResidencyNonResidentStrict;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceProperties
    {
        public uint ApiVersion;
        public uint DriverVersion;
        public uint VendorId;
        public uint DeviceId;
        public PhysicalDeviceType DeviceType;
        public fixed byte DeviceName[Vulkan.MaxPhysicalDeviceNameSize];
        public fixed byte PipelineCacheUuid[Vulkan.UUIDSize];
        public PhysicalDeviceLimits Limits;
        public PhysicalDeviceSparseProperties SparseProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct QueueFamilyProperties
    {
        public QueueFlags QueueFlags;
        public uint QueueCount;
        public uint TimestampValidBits;
        public Extent3D MinImageTransferGranularity;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryType
    {
        public MemoryPropertyFlags PropertyFlags;
        public uint HeapIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryHeap
    {
        public DeviceSize Size;
        public MemoryHeapFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceMemoryProperties
    {
        public uint MemoryTypeCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Vulkan.MaxMemoryTypes)]
        public MemoryType[] MemoryTypes;
        public uint MemoryHeapCount;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Vulkan.MaxMemoryHeaps)]
        public MemoryHeap[] MemoryHeaps;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceQueueCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceQueueCreateFlags Flags;
        public uint QueueFamilyIndex;
        public uint QueueCount;
        public float* QueuePriorities;

        public DeviceQueueCreateInfo(uint queueFamilyIndex, uint queueCount, float* queuePriorities)
        {
            Type = StructureType.DeviceQueueCreateInfo;
            Next = null;
            Flags = DeviceQueueCreateFlags.None;
            QueueFamilyIndex = queueFamilyIndex;
            QueueCount = queueCount;
            QueuePriorities = queuePriorities;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceCreateFlags Flags;
        public uint QueueCreateInfoCount;
        public DeviceQueueCreateInfo* QueueCreateInfos;
        public uint EnabledLayerCount;
        public byte** EnabledLayerNames;
        public uint EnabledExtensionCount;
        public byte** EnabledExtensionNames;
        public PhysicalDeviceFeatures* EnabledFeatures;

        public DeviceCreateInfo(DeviceQueueCreateInfo queueCreateInfo, uint enabledExtensionCount, byte** enabledExtensionNames, PhysicalDeviceFeatures enabledFeatures)
        {
            Type = StructureType.DeviceCreateInfo;
            Next = null;
            Flags = DeviceCreateFlags.None;
            QueueCreateInfoCount = 1;
            QueueCreateInfos = &queueCreateInfo;
            EnabledLayerCount = 0;
            EnabledLayerNames = null;
            EnabledExtensionCount = enabledExtensionCount;
            EnabledExtensionNames = enabledExtensionNames;
            EnabledFeatures = &enabledFeatures;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExtensionProperties
    {
        public fixed byte ExtensionName[Vulkan.MaxExtensionNameSize];
        public Version SpecVersion;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct LayerProperties
    {
        public fixed byte LayerName[Vulkan.MaxExtensionNameSize];
        public Version SpecVersion;
        public Version ImplementationVersion;
        public fixed byte Description[Vulkan.MaxDescriptionSize];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SubmitInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public SemaphoreHandle* WaitSemaphores;
        public PipelineStageFlags* WaitDstStageMask;
        public uint CommandBufferCount;
        public CommandBufferHandle* CommandBuffers;
        public uint SignalSemaphoreCount;
        public SemaphoreHandle* SignalSemaphores;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceSize AllocationSize;
        public uint MemoryTypeIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MappedMemoryRange
    {
        public StructureType Type;
        public void* Next;
        public DeviceMemoryHandle Memory;
        public DeviceSize Offset;
        public DeviceSize Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryRequirements
    {
        public DeviceSize Size;
        public DeviceSize Alignment;
        public uint MemoryTypeBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseImageFormatProperties
    {
        public ImageAspectFlags AspectMask;
        public Extent3D ImageGranularity;
        public SparseImageFormatFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseImageMemoryRequirements
    {
        public SparseImageFormatProperties FormatProperties;
        public uint ImageMipTailFirstLod;
        public DeviceSize ImageMipTailSize;
        public DeviceSize ImageMipTailOffset;
        public DeviceSize ImageMipTailStride;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseMemoryBind
    {
        public DeviceSize ResourceOffset;
        public DeviceSize Size;
        public DeviceMemoryHandle Memory;
        public DeviceSize MemoryOffset;
        public SparseMemoryBindFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseBufferMemoryBindInfo
    {
        public BufferHandle Buffer;
        public uint BindCount;
        public SparseMemoryBind* Binds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseImageOpaqueMemoryBindInfo
    {
        public ImageHandle Image;
        public uint BindCount;
        public SparseMemoryBind* Binds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageSubresource
    {
        public ImageAspectFlags AspectMask;
        public uint MipLevel;
        public uint ArrayLayer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Offset3D
    {
        public int X;
        public int Y;
        public int Z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseImageMemoryBind
    {
        public ImageSubresource Subresource;
        public Offset3D Offset;
        public Extent3D Extent;
        public DeviceMemoryHandle Memory;
        public DeviceSize MemoryOffset;
        public SparseMemoryBindFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseImageMemoryBindInfo
    {
        public ImageHandle Image;
        public uint BindCount;
        public SparseImageMemoryBind* Binds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BindSparseInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public SemaphoreHandle* WaitSemaphores;
        public uint BufferBindCount;
        public SparseBufferMemoryBindInfo* BufferBinds;
        public uint ImageOpaqueBindCount;
        public SparseImageOpaqueMemoryBindInfo* ImageOpaqueBinds;
        public uint ImageBindCount;
        public SparseImageMemoryBindInfo* ImageBinds;
        public uint SignalSemaphoreCount;
        public SemaphoreHandle* SignalSemaphores;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FenceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public FenceCreateFlags Flags;

        public FenceCreateInfo(FenceCreateFlags flags)
        {
            Type = StructureType.FenceCreateInfo;
            Next = null;
            Flags = flags;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SemaphoreCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SemaphoreCreateFlags Flags;

        public SemaphoreCreateInfo(SemaphoreCreateFlags flags = SemaphoreCreateFlags.None)
        {
            Type = StructureType.SemaphoreCreateInfo;
            Next = null;
            Flags = flags;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct EventCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public EventCreateFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct QueryPoolCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public QueryPoolCreateFlags Flags;
        public QueryType QueryType;
        public uint QueryCount;
        public QueryPipelineStatisticFlags PipelineStatistics;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BufferCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public BufferCreateFlags Flags;
        public DeviceSize Size;
        public BufferUsageFlags Usage;
        public SharingMode SharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BufferViewCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public BufferViewCreateFlags Flags;
        public BufferHandle Buffer;
        public Format Format;
        public DeviceSize Offset;
        public DeviceSize Range;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ImageCreateFlags Flags;
        public ImageType ImageType;
        public Format Format;
        public Extent3D Extent;
        public uint MipLevels;
        public uint ArrayLayers;
        public SampleCountFlags Samples;
        public ImageTiling Tiling;
        public ImageUsageFlags Usage;
        public SharingMode SharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
        public ImageLayout InitialLayout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SubresourceLayout
    {
        public DeviceSize Offset;
        public DeviceSize Size;
        public DeviceSize RowPitch;
        public DeviceSize ArrayPitch;
        public DeviceSize DepthPitch;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ComponentMapping
    {
        public ComponentSwizzle R;
        public ComponentSwizzle G;
        public ComponentSwizzle B;
        public ComponentSwizzle A;

        public ComponentMapping(ComponentSwizzle r, ComponentSwizzle g, ComponentSwizzle b, ComponentSwizzle a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageSubresourceRange
    {
        public ImageAspectFlags AspectMask;
        public uint BaseMipLevel;
        public uint LevelCount;
        public uint BaseArrayLayer;
        public uint LayerCount;

        public ImageSubresourceRange(ImageAspectFlags aspectMask, uint baseMipLevel, uint levelCount, uint baseArrayLayer, uint layerCount)
        {
            AspectMask = aspectMask;
            BaseMipLevel = baseMipLevel;
            LevelCount = levelCount;
            BaseArrayLayer = baseArrayLayer;
            LayerCount = layerCount;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageViewCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ImageViewCreateFlags Flags;
        public ImageHandle Image;
        public ImageViewType ViewType;
        public Format Format;
        public ComponentMapping Components;
        public ImageSubresourceRange SubresourceRange;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ShaderModuleCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ShaderModuleCreateFlags Flags;
        public Size CodeSize;
        public uint* Code;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineCacheCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCacheCreateFlags Flags;
        public Size InitialDataSize;
        public void* InitialData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpecializationMapEntry
    {
        public uint antId;
        public uint Offset;
        public Size Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpecializationInfo
    {
        public uint MapEntryCount;
        public SpecializationMapEntry* MapEntries;
        public Size DataSize;
        public void* Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineShaderStageCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineShaderStageCreateFlags Flags;
        public ShaderStageFlags Stage;
        public ShaderModuleHandle Module;
        public byte* Name;
        public SpecializationInfo* SpecializationInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VertexInputBindingDescription
    {
        public uint Binding;
        public uint Stride;
        public VertexInputRate InputRate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct VertexInputAttributeDescription
    {
        public uint Location;
        public uint Binding;
        public Format Format;
        public uint Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineVertexInputStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineVertexInputStateCreateFlags Flags;
        public uint VertexBindingDescriptionCount;
        public VertexInputBindingDescription* VertexInputBindingDescriptions;
        public uint VertexAttributeDescriptionCount;
        public VertexInputAttributeDescription* VertexAttributeDescriptions;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineInputAssemblyStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineInputAssemblyStateCreateFlags Flags;
        public PrimitiveTopology Topology;
        public Bool32 PrimitiveRestartEnable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineTessellationStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineTessellationStateCreateFlags Flags;
        public uint PatchControlPoints;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Viewport
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public float MinDepth;
        public float MaxDepth;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Offset2D
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Extent2D
    {
        public uint Width;
        public uint Height;

        public Extent2D(int width, int height)
        {
            Width = (uint)width;
            Height = (uint)height;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Rect2D
    {
        public Offset2D Offset;
        public Extent2D Extent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineViewportStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineViewportStateCreateFlags Flags;
        public uint ViewportCount;
        public Viewport* Viewports;
        public uint ScissorCount;
        public Rect2D* Scissors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineRasterizationStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineRasterizationStateCreateFlags Flags;
        public Bool32 DepthClampEnable;
        public Bool32 RasterizerDiscardEnable;
        public PolygonMode PolygonMode;
        public CullModeFlags CullMode;
        public FrontFace FrontFace;
        public Bool32 DepthBiasEnable;
        public float DepthBiasantFactor;
        public float DepthBiasClamp;
        public float DepthBiasSlopeFactor;
        public float LineWidth;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineMultisampleStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineMultisampleStateCreateFlags Flags;
        public SampleCountFlags RasterizationSamples;
        public Bool32 SampleShadingEnable;
        public float MinSampleShading;
        public SampleMask* SampleMask;
        public Bool32 AlphaToCoverageEnable;
        public Bool32 AlphaToOneEnable;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StencilOpState
    {
        public StencilOp FailOp;
        public StencilOp PassOp;
        public StencilOp DepthFailOp;
        public CompareOp CompareOp;
        public uint CompareMask;
        public uint WriteMask;
        public uint Reference;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineDepthStencilStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineDepthStencilStateCreateFlags Flags;
        public Bool32 DepthTestEnable;
        public Bool32 DepthWriteEnable;
        public CompareOp DepthCompareOp;
        public Bool32 DepthBoundsTestEnable;
        public Bool32 StencilTestEnable;
        public StencilOpState Front;
        public StencilOpState Back;
        public float MinDepthBounds;
        public float MaxDepthBounds;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineColorBlendAttachmentState
    {
        public Bool32 BlendEnable;
        public BlendFactor SrcColorBlendFactor;
        public BlendFactor DstColorBlendFactor;
        public BlendOp ColorBlendOp;
        public BlendFactor SrcAlphaBlendFactor;
        public BlendFactor DstAlphaBlendFactor;
        public BlendOp AlphaBlendOp;
        public ColorComponentFlags ColorWriteMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineColorBlendStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineColorBlendStateCreateFlags Flags;
        public Bool32 LogicOpEnable;
        public LogicOp LogicOp;
        public uint AttachmentCount;
        public PipelineColorBlendAttachmentState* Attachments;
        public fixed float Blendants[4];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineDynamicStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineDynamicStateCreateFlags Flags;
        public uint DynamicStateCount;
        public DynamicState* DynamicStates;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct GraphicsPipelineCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCreateFlags Flags;
        public uint StageCount;
        public PipelineShaderStageCreateInfo* Stages;
        public PipelineVertexInputStateCreateInfo* VertexInputState;
        public PipelineInputAssemblyStateCreateInfo* InputAssemblyState;
        public PipelineTessellationStateCreateInfo* TessellationState;
        public PipelineViewportStateCreateInfo* ViewportState;
        public PipelineRasterizationStateCreateInfo* RasterizationState;
        public PipelineMultisampleStateCreateInfo* MultisampleState;
        public PipelineDepthStencilStateCreateInfo* DepthStencilState;
        public PipelineColorBlendStateCreateInfo* ColorBlendState;
        public PipelineDynamicStateCreateInfo* DynamicState;
        public PipelineLayoutHandle Layout;
        public RenderPassHandle RenderPass;
        public uint Subpass;
        public PipelineHandle BasePipelineHandle;
        public int BasePipelineIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ComputePipelineCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCreateFlags Flags;
        public PipelineShaderStageCreateInfo Stage;
        public PipelineLayoutHandle Layout;
        public PipelineHandle BasePipelineHandle;
        public int BasePipelineIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PushantRange
    {
        public ShaderStageFlags StageFlags;
        public uint Offset;
        public uint Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineLayoutCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineLayoutCreateFlags Flags;
        public uint SetLayoutCount;
        public DescriptorSetLayoutHandle* SetLayouts;
        public uint PushantRangeCount;
        public PushantRange* PushantRanges;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SamplerCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SampleCountFlags Flags;
        public Filter MagFilter;
        public Filter MinFilter;
        public SamplerMipmapMode MipmapMode;
        public SamplerAddressMode AddressModeU;
        public SamplerAddressMode AddressModeV;
        public SamplerAddressMode AddressModeW;
        public float MipLodBias;
        public Bool32 AnisotropyEnable;
        public float MaxAnisotropy;
        public Bool32 CompareEnable;
        public CompareOp CompareOp;
        public float MinLod;
        public float MaxLod;
        public BorderColor BorderColor;
        public Bool32 UnnormalizedCoordinates;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorSetLayoutBinding
    {
        public uint Binding;
        public DescriptorType DescriptorType;
        public uint DescriptorCount;
        public ShaderStageFlags StageFlags;
        public SamplerHandle* ImmutableSamplers;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorSetLayoutCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorSetLayoutCreateFlags Flags;
        public uint BindingCount;
        public DescriptorSetLayoutBinding* Bindings;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorPoolSize
    {
        public DescriptorType Type;
        public uint DescriptorCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorPoolCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorPoolCreateFlags Flags;
        public uint MaxSets;
        public uint PoolSizeCount;
        public DescriptorPoolSize* PoolSizes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorSetAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorPoolHandle DescriptorPool;
        public uint DescriptorSetCount;
        public DescriptorSetLayoutHandle* SetLayouts;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorImageInfo
    {
        public SamplerHandle Sampler;
        public ImageViewHandle ImageView;
        public ImageLayout ImageLayout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorBufferInfo
    {
        public BufferHandle Buffer;
        public DeviceSize Offset;
        public DeviceSize Range;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct WriteDescriptorSet
    {
        public StructureType Type;
        public void* Next;
        public DescriptorSetHandle DstSet;
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
        public DescriptorType DescriptorType;
        public DescriptorImageInfo* ImageInfo;
        public DescriptorBufferInfo* BufferInfo;
        public BufferViewHandle* TexelBufferView;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CopyDescriptorSet
    {
        public StructureType Type;
        public void* Next;
        public DescriptorSetHandle SrcSet;
        public uint SrcBinding;
        public uint SrcArrayElement;
        public DescriptorSetHandle DstSet;
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FramebufferCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public FramebufferCreateFlags Flags;
        public RenderPassHandle RenderPass;
        public uint AttachmentCount;
        public ImageViewHandle* Attachments;
        public uint Width;
        public uint Height;
        public uint Layers;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AttachmentDescription
    {
        public AttachmentDescriptionFlags Flags;
        public Format Format;
        public SampleCountFlags Samples;
        public AttachmentLoadOp LoadOp;
        public AttachmentStoreOp StoreOp;
        public AttachmentLoadOp StencilLoadOp;
        public AttachmentStoreOp StencilStoreOp;
        public ImageLayout InitialLayout;
        public ImageLayout FinalLayout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AttachmentReference
    {
        public uint Attachment;
        public ImageLayout Layout;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SubpassDescription
    {
        public SubpassDescriptionFlags Flags;
        public PipelineBindPoint PipelineBindPoint;
        public uint InputAttachmentCount;
        public AttachmentReference* InputAttachments;
        public uint ColorAttachmentCount;
        public AttachmentReference* ColorAttachments;
        public AttachmentReference* ResolveAttachments;
        public AttachmentReference* DepthStencilAttachments;
        public uint PreserveAttachmentCount;
        public uint* PreserveAttachments;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SubpassDependency
    {
        public uint SrcSubpass;
        public uint DstSubpass;
        public PipelineStageFlags SrcStageMask;
        public PipelineStageFlags DstStageMask;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
        public DependencyFlags DependencyFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RenderPassCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public RenderPassCreateFlags Flags;
        public uint AttachmentCount;
        public AttachmentDescription* Attachments;
        public uint SubpassCount;
        public SubpassDescription* Subpasses;
        public uint DependencyCount;
        public SubpassDependency* Dependencies;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CommandPoolCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public CommandPoolCreateFlags Flags;
        public uint QueueFamilyIndex;

        public CommandPoolCreateInfo(CommandPoolCreateFlags flags, uint queueFamilyIndex)
        {
            Type = StructureType.CommandPoolCreateInfo;
            Next = null;
            Flags = flags;
            QueueFamilyIndex = queueFamilyIndex;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CommandBufferAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public CommandPoolHandle CommandPool;
        public CommandBufferLevel Level;
        public uint CommandBufferCount;

        public CommandBufferAllocateInfo(CommandPoolHandle commandPool, uint commandBufferCount)
        {
            Type = StructureType.CommandBufferAllocateInfo;
            Next = null;
            CommandPool = commandPool;
            Level = CommandBufferLevel.Primary;
            CommandBufferCount = commandBufferCount;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CommandBufferInheritanceInfo
    {
        public StructureType Type;
        public void* Next;
        public RenderPassHandle RenderPass;
        public uint Subpass;
        public FramebufferHandle Framebuffer;
        public Bool32 OcclusionQueryEnable;
        public QueryControlFlags QueryFlags;
        public QueryPipelineStatisticFlags PipelineStatistics;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CommandBufferBeginInfo
    {
        public StructureType Type;
        public void* Next;
        public CommandBufferUsageFlags Flags;
        public CommandBufferInheritanceInfo* InheritanceInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BufferCopy
    {
        public DeviceSize SrcOffset;
        public DeviceSize DstOffset;
        public DeviceSize Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageSubresourceLayers
    {
        public ImageAspectFlags AspectMask;
        public uint MipLevel;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageCopy
    {
        public ImageSubresourceLayers SrcSubresource;
        public Offset3D SrcOffset;
        public ImageSubresourceLayers DstSubresource;
        public Offset3D DstOffset;
        public Extent3D Extent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageBlit
    {
        public ImageSubresourceLayers SrcSubresource;
        public Offset3D* SrcOffsets;
        public ImageSubresourceLayers DstSubresource;
        public Offset3D* DstOffsets;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BufferImageCopy
    {
        public DeviceSize BufferOffset;
        public uint BufferRowLength;
        public uint BufferImageHeight;
        public ImageSubresourceLayers ImageSubresource;
        public Offset3D ImageOffset;
        public Extent3D ImageExtent;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ClearColorValue
    {
        [FieldOffset(0)] public fixed float Float32[4];
        [FieldOffset(0)] public fixed int Int32[4];
        [FieldOffset(0)] public fixed uint Uint32[4];
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ClearDepthStencilValue
    {
        public float Depth;
        public uint Stencil;
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ClearValue
    {
        [FieldOffset(0)] public ClearColorValue Color;
        [FieldOffset(0)] public ClearDepthStencilValue DepthStencil;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ClearAttachment
    {
        public ImageAspectFlags AspectMask;
        public uint ColorAttachment;
        public ClearValue ClearValue;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ClearRect
    {
        public Rect2D Rect;
        public uint BaseArrayLayer;
        public uint LayerCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageResolve
    {
        public ImageSubresourceLayers SrcSubresource;
        public Offset3D SrcOffset;
        public ImageSubresourceLayers DstSubresource;
        public Offset3D DstOffset;
        public Extent3D Extent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryBarrier
    {
        public StructureType Type;
        public void* Next;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BufferMemoryBarrier
    {
        public StructureType Type;
        public void* Next;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
        public uint SrcQueueFamilyIndex;
        public uint DstQueueFamilyIndex;
        public BufferHandle Buffer;
        public DeviceSize Offset;
        public DeviceSize Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageMemoryBarrier
    {
        public StructureType Type;
        public void* Next;
        public AccessFlags SrcAccessMask;
        public AccessFlags DstAccessMask;
        public ImageLayout OldLayout;
        public ImageLayout NewLayout;
        public uint SrcQueueFamilyIndex;
        public uint DstQueueFamilyIndex;
        public ImageHandle Image;
        public ImageSubresourceRange SubresourceRange;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RenderPassBeginInfo
    {
        public StructureType Type;
        public void* Next;
        public RenderPassHandle RenderPass;
        public FramebufferHandle Framebuffer;
        public Rect2D RenderArea;
        public uint ClearValueCount;
        public ClearValue* ClearValues;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DispatchIndirectCommand
    {
        public uint X;
        public uint Y;
        public uint Z;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DrawIndexedIndirectCommand
    {
        public uint IndexCount;
        public uint InstanceCount;
        public uint FirstIndex;
        public int VertexOffset;
        public uint FirstInstance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DrawIndirectCommand
    {
        public uint VertexCount;
        public uint InstanceCount;
        public uint FirstVertex;
        public uint FirstInstance;
    }

    //
    // KHR surface
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SurfaceCapabilities
    {
        public uint MinImageCount;
        public uint MaxImageCount;
        public Extent2D CurrentExtent;
        public Extent2D MinImageExtent;
        public Extent2D MaxImageExtent;
        public uint MaxImageArrayLayers;
        public SurfaceTransformFlags SupportedTransforms;
        public SurfaceTransformFlags CurrentTransform;
        public CompositeAlphaFlags SupportedCompositeAlpha;
        public ImageUsageFlags SupportedUsageFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SurfaceFormat
    {
        public Format Format;
        public ColorSpace ColorSpace;
    }

    //
    // KHR Swapchain
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SwapchainCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SwapchainCreateFlags Flags;
        public SurfaceHandle Surface;
        public uint MinImageCount;
        public Format ImageFormat;
        public ColorSpace ImageColorSpace;
        public Extent2D ImageExtent;
        public uint ImageArrayLayers;
        public ImageUsageFlags ImageUsage;
        public SharingMode ImageSharingMode;
        public uint QueueFamilyIndexCount;
        public uint* QueueFamilyIndices;
        public SurfaceTransformFlags PreTransform;
        public CompositeAlphaFlags CompositeAlpha;
        public PresentMode PresentMode;
        public Bool32 Clipped;
        public SwapchainHandle OldSwapchain;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PresentInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public SemaphoreHandle* WaitSemaphores;
        public uint SwapchainCount;
        public SwapchainHandle* Swapchains;
        public uint* ImageIndices;
        public Result* Results;
    }

    //
    // KHR display
    // 
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayProperties
    {
        public DisplayHandle Display;
        public byte* DisplayName;
        public Extent2D PhysicalDimensions;
        public Extent2D PhysicalResolution;
        public SurfaceTransformFlags SupportedTransforms;
        public Bool32 PlaneReorderPossible;
        public Bool32 PersistentContent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayModeParameters
    {
        public Extent2D VisibleRegion;
        public uint RefreshRate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayModeProperties
    {
        public DisplayModeHandle DisplayMode;
        public DisplayModeParameters Parameters;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayModeCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DisplayModeCreateFlags Flags;
        public DisplayModeParameters Parameters;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayPlaneCapabilities
    {
        public DisplayPlaneAlphaFlags SupportedAlpha;
        public Offset2D MinSrcPosition;
        public Offset2D MaxSrcPosition;
        public Extent2D MinSrcExtent;
        public Extent2D MaxSrcExtent;
        public Offset2D MinDstPosition;
        public Offset2D MaxDstPosition;
        public Extent2D MinDstExtent;
        public Extent2D MaxDstExtent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayPlaneProperties
    {
        public DisplayHandle CurrentDisplay;
        public uint CurrentStackIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplaySurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DisplaySurfaceCreateFlags Flags;
        public DisplayModeHandle DisplayMode;
        public uint PlaneIndex;
        public uint PlaneStackIndex;
        public SurfaceTransformFlags Transform;
        public float GlobalAlpha;
        public DisplayPlaneAlphaFlags AlphaMode;
        public Extent2D ImageExtent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayPresentInfo
    {
        public StructureType Type;
        public void* Next;
        public Rect2D SrcRect;
        public Rect2D DstRect;
        public Bool32 Persistent;
    }

    //
    // KHR Platforms
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct XlibSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public XlibSurfaceCreateFlags Flags;
        public IntPtr Dpy;
        public IntPtr Window;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct XcbSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public XcbSurfaceCreateFlags Flags;
        public IntPtr Connection;
        public IntPtr Window;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct WaylandSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public WaylandSurfaceCreateFlags Flags;
        public IntPtr Display;
        public IntPtr Surface;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MirSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public MirSurfaceCreateFlags Flags;
        public IntPtr Connection;
        public IntPtr MirSurface;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AndroidSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public AndroidSurfaceCreateFlags Flags;
        public IntPtr Window;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Win32SurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public Win32SurfaceCreateFlags Flags;
        public IntPtr Hinstance;
        public IntPtr Hwnd;

        public Win32SurfaceCreateInfo(IntPtr hinstance, IntPtr hwnd)
        {
            Type = StructureType.Win32SurfaceCreateInfo;
            Next = null;
            Flags = Win32SurfaceCreateFlags.None;
            Hinstance = hinstance;
            Hwnd = hwnd;
        }
    }

    //
    // KHR 2
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceFeatures2
    {
        public StructureType Type;
        public void* Next;
        public PhysicalDeviceFeatures Features;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceProperties2
    {
        public StructureType Type;
        public void* Next;
        public PhysicalDeviceProperties Properties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FormatProperties2
    {
        public StructureType Type;
        public void* Next;
        public FormatProperties FormatProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageFormatProperties2
    {
        public StructureType Type;
        public void* Next;
        public ImageFormatProperties ImageFormatProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceImageFormatInfo2
    {
        public StructureType Type;
        public void* Next;
        public Format Format;
        public ImageType ImageType;
        public ImageTiling Tiling;
        public ImageUsageFlags Usage;
        public ImageCreateFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct QueueFamilyProperties2
    {
        public StructureType Type;
        public void* Next;
        public QueueFamilyProperties QueueFamilyProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceMemoryProperties2
    {
        public StructureType Type;
        public void* Next;
        public PhysicalDeviceMemoryProperties MemoryProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseImageFormatProperties2
    {
        public StructureType Type;
        public void* Next;
        public SparseImageFormatProperties Properties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceSparseImageFormatInfo2
    {
        public StructureType Type;
        public void* Next;
        public Format Format;
        public ImageType ImageType;
        public SampleCountFlags Samples;
        public ImageUsageFlags Usage;
        public ImageTiling Tiling;
    }

    //
    // KHR Memory
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalMemoryProperties
    {
        public ExternalMemoryFeatureFlags ExternalMemoryFeatures;
        public ExternalMemoryHandleTypeFlags ExportFomImportedHandleTypes;
        public ExternalMemoryHandleTypeFlags CompatibleHandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceExternalImageFormatInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalImageFormatProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryProperties ExternalMemoryProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceExternalBufferInfo
    {
        public StructureType Type;
        public void* Next;
        public BufferCreateFlags Flags;
        public BufferUsageFlags Usage;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalBufferProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryProperties ExternalMemoryProperties;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceIDProperties
    {
        public StructureType Type;
        public void* Next;
        public fixed byte DeivceUUID[(int)Vulkan.UUIDSize];
        public fixed byte DriverUUID[(int)Vulkan.UUIDSize];
        public fixed byte DeviceLUID[Vulkan.LUIDSize];
        public uint DeviceNodeMask;
        public Bool32 DeviceLUIDValid;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalMemoryImageCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalMemoryBufferCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportMemoryAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImportMemoryWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportMemoryWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryWin32HandleProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MemoryTypeBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryGetWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceMemoryHandle Memory;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImportMemoryFdInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlags HandleType;
        public int Fd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryFdProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MemoryTypeBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryGetFdInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceMemoryHandle Memory;
        public ExternalMemoryHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Win32KeyedMutexAcquireReleaseInfo
    {
        public StructureType Type;
        public void* Next;
        public uint AcquireCount;
        public DeviceMemoryHandle* AcquireSyncs;
        public ulong* AcquireKeys;
        public uint* AcquireTimeouts;
        public uint ReleaseCount;
        public DeviceMemoryHandle* ReleaseSyncs;
        public ulong* ReleaseKeys;
    }

    //
    // KHR semaphore
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceExternalSemaphoreInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalSemaphoreHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalSemaphoreProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalSemaphoreHandleTypeFlags ExportFromImportedHandleTypes;
        public ExternalSemaphoreHandleTypeFlags CompatibleHandleTypes;
        public ExternalSemaphoreFeatureFlags ExternalSemaphoreFeatures;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportSemaphoreCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalSemaphoreHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImportSemaphoreWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public SemaphoreHandle Semaphore;
        public SemaphoreImportFlags Flags;
        public ExternalSemaphoreHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportSemaphoreWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct D3D12FenceSubmitInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreValuesCount;
        public ulong* WaitSemaphoreValues;
        public uint SignalSemaphoreValuesCount;
        public ulong* SignalSemaphoreValues;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SemaphoreGetWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public SemaphoreHandle Semaphore;
        public ExternalSemaphoreHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImportSemaphoreFdInfo
    {
        public StructureType Type;
        public void* Next;
        public SemaphoreHandle Semaphore;
        public SemaphoreImportFlags Flags;
        public ExternalSemaphoreHandleTypeFlags HandleType;
        public int Fd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SemaphoreGetFdInfo
    {
        public StructureType Type;
        public void* Next;
        public SemaphoreHandle Semaphore;
        public ExternalSemaphoreHandleTypeFlags HandleType;
    }

    //
    // KHR misc
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDevicePushDescriptorProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MaxPushDescriptors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDevice16BitStorageFeatures
    {
        public StructureType Type;
        public void* Next;
        public Bool32 StorageBuffer16BitAccess;
        public Bool32 UniformAndStorageBuffer16BitAccess;
        public Bool32 StoragePushant16;
        public Bool32 StorageInputOutput16;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RectLayer
    {
        public Offset2D Offset;
        public Extent2D Extent;
        public uint Layer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PresentRegion
    {
        public uint RectangleCount;
        public RectLayer* Rectangles;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PresentRegions
    {
        public StructureType Type;
        public void* Next;
        public uint SwapchainCount;
        public PresentRegion* Regions;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorUpdateTemplateEntry
    {
        public uint DstBinding;
        public uint DstArrayElement;
        public uint DescriptorCount;
        public DescriptorType DescriptorType;
        public Size Offset;
        public Size Stride;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DescriptorUpdateTemplateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DescriptorUpdateTemplateCreateFlags Flags;
        public uint DescriptorUpdateEntryCount;
        public DescriptorUpdateTemplateEntry* DescriptorUpdateEntries;
        public DescriptorUpdateTemplateType TemplateType;
        public DescriptorSetLayoutHandle DescriptorSetLayout;
        public PipelineBindPoint PipelineBindPoint;
        public PipelineLayoutHandle PipelineLayout;
        public uint Set;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SharedPresentSurfaceCapabilities
    {
        public StructureType Type;
        public void* Next;
        public ImageUsageFlags SharedPresentSupportedUsageFlags;
    }

    //
    // KHR fence
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceExternalFenceInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalFenceHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalFenceProperties
    {
        public StructureType Type;
        public void* Next;
        public ExternalFenceHandleTypeFlags ExportFromImportedHandleTypes;
        public ExternalFenceHandleTypeFlags CompatibleHandleTypes;
        public ExternalFenceFeatureFlags ExternalFenceFeatures;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportFenceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ExternalFenceHandleTypeFlags HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImportFenceWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public FenceHandle Fence;
        public FenceImportFlags Flags;
        public ExternalFenceHandleTypeFlags HandleType;
        public IntPtr Handle;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportFenceWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
        public char* Name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FenceGetWin32HandleInfo
    {
        public StructureType Type;
        public void* Next;
        public FenceHandle Fence;
        public ExternalFenceHandleTypeFlags HandleType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImportFenceFdInfo
    {
        public StructureType Type;
        public void* Next;
        public FenceHandle Fence;
        public FenceImportFlags Flags;
        public ExternalFenceHandleTypeFlags HandleType;
        public int Fd;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FenceGetFdInfo
    {
        public StructureType Type;
        public void* Next;
        public FenceHandle Fence;
        public ExternalFenceHandleTypeFlags HandleType;
    }

    //
    // KHR 2
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceSurfaceInfo2
    {
        public StructureType Type;
        public void* Next;
        public SurfaceHandle Surface;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SurfaceCapabilities2
    {
        public StructureType Type;
        public void* Next;
        public SurfaceCapabilities SurfaceCapabilities;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SurfaceFormat2
    {
        public StructureType Type;
        public void* Next;
        public SurfaceFormat SurfaceFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceVariablePointerFeatures
    {
        public StructureType Type;
        public void* Next;
        public Bool32 VariablePointersStorageBuffer;
        public Bool32 VariablePointers;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryDedicatedRequirements
    {
        public StructureType Type;
        public void* Next;
        public Bool32 PrefersDedicatedAllocation;
        public Bool32 RequiresDedicatedAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryDedicatedAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public ImageHandle Image;
        public BufferHandle Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BufferMemoryRequirementsInfo2
    {
        public StructureType Type;
        public void* Next;
        public BufferHandle Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageMemoryRequirementsInfo2
    {
        public StructureType Type;
        public void* Next;
        public ImageHandle Image;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageSparseMemoryRequirementsInfo2
    {
        public StructureType Type;
        public void* Next;
        public ImageHandle Image;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryRequirements2
    {
        public StructureType Type;
        public void* Next;
        public MemoryRequirements MemoryRequirements;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SparseImageMemoryRequirements2
    {
        public StructureType Type;
        public void* Next;
        public SparseImageMemoryRequirements MemoryRequirements;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DebugReportCallbackCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DebugReportFlags Flags;
        public IntPtr Callback;
        public IntPtr UserData;

        public DebugReportCallbackCreateInfo(DebugReportFlags flags, DebugReportCallbackEXTDelegate callback)
        {
            Type = StructureType.DebugReportCallbackCreateInfo;
            Next = null;
            Flags = flags;
            Callback = Marshal.GetFunctionPointerForDelegate(callback);
            UserData = IntPtr.Zero;
        }
    }

    //
    // AMD
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineRasterizationStateRasterizationOrder
    {
        public StructureType Type;
        public void* Next;
        public RasterizationOrder RasterizationOrder;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DebugMarkerObjectNameInfo
    {
        public StructureType Type;
        public void* Next;
        public DebugReportObjectType ObjectType;
        public ulong Object;
        public byte* ObjectName;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DebugMarkerObjectTagInfo
    {
        public StructureType Type;
        public void* Next;
        public DebugReportObjectType ObjectType;
        public ulong Object;
        public ulong Tagname;
        public Size TagSize;
        public void* Tag;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DebugMarkerMarkerInfo
    {
        public StructureType Type;
        public void* Next;
        public byte* MarkerName;
        public fixed float Color[4];
    }

    //
    // AMD
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DedicatedAllocationImageCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public Bool32 DedicatedAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DedicatedAllocationBufferCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public Bool32 DedicatedAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DedicatedAllocationMemoryAllocateInfo
    {
        public StructureType Type;
        public void* Next;
        public ImageHandle Image;
        public BufferHandle Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TextureLODGatherFormatProperties
    {
        public StructureType Type;
        public void* Next;
        public Bool32 SupportsTextureGatherLODBias;
    }

    //
    // KHX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RenderPassMultiviewCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public uint SubpassCount;
        public uint* ViewMasks;
        public uint DependencyCount;
        public uint* ViewOffsets;
        public uint CorrelationMaskCount;
        public uint* CorrelationMasks;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceMultiviewFeatures
    {
        public StructureType Type;
        public void* Next;
        public Bool32 Multiview;
        public Bool32 MultiviewGeometryShader;
        public Bool32 MultiviewTessellationShader;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceMultiviewProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MaxMultiviewViewCount;
        public uint MaxMultiviewInstanceIndex;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalImageFormatPropertiesNV
    {
        public ImageFormatProperties ImageFormatProperties;
        public ExternalMemoryFeatureFlagsNV ExternalMemoryFeatures;
        public ExternalMemoryHandleTypeFlagsNV ExportFromImportedHandleTypes;
        public ExternalMemoryHandleTypeFlagsNV CompatibleHandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExternalMemoryImageCreateInfoNV
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlagsNV HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportMemoryAllocateInfoNV
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlagsNV HandleTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImportMemoryWin32HandleInfoNV
    {
        public StructureType Type;
        public void* Next;
        public ExternalMemoryHandleTypeFlagsNV HandleType;
        public IntPtr Handle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ExportMemoryWin32HandleInfoNV
    {
        public StructureType Type;
        public void* Next;
        public IntPtr Attributes;
        public IntPtr DwAccess;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Win32KeyedMutexAcquireReleaseInfoNV
    {
        public StructureType Type;
        public void* Next;
        public uint AcquireCount;
        public DeviceMemoryHandle* AcquireSyncs;
        public ulong* AcquireKeys;
        public uint* AcquireTimeoutMilliseconds;
        public uint ReleaseCount;
        public DeviceMemoryHandle* ReleaseSyncs;
        public ulong* ReleaseKeys;
    }

    //
    // KHX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MemoryAllocateFlagsInfo
    {
        public StructureType Type;
        public void* Next;
        public MemoryAllocateFlags Flags;
        public uint DeviceMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BindBufferMemoryInfo
    {
        public StructureType Type;
        public void* Next;
        public BufferHandle Buffer;
        public DeviceMemoryHandle Memory;
        public DeviceSize MemoryOffset;
        public uint DeviceIndexCount;
        public uint* DeviceIndices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BindImageMemoryInfo
    {
        public StructureType Type;
        public void* Next;
        public ImageHandle Image;
        public DeviceMemoryHandle Memory;
        public DeviceSize MemoryOffset;
        public uint DeviceIndexCount;
        public uint* DeviceIndices;
        public uint SFRRectCount;
        public Rect2D* SFRRects;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupRenderPassBeginInfo
    {
        public StructureType Type;
        public void* Next;
        public uint DeviceMask;
        public uint DeviceRenderAreaCount;
        public Rect2D* DeviceRenderAreas;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupCommandBufferBeginInfo
    {
        public StructureType Type;
        public void* Next;
        public uint DeviceMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupSubmitInfo
    {
        public StructureType Type;
        public void* Next;
        public uint WaitSemaphoreCount;
        public uint* WaitSemaphoreDeviceIndices;
        public uint CommandBufferCount;
        public uint* CommandBufferDeviceMasks;
        public uint SignalSemaphoreCount;
        public uint* SignalSemaphoreDeviceIndices;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupBindSparseInfo
    {
        public StructureType Type;
        public void* Next;
        public uint ResourceDeviceIndex;
        public uint MemoryDeviceIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupPresentCapabilities
    {
        public StructureType Type;
        public void* Next;
        public fixed uint PresentMask[Vulkan.MaxDeviceGroupSize];
        public DeviceGroupPresentModeFlags Modes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ImageSwapchainCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SwapchainHandle Swapchain;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct BindImageMemorySwapchainInfo
    {
        public StructureType Type;
        public void* Next;
        public SwapchainHandle Swapchain;
        public uint ImageIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct AcquireNextImageInfo
    {
        public StructureType Type;
        public void* Next;
        public SwapchainHandle Swapchain;
        public ulong Timeout;
        public SemaphoreHandle Semaphore;
        public FenceHandle Fence;
        public uint DeviceMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupPresentInfo
    {
        public StructureType Type;
        public void* Next;
        public uint SwapchainCount;
        public uint* DeviceMasks;
        public DeviceGroupPresentModeFlags Mode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupSwapchainCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceGroupPresentModeFlags Modes;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ValidationFlags
    {
        public StructureType Type;
        public void* Next;
        public uint DisabledValidationCheckCount;
        public ValidationCheck* DisabledValidationChecks;
    }

    //
    // NN
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ViSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public ViSurfaceCreateFlags Flags;
        public void* Window;
    }

    //
    // KHX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceGroupProperties
    {
        public StructureType Type;
        public void* Next;
        public uint PhysicalDeviceCount;
        public PhysicalDeviceHandle* PhysicalDevices;
        public Bool32 SubsetAllocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGroupDeviceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public uint PhysicalDeviceCount;
        public PhysicalDeviceHandle* PhysicalDevices;
    }

    //
    // NVX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGeneratedCommandsFeatures
    {
        public StructureType Type;
        public void* Next;
        public Bool32 ComputeBindingPointSupport;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceGeneratedCommandsLimits
    {
        public StructureType Type;
        public void* Next;
        public uint MaxIndirectCommandsLayoutTokenCount;
        public uint MaxObjectEntryCounts;
        public uint MinSequenceCountBufferOffsetAlignment;
        public uint MinSequenceIndexBufferOffsetAlignment;
        public uint MinCommandsTokenBufferOffsetAlignment;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IndirectCommandsToken
    {
        public IndirectCommandsTokenType TokenType;
        public BufferHandle Buffer;
        public DeviceSize Offset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IndirectCommandsLayoutToken
    {
        public IndirectCommandsTokenType TokenType;
        public uint BindingUnit;
        public uint DynamicCount;
        public uint Divisor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IndirectCommandsLayoutCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineBindPoint PipelineBindPoint;
        public IndirectCommandsLayoutUsageFlags Flags;
        public uint TokenCount;
        public IndirectCommandsLayoutToken* Tokens;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CmdProcessCommandsInfo
    {
        public StructureType Type;
        public void* Next;
        public ObjectTableHandle ObjectTable;
        public IndirectCommandsLayoutHandle IndirectCommandsLayout;
        public uint IndirectCommandsTokenCount;
        public IndirectCommandsToken* IndirectCommandsTokens;
        public uint MaxSequencesCount;
        public CommandBufferHandle TargetCommandBuffer;
        public BufferHandle SequencesCountBuffer;
        public DeviceSize SequencesCountOffset;
        public BufferHandle SequencesIndexBuffer;
        public DeviceSize SequencesIndexOffset;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct CmdReserveSpaceForCommandsInfo
    {
        public StructureType Type;
        public void* Next;
        public ObjectTableHandle ObjectTable;
        public IndirectCommandsLayoutHandle IndirectCommandsLayout;
        public uint MaxSequencesCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ObjectTableCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public uint ObjectCount;
        public ObjectEntryType* ObjectEntryTypes;
        public uint* ObjectEntryCounts;
        public ObjectEntryUsageFlags* pObjectEntryUsageFlags;
        public uint MaxUniformBuffersPerDescriptor;
        public uint MaxStorageBuffersPerDescriptor;
        public uint MaxStorageImagesPerDescriptor;
        public uint MaxSampledImagesPerDescriptor;
        public uint MaxPipelineLayouts;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ObjectTableEntry
    {
        public ObjectEntryType Type;
        public ObjectEntryUsageFlags Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ObjectTablePipelineEntry
    {
        public ObjectEntryType Type;
        public ObjectEntryUsageFlags Flags;
        public PipelineHandle Pipeline;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ObjectTableDescriptorSetEntry
    {
        public ObjectEntryType Type;
        public ObjectEntryUsageFlags Flags;
        public PipelineLayoutHandle PipelineLayout;
        public DescriptorSetHandle DescriptorSet;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ObjectTableVertexBufferEntry
    {
        public ObjectEntryType Type;
        public ObjectEntryUsageFlags Flags;
        public BufferHandle Buffer;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ObjectTableIndexBufferEntry
    {
        public ObjectEntryType Type;
        public ObjectEntryUsageFlags Flags;
        public BufferHandle Buffer;
        public IndexType IndexType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ObjectTablePushantEntry
    {
        public ObjectEntryType Type;
        public ObjectEntryUsageFlags Flags;
        public PipelineLayoutHandle PipelineLayout;
        public ShaderStageFlags StageFlags;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ViewportWScaling
    {
        public float XCoeff;
        public float YCoeff;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineViewportWScalingStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public Bool32 ViewportWScalingEnable;
        public uint ViewportCount;
        public ViewportWScaling* ViewportWScalings;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SurfaceCapabilities2EXT
    {
        public StructureType Type;
        public void* Next;
        public uint MinImageCount;
        public uint MaxImageCount;
        public Extent2D CurrentExtent;
        public Extent2D MinImageExtent;
        public Extent2D MaxImageExtent;
        public uint MaxImageArrayLayers;
        public SurfaceTransformFlags SupportedTransforms;
        public SurfaceTransformFlags CurrentTransform;
        public CompositeAlphaFlags SupportedCompositeAlpha;
        public ImageUsageFlags SupportedUsageFlags;
        public SurfaceCounterFlags SupportedSurfaceCounters;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayPowerInfo
    {
        public StructureType Type;
        public void* Next;
        public DisplayPowerState PowerState;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DeviceEventInfo
    {
        public StructureType Type;
        public void* Next;
        public DeviceEventType DeviceEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayEventInfo
    {
        public StructureType Type;
        public void* Next;
        public DisplayEventType DisplayEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SwapchainCounterCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SurfaceCounterFlags SurfaceCounters;
    }

    //
    // GOOGLE
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RefreshCycleDuration
    {
        public ulong RefreshDuration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PastPresentationTiming
    {
        public uint PresentID;
        public ulong DesiredPrensetTime;
        public ulong ActualPresentTime;
        public ulong EarliestPresentTime;
        public ulong PresentMargin;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PresentTime
    {
        public uint PresentID;
        public ulong DesiredPresentTime;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PresentTimesInfo
    {
        public StructureType Type;
        public void* Next;
        public uint SwapchainCount;
        public PresentTime* Times;
    }

    //
    // NVX
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceMultiviewPerViewAttributesProperties
    {
        public StructureType Type;
        public void* Next;
        public Bool32 PerViewPositionAllComponents;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ViewportSwizzle
    {
        public ViewportCoordinateSwizzle X;
        public ViewportCoordinateSwizzle Y;
        public ViewportCoordinateSwizzle Z;
        public ViewportCoordinateSwizzle W;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineViewportSwizzleStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineViewportSwizzleStateCreateFlags Flags;
        public uint ViewportCount;
        public ViewportSwizzle* ViewportSwizzles;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceDiscardRectangleProperties
    {
        public StructureType Type;
        public void* Next;
        public uint MaxDiscardRectangles;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineDiscardRectangleStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineDiscardRectangleStateCreateFlags Flags;
        public DiscardRectangleMode DiscardRectangleMode;
        public uint DiscardRectangleCount;
        public Rect2D* DiscardRectangles;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct XYColor
    {
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct HdrMetadata
    {
        public StructureType Type;
        public void* Next;
        public XYColor DisplayPrimaryRed;
        public XYColor DisplayPrimaryGreen;
        public XYColor DisplayPrimaryBlue;
        public XYColor WhitePoint;
        public float MaxLuminance;
        public float MinLuminance;
        public float MaxContentLightLevel;
        public float MaxFrameAverageLightLevel;
    }

    //
    // M
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct IOSSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public IOSSurfaceCreateFlags Flags;
        public void* View;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MacOSSurfaceCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public MacOSSurfaceCreateFlags Flags;
        public void* View;
    }

    //
    // EXT
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SamplerReductionModeCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public SamplerReductionMode ReductionMode;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceSamplerFilterMinmaxProperties
    {
        public StructureType Type;
        public void* Next;
        public Bool32 FilterMinmaxSingleComponentFormats;
        public Bool32 FilterMinmaxImageComponentMapping;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceBlendOperationAdvancedFeatures
    {
        public StructureType Type;
        public void* Next;
        public Bool32 AdvancedBlendCoherentOperations;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PhysicalDeviceBlendOperationAdvancedProperties
    {
        public StructureType Type;
        public void* Next;
        public uint AdvancedBlendMaxColorAttachments;
        public Bool32 AdvancedBlendIndependentBlend;
        public Bool32 AdvancedBlendNonPremultipliedSrcColor;
        public Bool32 AdvancedBlendNonPremultipliedDstColor;
        public Bool32 AdvancedBlendCorrelatedOverlap;
        public Bool32 AdvancedBlendAllOperations;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineColorBlendAdvancedStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public Bool32 SrcPremultiplied;
        public Bool32 DstPremultiplied;
        public BlendOverlap BlendOverlap;
    }

    //
    // NV
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineCoverageToColorStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCoverageToColorStateCreateFlags Flags;
        public Bool32 CoverageToColorEnable;
        public uint CoverageToColorLocation;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct PipelineCoverageModulationStateCreateInfo
    {
        public StructureType Type;
        public void* Next;
        public PipelineCoverageModulationStateCreateFlags flags;
        public CoverageModulationMode CoverageModulationMode;
        public Bool32 CoverageModulationTableEnable;
        public uint CoverageModulationTableCount;
        public float* CoverageModulationTable;
    }
    #endregion

    #region Functions
    public unsafe delegate Result CreateInstanceDelegate(ref InstanceCreateInfo createInfo, AllocationCallbacks* Allocator, out InstanceHandle instance);
    public unsafe delegate void DestroyInstanceDelegate(InstanceHandle instance, AllocationCallbacks* Allocator);
    public unsafe delegate Result EnumeratePhysicalDevicesDelegate(InstanceHandle instance, ref uint physicalDeviceCount, PhysicalDeviceHandle* physicalDevices);
    public unsafe delegate void GetPhysicalDeviceFeaturesDelegate(PhysicalDeviceHandle physicalDevice, out PhysicalDeviceFeatures features);
    public unsafe delegate void GetPhysicalDeviceFormatPropertiesDelegate(PhysicalDeviceHandle physicalDevice, Format format, out FormatProperties formatProperties);
    public unsafe delegate Result GetPhysicalDeviceImageFormatPropertiesDelegate(PhysicalDeviceHandle physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, out ImageFormatProperties imageFormatProperties);
    public unsafe delegate void GetPhysicalDevicePropertiesDelegate(PhysicalDeviceHandle physicalDevice, out PhysicalDeviceProperties properties);
    public unsafe delegate void GetPhysicalDeviceQueueFamilyPropertiesDelegate(PhysicalDeviceHandle physicalDevice, ref uint queueFamilyPropertyCount, QueueFamilyProperties* queueFamilyProperties);
    public unsafe delegate void GetPhysicalDeviceMemoryPropertiesDelegate(PhysicalDeviceHandle physicalDevice, out PhysicalDeviceMemoryProperties memoryProperties);
    public unsafe delegate IntPtr GetInstanceProcAddrDelegate(InstanceHandle instance, byte* name);
    public unsafe delegate IntPtr GetDeviceProcAddrDelegate(DeviceHandle device, byte* name);
    public unsafe delegate Result CreateDeviceDelegate(PhysicalDeviceHandle physicalDevice, ref DeviceCreateInfo createInfo, AllocationCallbacks* Allocator, out DeviceHandle device);
    public unsafe delegate void DestroyDeviceDelegate(DeviceHandle device, AllocationCallbacks* Allocator);
    public unsafe delegate Result EnumerateInstanceExtensionPropertiesDelegate(byte* layerName, ref uint propertyCount, ExtensionProperties* properties);
    public unsafe delegate Result EnumerateDeviceExtensionPropertiesDelegate(PhysicalDeviceHandle physicalDevice, byte* layerName, ref uint propertyCount, ExtensionProperties* properties);
    public unsafe delegate Result EnumerateInstanceLayerPropertiesDelegate(ref uint propertyCount, LayerProperties* properties);
    public unsafe delegate Result EnumerateDeviceLayerPropertiesDelegate(PhysicalDeviceHandle physicalDevice, ref uint propertyCount, LayerProperties* properties);
    public unsafe delegate void GetDeviceQueueDelegate(DeviceHandle device, uint queueFamilyIndex, uint queueIndex, out QueueHandle queue);
    public unsafe delegate Result QueueSubmitDelegate(QueueHandle queue, uint submitCount, SubmitInfo* submits, FenceHandle fence);
    public unsafe delegate Result QueueWaitIdleDelegate(QueueHandle queue);
    public unsafe delegate Result DeviceWaitIdleDelegate(DeviceHandle device);
    public unsafe delegate Result AllocateMemoryDelegate(DeviceHandle device, ref MemoryAllocateInfo AllocateInfo, AllocationCallbacks* Allocator, out DeviceMemoryHandle memory);
    public unsafe delegate void FreeMemoryDelegate(DeviceHandle device, DeviceMemoryHandle memory, AllocationCallbacks* Allocator);
    public unsafe delegate Result MapMemoryDelegate(DeviceHandle device, DeviceMemoryHandle memory, DeviceSize offset, DeviceSize size, MemoryMapFlags flags, void** data);
    public unsafe delegate void UnmapMemoryDelegate(DeviceHandle device, DeviceMemoryHandle memory);
    public unsafe delegate Result FlushMappedMemoryRangesDelegate(DeviceHandle device, uint memoryRangeCount, MappedMemoryRange* memoryRanges);
    public unsafe delegate Result InvalidateMappedMemoryRangesDelegate(DeviceHandle device, uint memoryRangeCount, MappedMemoryRange* memoryRanges);
    public unsafe delegate void GetDeviceMemoryCommitmentDelegate(DeviceHandle device, DeviceMemoryHandle memory, out DeviceSize committedMemoryInBytes);
    public unsafe delegate Result BindBufferMemoryDelegate(DeviceHandle device, BufferHandle buffer, DeviceMemoryHandle memory, DeviceSize memoryOffset);
    public unsafe delegate Result BindImageMemoryDelegate(DeviceHandle device, ImageHandle image, DeviceMemoryHandle memory, DeviceSize memoryOffset);
    public unsafe delegate void GetBufferMemoryRequirementsDelegate(DeviceHandle device, BufferHandle buffer, out MemoryRequirements memoryRequirements);
    public unsafe delegate void GetImageMemoryRequirementsDelegate(DeviceHandle device, ImageHandle image, out MemoryRequirements memoryRequirements);
    public unsafe delegate void GetImageSparseMemoryRequirementsDelegate(DeviceHandle device, ImageHandle image, out uint sparseMemoryRequirementCount, SparseImageMemoryRequirements* sparseMemoryRequirements);
    public unsafe delegate void GetPhysicalDeviceSparseImageFormatPropertiesDelegate(PhysicalDeviceHandle physicalDevice, Format format, ImageType type, SampleCountFlags samples, ImageUsageFlags usage, ImageTiling tiling, ref uint propertyCount, SparseImageFormatProperties* properties);
    public unsafe delegate Result QueueBindSparseDelegate(QueueHandle queue, uint bindInfoCount, ref BindSparseInfo bindInfo, FenceHandle fence);
    public unsafe delegate Result CreateFenceDelegate(DeviceHandle device, ref FenceCreateInfo createInfo, AllocationCallbacks* Allocator, out FenceHandle fence);
    public unsafe delegate void DestroyFenceDelegate(DeviceHandle device, FenceHandle fence, AllocationCallbacks* Allocator);
    public unsafe delegate Result ResetFencesDelegate(DeviceHandle device, uint fenceCount, FenceHandle* fences);
    public unsafe delegate Result GetFenceStatusDelegate(DeviceHandle device, FenceHandle fence);
    public unsafe delegate Result WaitForFencesDelegate(DeviceHandle device, uint fenceCount, FenceHandle* fences, Bool32 waitAll, ulong timeout);
    public unsafe delegate Result CreateSemaphoreDelegate(DeviceHandle device, ref SemaphoreCreateInfo createInfo, AllocationCallbacks* Allocator, out SemaphoreHandle semaphore);
    public unsafe delegate void DestroySemaphoreDelegate(DeviceHandle device, SemaphoreHandle semaphore, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateEventDelegate(DeviceHandle device, ref EventCreateInfo createInfo, AllocationCallbacks* Allocator, out EventHandle evnt);
    public unsafe delegate void DestroyEventDelegate(DeviceHandle device, EventHandle evt, AllocationCallbacks* Allocator);
    public unsafe delegate Result GetEventStatusDelegate(DeviceHandle device, EventHandle evt);
    public unsafe delegate Result SetEventDelegate(DeviceHandle device, EventHandle evt);
    public unsafe delegate Result ResetEventDelegate(DeviceHandle device, EventHandle evt);
    public unsafe delegate Result CreateQueryPoolDelegate(DeviceHandle device, ref QueryPoolCreateInfo createInfo, AllocationCallbacks* Allocator, out QueryPoolHandle queryPool);
    public unsafe delegate void DestroyQueryPoolDelegate(DeviceHandle device, QueryPoolHandle queryPool, AllocationCallbacks* Allocator);
    public unsafe delegate Result GetQueryPoolResultsDelegate(DeviceHandle device, QueryPoolHandle queryPool, uint firstQuery, uint queryCount, Size dataSize, void* data, DeviceSize stride, QueryResultFlags flags);
    public unsafe delegate Result CreateBufferDelegate(DeviceHandle device, ref BufferCreateInfo createInfo, AllocationCallbacks* Allocator, out BufferHandle buffer);
    public unsafe delegate void DestroyBufferDelegate(DeviceHandle device, BufferHandle buffer, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateBufferViewDelegate(DeviceHandle device, ref BufferViewCreateInfo createInfo, AllocationCallbacks* Allocator, out BufferViewHandle view);
    public unsafe delegate void DestroyBufferViewDelegate(DeviceHandle device, BufferViewHandle bufferView, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateImageDelegate(DeviceHandle device, ref ImageCreateInfo createInfo, AllocationCallbacks* Allocator, out ImageHandle image);
    public unsafe delegate void DestroyImageDelegate(DeviceHandle device, ImageHandle image, AllocationCallbacks* Allocator);
    public unsafe delegate void GetImageSubresourceLayoutDelegate(DeviceHandle device, ImageHandle image, ref ImageSubresource subresource, out SubresourceLayout layout);
    public unsafe delegate Result CreateImageViewDelegate(DeviceHandle device, ref ImageViewCreateInfo createInfo, AllocationCallbacks* Allocator, out ImageViewHandle view);
    public unsafe delegate void DestroyImageViewDelegate(DeviceHandle device, ImageViewHandle imageView, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateShaderModuleDelegate(DeviceHandle device, ref ShaderModuleCreateInfo createInfo, AllocationCallbacks* Allocator, out ShaderModuleHandle shaderModule);
    public unsafe delegate void DestroyShaderModuleDelegate(DeviceHandle device, ShaderModuleHandle shaderModule, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreatePipelineCacheDelegate(DeviceHandle device, ref PipelineCacheCreateInfo createInfo, AllocationCallbacks* Allocator, out PipelineCacheHandle pipelineCache);
    public unsafe delegate void DestroyPipelineCacheDelegate(DeviceHandle device, PipelineCacheHandle pipelineCache, AllocationCallbacks* Allocator);
    public unsafe delegate Result GetPipelineCacheDataDelegate(DeviceHandle device, PipelineCacheHandle pipelineCache, out Size dataSize, void* data);
    public unsafe delegate Result MergePipelineCachesDelegate(DeviceHandle device, PipelineCacheHandle dstCache, uint srcCacheCount, ref PipelineCacheHandle* srcCaches);
    public unsafe delegate Result CreateGraphicsPipelinesDelegate(DeviceHandle device, PipelineCacheHandle pipelineCache, uint createInfoCount, GraphicsPipelineCreateInfo* createInfos, AllocationCallbacks* Allocator, PipelineHandle* pipelines);
    public unsafe delegate Result CreateComputePipelinesDelegate(DeviceHandle device, PipelineCacheHandle pipelineCache, uint createInfoCount, ComputePipelineCreateInfo* createInfos, AllocationCallbacks* Allocator, PipelineHandle* pipelines);
    public unsafe delegate void DestroyPipelineDelegate(DeviceHandle device, PipelineHandle pipeline, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreatePipelineLayoutDelegate(DeviceHandle device, ref PipelineLayoutCreateInfo createInfo, AllocationCallbacks* Allocator, out PipelineLayoutHandle pipelineLayout);
    public unsafe delegate void DestroyPipelineLayoutDelegate(DeviceHandle device, PipelineLayoutHandle pipelineLayout, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateSamplerDelegate(DeviceHandle device, ref SamplerCreateInfo createInfo, AllocationCallbacks* Allocator, out SamplerHandle sampler);
    public unsafe delegate void DestroySamplerDelegate(DeviceHandle device, SamplerHandle sampler, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateDescriptorSetLayoutDelegate(DeviceHandle device, ref DescriptorSetLayoutCreateInfo createInfo, AllocationCallbacks* Allocator, out DescriptorSetLayoutHandle setLayout);
    public unsafe delegate void DestroyDescriptorSetLayoutDelegate(DeviceHandle device, DescriptorSetLayoutHandle descriptorSetLayout, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateDescriptorPoolDelegate(DeviceHandle device, ref DescriptorPoolCreateInfo createInfo, AllocationCallbacks* Allocator, out DescriptorPoolHandle descriptorPool);
    public unsafe delegate void DestroyDescriptorPoolDelegate(DeviceHandle device, DescriptorPoolHandle descriptorPool, AllocationCallbacks* Allocator);
    public unsafe delegate Result ResetDescriptorPoolDelegate(DeviceHandle device, DescriptorPoolHandle descriptorPool, DescriptorPoolResetFlags flags);
    public unsafe delegate Result AllocateDescriptorSetsDelegate(DeviceHandle device, ref DescriptorSetAllocateInfo AllocateInfo, DescriptorSetHandle* descriptorSets);
    public unsafe delegate Result FreeDescriptorSetsDelegate(DeviceHandle device, DescriptorPoolHandle descriptorPool, uint descriptorSetCount, DescriptorSetHandle* descriptorSets);
    public unsafe delegate void UpdateDescriptorSetsDelegate(DeviceHandle device, uint descriptorWriteCount, WriteDescriptorSet* descriptorWrites, uint descriptorCopyCount, CopyDescriptorSet* descriptorCopies);
    public unsafe delegate Result CreateFramebufferDelegate(DeviceHandle device, ref FramebufferCreateInfo createInfo, AllocationCallbacks* Allocator, out FramebufferHandle framebuffer);
    public unsafe delegate void DestroyFramebufferDelegate(DeviceHandle device, FramebufferHandle framebuffer, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateRenderPassDelegate(DeviceHandle device, ref RenderPassCreateInfo createInfo, AllocationCallbacks* Allocator, out RenderPassHandle renderPass);
    public unsafe delegate void DestroyRenderPassDelegate(DeviceHandle device, RenderPassHandle renderPass, AllocationCallbacks* Allocator);
    public unsafe delegate void GetRenderAreaGranularityDelegate(DeviceHandle device, RenderPassHandle renderPass, out Extent2D granularity);
    public unsafe delegate Result CreateCommandPoolDelegate(DeviceHandle device, ref CommandPoolCreateInfo createInfo, AllocationCallbacks* Allocator, out CommandPoolHandle commandPool);
    public unsafe delegate void DestroyCommandPoolDelegate(DeviceHandle device, CommandPoolHandle commandPool, AllocationCallbacks* Allocator);
    public unsafe delegate Result ResetCommandPoolDelegate(DeviceHandle device, CommandPoolHandle commandPool, CommandPoolResetFlags flags);
    public unsafe delegate Result AllocateCommandBuffersDelegate(DeviceHandle device, ref CommandBufferAllocateInfo AllocateInfo, CommandBufferHandle* commandBuffers);
    public unsafe delegate void FreeCommandBuffersDelegate(DeviceHandle device, CommandPoolHandle commandPool, uint commandBufferCount, CommandBufferHandle* commandBuffers);
    public unsafe delegate Result BeginCommandBufferDelegate(CommandBufferHandle commandBuffer, ref CommandBufferBeginInfo beginInfo);
    public unsafe delegate Result EndCommandBufferDelegate(CommandBufferHandle commandBuffer);
    public unsafe delegate Result ResetCommandBufferDelegate(CommandBufferHandle commandBuffer, CommandBufferResetFlags flags);
    public unsafe delegate void CmdBindPipelineDelegate(CommandBufferHandle commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineHandle pipeline);
    public unsafe delegate void CmdSetViewportDelegate(CommandBufferHandle commandBuffer, uint firstViewport, uint viewportCount, Viewport* viewports);
    public unsafe delegate void CmdSetScissorDelegate(CommandBufferHandle commandBuffer, uint firstScissor, uint scissorCount, Rect2D* scissors);
    public unsafe delegate void CmdSetLineWidthDelegate(CommandBufferHandle commandBuffer, float lineWidth);
    public unsafe delegate void CmdSetDepthBiasDelegate(CommandBufferHandle commandBuffer, float depthBiasrefantFactor, float depthBiasClamp, float depthBiasSlopeFactor);
    public unsafe delegate void CmdSetBlendConstantsDelegate(CommandBufferHandle commandBuffer, float* blendConstants);
    public unsafe delegate void CmdSetDepthBoundsDelegate(CommandBufferHandle commandBuffer, float minDepthBounds, float maxDepthBounds);
    public unsafe delegate void CmdSetStencilCompareMaskDelegate(CommandBufferHandle commandBuffer, StencilFaceFlags faceMask, uint compareMask);
    public unsafe delegate void CmdSetStencilWriteMaskDelegate(CommandBufferHandle commandBuffer, StencilFaceFlags faceMask, uint writeMask);
    public unsafe delegate void CmdSetStencilReferenceDelegate(CommandBufferHandle commandBuffer, StencilFaceFlags faceMask, uint reference);
    public unsafe delegate void CmdBindDescriptorSetsDelegate(CommandBufferHandle commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineLayoutHandle layout, uint firstSet, uint descriptorSetCount, DescriptorSetHandle* descriptorSets, uint dynamicOffsetCount, uint* dynamicOffsets);
    public unsafe delegate void CmdBindIndexBufferDelegate(CommandBufferHandle commandBuffer, BufferHandle buffer, DeviceSize offset, IndexType indexType);
    public unsafe delegate void CmdBindVertexBuffersDelegate(CommandBufferHandle commandBuffer, uint firstBinding, uint bindingCount, BufferHandle* buffers, DeviceSize* offsets);
    public unsafe delegate void CmdDrawDelegate(CommandBufferHandle commandBuffer, uint vertexCount, uint instanceCount, uint firstVertex, uint firstInstance);
    public unsafe delegate void CmdDrawIndexedDelegate(CommandBufferHandle commandBuffer, uint indexCount, uint instanceCount, uint firstIndex, int vertexOffset, uint firstInstance);
    public unsafe delegate void CmdDrawIndirectDelegate(CommandBufferHandle commandBuffer, BufferHandle buffer, DeviceSize offset, uint drawCount, uint stride);
    public unsafe delegate void CmdDrawIndexedIndirectDelegate(CommandBufferHandle commandBuffer, BufferHandle buffer, DeviceSize offset, uint drawCount, uint stride);
    public unsafe delegate void CmdDispatchDelegate(CommandBufferHandle commandBuffer, uint groupCountX, uint groupCountY, uint groupCountZ);
    public unsafe delegate void CmdDispatchIndirectDelegate(CommandBufferHandle commandBuffer, BufferHandle buffer, DeviceSize offset);
    public unsafe delegate void CmdCopyBufferDelegate(CommandBufferHandle commandBuffer, BufferHandle srcBuffer, BufferHandle dstBuffer, uint regionCount, BufferCopy* regions);
    public unsafe delegate void CmdCopyImageDelegate(CommandBufferHandle commandBuffer, ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage, ImageLayout dstImageLayout, uint regionCount, ImageCopy* regions);
    public unsafe delegate void CmdBlitImageDelegate(CommandBufferHandle commandBuffer, ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage, ImageLayout dstImageLayout, uint regionCount, ImageBlit* regions, Filter filter);
    public unsafe delegate void CmdCopyBufferToImageDelegate(CommandBufferHandle commandBuffer, BufferHandle srcBuffer, ImageHandle dstImage, ImageLayout dstImageLayout, uint regionCount, BufferImageCopy* regions);
    public unsafe delegate void CmdCopyImageToBufferDelegate(CommandBufferHandle commandBuffer, ImageHandle srcImage, ImageLayout srcImageLayout, BufferHandle dstBuffer, uint regionCount, BufferImageCopy* regions);
    public unsafe delegate void CmdUpdateBufferDelegate(CommandBufferHandle commandBuffer, BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize dataSize, void* data);
    public unsafe delegate void CmdFillBufferDelegate(CommandBufferHandle commandBuffer, BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize size, uint data);
    public unsafe delegate void CmdClearColorImageDelegate(CommandBufferHandle commandBuffer, ImageHandle image, ImageLayout imageLayout, ref ClearColorValue color, uint rangeCount, ImageSubresourceRange* ranges);
    public unsafe delegate void CmdClearDepthStencilImageDelegate(CommandBufferHandle commandBuffer, ImageHandle image, ImageLayout imageLayout, ref ClearDepthStencilValue depthStencil, uint rangeCount, ImageSubresourceRange* ranges);
    public unsafe delegate void CmdClearAttachmentsDelegate(CommandBufferHandle commandBuffer, uint attachmentCount, ClearAttachment* attachments, uint rectCount, ClearRect* rects);
    public unsafe delegate void CmdResolveImageDelegate(CommandBufferHandle commandBuffer, ImageHandle srcImage, ImageLayout srcImageLayout, ImageHandle dstImage, ImageLayout dstImageLayout, uint regionCount, ImageResolve* regions);
    public unsafe delegate void CmdSetEventDelegate(CommandBufferHandle commandBuffer, EventHandle evt, PipelineStageFlags stageMask);
    public unsafe delegate void CmdResetEventDelegate(CommandBufferHandle commandBuffer, EventHandle evt, PipelineStageFlags stageMask);
    public unsafe delegate void CmdWaitEventsDelegate(CommandBufferHandle commandBuffer, uint eventCount, EventHandle* events, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, uint memoryBarrierCount, MemoryBarrier* memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier* imageMemoryBarriers);
    public unsafe delegate void CmdPipelineBarrierDelegate(CommandBufferHandle commandBuffer, PipelineStageFlags srcStageMask, PipelineStageFlags dstStageMask, DependencyFlags dependencyFlags, uint memoryBarrierCount, MemoryBarrier* memoryBarriers, uint bufferMemoryBarrierCount, BufferMemoryBarrier* bufferMemoryBarriers, uint imageMemoryBarrierCount, ImageMemoryBarrier* imageMemoryBarriers);
    public unsafe delegate void CmdBeginQueryDelegate(CommandBufferHandle commandBuffer, QueryPoolHandle queryPool, uint query, QueryControlFlags flags);
    public unsafe delegate void CmdEndQueryDelegate(CommandBufferHandle commandBuffer, QueryPoolHandle queryPool, uint query);
    public unsafe delegate void CmdResetQueryPoolDelegate(CommandBufferHandle commandBuffer, QueryPoolHandle queryPool, uint firstQuery, uint queryCount);
    public unsafe delegate void CmdWriteTimestampDelegate(CommandBufferHandle commandBuffer, PipelineStageFlags pipelineStage, QueryPoolHandle queryPool, uint query);
    public unsafe delegate void CmdCopyQueryPoolResultsDelegate(CommandBufferHandle commandBuffer, QueryPoolHandle queryPool, uint firstQuery, uint queryCount, BufferHandle dstBuffer, DeviceSize dstOffset, DeviceSize stride, QueryResultFlags flags);
    public unsafe delegate void CmdPushConstantsDelegate(CommandBufferHandle commandBuffer, PipelineLayoutHandle layout, ShaderStageFlags stageFlags, uint offset, uint size, void* values);
    public unsafe delegate void CmdBeginRenderPassDelegate(CommandBufferHandle commandBuffer, ref RenderPassBeginInfo renderPassBegin, SubpassContents contents);
    public unsafe delegate void CmdNextSubpassDelegate(CommandBufferHandle commandBuffer, SubpassContents contents);
    public unsafe delegate void CmdEndRenderPassDelegate(CommandBufferHandle commandBuffer);
    public unsafe delegate void CmdExecuteCommandsDelegate(CommandBufferHandle commandBuffer, uint commandBufferCount, CommandBufferHandle* commandBuffers);

    // Khronos
    public unsafe delegate void DestroySurfaceKHRDelegate(InstanceHandle instance, SurfaceHandle surface, AllocationCallbacks* Allocator);
    public unsafe delegate Result GetPhysicalDeviceSurfaceSupportKHRDelegate(PhysicalDeviceHandle physicalDevice, uint queueFamilyIndex, SurfaceHandle surface, out Bool32 supported);
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilitiesKHRDelegate(PhysicalDeviceHandle physicalDevice, SurfaceHandle surface, out SurfaceCapabilities surfaceCapabilities);
    public unsafe delegate Result GetPhysicalDeviceSurfaceFormatsKHRDelegate(PhysicalDeviceHandle physicalDevice, SurfaceHandle surface, ref uint surfaceFormatCount, SurfaceFormat* surfaceFormats);
    public unsafe delegate Result GetPhysicalDeviceSurfacePresentModesKHRDelegate(PhysicalDeviceHandle physicalDevice, SurfaceHandle surface, ref uint presentModeCount, PresentMode* presentModes);
    public unsafe delegate Result CreateSwapchainKHRDelegate(DeviceHandle device, ref SwapchainCreateInfo createInfo, AllocationCallbacks* Allocator, out SwapchainHandle swapchain);
    public unsafe delegate void DestroySwapchainKHRDelegate(DeviceHandle device, SwapchainHandle swapchain, AllocationCallbacks* Allocator);
    public unsafe delegate Result GetSwapchainImagesKHRDelegate(DeviceHandle device, SwapchainHandle swapchain, ref uint swapchainImageCount, ImageHandle* swapchainImages);
    public unsafe delegate Result AcquireNextImageKHRDelegate(DeviceHandle device, SwapchainHandle swapchain, ulong timeout, SemaphoreHandle semaphore, FenceHandle fence, out uint imageIndex);
    public unsafe delegate Result QueuePresentKHRDelegate(QueueHandle queue, ref PresentInfo presentInfo);
    public unsafe delegate Result GetPhysicalDeviceDisplayPropertiesKHRDelegate(PhysicalDeviceHandle physicalDevice, ref uint propertyCount, DisplayProperties* properties);
    public unsafe delegate Result GetPhysicalDeviceDisplayPlanePropertiesKHRDelegate(PhysicalDeviceHandle physicalDevice, ref uint propertyCount, DisplayPlaneProperties* properties);
    public unsafe delegate Result GetDisplayPlaneSupportedDisplaysKHRDelegate(PhysicalDeviceHandle physicalDevice, uint planeIndex, ref uint displayCount, DisplayHandle* displays);
    public unsafe delegate Result GetDisplayModePropertiesKHRDelegate(PhysicalDeviceHandle physicalDevice, DisplayHandle display, ref uint propertyCount, DisplayModeProperties* properties);
    public unsafe delegate Result CreateDisplayModeKHRDelegate(PhysicalDeviceHandle physicalDevice, DisplayHandle display, ref DisplayModeCreateInfo createInfo, AllocationCallbacks* Allocator, out DisplayModeHandle mode);
    public unsafe delegate Result GetDisplayPlaneCapabilitiesKHRDelegate(PhysicalDeviceHandle physicalDevice, DisplayModeHandle mode, uint planeIndex, out DisplayPlaneCapabilities capabilities);
    public unsafe delegate Result CreateDisplayPlaneSurfaceKHRDelegate(InstanceHandle instance, ref DisplaySurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Result CreateSharedSwapchainsKHRDelegate(DeviceHandle device, uint swapchainCount, SwapchainCreateInfo* createInfos, AllocationCallbacks* Allocator, SwapchainHandle* swapchains);
    public unsafe delegate Result CreateXlibSurfaceKHRDelegate(InstanceHandle instance, ref XlibSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Bool32 GetPhysicalDeviceXlibPresentationSupportKHRDelegate(PhysicalDeviceHandle physicalDevice, uint queueFamilyIndex, IntPtr dpy, IntPtr visualID);
    public unsafe delegate Result CreateXcbSurfaceKHRDelegate(InstanceHandle instance, ref XcbSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Bool32 GetPhysicalDeviceXcbPresentationSupportKHRDelegate(PhysicalDeviceHandle physicalDevice, uint queueFamilyIndex, IntPtr connection, IntPtr visualId);
    public unsafe delegate Result CreateWaylandSurfaceKHRDelegate(InstanceHandle instance, ref WaylandSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Bool32 GetPhysicalDeviceWaylandPresentationSupportKHRDelegate(PhysicalDeviceHandle physicalDevice, uint queueFamilyIndex, IntPtr display);
    public unsafe delegate Result CreateMirSurfaceKHRDelegate(InstanceHandle instance, ref MirSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Bool32 GetPhysicalDeviceMirPresentationSupportKHRDelegate(PhysicalDeviceHandle physicalDevice, uint queueFamilyIndex, IntPtr connection);
    public unsafe delegate Result CreateAndroidSurfaceKHRDelegate(InstanceHandle instance, ref AndroidSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Result CreateWin32SurfaceKHRDelegate(InstanceHandle instance, ref Win32SurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Bool32 GetPhysicalDeviceWin32PresentationSupportKHRDelegate(PhysicalDeviceHandle physicalDevice, uint queueFamilyIndex);
    public unsafe delegate void GetPhysicalDeviceFeatures2KHRDelegate(PhysicalDeviceHandle physicalDevice, out PhysicalDeviceFeatures2 features);
    public unsafe delegate void GetPhysicalDeviceProperties2KHRDelegate(PhysicalDeviceHandle physicalDevice, out PhysicalDeviceProperties2 properties);
    public unsafe delegate void GetPhysicalDeviceFormatProperties2KHRDelegate(PhysicalDeviceHandle physicalDevice, Format format, out FormatProperties2 formatProperties);
    public unsafe delegate Result GetPhysicalDeviceImageFormatProperties2KHRDelegate(PhysicalDeviceHandle physicalDevice, ref PhysicalDeviceImageFormatInfo2 imageFormatInfo, out ImageFormatProperties2 imageFormatProperties);
    public unsafe delegate void GetPhysicalDeviceQueueFamilyProperties2KHRDelegate(PhysicalDeviceHandle physicalDevice, ref uint queueFamilyPropertyCount, QueueFamilyProperties2* queueFamilyProperties);
    public unsafe delegate void GetPhysicalDeviceMemoryProperties2KHRDelegate(PhysicalDeviceHandle physicalDevice, out PhysicalDeviceMemoryProperties2 memoryProperties);
    public unsafe delegate void GetPhysicalDeviceSparseImageFormatProperties2KHRDelegate(PhysicalDeviceHandle physicalDevice, ref PhysicalDeviceSparseImageFormatInfo2 formatInfo, ref uint propertyCount, SparseImageFormatProperties2* properties);
    public unsafe delegate void TrimCommandPoolKHRDelegate(DeviceHandle device, CommandPoolHandle commandPool, CommandPoolTrimFlags flags);
    public unsafe delegate void GetPhysicalDeviceExternalBufferPropertiesKHRDelegate(PhysicalDeviceHandle physicalDevice, ref PhysicalDeviceExternalBufferInfo externalBufferInfo, out ExternalBufferProperties externalBufferProperties);
    public unsafe delegate Result GetMemoryWin32HandleKHRDelegate(DeviceHandle device, ref MemoryGetWin32HandleInfo getWin32HandleInfo, out IntPtr handle);
    public unsafe delegate Result GetMemoryWin32HandlePropertiesKHRDelegate(DeviceHandle device, ExternalMemoryHandleTypeFlags handleType, IntPtr handle, out MemoryWin32HandleProperties memoryWin32HandleProperties);
    public unsafe delegate Result GetMemoryFdKHRDelegate(DeviceHandle device, ref MemoryGetFdInfo getFdInfo, out int fd);
    public unsafe delegate Result GetMemoryFdPropertiesKHRDelegate(DeviceHandle device, ExternalMemoryHandleTypeFlags handleType, int fd, out MemoryFdProperties pMemoryFdProperties);
    public unsafe delegate void GetPhysicalDeviceExternalSemaphorePropertiesKHRDelegate(PhysicalDeviceHandle physicalDevice, ref PhysicalDeviceExternalSemaphoreInfo externalSemaphoreInfo, out ExternalSemaphoreProperties externalSemaphoreProperties);
    public unsafe delegate Result ImportSemaphoreWin32HandleKHRDelegate(DeviceHandle device, ref ImportSemaphoreWin32HandleInfo importSemaphoreWin32HandleInfo);
    public unsafe delegate Result GetSemaphoreWin32HandleKHRDelegate(DeviceHandle device, ref SemaphoreGetWin32HandleInfo getWin32HandleInfo, out IntPtr handle);
    public unsafe delegate Result ImportSemaphoreFdKHRDelegate(DeviceHandle device, ref ImportSemaphoreFdInfo importSemaphoreFdInfo);
    public unsafe delegate Result GetSemaphoreFdKHRDelegate(DeviceHandle device, ref SemaphoreGetFdInfo getFdInfo, out int fd);
    public unsafe delegate void CmdPushDescriptorSetKHRDelegate(CommandBufferHandle commandBuffer, PipelineBindPoint pipelineBindPoint, PipelineLayoutHandle layout, uint set, uint descriptorWriteCount, WriteDescriptorSet* descriptorWrites);
    public unsafe delegate Result CreateDescriptorUpdateTemplateKHRDelegate(DeviceHandle device, ref DescriptorUpdateTemplateCreateInfo createInfo, AllocationCallbacks* Allocator, out DescriptorUpdateTemplateHandle descriptorUpdateTemplate);
    public unsafe delegate void DestroyDescriptorUpdateTemplateKHRDelegate(DeviceHandle device, DescriptorUpdateTemplateHandle descriptorUpdateTemplate, AllocationCallbacks* Allocator);
    public unsafe delegate void UpdateDescriptorSetWithTemplateKHRDelegate(DeviceHandle device, DescriptorSetHandle descriptorSet, DescriptorUpdateTemplateHandle descriptorUpdateTemplate, void* data);
    public unsafe delegate void CmdPushDescriptorSetWithTemplateKHRDelegate(CommandBufferHandle commandBuffer, DescriptorUpdateTemplateHandle descriptorUpdateTemplate, PipelineLayoutHandle layout, uint set, void* data);
    public unsafe delegate Result GetSwapchainStatusKHRDelegate(DeviceHandle device, SwapchainHandle swapchain);
    public unsafe delegate void GetPhysicalDeviceExternalFencePropertiesKHRDelegate(PhysicalDeviceHandle physicalDevice, ref PhysicalDeviceExternalFenceInfo externalFenceInfo, out ExternalFenceProperties externalFenceProperties);
    public unsafe delegate Result ImportFenceWin32HandleKHRDelegate(DeviceHandle device, ref ImportFenceWin32HandleInfo importFenceWin32HandleInfo);
    public unsafe delegate Result GetFenceWin32HandleKHRDelegate(DeviceHandle device, ref FenceGetWin32HandleInfo getWin32HandleInfo, out IntPtr pHandle);
    public unsafe delegate Result ImportFenceFdKHRDelegate(DeviceHandle device, ref ImportFenceFdInfo importFenceFdInfo);
    public unsafe delegate Result GetFenceFdKHRDelegate(DeviceHandle device, ref FenceGetFdInfo getFdInfo, out int fd);
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2KHRDelegate(PhysicalDeviceHandle physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, out SurfaceCapabilities2 surfaceCapabilities);
    public unsafe delegate Result GetPhysicalDeviceSurfaceFormats2KHRDelegate(PhysicalDeviceHandle physicalDevice, ref PhysicalDeviceSurfaceInfo2 surfaceInfo, ref uint surfaceFormatCount, SurfaceFormat2* surfaceFormats);
    public unsafe delegate void GetImageMemoryRequirements2KHRDelegate(DeviceHandle device, ref ImageMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
    public unsafe delegate void GetBufferMemoryRequirements2KHRDelegate(DeviceHandle device, ref BufferMemoryRequirementsInfo2 info, out MemoryRequirements2 memoryRequirements);
    public unsafe delegate void GetImageSparseMemoryRequirements2KHRDelegate(DeviceHandle device, ref ImageSparseMemoryRequirementsInfo2 info, ref uint sparseMemoryRequirementCount, SparseImageMemoryRequirements2* sparseMemoryRequirements);

    // Khronos X
    public unsafe delegate void GetDeviceGroupPeerMemoryFeaturesKHXDelegate(DeviceHandle device, uint heapIndex, uint localDeviceIndex, uint remoteDeviceIndex, out PeerMemoryFeatureFlags peerMemoryFeatures);
    public unsafe delegate Result BindBufferMemory2KHXDelegate(DeviceHandle device, uint bindInfoCount, BindBufferMemoryInfo* bindInfos);
    public unsafe delegate Result BindImageMemory2KHXDelegate(DeviceHandle device, uint bindInfoCount, BindImageMemoryInfo* bindInfos);
    public unsafe delegate void CmdSetDeviceMaskKHXDelegate(CommandBufferHandle commandBuffer, uint deviceMask);
    public unsafe delegate Result GetDeviceGroupPresentCapabilitiesKHXDelegate(DeviceHandle device, out DeviceGroupPresentCapabilities deviceGroupPresentCapabilities);
    public unsafe delegate Result GetDeviceGroupSurfacePresentModesKHXDelegate(DeviceHandle device, SurfaceHandle surface, out DeviceGroupPresentModeFlags modes);
    public unsafe delegate Result AcquireNextImage2KHXDelegate(DeviceHandle device, ref AcquireNextImageInfo acquireInfo, out uint imageIndex);
    public unsafe delegate void CmdDispatchBaseKHXDelegate(CommandBufferHandle commandBuffer, uint baseGroupX, uint baseGroupY, uint baseGroupZ, uint groupCountX, uint groupCountY, uint groupCountZ);
    public unsafe delegate Result GetPhysicalDevicePresentRectanglesKHXDelegate(PhysicalDeviceHandle physicalDevice, SurfaceHandle surface, ref uint rectCount, Rect2D* rects);
    public unsafe delegate Result EnumeratePhysicalDeviceGroupsKHXDelegate(InstanceHandle instance, ref uint physicalDeviceGroupCount, PhysicalDeviceGroupProperties* physicalDeviceGroupProperties);

    // Multi-vendor
    public unsafe delegate Bool32 DebugReportCallbackEXTDelegate(DebugReportFlags flags, DebugReportObjectType objectType, ulong objectHandle, Size location, int messageCode, byte* layerPrefix, byte* message, IntPtr userData);
    public unsafe delegate Result CreateDebugReportCallbackEXTDelegate(InstanceHandle instance, ref DebugReportCallbackCreateInfo createInfo, AllocationCallbacks* Allocator, out DebugReportCallbackHandle callback);
    public unsafe delegate void DestroyDebugReportCallbackEXTDelegate(InstanceHandle instance, DebugReportCallbackHandle callback, AllocationCallbacks* Allocator);
    public unsafe delegate void DebugReportMessageEXTDelegate(InstanceHandle instance, DebugReportFlags flags, DebugReportObjectType objectType, ulong obj, Size location, int messageCode, byte* layerPrefix, byte* message);
    public unsafe delegate Result DebugMarkerSetObjectTagEXTDelegate(DeviceHandle device, ref DebugMarkerObjectTagInfo tagInfo);
    public unsafe delegate Result DebugMarkerSetObjectNameEXTDelegate(DeviceHandle device, ref DebugMarkerObjectNameInfo nameInfo);
    public unsafe delegate void CmdDebugMarkerBeginEXTDelegate(CommandBufferHandle commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
    public unsafe delegate void CmdDebugMarkerEndEXTDelegate(CommandBufferHandle commandBuffer);
    public unsafe delegate void CmdDebugMarkerInsertEXTDelegate(CommandBufferHandle commandBuffer, ref DebugMarkerMarkerInfo markerInfo);
    public unsafe delegate Result ReleaseDisplayEXTDelegate(PhysicalDeviceHandle physicalDevice, DisplayHandle display);
    public unsafe delegate Result AcquireXlibDisplayEXTDelegate(PhysicalDeviceHandle physicalDevice, IntPtr dpy, DisplayHandle display);
    public unsafe delegate Result GetRandROutputDisplayEXTDelegate(PhysicalDeviceHandle physicalDevice, IntPtr dpy, IntPtr rrOutput, out DisplayHandle display);
    public unsafe delegate Result GetPhysicalDeviceSurfaceCapabilities2EXTDelegate(PhysicalDeviceHandle physicalDevice, SurfaceHandle surface, out SurfaceCapabilities2EXT surfaceCapabilities);
    public unsafe delegate Result DisplayPowerControlEXTDelegate(DeviceHandle device, DisplayHandle display, ref DisplayPowerInfo displayPowerInfo);
    public unsafe delegate Result RegisterDeviceEventEXTDelegate(DeviceHandle device, ref DeviceEventInfo deviceEventInfo, AllocationCallbacks* Allocator, out FenceHandle fence);
    public unsafe delegate Result RegisterDisplayEventEXTDelegate(DeviceHandle device, DisplayHandle display, ref DisplayEventInfo displayEventInfo, AllocationCallbacks* Allocator, out FenceHandle fence);
    public unsafe delegate Result GetSwapchainCounterEXTDelegate(DeviceHandle device, SwapchainHandle swapchain, SurfaceCounterFlags counter, out ulong counterValue);
    public unsafe delegate void CmdSetDiscardRectangleEXTDelegate(CommandBufferHandle commandBuffer, uint firstDiscardRectangle, uint discardRectangleCount, Rect2D* discardRectangles);
    public unsafe delegate void SetHdrMetadataEXTDelegate(DeviceHandle device, uint swapchainCount, SwapchainHandle* swapchains, HdrMetadata* metadata);

    // AMD
    public unsafe delegate void CmdDrawIndirectCountAMDDelegate(CommandBufferHandle commandBuffer, BufferHandle buffer, DeviceSize offset, BufferHandle countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);
    public unsafe delegate void CmdDrawIndexedIndirectCountAMDDelegate(CommandBufferHandle commandBuffer, BufferHandle buffer, DeviceSize offset, BufferHandle countBuffer, DeviceSize countBufferOffset, uint maxDrawCount, uint stride);

    // Nvidia
    public unsafe delegate Result GetPhysicalDeviceExternalImageFormatPropertiesNVDelegate(PhysicalDeviceHandle physicalDevice, Format format, ImageType type, ImageTiling tiling, ImageUsageFlags usage, ImageCreateFlags flags, ExternalMemoryHandleTypeFlagsNV ExternalHandleType, out ExternalImageFormatPropertiesNV externalImageFormatProperties);
    public unsafe delegate Result GetMemoryWin32HandleNVDelegate(DeviceHandle device, DeviceMemoryHandle memory, ExternalMemoryHandleTypeFlagsNV handleType, out IntPtr handle);
    public unsafe delegate void CmdSetViewportWScalingNVDelegate(CommandBufferHandle commandBuffer, uint firstViewport, uint viewportCount, ViewportWScaling* viewportWScalings);

    // Nvidia X
    public unsafe delegate void CmdProcessCommandsNVXDelegate(CommandBufferHandle commandBuffer, ref CmdProcessCommandsInfo processCommandsInfo);
    public unsafe delegate void CmdReserveSpaceForCommandsNVXDelegate(CommandBufferHandle commandBuffer, ref CmdReserveSpaceForCommandsInfo reserveSpaceInfo);
    public unsafe delegate Result CreateIndirectCommandsLayoutNVXDelegate(DeviceHandle device, ref IndirectCommandsLayoutCreateInfo createInfo, AllocationCallbacks* Allocator, out IndirectCommandsLayoutHandle indirectCommandsLayout);
    public unsafe delegate void DestroyIndirectCommandsLayoutNVXDelegate(DeviceHandle device, IndirectCommandsLayoutHandle indirectCommandsLayout, AllocationCallbacks* Allocator);
    public unsafe delegate Result CreateObjectTableNVXDelegate(DeviceHandle device, ref ObjectTableCreateInfo createInfo, AllocationCallbacks* Allocator, out ObjectTableHandle objectTable);
    public unsafe delegate void DestroyObjectTableNVXDelegate(DeviceHandle device, ObjectTableHandle objectTable, AllocationCallbacks* Allocator);
    public unsafe delegate Result RegisterObjectsNVXDelegate(DeviceHandle device, ObjectTableHandle objectTable, uint objectCount, ref ObjectTableEntry* objectTableEntries, uint* objectIndices);
    public unsafe delegate Result UnregisterObjectsNVXDelegate(DeviceHandle device, ObjectTableHandle objectTable, uint objectCount, ObjectEntryType* objectEntryTypes, uint* objectIndices);
    public unsafe delegate void GetPhysicalDeviceGeneratedCommandsPropertiesNVXDelegate(PhysicalDeviceHandle physicalDevice, out DeviceGeneratedCommandsFeatures features, out DeviceGeneratedCommandsLimits limits);

    // Nintendo
    public unsafe delegate Result CreateViSurfaceNNDelegate(InstanceHandle instance, ref ViSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);

    // Google
    public unsafe delegate Result GetRefreshCycleDurationGOOGLEDelegate(DeviceHandle device, SwapchainHandle swapchain, ref RefreshCycleDuration displayTimingProperties);
    public unsafe delegate Result GetPastPresentationTimingGOOGLEDelegate(DeviceHandle device, SwapchainHandle swapchain, ref uint presentationTimingCount, PastPresentationTiming* presentationTimings);

    // Molten
    public unsafe delegate Result CreateIOSSurfaceMVKDelegate(InstanceHandle instance, ref IOSSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    public unsafe delegate Result CreateMacOSSurfaceMVKDelegate(InstanceHandle instance, ref MacOSSurfaceCreateInfo createInfo, AllocationCallbacks* Allocator, out SurfaceHandle surface);
    #endregion

    #region Utility
    public static class ResultExtensions
    {
        [Conditional("DEBUG")]
        public static void CheckError(this Result result)
        {
            if (result < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Trace.TraceError(result.ToString());
                Console.ResetColor();
            }
            else if (result > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Trace.TraceWarning(result.ToString());
                Console.ResetColor();
            }
        }
    }

    public static unsafe class Utf8
    {
        public static byte* AllocateFromString(string str)
        {
            int maxByteCount = Encoding.UTF8.GetMaxByteCount(str.Length);
            byte* bytePtr = (byte*)Marshal.AllocHGlobal(maxByteCount);
            int actualLength = 0;
            fixed (char* charPtr = str)
            {
                actualLength = Encoding.UTF8.GetBytes(charPtr, str.Length, bytePtr, maxByteCount);
            }

            *(bytePtr + actualLength) = 0;

            return bytePtr;
        }

        public static string ToString(byte* bytePtr)
        {
            int length = 0;
            while (*(bytePtr++) != 0) length++;
            return Encoding.UTF8.GetString(bytePtr, length);
        }

        public static void Free(byte* bytePtr)
        {
            Marshal.FreeHGlobal(new IntPtr(bytePtr));
        }

        public static bool Compare(byte* str1, byte* str2)
        {
            while (true)
            {
                if (*str1 != *str2) return false;
                if (*str1 == 0) return true;
                str1++;
                str2++;
            }
        }
    }
    #endregion
}