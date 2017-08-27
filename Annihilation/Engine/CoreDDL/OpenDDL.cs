using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ODDL
{
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

        public static DataResult ReadDataType(char* text, out int textLength, out DataType dataType)
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
                    return DataResult.Okay;
                }

                if ((chars[length] == '1') && (chars[length + 1] == '6') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Int16;
                    textLength = length + 2;
                    return DataResult.Okay;
                }

                if ((chars[length] == '3') && (chars[length + 1] == '2') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Int32;
                    textLength = length + 2;
                    return DataResult.Okay;
                }

                if ((chars[length] == '6') && (chars[length + 1] == '4') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Int64;
                    textLength = length + 2;
                    return DataResult.Okay;
                }
            }
            else if (c == 'u')
            {
                int length = text.Compare("unsigned_int", 12) ? 12 : 1;

                if ((chars[length] == '8') && (IdentifierCharState[chars[length + 1]] == 0))
                {
                    dataType = DataType.UInt8;
                    textLength = length + 1;
                    return DataResult.Okay;
                }

                if ((chars[length] == '1') && (chars[length + 1] == '6') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.UInt16;
                    textLength = length + 2;
                    return DataResult.Okay;
                }

                if ((chars[length] == '3') && (chars[length + 1] == '2') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.UInt32;
                    textLength = length + 2;
                    return DataResult.Okay;
                }

                if ((chars[length] == '6') && (chars[length + 1] == '4') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.UInt64;
                    textLength = length + 2;
                    return DataResult.Okay;
                }
            }
            else if (c == 'f')
            {
                int length = text.Compare("float", 5) ? 5 : 1;

                if (IdentifierCharState[chars[length + 1]] == 0)
                {
                    dataType = DataType.Float;
                    textLength = length;
                    return DataResult.Okay;
                }

                if ((chars[length] == '1') && (chars[length + 1] == '6') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Half;
                    textLength = length + 2;
                    return DataResult.Okay;
                }

                if ((chars[length] == '3') && (chars[length + 1] == '2') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Float;
                    textLength = length + 2;
                    return DataResult.Okay;
                }

                if ((chars[length] == '6') && (chars[length + 1] == '4') && (IdentifierCharState[chars[length + 2]] == 0))
                {
                    dataType = DataType.Double;
                    textLength = length + 2;
                    return DataResult.Okay;
                }
            }
            else if (c == 'b')
            {
                int length = text.Compare("bool", 4) ? 4 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Bool;
                    textLength = length;
                    return DataResult.Okay;
                }
            }
            else if (c == 'h')
            {
                int length = text.Compare("half", 4) ? 4 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Half;
                    textLength = length;
                    return DataResult.Okay;
                }
            }
            else if (c == 'd')
            {
                int length = text.Compare("double", 6) ? 6 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Double;
                    textLength = length;
                    return DataResult.Okay;
                }
            }
            else if (c == 's')
            {
                int length = text.Compare("string", 6) ? 6 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.String;
                    textLength = length;
                    return DataResult.Okay;
                }
            }
            else if (c == 'r')
            {
                int length = (chars[1] == 'e' && chars[2] == 'f') ? 3 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Reference;
                    textLength = length;
                    return DataResult.Okay;
                }
            }
            else if (c == 't')
            {
                int length = text.Compare("type", 4) ? 4 : 1;

                if (IdentifierCharState[chars[length]] == 0)
                {
                    dataType = DataType.Type;
                    textLength = length;
                    return DataResult.Okay;
                }
            }

            textLength = 0;
            dataType = DataType.Invalid;
            return DataResult.TypeInvalid;
        }

        public static DataResult ReadIdentifier(char* text, out int textLength, StringBuilder identifier)
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
                    return DataResult.IdentifierIllegalChar;
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
                        return DataResult.IdentifierIllegalChar;
                    }

                    break;
                }

                identifier.Append((char)0);

                textLength = count;
                return DataResult.Okay;
            }
            else if (state == 2)
            {
                return DataResult.IdentifierIllegalChar;
            }

            return DataResult.IdentifierEmpty;
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

        public static DataResult ReadStringLiteral(char* text, out int textLength, out int stringLength, char* str)
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
                    return DataResult.StringIllegalChar;
                }

                if (c != '\\')
                {
                    int length = Text.ValidateGlyphCodeUTF8(chars);
                    if (length == 0)
                    {
                        return DataResult.StringIllegalChar;
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
                        return DataResult.StringIllegalEscape;
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
            return DataResult.Okay;
        }

        public static DataResult ReadBoolLiteral(char* text, out int textLength, out bool value)
        {
            char* chars = text;

            char c = chars[0];
            if (c == 'f')
            {
                if ((chars[1] == 'a') && (chars[2] == 'l') && (chars[3] == 's') && (chars[4] == 'e') && (IdentifierCharState[chars[5]] == 0))
                {
                    value = false;
                    textLength = 5;
                    return DataResult.Okay;
                }
            }
            else if (c == 't')
            {
                if ((chars[1] == 'r') && (chars[2] == 'u') && (chars[3] == 'e') && (IdentifierCharState[chars[4]] == 0))
                {
                    value = true;
                    textLength = 4;
                    return DataResult.Okay;
                }
            }

            textLength = 0;
            value = false;
            return DataResult.BoolInvalid;
        }

        public static DataResult ReadDecimalLiteral(char* text, out int textLength, out ulong value)
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
                        return DataResult.IntegerOverflow;
                    }

                    ulong w = v;
                    v = v * 10 + x;

                    if ((w >= 9U) && (v < 9U))
                    {
                        return DataResult.IntegerOverflow;
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
                return DataResult.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return DataResult.Okay;
        }

        public static DataResult ReadHexadecimalLiteral(char* text, out int textLength, out ulong value)
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
                        return DataResult.IntegerOverflow;
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
                return DataResult.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return DataResult.Okay;
        }

        public static DataResult ReadOctalLiteral(char* text, out int textLength, out ulong value)
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
                        return DataResult.IntegerOverflow;
                    }

                    ulong w = v;
                    v = v * 8 + x;

                    if ((w >= 7U) && (v < 7U))
                    {
                        return DataResult.IntegerOverflow;
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
                return DataResult.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return DataResult.Okay;
        }

        public static DataResult ReadBinaryLiteral(char* text, out int textLength, out ulong value)
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
                        return DataResult.IntegerOverflow;
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
                return DataResult.SyntaxError;
            }

            value = v;
            textLength = (int)(chars - text);
            return DataResult.Okay;
        }

        public static DataResult ReadCharLiteral(char* text, out int textLength, out ulong value)
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
                    return DataResult.CharIllegalChar;
                }

                if (c != '\\')
                {
                    if ((v >> 56) != 0)
                    {
                        return DataResult.IntegerOverflow;
                    }

                    v = (v << 8) | c;
                    chars++;
                }
                else
                {
                    int length = ReadEscapeChar(++chars, out char x);
                    if (length == 0)
                    {
                        return DataResult.CharIllegalEscape;
                    }

                    if ((v >> 56) != 0)
                    {
                        return DataResult.IntegerOverflow;
                    }

                    v = (v << 8) | x;
                    chars += length;
                }
            }

            value = v;
            textLength = (int)(chars - text);
            return DataResult.Okay;
        }

        public static DataResult ReadIntegerLiteral(char* text, out int textLength, out ulong value)
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
                DataResult result = ReadCharLiteral(chars + 1, out int length, out value);
                if (result == DataResult.Okay)
                {
                    if (text[length + 1] != '\'')
                    {
                        return DataResult.CharEndOfFile;
                    }

                    textLength = length + 2;
                }

                return result;
            }

            return ReadDecimalLiteral(text, out textLength, out value);
        }

        public static DataResult ReadFloatLiteral(char* text, out int textLength, out float value)
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
                    DataResult result = ReadHexadecimalLiteral(text, out textLength, out ulong val);
                    if (result == DataResult.Okay)
                    {
                        if (val > ((1UL << (sizeof(float) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return DataResult.FloatOverflow;
                        }

                        value = val;
                    }

                    return result;
                }

                if ((c == 'o') || (c == 'O'))
                {
                    DataResult result = ReadOctalLiteral(text, out textLength, out ulong val);
                    if (result == DataResult.Okay)
                    {
                        if (val > ((1UL << (sizeof(float) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return DataResult.FloatOverflow;
                        }

                        value = val;
                    }

                    return (result);
                }

                if ((c == 'b') || (c == 'B'))
                {
                    DataResult result = ReadBinaryLiteral(text, out textLength, out ulong val);
                    if (result == DataResult.Okay)
                    {
                        if (val > ((1UL << (sizeof(float) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return DataResult.FloatOverflow;
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
                        return DataResult.FloatInvalid;
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
                return DataResult.FloatInvalid;
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
                            return DataResult.FloatInvalid;
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
                    return DataResult.FloatInvalid;
                }

                c = chars[0];
            }

            if (!(wholeFlag | fractionFlag))
            {
                return DataResult.FloatInvalid;
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
                            return DataResult.FloatInvalid;
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
                    return DataResult.FloatInvalid;
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
            return DataResult.Okay;
        }

        public static DataResult ReadDoubleLiteral(char* text, out int textLength, out double value)
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
                    DataResult result = ReadHexadecimalLiteral(text, out textLength, out ulong val);
                    if (result == DataResult.Okay)
                    {
                        if (val > ((1UL << (sizeof(double) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return DataResult.FloatOverflow;
                        }

                        value = val;
                    }

                    return result;
                }

                if ((c == 'o') || (c == 'O'))
                {
                    DataResult result = ReadOctalLiteral(text, out textLength, out ulong val);
                    if (result == DataResult.Okay)
                    {
                        if (val > ((1UL << (sizeof(double) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return DataResult.FloatOverflow;
                        }

                        value = val;
                    }

                    return (result);
                }

                if ((c == 'b') || (c == 'B'))
                {
                    DataResult result = ReadBinaryLiteral(text, out textLength, out ulong val);
                    if (result == DataResult.Okay)
                    {
                        if (val > ((1UL << (sizeof(double) * 8 - 1)) - 1) * 2 + 1)
                        {
                            return DataResult.FloatOverflow;
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
                        return DataResult.FloatInvalid;
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
                return DataResult.FloatInvalid;
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
                            return DataResult.FloatInvalid;
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
                    return DataResult.FloatInvalid;
                }

                c = chars[0];
            }

            if (!(wholeFlag | fractionFlag))
            {
                return DataResult.FloatInvalid;
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
                            return DataResult.FloatInvalid;
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
                    return DataResult.FloatInvalid;
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
            return DataResult.Okay;
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

        public static DataResult ParseBool(char* text, out bool value)
        {
            DataResult result = ReadBoolLiteral(text, out int length, out value);
            if (result != DataResult.Okay)
            {
                return result;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return DataResult.Okay;
        }

        public static DataResult ParseInt8(char* text, out sbyte value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7F)
                {
                    return DataResult.IntegerOverflow;
                }

                value = (sbyte)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x80)
                {
                    return DataResult.IntegerOverflow;
                }

                value = (sbyte)-(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return DataResult.Okay;
        }

        public static DataResult ParseInt16(char* text, out short value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7FFF)
                {
                    return DataResult.IntegerOverflow;
                }

                value = (short)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x8000)
                {
                    return DataResult.IntegerOverflow;
                }

                value = (short)-(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return DataResult.Okay;
        }

        public static DataResult ParseInt32(char* text, out int value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7FFFFFFF)
                {
                    return DataResult.IntegerOverflow;
                }

                value = (int)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x80000000)
                {
                    return DataResult.IntegerOverflow;
                }

                value = (int)-(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return DataResult.Okay;
        }

        public static DataResult ParseInt64(char* text, out long value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
            {
                return result;
            }

            if (!negative)
            {
                if (unsignedValue > 0x7FFFFFFFFFFFFFFF)
                {
                    return DataResult.IntegerOverflow;
                }

                value = (long)unsignedValue;
            }
            else
            {
                if (unsignedValue > 0x8000000000000000)
                {
                    return DataResult.IntegerOverflow;
                }

                value = -(long)unsignedValue;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return DataResult.Okay;
        }

        public static DataResult ParseUInt8(char* text, out byte value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
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

            return DataResult.Okay;
        }

        public static DataResult ParseUInt16(char* text, out ushort value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
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

            return DataResult.Okay;
        }

        public static DataResult ParseUInt32(char* text, out uint value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
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

            return DataResult.Okay;
        }

        public static DataResult ParseUInt64(char* text, out ulong value)
        {
            value = 0;

            bool negative = ParseSign(text);

            DataResult result = ReadIntegerLiteral(text, out int length, out ulong unsignedValue);
            if (result != DataResult.Okay)
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

            return DataResult.Okay;
        }

        public static DataResult ParseFloat(char* text, out float value)
        {
            value = 0f;

            bool negative = ParseSign(text);

            DataResult result = ReadFloatLiteral(text, out int length, out float floatValue);
            if (result != DataResult.Okay)
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

            return DataResult.Okay;
        }

        public static DataResult ParseDouble(char* text, out double value)
        {
            value = 0f;

            bool negative = ParseSign(text);

            DataResult result = ReadDoubleLiteral(text, out int length, out double doubleValue);
            if (result != DataResult.Okay)
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

            return DataResult.Okay;
        }

        public static DataResult ParseString(char* text, out string value)
        {
            value = null;

            if (text[0] != '"')
            {
                return DataResult.StringInvalid;
            }

            int accumLength = 0;
            while (true)
            {
                text++;

                DataResult result = ReadStringLiteral(text, out int textLength, out int stringLength, null);
                if (result != DataResult.Okay)
                {
                    return result;
                }

                ReadStringLiteral(text, out textLength, out stringLength, &value[accumLength]);
                accumLength += stringLength;

                text += textLength;
                if (text[0] != '"')
                {
                    return DataResult.StringInvalid;
                }

                text++;
                text += GetWhitespaceLength(text);

                if (text[0] != '"')
                {
                    break;
                }
            }

            return DataResult.Okay;
        }

        public static DataResult ParseReference(char* text, out Reference value)
        {

        }

        public static DataResult ParseType(char* text, out DataType value)
        {
            DataResult result = ReadDataType(text, out int length, out value);
            if (result != DataResult.Okay)
            {
                return result;
            }

            text += length;
            text += GetWhitespaceLength(text);

            return DataResult.Okay;
        }

        public static DataResult ProcessText(char* text)
        {

        }
    }
}