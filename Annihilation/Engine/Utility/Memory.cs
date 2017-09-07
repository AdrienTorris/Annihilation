using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Engine
{
    public static unsafe class Memory
    {
#if PLATFORM_WINDOWS
        private const string MemCopyDll = "msvcrt.dll";
#elif PLATFORM_LINUX || PLATFORM_MACOS
        private const string MemCopyDll = "libc.so";
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

        public static byte* AllocateBytes(int length)
        {
            return (byte*)Marshal.AllocHGlobal(length * sizeof(byte));
        }

        public static char* AllocateChars(int length)
        {
            return (char*)Marshal.AllocHGlobal(length * sizeof(char));
        }

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

        public static void Clear(IntPtr dest, byte value, int sizeInBytesToClear)
        {
            
        }

        public static void Free(void* ptr)
        {
            Marshal.FreeHGlobal(new IntPtr(ptr));
            ptr = null;
        }
    }
}
