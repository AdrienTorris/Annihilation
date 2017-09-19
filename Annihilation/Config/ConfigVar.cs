using System;
using System.IO;
using System.Runtime.InteropServices;
using Annihilation.Graphics;

namespace Annihilation.Config
{
    /*
    ===============================================================================
	Config variables
    ===============================================================================
    */
    [Flags]
    public enum ConfigVarFlags : byte
    {
        None = 0,
        Bool = 1 << 0,
        Int = 1 << 1,
        Float = 1 << 2,
        Cheat = 1 << 3,
        Archive = 1 << 4,
        Registered = 1 << 5,
        Server = 1 << 6,
        Client = 1 << 7
    }
    
    public struct BoolVar
    {
        public bool Value;

        private Name _hash;

        public BoolVar(string name, bool value)
        {
            Value = value;

            _hash = new Name(name);

            ConfigSystem.RegisterBool(_hash, this);
        }
        
        public override string ToString()
        {
            return Value ? "true" : "false";
        }

        public void DisplayValue(TextContext textContext)
        {

        }

        public void SetValue(StreamReader stream, string setting)
        {

        }

        public void SetValue(bool value)
        {
            Value = value;
        }

        public static implicit operator bool(BoolVar var) => var.Value;
    }

    public struct IntVar
    {
        public int Value;
        public int MinValue;
        public int MaxValue;

        private Name _hash;

        public IntVar(string name, int value, int min, int max)
        {
            Value = value;
            MinValue = min;
            MaxValue = max;

            _hash = new Name(name);

            ConfigSystem.RegisterInt(_hash, this);
        }

        public IntVar(string name, int value) : this(name, value, 0, int.MaxValue) { }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator int(IntVar var) => var.Value;
    }

    public struct FloatVar
    {
        public float Value;
        public float MinValue;
        public float MaxValue;

        private Name _hash;

        public FloatVar(string name, float value, float min, float max)
        {
            Value = value;
            MinValue = min;
            MaxValue = max;

            _hash = new Name(name);

            ConfigSystem.RegisterFloat(_hash, this);
        }

        public FloatVar(string name, float value) : this(name, value, 0, float.MaxValue) { }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator float(FloatVar var) => var.Value;
    }

    public struct StringVar
    {
        public Name Value;

        private Name _hash;

        public StringVar(string name, string value)
        {
            Value = new Name(value);

            _hash = new Name(name);

            ConfigSystem.RegisterString(_hash, this);
        }
    }

    public struct EnumVar<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        public T Value;

        private Name _hash;

        public EnumVar(string name, T value)
        {
            Value = value;

            _hash = new Name(name);
            

        }

        public static implicit operator T(EnumVar<T> var) => var.Value;
    }
}