﻿using System;

namespace TundraEngine.MessagePack
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class MessagePackObjectAttribute : Attribute
    {
        public bool KeyAsPropertyName { get; private set; }

        public MessagePackObjectAttribute(bool keyAsPropertyName = false)
        {
            KeyAsPropertyName = keyAsPropertyName;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class KeyAttribute : Attribute
    {
        public int? IntKey { get; private set; }
        public string StringKey { get; private set; }

        public KeyAttribute(int x)
        {
            IntKey = x;
        }

        public KeyAttribute(string x)
        {
            StringKey = x;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class IgnoreMemberAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
    public class UnionAttribute : Attribute
    {
        public int Key { get; private set; }
        public Type SubType { get; private set; }

        public UnionAttribute(int key, Type subType)
        {
            Key = key;
            SubType = subType;
        }
    }

    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
    public class SerializationConstructorAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class MessagePackFormatterAttribute : Attribute
    {
        public Type FormatterType { get; private set; }

        public MessagePackFormatterAttribute(Type formatterType)
        {
            FormatterType = formatterType;
        }
    }
}