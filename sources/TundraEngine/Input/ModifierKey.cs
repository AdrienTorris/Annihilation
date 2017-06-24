using System;

namespace TundraEngine.Input
{
    [Flags]
    public enum ModifierKeys : byte
    {
        None = 0,
        LeftShift = 1 << 0,
        RightShift = 1 << 1,
        LeftControl = 1 << 2,
        RightControl = 1 << 3,
        LeftAlt = 1 << 4,
        RightAlt = 1 << 5,
        LeftCommand = 1 << 6,
        RightCommand = 1 << 7,
        Control = LeftControl | RightControl,
        Shift = LeftShift | RightShift,
        Alt = LeftAlt | RightAlt,
        Command = LeftCommand | RightCommand
    }
}