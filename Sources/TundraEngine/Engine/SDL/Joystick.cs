using System;

namespace Engine.SDL
{
    public enum JoystickAxis
    {
        X = 0,
        Y = 1
    }

    public struct Joystick
    {
        internal IntPtr NativeHandle;
    }
}