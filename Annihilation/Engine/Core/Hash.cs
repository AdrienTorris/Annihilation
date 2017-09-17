using System;
using Engine.Mathematics;

namespace Engine
{
    public struct Hash : IEquatable<Hash>
    {
        public ulong Value;
        
        public static readonly Hash Null;

        public Hash(ulong hash)
        {
            Value = hash;
        }

        public Hash(string str)
        {
            Value = MetroHash.Hash64(str);
        }

        public unsafe Hash(char* chars)
        {
            Value = MetroHash.Hash64(chars, StringUtility.GetLength(chars));
        }

        public unsafe Hash(byte* bytes)
        {
            Value = MetroHash.Hash64(bytes, Utf8.GetLength(bytes));
        }
        
        public bool Equals(Hash other) => Value == other.Value;
        public override bool Equals(object obj) => obj is Hash && this == (Hash)obj;
        public override int GetHashCode() => Value.GetHashCode();
        public override string ToString() => Value.ToString();

        public static bool operator ==(Hash left, Hash right) => left.Equals(right);
        public static bool operator !=(Hash left, Hash right) => !left.Equals(right);

        public static implicit operator ulong(Hash hash) => hash.Value;
        public static implicit operator Hash(ulong hash) => new Hash(hash);
    }
}