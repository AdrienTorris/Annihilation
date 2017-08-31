namespace OpenDDL
{
    public enum PropertyType
    {
        Bool,
        Integer,
        Float,
        String,
        Reference,
        Type
    }

    public abstract class Property
    {
        public Identifier Identifier { get; set; }
        public PropertyType Type { get; set; }

        protected Property(Identifier identifier, PropertyType type)
        {
            Identifier = identifier;
            Type = type;
        }
    }

    public class Property<T> : Property
    {
        public T Value { get; set; }

        public Property(Identifier identifier, PropertyType type, T value) : base(identifier, type)
        {
            Value = value;
        }
    }

    public class BoolProperty : Property<bool>
    {
        public BoolProperty(Identifier identifier, bool value) : base(identifier, PropertyType.Bool, value) { }
    }

    public class IntProperty : Property<int>
    {
        public IntProperty(Identifier identifier, int value) : base(identifier, PropertyType.Integer, value) { }
    }

    public class FloatProperty : Property<float>
    {
        public FloatProperty(Identifier identifier, float value) : base(identifier, PropertyType.Float, value) { }
    }

    public class StringProperty : Property<string>
    {
        public StringProperty(Identifier identifier, string value) : base(identifier, PropertyType.String, value) { }
    }

    public class ReferenceProperty : Property<Reference>
    {
        public ReferenceProperty(Identifier identifier, Reference value) : base(identifier, PropertyType.Reference, value) { }
    }

    public class TypeProperty : Property<DataType>
    {
        public TypeProperty(Identifier identifier, DataType value) : base(identifier, PropertyType.Type, value) { }
    }
}