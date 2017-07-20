using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace TundraEngine.Mathematics
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color : IEquatable<Color>
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
        
        /// <summary>
        /// Zero color.
        /// </summary>
        public static readonly Color Zero = Color.FromBgra(0x00000000);
        /// <summary>
        /// Transparent color.
        /// </summary>
        public static readonly Color Transparent = Color.FromBgra(0x00000000);
        /// <summary>
        /// AliceBlue color.
        /// </summary>
        public static readonly Color AliceBlue = Color.FromBgra(0xFFF0F8FF);
        /// <summary>
        /// AntiqueWhite color.
        /// </summary>
        public static readonly Color AntiqueWhite = Color.FromBgra(0xFFFAEBD7);
        /// <summary>
        /// Aqua color.
        /// </summary>
        public static readonly Color Aqua = Color.FromBgra(0xFF00FFFF);
        /// <summary>
        /// Aquamarine color.
        /// </summary>
        public static readonly Color Aquamarine = Color.FromBgra(0xFF7FFFD4);
        /// <summary>
        /// Azure color.
        /// </summary>
        public static readonly Color Azure = Color.FromBgra(0xFFF0FFFF);
        /// <summary>
        /// Beige color.
        /// </summary>
        public static readonly Color Beige = Color.FromBgra(0xFFF5F5DC);
        /// <summary>
        /// Bisque color.
        /// </summary>
        public static readonly Color Bisque = Color.FromBgra(0xFFFFE4C4);
        /// <summary>
        /// Black color.
        /// </summary>
        public static readonly Color Black = Color.FromBgra(0xFF000000);
        /// <summary>
        /// BlanchedAlmond color.
        /// </summary>
        public static readonly Color BlanchedAlmond = Color.FromBgra(0xFFFFEBCD);
        /// <summary>
        /// Blue color.
        /// </summary>
        public static readonly Color Blue = Color.FromBgra(0xFF0000FF);
        /// <summary>
        /// BlueViolet color.
        /// </summary>
        public static readonly Color BlueViolet = Color.FromBgra(0xFF8A2BE2);
        /// <summary>
        /// Brown color.
        /// </summary>
        public static readonly Color Brown = Color.FromBgra(0xFFA52A2A);
        /// <summary>
        /// BurlyWood color.
        /// </summary>
        public static readonly Color BurlyWood = Color.FromBgra(0xFFDEB887);
        /// <summary>
        /// CadetBlue color.
        /// </summary>
        public static readonly Color CadetBlue = Color.FromBgra(0xFF5F9EA0);
        /// <summary>
        /// Chartreuse color.
        /// </summary>
        public static readonly Color Chartreuse = Color.FromBgra(0xFF7FFF00);
        /// <summary>
        /// Chocolate color.
        /// </summary>
        public static readonly Color Chocolate = Color.FromBgra(0xFFD2691E);
        /// <summary>
        /// Coral color.
        /// </summary>
        public static readonly Color Coral = Color.FromBgra(0xFFFF7F50);
        /// <summary>
        /// CornflowerBlue color.
        /// </summary>
        public static readonly Color CornflowerBlue = Color.FromBgra(0xFF6495ED);
        /// <summary>
        /// Cornsilk color.
        /// </summary>
        public static readonly Color Cornsilk = Color.FromBgra(0xFFFFF8DC);
        /// <summary>
        /// Crimson color.
        /// </summary>
        public static readonly Color Crimson = Color.FromBgra(0xFFDC143C);
        /// <summary>
        /// Cyan color.
        /// </summary>
        public static readonly Color Cyan = Color.FromBgra(0xFF00FFFF);
        /// <summary>
        /// DarkBlue color.
        /// </summary>
        public static readonly Color DarkBlue = Color.FromBgra(0xFF00008B);
        /// <summary>
        /// DarkCyan color.
        /// </summary>
        public static readonly Color DarkCyan = Color.FromBgra(0xFF008B8B);
        /// <summary>
        /// DarkGoldenrod color.
        /// </summary>
        public static readonly Color DarkGoldenrod = Color.FromBgra(0xFFB8860B);
        /// <summary>
        /// DarkGray color.
        /// </summary>
        public static readonly Color DarkGray = Color.FromBgra(0xFFA9A9A9);
        /// <summary>
        /// DarkGreen color.
        /// </summary>
        public static readonly Color DarkGreen = Color.FromBgra(0xFF006400);
        /// <summary>
        /// DarkKhaki color.
        /// </summary>
        public static readonly Color DarkKhaki = Color.FromBgra(0xFFBDB76B);
        /// <summary>
        /// DarkMagenta color.
        /// </summary>
        public static readonly Color DarkMagenta = Color.FromBgra(0xFF8B008B);
        /// <summary>
        /// DarkOliveGreen color.
        /// </summary>
        public static readonly Color DarkOliveGreen = Color.FromBgra(0xFF556B2F);
        /// <summary>
        /// DarkOrange color.
        /// </summary>
        public static readonly Color DarkOrange = Color.FromBgra(0xFFFF8C00);
        /// <summary>
        /// DarkOrchid color.
        /// </summary>
        public static readonly Color DarkOrchid = Color.FromBgra(0xFF9932CC);
        /// <summary>
        /// DarkRed color.
        /// </summary>
        public static readonly Color DarkRed = Color.FromBgra(0xFF8B0000);
        /// <summary>
        /// DarkSalmon color.
        /// </summary>
        public static readonly Color DarkSalmon = Color.FromBgra(0xFFE9967A);
        /// <summary>
        /// DarkSeaGreen color.
        /// </summary>
        public static readonly Color DarkSeaGreen = Color.FromBgra(0xFF8FBC8B);
        /// <summary>
        /// DarkSlateBlue color.
        /// </summary>
        public static readonly Color DarkSlateBlue = Color.FromBgra(0xFF483D8B);
        /// <summary>
        /// DarkSlateGray color.
        /// </summary>
        public static readonly Color DarkSlateGray = Color.FromBgra(0xFF2F4F4F);
        /// <summary>
        /// DarkTurquoise color.
        /// </summary>
        public static readonly Color DarkTurquoise = Color.FromBgra(0xFF00CED1);
        /// <summary>
        /// DarkViolet color.
        /// </summary>
        public static readonly Color DarkViolet = Color.FromBgra(0xFF9400D3);
        /// <summary>
        /// DeepPink color.
        /// </summary>
        public static readonly Color DeepPink = Color.FromBgra(0xFFFF1493);
        /// <summary>
        /// DeepSkyBlue color.
        /// </summary>
        public static readonly Color DeepSkyBlue = Color.FromBgra(0xFF00BFFF);
        /// <summary>
        /// DimGray color.
        /// </summary>
        public static readonly Color DimGray = Color.FromBgra(0xFF696969);
        /// <summary>
        /// VeryDimGray color.
        /// </summary>
        public static readonly Color VeryDimGray = Color.FromBgra(0xFF404040);
        /// <summary>
        /// DodgerBlue color.
        /// </summary>
        public static readonly Color DodgerBlue = Color.FromBgra(0xFF1E90FF);
        /// <summary>
        /// Firebrick color.
        /// </summary>
        public static readonly Color Firebrick = Color.FromBgra(0xFFB22222);
        /// <summary>
        /// FloralWhite color.
        /// </summary>
        public static readonly Color FloralWhite = Color.FromBgra(0xFFFFFAF0);
        /// <summary>
        /// ForestGreen color.
        /// </summary>
        public static readonly Color ForestGreen = Color.FromBgra(0xFF228B22);
        /// <summary>
        /// Fuchsia color.
        /// </summary>
        public static readonly Color Fuchsia = Color.FromBgra(0xFFFF00FF);
        /// <summary>
        /// Gainsboro color.
        /// </summary>
        public static readonly Color Gainsboro = Color.FromBgra(0xFFDCDCDC);
        /// <summary>
        /// GhostWhite color.
        /// </summary>
        public static readonly Color GhostWhite = Color.FromBgra(0xFFF8F8FF);
        /// <summary>
        /// Gold color.
        /// </summary>
        public static readonly Color Gold = Color.FromBgra(0xFFFFD700);
        /// <summary>
        /// Goldenrod color.
        /// </summary>
        public static readonly Color Goldenrod = Color.FromBgra(0xFFDAA520);
        /// <summary>
        /// Gray color.
        /// </summary>
        public static readonly Color Gray = Color.FromBgra(0xFF808080);
        /// <summary>
        /// Green color.
        /// </summary>
        public static readonly Color Green = Color.FromBgra(0xFF008000);
        /// <summary>
        /// GreenYellow color.
        /// </summary>
        public static readonly Color GreenYellow = Color.FromBgra(0xFFADFF2F);
        /// <summary>
        /// Honeydew color.
        /// </summary>
        public static readonly Color Honeydew = Color.FromBgra(0xFFF0FFF0);
        /// <summary>
        /// HotPink color.
        /// </summary>
        public static readonly Color HotPink = Color.FromBgra(0xFFFF69B4);
        /// <summary>
        /// IndianRed color.
        /// </summary>
        public static readonly Color IndianRed = Color.FromBgra(0xFFCD5C5C);
        /// <summary>
        /// Indigo color.
        /// </summary>
        public static readonly Color Indigo = Color.FromBgra(0xFF4B0082);
        /// <summary>
        /// Ivory color.
        /// </summary>
        public static readonly Color Ivory = Color.FromBgra(0xFFFFFFF0);
        /// <summary>
        /// Khaki color.
        /// </summary>
        public static readonly Color Khaki = Color.FromBgra(0xFFF0E68C);
        /// <summary>
        /// Lavender color.
        /// </summary>
        public static readonly Color Lavender = Color.FromBgra(0xFFE6E6FA);
        /// <summary>
        /// LavenderBlush color.
        /// </summary>
        public static readonly Color LavenderBlush = Color.FromBgra(0xFFFFF0F5);
        /// <summary>
        /// LawnGreen color.
        /// </summary>
        public static readonly Color LawnGreen = Color.FromBgra(0xFF7CFC00);
        /// <summary>
        /// LemonChiffon color.
        /// </summary>
        public static readonly Color LemonChiffon = Color.FromBgra(0xFFFFFACD);
        /// <summary>
        /// LightBlue color.
        /// </summary>
        public static readonly Color LightBlue = Color.FromBgra(0xFFADD8E6);
        /// <summary>
        /// LightCoral color.
        /// </summary>
        public static readonly Color LightCoral = Color.FromBgra(0xFFF08080);
        /// <summary>
        /// LightCyan color.
        /// </summary>
        public static readonly Color LightCyan = Color.FromBgra(0xFFE0FFFF);
        /// <summary>
        /// LightGoldenrodYellow color.
        /// </summary>
        public static readonly Color LightGoldenrodYellow = Color.FromBgra(0xFFFAFAD2);
        /// <summary>
        /// LightGray color.
        /// </summary>
        public static readonly Color LightGray = Color.FromBgra(0xFFD3D3D3);
        /// <summary>
        /// LightGreen color.
        /// </summary>
        public static readonly Color LightGreen = Color.FromBgra(0xFF90EE90);
        /// <summary>
        /// LightPink color.
        /// </summary>
        public static readonly Color LightPink = Color.FromBgra(0xFFFFB6C1);
        /// <summary>
        /// LightSalmon color.
        /// </summary>
        public static readonly Color LightSalmon = Color.FromBgra(0xFFFFA07A);
        /// <summary>
        /// LightSeaGreen color.
        /// </summary>
        public static readonly Color LightSeaGreen = Color.FromBgra(0xFF20B2AA);
        /// <summary>
        /// LightSkyBlue color.
        /// </summary>
        public static readonly Color LightSkyBlue = Color.FromBgra(0xFF87CEFA);
        /// <summary>
        /// LightSlateGray color.
        /// </summary>
        public static readonly Color LightSlateGray = Color.FromBgra(0xFF778899);
        /// <summary>
        /// LightSteelBlue color.
        /// </summary>
        public static readonly Color LightSteelBlue = Color.FromBgra(0xFFB0C4DE);
        /// <summary>
        /// LightYellow color.
        /// </summary>
        public static readonly Color LightYellow = Color.FromBgra(0xFFFFFFE0);
        /// <summary>
        /// Lime color.
        /// </summary>
        public static readonly Color Lime = Color.FromBgra(0xFF00FF00);
        /// <summary>
        /// LimeGreen color.
        /// </summary>
        public static readonly Color LimeGreen = Color.FromBgra(0xFF32CD32);
        /// <summary>
        /// Linen color.
        /// </summary>
        public static readonly Color Linen = Color.FromBgra(0xFFFAF0E6);
        /// <summary>
        /// Magenta color.
        /// </summary>
        public static readonly Color Magenta = Color.FromBgra(0xFFFF00FF);
        /// <summary>
        /// Maroon color.
        /// </summary>
        public static readonly Color Maroon = Color.FromBgra(0xFF800000);
        /// <summary>
        /// MediumAquamarine color.
        /// </summary>
        public static readonly Color MediumAquamarine = Color.FromBgra(0xFF66CDAA);
        /// <summary>
        /// MediumBlue color.
        /// </summary>
        public static readonly Color MediumBlue = Color.FromBgra(0xFF0000CD);
        /// <summary>
        /// MediumOrchid color.
        /// </summary>
        public static readonly Color MediumOrchid = Color.FromBgra(0xFFBA55D3);
        /// <summary>
        /// MediumPurple color.
        /// </summary>
        public static readonly Color MediumPurple = Color.FromBgra(0xFF9370DB);
        /// <summary>
        /// MediumSeaGreen color.
        /// </summary>
        public static readonly Color MediumSeaGreen = Color.FromBgra(0xFF3CB371);
        /// <summary>
        /// MediumSlateBlue color.
        /// </summary>
        public static readonly Color MediumSlateBlue = Color.FromBgra(0xFF7B68EE);
        /// <summary>
        /// MediumSpringGreen color.
        /// </summary>
        public static readonly Color MediumSpringGreen = Color.FromBgra(0xFF00FA9A);
        /// <summary>
        /// MediumTurquoise color.
        /// </summary>
        public static readonly Color MediumTurquoise = Color.FromBgra(0xFF48D1CC);
        /// <summary>
        /// MediumVioletRed color.
        /// </summary>
        public static readonly Color MediumVioletRed = Color.FromBgra(0xFFC71585);
        /// <summary>
        /// MidnightBlue color.
        /// </summary>
        public static readonly Color MidnightBlue = Color.FromBgra(0xFF191970);
        /// <summary>
        /// MintCream color.
        /// </summary>
        public static readonly Color MintCream = Color.FromBgra(0xFFF5FFFA);
        /// <summary>
        /// MistyRose color.
        /// </summary>
        public static readonly Color MistyRose = Color.FromBgra(0xFFFFE4E1);
        /// <summary>
        /// Moccasin color.
        /// </summary>
        public static readonly Color Moccasin = Color.FromBgra(0xFFFFE4B5);
        /// <summary>
        /// NavajoWhite color.
        /// </summary>
        public static readonly Color NavajoWhite = Color.FromBgra(0xFFFFDEAD);
        /// <summary>
        /// Navy color.
        /// </summary>
        public static readonly Color Navy = Color.FromBgra(0xFF000080);
        /// <summary>
        /// OldLace color.
        /// </summary>
        public static readonly Color OldLace = Color.FromBgra(0xFFFDF5E6);
        /// <summary>
        /// Olive color.
        /// </summary>
        public static readonly Color Olive = Color.FromBgra(0xFF808000);
        /// <summary>
        /// OliveDrab color.
        /// </summary>
        public static readonly Color OliveDrab = Color.FromBgra(0xFF6B8E23);
        /// <summary>
        /// Orange color.
        /// </summary>
        public static readonly Color Orange = Color.FromBgra(0xFFFFA500);
        /// <summary>
        /// OrangeRed color.
        /// </summary>
        public static readonly Color OrangeRed = Color.FromBgra(0xFFFF4500);
        /// <summary>
        /// Orchid color.
        /// </summary>
        public static readonly Color Orchid = Color.FromBgra(0xFFDA70D6);
        /// <summary>
        /// PaleGoldenrod color.
        /// </summary>
        public static readonly Color PaleGoldenrod = Color.FromBgra(0xFFEEE8AA);
        /// <summary>
        /// PaleGreen color.
        /// </summary>
        public static readonly Color PaleGreen = Color.FromBgra(0xFF98FB98);
        /// <summary>
        /// PaleTurquoise color.
        /// </summary>
        public static readonly Color PaleTurquoise = Color.FromBgra(0xFFAFEEEE);
        /// <summary>
        /// PaleVioletRed color.
        /// </summary>
        public static readonly Color PaleVioletRed = Color.FromBgra(0xFFDB7093);
        /// <summary>
        /// PapayaWhip color.
        /// </summary>
        public static readonly Color PapayaWhip = Color.FromBgra(0xFFFFEFD5);
        /// <summary>
        /// PeachPuff color.
        /// </summary>
        public static readonly Color PeachPuff = Color.FromBgra(0xFFFFDAB9);
        /// <summary>
        /// Peru color.
        /// </summary>
        public static readonly Color Peru = Color.FromBgra(0xFFCD853F);
        /// <summary>
        /// Pink color.
        /// </summary>
        public static readonly Color Pink = Color.FromBgra(0xFFFFC0CB);
        /// <summary>
        /// Plum color.
        /// </summary>
        public static readonly Color Plum = Color.FromBgra(0xFFDDA0DD);
        /// <summary>
        /// PowderBlue color.
        /// </summary>
        public static readonly Color PowderBlue = Color.FromBgra(0xFFB0E0E6);
        /// <summary>
        /// Purple color.
        /// </summary>
        public static readonly Color Purple = Color.FromBgra(0xFF800080);
        /// <summary>
        /// Red color.
        /// </summary>
        public static readonly Color Red = Color.FromBgra(0xFFFF0000);
        /// <summary>
        /// RosyBrown color.
        /// </summary>
        public static readonly Color RosyBrown = Color.FromBgra(0xFFBC8F8F);
        /// <summary>
        /// RoyalBlue color.
        /// </summary>
        public static readonly Color RoyalBlue = Color.FromBgra(0xFF4169E1);
        /// <summary>
        /// SaddleBrown color.
        /// </summary>
        public static readonly Color SaddleBrown = Color.FromBgra(0xFF8B4513);
        /// <summary>
        /// Salmon color.
        /// </summary>
        public static readonly Color Salmon = Color.FromBgra(0xFFFA8072);
        /// <summary>
        /// SandyBrown color.
        /// </summary>
        public static readonly Color SandyBrown = Color.FromBgra(0xFFF4A460);
        /// <summary>
        /// SeaGreen color.
        /// </summary>
        public static readonly Color SeaGreen = Color.FromBgra(0xFF2E8B57);
        /// <summary>
        /// SeaShell color.
        /// </summary>
        public static readonly Color SeaShell = Color.FromBgra(0xFFFFF5EE);
        /// <summary>
        /// Sienna color.
        /// </summary>
        public static readonly Color Sienna = Color.FromBgra(0xFFA0522D);
        /// <summary>
        /// Silver color.
        /// </summary>
        public static readonly Color Silver = Color.FromBgra(0xFFC0C0C0);
        /// <summary>
        /// SkyBlue color.
        /// </summary>
        public static readonly Color SkyBlue = Color.FromBgra(0xFF87CEEB);
        /// <summary>
        /// SlateBlue color.
        /// </summary>
        public static readonly Color SlateBlue = Color.FromBgra(0xFF6A5ACD);
        /// <summary>
        /// SlateGray color.
        /// </summary>
        public static readonly Color SlateGray = Color.FromBgra(0xFF708090);
        /// <summary>
        /// Snow color.
        /// </summary>
        public static readonly Color Snow = Color.FromBgra(0xFFFFFAFA);
        /// <summary>
        /// SpringGreen color.
        /// </summary>
        public static readonly Color SpringGreen = Color.FromBgra(0xFF00FF7F);
        /// <summary>
        /// SteelBlue color.
        /// </summary>
        public static readonly Color SteelBlue = Color.FromBgra(0xFF4682B4);
        /// <summary>
        /// Tan color.
        /// </summary>
        public static readonly Color Tan = Color.FromBgra(0xFFD2B48C);
        /// <summary>
        /// Teal color.
        /// </summary>
        public static readonly Color Teal = Color.FromBgra(0xFF008080);
        /// <summary>
        /// Thistle color.
        /// </summary>
        public static readonly Color Thistle = Color.FromBgra(0xFFD8BFD8);
        /// <summary>
        /// Tomato color.
        /// </summary>
        public static readonly Color Tomato = Color.FromBgra(0xFFFF6347);
        /// <summary>
        /// Turquoise color.
        /// </summary>
        public static readonly Color Turquoise = Color.FromBgra(0xFF40E0D0);
        /// <summary>
        /// Violet color.
        /// </summary>
        public static readonly Color Violet = Color.FromBgra(0xFFEE82EE);
        /// <summary>
        /// Wheat color.
        /// </summary>
        public static readonly Color Wheat = Color.FromBgra(0xFFF5DEB3);
        /// <summary>
        /// White color.
        /// </summary>
        public static readonly Color White = Color.FromBgra(0xFFFFFFFF);
        /// <summary>
        /// WhiteSmoke color.
        /// </summary>
        public static readonly Color WhiteSmoke = Color.FromBgra(0xFFF5F5F5);
        /// <summary>
        /// Yellow color.
        /// </summary>
        public static readonly Color Yellow = Color.FromBgra(0xFFFFFF00);
        /// <summary>
        /// YellowGreen color.
        /// </summary>
        public static readonly Color YellowGreen = Color.FromBgra(0xFF9ACD32);
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color(byte value)
        {
            A = R = G = B = value;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color(float value)
        {
            A = R = G = B = ToByte(value);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color(byte red, byte green, byte blue, byte alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.  Alpha is set to 255.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public Color(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 255;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color(float red, float green, float blue, float alpha)
        {
            R = ToByte(red);
            G = ToByte(green);
            B = ToByte(blue);
            A = ToByte(alpha);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.  Alpha is set to 255.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public Color(float red, float green, float blue)
        {
            R = ToByte(red);
            G = ToByte(green);
            B = ToByte(blue);
            A = 255;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="value">The red, green, blue, and alpha components of the color.</param>
        public Color(Vector4 value)
        {
            R = ToByte(value.X);
            G = ToByte(value.Y);
            B = ToByte(value.Z);
            A = ToByte(value.W);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color(Vector3 value, float alpha)
        {
            R = ToByte(value.X);
            G = ToByte(value.Y);
            B = ToByte(value.Z);
            A = ToByte(alpha);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct. Alpha is set to 255.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        public Color(Vector3 value)
        {
            R = ToByte(value.X);
            G = ToByte(value.Y);
            B = ToByte(value.Z);
            A = 255;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
        public Color(uint rgba)
        {
            A = (byte)((rgba >> 24) & 255);
            B = (byte)((rgba >> 16) & 255);
            G = (byte)((rgba >> 8) & 255);
            R = (byte)(rgba & 255);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
        public Color(int rgba)
        {
            A = (byte)((rgba >> 24) & 255);
            B = (byte)((rgba >> 16) & 255);
            G = (byte)((rgba >> 8) & 255);
            R = (byte)(rgba & 255);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="values">The values to assign to the red, green, and blue, alpha components of the color. This must be an array with four elements.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than four elements.</exception>
        public Color(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(values), "There must be four and only four input values for Color.");
            R = ToByte(values[0]);
            G = ToByte(values[1]);
            B = ToByte(values[2]);
            A = ToByte(values[3]);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="values">The values to assign to the alpha, red, green, and blue components of the color. This must be an array with four elements.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than four elements.</exception>
        public Color(byte[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(values), "There must be four and only four input values for Color.");
            R = values[0];
            G = values[1];
            B = values[2];
            A = values[3];
        }
        /// <summary>
        /// Gets or sets the component at the specified index.
        /// </summary>
        /// <value>The value of the alpha, red, green, or blue component, depending on the index.</value>
        /// <param name="index">The index of the component to access. Use 0 for the alpha component, 1 for the red component, 2 for the green component, and 3 for the blue component.</param>
        /// <returns>The value of the component at the specified index.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is out of the range [0, 3].</exception>
        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return R;
                    case 1: return G;
                    case 2: return B;
                    case 3: return A;
                }
                throw new ArgumentOutOfRangeException(nameof(index), "Indices for Color run from 0 to 3, inclusive.");
            }
            set
            {
                switch (index)
                {
                    case 0: R = value; break;
                    case 1: G = value; break;
                    case 2: B = value; break;
                    case 3: A = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Indices for Color run from 0 to 3, inclusive.");
                }
            }
        }
        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public int ToBgra()
        {
            int value = B;
            value |= G << 8;
            value |= R << 16;
            value |= A << 24;
            return value;
        }
        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public int ToRgba()
        {
            int value = R;
            value |= G << 8;
            value |= B << 16;
            value |= A << 24;
            return value;
        }
        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public int ToArgb()
        {
            int value = A;
            value |= R << 8;
            value |= G << 16;
            value |= B << 24;
            return value;
        }
        /// <summary>
        /// Converts the color into a packed integer.
        /// </summary>
        /// <returns>A packed integer containing all four color components.</returns>
        public int ToAbgr()
        {
            int value = A;
            value |= B << 8;
            value |= G << 16;
            value |= R << 24;
            return value;
        }
        /// <summary>
        /// Converts the color into a three component vector.
        /// </summary>
        /// <returns>A three component vector containing the red, green, and blue components of the color.</returns>
        public Vector3 ToVector3()
        {
            return new Vector3(R / 255.0f, G / 255.0f, B / 255.0f);
        }
        /// <summary>
        /// Converts the color into a three component color.
        /// </summary>
        /// <returns>A three component color containing the red, green, and blue components of the color.</returns>
        public Color3 ToColor3()
        {
            return new Color3(R / 255.0f, G / 255.0f, B / 255.0f);
        }
        /// <summary>
        /// Converts the color into a four component vector.
        /// </summary>
        /// <returns>A four component vector containing all four color components.</returns>
        public Vector4 ToVector4()
        {
            return new Vector4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
        }
        /// <summary>
        /// Creates an array containing the elements of the color.
        /// </summary>
        /// <returns>A four-element array containing the components of the color in RGBA order.</returns>
        public byte[] ToArray()
        {
            return new[] { R, G, B, A };
        }
        /// <summary>
        /// Gets the brightness.
        /// </summary>
        /// <returns>The Hue-Saturation-Brightness (HSB) saturation for this <see cref="Color"/></returns>
        public float GetBrightness()
        {
            float r = R / 255.0f;
            float g = G / 255.0f;
            float b = B / 255.0f;
            float max, min;
            max = r; min = r;
            if (g > max) max = g;
            if (b > max) max = b;
            if (g < min) min = g;
            if (b < min) min = b;
            return (max + min) / 2;
        }
        /// <summary>
        /// Gets the hue.
        /// </summary>
        /// <returns>The Hue-Saturation-Brightness (HSB) saturation for this <see cref="Color"/></returns>
        public float GetHue()
        {
            if (R == G && G == B)
                return 0; // 0 makes as good an UNDEFINED value as any
            float r = R / 255.0f;
            float g = G / 255.0f;
            float b = B / 255.0f;
            float max, min;
            float delta;
            float hue = 0.0f;
            max = r; min = r;
            if (g > max) max = g;
            if (b > max) max = b;
            if (g < min) min = g;
            if (b < min) min = b;
            delta = max - min;
            if (r == max)
            {
                hue = (g - b) / delta;
            }
            else if (g == max)
            {
                hue = 2 + (b - r) / delta;
            }
            else if (b == max)
            {
                hue = 4 + (r - g) / delta;
            }
            hue *= 60;
            if (hue < 0.0f)
            {
                hue += 360.0f;
            }
            return hue;
        }
        /// <summary>
        /// Gets the saturation.
        /// </summary>
        /// <returns>The Hue-Saturation-Brightness (HSB) saturation for this <see cref="Color"/></returns>
        public float GetSaturation()
        {
            float r = R / 255.0f;
            float g = G / 255.0f;
            float b = B / 255.0f;
            float max, min;
            float l, s = 0;
            max = r; min = r;
            if (g > max) max = g;
            if (b > max) max = b;
            if (g < min) min = g;
            if (b < min) min = b;
            // if max == min, then there is no color and
            // the saturation is zero.
            //
            if (max != min)
            {
                l = (max + min) / 2;
                if (l <= .5)
                {
                    s = (max - min) / (max + min);
                }
                else
                {
                    s = (max - min) / (2 - max - min);
                }
            }
            return s;
        }
        /// <summary>
        /// Adds two colors.
        /// </summary>
        /// <param name="left">The first color to add.</param>
        /// <param name="right">The second color to add.</param>
        /// <param name="result">When the method completes, completes the sum of the two colors.</param>
        public static void Add(ref Color left, ref Color right, out Color result)
        {
            result.A = (byte)(left.A + right.A);
            result.R = (byte)(left.R + right.R);
            result.G = (byte)(left.G + right.G);
            result.B = (byte)(left.B + right.B);
        }
        /// <summary>
        /// Adds two colors.
        /// </summary>
        /// <param name="left">The first color to add.</param>
        /// <param name="right">The second color to add.</param>
        /// <returns>The sum of the two colors.</returns>
        public static Color Add(Color left, Color right)
        {
            return new Color(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
        }
        /// <summary>
        /// Subtracts two colors.
        /// </summary>
        /// <param name="left">The first color to subtract.</param>
        /// <param name="right">The second color to subtract.</param>
        /// <param name="result">WHen the method completes, contains the difference of the two colors.</param>
        public static void Subtract(ref Color left, ref Color right, out Color result)
        {
            result.A = (byte)(left.A - right.A);
            result.R = (byte)(left.R - right.R);
            result.G = (byte)(left.G - right.G);
            result.B = (byte)(left.B - right.B);
        }
        /// <summary>
        /// Subtracts two colors.
        /// </summary>
        /// <param name="left">The first color to subtract.</param>
        /// <param name="right">The second color to subtract</param>
        /// <returns>The difference of the two colors.</returns>
        public static Color Subtract(Color left, Color right)
        {
            return new Color(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
        }
        /// <summary>
        /// Modulates two colors.
        /// </summary>
        /// <param name="left">The first color to modulate.</param>
        /// <param name="right">The second color to modulate.</param>
        /// <param name="result">When the method completes, contains the modulated color.</param>
        public static void Modulate(ref Color left, ref Color right, out Color result)
        {
            result.A = (byte)(left.A * right.A / 255.0f);
            result.R = (byte)(left.R * right.R / 255.0f);
            result.G = (byte)(left.G * right.G / 255.0f);
            result.B = (byte)(left.B * right.B / 255.0f);
        }
        /// <summary>
        /// Modulates two colors.
        /// </summary>
        /// <param name="left">The first color to modulate.</param>
        /// <param name="right">The second color to modulate.</param>
        /// <returns>The modulated color.</returns>
        public static Color Modulate(Color left, Color right)
        {
            return new Color(left.R * right.R, left.G * right.G, left.B * right.B, left.A * right.A);
        }
        /// <summary>
        /// Scales a color.
        /// </summary>
        /// <param name="value">The color to scale.</param>
        /// <param name="scale">The amount by which to scale.</param>
        /// <param name="result">When the method completes, contains the scaled color.</param>
        public static void Scale(ref Color value, float scale, out Color result)
        {
            result.A = (byte)(value.A * scale);
            result.R = (byte)(value.R * scale);
            result.G = (byte)(value.G * scale);
            result.B = (byte)(value.B * scale);
        }
        /// <summary>
        /// Scales a color.
        /// </summary>
        /// <param name="value">The color to scale.</param>
        /// <param name="scale">The amount by which to scale.</param>
        /// <returns>The scaled color.</returns>
        public static Color Scale(Color value, float scale)
        {
            return new Color((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }
        /// <summary>
        /// Negates a color.
        /// </summary>
        /// <param name="value">The color to negate.</param>
        /// <param name="result">When the method completes, contains the negated color.</param>
        public static void Negate(ref Color value, out Color result)
        {
            result.A = (byte)(255 - value.A);
            result.R = (byte)(255 - value.R);
            result.G = (byte)(255 - value.G);
            result.B = (byte)(255 - value.B);
        }
        /// <summary>
        /// Negates a color.
        /// </summary>
        /// <param name="value">The color to negate.</param>
        /// <returns>The negated color.</returns>
        public static Color Negate(Color value)
        {
            return new Color(255 - value.R, 255 - value.G, 255 - value.B, 255 - value.A);
        }
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <param name="result">When the method completes, contains the clamped value.</param>
        public static void Clamp(ref Color value, ref Color min, ref Color max, out Color result)
        {
            byte alpha = value.A;
            alpha = (alpha > max.A) ? max.A : alpha;
            alpha = (alpha < min.A) ? min.A : alpha;
            byte red = value.R;
            red = (red > max.R) ? max.R : red;
            red = (red < min.R) ? min.R : red;
            byte green = value.G;
            green = (green > max.G) ? max.G : green;
            green = (green < min.G) ? min.G : green;
            byte blue = value.B;
            blue = (blue > max.B) ? max.B : blue;
            blue = (blue < min.B) ? min.B : blue;
            result = new Color(red, green, blue, alpha);
        }
        /// <summary>
        /// Converts the color from a packed BGRA integer.
        /// </summary>
        /// <param name="color">A packed integer containing all four color components in BGRA order</param>
        /// <returns>A color.</returns>
        public static Color FromBgra(int color)
        {
            return new Color((byte)((color >> 16) & 255), (byte)((color >> 8) & 255), (byte)(color & 255), (byte)((color >> 24) & 255));
        }
        /// <summary>
        /// Converts the color from a packed BGRA integer.
        /// </summary>
        /// <param name="color">A packed integer containing all four color components in BGRA order</param>
        /// <returns>A color.</returns>
        public static Color FromBgra(uint color)
        {
            return FromBgra(unchecked((int)color));
        }
        /// <summary>
        /// Converts the color from a packed ABGR integer.
        /// </summary>
        /// <param name="color">A packed integer containing all four color components in ABGR order</param>
        /// <returns>A color.</returns>
        public static Color FromAbgr(int color)
        {
            return new Color((byte)(color >> 24), (byte)(color >> 16), (byte)(color >> 8), (byte)color);
        }
        /// <summary>
        /// Converts the color from a packed ABGR integer.
        /// </summary>
        /// <param name="color">A packed integer containing all four color components in ABGR order</param>
        /// <returns>A color.</returns>
        public static Color FromAbgr(uint color)
        {
            return FromAbgr(unchecked((int)color));
        }
        /// <summary>
        /// Converts the color from a packed RGBA integer.
        /// </summary>
        /// <param name="color">A packed integer containing all four color components in RGBA order</param>
        /// <returns>A color.</returns>
        public static Color FromRgba(int color)
        {
            return new Color(color);
        }
        /// <summary>
        /// Converts the color from a packed RGBA integer.
        /// </summary>
        /// <param name="color">A packed integer containing all four color components in RGBA order</param>
        /// <returns>A color.</returns>
        public static Color FromRgba(uint color)
        {
            return new Color(color);
        }
        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The clamped value.</returns>
        public static Color Clamp(Color value, Color min, Color max)
        {
            Clamp(ref value, ref min, ref max, out Color result);
            return result;
        }
        /// <summary>
        /// Performs a linear interpolation between two colors.
        /// </summary>
        /// <param name="start">Start color.</param>
        /// <param name="end">End color.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
        /// <param name="result">When the method completes, contains the linear interpolation of the two colors.</param>
        /// <remarks>
        /// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned. 
        /// </remarks>
        public static void Lerp(ref Color start, ref Color end, float amount, out Color result)
        {
            result.R = MathUtility.Lerp(start.R, end.R, amount);
            result.G = MathUtility.Lerp(start.G, end.G, amount);
            result.B = MathUtility.Lerp(start.B, end.B, amount);
            result.A = MathUtility.Lerp(start.A, end.A, amount);
        }
        /// <summary>
        /// Performs a linear interpolation between two colors.
        /// </summary>
        /// <param name="start">Start color.</param>
        /// <param name="end">End color.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
        /// <returns>The linear interpolation of the two colors.</returns>
        /// <remarks>
        /// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned. 
        /// </remarks>
        public static Color Lerp(Color start, Color end, float amount)
        {
            Lerp(ref start, ref end, amount, out Color result);
            return result;
        }
        /// <summary>
        /// Performs a cubic interpolation between two colors.
        /// </summary>
        /// <param name="start">Start color.</param>
        /// <param name="end">End color.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
        /// <param name="result">When the method completes, contains the cubic interpolation of the two colors.</param>
        public static void SmoothStep(ref Color start, ref Color end, float amount, out Color result)
        {
            amount = MathUtility.SmoothStep(amount);
            Lerp(ref start, ref end, amount, out result);
        }
        /// <summary>
        /// Performs a cubic interpolation between two colors.
        /// </summary>
        /// <param name="start">Start color.</param>
        /// <param name="end">End color.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
        /// <returns>The cubic interpolation of the two colors.</returns>
        public static Color SmoothStep(Color start, Color end, float amount)
        {
            SmoothStep(ref start, ref end, amount, out Color result);
            return result;
        }
        /// <summary>
        /// Returns a color containing the smallest components of the specified colors.
        /// </summary>
        /// <param name="left">The first source color.</param>
        /// <param name="right">The second source color.</param>
        /// <param name="result">When the method completes, contains an new color composed of the largest components of the source colors.</param>
        public static void Max(ref Color left, ref Color right, out Color result)
        {
            result.A = (left.A > right.A) ? left.A : right.A;
            result.R = (left.R > right.R) ? left.R : right.R;
            result.G = (left.G > right.G) ? left.G : right.G;
            result.B = (left.B > right.B) ? left.B : right.B;
        }
        /// <summary>
        /// Returns a color containing the largest components of the specified colorss.
        /// </summary>
        /// <param name="left">The first source color.</param>
        /// <param name="right">The second source color.</param>
        /// <returns>A color containing the largest components of the source colors.</returns>
        public static Color Max(Color left, Color right)
        {
            Max(ref left, ref right, out Color result);
            return result;
        }
        /// <summary>
        /// Returns a color containing the smallest components of the specified colors.
        /// </summary>
        /// <param name="left">The first source color.</param>
        /// <param name="right">The second source color.</param>
        /// <param name="result">When the method completes, contains an new color composed of the smallest components of the source colors.</param>
        public static void Min(ref Color left, ref Color right, out Color result)
        {
            result.A = (left.A < right.A) ? left.A : right.A;
            result.R = (left.R < right.R) ? left.R : right.R;
            result.G = (left.G < right.G) ? left.G : right.G;
            result.B = (left.B < right.B) ? left.B : right.B;
        }
        /// <summary>
        /// Returns a color containing the smallest components of the specified colors.
        /// </summary>
        /// <param name="left">The first source color.</param>
        /// <param name="right">The second source color.</param>
        /// <returns>A color containing the smallest components of the source colors.</returns>
        public static Color Min(Color left, Color right)
        {
            Min(ref left, ref right, out Color result);
            return result;
        }
        /// <summary>
        /// Adjusts the contrast of a color.
        /// </summary>
        /// <param name="value">The color whose contrast is to be adjusted.</param>
        /// <param name="contrast">The amount by which to adjust the contrast.</param>
        /// <param name="result">When the method completes, contains the adjusted color.</param>
        public static void AdjustContrast(ref Color value, float contrast, out Color result)
        {
            result.A = value.A;
            result.R = ToByte(0.5f + contrast * (value.R / 255.0f - 0.5f));
            result.G = ToByte(0.5f + contrast * (value.G / 255.0f - 0.5f));
            result.B = ToByte(0.5f + contrast * (value.B / 255.0f - 0.5f));
        }
        /// <summary>
        /// Adjusts the contrast of a color.
        /// </summary>
        /// <param name="value">The color whose contrast is to be adjusted.</param>
        /// <param name="contrast">The amount by which to adjust the contrast.</param>
        /// <returns>The adjusted color.</returns>
        public static Color AdjustContrast(Color value, float contrast)
        {
            return new Color(
                ToByte(0.5f + contrast * (value.R / 255.0f - 0.5f)),
                ToByte(0.5f + contrast * (value.G / 255.0f - 0.5f)),
                ToByte(0.5f + contrast * (value.B / 255.0f - 0.5f)),
                value.A);
        }
        /// <summary>
        /// Adjusts the saturation of a color.
        /// </summary>
        /// <param name="value">The color whose saturation is to be adjusted.</param>
        /// <param name="saturation">The amount by which to adjust the saturation.</param>
        /// <param name="result">When the method completes, contains the adjusted color.</param>
        public static void AdjustSaturation(ref Color value, float saturation, out Color result)
        {
            float grey = value.R / 255.0f * 0.2125f + value.G / 255.0f * 0.7154f + value.B / 255.0f * 0.0721f;
            result.A = value.A;
            result.R = ToByte(grey + saturation * (value.R / 255.0f - grey));
            result.G = ToByte(grey + saturation * (value.G / 255.0f - grey));
            result.B = ToByte(grey + saturation * (value.B / 255.0f - grey));
        }
        /// <summary>
        /// Adjusts the saturation of a color.
        /// </summary>
        /// <param name="value">The color whose saturation is to be adjusted.</param>
        /// <param name="saturation">The amount by which to adjust the saturation.</param>
        /// <returns>The adjusted color.</returns>
        public static Color AdjustSaturation(Color value, float saturation)
        {
            float grey = value.R / 255.0f * 0.2125f + value.G / 255.0f * 0.7154f + value.B / 255.0f * 0.0721f;
            return new Color(
                ToByte(grey + saturation * (value.R / 255.0f - grey)),
                ToByte(grey + saturation * (value.G / 255.0f - grey)),
                ToByte(grey + saturation * (value.B / 255.0f - grey)),
                value.A);
        }
        /// <summary>
        /// Adds two colors.
        /// </summary>
        /// <param name="left">The first color to add.</param>
        /// <param name="right">The second color to add.</param>
        /// <returns>The sum of the two colors.</returns>
        public static Color operator +(Color left, Color right)
        {
            return new Color(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
        }
        /// <summary>
        /// Assert a color (return it unchanged).
        /// </summary>
        /// <param name="value">The color to assert (unchanged).</param>
        /// <returns>The asserted (unchanged) color.</returns>
        public static Color operator +(Color value)
        {
            return value;
        }
        /// <summary>
        /// Subtracts two colors.
        /// </summary>
        /// <param name="left">The first color to subtract.</param>
        /// <param name="right">The second color to subtract.</param>
        /// <returns>The difference of the two colors.</returns>
        public static Color operator -(Color left, Color right)
        {
            return new Color(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
        }
        /// <summary>
        /// Negates a color.
        /// </summary>
        /// <param name="value">The color to negate.</param>
        /// <returns>A negated color.</returns>
        public static Color operator -(Color value)
        {
            return new Color(-value.R, -value.G, -value.B, -value.A);
        }
        /// <summary>
        /// Scales a color.
        /// </summary>
        /// <param name="scale">The factor by which to scale the color.</param>
        /// <param name="value">The color to scale.</param>
        /// <returns>The scaled color.</returns>
        public static Color operator *(float scale, Color value)
        {
            return new Color((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }
        /// <summary>
        /// Scales a color.
        /// </summary>
        /// <param name="value">The factor by which to scale the color.</param>
        /// <param name="scale">The color to scale.</param>
        /// <returns>The scaled color.</returns>
        public static Color operator *(Color value, float scale)
        {
            return new Color((byte)(value.R * scale), (byte)(value.G * scale), (byte)(value.B * scale), (byte)(value.A * scale));
        }
        /// <summary>
        /// Modulates two colors.
        /// </summary>
        /// <param name="left">The first color to modulate.</param>
        /// <param name="right">The second color to modulate.</param>
        /// <returns>The modulated color.</returns>
        public static Color operator *(Color left, Color right)
        {
            return new Color((byte)(left.R * right.R / 255.0f), (byte)(left.G * right.G / 255.0f), (byte)(left.B * right.B / 255.0f), (byte)(left.A * right.A / 255.0f));
        }
        /// <summary>
        /// Tests for equality between two objects.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Color left, Color right)
        {
            return left.Equals(right);
        }
        /// <summary>
        /// Tests for inequality between two objects.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Color left, Color right)
        {
            return !left.Equals(right);
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Color"/> to <see cref="Color3"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color3(Color value)
        {
            return value.ToColor3();
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Color"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector3(Color value)
        {
            return new Vector3(value.R / 255.0f, value.G / 255.0f, value.B / 255.0f);
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Color"/> to <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Vector4(Color value)
        {
            return new Vector4(value.R / 255.0f, value.G / 255.0f, value.B / 255.0f, value.A / 255.0f);
        }
        /// <summary>
        /// Convert this instance to a <see cref="Color4"/>
        /// </summary>
        /// <returns>The result of the conversion.</returns>
        public Color4 ToColor4()
        {
            return new Color4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
        }
        /// <summary>
        /// Performs an implicit conversion from <see cref="Color"/> to <see cref="Color4"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Color4(Color value)
        {
            return value.ToColor4();
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector3"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color(Vector3 value)
        {
            return new Color(value.X, value.Y, value.Z, 1.0f);
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Color3"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color(Color3 value)
        {
            return new Color(value.R, value.G, value.B, 1.0f);
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Vector4"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color(Vector4 value)
        {
            return new Color(value.X, value.Y, value.Z, value.W);
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="Color4"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator Color(Color4 value)
        {
            return new Color(value.R, value.G, value.B, value.A);
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="System.Int32"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator int(Color value)
        {
            return value.ToRgba();
        }
        /// <summary>
        /// Performs an explicit conversion from <see cref="System.Int32"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator Color(int value)
        {
            return new Color(value);
        }
        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ColorExtensions.RgbaToString(ToRgba());
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return A.GetHashCode() + R.GetHashCode() + G.GetHashCode() + B.GetHashCode();
        }
        /// <summary>
        /// Determines whether the specified <see cref="Color"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The <see cref="Color"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Color"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Color other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }
        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="value">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object value)
        {
            if (value == null)
                return false;
            if (!ReferenceEquals(value.GetType(), typeof(Color)))
                return false;
            return Equals((Color)value);
        }
        private static byte ToByte(float component)
        {
            var value = (int)(component * 255.0f);
            return (byte)(value < 0 ? 0 : value > 255 ? 255 : value);
        }
    }
}