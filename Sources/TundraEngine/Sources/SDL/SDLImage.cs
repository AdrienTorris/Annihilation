using System;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    public static partial class SDL
    {
        [Flags]
        public enum ImageInitFlags
        {
            Jpg = 0x00000001,
            Png = 0x00000002,
            Tif = 0x00000004,
            Webp = 0x00000008
        }
        
        /// <summary>
        /// Loads dynamic libraries and prepares them for use.  Flags should be one or more flags from IMG_InitFlags OR'd together. It returns the flags successfully initialized, or 0 on failure.
        /// </summary>
        [DllImport(LibName)]
        public static extern int IMG_Init(ImageInitFlags flags);
        
        [DllImport(LibName)]
        public static extern void IMG_Quit();
        
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadTyped_RW(IntPtr src, int freesrc, string type);
        [DllImport(LibName)]
        public static extern IntPtr IMG_Load(string file);
        [DllImport(LibName)]
        public static extern IntPtr IMG_Load_RW(IntPtr src, int freesrc);

        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadTexture(IntPtr renderer, string file);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadTexture_RW(IntPtr renderer, IntPtr src, int freesrc);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadTextureTyped_RW(IntPtr renderer, IntPtr src, int freesrc, string type);

        [DllImport(LibName)]
        public static extern int IMG_isICO(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isCUR(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isBMP(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isGIF(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isJPG(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isLBM(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isPCX(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isPNG(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isPNM(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isTIF(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isXCF(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isXPM(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isXV(IntPtr src);
        [DllImport(LibName)]
        public static extern int IMG_isWEBP(IntPtr src);

        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadICO_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadCUR_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadBMP_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadGIF_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadJPG_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadLBM_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadPCX_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadPNG_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadPNM_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadTGA_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadTIF_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadXCF_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadXPM_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadXV_RW(IntPtr src);
        [DllImport(LibName)]
        public static extern IntPtr IMG_LoadWEBP_RW(IntPtr src);

        [DllImport(LibName)]
        public static extern IntPtr IMG_ReadXPMFromArray(string[] xpm);

        [DllImport(LibName)]
        public static extern int IMG_SavePNG(IntPtr surface, string file);
        [DllImport(LibName)]
        public static extern int IMG_SavePNG_RW(IntPtr surface, IntPtr dst, int freedst);
    }
}