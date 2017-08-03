namespace Engine
{
    public struct Int3
    {
        public int X;
        public int Y;
        public int Z;

        public Int3 (int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Int3 (int value)
        {
            X = Y = Z = value;
        }
    }
}