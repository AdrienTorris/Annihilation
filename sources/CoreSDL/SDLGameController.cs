using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        public enum SDL_GameControllerBindType
        {
            None = 0,
            Button,
            Axis,
            Hat
        }

        public enum SDL_GameControllerAxis
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

        public enum SDL_GameControllerButton
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

        [StructLayout (LayoutKind.Sequential)]
        public struct SDL_Hat
        {
            public readonly int Hat;
            public readonly int HatMask;
        }

        [StructLayout (LayoutKind.Explicit)]
        public struct SDL_GameControllerButtonBind
        {
            [FieldOffset (0)] public readonly SDL_GameControllerBindType BindType;
            [FieldOffset (4)] public readonly int Button;
            [FieldOffset (4)] public readonly int Axis;
            [FieldOffset (4)] public readonly SDL_Hat Hat;
        }
    }
}