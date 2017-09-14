using System;
using System.IO;
using System.Runtime.InteropServices;
using Engine.Graphics;

namespace Engine.Config
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
        private bool _value;

        public BoolVar(string name, bool value)
        {
            _value = value;

            ConfigSystem.RegisterBool(name, this);
        }
        
        public override string ToString()
        {
            return _value ? "true" : "false";
        }

        public void DisplayValue(TextContext textContext)
        {

        }

        public void SetValue(StreamReader stream, string setting)
        {

        }

        public void SetValue(bool value)
        {
            _value = value;
        }

        public static implicit operator bool(BoolVar var) => var._value;
    }

    public class IntVar
    {
        private int _value;
        private int _minValue;
        private int _maxValue;

        public IntVar(string name, int value, int min, int max)
        {
            _value = value;
            _minValue = min;
            _maxValue = max;

            ConfigSystem.RegisterInt(name, this);
        }

        public IntVar(string name, int value) : this(name, value, 0, int.MaxValue) { }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}