using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        public delegate void LogOutputFunction (IntPtr userData, SDL_LogCategory category, SDL_LogPriority priority, IntPtr message);

        /// <summary>
        /// The predefined log categories
        /// <para /> By default the application category is enabled at the INFO level, the assert category is enabled at the WARN level, test is enabled at the VERBOSE level and all other categories are enabled at the CRITICAL level.
        /// </summary>
        public enum SDL_LogCategory
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
        public enum SDL_LogPriority
        {
            Verbose = 1,
            Debug,
            Info,
            Warn,
            Error,
            Critical,

            NumLogPriorities
        }
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogSetAllPriority (SDL_LogPriority priority);

        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogSetPriority (SDL_LogCategory category, SDL_LogPriority priority);

        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_LogPriority SDL_LogGetPriority (SDL_LogCategory category);

        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogResetPriorities ();

        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_Log (string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogVerbose (SDL_LogCategory category, string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogDebug (SDL_LogCategory category, string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogInfo (SDL_LogCategory category, string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogWarn (SDL_LogCategory category, string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogError (SDL_LogCategory category, string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogCritical (SDL_LogCategory category, string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogMessage (SDL_LogCategory category, SDL_LogPriority priority, string fmt, params object[] objects);
        
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogGetOutputFunction (LogOutputFunction callback, IntPtr userData);

        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_LogSetOutputFunction (LogOutputFunction callback, IntPtr userData);
    }
}