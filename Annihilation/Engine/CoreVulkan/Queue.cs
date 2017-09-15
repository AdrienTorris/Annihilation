using System;
using Engine.CoreVulkan;

namespace Engine.CoreVulkan
{
    public unsafe struct Queue
    {
        public QueueHandle Handle { get; private set; }
        public Device Device { get; private set; }

        public Queue(QueueHandle handle, Device device)
        {
            Handle = handle;
            Device = device;
        }
    }
}