using System;
using System.Collections.Generic;
using System.IO;

namespace Engine.Resources
{
    public unsafe class IniFile
    {
        private Dictionary<string, int> _intMap;
        private Dictionary<string, byte> _byteMap;

        public T TryGetValue<T>(string key, out T value)
        {

        }

        public void Parse(string path, string[] keys)
        {
            string line;
            using (StreamReader reader = new StreamReader(path))
            {
                while (true)
                {
                    line = reader.ReadLine();

                    // Reached end of file
                    if (line == null)
                    {
                        break;
                    }

                    // Skip comment
                    if (line[0] == '/')
                    {
                        break;
                    }

                    // Section/struct
                    if (line[0] == '[')
                    {
                        break;
                    }

                    // Read the data
                    int colonIndex = line.IndexOf(':');
                    int equalIndex = line.IndexOf('=');
                    string key = line.Substring(0, colonIndex);
                    string type = line.Substring(colonIndex + 1, equalIndex - colonIndex - 1);
                    string value = line.Substring(equalIndex + 1);
                }
            }
        }
    }
}