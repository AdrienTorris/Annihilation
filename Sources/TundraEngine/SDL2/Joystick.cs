using System;
using System.Runtime.CompilerServices;

using static SDL2.SDL;

namespace SDL2
{
    public enum JoystickPowerLevel
    {
        Unknown = -1,
        Empty,
        Low,
        Medium,
        Full,
        Wired,
        Max
    }

    public enum JoystickAxis
    {
        X = 0,
        Y = 1
    }

    public enum JoystickHat : byte
    {
        Centered = 0x00,
        Up = 0x01,
        Right = 0x02,
        Down = 0x04,
        Left = 0x08,
        RightUp = Right | Up,
        RightDown = Right | Down,
        LeftUp = Left | Up,
        LeftDown = Left | Down
    }

    public struct Joystick
    {
        internal IntPtr NativeHandle;

        //
        // Properties
        //
        public unsafe string Name => Native.SDL_JoystickName(this).ToString();

        public JoystickID InstanceID => Native.SDL_JoystickInstanceID(this);

        public int NumAxes => Native.SDL_JoystickNumAxes(this);

        public int NumBalls => Native.SDL_JoystickNumBalls(this);

        public int NumHats => Native.SDL_JoystickNumHats(this);

        public int NumButtons => Native.SDL_JoystickNumButtons(this);

        public JoystickPowerLevel CurrentPowerLevel => Native.SDL_JoystickCurrentPowerLevel(this);

        //
        // Constructors
        //
        public Joystick(int deviceIndex)
        {
            this = Native.SDL_JoystickOpen(deviceIndex).CheckErrorAndReturn("Could not open joystick " + deviceIndex);
        }

        public Joystick(JoystickID joystickID)
        {
            this = Native.SDL_JoystickFromInstanceID(joystickID);
        }

        //
        // Methods
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Guid GetGuid() => Native.SDL_JoystickGetGUID(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool GetAttached() => Native.SDL_JoystickGetAttached(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public short GetAxisState(JoystickAxis axis) => Native.SDL_JoystickGetAxis(this, axis);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JoystickHat GetHatState(int hat) => Native.SDL_JoystickGetHat(this, hat);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public unsafe int GetBallState(int ball, out int dx, out int dy) => Native.SDL_JoystickGetBall(this, ball, out dx, out dy).CheckErrorAndReturn("Could not get ball state");

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public byte GetButtonState(int button) => Native.SDL_JoystickGetButton(this, button);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Close() => Native.SDL_JoystickClose(this);
    }
}