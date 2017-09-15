using System;

namespace Engine.CoreVulkan
{
    public unsafe struct Display
    {
        public DisplayHandle Handle { get; private set; }
        public PhysicalDevice PhysicalDevice { get; private set; }

        public Display(DisplayHandle handle, PhysicalDevice physicalDevice)
        {
            Handle = handle;
            PhysicalDevice = physicalDevice;
        }
    }
}