using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct KeySym
    {
        public readonly ScanCode ScanCode;
        public readonly KeyCode Sym;
        public readonly KeyMod Mod;
        public readonly uint Unused;
    }
}