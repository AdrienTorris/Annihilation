using System;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    /// <summary>
    /// A 32 bit hashed string.
    /// </summary>
    public struct StringId32 : IEquatable<StringId32>
    {
        private uint _hash;
        
        public StringId32 (string text)
        {
            ulong hash = text.GetCityHash64();
            _hash = (uint)(hash >> 32);
        }

        public bool IsValid ()
        {
            return _hash != 0;
        }

        public bool Equals (StringId32 other)
        {
            return _hash == other._hash;
        }

        public override bool Equals (object obj)
        {
            return obj is StringId32 ? Equals ((StringId32)obj) : false;
        }

        public static bool operator == (StringId32 a, StringId32 b)
        {
            return a._hash == b._hash;
        }

        public static bool operator != (StringId32 a, StringId32 b)
        {
            return a._hash != b._hash;
        }
        
        public static implicit operator StringId32 (string text)
        {
            return new StringId32 (text);
        }

        public override int GetHashCode ()
        {
            return _hash.GetHashCode ();
        }
    }
}