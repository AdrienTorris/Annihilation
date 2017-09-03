using System;
using System.Collections.Generic;
using Vulkan;

namespace Engine.Rendering
{
    public class PhysicalDevice : IDisposable
    {
        public Vk.PhysicalDevice Handle { get; private set; }
        public Vk.PhysicalDeviceProperties Properties { get; private set; }
        public Vk.PhysicalDeviceFeatures Features { get; private set; }
        public Vk.QueueFamilyProperties[] QueueFamilies { get; private set; }

        private bool _isDisposed = false;

        // Vulkan instance functions
        private static Vk.GetPhysicalDeviceSurfaceSupportKHRDelegate GetPhysicalDeviceSurfaceSupport;

        private static readonly Dictionary<uint, string> _vendorMap = new Dictionary<uint, string>
        {
            { 0x1002, "AMD" },
            { 0x10DE, "NVIDIA" },
            { 0x8086, "INTEL" },
            { 0x13B5, "ARM" },
            { 0x5143, "Qualcomm" },
            { 0x1010, "ImgTec" }
        };

        public string GetVendorName() => _vendorMap[Properties.VendorId];

        public int FindQueueFamily(Vk.QueueFlags flags, Vk.Surface surface)
        {
            for (int i = 0; i < QueueFamilies.Length; ++i)
            {
                Vk.Bool32 canPresent = false;
                if ((QueueFamilies[i].QueueFlags & flags) != flags) continue;
                if (surface.Handle != 0) GetPhysicalDeviceSurfaceSupport(Handle, (uint)i, surface, out canPresent);
                if ((surface.Handle != 0) == canPresent) return i;
            }
            return -1;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }
                
                _isDisposed = true;
            }
        }
        
        ~PhysicalDevice()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}