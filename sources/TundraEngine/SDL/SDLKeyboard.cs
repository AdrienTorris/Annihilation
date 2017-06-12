using System.Runtime.InteropServices;

namespace SDL2
{
    /// <summary>
    /// The SDL keysym structure, used in key events.
    /// </summary>
    /// <remarks>
    /// If you are looking for translated character input, see the TextInput event.
    /// </remarks>
    [StructLayout (LayoutKind.Sequential)]
    public struct KeySym
    {
        /// <summary>
        /// SDL physical key code - see ScanCode for details
        /// </summary>
        public readonly ScanCode ScanCode;
        /// <summary>
        /// SDL virtual key code - see KeyCode for details
        /// </summary>
        public readonly KeyCode Sym;
        /// <summary>
        /// Current key modifiers
        /// </summary>
        public readonly KeyMod Mod;
        public readonly uint Unused;
    }

    public static partial class SDL
    {
        /**
        *  \brief Get the window which currently has keyboard focus.
        */
        extern public static SDL_Window* GetKeyboardFocus ();

        /**
         *  \brief Get a snapshot of the current state of the keyboard.
         *
         *  \param numkeys if non-NULL, receives the length of the returned array.
         *
         *  \return An array of key states. Indexes into this array are obtained by using ::SDL_Scancode values.
         *
         *  \b Example:
         *  \code
         *  const Uint8 *state = SDL_GetKeyboardState(NULL);
         *  if ( state[SDL_SCANCODE_RETURN] )   {
         *      printf("<RETURN> is pressed.\n");
         *  }
         *  \endcode
         */
        extern public static const Uint8* GetKeyboardState (int * numkeys);

        /**
         *  \brief Get the current key modifier state for the keyboard.
         */
        extern public static SDL_Keymod GetModState ();

        /**
         *  \brief Set the current key modifier state for the keyboard.
         *
         *  \note This does not change the keyboard state, only the key modifier flags.
         */
        extern public static void SetModState (SDL_Keymod modstate);

        /**
         *  \brief Get the key code corresponding to the given scancode according
         *         to the current keyboard layout.
         *
         *  See ::SDL_Keycode for details.
         *
         *  \sa SDL_GetKeyName()
         */
        extern public static SDL_Keycode GetKeyFromScancode (SDL_Scancode scancode);

        /**
         *  \brief Get the scancode corresponding to the given key code according to the
         *         current keyboard layout.
         *
         *  See ::SDL_Scancode for details.
         *
         *  \sa SDL_GetScancodeName()
         */
        extern public static SDL_Scancode GetScancodeFromKey (SDL_Keycode key);

        /**
         *  \brief Get a human-readable name for a scancode.
         *
         *  \return A pointer to the name for the scancode.
         *          If the scancode doesn't have a name, this function returns
         *          an empty string ("").
         *
         *  \sa SDL_Scancode
         */
        extern public static const char* GetScancodeName (SDL_Scancode scancode);

        /**
         *  \brief Get a scancode from a human-readable name
         *
         *  \return scancode, or SDL_SCANCODE_UNKNOWN if the name wasn't recognized
         *
         *  \sa SDL_Scancode
         */
        extern public static SDL_Scancode GetScancodeFromName (const char* name);

        /**
         *  \brief Get a human-readable name for a key.
         *
         *  \return A pointer to a UTF-8 string that stays valid at least until the next
         *          call to this function. If you need it around any longer, you must
         *          copy it.  If the key doesn't have a name, this function returns an
         *          empty string ("").
         *
         *  \sa SDL_Keycode
         */
        extern public static const char* GetKeyName (SDL_Keycode key);

        /**
         *  \brief Get a key code from a human-readable name
         *
         *  \return key code, or SDLK_UNKNOWN if the name wasn't recognized
         *
         *  \sa SDL_Keycode
         */
        extern public static SDL_Keycode GetKeyFromName (const char* name);

        /**
         *  \brief Start accepting Unicode text input events.
         *         This function will show the on-screen keyboard if supported.
         *
         *  \sa SDL_StopTextInput()
         *  \sa SDL_SetTextInputRect()
         *  \sa SDL_HasScreenKeyboardSupport()
         */
        extern public static void StartTextInput ();

        /**
         *  \brief Return whether or not Unicode text input events are enabled.
         *
         *  \sa SDL_StartTextInput()
         *  \sa SDL_StopTextInput()
         */
        extern public static SDL_bool IsTextInputActive ();

        /**
         *  \brief Stop receiving any text input events.
         *         This function will hide the on-screen keyboard if supported.
         *
         *  \sa SDL_StartTextInput()
         *  \sa SDL_HasScreenKeyboardSupport()
         */
        extern public static void StopTextInput ();

        /**
         *  \brief Set the rectangle used to type Unicode text inputs.
         *         This is used as a hint for IME and on-screen keyboard placement.
         *
         *  \sa SDL_StartTextInput()
         */
        extern public static void SetTextInputRect (SDL_Rect* rect);

        /**
         *  \brief Returns whether the platform has some screen keyboard support.
         *
         *  \return SDL_TRUE if some keyboard support is available else SDL_FALSE.
         *
         *  \note Not all screen keyboard functions are supported on all platforms.
         *
         *  \sa SDL_IsScreenKeyboardShown()
         */
        extern public static SDL_bool HasScreenKeyboardSupport ();

        /**
         *  \brief Returns whether the screen keyboard is shown for given window.
         *
         *  \param window The window for which screen keyboard should be queried.
         *
         *  \return SDL_TRUE if screen keyboard is shown else SDL_FALSE.
         *
         *  \sa SDL_HasScreenKeyboardSupport()
         */
        extern public static SDL_bool IsScreenKeyboardShown (SDL_Window* window);
    }
}