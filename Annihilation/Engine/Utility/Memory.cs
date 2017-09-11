using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Engine
{
    public static unsafe class Memory
    {
#if PLATFORM_WINDOWS
        private const string MemCopyDll = "msvcrt.dll";
#elif PLATFORM_LINUX || PLATFORM_MACOS
        private const string MemCopyDll = "libc";
#else
#error Unsupported platform
#endif

        [SuppressUnmanagedCodeSecurity, DllImport(MemCopyDll, EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern IntPtr CopyMemory(IntPtr dest, IntPtr src, ulong sizeInBytesToCopy);

        [SuppressUnmanagedCodeSecurity, DllImport(MemCopyDll, EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern IntPtr CopyMemory(void* dest, void* src, ulong sizeInBytesToCopy);

        public static void Copy(IntPtr dest, IntPtr src, int sizeInBytesToCopy)
        {
            CopyMemory(dest, src, (ulong)sizeInBytesToCopy);
        }

        public static void Copy(void* dest, void* src, int sizeInBytesToCopy)
        {
            CopyMemory(dest, src, (ulong)sizeInBytesToCopy);
        }

        public static bool Compare(IntPtr from, IntPtr against, int sizeToCompare)
        {
            var pSrc = (byte*)from;
            var pDst = (byte*)against;

            // Compare 8 bytes
            var numberOf = sizeToCompare >> 3;
            while (numberOf > 0)
            {
                if (*(long*)pSrc != *(long*)pDst)
                    return false;
                pSrc += 8;
                pDst += 8;
                numberOf--;
            }

            // Compare remaining bytes
            numberOf = sizeToCompare & 7;
            while (numberOf > 0)
            {
                if (*pSrc != *pDst)
                    return false;
                pSrc++;
                pDst++;
                numberOf--;
            }
            return true;
        }

        public static bool Compare(byte* from, byte* against, int sizeToCompare)
        {
            // Compare 8 bytes
            int numberOf = sizeToCompare >> 3;
            while (numberOf > 0)
            {
                if (*(long*)from != *(long*)against)
                    return false;
                from += 8;
                against += 8;
                numberOf--;
            }

            // Compare remaining bytes
            numberOf = sizeToCompare & 7;
            while (numberOf > 0)
            {
                if (*from != *against)
                    return false;
                from++;
                against++;
                numberOf--;
            }
            return true;
        }

        public static bool IsZeroed(byte* buffer, int byteCount)
        {
            // Compare 8 bytes
            int numberOf = byteCount >> 3;
            while (numberOf > 0)
            {
                if (*(long*)buffer != 0)
                    return false;
                buffer += 8;
                numberOf--;
            }

            // Compare remaining bytes
            numberOf = byteCount & 7;
            while (numberOf > 0)
            {
                if (*buffer != 0)
                    return false;
                buffer++;
                numberOf--;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte* AllocateBytes(int length)
        {
            return (byte*)Marshal.AllocHGlobal(length * sizeof(byte));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte* AllocateAndClearBytes(int length)
        {
            byte* buffer = AllocateBytes(length);
            Clear(buffer, 0, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static char* AllocateChars(int length)
        {
            return (char*)Marshal.AllocHGlobal(length * sizeof(char));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool* AllocateBools(int length)
        {
            return (bool*)Marshal.AllocHGlobal(length * sizeof(bool));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float* AllocateFloats(int length)
        {
            return (float*)Marshal.AllocHGlobal(length * sizeof(float));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float* AllocateAndClearFloats(int length)
        {
            float* buffer = AllocateFloats(length);
            Clear(buffer, 0f, length);
            return buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* AllocatePointers(int length)
        {
            return (void*)Marshal.AllocHGlobal(length * sizeof(void*));
        }

        public static IntPtr AllocateAligned(int sizeInBytes, int align = 16)
        {
            int mask = align - 1;
            if ((align & mask) != 0)
            {
                throw new ArgumentException("Alignment is not power of 2", nameof(align));
            }
            IntPtr memoryPtr = Marshal.AllocHGlobal(sizeInBytes + mask + sizeof(void*));
            byte* ptr = (byte*)((ulong)(memoryPtr + sizeof(void*) + mask) & ~(ulong)mask);
            ((IntPtr*)ptr)[-1] = memoryPtr;
            return new IntPtr(ptr);
        }

        public static void Clear(byte* dest, byte value, int byteCount)
        {
            // Clear 8 bytes
            int numberOf = byteCount >> 3;
            while (numberOf > 0)
            {
                *(long*)dest = value;
                dest += 8;
                numberOf--;
            }

            // Clear remaining bytes
            numberOf = byteCount & 7;
            while (numberOf > 0)
            {
                *dest = value;
                dest++;
                numberOf--;
            }
        }

        public static void Clear(bool* dest, byte value, int byteCount)
        {
            // Clear 8 bytes
            int numberOf = byteCount >> 3;
            while (numberOf > 0)
            {
                *(long*)dest = value;
                dest += 8;
                numberOf--;
            }

            // Clear remaining bytes
            numberOf = byteCount & 7;
            while (numberOf > 0)
            {
                *dest = value == 0 ? false : true;
                dest++;
                numberOf--;
            }
        }

        public static void Clear(float* dest, float value, int length)
        {
            while (length > 0)
            {
                *dest = value;
                dest++;
                length--;
            }
        }

        public static void Free(void* ptr)
        {
            Marshal.FreeHGlobal(new IntPtr(ptr));
            ptr = null;
        }
    }
}
