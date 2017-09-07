﻿using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Vulkan
{
    public static unsafe class Utf8
    {
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

        public static string ToString(byte* bytePtr)
        {
            int length = 0;
            while (*(bytePtr++) != 0) length++;
            return Encoding.UTF8.GetString(bytePtr, length);
        }

        public static void Free(byte* bytePtr)
        {
            Marshal.FreeHGlobal(new IntPtr(bytePtr));
        }
        
        public static bool Compare(byte* str1, byte* str2)
        {
            while (true)
            {
                if (*str1 != *str2) return false;
                if (*str1 == 0) return true;
                str1++;
                str2++;
            }
        }
    }
}