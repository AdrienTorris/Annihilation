using System.Text;

namespace Engine.Mathematics
{
    public static unsafe class MetroHash
    {
        private const ulong K0 = 0xD6D018F5;
        private const ulong K1 = 0xA2AA033B;
        private const ulong K2 = 0x62992FC1;
        private const ulong K3 = 0x30BC5B29;

        public static ulong Hash64(byte* buffer, ulong length, ulong seed = 0)
        {
            byte* ptr = buffer;
            byte* end = buffer + length;

            ulong h = (seed + K2) * K0;

            if (length >= 32)
            {
                ulong[] v = { h, h, h, h };

                do
                {
                    v[0] += ReadU64(ptr) * K0; ptr += 8; v[0] = RotateRight(v[0], 29) + v[2];
                    v[1] += ReadU64(ptr) * K1; ptr += 8; v[1] = RotateRight(v[1], 29) + v[3];
                    v[2] += ReadU64(ptr) * K2; ptr += 8; v[2] = RotateRight(v[2], 29) + v[0];
                    v[3] += ReadU64(ptr) * K3; ptr += 8; v[3] = RotateRight(v[3], 29) + v[1];
                }
                while (ptr <= (end - 32));

                v[2] ^= RotateRight(((v[0] + v[3]) * K0) + v[1], 37) * K1;
                v[3] ^= RotateRight(((v[1] + v[2]) * K1) + v[0], 37) * K0;
                v[0] ^= RotateRight(((v[0] + v[2]) * K0) + v[3], 37) * K1;
                v[1] ^= RotateRight(((v[1] + v[3]) * K1) + v[2], 37) * K0;
                h += v[0] ^ v[1];
            }

            if ((end - ptr) >= 16)
            {
                ulong v0 = h + (ReadU64(ptr) * K2); ptr += 8; v0 = RotateRight(v0, 29) * K3;
                ulong v1 = h + (ReadU64(ptr) * K2); ptr += 8; v1 = RotateRight(v1, 29) * K3;
                v0 ^= RotateRight(v0 * K0, 21) + v1;
                v1 ^= RotateRight(v1 * K3, 21) + v0;
                h += v1;
            }

            if ((end - ptr) >= 8)
            {
                h += ReadU64(ptr) * K3; ptr += 8;
                h ^= RotateRight(h, 55) * K1;
            }

            if ((end - ptr) >= 4)
            {
                h += ReadU32(ptr) * K3; ptr += 4;
                h ^= RotateRight(h, 26) * K1;
            }

            if ((end - ptr) >= 2)
            {
                h += ReadU16(ptr) * K3; ptr += 2;
                h ^= RotateRight(h, 48) * K1;
            }

            if ((end - ptr) >= 1)
            {
                h += ReadU8(ptr) * K3;
                h ^= RotateRight(h, 37) * K1;
            }

            h ^= RotateRight(h, 28);
            h *= K0;
            h ^= RotateRight(h, 29);

            return h;
        }

        public static ulong Hash64(char* text, int length, ulong seed = 0)
        {
            int maxBytes = Encoding.UTF8.GetMaxByteCount(length);
            byte* buffer = null;
            int byteCount = Encoding.UTF8.GetBytes(text, length, buffer, maxBytes);
            return Hash64(buffer, (ulong)byteCount, seed);
        }

        public static ulong Hash64(string text, ulong seed = 0)
        {
            fixed (char* chars = text)
            {
                return Hash64(chars, text.Length, seed);
            }
        }

        public static uint Hash32(byte* buffer, ulong length, ulong seed = 0)
        {
            return (uint)(Hash64(buffer, length, seed) >> 32);
        }

        public static uint Hash32(char* text, int length, ulong seed = 0)
        {
            return (uint)(Hash64(text, length, seed) >> 32);
        }

        public static uint Hash32(string text, ulong seed = 0)
        {
            return (uint)(Hash64(text, seed) >> 32);
        }

        private static ulong RotateRight(ulong v, int k) => (v >> k) | (v << (64 - k));

        private static ulong ReadU64(void* ptr) => *(ulong*)ptr;

        private static ulong ReadU32(void* ptr) => *(uint*)ptr;

        private static ulong ReadU16(void* ptr) => *(ushort*)ptr;

        private static ulong ReadU8(void* ptr) => *(byte*)ptr;
    }
}