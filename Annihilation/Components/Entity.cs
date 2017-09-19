using System;
using System.Runtime.InteropServices;

namespace Annihilation
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Entity : IEquatable<Entity>
    {
        public const uint InvalidIndex = uint.MaxValue;
        public const uint InvalidGeneration = uint.MaxValue;

        public static readonly Entity Invalid = new Entity(InvalidIndex, InvalidGeneration);

        public readonly uint Index;
        public readonly uint Generation;

        public Entity(uint index, uint generation)
        {
            Index = index;
            Generation = generation;
        }

        public bool IsValid()
        {
            return Index != InvalidIndex && Generation != InvalidGeneration;
        }

        public bool Equals(Entity other)
        {
            if (Index != other.Index)
            {
                return false;
            }
            return Generation == other.Generation;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity ? Equals((Entity)obj) : false;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            return a.Index == b.Index && a.Generation == b.Generation;
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return a.Index != b.Index || a.Generation != b.Generation;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Index.GetHashCode();
                hash = hash * 23 + Generation.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return "Entity " + Index + "-" + Generation;
        }
    }
}