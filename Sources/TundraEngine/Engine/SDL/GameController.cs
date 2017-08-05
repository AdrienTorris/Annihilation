using System;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public struct GameController
    {
        internal IntPtr NativeHandle;

        public unsafe string Mapping
        {
            get
            {
                byte* ptr = Native.SDL_GameControllerMapping(this);
                string str = GetString(ptr);
                Native.SDL_free(ptr);
                return str;
            }
        }

        public unsafe string Name => GetString(Native.SDL_GameControllerName(this));

        public GameController(int joystickIndex)
        {
            this = Native.SDL_GameControllerOpen(joystickIndex).CheckErrorAndReturn("Could not open game controller at index " + joystickIndex);
        }

        public GameController(JoystickID joystickID)
        {
            this = Native.SDL_GameControllerFromInstanceID(joystickID);
        }

        public bool GetAttached() => Native.SDL_GameControllerGetAttached(this);

        public Joystick GetJoystick() => Native.SDL_GameControllerGetJoystick(this);

        public GameControllerButtonBind GetBindForAxis(GameControllerAxis axis) => Native.SDL_GameControllerGetBindForAxis(this, axis);

        public short GetAxisState(GameControllerAxis axis) => Native.SDL_GameControllerGetAxis(this, axis);

        public GameControllerButtonBind GetBindForButton(GameControllerButton button) => Native.SDL_GameControllerGetBindForButton(this, button);

        public byte GetButtonState(GameControllerButton button) => Native.SDL_GameControllerGetButton(this, button);
        
        public void Close() => Native.SDL_GameControllerClose(this);
    }
}