using System;

using TundraEngine.Windowing;

using SharpVk;

using static TundraEngine.Graphics.Vulkan;

namespace TundraEngine.Graphics
{
    public class GraphicsProviderVulkan : GraphicsProvider
    {
        private Instance _instance;
        private PhysicalDevice _physicalDevice;
        private Device _device;
        private CommandPool _commandPool;
        private QueueFamilyIndices _queueFamilyIndices;
        private Queue _graphicsQueue;
        private Queue _presentQueue;
        private Surface _surface;
        private Swapchain _swapchain;

        private const QueueFlags QueueTypes = QueueFlags.Graphics | QueueFlags.Compute;
        private const Format DepthFormat = Format.D32SFloatS8UInt;

        public GraphicsProviderVulkan(ref ApplicationInfo applicationInfo, ref WindowManagerInfo windowManagerInfo)
            : base(ref applicationInfo, ref windowManagerInfo)
        {
            CreateInstance(applicationInfo.Name.ToString(), windowManagerInfo.Type, out _instance);
            CreateSurface(_instance, ref windowManagerInfo, out _surface);
            SelectPhysicalDevice(_instance, out _physicalDevice);
            CreateLogicalDevice(_physicalDevice, QueueTypes, new PhysicalDeviceFeatures { }, out _device, out _queueFamilyIndices);
            CreateCommandPool(_device, _queueFamilyIndices.Graphics, out _commandPool);
            CreateSwapchain(
                _device,
                (uint)(applicationInfo.WindowInfo.Width * applicationInfo.GraphicsInfo.RenderScale),
                (uint)(applicationInfo.WindowInfo.Height * applicationInfo.GraphicsInfo.RenderScale),
                _swapchain,
                out _swapchain);
            // TODO: Create command buffers
            // TODO: Setup depth stencil
            // TODO: Setup render pass
            // TODO: Create pipeline cache
            // TODO: Setup framebuffer

            _graphicsQueue = _device.GetQueue(_queueFamilyIndices.Graphics, 0);
        }
        
        public override void Render(int width, int height)
        {
            throw new NotImplementedException();
        }

        protected override void DisposeUnmanaged()
        {
            _swapchain.Dispose();
            _surface.Dispose();
            _commandPool.Dispose();
            _device.Dispose();
            _instance.Dispose();
        }
    }
}