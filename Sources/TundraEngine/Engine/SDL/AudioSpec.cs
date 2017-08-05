using System;
using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void AudioCallback(void* userdata, byte* stream, int length);

    public unsafe struct AudioSpec
    {
        public int Frequency;
        public AudioFormat Format;
        public byte Channels;
        public byte Silence;
        public ushort Samples;
        public ushort Padding;
        public uint Size;
        public IntPtr Callback;
        public void* Userdata;
    }
}