using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Engine.Config
{
    public unsafe class ConfigFile
    {
        public const string TypeUint8 = "uint8";
        public const string TypeUint16 = "uint16";
        public const string TypeUint32 = "uint32";
        public const string TypeUint64 = "uint64";
        public const string TypeInt8 = "int8";
        public const string TypeInt16 = "int16";
        public const string TypeInt32 = "int32";
        public const string TypeInt64 = "int64";
        public const string TypeFloat = "float";
        public const string TypeDouble = "double";
        public const string TypeBool = "bool";
        public const string TypeString = "string";

        private string _path;
        private IConfigData _data;

        private Dictionary<string, ConfigValue> _values = new Dictionary<string, ConfigValue>();
        
        public ConfigFile(string path, IConfigData data)
        {
            _path = path;
            _data = data;
        }

        public ConfigValue GetValue(string key)
        {
            return _values[key];
        }
        
        public bool TryGetValue(string key, out ConfigValue value)
        {
            if (_values.TryGetValue(key, out value))
            {
                return true;
            }
            return false;
        }

        public void Write()
        {
            // Get the category and field strings for the data
            _data.GetConfigFields(out string category, out ConfigFileEntry[] fields);

            // Append the category
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('[').Append(category).Append(']');

            // Append the fields (key:type=value)
            foreach (ConfigFileEntry field in fields)
            {
                stringBuilder.AppendLine();
                stringBuilder.Append(field.Key).Append(':').Append(field.Type).Append('=').Append(field.Value);
            }

            // Write to the file
            using (StreamWriter writer = new StreamWriter(_path))
            {
                writer.Write(stringBuilder.ToString());
            }
        }

        public void Read()
        {
            _values.Clear();

            string line;
            using (StreamReader reader = new StreamReader(_path))
            {
                while (true)
                {
                    line = reader.ReadLine();

                    if (line == null ||     // Reached end of file
                        line[0] == '/' ||   // Skip comment
                        line[0] == '[' ||   // Skip category
                        line.Length == 0)   // Skip empty line
                    {
                        break;
                    }

                    // Read the data
                    int colonIndex = line.IndexOf(':');
                    int equalIndex = line.IndexOf('=');
                    string key = line.Substring(0, colonIndex);
                    string type = line.Substring(colonIndex + 1, equalIndex - colonIndex - 1);
                    string value = line.Substring(equalIndex + 1);

                    // Parse the data
                    switch (type)
                    {
                        case TypeUint8:
                        {
                            if (byte.TryParse(value, out byte result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to byte.");
                            }

                            break;
                        }
                        case TypeUint16:
                        {
                            if (ushort.TryParse(value, out ushort result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to ushort");
                            }

                            break;
                        }
                        case TypeUint32:
                        {
                            if (uint.TryParse(value, out uint result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to uint");
                            }

                            break;
                        }
                        case TypeUint64:
                        {
                            if (ulong.TryParse(value, out ulong result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to ulong");
                            }

                            break;
                        }
                        case TypeInt8:
                        {
                            if (sbyte.TryParse(value, out sbyte result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to sbyte.");
                            }

                            break;
                        }
                        case TypeInt16:
                        {
                            if (short.TryParse(value, out short result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to short");
                            }

                            break;
                        }
                        case TypeInt32:
                        {
                            if (int.TryParse(value, out int result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to int");
                            }

                            break;
                        }
                        case TypeInt64:
                        {
                            if (long.TryParse(value, out long result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to long");
                            }

                            break;
                        }
                        case TypeFloat:
                        {
                            if (float.TryParse(value, out float result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to float");
                            }

                            break;
                        }
                        case TypeDouble:
                        {
                            if (double.TryParse(value, out double result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to double");
                            }

                            break;
                        }
                        case TypeBool:
                        {
                            if (bool.TryParse(value, out bool result))
                            {
                                _values.Add(key, new ConfigValue(result));
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to bool");
                            }

                            break;
                        }
                        case TypeString:
                        {
                            _values.Add(key, new ConfigValue(value));

                            break;
                        }
                    }
                }
            }
        }
    }
}