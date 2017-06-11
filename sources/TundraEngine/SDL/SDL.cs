using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    unsafe public static partial class SDL
    {
        private const string LibName = "SDL2.dll";

        public enum SDLBool
        {
            False = 0,
            True = 1,
        }

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

        /// <summary>
        /// This function initializes  the subsystems specified by <paramref name="flags"/>
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_Init", CallingConvention = CallingConvention.Cdecl)]
        extern public static int Init (SDL_InitFlags flags);

        /// <summary>
        /// This function initializes specific SDL subsystems
        /// <para /> Subsystem initialization is ref-counted, you must call SDL_QuitSubSystem () for each SDL_InitSubSystem () to correctly shutdown a subsystem manually (or call SDL_Quit() to force shutdown).
        /// <para /> If a subsystem is already loaded then this call will increase the ref-count and return.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_InitSubSystem", CallingConvention = CallingConvention.Cdecl)]
        extern public static int InitSubSystem (SDL_InitFlags flags);

        /// <summary>
        /// This function cleans up specific SDL subsystems
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_QuitSubSystem", CallingConvention = CallingConvention.Cdecl)]
        extern public static void QuitSubSystem (SDL_InitFlags flags);

        /// <summary>
        /// This function returns a mask of the specified subsystems which have previously been initialized.
        /// <para /> If <paramref name="flags"/> is 0, it returns a mask of all initialized subsystems.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_WasInit", CallingConvention = CallingConvention.Cdecl)]
        extern public static uint WasInit (SDL_InitFlags flags);

        /// <summary>
        /// This function cleans up all initialized subsystems. You should call it upon all exit conditions.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_Quit", CallingConvention = CallingConvention.Cdecl)]
        extern public static void Quit ();
    }
}
