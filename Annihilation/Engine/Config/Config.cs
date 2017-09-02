using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Engine.Config
{
    public static class Config
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

        private const int ApproximateCharsPerLine = 24;

        private static readonly Dictionary<ConfigVarType, string> _typeStrings = new Dictionary<ConfigVarType, string>
        {
            [ConfigVarType.Uint8] = TypeUint8,
            [ConfigVarType.Uint16] = TypeUint16,
            [ConfigVarType.Uint32] = TypeUint32,
            [ConfigVarType.Uint64] = TypeUint64,
            [ConfigVarType.Int8] = TypeInt8,
            [ConfigVarType.Int16] = TypeInt16,
            [ConfigVarType.Int32] = TypeInt32,
            [ConfigVarType.Int64] = TypeInt64,
            [ConfigVarType.Float] = TypeFloat,
            [ConfigVarType.Double] = TypeDouble,
            [ConfigVarType.Bool] = TypeBool,
            [ConfigVarType.String] = TypeString,
        };

        private static readonly List<string> _varsToWrite = new List<string>(128);
        private static readonly Dictionary<string, ConfigVar> _vars = new Dictionary<string, ConfigVar>(128);

        public static void AddVar(string name, byte value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, ushort value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, uint value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, ulong value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, sbyte value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, short value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, int value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, long value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, float value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, double value = 0, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, bool value = false, ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }
        public static void AddVar(string name, string value = "", ConfigVarFlags flags = ConfigVarFlags.None)
        {
            if ((flags & ConfigVarFlags.WriteToFile) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new ConfigVar(value));
        }

        public static ConfigVar GetVar(string name) => _vars[name];

        public static bool TryGetVar(string name, out ConfigVar value)
        {
            if (_vars.TryGetValue(name, out value))
            {
                return true;
            }
            return false;
        }

        public static void SetVar(string name, byte value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, ushort value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, uint value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, ulong value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, sbyte value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, short value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, int value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, long value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, float value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, double value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, bool value) => _vars[name] = new ConfigVar(value);
        public static void SetVar(string name, string value) => _vars[name] = new ConfigVar(value);

        public static void WriteVarsToFile(string path)
        {
            if (_varsToWrite.Count == 0)
            {
                return;
            }

            StringBuilder stringBuilder = new StringBuilder(_varsToWrite.Count * ApproximateCharsPerLine);

            for (int i = 0; i < _varsToWrite.Count; i++)
            {
                string name = _varsToWrite[i];
                ConfigVar value = _vars[name];

                if (i > 0)
                {
                    stringBuilder.AppendLine();
                }

                stringBuilder.Append(name).Append(':').Append(_typeStrings[value.Type]).Append('=').Append(value);
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(stringBuilder.ToString());
            }
        }

        public static void AddVarsFromFile(string path)
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
                    
                    if (line[0] == '/' ||   // Skip comment
                        line[0] == '[' ||   // Skip category
                        line.Length == 0)   // Skip empty line
                    {
                        continue;
                    }

                    // Read the data
                    int colonIndex = line.IndexOf(':');
                    int equalIndex = line.IndexOf('=');
                    string key = line.Substring(0, colonIndex);

                    Assert.IsFalse(_vars.ContainsKey(key), $"{key} was already added to the config.");

                    string type = line.Substring(colonIndex + 1, equalIndex - colonIndex - 1);
                    string value = line.Substring(equalIndex + 1);

                    // Parse the data
                    switch (type)
                    {
                        case TypeUint8:
                        {
                            if (byte.TryParse(value, out byte result))
                            {
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
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
                                AddVar(key, result, ConfigVarFlags.WriteToFile);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to bool");
                            }

                            break;
                        }
                        case TypeString:
                        {
                            AddVar(key, value, ConfigVarFlags.WriteToFile);

                            break;
                        }
                    }
                }
            }
        }
    }
}