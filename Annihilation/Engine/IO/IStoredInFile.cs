namespace Engine.IO
{
    public interface IConfigData
    {
        void GetConfigFields(out string category, out ConfigField[] fields);
        
    }
}