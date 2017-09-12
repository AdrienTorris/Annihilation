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
        public ConfigVarGroup Group;
        
        public ConfigVar() { }

        public ConfigVar(string path)
        {
            ConfigSystem.RegisterVar(path, this);
        }

        public ConfigVar Next()
        {
            ConfigVar next = null;

            if (this is ConfigVarGroup group && group.IsExpanded)
            {
                next = group.FirstVar();
            }

            if (next == null)
            {
                next = Group.NextVar(this);
            }

            return next ?? this;
        }

        public ConfigVar Previous()
        {
            ConfigVar previous = Group.PreviousVar(this);
            if (previous != null && previous != Group)
            {
                if (previous is ConfigVarGroup group && group.IsExpanded)
                {
                    previous = group.LastVar();
                }
            }

            return previous ?? this;
        }

        public abstract void Increment();
        public abstract void Decrement();
        public abstract void Select();

        public abstract void DisplayValue(TextContext textContext);
        public abstract void SetValue(StreamReader stream, string setting);

        public override string ToString() => "";
    }

    public class BoolVar : ConfigVar
    {
        private bool _value;

        public BoolVar(string path, bool value) : base(path)
        {
            _value = value;
        }

        public override void Increment() => _value = true;
        public override void Decrement() => _value = false;
        public override void Select() => _value = !_value;

        public override string ToString()
        {
            return _value ? "on" : "off";
        }

        public override void DisplayValue(TextContext textContext)
        {
            textContext.Draw(_value ? "[X]" : "[-]");
        }
        
        public override void SetValue(StreamReader stream, string setting)
        {

        }

        public static implicit operator bool(BoolVar var) => var._value;
    }
}