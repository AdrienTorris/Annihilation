using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct WindowID
    {
        internal uint NativeHandle;
    }
}