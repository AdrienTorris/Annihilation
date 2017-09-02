namespace Engine.Config
{
    public class ConfigFileEntry
    {
        public string Key;
        public string Type;
        public string Value;

        public ConfigFileEntry(string key, string type, string value)
        {
            Key = key;
            Type = type;
            Value = value;
        }
    }
}