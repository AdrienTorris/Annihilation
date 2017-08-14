using System;

namespace SDL2
{
    public struct GameController
    {
        internal IntPtr NativeHandle;

        public unsafe string Mapping
        {
            get
            {
                byte* ptr = SDL.GameControllerMapping(this);
                string str = GetString(ptr);
                SDL.Free(ptr);
                return str;
            }
        }

        public unsafe string Name => SDL.GameControllerName(this);

        public GameController(int joystickIndex)
        {
            this = SDL.GameControllerOpen(joystickIndex).CheckErrorAndReturn("Could not open game controller at index " + joystickIndex);
        }

        public GameController(JoystickID joystickID)
        {
            this = SDL.GameControllerFromInstanceID(joystickID);
        }

        public bool GetAttached() => SDL.GameControllerGetAttached(this);

        public Joystick GetJoystick() => SDL.GameControllerGetJoystick(this);

        public GameControllerButtonBind GetBindForAxis(GameControllerAxis axis) => SDL.GameControllerGetBindForAxis(this, axis);

        public short GetAxisState(GameControllerAxis axis) => SDL.GameControllerGetAxis(this, axis);

        public GameControllerButtonBind GetBindForButton(GameControllerButton button) => SDL.GameControllerGetBindForButton(this, button);

        public byte GetButtonState(GameControllerButton button) => SDL.GameControllerGetButton(this, button);
        
        public void Close() => SDL.GameControllerClose(this);
    }
}