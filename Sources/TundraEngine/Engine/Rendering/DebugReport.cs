#if ENABLE_VALIDATION
using System;
using Vulkan;

namespace Engine.Rendering
{
    public class DebugReport : IDisposable
    {
        private Vk.DebugReportCallback _debugReportCallback;
        private Vk.Instance _instance;
        private bool _isDisposed = false;
        
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

        public DebugReport(Vk.Instance instance)
        {
            // Load functions
            CreateDebugReportCallback = Vk.LoadInstanceFunction<CreateDebugReportCallback>(instance);
            DestroyDebugReportCallback = Vk.LoadInstanceFunction<DestroyDebugReportCallback>(instance);

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

        private void Destroy()
        {
            if (_debugReportCallback.Handle != 0)
            {
                DestroyDebugReportCallback(_instance, _debugReportCallback, ref AllocationCallbacks.Null);
            }
        }
        
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                }

                if (_debugReportCallback.Handle != 0)
                {
                    DestroyDebugReportCallback(_instance, _debugReportCallback, ref AllocationCallbacks.Null);
                }
                Callback = null;
                CreateDebugReportCallback = null;
                DestroyDebugReportCallback = null;

                _isDisposed = true;
            }
        }
        
        ~DebugReport()
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
#endif