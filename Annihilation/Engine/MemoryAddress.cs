using System.Runtime.InteropServices;

namespace Engine.Memory
{
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    unsafe public struct MemoryAddress
    {
        [FieldOffset(0)] public byte* Address;
    }
}