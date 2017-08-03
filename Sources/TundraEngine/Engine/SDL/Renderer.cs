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

    public static class RendererExtensions
    {
        public static void CheckError(this Renderer renderer, string message)
        {
            Assert.IsTrue(renderer != Renderer.Null, "[SDL] " + message + ": " + SDL.GetError());
        }
    }
}