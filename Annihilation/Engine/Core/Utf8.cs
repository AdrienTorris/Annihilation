using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Engine
{
    public static unsafe class Utf8
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetLength(byte* text)
        {
            int count = 0;
            while (*(text++) != 0) count++;
            return count;
        }

        public static bool Compare(byte* s1, byte* s2)
        {
            while (true)
            {
                if (*s1 != *s2) return false;
                if (*s1 == 0) return true;
                s1++;
                s2++;
            }
        }

        public static bool Compare(byte* s1, byte* s2, int count)
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

        public static byte* AllocateFromString(string str)
        {
            int maxByteCount = Encoding.UTF8.GetMaxByteCount(str.Length);
            byte* bytePtr = (byte*)Marshal.AllocHGlobal(maxByteCount);
            int actualLength = 0;
            fixed (char* charPtr = str)
            {
                actualLength = Encoding.UTF8.GetBytes(charPtr, str.Length, bytePtr, maxByteCount);
            }

            *(bytePtr + actualLength) = 0;

            return bytePtr;
        }

        public static byte* AllocateFromChars(char* chars, int length)
        {
            int maxByteCount = Encoding.UTF8.GetMaxByteCount(length);
            byte* bytePtr = (byte*)Marshal.AllocHGlobal(maxByteCount);
            int bytesLength = Encoding.UTF8.GetBytes(chars, length, bytePtr, maxByteCount);

            *(bytePtr + bytesLength) = 0;

            return bytePtr;
        }

        public static byte* AllocateFromChars(char* chars, int length, out int bytesLength)
        {
            int maxByteCount = Encoding.UTF8.GetMaxByteCount(length);
            byte* bytePtr = (byte*)Marshal.AllocHGlobal(maxByteCount);
            bytesLength = Encoding.UTF8.GetBytes(chars, length, bytePtr, maxByteCount);

            *(bytePtr + bytesLength) = 0;

            return bytePtr;
        }

        public static byte* AllocateFromChars(char* chars)
        {
            return AllocateFromChars(chars, StringUtility.GetLength(chars));
        }

        public static string ToString(byte* bytePtr)
        {
            int length = GetLength(bytePtr);
            return Encoding.UTF8.GetString(bytePtr, length);
        }

        public static void Free(byte* bytePtr)
        {
            Marshal.FreeHGlobal(new IntPtr(bytePtr));
        }
    }
}