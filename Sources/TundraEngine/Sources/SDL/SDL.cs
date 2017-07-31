using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    /// <summary> Pixel type. </summary>
    public enum PixelType : uint
    {
        Unknown,
        Index1,
        Index4,
        Index8,
        Packed8,
        Packed16,
        Packed32,
        ArrayU8,
        ArrayU16,
        ArrayU32,
        ArrayF16,
        ArrayF32
    }

    public enum PixelOrder : uint
    {
        // Bitmap order
        BitmapNone = 0,
        Bitmap4321,
        Bitmap1234,
        // Packed order
        PackedNone = 0,
        PackedXRGB,
        PackedRGBX,
        PackedARGB,
        PackedRGBA,
        PackedXBGR,
        PackedBGRX,
        PackedABGR,
        PackedBGRA,
        // Array order
        ArrayNone = 0,
        ArrayRGB,
        ArrayRGBA,
        ArrayARGB,
        ArrayBGR,
        ArrayBGRA,
        ArrayABGR
    }

    /// <summary> Packed component layout. </summary>
    public enum PackedLayout : uint
    {
        None,
        Layout332,
        Layout4444,
        Layout1555,
        Layout5551,
        Layout565,
        Layout8888,
        Layout2101010,
        Layout1010102
    }
    
    [SuppressUnmanagedCodeSecurity]
    public static unsafe partial class SDL
    {
        public const string LibraryName = "SDL2.dll";
        
        /// <summary>
        /// These are the flags which may be passed to SDL_Init(). You should specify the subsystems which you will be using in your application.
        /// </summary>
        [Flags]
        public enum SDL_InitFlags : uint
        {
            Timer = 0x00000001u,
            Audio = 0x00000010u,
            Video = 0x00000020u,
            Joystick = 0x00000200u,
            Haptic = 0x00001000u,
            GameController = 0x00002000u,
            Events = 0x00004000u,
            NoParachute = 0x00100000u,
            Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events
        }

        unsafe internal static string GetString (IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                return string.Empty;

            var ptr = (byte*)handle;
            return GetString(ptr);
        }

        internal static unsafe string GetString(byte* ptr)
        {
            byte* counter = ptr;
            while (*counter != 0)
            {
                counter++;
            }
            int count = (int)(counter - ptr);

            return Encoding.UTF8.GetString(ptr, count);
        }

        public static uint FourCharacterCode(byte a, byte b, byte c, byte d)
        {
            return (uint)(a | (b << 8) | (c << 16) | (d << 24));
        }

        /// <summary>
        /// This function initializes the subsystems specified by <paramref name="flags"/>
        /// </summary>
        [DllImport (LibraryName)]
        public extern static int SDL_Init (SDL_InitFlags flags);
        
        /// <summary>
        /// This function initializes specific SDL subsystems
        /// <para /> Subsystem initialization is ref-counted, you must call SDL_QuitSubSystem () for each SDL_InitSubSystem () to correctly shutdown a subsystem manually (or call SDL_Quit() to force shutdown).
        /// <para /> If a subsystem is already loaded then this call will increase the ref-count and return.
        /// </summary>
        [DllImport (LibraryName)]
        internal extern static int SDL_InitSubSystem (SDL_InitFlags flags);

        /// <summary>
        /// This function cleans up specific SDL subsystems
        /// </summary>
        [DllImport (LibraryName)]
        public extern static void SDL_QuitSubSystem (SDL_InitFlags flags);

        /// <summary>
        /// This function returns a mask of the specified subsystems which have previously been initialized.
        /// <para /> If <paramref name="flags"/> is 0, it returns a mask of all initialized subsystems.
        /// </summary>
        [DllImport (LibraryName)]
        public extern static SDL_InitFlags SDL_WasInit (SDL_InitFlags flags);

        /// <summary>
        /// This function cleans up all initialized subsystems. You should call it upon all exit conditions.
        /// </summary>
        [DllImport (LibraryName)]
        public extern static void SDL_Quit ();
        
        // SDL_loadso.h

        [DllImport(LibraryName)]
        public extern static void* SDL_LoadObject(byte* file);

        [DllImport(LibraryName)]
        public extern static void* SDL_LoadFunction(void* handle, byte* name);

        [DllImport(LibraryName)]
        public extern static void SDL_UnloadObject(void* handle);

        // SDL_pixels.h

        public static uint DefinePixelFormat(PixelType type, PixelOrder order, PackedLayout layout, byte bits, byte bytes)
        {
            return (uint)((1 << 28) | ((byte)type << 24) | ((byte)order << 20) | ((byte)layout << 16) | (bits << 8) | bytes);
        }

        public static readonly uint PixelFormatUnknown = 0;
        public static readonly uint PixelFormatIndex1LSB = DefinePixelFormat(PixelType.Index1, PixelOrder.Bitmap4321, 0, 1, 0);
        public static readonly uint PixelFormatIndex1MSB = DefinePixelFormat(PixelType.Index1, PixelOrder.Bitmap1234, 0, 1, 0);
        public static readonly uint PixelFormatIndex4LSB = DefinePixelFormat(PixelType.Index4, PixelOrder.Bitmap4321, 0, 4, 0);
        public static readonly uint PixelFormatIndex4MSB = DefinePixelFormat(PixelType.Index4, PixelOrder.Bitmap1234, 0, 4, 0);
        public static readonly uint PixelFormatIndex8 = DefinePixelFormat(PixelType.Index8, 0, 0, 8, 1);
        public static readonly uint PixelFormatRGB332 = DefinePixelFormat(PixelType.Packed8, PixelOrder.PackedXRGB, PackedLayout.Layout332, 8, 1);
        public static readonly uint PixelFormatRGB444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout4444, 12, 2);
        public static readonly uint PixelFormatRGB555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout1555, 15, 2);
        public static readonly uint PixelFormatBGR555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXBGR, PackedLayout.Layout1555, 15, 2);
        public static readonly uint PixelFormatARGB4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedARGB, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatRGBA4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedRGBA, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatABGR4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedABGR, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatBGRA4444 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedBGRA, PackedLayout.Layout4444, 16, 2);
        public static readonly uint PixelFormatARGB1555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedARGB, PackedLayout.Layout1555, 16, 2);
        public static readonly uint PixelFormatRGBA5551 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedRGBA, PackedLayout.Layout5551, 16, 2);
        public static readonly uint PixelFormatABGR1555 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedABGR, PackedLayout.Layout1555, 16, 2);
        public static readonly uint PixelFormatBGRA5551 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedBGRA, PackedLayout.Layout5551, 16, 2);
        public static readonly uint PixelFormatRGB565 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXRGB, PackedLayout.Layout565, 16, 2);
        public static readonly uint PixelFormatBGR565 = DefinePixelFormat(PixelType.Packed16, PixelOrder.PackedXBGR, PackedLayout.Layout565, 16, 2);
        public static readonly uint PixelFormatRGB24 = DefinePixelFormat(PixelType.ArrayU8, PixelOrder.ArrayRGB, 0, 24, 3);
        public static readonly uint PixelFormatBGR24 = DefinePixelFormat(PixelType.ArrayU8, PixelOrder.ArrayBGR, 0, 24, 3);
        public static readonly uint PixelFormatRGB888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedXRGB, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatRGBX8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedRGBX, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatBGR888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedXBGR, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatBGRX8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedBGRX, PackedLayout.Layout8888, 24, 4);
        public static readonly uint PixelFormatARGB8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatRGBA8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatABGR8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedABGR, PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatBGRA8888 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedBGRA,  PackedLayout.Layout8888, 32, 4);
        public static readonly uint PixelFormatARGB2101010 = DefinePixelFormat(PixelType.Packed32, PixelOrder.PackedARGB, 0, 32, 4);
        public static readonly uint PixelFormatYV12 = FourCharacterCode((byte)'Y', (byte)'V', (byte)'1', (byte)'2');
        public static readonly uint PixelFormatIYUV = FourCharacterCode((byte)'I', (byte)'Y', (byte)'U', (byte)'V');
        public static readonly uint PixelFormatYUY2 = FourCharacterCode((byte)'Y', (byte)'U', (byte)'Y', (byte)'2');
        public static readonly uint PixelFormatUYVY = FourCharacterCode((byte)'U', (byte)'Y', (byte)'V', (byte)'Y');
        public static readonly uint PixelFormatYVYU = FourCharacterCode((byte)'Y', (byte)'V', (byte)'Y', (byte)'U');
        public static readonly uint PixelFormatNV12 = FourCharacterCode((byte)'N', (byte)'V', (byte)'1', (byte)'2');
        public static readonly uint PixelFormatNV21 = FourCharacterCode((byte)'N', (byte)'V', (byte)'2', (byte)'1');
    }
}