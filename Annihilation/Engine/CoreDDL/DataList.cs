using System.Collections.Generic;

namespace ODDL
{
    public abstract class DataList
    {
        public DataType DataType { get; }

        protected DataList(DataType dataType)
        {
            DataType = dataType;
        }
    }

    public class DataList<T> : DataList
    {
        public List<T> Values { get; } = new List<T>();
        
        protected DataList(DataType dataType) : base(dataType) { }

        public void Add(T value)
        {
            Values.Add(value);
        }
    }

    public class BoolList : DataList<bool>
    {
        public BoolList() : base(DataType.Bool) { }
    }

    public class Int8List : DataList<sbyte>
    {
        public Int8List() : base(DataType.Int8) { }
    }

    public class Int16List : DataList<short>
    {
        public Int16List() : base(DataType.Int16) { }
    }

    public class Int32List : DataList<int>
    {
        public Int32List() : base(DataType.Int32) { }
    }

    public class Int64List : DataList<long>
    {
        public Int64List() : base(DataType.Int64) { }
    }

    public class UInt8List : DataList<byte>
    {
        public UInt8List() : base(DataType.UInt8) { }
    }

    public class UInt16List : DataList<ushort>
    {
        public UInt16List() : base(DataType.UInt16) { }
    }

    public class UInt32List : DataList<uint>
    {
        public UInt32List() : base(DataType.UInt32) { }
    }

    public class UInt64List : DataList<ulong>
    {
        public UInt64List() : base(DataType.UInt64) { }
    }

    public class HalfList : DataList<short>
    {
        public HalfList() : base(DataType.Half) { }
    }

    public class FloatList : DataList<float>
    {
        public FloatList() : base(DataType.Float) { }
    }

    public class DoubleList : DataList<double>
    {
        public DoubleList() : base(DataType.Double) { }
    }

    public class StringList : DataList<string>
    {
        public StringList() : base(DataType.String) { }
    }

    public class ReferenceList : DataList<Reference>
    {
        public ReferenceList() : base(DataType.Reference) { }
    }

    public class TypeList : DataList<DataType>
    {
        public TypeList() : base(DataType.Type) { }
    }
}