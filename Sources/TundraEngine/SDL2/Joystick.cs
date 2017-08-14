using System;
using System.Runtime.CompilerServices;

namespace SDL2
{
    public struct Joystick
    {
        internal IntPtr NativeHandle;

        //
        // Properties
        //
        public unsafe string Name => SDL.JoystickName(this).ToString();

        public JoystickID InstanceID => SDL.JoystickInstanceID(this);

        public int NumAxes => SDL.JoystickNumAxes(this);

        public int NumBalls => SDL.JoystickNumBalls(this);

        public int NumHats => SDL.JoystickNumHats(this);

        public int NumButtons => SDL.JoystickNumButtons(this);

        public JoystickPowerLevel CurrentPowerLevel => SDL.JoystickCurrentPowerLevel(this);

        //
        // Constructors
        //
        public Joystick(int deviceIndex)
        {
            this = SDL.JoystickOpen(deviceIndex).CheckErrorAndReturn("Could not open joystick " + deviceIndex);
        }

        public Joystick(JoystickID joystickID)
        {
            this = SDL.JoystickFromInstanceID(joystickID);
        }

        //
        // Methods
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public Guid GetGuid() => SDL.JoystickGetGUID(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public bool GetAttached() => SDL.JoystickGetAttached(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public short GetAxisState(JoystickAxis axis) => SDL.JoystickGetAxis(this, axis);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public JoystickHat GetHatState(int hat) => SDL.JoystickGetHat(this, hat);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public unsafe int GetBallState(int ball, out int dx, out int dy) => SDL.JoystickGetBall(this, ball, out dx, out dy).CheckErrorAndReturn("Could not get ball state");

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public byte GetButtonState(int button) => SDL.JoystickGetButton(this, button);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public void Close() => SDL.JoystickClose(this);
    }
}