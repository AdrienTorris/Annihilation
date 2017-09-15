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

        public T GetDeviceProcAddr<T>(byte* functionName)
        {
            _getDeviceProcAddr = _getDeviceProcAddr ?? PhysicalDevice.Instance.GetProcAddr<GetDeviceProcAddrDelegate>(FunctionName.GetDeviceProcAddr);

            IntPtr func = _getDeviceProcAddr(Handle, functionName);
            if (func == IntPtr.Zero) throw new Exception("Could not load Vulkan function " + Utf8.ToString(functionName));
            return Marshal.GetDelegateForFunctionPointer<T>(func);
        }

        public void Destroy()
        {
            _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice);

            _destroyDevice(Handle, null);
        }

        public void GetQueue()
        {
            _getDeviceQueue = _getDeviceQueue ?? GetDeviceProcAddr<GetDeviceQueueDelegate>(FunctionName.GetDeviceQueue);


        }

        public void WaitIdle()
        {
            _deviceWaitIdle = _deviceWaitIdle ?? GetDeviceProcAddr<DeviceWaitIdleDelegate>(FunctionName.DeviceWaitIdle);


        }

        public void AllocateMemory()
        {
            _allocateMemory = _allocateMemory ?? GetDeviceProcAddr<AllocateMemoryDelegate>(FunctionName.AllocateMemory);


        }

        public void FreeMemory()
        {
            _freeMemory = _freeMemory ?? GetDeviceProcAddr<FreeMemoryDelegate>(FunctionName.FreeMemory);


        }

        public void MapMemory()
        {
            _mapMemory = _mapMemory ?? GetDeviceProcAddr<MapMemoryDelegate>(FunctionName.MapMemory);


        }

        public void UnmapMemory()
        {
            _unmapMemory = _unmapMemory ?? GetDeviceProcAddr<UnmapMemoryDelegate>(FunctionName.UnmapMemory);


        }

        public void FlushMappedMemoryRanges()
        {
            _flushMappedMemoryRanges = _flushMappedMemoryRanges ?? GetDeviceProcAddr<FlushMappedMemoryRangesDelegate>(FunctionName.FlushMappedMemoryRanges);


        }

        public void InvalidateMappedMemoryRanges()
        {
            _invalidateMappedMemoryRanges = _invalidateMappedMemoryRanges ?? GetDeviceProcAddr<InvalidateMappedMemoryRangesDelegate>(FunctionName.InvalidateMappedMemoryRanges);


        }

        public void GetMemoryCommitment()
        {
            _getDeviceMemoryCommitment = _getDeviceMemoryCommitment ?? GetDeviceProcAddr<GetDeviceMemoryCommitmentDelegate>(FunctionName.GetDeviceMemoryCommitment);


        }
        
        public void GetBufferMemoryRequirements() { _getBufferMemoryRequirements = _getBufferMemoryRequirements ?? GetDeviceProcAddr<GetBufferMemoryRequirementsDelegate>(FunctionName.GetBufferMemoryRequirements); }
        public void BindBufferMemory() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetImageMemoryRequirements() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void BindImageMemory() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetImageSparseMemoryRequirements() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateFence() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyFence() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ResetFences() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetFenceStatus() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void WaitForFences() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateSemaphore() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroySemaphore() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateEvent() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyEvent() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetEventStatus() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void SetEvent() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ResetEvent() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateQueryPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyQueryPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetQueryPoolResults() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateBuffer() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyBuffer() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateBufferView() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyBufferView() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateImage() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyImage() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetImageSubresourceLayout() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateImageView() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyImageView() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateShaderModule() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyShaderModule() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreatePipelineCache() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyPipelineCache() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetPipelineCacheData() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void MergePipelineCaches() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateGraphicsPipelines() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateComputePipelines() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyPipeline() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreatePipelineLayout() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyPipelineLayout() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateSampler() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroySampler() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateDescriptorSetLayout() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyDescriptorSetLayout() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateDescriptorPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyDescriptorPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ResetDescriptorPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void AllocateDescriptorSets() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void FreeDescriptorSets() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void UpdateDescriptorSets() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateFramebuffer() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyFramebuffer() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateRenderPass() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyRenderPass() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetRenderAreaGranularity() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateCommandPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyCommandPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ResetCommandPool() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void AllocateCommandBuffers() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void FreeCommandBuffers() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateSharedSwapchainsKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateSwapchainKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroySwapchainKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetSwapchainImagesKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void AcquireNextImageKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DebugMarkerSetObjectNameEXT() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DebugMarkerSetObjectTagEXT() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetMemoryWin32HandleNV() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateIndirectCommandsLayoutNVX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyIndirectCommandsLayoutNVX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateObjectTableNVX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyObjectTableNVX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void RegisterObjectsNVX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void UnregisterObjectsNVX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void TrimCommandPoolKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetMemoryWin32HandleKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetMemoryWin32HandlePropertiesKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetMemoryFdKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetMemoryFdPropertiesKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetSemaphoreWin32HandleKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ImportSemaphoreWin32HandleKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetSemaphoreFdKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ImportSemaphoreFdKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetFenceWin32HandleKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ImportFenceWin32HandleKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetFenceFdKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void ImportFenceFdKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DisplayPowerControlEXT() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void RegisterEventEXT() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void RegisterDisplayEventEXT() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetSwapchainCounterEXT() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetGroupPeerMemoryFeaturesKHX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void BindBufferMemory2KHX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void BindImageMemory2KHX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetGroupPresentCapabilitiesKHX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetGroupSurfacePresentModesKHX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void AcquireNextImage2KHX() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void CreateDescriptorUpdateTemplateKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void DestroyDescriptorUpdateTemplateKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void UpdateDescriptorSetWithTemplateKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void SetHdrMetadataEXT() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetSwapchainStatusKHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetRefreshCycleDurationGOOGLE() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetPastPresentationTimingGOOGLE() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetBufferMemoryRequirements2KHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetImageMemoryRequirements2KHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
        public void GetImageSparseMemoryRequirements2KHR() { _destroyDevice = _destroyDevice ?? GetDeviceProcAddr<DestroyDeviceDelegate>(FunctionName.DestroyDevice); }
    }
}