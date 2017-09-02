namespace Engine.IO
{
    public interface IInitFileData
    {
        void GetInitFields(out string category, out InitField[] fields);
    }
}