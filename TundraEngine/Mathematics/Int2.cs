namespace TundraEngine
{
    public struct Int2
    {
        public readonly int X;
        public readonly int Y;

        public Int2 (int x, int y)
        {
            X = x;
            Y = y;
        }

        public Int2 (int value)
        {
            X = Y = value;
        }
    }
}