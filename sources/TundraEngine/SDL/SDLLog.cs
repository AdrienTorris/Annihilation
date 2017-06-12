using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        public delegate void LogOutputFunction (IntPtr userData, LogCategory category, LogPriority priority, IntPtr message);

        /// <summary>
        /// The predefined log categories
        /// <para /> By default the application category is enabled at the INFO level, the assert category is enabled at the WARN level, test is enabled at the VERBOSE level and all other categories are enabled at the CRITICAL level.
        /// </summary>
        public enum LogCategory
        {
            Application,
            Error,
            Assert,
            System,
            Audio,
            Video,
            Render,
            Input,
            Test,

            /* Reserved for future SDL library use */
            Reserved1,
            Reserved2,
            Reserved3,
            Reserved4,
            Reserved5,
            Reserved6,
            Reserved7,
            Reserved8,
            Reserved9,
            Reserved10,

            /* Beyond this point is reserved for application use, e.g.
                enum {
                    MYAPP_CATEGORY_AWESOME1 = SDL_LOG_CATEGORY_CUSTOM,
                    MYAPP_CATEGORY_AWESOME2,
                    MYAPP_CATEGORY_AWESOME3,
                    ...
                };
            */
            Custom,
        }

        /// <summary>
        /// The predefined log priorities
        /// </summary>
        public enum LogPriority
        {
            Verbose = 1,
            Debug,
            Info,
            Warn,
            Error,
            Critical,

            NumLogPriorities
        }
        
        [DllImport (LibName, EntryPoint = "SDL_LogSetAllPriority", CallingConvention = CallingConvention.Cdecl)]
        public extern static void LogSetAllPriority (LogPriority priority);

        [DllImport (LibName, EntryPoint = "SDL_LogSetPriority", CallingConvention = CallingConvention.Cdecl)]
        public extern static void LogSetPriority (LogCategory category, LogPriority priority);

        [DllImport (LibName, EntryPoint = "SDL_LogGetPriority", CallingConvention = CallingConvention.Cdecl)]
        public extern static LogPriority LogGetPriority (LogCategory category);

        [DllImport (LibName, EntryPoint = "SDL_LogResetPriorities", CallingConvention = CallingConvention.Cdecl)]
        public extern static void LogResetPriorities ();

        [DllImport (LibName, EntryPoint = "SDL_Log", CallingConvention = CallingConvention.Cdecl)]
        private extern static void Log (IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogVerbose", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogVerbose (LogCategory category, IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogDebug", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogDebug (LogCategory category, IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogInfo", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogInfo (LogCategory category, IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogWarn", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogWarn (LogCategory category, IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogError", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogError (LogCategory category, IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogCritical", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogCritical (LogCategory category, IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogMessage", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogMessage (LogCategory category, LogPriority priority, IntPtr fmt, params object[] objects);

        [DllImport (LibName, EntryPoint = "SDL_LogGetOutputFunction", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogGetOutputFunction (LogOutputFunction callback, IntPtr userData);

        [DllImport (LibName, EntryPoint = "SDL_LogSetOutputFunction", CallingConvention = CallingConvention.Cdecl)]
        private extern static void LogSetOutputFunction (LogOutputFunction callback, IntPtr userData);
    }
}