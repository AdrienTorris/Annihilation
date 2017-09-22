namespace Annihilation.Vulkan
{
    public unsafe class DebugReportCallback
    {
        private DebugReportCallbackHandle _handle;
        private Instance _instance;

        public DebugReportCallback(DebugReportCallbackHandle handle, Instance instance)
        {
            _handle = handle;
            _instance = instance;
        }

        public DebugReportCallback(Instance instance, ref DebugReportCallbackCreateInfo createInfo)
        {
            _instance = instance;
            instance.CreateDebugReportCallback(ref createInfo, out _handle);
        }

        public void Destroy()
        {
            _instance.DestroyDebugReportCallback(_handle);
        }
    }
}