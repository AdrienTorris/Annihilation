using System.Runtime.InteropServices;

namespace Engine.SDL
{
    /// <summary>
    /// The SDL keysym structure, used in key events.
    /// </summary>
    /// <remarks>
    /// If you are looking for translated character input, see the TextInput event.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
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
}