using System;
using System.Collections.Generic;
using Engine.Mathematics;

namespace Engine
{
    [Flags]
    public enum VariableFlags : byte
    {
        None = 0,
        Archive = 1 << 0,
        Registered = 1 << 1,
        Server = 1 << 2,
        Client = 1 << 3
    }

    public unsafe delegate void VariableCallback(Variable* var);

    public unsafe struct Variable
    {
        private static readonly Dictionary<uint, VariableCallback> _callbacks = new Dictionary<uint, VariableCallback>();

        public char* Name;
        public char* ValueString;
        public char* DefaultValueString;
        public VariableFlags Flags;
        public Value Value;
        public Variable* Next;
        public uint CallbackId;

        public Variable(char* name, Value value, VariableFlags flags, VariableCallback callback = null)
        {
            Name = name;
            Value = value;
            Flags = flags;
            ValueString = null;
            DefaultValueString = null;
            Next = null;

            if (callback != null)
            {
                CallbackId = MetroHash.Hash32(name, StringUtility.GetLength(name));
                _callbacks.Add(CallbackId, callback);
            }
            else
            {
                CallbackId = 0;
            }
        }
    }
}