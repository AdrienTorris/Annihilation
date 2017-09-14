using System;
using System.Runtime.InteropServices;
using SDL2;

namespace Vulkan
{
    public partial struct Instance : IEquatable<Instance>
    {
        public IntPtr Handle;

        public static readonly Instance Null;

        public bool Equals(Instance other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Instance && this == (Instance)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Instance left, Instance right) => left.Equals(right);
        public static bool operator !=(Instance left, Instance right) => !left.Equals(right);

        public static implicit operator IntPtr(Instance instance) => instance.Handle;
        public static implicit operator Instance(IntPtr handle) => new Instance { Handle = handle };
        public static implicit operator SDL.VkInstance(Instance instance) => new SDL.VkInstance { Handle = instance.Handle };
        public static implicit operator Instance(SDL.VkInstance instance) => new Instance { Handle = instance.Handle };
    }

    [StructLayout(LayoutKind.Sequential)]
    public partial struct PhysicalDevice : IEquatable<PhysicalDevice>
    {
        public IntPtr Handle;

        public readonly static PhysicalDevice Null;

        public bool Equals(PhysicalDevice other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is PhysicalDevice && this == (PhysicalDevice)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(PhysicalDevice left, PhysicalDevice right) => left.Equals(right);
        public static bool operator !=(PhysicalDevice left, PhysicalDevice right) => !left.Equals(right);

        public static implicit operator IntPtr(PhysicalDevice physicalDevice) => physicalDevice.Handle;
        public static implicit operator PhysicalDevice(IntPtr handle) => new PhysicalDevice { Handle = handle };
    }

    public partial struct Device : IEquatable<Device>
    {
        public IntPtr Handle;

        public static readonly Device Null;

        public bool Equals(Device other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Device && this == (Device)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Device left, Device right) => left.Equals(right);
        public static bool operator !=(Device left, Device right) => !left.Equals(right);

        public static implicit operator IntPtr(Device device) => device.Handle;
        public static implicit operator Device(IntPtr handle) => new Device { Handle = handle };
    }

    public partial struct Queue : IEquatable<Queue>
    {
        public IntPtr Handle;

        public static readonly Queue Null;

        public bool Equals(Queue other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Queue && this == (Queue)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Queue left, Queue right) => left.Equals(right);
        public static bool operator !=(Queue left, Queue right) => !left.Equals(right);

        public static implicit operator IntPtr(Queue queue) => queue.Handle;
        public static implicit operator Queue(IntPtr handle) => new Queue { Handle = handle };
    }

    public partial struct CommandBuffer : IEquatable<CommandBuffer>
    {
        public IntPtr Handle;

        public static readonly CommandBuffer Null;

        public bool Equals(CommandBuffer other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is CommandBuffer && this == (CommandBuffer)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(CommandBuffer left, CommandBuffer right) => left.Equals(right);
        public static bool operator !=(CommandBuffer left, CommandBuffer right) => !left.Equals(right);

        public static implicit operator IntPtr(CommandBuffer commandBuffer) => commandBuffer.Handle;
        public static implicit operator CommandBuffer(IntPtr handle) => new CommandBuffer { Handle = handle };
    }

    //
    // Non dispatchable handles
    //
    public partial struct Semaphore : IEquatable<Semaphore>
    {
        public ulong Handle;

        public static readonly Semaphore Null;

        public bool Equals(Semaphore other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Semaphore && this == (Semaphore)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Semaphore left, Semaphore right) => left.Equals(right);
        public static bool operator !=(Semaphore left, Semaphore right) => !left.Equals(right);

        public static implicit operator ulong(Semaphore semaphore) => semaphore.Handle;
        public static implicit operator Semaphore(ulong handle) => new Semaphore { Handle = handle };
    }

    public partial struct Fence : IEquatable<Fence>
    {
        public ulong Handle;

        public static readonly Fence Null;

        public bool Equals(Fence other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Fence && this == (Fence)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Fence left, Fence right) => left.Equals(right);
        public static bool operator !=(Fence left, Fence right) => !left.Equals(right);

        public static implicit operator ulong(Fence fence) => fence.Handle;
        public static implicit operator Fence(ulong handle) => new Fence { Handle = handle };
    }

    public partial struct DeviceMemory : IEquatable<DeviceMemory>
    {
        public ulong Handle;

        public static readonly DeviceMemory Null;

        public bool Equals(DeviceMemory other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DeviceMemory && this == (DeviceMemory)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DeviceMemory left, DeviceMemory right) => left.Equals(right);
        public static bool operator !=(DeviceMemory left, DeviceMemory right) => !left.Equals(right);

        public static implicit operator ulong(DeviceMemory deviceMemory) => deviceMemory.Handle;
        public static implicit operator DeviceMemory(ulong handle) => new DeviceMemory { Handle = handle };
    }

    public partial struct Buffer : IEquatable<Buffer>
    {
        public ulong Handle;

        public static readonly Buffer Null;

        public bool Equals(Buffer other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Buffer && this == (Buffer)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Buffer left, Buffer right) => left.Equals(right);
        public static bool operator !=(Buffer left, Buffer right) => !left.Equals(right);

        public static implicit operator ulong(Buffer buffer) => buffer.Handle;
        public static implicit operator Buffer(ulong handle) => new Buffer { Handle = handle };
    }

    public partial struct Image : IEquatable<Image>
    {
        public ulong Handle;

        public static readonly Image Null;

        public bool Equals(Image other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Image && this == (Image)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Image left, Image right) => left.Equals(right);
        public static bool operator !=(Image left, Image right) => !left.Equals(right);

        public static implicit operator ulong(Image image) => image.Handle;
        public static implicit operator Image(ulong handle) => new Image { Handle = handle };
    }

    public partial struct Event : IEquatable<Event>
    {
        public ulong Handle;

        public static readonly Event Null;

        public bool Equals(Event other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Event && this == (Event)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Event left, Event right) => left.Equals(right);
        public static bool operator !=(Event left, Event right) => !left.Equals(right);

        public static implicit operator ulong(Event @event) => @event.Handle;
        public static implicit operator Event(ulong handle) => new Event { Handle = handle };
    }

    public partial struct QueryPool : IEquatable<QueryPool>
    {
        public ulong Handle;

        public static readonly QueryPool Null;

        public bool Equals(QueryPool other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is QueryPool && this == (QueryPool)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(QueryPool left, QueryPool right) => left.Equals(right);
        public static bool operator !=(QueryPool left, QueryPool right) => !left.Equals(right);

        public static implicit operator ulong(QueryPool queryPool) => queryPool.Handle;
        public static implicit operator QueryPool(ulong handle) => new QueryPool { Handle = handle };
    }

    public partial struct BufferView : IEquatable<BufferView>
    {
        public ulong Handle;

        public static readonly BufferView Null;

        public bool Equals(BufferView other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is BufferView && this == (BufferView)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(BufferView left, BufferView right) => left.Equals(right);
        public static bool operator !=(BufferView left, BufferView right) => !left.Equals(right);

        public static implicit operator ulong(BufferView bufferView) => bufferView.Handle;
        public static implicit operator BufferView(ulong handle) => new BufferView { Handle = handle };
    }

    public partial struct ImageView : IEquatable<ImageView>
    {
        public ulong Handle;

        public static readonly ImageView Null;

        public bool Equals(ImageView other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is ImageView && this == (ImageView)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(ImageView left, ImageView right) => left.Equals(right);
        public static bool operator !=(ImageView left, ImageView right) => !left.Equals(right);

        public static implicit operator ulong(ImageView imageView) => imageView.Handle;
        public static implicit operator ImageView(ulong handle) => new ImageView { Handle = handle };
    }

    public partial struct ShaderModule : IEquatable<ShaderModule>
    {
        public ulong Handle;

        public static readonly ShaderModule Null;

        public bool Equals(ShaderModule other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is ShaderModule && this == (ShaderModule)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(ShaderModule left, ShaderModule right) => left.Equals(right);
        public static bool operator !=(ShaderModule left, ShaderModule right) => !left.Equals(right);

        public static implicit operator ulong(ShaderModule shaderModule) => shaderModule.Handle;
        public static implicit operator ShaderModule(ulong handle) => new ShaderModule { Handle = handle };
    }

    public partial struct PipelineCache : IEquatable<PipelineCache>
    {
        public ulong Handle;

        public static readonly PipelineCache Null;

        public bool Equals(PipelineCache other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is PipelineCache && this == (PipelineCache)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(PipelineCache left, PipelineCache right) => left.Equals(right);
        public static bool operator !=(PipelineCache left, PipelineCache right) => !left.Equals(right);

        public static implicit operator ulong(PipelineCache pipelineCache) => pipelineCache.Handle;
        public static implicit operator PipelineCache(ulong handle) => new PipelineCache { Handle = handle };
    }

    public partial struct PipelineLayout : IEquatable<PipelineLayout>
    {
        public ulong Handle;

        public static readonly PipelineLayout Null;

        public bool Equals(PipelineLayout other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is PipelineLayout && this == (PipelineLayout)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(PipelineLayout left, PipelineLayout right) => left.Equals(right);
        public static bool operator !=(PipelineLayout left, PipelineLayout right) => !left.Equals(right);

        public static implicit operator ulong(PipelineLayout pipelineLayout) => pipelineLayout.Handle;
        public static implicit operator PipelineLayout(ulong handle) => new PipelineLayout { Handle = handle };
    }

    public partial struct RenderPass : IEquatable<RenderPass>
    {
        public ulong Handle;

        public static readonly RenderPass Null;

        public bool Equals(RenderPass other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is RenderPass && this == (RenderPass)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(RenderPass left, RenderPass right) => left.Equals(right);
        public static bool operator !=(RenderPass left, RenderPass right) => !left.Equals(right);

        public static implicit operator ulong(RenderPass renderPass) => renderPass.Handle;
        public static implicit operator RenderPass(ulong handle) => new RenderPass { Handle = handle };
    }

    public partial struct Pipeline : IEquatable<Pipeline>
    {
        public ulong Handle;

        public static readonly Pipeline Null;

        public bool Equals(Pipeline other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Pipeline && this == (Pipeline)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Pipeline left, Pipeline right) => left.Equals(right);
        public static bool operator !=(Pipeline left, Pipeline right) => !left.Equals(right);

        public static implicit operator ulong(Pipeline pipeline) => pipeline.Handle;
        public static implicit operator Pipeline(ulong handle) => new Pipeline { Handle = handle };
    }

    public partial struct DescriptorSetLayout : IEquatable<DescriptorSetLayout>
    {
        public ulong Handle;

        public static readonly DescriptorSetLayout Null;

        public bool Equals(DescriptorSetLayout other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorSetLayout && this == (DescriptorSetLayout)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorSetLayout left, DescriptorSetLayout right) => left.Equals(right);
        public static bool operator !=(DescriptorSetLayout left, DescriptorSetLayout right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorSetLayout descriptorSetLayout) => descriptorSetLayout.Handle;
        public static implicit operator DescriptorSetLayout(ulong handle) => new DescriptorSetLayout { Handle = handle };
    }

    public partial struct Sampler : IEquatable<Sampler>
    {
        public ulong Handle;

        public static readonly Sampler Null;

        public bool Equals(Sampler other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Sampler && this == (Sampler)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Sampler left, Sampler right) => left.Equals(right);
        public static bool operator !=(Sampler left, Sampler right) => !left.Equals(right);

        public static implicit operator ulong(Sampler sampler) => sampler.Handle;
        public static implicit operator Sampler(ulong handle) => new Sampler { Handle = handle };
    }

    public partial struct DescriptorPool : IEquatable<DescriptorPool>
    {
        public ulong Handle;

        public static readonly DescriptorPool Null;

        public bool Equals(DescriptorPool other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorPool && this == (DescriptorPool)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorPool left, DescriptorPool right) => left.Equals(right);
        public static bool operator !=(DescriptorPool left, DescriptorPool right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorPool descriptorPool) => descriptorPool.Handle;
        public static implicit operator DescriptorPool(ulong handle) => new DescriptorPool { Handle = handle };
    }

    public partial struct DescriptorSet : IEquatable<DescriptorSet>
    {
        public ulong Handle;

        public static readonly DescriptorSet Null;

        public bool Equals(DescriptorSet other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorSet && this == (DescriptorSet)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorSet left, DescriptorSet right) => left.Equals(right);
        public static bool operator !=(DescriptorSet left, DescriptorSet right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorSet descriptorSet) => descriptorSet.Handle;
        public static implicit operator DescriptorSet(ulong handle) => new DescriptorSet { Handle = handle };
    }

    public partial struct Framebuffer : IEquatable<Framebuffer>
    {
        public ulong Handle;

        public static readonly Framebuffer Null;

        public bool Equals(Framebuffer other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Framebuffer && this == (Framebuffer)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Framebuffer left, Framebuffer right) => left.Equals(right);
        public static bool operator !=(Framebuffer left, Framebuffer right) => !left.Equals(right);

        public static implicit operator ulong(Framebuffer framebuffer) => framebuffer.Handle;
        public static implicit operator Framebuffer(ulong handle) => new Framebuffer { Handle = handle };
    }

    public partial struct CommandPool : IEquatable<CommandPool>
    {
        public ulong Handle;

        public static readonly CommandPool Null;

        public bool Equals(CommandPool other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is CommandPool && this == (CommandPool)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(CommandPool left, CommandPool right) => left.Equals(right);
        public static bool operator !=(CommandPool left, CommandPool right) => !left.Equals(right);

        public static implicit operator ulong(CommandPool commandPool) => commandPool.Handle;
        public static implicit operator CommandPool(ulong handle) => new CommandPool { Handle = handle };
    }

    // Khronos
    public partial struct Surface : IEquatable<Surface>
    {
        public ulong Handle;

        public static readonly Surface Null;

        public bool Equals(Surface other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Surface && this == (Surface)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Surface left, Surface right) => left.Equals(right);
        public static bool operator !=(Surface left, Surface right) => !left.Equals(right);

        public static implicit operator ulong(Surface surface) => surface.Handle;
        public static implicit operator Surface(ulong handle) => new Surface { Handle = handle };
        public static implicit operator SDL.VkSurfaceKHR(Surface surface) => new SDL.VkSurfaceKHR { Handle = surface.Handle };
        public static implicit operator Surface(SDL.VkSurfaceKHR surface) => new Surface { Handle = surface.Handle };
    }

    public partial struct Swapchain : IEquatable<Swapchain>
    {
        public ulong Handle;

        public static readonly Swapchain Null;

        public bool Equals(Swapchain other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Swapchain && this == (Swapchain)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Swapchain left, Swapchain right) => left.Equals(right);
        public static bool operator !=(Swapchain left, Swapchain right) => !left.Equals(right);

        public static implicit operator ulong(Swapchain swapchain) => swapchain.Handle;
        public static implicit operator Swapchain(ulong handle) => new Swapchain { Handle = handle };
    }

    public partial struct Display : IEquatable<Display>
    {
        public ulong Handle;

        public static readonly Display Null;

        public bool Equals(Display other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is Display && this == (Display)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(Display left, Display right) => left.Equals(right);
        public static bool operator !=(Display left, Display right) => !left.Equals(right);

        public static implicit operator ulong(Display display) => display.Handle;
        public static implicit operator Display(ulong handle) => new Display { Handle = handle };
    }

    public partial struct DisplayMode : IEquatable<DisplayMode>
    {
        public ulong Handle;

        public static readonly DisplayMode Null;

        public bool Equals(DisplayMode other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DisplayMode && this == (DisplayMode)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DisplayMode left, DisplayMode right) => left.Equals(right);
        public static bool operator !=(DisplayMode left, DisplayMode right) => !left.Equals(right);

        public static implicit operator ulong(DisplayMode displayMode) => displayMode.Handle;
        public static implicit operator DisplayMode(ulong handle) => new DisplayMode { Handle = handle };
    }

    public partial struct DescriptorUpdateTemplate : IEquatable<DescriptorUpdateTemplate>
    {
        public ulong Handle;

        public static readonly DescriptorUpdateTemplate Null;

        public bool Equals(DescriptorUpdateTemplate other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DescriptorUpdateTemplate && this == (DescriptorUpdateTemplate)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DescriptorUpdateTemplate left, DescriptorUpdateTemplate right) => left.Equals(right);
        public static bool operator !=(DescriptorUpdateTemplate left, DescriptorUpdateTemplate right) => !left.Equals(right);

        public static implicit operator ulong(DescriptorUpdateTemplate descriptorUpdateTemplate) => descriptorUpdateTemplate.Handle;
        public static implicit operator DescriptorUpdateTemplate(ulong handle) => new DescriptorUpdateTemplate { Handle = handle };
    }

    // Multi-vendor
    public partial struct DebugReportCallback : IEquatable<DebugReportCallback>
    {
        public ulong Handle;

        public static readonly DebugReportCallback Null;

        public bool Equals(DebugReportCallback other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is DebugReportCallback && this == (DebugReportCallback)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(DebugReportCallback left, DebugReportCallback right) => left.Equals(right);
        public static bool operator !=(DebugReportCallback left, DebugReportCallback right) => !left.Equals(right);

        public static implicit operator ulong(DebugReportCallback debugReportCallback) => debugReportCallback.Handle;
        public static implicit operator DebugReportCallback(ulong handle) => new DebugReportCallback { Handle = handle };
    }

    // Nvidia
    public partial struct ObjectTable : IEquatable<ObjectTable>
    {
        public ulong Handle;

        public static readonly ObjectTable Null;

        public bool Equals(ObjectTable other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is ObjectTable && this == (ObjectTable)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(ObjectTable left, ObjectTable right) => left.Equals(right);
        public static bool operator !=(ObjectTable left, ObjectTable right) => !left.Equals(right);

        public static implicit operator ulong(ObjectTable objectTable) => objectTable.Handle;
        public static implicit operator ObjectTable(ulong handle) => new ObjectTable { Handle = handle };
    }

    public partial struct IndirectCommandsLayout : IEquatable<IndirectCommandsLayout>
    {
        public ulong Handle;

        public static readonly IndirectCommandsLayout Null;

        public bool Equals(IndirectCommandsLayout other) => Handle == other.Handle;
        public override bool Equals(object obj) => obj is IndirectCommandsLayout && this == (IndirectCommandsLayout)obj;
        public override int GetHashCode() => Handle.GetHashCode();
        public override string ToString() => Handle.ToString();

        public static bool operator ==(IndirectCommandsLayout left, IndirectCommandsLayout right) => left.Equals(right);
        public static bool operator !=(IndirectCommandsLayout left, IndirectCommandsLayout right) => !left.Equals(right);

        public static implicit operator ulong(IndirectCommandsLayout indirectCommandsLayout) => indirectCommandsLayout.Handle;
        public static implicit operator IndirectCommandsLayout(ulong handle) => new IndirectCommandsLayout { Handle = handle };
    }
}