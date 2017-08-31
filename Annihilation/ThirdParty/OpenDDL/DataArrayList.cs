using System;
using System.Collections.Generic;

namespace OpenDDL
{
    public abstract class DataArrayList
    {
        public DataType DataType { get; }
        public int ArraySize { get; set; }

        protected DataArrayList(DataType dataType, int arraySize)
        {
            DataType = dataType;
            ArraySize = arraySize;
        }

        public static DataArrayList Create(DataType dataType, int arraySize)
        {
            switch (dataType)
            {
                case DataType.Bool:
                    return new BoolDataArrayList(arraySize);
                case DataType.Int8:
                    return new Int8DataArrayList(arraySize);
                case DataType.Int16:
                    return new Int16DataArrayList(arraySize);
                case DataType.Int32:
                    return new Int32DataArrayList(arraySize);
                case DataType.Int64:
                    return new Int64DataArrayList(arraySize);
                case DataType.UInt8:
                    return new UInt8DataArrayList(arraySize);
                case DataType.UInt16:
                    return new UInt16DataArrayList(arraySize);
                case DataType.UInt32:
                    return new UInt32DataArrayList(arraySize);
                case DataType.UInt64:
                    return new UInt64DataArrayList(arraySize);
                case DataType.Half:
                    return new HalfDataArrayList(arraySize);
                case DataType.Float:
                    return new FloatDataArrayList(arraySize);
                case DataType.Double:
                    return new DoubleDataArrayList(arraySize);
                case DataType.String:
                    return new StringDataArrayList(arraySize);
                case DataType.Reference:
                    return new ReferenceDataArrayList(arraySize);
                case DataType.Type:
                    return new TypeDataArrayList(arraySize);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
            }
        }
    }

    public class DataArrayList<T> : DataArrayList
    {
        public List<T[]> Values { get; } = new List<T[]>();

        protected DataArrayList(DataType dataType, int arraySize) : base(dataType, arraySize) { }
    }

    public class BoolDataArrayList : DataArrayList<bool>
    {
        public BoolDataArrayList(int arraySize) : base(DataType.Bool, arraySize) { }
    }

    public class Int8DataArrayList : DataArrayList<sbyte>
    {
        public Int8DataArrayList(int arraySize) : base(DataType.Int8, arraySize) { }
    }

    public class Int16DataArrayList : DataArrayList<short>
    {
        public Int16DataArrayList(int arraySize) : base(DataType.Int16, arraySize) { }
    }

    public class Int32DataArrayList : DataArrayList<int>
    {
        public Int32DataArrayList(int arraySize) : base(DataType.Int32, arraySize) { }
    }

    public class Int64DataArrayList : DataArrayList<long>
    {
        public Int64DataArrayList(int arraySize) : base(DataType.Int64, arraySize) { }
    }

    public class UInt8DataArrayList : DataArrayList<byte>
    {
        public UInt8DataArrayList(int arraySize) : base(DataType.UInt8, arraySize) { }
    }

    public class UInt16DataArrayList : DataArrayList<ushort>
    {
        public UInt16DataArrayList(int arraySize) : base(DataType.UInt16, arraySize) { }
    }

    public class UInt32DataArrayList : DataArrayList<uint>
    {
        public UInt32DataArrayList(int arraySize) : base(DataType.UInt32, arraySize) { }
    }

    public class UInt64DataArrayList : DataArrayList<ulong>
    {
        public UInt64DataArrayList(int arraySize) : base(DataType.UInt64, arraySize) { }
    }

    public class HalfDataArrayList : DataArrayList<short>
    {
        public HalfDataArrayList(int arraySize) : base(DataType.Half, arraySize) { }
    }

    public class FloatDataArrayList : DataArrayList<float>
    {
        public FloatDataArrayList(int arraySize) : base(DataType.Float, arraySize) { }
    }

    public class DoubleDataArrayList : DataArrayList<double>
    {
        public DoubleDataArrayList(int arraySize) : base(DataType.Double, arraySize) { }
    }

    public class StringDataArrayList : DataArrayList<string>
    {
        public StringDataArrayList(int arraySize) : base(DataType.String, arraySize) { }
    }

    public class ReferenceDataArrayList : DataArrayList<Reference>
    {
        public ReferenceDataArrayList(int arraySize) : base(DataType.Reference, arraySize) { }
    }

    public class TypeDataArrayList : DataArrayList<DataType>
    {
        public TypeDataArrayList(int arraySize) : base(DataType.Type, arraySize) { }
    }
}