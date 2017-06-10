using System;
using System.Runtime.InteropServices;
using MessagePack;

namespace TundraEngine
{
    [MessagePackObject]
    [StructLayout (LayoutKind.Sequential, Pack = 1)]
    public struct Entity : IEquatable<Entity>
    {
        [Key(0)] public readonly int Index;
        [Key(1)] public readonly byte Generation;

        public static readonly Entity Invalid;

        public Entity (int index, byte generation)
        {
            Index = index;
            Generation = generation;
        }

        public bool IsValid ()
        {
            return Index != 0;
        }

        public bool Equals (Entity other)
        {
            if (Index != other.Index)
            {
                return false;
            }
            return Generation == other.Generation;
        }

        public override bool Equals (object obj)
        {
            return obj is Entity ? Equals ((Entity)obj) : false;
        }

        public static bool operator == (Entity a, Entity b)
        {
            return a.Index == b.Index && a.Generation == b.Generation;
        }

        public static bool operator != (Entity a, Entity b)
        {
            return a.Index != b.Index || a.Generation != b.Generation;
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Index.GetHashCode ();
                hash = hash * 23 + Generation.GetHashCode ();
                return hash;
            }
        }

        public override string ToString ()
        {
            return "Entity " + Index + "-" + Generation;
        }
    }
}