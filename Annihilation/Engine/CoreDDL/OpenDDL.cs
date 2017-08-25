using System;
using System.Collections.Generic;

namespace ODDL
{
    public static class OpenDDL
    {
        private const int DataMaxPrimitiveArraySize = 256;

        public enum StructureType
        {
            Root,
            Primitive
        }
        
        public enum DataType
        {
            Bool,
            Int8,
            Int16,
            Int32,
            Int64,
            UnsignedInt8,
            UnsignedInt16,
            UnsignedInt32,
            UnsignedInt64,
            Half,
            Float,
            Double,
            String,
            Ref,
            Type,
        }

        public enum DataResult
        {
            Okay,
            SyntaxError,
            IdentifierEmpty,
            IdentifierIllegalChar,
            StringInvalid,
            StringIllegalChar,
            StringIllegalEscape,
            StringEndOfFile,
            CharIllegalChar,
            CharIllegalEscape,
            CharEndOfFile,
            BoolInvalid,
            TypeInvalid,
            IntegerOverflow,
            FloatOverflow,
            FloatInvalid,
            ReferenceInvalid,
            StructUndefined,
            StructNameExists,
            PropertySyntaxError,
            PropertyUndefined,
            PropertyInvalidType,
            PrimitiveSyntaxError,
            PrimitiveIllegalArraySize,
            PrimitiveInvalidFormat,
            PrimitiveArrayUnderSize,
            PrimitiveArrayOverSize,
            InvalidStructure,
            MissingSubstructure,
            ExtraneousSubstructure,
            InvalidDataFormat,
            BrokenRef
        }
    }
}