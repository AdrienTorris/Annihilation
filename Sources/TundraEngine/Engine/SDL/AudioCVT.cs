using System;
using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate void AudioFilter(AudioCVT* cvt, AudioFormat format);

    public unsafe struct AudioCVT
    {
        public int Needed;
        public AudioFormat SrcFormat;
        public AudioFormat DstFormat;
        public double RateIncrement;
        public byte* Buffer;
        public int Length;
        public int LengthCVT;
        public int LengthMultiplier;
        public double LengthRatio;
        // TODO: Need fixed array (10) of AudioFilter
        public IntPtr Filters;
        public int FilterIndex;
    }
}