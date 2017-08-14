using System.Runtime.InteropServices;

namespace SDL2
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