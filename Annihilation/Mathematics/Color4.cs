using System;
using System.Numerics;

namespace Engine.Mathematics
{
    public struct Color4
    {
        /// <summary>
        /// The red component of the color.
        /// </summary>
        public float R;
        /// <summary>
        /// The green component of the color.
        /// </summary>
        public float G;
        /// <summary>
        /// The blue component of the color.
        /// </summary>
        public float B;
        /// <summary>
        /// The alpha component of the color.
        /// </summary>
        public float A;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color4(float value)
        {
            A = R = G = B = value;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(float red, float green, float blue, float alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, blue, and alpha components of the color.</param>
        public Color4(Vector4 value)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = value.W;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(Vector3 value, float alpha)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
            A = alpha;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
        public Color4(uint rgba)
        {
            A = ((rgba >> 24) & 255) / 255.0f;
            B = ((rgba >> 16) & 255) / 255.0f;
            G = ((rgba >> 8) & 255) / 255.0f;
            R = (rgba & 255) / 255.0f;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="rgba">A packed integer containing all four color components in RGBA order.</param>
        public Color4(int rgba)
        {
            A = ((rgba >> 24) & 255) / 255.0f;
            B = ((rgba >> 16) & 255) / 255.0f;
            G = ((rgba >> 8) & 255) / 255.0f;
            R = (rgba & 255) / 255.0f;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="values">The values to assign to the red, green, blue, and alpha components of the color. This must be an array with four elements.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than four elements.</exception>
        public Color4(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(values), "There must be four and only four input values for Color4.");
            R = values[0];
            G = values[1];
            B = values[2];
            A = values[3];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="color"><see cref="Color3"/> used to initialize the color.</param>
        public Color4(Color3 color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = 1.0f;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="color"><see cref="Color"/> used to initialize the color.</param>
        public Color4(Color color)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
            A = color.A / 255.0f;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color4"/> struct.
        /// </summary>
        /// <param name="color"><see cref="Color3"/> used to initialize the color.</param>
        /// <param name="alpha">The alpha component of the color.</param>
        public Color4(Color3 color, float alpha)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = alpha;
        }
    }
}