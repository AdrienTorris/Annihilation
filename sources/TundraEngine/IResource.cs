namespace TundraEngine
{
    public interface IResource
    {
        StringId32 Name { get; }
        byte[] Bytes { get; }
    }
}