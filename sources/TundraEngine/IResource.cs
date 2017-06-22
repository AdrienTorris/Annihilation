namespace TundraEngine
{
    unsafe public interface IResource
    {
        StringId32 Name { get; set; }
        int NumBytes { get; set; }
        byte* Bytes { get; set; }
    }
}