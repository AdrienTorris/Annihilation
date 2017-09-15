using System;
using System.Runtime.InteropServices;

namespace CoreVulkan
{
    public unsafe struct Device
    {
        public DeviceHandle Handle { get; private set; }
        public PhysicalDevice PhysicalDevice { get; private set; }

        private static GetDeviceProcAddrDelegate _getDeviceProcAddr;
        private static DestroyDeviceDelegate _destroyDevice;
        private static GetDeviceQueueDelegate _getDeviceQueue;
        private static DeviceWaitIdleDelegate _deviceWaitIdle;
        private static AllocateMemoryDelegate _allocateMemory;
        private static FreeMemoryDelegate _freeMemory;
        private static MapMemoryDelegate _mapMemory;
        private static UnmapMemoryDelegate _unmapMemory;
        private static FlushMappedMemoryRangesDelegate _flushMappedMemoryRanges;
        private static InvalidateMappedMemoryRangesDelegate _invalidateMappedMemoryRanges;
        private static GetDeviceMemoryCommitmentDelegate _getDeviceMemoryCommitment;
        private static GetBufferMemoryRequirementsDelegate _getBufferMemoryRequirements;
        private static BindBufferMemoryDelegate _bindBufferMemory;
        private static GetImageMemoryRequirementsDelegate _getImageMemoryRequirements;
        private static BindImageMemoryDelegate _bindImageMemory;
        private static GetImageSparseMemoryRequirementsDelegate _getImageSparseMemoryRequirements;
        private static CreateFenceDelegate _createFence;
        private static DestroyFenceDelegate _destroyFence;
        private static ResetFencesDelegate _resetFences;
        private static GetFenceStatusDelegate _getFenceStatus;
        private static WaitForFencesDelegate _waitForFences;
        private static CreateSemaphoreDelegate _createSemaphore;
        private static DestroySemaphoreDelegate _destroySemaphore;
        private static CreateEventDelegate _createEvent;
        private static DestroyEventDelegate _destroyEvent;
        private static GetEventStatusDelegate _getEventStatus;
        private static SetEventDelegate _setEvent;
        private static ResetEventDelegate _resetEvent;
        private static CreateQueryPoolDelegate _createQueryPool;
        private static DestroyQueryPoolDelegate _destroyQueryPool;
        private static GetQueryPoolResultsDelegate _getQueryPoolResults;
        private static CreateBufferDelegate _createBuffer;
        private static DestroyBufferDelegate _destroyBuffer;
        private static CreateBufferViewDelegate _createBufferView;
        private static DestroyBufferViewDelegate _destroyBufferView;
        private static CreateImageDelegate _createImage;
        private static DestroyImageDelegate _destroyImage;
        private static GetImageSubresourceLayoutDelegate _getImageSubresourceLayout;
        private static CreateImageViewDelegate _createImageView;
        private static DestroyImageViewDelegate _destroyImageView;
        private static CreateShaderModuleDelegate _createShaderModule;
        private static DestroyShaderModuleDelegate _destroyShaderModule;
        private static CreatePipelineCacheDelegate _createPipelineCache;
        private static DestroyPipelineCacheDelegate _destroyPipelineCache;
        private static GetPipelineCacheDataDelegate _getPipelineCacheData;
        private static MergePipelineCachesDelegate _mergePipelineCaches;
        private static CreateGraphicsPipelinesDelegate _createGraphicsPipelines;
        private static CreateComputePipelinesDelegate _createComputePipelines;
        private static DestroyPipelineDelegate _destroyPipeline;
        private static CreatePipelineLayoutDelegate _createPipelineLayout;
        private static DestroyPipelineLayoutDelegate _destroyPipelineLayout;
        private static CreateSamplerDelegate _createSampler;
        private static DestroySamplerDelegate _destroySampler;
        private static CreateDescriptorSetLayoutDelegate _createDescriptorSetLayout;
        private static DestroyDescriptorSetLayoutDelegate _destroyDescriptorSetLayout;
        private static CreateDescriptorPoolDelegate _createDescriptorPool;
        private static DestroyDescriptorPoolDelegate _destroyDescriptorPool;
        private static ResetDescriptorPoolDelegate _resetDescriptorPool;
        private static AllocateDescriptorSetsDelegate _allocateDescriptorSets;
        private static FreeDescriptorSetsDelegate _freeDescriptorSets;
        private static UpdateDescriptorSetsDelegate _updateDescriptorSets;
        private static CreateFramebufferDelegate _createFramebuffer;
        private static DestroyFramebufferDelegate _destroyFramebuffer;
        private static CreateRenderPassDelegate _createRenderPass;
        private static DestroyRenderPassDelegate _destroyRenderPass;
        private static GetRenderAreaGranularityDelegate _getRenderAreaGranularity;
        private static CreateCommandPoolDelegate _createCommandPool;
        private static DestroyCommandPoolDelegate _destroyCommandPool;
        private static ResetCommandPoolDelegate _resetCommandPool;
        private static AllocateCommandBuffersDelegate _allocateCommandBuffers;
        private static FreeCommandBuffersDelegate _freeCommandBuffers;
        private static CreateSharedSwapchainsKHRDelegate _createSharedSwapchainsKHR;
        private static CreateSwapchainKHRDelegate _createSwapchainKHR;
        private static DestroySwapchainKHRDelegate _destroySwapchainKHR;
        private static GetSwapchainImagesKHRDelegate _getSwapchainImagesKHR;
        private static AcquireNextImageKHRDelegate _acquireNextImageKHR;
        private static DebugMarkerSetObjectNameEXTDelegate _debugMarkerSetObjectNameEXT;
        private static DebugMarkerSetObjectTagEXTDelegate _debugMarkerSetObjectTagEXT;
        private static GetMemoryWin32HandleNVDelegate _getMemoryWin32HandleNV;
        private static CreateIndirectCommandsLayoutNVXDelegate _createIndirectCommandsLayoutNVX;
        private static DestroyIndirectCommandsLayoutNVXDelegate _destroyIndirectCommandsLayoutNVX;
        private static CreateObjectTableNVXDelegate _createObjectTableNVX;
        private static DestroyObjectTableNVXDelegate _destroyObjectTableNVX;
        private static RegisterObjectsNVXDelegate _registerObjectsNVX;
        private static UnregisterObjectsNVXDelegate _unregisterObjectsNVX;
        private static TrimCommandPoolKHRDelegate _trimCommandPoolKHR;
        private static GetMemoryWin32HandleKHRDelegate _getMemoryWin32HandleKHR;
        private static GetMemoryWin32HandlePropertiesKHRDelegate _getMemoryWin32HandlePropertiesKHR;
        private static GetMemoryFdKHRDelegate _getMemoryFdKHR;
        private static GetMemoryFdPropertiesKHRDelegate _getMemoryFdPropertiesKHR;
        private static GetSemaphoreWin32HandleKHRDelegate _getSemaphoreWin32HandleKHR;
        private static ImportSemaphoreWin32HandleKHRDelegate _importSemaphoreWin32HandleKHR;
        private static GetSemaphoreFdKHRDelegate _getSemaphoreFdKHR;
        private static ImportSemaphoreFdKHRDelegate _importSemaphoreFdKHR;
        private static GetFenceWin32HandleKHRDelegate _getFenceWin32HandleKHR;
        private static ImportFenceWin32HandleKHRDelegate _importFenceWin32HandleKHR;
        private static GetFenceFdKHRDelegate _getFenceFdKHR;
        private static ImportFenceFdKHRDelegate _importFenceFdKHR;
        private static DisplayPowerControlEXTDelegate _displayPowerControlEXT;
        private static RegisterDeviceEventEXTDelegate _registerDeviceEventEXT;
        private static RegisterDisplayEventEXTDelegate _registerDisplayEventEXT;
        private static GetSwapchainCounterEXTDelegate _getSwapchainCounterEXT;
        private static GetDeviceGroupPeerMemoryFeaturesKHXDelegate _getDeviceGroupPeerMemoryFeaturesKHX;
        private static BindBufferMemory2KHXDelegate _bindBufferMemory2KHX;
        private static BindImageMemory2KHXDelegate _bindImageMemory2KHX;
        private static GetDeviceGroupPresentCapabilitiesKHXDelegate _getDeviceGroupPresentCapabilitiesKHX;
        private static GetDeviceGroupSurfacePresentModesKHXDelegate _getDeviceGroupSurfacePresentModesKHX;
        private static AcquireNextImage2KHXDelegate _acquireNextImage2KHX;
        private static CreateDescriptorUpdateTemplateKHRDelegate _createDescriptorUpdateTemplateKHR;
        private static DestroyDescriptorUpdateTemplateKHRDelegate _destroyDescriptorUpdateTemplateKHR;
        private static UpdateDescriptorSetWithTemplateKHRDelegate _updateDescriptorSetWithTemplateKHR;
        private static SetHdrMetadataEXTDelegate _setHdrMetadataEXT;
        private static GetSwapchainStatusKHRDelegate _getSwapchainStatusKHR;
        private static GetRefreshCycleDurationGOOGLEDelegate _getRefreshCycleDurationGOOGLE;
        private static GetPastPresentationTimingGOOGLEDelegate _getPastPresentationTimingGOOGLE;
        private static GetBufferMemoryRequirements2KHRDelegate _getBufferMemoryRequirements2KHR;
        private static GetImageMemoryRequirements2KHRDelegate _getImageMemoryRequirements2KHR;
        private static GetImageSparseMemoryRequirements2KHRDelegate _getImageSparseMemoryRequirements2KHR;
        
        public Device(DeviceHandle handle, PhysicalDevice physicalDevice)
        {
            Handle = handle;
            PhysicalDevice = physicalDevice;
        }
    }
}