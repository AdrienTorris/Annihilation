namespace ODDL
{
    public enum NameType
    {
        Global,
        Local
    }

    public class Name
    {
        public NameType Type { get; }
        public Identifier Identifier { get; }

        public Name(NameType type, Identifier identifier)
        {
            Type = type;
            Identifier = identifier;
        }

        public Name(NameType type, string identifier) : this(type, new Identifier(identifier))
        { }
    }
}