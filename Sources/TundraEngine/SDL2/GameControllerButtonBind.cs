using System.Runtime.InteropServices;

namespace SDL2
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

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Hat
    {
        public readonly int HatIndex;
        public readonly int HatMask;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GameControllerButtonBind
    {
        [FieldOffset(0)] public readonly GameControllerBindType BindType;
        [FieldOffset(4)] public readonly int Button;
        [FieldOffset(4)] public readonly int Axis;
        [FieldOffset(4)] public readonly Hat Hat;
    }
}