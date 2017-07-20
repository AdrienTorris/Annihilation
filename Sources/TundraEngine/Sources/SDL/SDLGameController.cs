using System;
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

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct SDL_Hat
        {
            public readonly int Hat;
            public readonly int HatMask;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct SDL_GameControllerButtonBind
        {
            [FieldOffset(0)] public readonly SDL_GameControllerBindType BindType;
            [FieldOffset(4)] public readonly int Button;
            [FieldOffset(4)] public readonly int Axis;
            [FieldOffset(4)] public readonly SDL_Hat Hat;
        }

        [DllImport(LibName)]
        private static extern int SDL_GameControllerAddMappingsFromRW(IntPtr rwOps, int freeRW);

        /// <summary>
        /// Load a set of mappings from a seekable SDL data stream (memory or file), filtered by the current SDL_GetPlatform()
        /// <para/> A community sourced database of controllers is available at https://raw.github.com/gabomdq/SDL_GameControllerDB/master/gamecontrollerdb.txt
        /// <para/> If \c freerw is non-zero, the stream will be closed after being read.
        /// </summary>
        /// <returns> Number of mappings added, -1 on error. </returns>
        public static int SDL_GameControllerAddMappingsFromFile(string file)
        {
            return SDL_GameControllerAddMappingsFromRW(SDL_RWFromFile(file, "rb"), 1);
        }

        // TODO: Format all comments here
        /**
         *  Add or update an existing mapping configuration
         *
         * \return 1 if mapping is added, 0 if updated, -1 on error
         */
        [DllImport(LibName)]
        public static extern int SDL_GameControllerAddMapping(string mappingString);

        /**
         *  Get the number of mappings installed
         *
         *  \return the number of mappings
         */
        [DllImport(LibName)]
        public static extern int SDL_GameControllerNumMappings();

        /**
         *  Get the mapping at a particular index.
         *
         *  \return the mapping string.  Must be freed with SDL_free().  Returns NULL if the index is out of range.
         */
        [DllImport(LibName)]
        public static extern IntPtr SDL_GameControllerMappingForIndex(int mapping_index);

        /**
         *  Get a mapping string for a GUID
         *
         *  \return the mapping string.  Must be freed with SDL_free().  Returns NULL if no mapping is available
         */
        [DllImport(LibName)]
        public static extern IntPtr SDL_GameControllerMappingForGUID(Guid guid);

        /**
         *  Get a mapping string for an open GameController
         *
         *  \return the mapping string.  Must be freed with SDL_free().  Returns NULL if no mapping is available
         */
        [DllImport(LibName)]
        public static extern IntPtr SDL_GameControllerMapping(IntPtr gamecontroller);

        /**
         *  Is the joystick on this index supported by the game controller interface?
         */
        [DllImport(LibName)]
        public static extern bool SDL_IsGameController(int joystick_index);

        /**
         *  Get the implementation dependent name of a game controller.
         *  This can be called before any controllers are opened.
         *  If no name can be found, this function returns NULL.
         */
        [DllImport(LibName)]
        public static extern string SDL_GameControllerNameForIndex(int joystick_index);

        /**
         *  Open a game controller for use.
         *  The index passed as an argument refers to the N'th game controller on the system.
         *  This index is not the value which will identify this controller in future
         *  controller events.  The joystick's instance id (::SDL_JoystickID) will be
         *  used there instead.
         *
         *  \return A controller identifier, or NULL if an error occurred.
         */
        [DllImport(LibName)]
        public static extern IntPtr SDL_GameControllerOpen(int joystick_index);

        /**
         * Return the SDL_GameController associated with an instance id.
         */
        [DllImport(LibName)]
        public static extern IntPtr SDL_GameControllerFromInstanceID(int joyid);

        /**
         *  Return the name for this currently opened controller
         */
        [DllImport(LibName)]
        public static extern string SDL_GameControllerName(IntPtr gamecontroller);

        /**
         *  Get the USB vendor ID of an opened controller, if available.
         *  If the vendor ID isn't available this function returns 0.
         */
        [DllImport(LibName)]
        public static extern ushort SDL_GameControllerGetVendor(IntPtr gamecontroller);

        /**
         *  Get the USB product ID of an opened controller, if available.
         *  If the product ID isn't available this function returns 0.
         */
        [DllImport(LibName)]
        public static extern ushort SDL_GameControllerGetProduct(IntPtr gamecontroller);

        /**
         *  Get the product version of an opened controller, if available.
         *  If the product version isn't available this function returns 0.
         */
        [DllImport(LibName)]
        public static extern ushort SDL_GameControllerGetProductVersion(IntPtr gamecontroller);

        /**
         *  Returns SDL_TRUE if the controller has been opened and currently connected,
         *  or SDL_FALSE if it has not.
         */
        [DllImport(LibName)]
        public static extern bool SDL_GameControllerGetAttached(IntPtr gamecontroller);

        /**
         *  Get the underlying joystick object used by a controller
         */
        [DllImport(LibName)]
        public static extern IntPtr SDL_GameControllerGetJoystick(IntPtr gamecontroller);

        /**
         *  Enable/disable controller event polling.
         *
         *  If controller events are disabled, you must call SDL_GameControllerUpdate()
         *  yourself and check the state of the controller when you want controller
         *  information.
         *
         *  The state can be one of ::SDL_QUERY, ::SDL_ENABLE or ::SDL_IGNORE.
         */
        [DllImport(LibName)]
        public static extern int SDL_GameControllerEventState(int state);

        /**
         *  Update the current state of the open game controllers.
         *
         *  This is called automatically by the event loop if any game controller
         *  events are enabled.
         */
        [DllImport(LibName)]
        public static extern void SDL_GameControllerUpdate();

        /**
         *  turn this string into a axis mapping
         */
        [DllImport(LibName)]
        public static extern SDL_GameControllerAxis SDL_GameControllerGetAxisFromString(string pchString);

        /**
         *  turn this axis enum into a string mapping
         */
        [DllImport(LibName)]
        public static extern string SDL_GameControllerGetStringForAxis(SDL_GameControllerAxis axis);

        /**
         *  Get the SDL joystick layer binding for this controller button mapping
         */
        [DllImport(LibName)]
        public static extern SDL_GameControllerButtonBind
        SDL_GameControllerGetBindForAxis(IntPtr gamecontroller,
                                         SDL_GameControllerAxis axis);

        /**
         *  Get the current state of an axis control on a game controller.
         *
         *  The state is a value ranging from -32768 to 32767 (except for the triggers,
         *  which range from 0 to 32767).
         *
         *  The axis indices start at index 0.
         */
        [DllImport(LibName)]
        public static extern short
        SDL_GameControllerGetAxis(IntPtr gamecontroller,
                                  SDL_GameControllerAxis axis);

        /**
         *  turn this string into a button mapping
         */
        [DllImport(LibName)]
        public static extern SDL_GameControllerButton SDL_GameControllerGetButtonFromString(string pchString);

        /**
         *  turn this button enum into a string mapping
         */
        [DllImport(LibName)]
        public static extern string SDL_GameControllerGetStringForButton(SDL_GameControllerButton button);

        /**
         *  Get the SDL joystick layer binding for this controller button mapping
         */
        [DllImport(LibName)]
        public static extern SDL_GameControllerButtonBind
        SDL_GameControllerGetBindForButton(IntPtr gamecontroller,
                                           SDL_GameControllerButton button);


        /**
         *  Get the current state of a button on a game controller.
         *
         *  The button indices start at index 0.
         */
        [DllImport(LibName)]
        public static extern byte SDL_GameControllerGetButton(IntPtr gamecontroller,
                                                                  SDL_GameControllerButton button);

        /**
         *  Close a controller previously opened with SDL_GameControllerOpen().
         */
        [DllImport(LibName)]
        public static extern void SDL_GameControllerClose(IntPtr gamecontroller);
    }
}