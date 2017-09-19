using System;

namespace Annihilation.Vulkan
{
    public unsafe struct DebugReportCallback
    {
        public DebugReportCallbackHandle Handle { get; private set; }
        public Instance Instance { get; private set; }

        public DebugReportCallback(DebugReportCallbackHandle handle, Instance instance)
        {
            Handle = handle;
            Instance = instance;
        }
    }
}
