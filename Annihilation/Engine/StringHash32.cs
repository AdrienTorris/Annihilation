using System;

namespace Engine
{
    // TODO: Currently useless, as passing a string involves hash computation or dictionary lookup.
    /// <summary>
    /// A 32 bit hashed string.
    /// </summary>
    public struct StringHash32 : IEquatable<StringHash32>
    {
        private uint _hash;
        
        public StringHash32 (string text)
        {
            _hash = text.ToHash32();
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
        
        public override int GetHashCode ()
        {
            return _hash.GetHashCode ();
        }
    }
}