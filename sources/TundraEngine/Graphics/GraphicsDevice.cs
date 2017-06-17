using System;
using SharpVk;

namespace TundraEngine.Graphics
{
    public class GraphicsDevice : IDisposable
    {
        public GraphicsDevice (Instance instance)
        {
            // Physical devices
            PhysicalDevice[] physicalDevices = instance.EnumeratePhysicalDevices ();
            PhysicalDevice selectedPhysicalDevice = null;
            uint queueFamilyIndex = 0;

            foreach (var physicalDevice in physicalDevices)
            {
                if (CheckPhysicalDevice (physicalDevice, out queueFamilyIndex))
                {
                    selectedPhysicalDevice = physicalDevice;
                }
            }

            Device device = selectedPhysicalDevice.CreateDevice (new DeviceCreateInfo
            {
                QueueCreateInfos = new DeviceQueueCreateInfo[1]
                {
                    new DeviceQueueCreateInfo
                    {
                        QueueFamilyIndex = queueFamilyIndex,
                        QueuePriorities = new float[1] { 1f }
                    }
                }
            });

            Queue queue = device.GetQueue (queueFamilyIndex, 0);
        }

        private bool CheckPhysicalDevice (PhysicalDevice device, out uint queueFamilyIndex)
        {
            queueFamilyIndex = 0;
            PhysicalDeviceProperties physicalDeviceProperties = device.GetProperties ();
            PhysicalDeviceFeatures physicalDeviceFeatures = device.GetFeatures ();

            if (physicalDeviceProperties.ApiVersion.Major < 1 ||
                physicalDeviceProperties.Limits.MaxImageDimension2D < 4096)
            {
                return false;
            }

            QueueFamilyProperties[] queueFamilyProperties = device.GetQueueFamilyProperties ();
            if (queueFamilyProperties.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < queueFamilyProperties.Length; ++i)
            {
                if (queueFamilyProperties[i].QueueCount > 0 &&
                    (queueFamilyProperties[i].QueueFlags & QueueFlags.Graphics) != 0)
                {
                    queueFamilyIndex = (uint)i;
                    return true;
                }
            }
            return false;
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

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GraphicsDevice() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose ()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose (true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}