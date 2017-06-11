namespace TundraEngine.Mathematics
{
    public static class VectorExtensions
    {
        public static Vector2 YX(this Vector2 vector)
        {
            return new Vector2(vector.Y, vector.X);
        }

        public static Vector2 XY(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector2 XZ(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Z);
        }

        public static Vector2 YZ(this Vector3 vector)
        {
            return new Vector2(vector.Y, vector.Z);
        }

        public static Vector2 XY(this Vector4 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector3 XYZ(this Vector4 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
    }
}