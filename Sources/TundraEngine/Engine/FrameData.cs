using System.Numerics;

namespace Engine
{
    public struct FrameData
    {
        public int FrameNumber;
        public double DeltaTime;
        public double LogicStart;
        public double LogicEnd;
        public double RenderStart;
        public double RenderEnd;
        public double GPUStart;
        public double GPUEnd;
        public double PresentStart;
        public double PresentEnd;
        public Matrix4x4[] Matrices;
        public Vector3[] Vertices;
    }
}