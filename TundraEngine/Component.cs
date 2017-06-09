using System;
using System.Runtime.InteropServices;

namespace TundraEngine
{
    [StructLayout (LayoutKind.Sequential, Pack = 2)]
    public struct Component : IEquatable<Component>
    {
        public readonly ushort ID;

        public static readonly Component Invalid;

        public Component (ushort id)
        {
            ID = id;
        }

        public bool IsValid ()
        {
            return ID != 0;
        }

        public bool Equals (Component other)
        {
            if (ID == other.ID)
            {
                return true;
            }
            return false;
        }

        public override bool Equals (object obj)
        {
            return obj is Component ? Equals ((Component)obj) : false;
        }

        public static bool operator == (Component a, Component b)
        {
            return a.ID == b.ID;
        }

        public static bool operator != (Component a, Component b)
        {
            return a.ID != b.ID;
        }

        public override int GetHashCode ()
        {
            return ID.GetHashCode ();
        }

        public override string ToString ()
        {
            return "Component " + ID;
        }
    }
}