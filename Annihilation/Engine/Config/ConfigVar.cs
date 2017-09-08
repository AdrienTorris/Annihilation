using System;
using System.IO;
using Engine.Graphics;

namespace Engine.Config
{
    [Flags]
    public enum ConfigVarFlags : byte
    {
        None = 0,
        Archive = 1 << 0,
        Registered = 1 << 1,
        Server = 1 << 2,
        Client = 1 << 3
    }

    public unsafe delegate void VariableCallback(ConfigVar var);

    public abstract unsafe class ConfigVar
    {
        public char* Name;
        public char* ValueString;
        public char* DefaultValueString;
        public ConfigVarFlags Flags;
        public Value Value;
        //public uint CallbackId;

        public ConfigVar(char* name, Value value, ConfigVarFlags flags)
        {
            Name = name;
            Value = value;
            Flags = flags;
            ValueString = null;
            DefaultValueString = null;
        }

        public abstract void Increment();
        public abstract void Decrement();
        public abstract void Select();

        public abstract void DisplayValue(TextContext textContext);
        public abstract void SetValue(FileStream file, string setting);

        public override string ToString() => "";
    }

    
}