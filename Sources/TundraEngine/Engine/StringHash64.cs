using System;
using Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// A 64 bit hashed string.
    /// </summary>
    public struct StringHash64 : IEquatable<StringHash64>
    {
        private ulong _hash;

        public StringHash64 (string text)
        {
            _hash = text.GetCityHash64();
        }

        public bool IsValid()
        {
            return _hash != 0;
        }

        public bool Equals(StringHash64 other)
        {
            return _hash == other._hash;
        }

        public override bool Equals(object obj)
        {
            return obj is StringHash64 ? Equals((StringHash64)obj) : false;
        }

        public static bool operator ==(StringHash64 a, StringHash64 b)
        {
            return a._hash == b._hash;
        }

        public static bool operator !=(StringHash64 a, StringHash64 b)
        {
            return a._hash != b._hash;
        }

        public static implicit operator StringHash64(string text)
        {
            return new StringHash64(text);
        }

        public override int GetHashCode()
        {
            return _hash.GetHashCode();
        }
    }
}