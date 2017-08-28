namespace OpenDDL
{
    public enum DataType
    {
        Invalid,
        Bool,
        Int8,
        Int16,
        Int32,
        Int64,
        UInt8,
        UInt16,
        UInt32,
        UInt64,
        Half,
        Float,
        Double,
        String,
        Reference,
        Type,
    }

    public class Data
    {
        public DataType Type { get; }

        public Data(DataType type)
        {
            Type = type;
        }
    }
}