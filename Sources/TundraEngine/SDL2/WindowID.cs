using System.Runtime.InteropServices;

namespace SDL2
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct WindowID
    {
        internal uint NativeHandle;
    }
}