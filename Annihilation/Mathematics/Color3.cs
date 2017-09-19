using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Annihilation.Mathematics
{
    public struct Color3
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
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="value">The value that will be assigned to all components.</param>
        public Color3(float value)
        {
            R = G = B = value;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="red">The red component of the color.</param>
        /// <param name="green">The green component of the color.</param>
        /// <param name="blue">The blue component of the color.</param>
        public Color3(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="value">The red, green, and blue components of the color.</param>
        public Color3(Vector3 value)
        {
            R = value.X;
            G = value.Y;
            B = value.Z;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="rgb">A packed integer containing all three color components.
        /// The alpha component is ignored.</param>
        public Color3(int rgb)
        {
            B = ((rgb >> 16) & 255) / 255.0f;
            G = ((rgb >> 8) & 255) / 255.0f;
            R = (rgb & 255) / 255.0f;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="rgb">A packed unsigned integer containing all three color components.
        /// The alpha component is ignored.</param>
        public Color3(uint rgb)
        {
            B = ((rgb >> 16) & 255) / 255.0f;
            G = ((rgb >> 8) & 255) / 255.0f;
            R = (rgb & 255) / 255.0f;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Color3"/> struct.
        /// </summary>
        /// <param name="values">The values to assign to the red, green, and blue components of the color. This must be an array with three elements.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than four elements.</exception>
        public Color3(float[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(values), "There must be three and only three input values for Color3.");
            R = values[0];
            G = values[1];
            B = values[2];
        }
    }
}