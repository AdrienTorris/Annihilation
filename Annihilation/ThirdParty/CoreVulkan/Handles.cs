using System;
using System.Runtime.InteropServices;
using SDL2;

namespace Vulkan
{
    public struct VkInstance : IEquatable<VkInstance>
    {
        public IntPtr Handle;

        public static readonly VkInstance Null;

        public bool Equals(VkInstance other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkInstance && this == (VkInstance)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkInstance left, VkInstance right) => left.Equals(right);
        public static bool operator !=(VkInstance left, VkInstance right) => !left.Equals(right);

        public static implicit operator IntPtr(VkInstance instance) => instance.Handle;
        public static implicit operator VkInstance(IntPtr handle) => new VkInstance { Handle = handle };
        public static implicit operator SDL.VkInstance(VkInstance instance) => new SDL.VkInstance { Handle = instance.Handle };
        public static implicit operator VkInstance(SDL.VkInstance instance) => new VkInstance { Handle = instance.Handle };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VkPhysicalDevice : IEquatable<VkPhysicalDevice>
    {
        public IntPtr Handle;

        public readonly static VkPhysicalDevice Null;

        public bool Equals(VkPhysicalDevice other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkPhysicalDevice && this == (VkPhysicalDevice)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkPhysicalDevice left, VkPhysicalDevice right) => left.Equals(right);
        public static bool operator !=(VkPhysicalDevice left, VkPhysicalDevice right) => !left.Equals(right);

        public static implicit operator IntPtr(VkPhysicalDevice physicalDevice) => physicalDevice.Handle;
        public static implicit operator VkPhysicalDevice(IntPtr handle) => new VkPhysicalDevice { Handle = handle };
    }

    public struct VkDevice : IEquatable<VkDevice>
    {
        public IntPtr Handle;

        public static readonly VkDevice Null;

        public bool Equals(VkDevice other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDevice && this == (VkDevice)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDevice left, VkDevice right) => left.Equals(right);
        public static bool operator !=(VkDevice left, VkDevice right) => !left.Equals(right);

        public static implicit operator IntPtr(VkDevice device) => device.Handle;
        public static implicit operator VkDevice(IntPtr handle) => new VkDevice { Handle = handle };
    }

    public struct VkQueue : IEquatable<VkQueue>
    {
        public IntPtr Handle;

        public static readonly VkQueue Null;

        public bool Equals(VkQueue other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkQueue && this == (VkQueue)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkQueue left, VkQueue right) => left.Equals(right);
        public static bool operator !=(VkQueue left, VkQueue right) => !left.Equals(right);

        public static implicit operator IntPtr(VkQueue queue) => queue.Handle;
        public static implicit operator VkQueue(IntPtr handle) => new VkQueue { Handle = handle };
    }

    public struct VkCommandBuffer : IEquatable<VkCommandBuffer>
    {
        public IntPtr Handle;

        public static readonly VkCommandBuffer Null;

        public bool Equals(VkCommandBuffer other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkCommandBuffer && this == (VkCommandBuffer)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkCommandBuffer left, VkCommandBuffer right) => left.Equals(right);
        public static bool operator !=(VkCommandBuffer left, VkCommandBuffer right) => !left.Equals(right);

        public static implicit operator IntPtr(VkCommandBuffer commandBuffer) => commandBuffer.Handle;
        public static implicit operator VkCommandBuffer(IntPtr handle) => new VkCommandBuffer { Handle = handle };
    }

    //
    // Non dispatchable handles
    //
    public struct VkSemaphore : IEquatable<VkSemaphore>
    {
        public ulong Handle;

        public static readonly VkSemaphore Null;

        public bool Equals(VkSemaphore other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkSemaphore && this == (VkSemaphore)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkSemaphore left, VkSemaphore right) => left.Equals(right);
        public static bool operator !=(VkSemaphore left, VkSemaphore right) => !left.Equals(right);

        public static implicit operator ulong(VkSemaphore semaphore) => semaphore.Handle;
        public static implicit operator VkSemaphore(ulong handle) => new VkSemaphore { Handle = handle };
    }

    public struct VkFence : IEquatable<VkFence>
    {
        public ulong Handle;

        public static readonly VkFence Null;

        public bool Equals(VkFence other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkFence && this == (VkFence)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkFence left, VkFence right) => left.Equals(right);
        public static bool operator !=(VkFence left, VkFence right) => !left.Equals(right);

        public static implicit operator ulong(VkFence fence) => fence.Handle;
        public static implicit operator VkFence(ulong handle) => new VkFence { Handle = handle };
    }

    public struct VkDeviceMemory : IEquatable<VkDeviceMemory>
    {
        public ulong Handle;

        public static readonly VkDeviceMemory Null;

        public bool Equals(VkDeviceMemory other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDeviceMemory && this == (VkDeviceMemory)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDeviceMemory left, VkDeviceMemory right) => left.Equals(right);
        public static bool operator !=(VkDeviceMemory left, VkDeviceMemory right) => !left.Equals(right);

        public static implicit operator ulong(VkDeviceMemory deviceMemory) => deviceMemory.Handle;
        public static implicit operator VkDeviceMemory(ulong handle) => new VkDeviceMemory { Handle = handle };
    }

    public struct VkBuffer : IEquatable<VkBuffer>
    {
        public ulong Handle;

        public static readonly VkBuffer Null;

        public bool Equals(VkBuffer other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkBuffer && this == (VkBuffer)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkBuffer left, VkBuffer right) => left.Equals(right);
        public static bool operator !=(VkBuffer left, VkBuffer right) => !left.Equals(right);

        public static implicit operator ulong(VkBuffer buffer) => buffer.Handle;
        public static implicit operator VkBuffer(ulong handle) => new VkBuffer { Handle = handle };
    }

    public struct VkImage : IEquatable<VkImage>
    {
        public ulong Handle;

        public static readonly VkImage Null;

        public bool Equals(VkImage other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkImage && this == (VkImage)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkImage left, VkImage right) => left.Equals(right);
        public static bool operator !=(VkImage left, VkImage right) => !left.Equals(right);

        public static implicit operator ulong(VkImage image) => image.Handle;
        public static implicit operator VkImage(ulong handle) => new VkImage { Handle = handle };
    }

    public struct VkEvent : IEquatable<VkEvent>
    {
        public ulong Handle;

        public static readonly VkEvent Null;

        public bool Equals(VkEvent other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkEvent && this == (VkEvent)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkEvent left, VkEvent right) => left.Equals(right);
        public static bool operator !=(VkEvent left, VkEvent right) => !left.Equals(right);

        public static implicit operator ulong(VkEvent @event) => @event.Handle;
        public static implicit operator VkEvent(ulong handle) => new VkEvent { Handle = handle };
    }

    public struct VkQueryPool : IEquatable<VkQueryPool>
    {
        public ulong Handle;

        public static readonly VkQueryPool Null;

        public bool Equals(VkQueryPool other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkQueryPool && this == (VkQueryPool)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkQueryPool left, VkQueryPool right) => left.Equals(right);
        public static bool operator !=(VkQueryPool left, VkQueryPool right) => !left.Equals(right);

        public static implicit operator ulong(VkQueryPool queryPool) => queryPool.Handle;
        public static implicit operator VkQueryPool(ulong handle) => new VkQueryPool { Handle = handle };
    }

    public struct VkBufferView : IEquatable<VkBufferView>
    {
        public ulong Handle;

        public static readonly VkBufferView Null;

        public bool Equals(VkBufferView other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkBufferView && this == (VkBufferView)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkBufferView left, VkBufferView right) => left.Equals(right);
        public static bool operator !=(VkBufferView left, VkBufferView right) => !left.Equals(right);

        public static implicit operator ulong(VkBufferView bufferView) => bufferView.Handle;
        public static implicit operator VkBufferView(ulong handle) => new VkBufferView { Handle = handle };
    }

    public struct VkImageView : IEquatable<VkImageView>
    {
        public ulong Handle;

        public static readonly VkImageView Null;

        public bool Equals(VkImageView other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkImageView && this == (VkImageView)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkImageView left, VkImageView right) => left.Equals(right);
        public static bool operator !=(VkImageView left, VkImageView right) => !left.Equals(right);

        public static implicit operator ulong(VkImageView imageView) => imageView.Handle;
        public static implicit operator VkImageView(ulong handle) => new VkImageView { Handle = handle };
    }

    public struct VkShaderModule : IEquatable<VkShaderModule>
    {
        public ulong Handle;

        public static readonly VkShaderModule Null;

        public bool Equals(VkShaderModule other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkShaderModule && this == (VkShaderModule)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkShaderModule left, VkShaderModule right) => left.Equals(right);
        public static bool operator !=(VkShaderModule left, VkShaderModule right) => !left.Equals(right);

        public static implicit operator ulong(VkShaderModule shaderModule) => shaderModule.Handle;
        public static implicit operator VkShaderModule(ulong handle) => new VkShaderModule { Handle = handle };
    }

    public struct VkPipelineCache : IEquatable<VkPipelineCache>
    {
        public ulong Handle;

        public static readonly VkPipelineCache Null;

        public bool Equals(VkPipelineCache other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkPipelineCache && this == (VkPipelineCache)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkPipelineCache left, VkPipelineCache right) => left.Equals(right);
        public static bool operator !=(VkPipelineCache left, VkPipelineCache right) => !left.Equals(right);

        public static implicit operator ulong(VkPipelineCache pipelineCache) => pipelineCache.Handle;
        public static implicit operator VkPipelineCache(ulong handle) => new VkPipelineCache { Handle = handle };
    }

    public struct VkPipelineLayout : IEquatable<VkPipelineLayout>
    {
        public ulong Handle;

        public static readonly VkPipelineLayout Null;

        public bool Equals(VkPipelineLayout other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkPipelineLayout && this == (VkPipelineLayout)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkPipelineLayout left, VkPipelineLayout right) => left.Equals(right);
        public static bool operator !=(VkPipelineLayout left, VkPipelineLayout right) => !left.Equals(right);

        public static implicit operator ulong(VkPipelineLayout pipelineLayout) => pipelineLayout.Handle;
        public static implicit operator VkPipelineLayout(ulong handle) => new VkPipelineLayout { Handle = handle };
    }

    public struct VkRenderPass : IEquatable<VkRenderPass>
    {
        public ulong Handle;

        public static readonly VkRenderPass Null;

        public bool Equals(VkRenderPass other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkRenderPass && this == (VkRenderPass)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkRenderPass left, VkRenderPass right) => left.Equals(right);
        public static bool operator !=(VkRenderPass left, VkRenderPass right) => !left.Equals(right);

        public static implicit operator ulong(VkRenderPass renderPass) => renderPass.Handle;
        public static implicit operator VkRenderPass(ulong handle) => new VkRenderPass { Handle = handle };
    }

    public struct VkPipeline : IEquatable<VkPipeline>
    {
        public ulong Handle;

        public static readonly VkPipeline Null;

        public bool Equals(VkPipeline other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkPipeline && this == (VkPipeline)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkPipeline left, VkPipeline right) => left.Equals(right);
        public static bool operator !=(VkPipeline left, VkPipeline right) => !left.Equals(right);

        public static implicit operator ulong(VkPipeline pipeline) => pipeline.Handle;
        public static implicit operator VkPipeline(ulong handle) => new VkPipeline { Handle = handle };
    }

    public struct VkDescriptorSetLayout : IEquatable<VkDescriptorSetLayout>
    {
        public ulong Handle;

        public static readonly VkDescriptorSetLayout Null;

        public bool Equals(VkDescriptorSetLayout other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDescriptorSetLayout && this == (VkDescriptorSetLayout)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDescriptorSetLayout left, VkDescriptorSetLayout right) => left.Equals(right);
        public static bool operator !=(VkDescriptorSetLayout left, VkDescriptorSetLayout right) => !left.Equals(right);

        public static implicit operator ulong(VkDescriptorSetLayout descriptorSetLayout) => descriptorSetLayout.Handle;
        public static implicit operator VkDescriptorSetLayout(ulong handle) => new VkDescriptorSetLayout { Handle = handle };
    }

    public struct VkSampler : IEquatable<VkSampler>
    {
        public ulong Handle;

        public static readonly VkSampler Null;

        public bool Equals(VkSampler other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkSampler && this == (VkSampler)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkSampler left, VkSampler right) => left.Equals(right);
        public static bool operator !=(VkSampler left, VkSampler right) => !left.Equals(right);

        public static implicit operator ulong(VkSampler sampler) => sampler.Handle;
        public static implicit operator VkSampler(ulong handle) => new VkSampler { Handle = handle };
    }

    public struct VkDescriptorPool : IEquatable<VkDescriptorPool>
    {
        public ulong Handle;

        public static readonly VkDescriptorPool Null;

        public bool Equals(VkDescriptorPool other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDescriptorPool && this == (VkDescriptorPool)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDescriptorPool left, VkDescriptorPool right) => left.Equals(right);
        public static bool operator !=(VkDescriptorPool left, VkDescriptorPool right) => !left.Equals(right);

        public static implicit operator ulong(VkDescriptorPool descriptorPool) => descriptorPool.Handle;
        public static implicit operator VkDescriptorPool(ulong handle) => new VkDescriptorPool { Handle = handle };
    }

    public struct VkDescriptorSet : IEquatable<VkDescriptorSet>
    {
        public ulong Handle;

        public static readonly VkDescriptorSet Null;

        public bool Equals(VkDescriptorSet other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDescriptorSet && this == (VkDescriptorSet)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDescriptorSet left, VkDescriptorSet right) => left.Equals(right);
        public static bool operator !=(VkDescriptorSet left, VkDescriptorSet right) => !left.Equals(right);

        public static implicit operator ulong(VkDescriptorSet descriptorSet) => descriptorSet.Handle;
        public static implicit operator VkDescriptorSet(ulong handle) => new VkDescriptorSet { Handle = handle };
    }

    public struct VkFramebuffer : IEquatable<VkFramebuffer>
    {
        public ulong Handle;

        public static readonly VkFramebuffer Null;

        public bool Equals(VkFramebuffer other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkFramebuffer && this == (VkFramebuffer)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkFramebuffer left, VkFramebuffer right) => left.Equals(right);
        public static bool operator !=(VkFramebuffer left, VkFramebuffer right) => !left.Equals(right);

        public static implicit operator ulong(VkFramebuffer framebuffer) => framebuffer.Handle;
        public static implicit operator VkFramebuffer(ulong handle) => new VkFramebuffer { Handle = handle };
    }

    public struct VkCommandPool : IEquatable<VkCommandPool>
    {
        public ulong Handle;

        public static readonly VkCommandPool Null;

        public bool Equals(VkCommandPool other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkCommandPool && this == (VkCommandPool)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkCommandPool left, VkCommandPool right) => left.Equals(right);
        public static bool operator !=(VkCommandPool left, VkCommandPool right) => !left.Equals(right);

        public static implicit operator ulong(VkCommandPool commandPool) => commandPool.Handle;
        public static implicit operator VkCommandPool(ulong handle) => new VkCommandPool { Handle = handle };
    }

    // Khronos
    public struct VkSurface : IEquatable<VkSurface>
    {
        public ulong Handle;

        public static readonly VkSurface Null;

        public bool Equals(VkSurface other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkSurface && this == (VkSurface)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkSurface left, VkSurface right) => left.Equals(right);
        public static bool operator !=(VkSurface left, VkSurface right) => !left.Equals(right);

        public static implicit operator ulong(VkSurface surface) => surface.Handle;
        public static implicit operator VkSurface(ulong handle) => new VkSurface { Handle = handle };
        public static implicit operator SDL.VkSurfaceKHR(VkSurface surface) => new SDL.VkSurfaceKHR { Handle = surface.Handle };
        public static implicit operator VkSurface(SDL.VkSurfaceKHR surface) => new VkSurface { Handle = surface.Handle };
    }

    public struct VkSwapchain : IEquatable<VkSwapchain>
    {
        public ulong Handle;

        public static readonly VkSwapchain Null;

        public bool Equals(VkSwapchain other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkSwapchain && this == (VkSwapchain)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkSwapchain left, VkSwapchain right) => left.Equals(right);
        public static bool operator !=(VkSwapchain left, VkSwapchain right) => !left.Equals(right);

        public static implicit operator ulong(VkSwapchain swapchain) => swapchain.Handle;
        public static implicit operator VkSwapchain(ulong handle) => new VkSwapchain { Handle = handle };
    }

    public struct VkDisplay : IEquatable<VkDisplay>
    {
        public ulong Handle;

        public static readonly VkDisplay Null;

        public bool Equals(VkDisplay other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDisplay && this == (VkDisplay)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDisplay left, VkDisplay right) => left.Equals(right);
        public static bool operator !=(VkDisplay left, VkDisplay right) => !left.Equals(right);

        public static implicit operator ulong(VkDisplay display) => display.Handle;
        public static implicit operator VkDisplay(ulong handle) => new VkDisplay { Handle = handle };
    }

    public struct VkDisplayMode : IEquatable<VkDisplayMode>
    {
        public ulong Handle;

        public static readonly VkDisplayMode Null;

        public bool Equals(VkDisplayMode other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDisplayMode && this == (VkDisplayMode)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDisplayMode left, VkDisplayMode right) => left.Equals(right);
        public static bool operator !=(VkDisplayMode left, VkDisplayMode right) => !left.Equals(right);

        public static implicit operator ulong(VkDisplayMode displayMode) => displayMode.Handle;
        public static implicit operator VkDisplayMode(ulong handle) => new VkDisplayMode { Handle = handle };
    }

    public struct VkDescriptorUpdateTemplate : IEquatable<VkDescriptorUpdateTemplate>
    {
        public ulong Handle;

        public static readonly VkDescriptorUpdateTemplate Null;

        public bool Equals(VkDescriptorUpdateTemplate other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDescriptorUpdateTemplate && this == (VkDescriptorUpdateTemplate)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDescriptorUpdateTemplate left, VkDescriptorUpdateTemplate right) => left.Equals(right);
        public static bool operator !=(VkDescriptorUpdateTemplate left, VkDescriptorUpdateTemplate right) => !left.Equals(right);

        public static implicit operator ulong(VkDescriptorUpdateTemplate descriptorUpdateTemplate) => descriptorUpdateTemplate.Handle;
        public static implicit operator VkDescriptorUpdateTemplate(ulong handle) => new VkDescriptorUpdateTemplate { Handle = handle };
    }

    // Multi-vendor
    public struct VkDebugReportCallback : IEquatable<VkDebugReportCallback>
    {
        public ulong Handle;

        public static readonly VkDebugReportCallback Null;

        public bool Equals(VkDebugReportCallback other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkDebugReportCallback && this == (VkDebugReportCallback)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkDebugReportCallback left, VkDebugReportCallback right) => left.Equals(right);
        public static bool operator !=(VkDebugReportCallback left, VkDebugReportCallback right) => !left.Equals(right);

        public static implicit operator ulong(VkDebugReportCallback debugReportCallback) => debugReportCallback.Handle;
        public static implicit operator VkDebugReportCallback(ulong handle) => new VkDebugReportCallback { Handle = handle };
    }

    // Nvidia
    public struct VkObjectTable : IEquatable<VkObjectTable>
    {
        public ulong Handle;

        public static readonly VkObjectTable Null;

        public bool Equals(VkObjectTable other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkObjectTable && this == (VkObjectTable)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkObjectTable left, VkObjectTable right) => left.Equals(right);
        public static bool operator !=(VkObjectTable left, VkObjectTable right) => !left.Equals(right);

        public static implicit operator ulong(VkObjectTable objectTable) => objectTable.Handle;
        public static implicit operator VkObjectTable(ulong handle) => new VkObjectTable { Handle = handle };
    }

    public struct VkIndirectCommandsLayout : IEquatable<VkIndirectCommandsLayout>
    {
        public ulong Handle;

        public static readonly VkIndirectCommandsLayout Null;

        public bool Equals(VkIndirectCommandsLayout other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is VkIndirectCommandsLayout && this == (VkIndirectCommandsLayout)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(VkIndirectCommandsLayout left, VkIndirectCommandsLayout right) => left.Equals(right);
        public static bool operator !=(VkIndirectCommandsLayout left, VkIndirectCommandsLayout right) => !left.Equals(right);

        public static implicit operator ulong(VkIndirectCommandsLayout indirectCommandsLayout) => indirectCommandsLayout.Handle;
        public static implicit operator VkIndirectCommandsLayout(ulong handle) => new VkIndirectCommandsLayout { Handle = handle };
    }
}