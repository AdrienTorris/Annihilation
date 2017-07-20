using System;

namespace TundraEngine.Input
{
    [Flags]
    public enum GamepadButton : ushort
    {
        None = 0,
        PadUp = 1 << 0,
        PadDown = 1 << 1,
        PadLeft = 1 << 2,
        PadRight = 1 << 3,
        Start = 1 << 4,
        Back = 1 << 5,
        LeftThumb = 1 << 6,
        RightThumb = 1 << 7,
        LeftShoulder = 1 << 8,
        RightShoulder = 1 << 9,
        A = 1 << 12,
        B = 1 << 13,
        X = 1 << 14,
        Y = 1 << 15,
    }
}