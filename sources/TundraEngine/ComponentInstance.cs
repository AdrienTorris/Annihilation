using System;
using System.Runtime.InteropServices;

namespace TundraEngine
{
    [StructLayout (LayoutKind.Sequential, Pack = 4)]
    public struct ComponentInstance : IEquatable<ComponentInstance>
    {
        public readonly int Index;

        public static ComponentInstance Invalid = new ComponentInstance (-1);
        
        private ComponentInstance (int index)
        {
            Index = index;
        }

        public bool IsValid ()
        {
            return Index != -1;
        }

        public bool Equals (ComponentInstance other)
        {
            return Index == other.Index;
        }

        public override bool Equals (object obj)
        {
            return obj is ComponentInstance ? Equals ((ComponentInstance)obj) : false;
        }

        public static bool operator == (ComponentInstance a, ComponentInstance b)
        {
            return a.Index == b.Index;
        }

        public static bool operator != (ComponentInstance a, ComponentInstance b)
        {
            return a.Index != b.Index;
        }

        public override int GetHashCode ()
        {
            return Index.GetHashCode ();
        }

        public override string ToString ()
        {
            return "Component Instance " + Index;
        }

        public static implicit operator int (ComponentInstance c)
        {
            return c.Index;
        }

        public static implicit operator ComponentInstance (int i)
        {
            return new ComponentInstance (i);
        }
    }
}