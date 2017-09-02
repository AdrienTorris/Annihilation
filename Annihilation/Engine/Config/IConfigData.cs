namespace Engine.Config
{
    public interface IConfigData
    {
        void GetConfigFields(out string category, out ConfigFileEntry[] fields);
    }
}