using System;
using CoreVulkan;

namespace Engine.Rendering
{
    public class DebugReport
    {
        private Vulkan.DebugReportCallback _debugReportCallback;
        private Vulkan.Instance _instance;
        
        public DebugReportFlags Flags { get; set; }
        public DebugReportCallback Callback { get; set; }

        // Vulkan functions
        private static CreateDebugReportCallback CreateDebugReportCallback;
        private static DestroyDebugReportCallback DestroyDebugReportCallback;

        private static Bool32 DebugCallback(DebugReportFlags flags, DebugReportObjectType objectType, ulong @object, Size location, int messageCode, Text layerPrefix, Text message, IntPtr userData)
        {
            if (objectType == DebugReportObjectType.DebugReportCallback && messageCode == 1)
            {
                return false;
            }

            string output = "[" + layerPrefix + "] Code " + messageCode + ": " + message;

            switch (flags)
            {
                case DebugReportFlags.Information: Log.Info(output); return false;
                case DebugReportFlags.Warning: Log.Warning(output); return false;
                case DebugReportFlags.PerformanceWarning: Log.Performance(output); return false;
                case DebugReportFlags.Error: Log.Error(output); return true;
                case DebugReportFlags.Debug: Log.Debug(output); return false;
                default: return false;
            }
        }

        public void Init(Vulkan.Instance instance)
        {
            // Load functions
            CreateDebugReportCallback = Vulkan.LoadInstanceFunction<CreateDebugReportCallback>(instance);
            DestroyDebugReportCallback = Vulkan.LoadInstanceFunction<DestroyDebugReportCallback>(instance);

            _instance = instance;
            Callback = DebugCallback;
            Flags = DebugReportFlags.Information |
                    DebugReportFlags.Warning |
                    DebugReportFlags.PerformanceWarning |
                    DebugReportFlags.Error |
                    DebugReportFlags.Debug |
                    0;

            Set(Flags, Callback);
        }

        public void Set(DebugReportFlags newFlags, DebugReportCallback newCallback)
        {
            if (_instance.Handle == IntPtr.Zero)
            {
                Log.Warning("Debug report was not initialized.");
                return;
            }
            if (newCallback == null)
            {
                newCallback = DebugCallback;
            }

            Callback = newCallback;
            Flags = newFlags;

            Destroy();
            DebugReportCallbackCreateInfo createInfo = new DebugReportCallbackCreateInfo(
                Flags,
                Callback
            );
            CreateDebugReportCallback(_instance, ref createInfo, ref AllocationCallbacks.Null, out _debugReportCallback).CheckError();
        }

        public void Destroy()
        {
            if (_debugReportCallback.Handle != 0)
            {
                DestroyDebugReportCallback(_instance, _debugReportCallback, ref AllocationCallbacks.Null);
            }
        }
    }
}