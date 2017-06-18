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

        internal GraphicsDevice (string applicationName, SysWMInfo windowManagerInfo)
        {
            _instance = Vulkan.CreateInstance (applicationName, windowManagerInfo.SubSystem);
            _physicalDevice = Vulkan.GetPhysicalDevice (_instance);
            (_device, _commandPool) = Vulkan.CreateDeviceAndCommandPool (_physicalDevice, QueueTypes, out _queueFamilyIndices, null);
            _graphicsQueue = _device.GetQueue (_queueFamilyIndices.Graphics, 0);
            _surface = Vulkan.CreateSurface (_instance, windowManagerInfo);
            // TODO: Create command pool
            // TODO: Setup swap chain
            // TODO: Create command buffers
            // TODO: Setup depth stencil
            // TODO: Setup render pass
            // TODO: Create pipeline cache
            // TODO: Setup framebuffer
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