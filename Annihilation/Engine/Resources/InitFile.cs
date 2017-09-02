using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace Engine.IO
{
    public class InitField
    {
        public string Key;
        public string Type;
        public string Value;

        public InitField(string key, string type, string value)
        {
            Key = key;
            Type = type;
            Value = value;
        }
    }

    public unsafe class InitFile
    {
        public const string TypeUint8 = "uint8";
        public const string TypeUint16 = "uint16";
        public const string TypeUint32 = "uint32";
        public const string TypeUint64 = "uint64";
        public const string TypeInt8 = "int8";
        public const string TypeInt16 = "int16";
        public const string TypeInt32 = "int32";
        public const string TypeInt64 = "int64";
        public const string TypeBool = "bool";
        public const string TypeFloat = "float";
        public const string TypeDouble = "double";
        public const string TypeString = "string";
        
        public static void Write(IInitFileData data, string path)
        {
            // Get the category and field strings for the data
            data.GetInitFields(out string category, out InitField[] fields);

            // Append the category
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('[').Append(category).Append(']');

            // Append the fields (key:type=value)
            foreach (InitField field in fields)
            {
                stringBuilder.AppendLine();
                stringBuilder.Append(field.Key).Append(':').Append(field.Type).Append('=').Append(field.Value);
            }

            // Write to the file
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(stringBuilder.ToString());
            }
        }

        public static void Read(string path)
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