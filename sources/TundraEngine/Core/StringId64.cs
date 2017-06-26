using System;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    /// <summary>
    /// A 64 bit hashed string.
    /// </summary>
    public struct StringId64 : IEquatable<StringId64>
    {
        private ulong _hash;

        public StringId64 (string text)
        {
            _hash = text.GetCityHash64();
        }

        public bool IsValid()
        {
            return _hash != 0;
        }

        public bool Equals(StringId64 other)
        {
            return _hash == other._hash;
        }

        public override bool Equals(object obj)
        {
            return obj is StringId64 ? Equals((StringId64)obj) : false;
        }

        public static bool operator ==(StringId64 a, StringId64 b)
        {
            return a._hash == b._hash;
        }

        public static bool operator !=(StringId64 a, StringId64 b)
        {
            return a._hash != b._hash;
        }

        public static implicit operator StringId64(string text)
        {
            return new StringId64(text);
        }

        public override int GetHashCode()
        {
            return _hash.GetHashCode();
        }
    }
}