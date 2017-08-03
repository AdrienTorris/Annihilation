namespace Engine.SDL
{
    /// <summary> The structure that defines a display mode. </summary>
    public unsafe struct DisplayMode
    {
        /// <summary> Pixel format </summary>
        public uint Format;
        public int Width;
        public int Height;
        public int RefreshRate;
        public void* DriverData;
    }
}