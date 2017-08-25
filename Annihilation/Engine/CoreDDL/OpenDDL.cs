using System;
using System.Collections.Generic;

namespace OpenDDL
{
    public static class OpenDDL
    {
        private const int DataMaxPrimitiveArraySize = 256;

        public static class StructureType
        {
            private const int Root = 0;
            private const string Primitive = "PRIM";
        }

        public static class DataType
        {
            private const string Bool = "BOOL";
            private const string Int8 = "INT8";
            private const string Int16 = "IN16";
            private const string Int32 = "IN32";
            private const string Int64 = "IN64";
            private const string UnsignedInt8 = "UIN8";
            private const string UnsignedInt16 = "UI16";
            private const string UnsignedInt32 = "UI32";
            private const string UnsignedInt64 = "UI64";
            private const string Half = "HALF";
            private const string Float = "FLOT";
            private const string Double = "DOUB";
            private const string String = "STRG";
            private const string Ref = "RFNC";
            private const string Type = "TYPE";
        }

        public static class DataResult
        {
            private const int Okay = 0;
            private const string SyntaxError = "SYNT";
            private const string IdentifierEmpty = "IDEM";
            private const string IdentifierIllegalChar = "IDIC";
            private const string StringInvalid = "STIV";
            private const string StringIllegalChar = "STIC";
            private const string StringIllegalEscape = "STIE";
            private const string StringEndOfFile = "STEF";
            private const string CharIllegalChar = "CHIC";
            private const string CharIllegalEscape = "CHIE";
            private const string CharEndOfFile = "CHEF";
            private const string BoolInvalid = "BLIV";
            private const string TypeInvalid = "TYIV";
            private const string IntegerOverflow = "INOV";
            private const string FloatOverflow = "FLOB";
            private const string FloatInvalid = "FLIV";
            private const string ReferenceInvalid = "RFIV";
            private const string StructUndefined = "STUD";
            private const string StructNameExists = "STNE";
            private const string PropertySyntaxError = "PPSE";
            private const string PropertyUndefined = "STUD";
            private const string PropertyInvalidType = "PPIT";
            private const string PrimitiveSyntaxError = "PMSE";
            private const string PrimitiveIllegalArraySize = "PMAS";
            private const string PrimitiveInvalidFormat = "PMIF";
            private const string PrimitiveArrayUnderSize = "PMUS";
            private const string PrimitiveArrayOverSize = "PMOS";
            private const string InvalidStructure = "IVST";
            private const string MissingSubstructure = "MSSB";
            private const string ExtraneousSubstructure = "EXSB";
            private const string InvalidDataFormat = "IVDF";
            private const string BrokenRef = "BREF";
        }
    }
}