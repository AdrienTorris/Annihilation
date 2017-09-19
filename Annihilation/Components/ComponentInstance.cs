using System;

namespace Engine.EntityComponent
{
    public struct Index<T> : IEquatable<Index<T>>
    {
        private int _value;

        public static readonly Index<T> Invalid = -1;

        public bool Equals(Index<T> other) => _value == other._value;
        public override bool Equals(object obj) => obj is Index<T> && this == (Index<T>)obj;
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();

        public static bool operator ==(Index<T> left, Index<T> right) => left.Equals(right);
        public static bool operator !=(Index<T> left, Index<T> right) => !left.Equals(right);

        public static implicit operator int(Index<T> transform) => transform._value;
        public static implicit operator Index<T>(int value) => new Index<T> { _value = value };
    }
}