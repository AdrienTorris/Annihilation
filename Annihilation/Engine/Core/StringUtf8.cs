using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Engine
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct StringUtf8 : IEquatable<StringUtf8>
    {
        public static int GetLength(byte* text)
        {
            int count = 0;
            while (*(text++) != 0) count++;
            return count;
        }

        public static unsafe bool Compare(StringUtf8 s1, StringUtf8 s2)
        {
            while (true)
            {
                if (*s1.BytePtr != *s2.BytePtr) return false;
                if (*s1.BytePtr == 0) return true;
                s1.BytePtr++;
                s2.BytePtr++;
            }
        }

        public static unsafe bool Compare(StringUtf8 s1, StringUtf8 s2, int count)
        {
            while (true)
            {
                if (count-- <= 0) return true;
                if (*s1.BytePtr != *s2.BytePtr) return false;
                if (*s1.BytePtr == 0) return true;
                s1.BytePtr++;
                s2.BytePtr++;
            }
        }

        public byte* BytePtr;

        public StringUtf8(byte* ptr) => BytePtr = ptr;

        public StringUtf8(string str)
        {
            int maxByteCount = Encoding.UTF8.GetMaxByteCount(str.Length);
            byte* bytePtr = Memory.AllocateBytes(maxByteCount);
            int actualLength = 0;
            fixed (char* charPtr = str)
            {
                actualLength = Encoding.UTF8.GetBytes(charPtr, str.Length, bytePtr, maxByteCount);
            }

            *(bytePtr + actualLength) = 0;

            BytePtr = bytePtr;
        }

        public int GetLength() => GetLength(BytePtr);

        public void Free() => Memory.Free(BytePtr);

        public bool Equals(StringUtf8 other) => BytePtr == other.BytePtr;

        public override int GetHashCode() => (int)BytePtr;
        public override bool Equals(object obj) => obj is StringUtf8 ? Equals((StringUtf8)obj) : false;
        public override string ToString() => Encoding.UTF8.GetString(BytePtr, GetLength());

        public static bool operator ==(StringUtf8 s1, StringUtf8 s2) => Compare(s1, s2);
        public static bool operator !=(StringUtf8 s1, StringUtf8 s2) => !(s1 == s2);
        
        public static implicit operator byte* (StringUtf8 text) => text.BytePtr;
        public static implicit operator StringUtf8(byte* ptr) => new StringUtf8(ptr);
    }
}