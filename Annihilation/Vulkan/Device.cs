using System;
using System.Runtime.InteropServices;

namespace Annihilation.Vk
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

        public void GetQueue(uint queueFamilyIndex, uint queueIndex, out Queue queue)
        {
            _getDeviceQueue = _getDeviceQueue ?? GetDeviceProcAddr<GetDeviceQueueDelegate>(FunctionName.GetDeviceQueue);

            _getDeviceQueue(Handle, queueFamilyIndex, queueIndex, out QueueHandle handle);

            queue = new Queue(handle, this);
        }

        public void WaitIdle()
        {
            _deviceWaitIdle = _deviceWaitIdle ?? GetDeviceProcAddr<DeviceWaitIdleDelegate>(FunctionName.DeviceWaitIdle);

            _deviceWaitIdle(Handle).CheckError();
        }

        public void AllocateMemory(ref MemoryAllocateInfo memoryAllocateInfo, out DeviceMemoryHandle deviceMemoryHandle)
        {
            _allocateMemory = _allocateMemory ?? GetDeviceProcAddr<AllocateMemoryDelegate>(FunctionName.AllocateMemory);

            _allocateMemory(Handle, ref memoryAllocateInfo, null, out deviceMemoryHandle).CheckError();
        }

        public void FreeMemory(DeviceMemoryHandle deviceMemoryHandle)
        {
            _freeMemory = _freeMemory ?? GetDeviceProcAddr<FreeMemoryDelegate>(FunctionName.FreeMemory);

            _freeMemory(Handle, deviceMemoryHandle, null);
        }

        public void MapMemory(DeviceMemoryHandle memory, DeviceSize offset, DeviceSize size, MemoryMapFlags flags, void** data)
        {
            _mapMemory = _mapMemory ?? GetDeviceProcAddr<MapMemoryDelegate>(FunctionName.MapMemory);

            _mapMemory(Handle, memory, offset, size, flags, data).CheckError();
        }

        public void UnmapMemory(DeviceMemoryHandle memory)
        {
            _unmapMemory = _unmapMemory ?? GetDeviceProcAddr<UnmapMemoryDelegate>(FunctionName.UnmapMemory);

            _unmapMemory(Handle, memory);
        }

        public void FlushMappedMemoryRanges(uint memoryRangeCount, MappedMemoryRange* memoryRanges)
        {
            _flushMappedMemoryRanges = _flushMappedMemoryRanges ?? GetDeviceProcAddr<FlushMappedMemoryRangesDelegate>(FunctionName.FlushMappedMemoryRanges);

            _flushMappedMemoryRanges(Handle, memoryRangeCount, memoryRanges).CheckError();
        }

        public void InvalidateMappedMemoryRanges(uint memoryRangeCount, MappedMemoryRange* memoryRanges)
        {
            _invalidateMappedMemoryRanges = _invalidateMappedMemoryRanges ?? GetDeviceProcAddr<InvalidateMappedMemoryRangesDelegate>(FunctionName.InvalidateMappedMemoryRanges);

            _invalidateMappedMemoryRanges(Handle, memoryRangeCount, memoryRanges).CheckError();
        }

        public void GetMemoryCommitment(DeviceMemoryHandle memory, out DeviceSize committedMemoryInBytes)
        {
            _getDeviceMemoryCommitment = _getDeviceMemoryCommitment ?? GetDeviceProcAddr<GetDeviceMemoryCommitmentDelegate>(FunctionName.GetDeviceMemoryCommitment);

            _getDeviceMemoryCommitment(Handle, memory, out committedMemoryInBytes);
        }

        public void GetBufferMemoryRequirements(BufferHandle buffer, out MemoryRequirements memoryRequirements)
        {
            _getBufferMemoryRequirements = _getBufferMemoryRequirements ??
                                           GetDeviceProcAddr<GetBufferMemoryRequirementsDelegate>(FunctionName
                                               .GetBufferMemoryRequirements);

            _getBufferMemoryRequirements(Handle, buffer, out memoryRequirements);
        }

        public void BindBufferMemory()
        {
            _bindBufferMemory = _bindBufferMemory ??
                                GetDeviceProcAddr<BindBufferMemoryDelegate>(FunctionName.BindBufferMemory);

            //_bindBufferMemory(Handle, )
        }

        public void GetImageMemoryRequirements()
        {
            _getImageMemoryRequirements = _getImageMemoryRequirements ??
                                          GetDeviceProcAddr<GetImageMemoryRequirementsDelegate>(FunctionName
                                              .GetImageMemoryRequirements);
        }

        public void BindImageMemory()
        {
            _bindImageMemory = _bindImageMemory ??
                               GetDeviceProcAddr<BindImageMemoryDelegate>(FunctionName.BindImageMemory);
        }

        public void GetImageSparseMemoryRequirements()
        {
            _getImageSparseMemoryRequirements = _getImageSparseMemoryRequirements ??
                                                GetDeviceProcAddr<GetImageSparseMemoryRequirementsDelegate>(FunctionName
                                                    .GetImageSparseMemoryRequirements);
        }

        public void CreateFence()
        {
            _createFence = _createFence ?? GetDeviceProcAddr<CreateFenceDelegate>(FunctionName.CreateFence);
        }

        public void DestroyFence()
        {
            _destroyFence = _destroyFence ?? GetDeviceProcAddr<DestroyFenceDelegate>(FunctionName.DestroyFence);
        }

        public void ResetFences()
        {
            _resetFences = _resetFences ?? GetDeviceProcAddr<ResetFencesDelegate>(FunctionName.ResetFences);
        }

        public void GetFenceStatus()
        {
            _getFenceStatus = _getFenceStatus ?? GetDeviceProcAddr<GetFenceStatusDelegate>(FunctionName.GetFenceStatus);
        }

        public void WaitForFences()
        {
            _waitForFences = _waitForFences ?? GetDeviceProcAddr<WaitForFencesDelegate>(FunctionName.WaitForFences);
        }

        public void CreateSemaphore()
        {
            _createSemaphore = _createSemaphore ??
                               GetDeviceProcAddr<CreateSemaphoreDelegate>(FunctionName.CreateSemaphore);
        }

        public void DestroySemaphore()
        {
            _destroySemaphore = _destroySemaphore ??
                                GetDeviceProcAddr<DestroySemaphoreDelegate>(FunctionName.DestroySemaphore);
        }

        public void CreateEvent()
        {
            _createEvent = _createEvent ?? GetDeviceProcAddr<CreateEventDelegate>(FunctionName.CreateEvent);
        }

        public void DestroyEvent()
        {
            _destroyEvent = _destroyEvent ?? GetDeviceProcAddr<DestroyEventDelegate>(FunctionName.DestroyEvent);
        }

        public void GetEventStatus()
        {
            _getEventStatus = _getEventStatus ?? GetDeviceProcAddr<GetEventStatusDelegate>(FunctionName.GetEventStatus);
        }

        public void SetEvent()
        {
            _setEvent = _setEvent ?? GetDeviceProcAddr<SetEventDelegate>(FunctionName.SetEvent);
        }

        public void ResetEvent()
        {
            _resetEvent = _resetEvent ?? GetDeviceProcAddr<ResetEventDelegate>(FunctionName.ResetEvent);
        }

        public void CreateQueryPool()
        {
            _createQueryPool = _createQueryPool ??
                               GetDeviceProcAddr<CreateQueryPoolDelegate>(FunctionName.CreateQueryPool);
        }

        public void DestroyQueryPool()
        {
            _destroyQueryPool = _destroyQueryPool ??
                                GetDeviceProcAddr<DestroyQueryPoolDelegate>(FunctionName.DestroyQueryPool);
        }

        public void GetQueryPoolResults()
        {
            _getQueryPoolResults = _getQueryPoolResults ??
                                   GetDeviceProcAddr<GetQueryPoolResultsDelegate>(FunctionName.GetQueryPoolResults);
        }

        public void CreateBuffer()
        {
            _createBuffer = _createBuffer ?? GetDeviceProcAddr<CreateBufferDelegate>(FunctionName.CreateBuffer);
        }

        public void DestroyBuffer()
        {
            _destroyBuffer = _destroyBuffer ?? GetDeviceProcAddr<DestroyBufferDelegate>(FunctionName.DestroyBuffer);
        }

        public void CreateBufferView()
        {
            _createBufferView = _createBufferView ??
                                GetDeviceProcAddr<CreateBufferViewDelegate>(FunctionName.CreateBufferView);
        }

        public void DestroyBufferView()
        {
            _destroyBufferView = _destroyBufferView ??
                                 GetDeviceProcAddr<DestroyBufferViewDelegate>(FunctionName.DestroyBufferView);
        }

        public void CreateImage()
        {
            _createImage = _createImage ?? GetDeviceProcAddr<CreateImageDelegate>(FunctionName.CreateImage);
        }

        public void DestroyImage()
        {
            _destroyImage = _destroyImage ?? GetDeviceProcAddr<DestroyImageDelegate>(FunctionName.DestroyImage);
        }

        public void GetImageSubresourceLayout()
        {
            _getImageSubresourceLayout = _getImageSubresourceLayout ??
                                         GetDeviceProcAddr<GetImageSubresourceLayoutDelegate>(FunctionName
                                             .GetImageSubresourceLayout);
        }

        public void CreateImageView()
        {
            _createImageView = _createImageView ??
                               GetDeviceProcAddr<CreateImageViewDelegate>(FunctionName.CreateImageView);
        }

        public void DestroyImageView()
        {
            _destroyImageView = _destroyImageView ??
                                GetDeviceProcAddr<DestroyImageViewDelegate>(FunctionName.DestroyImageView);
        }

        public void CreateShaderModule()
        {
            _createShaderModule = _createShaderModule ??
                                  GetDeviceProcAddr<CreateShaderModuleDelegate>(FunctionName.CreateShaderModule);
        }

        public void DestroyShaderModule()
        {
            _destroyShaderModule = _destroyShaderModule ??
                                   GetDeviceProcAddr<DestroyShaderModuleDelegate>(FunctionName.DestroyShaderModule);
        }

        public void CreatePipelineCache()
        {
            _createPipelineCache = _createPipelineCache ??
                                   GetDeviceProcAddr<CreatePipelineCacheDelegate>(FunctionName.CreatePipelineCache);
        }

        public void DestroyPipelineCache()
        {
            _destroyPipelineCache = _destroyPipelineCache ??
                                    GetDeviceProcAddr<DestroyPipelineCacheDelegate>(FunctionName.DestroyPipelineCache);
        }

        public void GetPipelineCacheData()
        {
            _getPipelineCacheData = _getPipelineCacheData ??
                                    GetDeviceProcAddr<GetPipelineCacheDataDelegate>(FunctionName.GetPipelineCacheData);
        }

        public void MergePipelineCaches()
        {
            _mergePipelineCaches = _mergePipelineCaches ??
                                   GetDeviceProcAddr<MergePipelineCachesDelegate>(FunctionName.MergePipelineCaches);
        }

        public void CreateGraphicsPipelines()
        {
            _createGraphicsPipelines = _createGraphicsPipelines ??
                                       GetDeviceProcAddr<CreateGraphicsPipelinesDelegate>(FunctionName
                                           .CreateGraphicsPipelines);
        }

        public void CreateComputePipelines()
        {
            _createComputePipelines = _createComputePipelines ??
                                      GetDeviceProcAddr<CreateComputePipelinesDelegate>(FunctionName
                                          .CreateComputePipelines);
        }

        public void DestroyPipeline()
        {
            _destroyPipeline = _destroyPipeline ??
                               GetDeviceProcAddr<DestroyPipelineDelegate>(FunctionName.DestroyPipeline);
        }

        public void CreatePipelineLayout()
        {
            _createPipelineLayout = _createPipelineLayout ??
                                    GetDeviceProcAddr<CreatePipelineLayoutDelegate>(FunctionName.CreatePipelineLayout);
        }

        public void DestroyPipelineLayout()
        {
            _destroyPipelineLayout = _destroyPipelineLayout ??
                                     GetDeviceProcAddr<DestroyPipelineLayoutDelegate>(
                                         FunctionName.DestroyPipelineLayout);
        }

        public void CreateSampler()
        {
            _createSampler = _createSampler ?? GetDeviceProcAddr<CreateSamplerDelegate>(FunctionName.CreateSampler);
        }

        public void DestroySampler()
        {
            _destroySampler = _destroySampler ?? GetDeviceProcAddr<DestroySamplerDelegate>(FunctionName.DestroySampler);
        }

        public void CreateDescriptorSetLayout()
        {
            _createDescriptorSetLayout = _createDescriptorSetLayout ??
                                         GetDeviceProcAddr<CreateDescriptorSetLayoutDelegate>(FunctionName
                                             .CreateDescriptorSetLayout);
        }

        public void DestroyDescriptorSetLayout()
        {
            _destroyDescriptorSetLayout = _destroyDescriptorSetLayout ??
                                          GetDeviceProcAddr<DestroyDescriptorSetLayoutDelegate>(FunctionName
                                              .DestroyDescriptorSetLayout);
        }

        public void CreateDescriptorPool()
        {
            _createDescriptorPool = _createDescriptorPool ??
                                    GetDeviceProcAddr<CreateDescriptorPoolDelegate>(FunctionName.CreateDescriptorPool);
        }

        public void DestroyDescriptorPool()
        {
            _destroyDescriptorPool = _destroyDescriptorPool ??
                                     GetDeviceProcAddr<DestroyDescriptorPoolDelegate>(
                                         FunctionName.DestroyDescriptorPool);
        }

        public void ResetDescriptorPool()
        {
            _resetDescriptorPool = _resetDescriptorPool ??
                                   GetDeviceProcAddr<ResetDescriptorPoolDelegate>(FunctionName.ResetDescriptorPool);
        }

        public void AllocateDescriptorSets()
        {
            _allocateDescriptorSets = _allocateDescriptorSets ??
                                      GetDeviceProcAddr<AllocateDescriptorSetsDelegate>(FunctionName
                                          .AllocateDescriptorSets);
        }

        public void FreeDescriptorSets()
        {
            _freeDescriptorSets = _freeDescriptorSets ??
                                  GetDeviceProcAddr<FreeDescriptorSetsDelegate>(FunctionName.FreeDescriptorSets);
        }

        public void UpdateDescriptorSets()
        {
            _updateDescriptorSets = _updateDescriptorSets ??
                                    GetDeviceProcAddr<UpdateDescriptorSetsDelegate>(FunctionName.UpdateDescriptorSets);
        }

        public void CreateFramebuffer()
        {
            _createFramebuffer = _createFramebuffer ??
                                 GetDeviceProcAddr<CreateFramebufferDelegate>(FunctionName.CreateFramebuffer);
        }

        public void DestroyFramebuffer()
        {
            _destroyFramebuffer = _destroyFramebuffer ??
                                  GetDeviceProcAddr<DestroyFramebufferDelegate>(FunctionName.DestroyFramebuffer);
        }

        public void CreateRenderPass()
        {
            _createRenderPass = _createRenderPass ??
                                GetDeviceProcAddr<CreateRenderPassDelegate>(FunctionName.CreateRenderPass);
        }

        public void DestroyRenderPass()
        {
            _destroyRenderPass = _destroyRenderPass ??
                                 GetDeviceProcAddr<DestroyRenderPassDelegate>(FunctionName.DestroyRenderPass);
        }

        public void GetRenderAreaGranularity()
        {
            _getRenderAreaGranularity = _getRenderAreaGranularity ??
                                        GetDeviceProcAddr<GetRenderAreaGranularityDelegate>(FunctionName
                                            .GetRenderAreaGranularity);
        }

        public void CreateCommandPool()
        {
            _createCommandPool = _createCommandPool ??
                                 GetDeviceProcAddr<CreateCommandPoolDelegate>(FunctionName.CreateCommandPool);
        }

        public void DestroyCommandPool()
        {
            _destroyCommandPool = _destroyCommandPool ??
                                  GetDeviceProcAddr<DestroyCommandPoolDelegate>(FunctionName.DestroyCommandPool);
        }

        public void ResetCommandPool()
        {
            _resetCommandPool = _resetCommandPool ??
                                GetDeviceProcAddr<ResetCommandPoolDelegate>(FunctionName.ResetCommandPool);
        }

        public void AllocateCommandBuffers()
        {
            _allocateCommandBuffers = _allocateCommandBuffers ??
                                      GetDeviceProcAddr<AllocateCommandBuffersDelegate>(FunctionName
                                          .AllocateCommandBuffers);
        }

        public void FreeCommandBuffers()
        {
            _freeCommandBuffers = _freeCommandBuffers ??
                                  GetDeviceProcAddr<FreeCommandBuffersDelegate>(FunctionName.FreeCommandBuffers);
        }

        public void CreateSharedSwapchainsKHR()
        {
            _createSharedSwapchainsKHR = _createSharedSwapchainsKHR ??
                                         GetDeviceProcAddr<CreateSharedSwapchainsKHRDelegate>(FunctionName
                                             .CreateSharedSwapchainsKHR);
        }

        public void CreateSwapchainKHR()
        {
            _createSwapchainKHR = _createSwapchainKHR ??
                                  GetDeviceProcAddr<CreateSwapchainKHRDelegate>(FunctionName.CreateSwapchainKHR);
        }

        public void DestroySwapchainKHR()
        {
            _destroySwapchainKHR = _destroySwapchainKHR ??
                                   GetDeviceProcAddr<DestroySwapchainKHRDelegate>(FunctionName.DestroySwapchainKHR);
        }

        public void GetSwapchainImagesKHR()
        {
            _getSwapchainImagesKHR = _getSwapchainImagesKHR ??
                                     GetDeviceProcAddr<GetSwapchainImagesKHRDelegate>(
                                         FunctionName.GetSwapchainImagesKHR);
        }

        public void AcquireNextImageKHR()
        {
            _acquireNextImageKHR = _acquireNextImageKHR ??
                                   GetDeviceProcAddr<AcquireNextImageKHRDelegate>(FunctionName.AcquireNextImageKHR);
        }

        public void DebugMarkerSetObjectNameEXT()
        {
            _debugMarkerSetObjectNameEXT = _debugMarkerSetObjectNameEXT ??
                                           GetDeviceProcAddr<DebugMarkerSetObjectNameEXTDelegate>(FunctionName
                                               .DebugMarkerSetObjectNameEXT);
        }

        public void DebugMarkerSetObjectTagEXT()
        {
            _debugMarkerSetObjectTagEXT = _debugMarkerSetObjectTagEXT ??
                                          GetDeviceProcAddr<DebugMarkerSetObjectTagEXTDelegate>(FunctionName
                                              .DebugMarkerSetObjectTagEXT);
        }

        public void GetMemoryWin32HandleNV()
        {
            _getMemoryWin32HandleNV = _getMemoryWin32HandleNV ??
                                      GetDeviceProcAddr<GetMemoryWin32HandleNVDelegate>(FunctionName
                                          .GetMemoryWin32HandleNV);
        }

        public void CreateIndirectCommandsLayoutNVX()
        {
            _createIndirectCommandsLayoutNVX = _createIndirectCommandsLayoutNVX ??
                                               GetDeviceProcAddr<CreateIndirectCommandsLayoutNVXDelegate>(FunctionName
                                                   .CreateIndirectCommandsLayoutNVX);
        }

        public void DestroyIndirectCommandsLayoutNVX()
        {
            _destroyIndirectCommandsLayoutNVX = _destroyIndirectCommandsLayoutNVX ??
                                                GetDeviceProcAddr<DestroyIndirectCommandsLayoutNVXDelegate>(FunctionName
                                                    .DestroyIndirectCommandsLayoutNVX);
        }

        public void CreateObjectTableNVX()
        {
            _createObjectTableNVX = _createObjectTableNVX ??
                                    GetDeviceProcAddr<CreateObjectTableNVXDelegate>(FunctionName.CreateObjectTableNVX);
        }

        public void DestroyObjectTableNVX()
        {
            _destroyObjectTableNVX = _destroyObjectTableNVX ??
                                     GetDeviceProcAddr<DestroyObjectTableNVXDelegate>(
                                         FunctionName.DestroyObjectTableNVX);
        }

        public void RegisterObjectsNVX()
        {
            _registerObjectsNVX = _registerObjectsNVX ??
                                  GetDeviceProcAddr<RegisterObjectsNVXDelegate>(FunctionName.RegisterObjectsNVX);
        }

        public void UnregisterObjectsNVX()
        {
            _unregisterObjectsNVX = _unregisterObjectsNVX ??
                                    GetDeviceProcAddr<UnregisterObjectsNVXDelegate>(FunctionName.UnregisterObjectsNVX);
        }

        public void TrimCommandPoolKHR()
        {
            _trimCommandPoolKHR = _trimCommandPoolKHR ??
                                  GetDeviceProcAddr<TrimCommandPoolKHRDelegate>(FunctionName.TrimCommandPoolKHR);
        }

        public void GetMemoryWin32HandleKHR()
        {
            _getMemoryWin32HandleKHR = _getMemoryWin32HandleKHR ??
                                       GetDeviceProcAddr<GetMemoryWin32HandleKHRDelegate>(FunctionName
                                           .GetMemoryWin32HandleKHR);
        }

        public void GetMemoryWin32HandlePropertiesKHR()
        {
            _getMemoryWin32HandlePropertiesKHR = _getMemoryWin32HandlePropertiesKHR ??
                                                 GetDeviceProcAddr<GetMemoryWin32HandlePropertiesKHRDelegate>(
                                                     FunctionName.GetMemoryWin32HandlePropertiesKHR);
        }

        public void GetMemoryFdKHR()
        {
            _getMemoryFdKHR = _getMemoryFdKHR ?? GetDeviceProcAddr<GetMemoryFdKHRDelegate>(FunctionName.GetMemoryFdKHR);
        }

        public void GetMemoryFdPropertiesKHR()
        {
            _getMemoryFdPropertiesKHR = _getMemoryFdPropertiesKHR ??
                                        GetDeviceProcAddr<GetMemoryFdPropertiesKHRDelegate>(FunctionName
                                            .GetMemoryFdPropertiesKHR);
        }

        public void GetSemaphoreWin32HandleKHR()
        {
            _getSemaphoreWin32HandleKHR = _getSemaphoreWin32HandleKHR ??
                                          GetDeviceProcAddr<GetSemaphoreWin32HandleKHRDelegate>(FunctionName
                                              .GetSemaphoreWin32HandleKHR);
        }

        public void ImportSemaphoreWin32HandleKHR()
        {
            _importSemaphoreWin32HandleKHR = _importSemaphoreWin32HandleKHR ??
                                             GetDeviceProcAddr<ImportSemaphoreWin32HandleKHRDelegate>(FunctionName
                                                 .ImportSemaphoreWin32HandleKHR);
        }

        public void GetSemaphoreFdKHR()
        {
            _getSemaphoreFdKHR = _getSemaphoreFdKHR ??
                                 GetDeviceProcAddr<GetSemaphoreFdKHRDelegate>(FunctionName.GetSemaphoreFdKHR);
        }

        public void ImportSemaphoreFdKHR()
        {
            _importSemaphoreFdKHR = _importSemaphoreFdKHR ??
                                    GetDeviceProcAddr<ImportSemaphoreFdKHRDelegate>(FunctionName.ImportSemaphoreFdKHR);
        }

        public void GetFenceWin32HandleKHR()
        {
            _getFenceWin32HandleKHR = _getFenceWin32HandleKHR ??
                                      GetDeviceProcAddr<GetFenceWin32HandleKHRDelegate>(FunctionName
                                          .GetFenceWin32HandleKHR);
        }

        public void ImportFenceWin32HandleKHR()
        {
            _importFenceWin32HandleKHR = _importFenceWin32HandleKHR ??
                                         GetDeviceProcAddr<ImportFenceWin32HandleKHRDelegate>(FunctionName
                                             .ImportFenceWin32HandleKHR);
        }

        public void GetFenceFdKHR()
        {
            _getFenceFdKHR = _getFenceFdKHR ?? GetDeviceProcAddr<GetFenceFdKHRDelegate>(FunctionName.GetFenceFdKHR);
        }

        public void ImportFenceFdKHR()
        {
            _importFenceFdKHR = _importFenceFdKHR ??
                                GetDeviceProcAddr<ImportFenceFdKHRDelegate>(FunctionName.ImportFenceFdKHR);
        }

        public void DisplayPowerControlEXT()
        {
            _displayPowerControlEXT = _displayPowerControlEXT ??
                                      GetDeviceProcAddr<DisplayPowerControlEXTDelegate>(FunctionName
                                          .DisplayPowerControlEXT);
        }

        public void RegisterEventEXT()
        {
            _registerDeviceEventEXT = _registerDeviceEventEXT ??
                                      GetDeviceProcAddr<RegisterDeviceEventEXTDelegate>(FunctionName
                                          .RegisterDeviceEventEXT);
        }

        public void RegisterDisplayEventEXT()
        {
            _registerDisplayEventEXT = _registerDisplayEventEXT ??
                                       GetDeviceProcAddr<RegisterDisplayEventEXTDelegate>(FunctionName
                                           .RegisterDisplayEventEXT);
        }

        public void GetSwapchainCounterEXT()
        {
            _getSwapchainCounterEXT = _getSwapchainCounterEXT ??
                                      GetDeviceProcAddr<GetSwapchainCounterEXTDelegate>(FunctionName
                                          .GetSwapchainCounterEXT);
        }

        public void GetGroupPeerMemoryFeaturesKHX()
        {
            _getDeviceGroupPeerMemoryFeaturesKHX = _getDeviceGroupPeerMemoryFeaturesKHX ??
                                                   GetDeviceProcAddr<GetDeviceGroupPeerMemoryFeaturesKHXDelegate>(
                                                       FunctionName.GetDeviceGroupPeerMemoryFeaturesKHX);
        }

        public void BindBufferMemory2KHX()
        {
            _bindBufferMemory2KHX = _bindBufferMemory2KHX ??
                                    GetDeviceProcAddr<BindBufferMemory2KHXDelegate>(FunctionName.BindBufferMemory2KHX);
        }

        public void BindImageMemory2KHX()
        {
            _bindImageMemory2KHX = _bindImageMemory2KHX ??
                                   GetDeviceProcAddr<BindImageMemory2KHXDelegate>(FunctionName.BindImageMemory2KHX);
        }

        public void GetGroupPresentCapabilitiesKHX()
        {
            _getDeviceGroupPresentCapabilitiesKHX = _getDeviceGroupPresentCapabilitiesKHX ??
                                                    GetDeviceProcAddr<GetDeviceGroupPresentCapabilitiesKHXDelegate>(
                                                        FunctionName.GetDeviceGroupPresentCapabilitiesKHX);
        }

        public void GetGroupSurfacePresentModesKHX()
        {
            _getDeviceGroupSurfacePresentModesKHX = _getDeviceGroupSurfacePresentModesKHX ??
                                                    GetDeviceProcAddr<GetDeviceGroupSurfacePresentModesKHXDelegate>(
                                                        FunctionName.GetDeviceGroupSurfacePresentModesKHX);
        }

        public void AcquireNextImage2KHX()
        {
            _acquireNextImage2KHX = _acquireNextImage2KHX ??
                                    GetDeviceProcAddr<AcquireNextImage2KHXDelegate>(FunctionName.AcquireNextImage2KHX);
        }

        public void CreateDescriptorUpdateTemplateKHR()
        {
            _createDescriptorUpdateTemplateKHR = _createDescriptorUpdateTemplateKHR ??
                                                 GetDeviceProcAddr<CreateDescriptorUpdateTemplateKHRDelegate>(
                                                     FunctionName.CreateDescriptorUpdateTemplateKHR);
        }

        public void DestroyDescriptorUpdateTemplateKHR()
        {
            _destroyDescriptorUpdateTemplateKHR = _destroyDescriptorUpdateTemplateKHR ??
                                                  GetDeviceProcAddr<DestroyDescriptorUpdateTemplateKHRDelegate>(
                                                      FunctionName.DestroyDescriptorUpdateTemplateKHR);
        }

        public void UpdateDescriptorSetWithTemplateKHR()
        {
            _updateDescriptorSetWithTemplateKHR = _updateDescriptorSetWithTemplateKHR ??
                                                  GetDeviceProcAddr<UpdateDescriptorSetWithTemplateKHRDelegate>(
                                                      FunctionName.UpdateDescriptorSetWithTemplateKHR);
        }

        public void SetHdrMetadataEXT()
        {
            _setHdrMetadataEXT = _setHdrMetadataEXT ??
                                 GetDeviceProcAddr<SetHdrMetadataEXTDelegate>(FunctionName.SetHdrMetadataEXT);
        }

        public void GetSwapchainStatusKHR()
        {
            _getSwapchainStatusKHR = _getSwapchainStatusKHR ??
                                     GetDeviceProcAddr<GetSwapchainStatusKHRDelegate>(
                                         FunctionName.GetSwapchainStatusKHR);
        }

        public void GetRefreshCycleDurationGOOGLE()
        {
            _getRefreshCycleDurationGOOGLE = _getRefreshCycleDurationGOOGLE ??
                                             GetDeviceProcAddr<GetRefreshCycleDurationGOOGLEDelegate>(FunctionName
                                                 .GetRefreshCycleDurationGOOGLE);
        }

        public void GetPastPresentationTimingGOOGLE()
        {
            _getPastPresentationTimingGOOGLE = _getPastPresentationTimingGOOGLE ??
                                               GetDeviceProcAddr<GetPastPresentationTimingGOOGLEDelegate>(FunctionName
                                                   .GetPastPresentationTimingGOOGLE);
        }

        public void GetBufferMemoryRequirements2KHR()
        {
            _getBufferMemoryRequirements2KHR = _getBufferMemoryRequirements2KHR ??
                                               GetDeviceProcAddr<GetBufferMemoryRequirements2KHRDelegate>(FunctionName
                                                   .GetBufferMemoryRequirements2KHR);
        }

        public void GetImageMemoryRequirements2KHR()
        {
            _getImageMemoryRequirements2KHR = _getImageMemoryRequirements2KHR ??
                                              GetDeviceProcAddr<GetImageMemoryRequirements2KHRDelegate>(FunctionName
                                                  .GetImageMemoryRequirements2KHR);
        }

        public void GetImageSparseMemoryRequirements2KHR()
        {
            _getImageSparseMemoryRequirements2KHR = _getImageSparseMemoryRequirements2KHR ??
                                                    GetDeviceProcAddr<GetImageSparseMemoryRequirements2KHRDelegate>(
                                                        FunctionName.GetImageSparseMemoryRequirements2KHR);
        }
    }
}