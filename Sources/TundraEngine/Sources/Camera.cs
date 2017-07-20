using System;
using System.Numerics;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    public class Camera
    {
        public Matrix4x4 ViewMatrix { get; protected set; }
        public Matrix4x4 ProjectionMatrix { get; protected set; }
        
    }
}