using System;
using System.Text;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    public static partial class SDL
    {
        public const string LibName = "SDL2.dll";
        
        /// <summary>
        /// These are the flags which may be passed to SDL_Init(). You should specify the subsystems which you will be using in your application.
        /// </summary>
        [Flags]
        public enum SDL_InitFlags : uint
        {
            Timer = 0x00000001u,
            Audio = 0x00000010u,
            Video = 0x00000020u,
            Joystick = 0x00000200u,
            Haptic = 0x00001000u,
            GameController = 0x00002000u,
            Events = 0x00004000u,
            NoParachute = 0x00100000u,
            Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events
        }

        unsafe private static string GetString (IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                return string.Empty;

            var ptr = (byte*)handle;
            while (*ptr != 0)
                ptr++;

            var bytes = new byte[ptr - (byte*)handle];
            Marshal.Copy (handle, bytes, 0, bytes.Length);

            return Encoding.UTF8.GetString (bytes);
        }

        /// <summary>
        /// This function initializes  the subsystems specified by <paramref name="flags"/>
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_Init (SDL_InitFlags flags);
        
        /// <summary>
        /// This function initializes specific SDL subsystems
        /// <para /> Subsystem initialization is ref-counted, you must call SDL_QuitSubSystem () for each SDL_InitSubSystem () to correctly shutdown a subsystem manually (or call SDL_Quit() to force shutdown).
        /// <para /> If a subsystem is already loaded then this call will increase the ref-count and return.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_InitSubSystem (SDL_InitFlags flags);

        /// <summary>
        /// This function cleans up specific SDL subsystems
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_QuitSubSystem (SDL_InitFlags flags);

        /// <summary>
        /// This function returns a mask of the specified subsystems which have previously been initialized.
        /// <para /> If <paramref name="flags"/> is 0, it returns a mask of all initialized subsystems.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_InitFlags SDL_WasInit (SDL_InitFlags flags);

        /// <summary>
        /// This function cleans up all initialized subsystems. You should call it upon all exit conditions.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_Quit ();
    }
}
