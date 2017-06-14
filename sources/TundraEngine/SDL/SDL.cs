using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        public const string LibName = "SDL2.dll";
        
        /// <summary>
        /// These are the flags which may be passed to SDL_Init(). You should specify the subsystems which you will be using in your application.
        /// </summary>
        [Flags]
        public enum InitFlags : uint
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
        public extern static int Init (InitFlags flags);

        /// <summary>
        /// This function initializes specific SDL subsystems
        /// <para /> Subsystem initialization is ref-counted, you must call SDL_QuitSubSystem () for each SDL_InitSubSystem () to correctly shutdown a subsystem manually (or call SDL_Quit() to force shutdown).
        /// <para /> If a subsystem is already loaded then this call will increase the ref-count and return.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_InitSubSystem", CallingConvention = CallingConvention.Cdecl)]
        public extern static int InitSubSystem (InitFlags flags);

        /// <summary>
        /// This function cleans up specific SDL subsystems
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_QuitSubSystem", CallingConvention = CallingConvention.Cdecl)]
        public extern static void QuitSubSystem (InitFlags flags);

        /// <summary>
        /// This function returns a mask of the specified subsystems which have previously been initialized.
        /// <para /> If <paramref name="flags"/> is 0, it returns a mask of all initialized subsystems.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_WasInit", CallingConvention = CallingConvention.Cdecl)]
        public extern static InitFlags WasInit (InitFlags flags);

        /// <summary>
        /// This function cleans up all initialized subsystems. You should call it upon all exit conditions.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_Quit", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Quit ();
    }
}
