using System.Text;
using System.Runtime.InteropServices;

namespace Engine
{
    public static class StringUtility
    {
        public static unsafe int GetLength(char* text)
        {
            int count = 0;
            while (*(text++) != 0) count++;
            return count;
        }

        public static unsafe bool AreEqual(char* s1, char* s2)
        {
            while (true)
            {
                if (*s1 != *s2) return false;
                if (*s1 == 0) return true;
                s1++;
                s2++;
            }
        }

        public static unsafe bool Compare(char* s1, char* s2, int count)
        {
            while (true)
            {
                if (count-- <= 0) return true;
                if (*s1 != *s2) return false;
                if (*s1 == 0) return true;
                s1++;
                s2++;
            }
        }

        public static unsafe bool Compare(string s1, string s2)
        {
            fixed (char* s1Ptr = s1)
            fixed (char* s2Ptr = s2)
            {
                return AreEqual(s1Ptr, s2Ptr);
            }
        }

        public static unsafe bool Compare(string s1, string s2, int count)
        {
            fixed (char* s1Ptr = s1)
            fixed (char* s2Ptr = s2)
            {
                return Compare(s1Ptr, s2Ptr, count);
            }
        }

        public static unsafe int IndexOf(char* text, char c)
        {
            int index = 0;
            while (true)
            {
                if (*text == c) return index;
                if (*text == 0) return -1;
                text++;
                index++;
            }
        }

        public static unsafe char* Duplicate(char* text)
        {
            int length = GetLength(text);
            char* ptr = Memory.AllocateChars(length);
            Memory.Copy(ptr, text, length);
            return ptr;
        }

        public static unsafe ValueType GetType(char* text)
        {
            if (*text == '"') return ValueType.String;
            if (*text == 't' || *text == 'f') return ValueType.Bool;
            if (IndexOf(text, '.') > -1) return ValueType.Float;
            else if (*text == '-' || (*text >= '0' && *text <= '9')) return ValueType.Int32;

            return ValueType.String;
        }

        public static unsafe bool ToBool(char* text)
        {
            return *text == 't' ? true : false;
        }

        public static unsafe int ToInt(char* text)
        {
            int val = 0;
            int sign = 1;
            int c;

            if (*text == '-')
            {
                sign = -1;
                text++;
            }

            // Check for hex
            if (*text == '0' && (*(text + 1) == 'x' || *(text + 1) == 'X'))
            {
                text += 2;
                while (true)
                {
                    c = *(text++);
                    if (c >= '0' && c <= '9') val = (val << 4) + c - '0';
                    else if (c >= 'a' && c <= 'f') val = (val << 4) + c - 'a' + 10;
                    else if (c >= 'A' && c <= 'F') val = (val << 4) + c - 'A' + 10;
                    else return val * sign;
                }
            }

            // Check for character
            if (*text == '\'')
            {
                return sign * *(text + 1);
            }

            // Assume decimal
            while (true)
            {
                c = *(text++);
                if (c < '0' || c > '9') return val * sign;
                val = val * 10 + c - '0';
            }
        }

        public static unsafe float ToFloat(char* text)
        {
            float val = 0;
            int sign = 1;
            int c;

            if (*text == '-')
            {
                sign = -1;
                text++;
            }

            // Check for hex
            if (*text == '0' && (*(text + 1) == 'x' || *(text + 1) == 'X'))
            {
                text += 2;
                while (true)
                {
                    c = *(text++);
                    if (c >= '0' && c <= '9') val = (val * 16) + c - '0';
                    else if (c >= 'a' && c <= 'f') val = (val * 16) + c - 'a' + 10;
                    else if (c >= 'A' && c <= 'F') val = (val * 16) + c - 'A' + 10;
                    else return val * sign;
                }
            }

            // Check for character
            if (*text == '\'')
            {
                return sign * *(text + 1);
            }

            // Assume decimal
            int @decimal = -1;
            int total = 0;
            while (true)
            {
                c = *(text++);
                if (c == '.')
                {
                    @decimal = total;
                    continue;
                }
                if (c < '0' || c > '9') break;

                val = val * 10 + c - '0';
                total++;
            }

            if (@decimal == -1) return val * sign;

            while (total > @decimal)
            {
                val /= 10;
                total--;
            }

            return val * sign;
        }
    }
}