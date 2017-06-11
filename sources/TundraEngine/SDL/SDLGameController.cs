using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    unsafe public static partial class SDL
    {
        public enum GameControllerBindType
        {
            None = 0,
            Button,
            Axis,
            Hat
        }

        public enum GameControllerAxis
        {
            Invalid = -1,
            LeftX,
            LeftY,
            RightX,
            RightY,
            TriggertLeft,
            TriggerRight,
            Max
        }

        public enum GameControllerButton
        {
            Invalid = -1,
            A,
            B,
            X,
            Y,
            Back,
            Guide,
            Start,
            LeftStick,
            RightStick,
            LeftShoulder,
            RightShoulder,
            DPadUp,
            DPadDown,
            DPadLeft,
            DPadRight,
            Max
        }

        [StructLayout (LayoutKind.Explicit)]
        public struct GameControllerButtonBind
        {
            [StructLayout (LayoutKind.Sequential)]
            public struct HatStruct
            {
                public readonly int Hat;
                public readonly int HatMask;
            }

            [FieldOffset (0)] public readonly GameControllerBindType BindType;
            [FieldOffset (4)] public readonly int Button;
            [FieldOffset (4)] public readonly int Axis;
            [FieldOffset (4)] public readonly HatStruct Hat;
        }

        /// <summary>
        /// Load a set of mappings from a seekable SDL data stream (memory or file), filtered by the current SDL_GetPlatform().
        /// <para /> A community sourced database of controllers is available at https://raw.github.com/gabomdq/SDL_GameControllerDB/master/gamecontrollerdb.txt
        /// <para/> If <paramref name="freeRW"/> is non-zero, the stream will be closed after being read.
        /// </summary>
        /// <returns> Number of mappings added, -1 on error </returns>
        [DllImport (LibName, EntryPoint = "SDL_GameControllerAddMappingsFromRW", CallingConvention = CallingConvention.Cdecl)]
        extern private static int GameControllerAddMappingFromRW (IntPtr rw, int freeRW);

        /// <summary>
        /// Load a set of mappings from a file, filtered by the current SDL_GetPlatform()
        /// </summary>
        /*public static int GameControllerAddMappingsFromFile (string file)
        {
            return GameControllerAddMappingFromRW (RWFromFile (file, "rb"), 1);
        }*/

        /// <summary>
        /// Add or update an existing mapping configuration
        /// </summary>
        /// <returns> 1 if mapping is added, 0 if updated, -1 on error </returns>
        [DllImport (LibName, EntryPoint = "SDL_GameControllerAddMapping", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        extern private static int GameControllerAddMapping ([MarshalAs (UnmanagedType.LPTStr)] string mappingString);

        /// <summary>
        /// Get the number of mappings installed
        /// </summary>
        /// <returns> the number of mappings </returns>
        [DllImport (LibName, EntryPoint = "SDL_GameControllerNumMappings", CallingConvention = CallingConvention.Cdecl)]
        extern private static int GameControllerNumMappings ();

        /// <summary>
        /// Get the mapping at a particular index.
        /// </summary>
        /// <returns> the mapping string.  Must be freed with SDL_free().  Returns NULL if the index is out of range. </returns>
        [DllImport (LibName, EntryPoint = "SDL_GameControllerMappingForIndex", CallingConvention = CallingConvention.Cdecl)]
        [return : MarshalAs (UnmanagedType.LPTStr)]
        extern private static string GameControllerMappingForIndex (int mappingIndex);

        // TODO: Rest SDL_gamecontroller.h
    }
}