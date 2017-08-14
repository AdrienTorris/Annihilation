using System;
using System.Runtime.CompilerServices;

namespace SDL2
{
    public static class JoystickExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Joystick CheckErrorAndReturn(this Joystick joystick, string message)
        {
            Assert.IsTrue(joystick.NativeHandle != IntPtr.Zero, "[SDL] " + message + ": " + SDL.GetError());
            return joystick;
        }
    }
}