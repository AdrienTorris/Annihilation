using System;
using System.Numerics;
using Engine.Mathematics;

namespace Engine
{
    public class Camera
    {
        public Matrix4x4 ViewMatrix { get; protected set; }
        public Matrix4x4 ProjectionMatrix { get; protected set; }
        
    }
}