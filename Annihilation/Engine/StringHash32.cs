using System;
using System.Collections.Generic;
using Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// A 32 bit hashed string.
    /// </summary>
    public struct StringHash32 : IEquatable<StringHash32>
    {
        private static Dictionary<string, StringHash32> _map = new Dictionary<string, StringHash32>(128);

        private uint _hash;
        
        public StringHash32 (string text)
        {
            ulong hash = text.GetCityHash64();
            _hash = (uint)(hash >> 32);

            _map.Add(text, this);
        }

        public bool IsValid ()
        {
            return _hash != 0;
        }

        public bool Equals (StringHash32 other)
        {
            return _hash == other._hash;
        }

        public override bool Equals (object obj)
        {
            return obj is StringHash32 ? Equals ((StringHash32)obj) : false;
        }

        public static bool operator == (StringHash32 a, StringHash32 b)
        {
            return a._hash == b._hash;
        }

        public static bool operator != (StringHash32 a, StringHash32 b)
        {
            return a._hash != b._hash;
        }
        
        public static implicit operator StringHash32 (string text)
        {
            if (_map.TryGetValue(text, out StringHash32 hash))
            {
                return hash;
            }
            else
            {
                return new StringHash32(text);
            }
        }

        public override int GetHashCode ()
        {
            return _hash.GetHashCode ();
        }
    }
}