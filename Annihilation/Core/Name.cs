using System;
using System.Collections.Generic;
using Engine.Mathematics;

namespace Engine
{
    /// <summary>
    /// Represents a hashed string.
    /// </summary>
    public unsafe struct Name : IEquatable<Name>
    {
        public static readonly Name Null;

        private static readonly Dictionary<ulong, string> _stringMap = new Dictionary<ulong, string>(512);

        private ulong _hash;

        public bool IsValid => _hash != 0;

        public Name(string str)
        {
            _hash = MetroHash.Hash64(str);

            if (_stringMap.ContainsKey(_hash) == false)
            {
                _stringMap.Add(_hash, str);
            }
        }
        
        public bool Equals(Name other) => _hash == other._hash;
        public override bool Equals(object obj) => obj is Name && this == (Name)obj;
        public override int GetHashCode() => _hash.GetHashCode();
        public override string ToString() => _stringMap[_hash];

        public static bool operator ==(Name left, Name right) => left.Equals(right);
        public static bool operator !=(Name left, Name right) => !left.Equals(right);
    }
}