using System;
using System.Collections.Generic;
using System.Text;

namespace TundraEngine
{
    unsafe public struct ResourceRequest
    {
        public StringId64 Type;
        public StringId64 Name;
        public uint Version;
        public LoadFunction LoadFunction;
        public byte* Data;
    }
}