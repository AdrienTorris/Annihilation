using System;
using System.Runtime.InteropServices;

namespace TundraEngine
{
    public struct StaticString
    {
        public int Length;
        public IntPtr Buffer;
    }
}