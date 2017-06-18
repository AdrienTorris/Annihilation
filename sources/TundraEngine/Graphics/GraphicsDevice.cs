using System;
using SharpVk;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Graphics
{
    internal struct QueueFamilyIndices
    {
        public uint Graphics;
        public uint Compute;
        public uint Transfer;
    }

    public class GraphicsDevice : IDisposable
    {
        private Instance _instance;
        private PhysicalDevice _physicalDevice;
        private Device _device;
        private CommandPool _commandPool;
        private QueueFamilyIndices _queueFamilyIndices;
        private Queue _graphicsQueue;
        private Surface _surface;
        private Swapchain _swapchain;

        private const QueueFlags QueueTypes = QueueFlags.Graphics | QueueFlags.Compute;
        private const Format DepthFormat = Format.D32SFloatS8UInt;

        internal GraphicsDevice (string applicationName, GraphicsInfo graphicsInfo, SysWMInfo windowManagerInfo)
        {
            Vulkan.CreateInstance (applicationName, windowManagerInfo.SubSystem, out _instance);
            Vulkan.CreateSurface (_instance, windowManagerInfo, out _surface);
            Vulkan.SelectPhysicalDevice (_instance, out _physicalDevice);
            Vulkan.CreateLogicalDevice (_physicalDevice, QueueTypes, new PhysicalDeviceFeatures { }, out _device, out _queueFamilyIndices);
            Vulkan.CreateCommandPool (_device, _queueFamilyIndices.Graphics, out _commandPool);
            Vulkan.CreateSwapchain (_device, graphicsInfo.ResolutionX, graphicsInfo.ResolutionY, _swapchain, out _swapchain);
            // TODO: Create command buffers
            // TODO: Setup depth stencil
            // TODO: Setup render pass
            // TODO: Create pipeline cache
            // TODO: Setup framebuffer
            
            _graphicsQueue = _device.GetQueue (_queueFamilyIndices.Graphics, 0);
        }
        
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose (bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _swapchain.Dispose ();
                _surface.Dispose ();
                _commandPool.Dispose ();
                _device.Dispose ();
                _instance.Dispose ();

                disposedValue = true;
            }
        }
        
        ~GraphicsDevice ()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose (false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose ()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose (true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}