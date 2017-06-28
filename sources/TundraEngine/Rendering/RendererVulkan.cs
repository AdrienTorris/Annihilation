using System;
using System.Threading.Tasks;

using TundraEngine.Windowing;
using SharpVk;

using static TundraEngine.Rendering.Vulkan;

namespace TundraEngine.Rendering
{
    public class RendererVulkan : IRenderer
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

        public void Initialize(ref ApplicationInfo applicationInfo, ref WindowManagerInfo windowManagerInfo)
        {
            CreateInstance(applicationInfo.Name.ToString(), windowManagerInfo.Type, out _instance);
            CreateSurface(_instance, ref windowManagerInfo, out _surface);
            SelectPhysicalDevice(_instance, out _physicalDevice);
            CreateLogicalDevice(_physicalDevice, QueueTypes, new PhysicalDeviceFeatures { }, out _device, out _queueFamilyIndices);
            CreateCommandPool(_device, _queueFamilyIndices.Graphics, out _commandPool);
            CreateSwapchain(
                _device,
                (uint)(applicationInfo.WindowInfo.Width * applicationInfo.RendererInfo.RenderScale),
                (uint)(applicationInfo.WindowInfo.Height * applicationInfo.RendererInfo.RenderScale),
                _swapchain,
                out _swapchain);
            // TODO: Create command buffers
            // TODO: Setup depth stencil
            // TODO: Setup render pass
            // TODO: Create pipeline cache
            // TODO: Setup framebuffer

            _graphicsQueue = _device.GetQueue(_queueFamilyIndices.Graphics, 0);
        }
        
        public async Task RenderAsync()
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) { }

                _swapchain.Dispose();
                _surface.Dispose();
                _commandPool.Dispose();
                _device.Dispose();
                _instance.Dispose();

                disposedValue = true;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        
        ~RendererVulkan()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}