using System;
using System.Collections.Generic;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    public struct StringId32 : IEquatable<StringId32>
    {
        private uint _hash;

        private static readonly Dictionary<uint, string> _stringMap = new Dictionary<uint, string> ();

        public StringId32 (string text)
        {
            _hash = MathUtility.Hash (text);
            if (_stringMap.ContainsKey (_hash) == false)
            {
                _stringMap.Add (_hash, text);
            }
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

        public static implicit operator string (StringId32 stringId)
        {
            Assert.IsTrue(_stringMap.ContainsKey(stringId._hash), "String map does not contain Id \"" + stringId.ToString() + "\"");
            return _stringMap[stringId._hash];
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