using System;
using System.Runtime.InteropServices;
using System.Globalization;
using MessagePack;

namespace TundraEngine.Mathematics
{
    [MessagePackObject]
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct Half
    {
        [Key(0)] private ushort _value;


    }
}