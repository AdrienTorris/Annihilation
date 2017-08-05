using System;

namespace Engine.SDL
{
    public struct Renderer : IEquatable<Renderer>
    {
        internal IntPtr NativeHandle;

        public static readonly Renderer Null = new Renderer { NativeHandle = IntPtr.Zero };

        public bool Equals(Renderer other)
        {
            return NativeHandle == other.NativeHandle;
        }

        public override bool Equals(object obj)
        {
            return obj is Renderer ? Equals((Renderer)obj) : false;
        }

        public static bool operator ==(Renderer a, Renderer b)
        {
            return a.NativeHandle == b.NativeHandle;
        }

        public static bool operator !=(Renderer a, Renderer b)
        {
            return a.NativeHandle != b.NativeHandle;
        }

        public override int GetHashCode()
        {
            return NativeHandle.GetHashCode();
        }
    }
}