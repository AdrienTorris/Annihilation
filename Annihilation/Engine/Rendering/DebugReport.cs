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
        
        public Vk.DebugReportFlags Flags { get; set; }
        public Vk.DebugReportCallbackDelegate Callback { get; set; }

        // Vulkan functions
        private static Vk.CreateDebugReportCallbackDelegate CreateDebugReportCallback;
        private static Vk.DestroyDebugReportCallbackDelegate DestroyDebugReportCallback;

        private static Vk.Bool32 DebugCallback(Vk.DebugReportFlags flags, Vk.DebugReportObjectType objectType, ulong @object, Size location, int messageCode, Text layerPrefix, Text message, IntPtr userData)
        {
            if (objectType == Vk.DebugReportObjectType.DebugReportCallback && messageCode == 1)
            {
                return false;
            }

            string output = "[" + layerPrefix + "] Code " + messageCode + ": " + message;

            switch (flags)
            {
                case Vk.DebugReportFlags.Information: Log.Info(output); return false;
                case Vk.DebugReportFlags.Warning: Log.Warning(output); return false;
                case Vk.DebugReportFlags.PerformanceWarning: Log.Performance(output); return false;
                case Vk.DebugReportFlags.Error: Log.Error(output); return true;
                case Vk.DebugReportFlags.Debug: Log.Info(output); return false;
                default: return false;
            }
        }

        public DebugReport(Vk.Instance instance)
        {
            // Load functions
            CreateDebugReportCallback = Vk.LoadInstanceFunction<Vk.CreateDebugReportCallbackDelegate>(instance);
            DestroyDebugReportCallback = Vk.LoadInstanceFunction<Vk.DestroyDebugReportCallbackDelegate>(instance);

            _instance = instance;
            Callback = DebugCallback;
            Flags = Vk.DebugReportFlags.Information |
                    Vk.DebugReportFlags.Warning |
                    Vk.DebugReportFlags.PerformanceWarning |
                    Vk.DebugReportFlags.Error |
                    Vk.DebugReportFlags.Debug |
                    0;

            Set(Flags, Callback);
        }

        public void Set(Vk.DebugReportFlags newFlags, Vk.DebugReportCallbackDelegate newCallback)
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
            Vk.DebugReportCallbackCreateInfo createInfo = new Vk.DebugReportCallbackCreateInfo(
                Flags,
                _debugReportCallback
            );
            CreateDebugReportCallback(_instance, ref createInfo, ref Vk.AllocationCallbacks.Null, out _debugReportCallback);
        }

        private void Destroy()
        {
            if (_debugReportCallback.Handle != 0)
            {
                DestroyDebugReportCallback(_instance, _debugReportCallback, ref Vk.AllocationCallbacks.Null);
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
                    DestroyDebugReportCallback(_instance, _debugReportCallback, ref Vk.AllocationCallbacks.Null);
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