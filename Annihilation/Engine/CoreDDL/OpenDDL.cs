using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ODDL
{
    public enum Result
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

    public unsafe static class OpenDDL
    {
        private const int DataMaxPrimitiveArraySize = 256;

        private static readonly sbyte[] HexadecimalCharValues =
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9, -1, -1, -1, -1, -1, -1,
            -1, 10, 11, 12, 13, 14, 15, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
            -1, 10, 11, 12, 13, 14, 15
        };

        private static readonly byte[] IdentifierCharState =
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, // $, 0-9
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, // A-Z, _
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 2, // a-z
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, // 128 - 159
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, // 160 - 191
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, // 192 - 223
            2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2  // 224 - 255
        };

        public static Result ParseFile(string path)
        {
            long byteCount = new FileInfo(path).Length;
            char[] chars = new char[byteCount];

            int readCount = 0;
            using (StreamReader stream = new StreamReader(path, Encoding.UTF8))
            {
                readCount = stream.Read(chars, 0, (int)byteCount);
            }

            fixed (char* start = chars)
            {
                RootStructure rootStructure;
                Structure errorStructure = null;
                int errorLine = 0;

                char* text = start;
                text += GetWhitespaceLength(text);

                Result result = ParseStructures(text, ref rootStructure);
                if ((result == Result.Okay) && (text[0] != 0))
                {
                    result = Result.SyntaxError;
                }

                if (result == Result.Okay)
                {
                    result = ProcessData();
                    if ((result != Result.Okay) && (errorStructure != null))
                    {
                        text = errorStructure.TextLocation;
                    }
                }

                if (result != Result.Okay)
                {
                    rootStructure.PurgeSubtree();

                    int line = 1;
                    while (text != start)
                    {
                        if ((--text)[0] == '\n')
                        {
                            line++;
                        }
                    }

                    errorLine = line;
                }

                return result;
            }
        }

        public static int GetWhitespaceLength(char* text)
        {
            char* chars = text;
            while (true)
            {
                char c = chars[0];
                if (c == 0)
                {
                    break;
                }

                if (c >= 33U)
                {
                    if (c != '/')
                    {
                        break;
                    }

                    c = chars[1];
                    if (c == '/')
                    {
                        chars += 2;
                        while (true)
                        {
                            c = chars[0];
                            if (c == 0)
                            {
                                goto end;
                            }

                            chars++;

                            if (c == 10)
                            {
                                break;
                            }
                        }

                        continue;
                    }
                    else if (c == '*')
                    {
                        chars += 2;
                        while (true)
                        {
                            c = chars[0];
                            if (c == 0)
                            {
                                goto end;
                            }

                            chars++;

                            if ((c == '*') && (chars[0] == '/'))
                            {
                                chars++;
                                break;
                            }
                        }

                        continue;
                    }

                    break;
                }

                chars++;
            }

            end:
            return (int)(chars - text);
        }

        public static Result ReadDataType(char* text, out int textLength, out DataType dataType)
        {
            char* chars = text;

            char c = chars[0];
            if (c == 'i')
            {
                int length = (chars[1] == 'n' && chars[2] == 't') ? 3 : 1;

                if ((chars[length] == '8') && (IdentifierCharState[chars[length + 1]] == 0))
                {
                    dataType = DataType.Int8;
                    textLength = length + 1;
                    return Result.Okay;
                }

                if ((chars[length] == '1') && (chars[length + 1] == '6') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Int16;
                    textLength = length + 2;
                    return Result.Okay;
                }

                if ((chars[length] == '3') && (chars[length + 1] == '2') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Int32;
                    textLength = length + 2;
                    return Result.Okay;
                }

                if ((chars[length] == '6') && (chars[length + 1] == '4') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Int64;
                    textLength = length + 2;
                    return Result.Okay;
                }
            }
            else if (c == 'u')
            {
                int length = text.Compare("unsigned_int", 12) ? 12 : 1;

                if ((chars[length] == '8') && (IdentifierCharState[chars[length + 1]] == 0))
                {
                    dataType = DataType.UInt8;
                    textLength = length + 1;
                    return Result.Okay;
                }

                if ((chars[length] == '1') && (chars[length + 1] == '6') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.UInt16;
                    textLength = length + 2;
                    return Result.Okay;
                }

                if ((chars[length] == '3') && (chars[length + 1] == '2') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.UInt32;
                    textLength = length + 2;
                    return Result.Okay;
                }

                if ((chars[length] == '6') && (chars[length + 1] == '4') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.UInt64;
                    textLength = length + 2;
                    return Result.Okay;
                }
            }
            else if (c == 'f')
            {
                int length = text.Compare("float", 5) ? 5 : 1;

                if (IdentifierCharState[chars[length + 1]] == 0)
                {
                    dataType = DataType.Float;
                    textLength = length;
                    return Result.Okay;
                }

                if ((chars[length] == '1') && (chars[length + 1] == '6') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Half;
                    textLength = length + 2;
                    return Result.Okay;
                }

                if ((chars[length] == '3') && (chars[length + 1] == '2') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Float;
                    textLength = length + 2;
                    return Result.Okay;
                }

                if ((chars[length] == '6') && (chars[length + 1] == '4') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Double;
                    textLength = length + 2;
                    return Result.Okay;
                }
            }
            else if (c == 'b')
            {
                int length = text.Compare("bool", 4) ? 4 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Bool;
                    textLength = length;
                    return Result.Okay;
                }
            }
            else if (c == 'h')
            {
                int length = text.Compare("half", 4) ? 4 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Half;
                    textLength = length;
                    return Result.Okay;
                }
            }
            else if (c == 'd')
            {
                int length = text.Compare("double", 6) ? 6 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Double;
                    textLength = length;
                    return Result.Okay;
                }
            }
            else if (c == 's')
            {
                int length = text.Compare("string", 6) ? 6 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.String;
                    textLength = length;
                    return Result.Okay;
                }
            }
            else if (c == 'r')
            {
                int length = (chars[1] == 'e' && chars[2] == 'f') ? 3 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Reference;
                    textLength = length;
                    return Result.Okay;
                }
            }
            else if (c == 't')
            {
                int length = text.Compare("type", 4) ? 4 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Type;
                    textLength = length;
                    return Result.Okay;
                }
            }

            textLength = 0;
            dataType = DataType.Invalid;
            return Result.TypeInvalid;
        }

        public static Result ReadIdentifier(char* text, out int textLength, StringBuilder identifier = null)
        {
            char* chars = text;

            textLength = 0;
            int count = 0;

            char c = chars[0];
            byte state = IdentifierCharState[c];

            if (state == 1)
            {
                if (c < 'A')
                {
                    return Result.IdentifierIllegalChar;
                }

                identifier.Append(c);
                count++;
                while (true)
                {
                    c = chars[count];
                    state = IdentifierCharState[c];

                    if (state == 1)
                    {
                        identifier.Append(c);

                        count++;
                        continue;
                    }
                    else if (state == 2)
                    {
                        return Result.IdentifierIllegalChar;
                    }

                    break;
                }

                identifier.Append((char)0);

                textLength = count;
                return Result.Okay;
            }
            else if (state == 2)
            {
                return Result.IdentifierIllegalChar;
            }

            return Result.IdentifierEmpty;
        }

        public static int ReadEscapeChar(char* text, out char escapeChar)
        {
            char* chars = text;

            char c = chars[0];

            if ((c == '\"') || (c == '\'') || (c == '?') || (c == '\\'))
            {
                escapeChar = c;
                return 1;
            }
            else if (c == 'a')
            {
                escapeChar = '\a';
                return 1;
            }
            else if (c == 'b')
            {
                escapeChar = '\b';
                return 1;
            }
            else if (c == 'f')
            {
                escapeChar = '\f';
                return 1;
            }
            else if (c == 'n')
            {
                escapeChar = '\n';
                return 1;
            }
            else if (c == 'r')
            {
                escapeChar = '\r';
                return 1;
            }
            else if (c == 't')
            {
                escapeChar = '\t';
                return 1;
            }
            else if (c == 'v')
            {
                escapeChar = '\v';
                return 1;
            }
            else if (c == 'x')
            {
                c = (char)(chars[1] - '0');
                if (c < 55U)
                {
                    int x = HexadecimalCharValues[c];
                    if (x >= 0)
                    {
                        c = (char)(chars[2] - '0');
                        if (c < 55U)
                        {
                            int y = HexadecimalCharValues[c];
                            if (y >= 0)
                            {
                                escapeChar = (char)((x << 4) | y);
                                return 3;
                            }
                        }
                    }
                }
            }

            escapeChar = '0';
            return 0;
        }

        public static int ReadStringEscapeChar(char* text, out int stringLength, char* escapeString)
        {
            stringLength = 0;

            char* chars = text;
            char c = chars[0];

            if (c == 'u')
            {
                uint code = 0;

                for (int a = 1; a <= 4; a++)
                {
                    c = (char)(chars[a] - '0');
                    if (c >= 55U)
                    {
                        return 0;
                    }

                    int x = HexadecimalCharValues[c];
                    if (x < 0)
                    {
                        return 0;
                    }

                    code = (uint)((int)(code << 4) | x);
                }

                if (code != 0)
                {
                    if (escapeString != null)
                    {
                        stringLength = Text.WriteGlyphCodeUTF8(escapeString, code);
                    }
                    else
                    {
                        stringLength = 1 + ((code >= 0x000080) ? 1 : 0) + ((code >= 0x000800) ? 1 : 0);
                    }

                    return 5;
                }
            }
            if (c == 'U')
            {
                uint code = 0;

                for (int a = 1; a <= 6; a++)
                {
                    c = (char)(chars[a] - '0');
                    if (c >= 55U)
                    {
                        return 0;
                    }

                    int x = HexadecimalCharValues[c];
                    if (x < 0)
                    {
                        return 0;
                    }

                    code = (uint)((int)(code << 4) | x);
                }

                if ((code != 0) && (code <= 0x10FFFF))
                {
                    if (escapeString != null)
                    {
                        stringLength = Text.WriteGlyphCodeUTF8(escapeString, code);
                    }
                    else
                    {
                        stringLength = 1 + ((code >= 0x000080) ? 1 : 0) + ((code >= 0x000800) ? 1 : 0) + ((code >= 0x010000) ? 1 : 0);
                    }

                    return 7;
                }
            }
            else
            {
                char value;

                int textLength = ReadEscapeChar(text, out value);
                if (textLength != 0)
                {
                    if (escapeString != null)
                    {
                        *escapeString = value;
                    }

                    stringLength = 1;
                    return textLength;
                }
            }

            return 0;
        }

        public static Result ReadStringLiteral(char* text, out int textLength, out int stringLength, char* str)
        {
            textLength = 0;
            stringLength = 0;

            char* chars = text;
            int count = 0;

            while (true)
            {
                char c = chars[0];
                if ((c == 0) || (c == '\"'))
                {
                    break;
                }

                if ((c < 32U) || (c == 127U))
                {
                    return Result.StringIllegalChar;
                }

                if (c != '\\')
                {
                    int length = Text.ValidateGlyphCodeUTF8(chars);
                    if (length == 0)
                    {
                        return Result.StringIllegalChar;
                    }

                    if (str != null)
                    {
                        for (int a = 0; a < length; a++)
                        {
                            str[a] = chars[a];
                        }

                        str += length;
                    }

                    chars += length;
                    count += length;
                }
                else
                {
                    int textLen = ReadStringEscapeChar(++chars, out int stringLen, str);
                    if (textLen == 0)
                    {
                        return Result.StringIllegalEscape;
                    }

                    if (str != null)
                    {
                        str += stringLen;
                    }

                    chars += textLen;
                    count += stringLen;
                }
            }

            textLength = (int)(chars - text);
            stringLength = count;
            return Result.Okay;
        }

        public static Result ReadBoolLiteral(char* text, out int textLength, out bool value)
        {
            char* chars = text;

            char c = chars[0];
            if (c == 'f')
            {
                if ((chars[1] == 'a') && (chars[2] == 'l') && (chars[3] == 's') && (chars[4] == 'e') && (IdentifierCharState[chars[5]] == 0))
                {
                    value = false;
                    textLength = 5;
                    return Result.Okay;
                }
            }
            else if (c == 't')
            {
                if ((chars[1] == 'r') && (chars[2] == 'u') && (chars[3] == 'e') && (IdentifierCharState[chars[4]] == 0))
                {
                    value = true;
                    textLength = 4;
                    return Result.Okay;
                }
            }

            textLength = 0;
            value = false;
            return Result.BoolInvalid;
        }

        public static Result ReadDecimalLiteral(char* text, out int textLength, out ulong value)
        {
            value = 0;
            textLength = 0;

            char* chars = text;

            ulong v = 0;
            bool separator = false;
            while (true)
            {
                char x = (char)(chars[0] - '0');
                if (x < 10U)
                {
                    if (v >= 0x199999999999999AUL)
                    {
                        return Result.IntegerOverflow;
                    }

                    ulong w = v;
                    v = v * 10 + x;

                    if ((w >= 9U) && (v < 9U))
                    {
                        return Result.IntegerOverflow;
                    }

                    separator = true;
                }
                else
                {
                    if ((x != 47) || (!separator))
                    {
                        break;
                    }

                    separator = false;
                }

                chars++;
            }

            if (!separator)
            {
                return Result.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return Result.Okay;
        }

        public static Result ReadHexadecimalLiteral(char* text, out int textLength, out ulong value)
        {
            value = 0;
            textLength = 0;

            char* chars = text;

            ulong v = 0;
            bool separator = false;
            while (true)
            {
                char c = (char)(chars[0] - '0');
                if (c >= 55U)
                {
                    break;
                }

                int x = HexadecimalCharValues[c];
                if (x >= 0)
                {
                    if ((v >> 60) != 0)
                    {
                        return Result.IntegerOverflow;
                    }

                    v = (ulong)((int)(v << 4) | x);
                    separator = true;
                }
                else
                {
                    if ((c != 47) || (!separator))
                    {
                        break;
                    }

                    separator = false;
                }

                chars++;
            }

            if (!separator)
            {
                return Result.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return Result.Okay;
        }

        public static Result ReadOctalLiteral(char* text, out int textLength, out ulong value)
        {
            value = 0;
            textLength = 0;

            char* chars = text;

            ulong v = 0;
            bool separator = false;
            while (true)
            {
                char x = (char)(chars[0] - '0');
                if (x < 8U)
                {
                    if (v >= 0x2000000000000000UL)
                    {
                        return Result.IntegerOverflow;
                    }

                    ulong w = v;
                    v = v * 8 + x;

                    if ((w >= 7U) && (v < 7U))
                    {
                        return Result.IntegerOverflow;
                    }

                    separator = true;
                }
                else
                {
                    if ((x != 47) || (!separator))
                    {
                        break;
                    }

                    separator = false;
                }

                chars++;
            }

            if (!separator)
            {
                return Result.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return Result.Okay;
        }

        public static Result ReadBinaryLiteral(char* text, out int textLength, out ulong value)
        {
            value = 0;
            textLength = 0;

            char* chars = text;

            ulong v = 0;
            bool separator = false;
            while (true)
            {
                char x = (char)(chars[0] - '0');
                if (x < 2U)
                {
                    if ((v >> 63) != 0)
                    {
                        return Result.IntegerOverflow;
                    }

                    v = (v << 1) | x;
                    separator = true;
                }
                else
                {
                    if ((x != 47) || (!separator))
                    {
                        break;
                    }

                    separator = false;
                }

                chars++;
            }

            if (!separator)
            {
                return Result.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return Result.Okay;
        }

        public static Result ReadCharLiteral(char* text, out int textLength, out ulong value)
        {
            value = 0;
            textLength = 0;

            char* chars = text;

            ulong v = 0;
            while (true)
            {
                char c = (char)(chars[0] - '0');
                if ((c == 0) || (c == '\''))
                {
                    break;
                }

                if ((c < 32U) || (c >= 127U))
                {
                    return Result.CharIllegalChar;
                }

                if (c != '\\')
                {
                    if ((v >> 56) != 0)
                    {
                        return Result.IntegerOverflow;
                    }

                    v = (v << 8) | c;
                    chars++;
                }
                else
                {
                    int length = ReadEscapeChar(++chars, out char x);
                    if (length == 0)
                    {
                        return Result.CharIllegalEscape;
                    }

                    if ((v >> 56) != 0)
                    {
                        return Result.IntegerOverflow;
                    }

                    v = (v << 8) | x;
                    chars += length;
                }
            }

            value = v;
            textLength = (int)(chars - text);
            return Result.Okay;
        }

        public static Result ReadIntegerLiteral(char* text, out int textLength, out ulong value)
        {
            textLength = 0;
            value = 0;

            char* chars = text;

            char c = text[0];
            if (c == '0')
            {
                c = text[1];

                if ((c == 'x') || (c == 'X'))
                {
                    return ReadHexadecimalLiteral(text, out textLength, out value);
                }

                if ((c == 'o') || (c == 'O'))
                {
                    return ReadOctalLiteral(text, out textLength, out value);
                }

                if ((c == 'b') || (c == 'B'))
                {
                    return ReadBinaryLiteral(text, out textLength, out value);
                }
            }
            else if (c == '\'')
            {
                Result result = ReadCharLiteral(chars + 1, out int length, out value);
                if (result == Result.Okay)
                {
                    if (text[length + 1] != '\'')
                    {
                        return Result.CharEndOfFile;
                    }

                    textLength = length + 2;
                }

                return result;
            }

            return ReadDecimalLiteral(text, out textLength, out value);
        }

        public static Result ReadFloatLiteral(char* text, out int textLength, out float value)
        {
            textLength = 0;
            value = 0f;

            char* chars = text;

            char c = chars[0];
            if (c == '0')
            {
                c = chars[1];

                if ((c == 'x') || (c == 'X'))
                {
                    Result result = ReadHexadecimalLiteral(text, out textLength, out ulong val);
                    if (result == Result.Okay)
                    {
                        if (val > ((1UL << (sizeof(float) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return Result.FloatOverflow;
                        }

                        value = val;
                    }

                    return result;
                }

                if ((c == 'o') || (c == 'O'))
                {
                    Result result = ReadOctalLiteral(text, out textLength, out ulong val);
                    if (result == Result.Okay)
                    {
                        if (val > ((1UL << (sizeof(float) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return Result.FloatOverflow;
                        }

                        value = val;
                    }

                    return (result);
                }

                if ((c == 'b') || (c == 'B'))
                {
                    Result result = ReadBinaryLiteral(text, out textLength, out ulong val);
                    if (result == Result.Okay)
                    {
                        if (val > ((1UL << (sizeof(float) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return Result.FloatOverflow;
                        }

                        value = val;
                    }

                    return (result);
                }
            }

            double v = 0.0F;
            bool digitFlag = false;
            bool wholeFlag = false;
            for (; ; chars++)
            {
                uint x = (uint)(chars[0] - '0');
                if (x < 10U)
                {
                    v = v * 10.0F + x;
                    digitFlag = true;
                    wholeFlag = true;
                }
                else if (x == 47)
                {
                    if (!digitFlag)
                    {
                        return Result.FloatInvalid;
                    }

                    digitFlag = false;
                }
                else
                {
                    break;
                }
            }

            if (wholeFlag & !digitFlag)
            {
                return Result.FloatInvalid;
            }

            bool fractionFlag = false;

            c = chars[0];
            if (c == '.')
            {
                digitFlag = false;
                double decim = 10.0F;
                for (++chars; ; chars++)
                {
                    uint x = (uint)(chars[0] - '0');
                    if (x < 10U)
                    {
                        v += x / decim;
                        digitFlag = true;
                        fractionFlag = true;
                        decim *= 10.0F;
                    }
                    else if (x == 47)
                    {
                        if (!digitFlag)
                        {
                            return Result.FloatInvalid;
                        }

                        digitFlag = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (fractionFlag & !digitFlag)
                {
                    return Result.FloatInvalid;
                }

                c = chars[0];
            }

            if (!(wholeFlag | fractionFlag))
            {
                return Result.FloatInvalid;
            }

            if ((c == 'e') || (c == 'E'))
            {
                bool negative = false;

                c = (++chars)[0];
                if (c == '-')
                {
                    negative = true;
                    chars++;
                }
                else if (c == '+')
                {
                    chars++;
                }

                int exponent = 0;
                digitFlag = false;
                for (; ; chars++)
                {
                    uint x = (uint)(chars[0] - '0');
                    if (x < 10U)
                    {
                        exponent = (int)Math.Min(exponent * 10 + x, 65535);
                        digitFlag = true;
                    }
                    else if (x == 47)
                    {
                        if (!digitFlag)
                        {
                            return Result.FloatInvalid;
                        }

                        digitFlag = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!digitFlag)
                {
                    return Result.FloatInvalid;
                }

                if (exponent != 0)
                {
                    double f = 1.0F;
                    double p = 10.0F;
                    do
                    {
                        if ((exponent & 1) != 0)
                        {
                            f *= p;
                        }

                        p *= p;
                        exponent >>= 1;
                    } while (exponent != 0);

                    if (negative)
                    {
                        f = 1.0F / f;
                    }

                    v *= f;
                }
            }

            value = (float)v;
            textLength = (int)(chars - text);
            return Result.Okay;
        }

        public static Result ReadDoubleLiteral(char* text, out int textLength, out double value)
        {
            textLength = 0;
            value = 0f;

            char* chars = text;

            char c = chars[0];
            if (c == '0')
            {
                c = chars[1];

                if ((c == 'x') || (c == 'X'))
                {
                    Result result = ReadHexadecimalLiteral(text, out textLength, out ulong val);
                    if (result == Result.Okay)
                    {
                        if (val > ((1UL << (sizeof(double) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return Result.FloatOverflow;
                        }

                        value = val;
                    }

                    return result;
                }

                if ((c == 'o') || (c == 'O'))
                {
                    Result result = ReadOctalLiteral(text, out textLength, out ulong val);
                    if (result == Result.Okay)
                    {
                        if (val > ((1UL << (sizeof(double) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return Result.FloatOverflow;
                        }

                        value = val;
                    }

                    return (result);
                }

                if ((c == 'b') || (c == 'B'))
                {
                    Result result = ReadBinaryLiteral(text, out textLength, out ulong val);
                    if (result == Result.Okay)
                    {
                        if (val > ((1UL << (sizeof(double) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return Result.FloatOverflow;
                        }

                        value = val;
                    }

                    return (result);
                }
            }

            double v = 0.0F;
            bool digitFlag = false;
            bool wholeFlag = false;
            for (; ; chars++)
            {
                uint x = (uint)(chars[0] - '0');
                if (x < 10U)
                {
                    v = v * 10.0F + x;
                    digitFlag = true;
                    wholeFlag = true;
                }
                else if (x == 47)
                {
                    if (!digitFlag)
                    {
                        return Result.FloatInvalid;
                    }

                    digitFlag = false;
                }
                else
                {
                    break;
                }
            }

            if (wholeFlag & !digitFlag)
            {
                return Result.FloatInvalid;
            }

            bool fractionFlag = false;

            c = chars[0];
            if (c == '.')
            {
                digitFlag = false;
                double decim = 10.0F;
                for (++chars; ; chars++)
                {
                    uint x = (uint)(chars[0] - '0');
                    if (x < 10U)
                    {
                        v += x / decim;
                        digitFlag = true;
                        fractionFlag = true;
                        decim *= 10.0F;
                    }
                    else if (x == 47)
                    {
                        if (!digitFlag)
                        {
                            return Result.FloatInvalid;
                        }

                        digitFlag = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (fractionFlag & !digitFlag)
                {
                    return Result.FloatInvalid;
                }

                c = chars[0];
            }

            if (!(wholeFlag | fractionFlag))
            {
                return Result.FloatInvalid;
            }

            if ((c == 'e') || (c == 'E'))
            {
                bool negative = false;

                c = (++chars)[0];
                if (c == '-')
                {
                    negative = true;
                    chars++;
                }
                else if (c == '+')
                {
                    chars++;
                }

                int exponent = 0;
                digitFlag = false;
                for (; ; chars++)
                {
                    uint x = (uint)(chars[0] - '0');
                    if (x < 10U)
                    {
                        exponent = (int)Math.Min(exponent * 10 + x, 65535);
                        digitFlag = true;
                    }
                    else if (x == 47)
                    {
                        if (!digitFlag)
                        {
                            return Result.FloatInvalid;
                        }

                        digitFlag = false;
                    }
                    else
                    {
                        break;
                    }
                }

                if (!digitFlag)
                {
                    return Result.FloatInvalid;
                }

                if (exponent != 0)
                {
                    double f = 1.0F;
                    double p = 10.0F;
                    do
                    {
                        if ((exponent & 1) != 0)
                        {
                            f *= p;
                        }

                        p *= p;
                        exponent >>= 1;
                    } while (exponent != 0);

                    if (negative)
                    {
                        f = 1.0F / f;
                    }

                    v *= f;
                }
            }

            value = v;
            textLength = (int)(chars - text);
            return Result.Okay;
        }

        public static bool ParseSign(char* text)
        {
            char c = text[0];

            if (c == '-')
            {
                text++;
                text += GetWhitespaceLength(text);
                return true;
            }

            if (c == '+')
            {
                text++;
                text += GetWhitespaceLength(text);
            }

            return false;
        }

        public static Result ParseBool(char* text, out bool value)
        {
            Result result = ReadBoolLiteral(text, out int length, out value);
            if (result != Result.Okay)
            {
                return result;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseInt8(char* text, out sbyte value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7F)
                {
                    return Result.IntegerOverflow;
                }

                value = (sbyte)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x80)
                {
                    return Result.IntegerOverflow;
                }

                value = (sbyte)-(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseInt16(char* text, out short value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7FFF)
                {
                    return Result.IntegerOverflow;
                }

                value = (short)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x8000)
                {
                    return Result.IntegerOverflow;
                }

                value = (short)-(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseInt32(char* text, out int value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7FFFFFFF)
                {
                    return Result.IntegerOverflow;
                }

                value = (int)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x80000000)
                {
                    return Result.IntegerOverflow;
                }

                value = (int)-(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseInt64(char* text, out long value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7FFFFFFFFFFFFFFF)
                {
                    return Result.IntegerOverflow;
                }

                value = (long)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x8000000000000000)
                {
                    return Result.IntegerOverflow;
                }

                value = -(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseUInt8(char* text, out byte value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (negative)
            {
                unsignedValue = (ulong)-(long)unsignedValue;
            }

            value = (byte)unsignedValue;

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseUInt16(char* text, out ushort value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (negative)
            {
                unsignedValue = (ulong)-(long)unsignedValue;
            }

            value = (ushort)unsignedValue;

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseUInt32(char* text, out uint value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (negative)
            {
                unsignedValue = (ulong)-(long)unsignedValue;
            }

            value = (uint)unsignedValue;

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseUInt64(char* text, out ulong value)
        {
            value = 0;

            bool negative = ParseSign(text);

            Result result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (negative)
            {
                unsignedValue = (ulong)-(long)unsignedValue;
            }

            value = unsignedValue;

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseFloat(char* text, out float value)
        {
            value = 0f;

            bool negative = ParseSign(text);

            Result result = ReadFloatLiteral(text, out int length, out float floatValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (negative)
            {
                floatValue = -floatValue;
            }

            value = floatValue;

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseDouble(char* text, out double value)
        {
            value = 0f;

            bool negative = ParseSign(text);

            Result result = ReadDoubleLiteral(text, out int length, out double doubleValue);
            if (result != Result.Okay)
            {
                return result;
            }

            if (negative)
            {
                doubleValue = -doubleValue;
            }

            value = doubleValue;

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseString(char* text, out string value)
        {
            value = null;

            if (text[0] != '"')
            {
                return Result.StringInvalid;
            }

            int accumLength = 0;
            while (true)
            {
                text++;

                Result result = ReadStringLiteral(text, out int textLength, out int stringLength, null);
                if (result != Result.Okay)
                {
                    return result;
                }

                ReadStringLiteral(text, out textLength, out stringLength, &value[accumLength]);
                accumLength += stringLength;

                text += textLength;
                if (text[0] != '"')
                {
                    return Result.StringInvalid;
                }

                text++;
                text += GetWhitespaceLength(text);

                if (text[0] != '"')
                {
                    break;
                }
            }

            return Result.Okay;
        }

        public static Result ParseReference(char* text, out Reference value)
        {

        }

        public static Result ParseType(char* text, out DataType value)
        {
            Result result = ReadDataType(text, out int length, out value);
            if (result != Result.Okay)
            {
                return result;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return Result.Okay;
        }

        public static Result ParseStructures(char* text, out Structure root)
        {
            root = null;

            while (true)
            {
                Result result = ReadIdentifier(text, out int length);
                if (result != Result.Okay)
                {
                    return result;
                }

                StringBuilder identifier = new StringBuilder();
                ReadIdentifier(text, out length, identifier);

                bool primitive = false;

                Structure structure = CreatePrimitive(identifier);
                if (structure != null)
                {
                    primitive = true;
                }
                else
                {
                    structure = CreateStructure(identifier);
                    if (structure == null)
                    {
                        return Result.StructUndefined;
                    }
                }

                identifier.Clear();

                AutoDelete<Structure> structurePtr(structure);
                structure->textLocation = text;

                text += length;
                text += GetWhitespaceLength(text);

                if ((primitive) && (text[0] == '['))
                {
                    ulong value;

                    text++;
                    text += GetWhitespaceLength(text);

                    if (ParseSign(text))
                    {
                        return Result.PrimitiveIllegalArraySize;
                    }

                    result = ReadUnsignedLiteral(text, &length, &value);
                    if (result != Result.Okay)
                    {
                        return (result);
                    }

                    if ((value == 0) || (value > DataMaxPrimitiveArraySize))
                    {
                        return Result.PrimitiveIllegalArraySize;
                    }

                    text += length;
                    text += GetWhitespaceLength(text);

                    if (text[0] != ']')
                    {
                        return Result.PrimitiveSyntaxError;
                    }

                    text++;
                    text += GetWhitespaceLength(text);

                    static_cast<PrimitiveStructure*>(structure)->arraySize = (uint)value;
                }

                if (!root.ValidateSubstructure(this, structure))
                {
                    return Result.InvalidStructure;
                }

                char c = text[0];
                if ((uint)(c - '$') < 2U)
                {
                    text++;

                    result = ReadIdentifier(text, out length);
                    if (result != Result.Okay)
                    {
                        return (result);
                    }

                    ReadIdentifier(text, &length, structure->structureName.SetLength(length));

                    bool global = (c == '$');
                    structure->globalNameFlag = global;

                    Map<Structure>* map = (global) ? &structureMap : &root->structureMap;
                    if (!map->Insert(structure))
                    {
                        return (kDataStructNameExists);
                    }

                    text += length;
                    text += GetWhitespaceLength(text);
                }

                if ((!primitive) && (text[0] == '('))
                {
                    text++;
                    text += GetWhitespaceLength(text);

                    if (text[0] != ')')
                    {
                        result = ParseProperties(text, structure);
                        if (result != Result.Okay)
                        {
                            return (result);
                        }

                        if (text[0] != ')')
                        {
                            return (kDataPropertySyntaxError);
                        }
                    }

                    text++;
                    text += GetWhitespaceLength(text);
                }

                if (text[0] != '{')
                {
                    return (kDataSyntaxError);
                }

                text++;
                text += GetWhitespaceLength(text);

                if (text[0] != '}')
                {
                    if (primitive)
                    {
                        result = static_cast<PrimitiveStructure*>(structure)->ParseData(text);
                        if (result != Result.Okay)
                        {
                            return (result);
                        }
                    }
                    else
                    {
                        result = ParseStructures(text, structure);
                        if (result != Result.Okay)
                        {
                            return (result);
                        }
                    }
                }

                if (text[0] != '}')
                {
                    return (kDataSyntaxError);
                }

                text++;
                text += GetWhitespaceLength(text);

                root->AppendSubnode(structure);
                structurePtr = nullptr;

                c = text[0];
                if ((c == 0) || (c == '}'))
                {
                    break;
                }
            }

            return Result.Okay;
        }
    }
}