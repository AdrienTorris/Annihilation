using System;
using System.Collections.Generic;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    public struct Text : IEquatable<Text>
    {
        private uint _hash;

        public static readonly Dictionary<uint, string> TextMap = new Dictionary<uint, string> ();

        public Text (string text)
        {
            _hash = MathUtility.Hash (text);
            if (TextMap.ContainsKey (_hash) == false)
            {
                TextMap.Add (_hash, text);
            }
        }

        public bool IsValid ()
        {
            return _hash != 0;
        }

        public bool Equals (Text other)
        {
            return _hash == other._hash;
        }

        public override bool Equals (object obj)
        {
            return obj is Text ? Equals ((Text)obj) : false;
        }

        public static bool operator == (Text a, Text b)
        {
            return a._hash == b._hash;
        }

        public static bool operator != (Text a, Text b)
        {
            return a._hash != b._hash;
        }

        public static implicit operator string (Text text)
        {
            return TextMap[text._hash];
        }

        public static implicit operator Text (string text)
        {
            return new Text (text);
        }

        public override int GetHashCode ()
        {
            return _hash.GetHashCode ();
        }
    }
}