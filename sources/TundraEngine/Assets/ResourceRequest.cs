using System;
using System.Collections.Generic;
using System.Text;

namespace TundraEngine
{
    unsafe public struct ResourceRequest
    {
        public StringHash64 Type;
        public StringHash64 Name;
        public uint Version;
        public LoadFunction LoadFunction;
        public byte* Data;
    }
}