namespace Annihilation.Vulkan
{
    public unsafe struct Surface
    {
        private SurfaceHandle _handle;
        private Instance _instance;

        public SurfaceHandle Handle => _handle;

        public Surface(SurfaceHandle handle, Instance instance)
        {
            _handle = handle;
            _instance = instance;
        }

        public void Destroy()
        {
            _instance.DestroySurface(_handle);
        }
    }
}