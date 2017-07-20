using System.Numerics;

using TundraEngine.Rendering.Vulkan;

namespace TundraEngine.Rendering
{
    public struct Vertex
    {
        public Vector2 Position;
        public Vector3 Color;

        public static readonly VertexInputBindingDescription BindingDescription = new VertexInputBindingDescription
        {
            Binding = 0,
            Stride = sizeof(float) * 2 * 3,
            InputRate = VertexInputRate.Vertex
        };

        public static readonly VertexInputAttributeDescription[] AttributeDescriptions = new VertexInputAttributeDescription[]
        {
            new VertexInputAttributeDescription
            {
                Binding = 0,
                Location = 0,
                Format = Format.R32G32SFloat,
                Offset = 0
            },
            new VertexInputAttributeDescription
            {
                Binding = 0,
                Location = 1,
                Format = Format.R32G32B32SFloat,
                Offset = sizeof(float) * 2
            }
        };
    }
}