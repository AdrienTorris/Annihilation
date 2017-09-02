using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Engine.IO
{
    public enum ConfigValueType : byte
    {
        Uint8,
        Uint16,
        Uint32,
        Uint64,
        Int8,
        Int16,
        Int32,
        Int64,
        Float,
        Double,
        String
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct ConfigValue
    {
        [FieldOffset(0)] public ConfigValueType Type;
        [FieldOffset(1)] public byte Uint8;
        [FieldOffset(1)] public ushort Uint16;
        [FieldOffset(1)] public uint Uint32;
        [FieldOffset(1)] public ulong Uint64;
        [FieldOffset(1)] public sbyte Int8;
        [FieldOffset(1)] public short Int16;
        [FieldOffset(1)] public int Int32;
        [FieldOffset(1)] public long Int64;
        [FieldOffset(1)] public float Float;
        [FieldOffset(1)] public double Double;
        [FieldOffset(1)] public bool Bool;
        [FieldOffset(1)] public string String;
    }

    public class ConfigField
    {
        public string Key;
        public string Type;
        public string Value;

        public ConfigField(string key, string type, string value)
        {
            Key = key;
            Type = type;
            Value = value;
        }
    }

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

        /*private Dictionary<string, byte> _uint8Map;
        private Dictionary<string, ushort> _uint16Map;
        private Dictionary<string, uint> _uint32Map;
        private Dictionary<string, ulong> _uint64Map;
        private Dictionary<string, sbyte> _int8Map;
        private Dictionary<string, short> _int16Map;
        private Dictionary<string, int> _int32Map;
        private Dictionary<string, long> _int64Map;
        private Dictionary<string, float> _floatMap;
        private Dictionary<string, double> _doubleMap;
        private Dictionary<string, bool> _boolMap;
        private Dictionary<string, string> _stringMap;*/

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
            _data.GetConfigFields(out string category, out ConfigField[] fields);

            // Append the category
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('[').Append(category).Append(']');

            // Append the fields (key:type=value)
            foreach (ConfigField field in fields)
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
                            if (_uint8Map == null)
                            {
                                _uint8Map = new Dictionary<string, byte>();
                            }

                            if (byte.TryParse(value, out byte result))
                            {
                                _uint8Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to byte.");
                            }

                            break;
                        }
                        case TypeUint16:
                        {
                            if (_uint16Map == null)
                            {
                                _uint16Map = new Dictionary<string, ushort>();
                            }

                            if (ushort.TryParse(value, out ushort result))
                            {
                                _uint16Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to ushort");
                            }

                            break;
                        }
                        case TypeUint32:
                        {
                            if (_uint32Map == null)
                            {
                                _uint32Map = new Dictionary<string, uint>();
                            }

                            if (uint.TryParse(value, out uint result))
                            {
                                _uint32Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to uint");
                            }

                            break;
                        }
                        case TypeUint64:
                        {
                            if (_uint64Map == null)
                            {
                                _uint64Map = new Dictionary<string, ulong>();
                            }

                            if (ulong.TryParse(value, out ulong result))
                            {
                                _uint64Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to ulong");
                            }

                            break;
                        }
                        case TypeInt8:
                        {
                            if (_int8Map == null)
                            {
                                _int8Map = new Dictionary<string, sbyte>();
                            }

                            if (sbyte.TryParse(value, out sbyte result))
                            {
                                _int8Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to sbyte.");
                            }

                            break;
                        }
                        case TypeInt16:
                        {
                            if (_int16Map == null)
                            {
                                _int16Map = new Dictionary<string, short>();
                            }

                            if (short.TryParse(value, out short result))
                            {
                                _int16Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to short");
                            }

                            break;
                        }
                        case TypeInt32:
                        {
                            if (_int32Map == null)
                            {
                                _int32Map = new Dictionary<string, int>();
                            }

                            if (int.TryParse(value, out int result))
                            {
                                _int32Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to int");
                            }

                            break;
                        }
                        case TypeInt64:
                        {
                            if (_int64Map == null)
                            {
                                _int64Map = new Dictionary<string, long>();
                            }

                            if (long.TryParse(value, out long result))
                            {
                                _int64Map.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to long");
                            }

                            break;
                        }
                        case TypeFloat:
                        {
                            if (_floatMap == null)
                            {
                                _floatMap = new Dictionary<string, float>();
                            }

                            if (float.TryParse(value, out float result))
                            {
                                _floatMap.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to float");
                            }

                            break;
                        }
                        case TypeDouble:
                        {
                            if (_doubleMap == null)
                            {
                                _doubleMap = new Dictionary<string, double>();
                            }

                            if (double.TryParse(value, out double result))
                            {
                                _doubleMap.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to double");
                            }

                            break;
                        }
                        case TypeBool:
                        {
                            if (_boolMap == null)
                            {
                                _boolMap = new Dictionary<string, bool>();
                            }

                            if (bool.TryParse(value, out bool result))
                            {
                                _boolMap.Add(key, result);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to bool");
                            }

                            break;
                        }
                        case TypeString:
                        {
                            if (_stringMap == null)
                            {
                                _stringMap = new Dictionary<string, string>();
                            }

                            _stringMap.Add(key, value);

                            break;
                        }
                    }
                }
            }
        }

        /*public byte GetUint8(string key)
        {
            Assert.IsFalse(_uint8Map == null);
            if (_uint8Map.TryGetValue(key, out byte value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(byte);
        }

        public ushort GetUint16(string key)
        {
            Assert.IsFalse(_uint16Map == null);
            if (_uint16Map.TryGetValue(key, out ushort value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(ushort);
        }

        public uint GetUint32(string key)
        {
            Assert.IsFalse(_uint32Map == null);
            if (_uint32Map.TryGetValue(key, out uint value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(uint);
        }

        public ulong GetUint64(string key)
        {
            Assert.IsFalse(_uint64Map == null);
            if (_uint64Map.TryGetValue(key, out ulong value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(ulong);
        }

        public sbyte GetInt8(string key)
        {
            Assert.IsFalse(_int8Map == null);
            if (_int8Map.TryGetValue(key, out sbyte value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(sbyte);
        }

        public short GetInt16(string key)
        {
            Assert.IsFalse(_int16Map == null);
            if (_int16Map.TryGetValue(key, out short value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(short);
        }

        public int GetInt32(string key)
        {
            Assert.IsFalse(_int32Map == null);
            if (_int32Map.TryGetValue(key, out int value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(int);
        }

        public long GetInt64(string key)
        {
            Assert.IsFalse(_int64Map == null);
            if (_int64Map.TryGetValue(key, out long value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(long);
        }

        public float GetFloat(string key)
        {
            Assert.IsFalse(_floatMap == null);
            if (_floatMap.TryGetValue(key, out float value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(float);
        }

        public double GetDouble(string key)
        {
            Assert.IsFalse(_doubleMap == null);
            if (_doubleMap.TryGetValue(key, out double value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(double);
        }

        public bool GetBool(string key)
        {
            Assert.IsFalse(_boolMap == null);
            if (_boolMap.TryGetValue(key, out bool value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(bool);
        }

        public string GetString(string key)
        {
            Assert.IsFalse(_stringMap == null);
            if (_stringMap.TryGetValue(key, out string value))
            {
                return value;
            }
            Log.Warning($"Could not find key {key} in file {_path}.");
            return default(string);
        }*/
    }
}