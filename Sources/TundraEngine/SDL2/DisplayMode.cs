namespace SDL2
{
    public unsafe struct DisplayMode
    {
        public uint Format;
        public int Width;
        public int Height;
        public int RefreshRate;
        public void* DriverData;
    }
}