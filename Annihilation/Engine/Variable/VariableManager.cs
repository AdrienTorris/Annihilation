using System.IO;
using System.Text;
using System.Collections.Generic;
using Engine.Mathematics;

namespace Engine
{
    public static unsafe class VariableSystem
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

        private static readonly Dictionary<ValueType, string> _typeStrings = new Dictionary<ValueType, string>
        {
            [ValueType.Uint8] = TypeUint8,
            [ValueType.Uint16] = TypeUint16,
            [ValueType.Uint32] = TypeUint32,
            [ValueType.Uint64] = TypeUint64,
            [ValueType.Int8] = TypeInt8,
            [ValueType.Int16] = TypeInt16,
            [ValueType.Int32] = TypeInt32,
            [ValueType.Int64] = TypeInt64,
            [ValueType.Float] = TypeFloat,
            [ValueType.Double] = TypeDouble,
            [ValueType.Bool] = TypeBool,
            [ValueType.String] = TypeString,
        };
        
        private static readonly Dictionary<Hash, Variable> _variables = new Dictionary<Hash, Variable>(128);
        
        public static void Initialize()
        {

        }

        public static void Update(float deltaTime)
        {

        }

        public static Variable* Find(Hash nameHash)
        {
            if (_variables.TryGetValue(nameHash, out Variable variable))
            {
                return &variable;
            }
            return null;
        }

        public static Variable* Find(char* name)
        {
            return Find(new Hash(name));
        }

        public static Variable* Find(string name)
        {
            return Find(new Hash(name));
        }

        public static string GetValueString(char* name)
        {
            Variable* var = Find(name);
            if (var == null) return "";
            return new string(var->ValueString);
        }

        public static string CompleteVariable(char* partial)
        {
            int length = StringUtility.GetLength(partial);
            if (length == 0) return null;

            for (Variable* var = _variables; var != null; var = var->Next)
            {
                if (!StringUtility.Compare(partial, var->Name, length))
                {
                    return new string(var->Name);
                }
            }
            return null;
        }

        public static void Reset(char* name)
        {
            Variable* var = Find(name);
            if (var == null)
            {
                Log.Warning($"Variable {new string(name)} not found.");
            }
            else
            {
                Set(var, var->DefaultValueString);
            }
        }

        public static void Set(char* name, char* value)
        {
            Variable* var = Find(name);
            if (var == null)
            {
                Log.Warning($"Variable {new string(name)} not found.");
                return;
            }

            Set(var, value);
        }

        public static void Set(string name, bool value)
        {
            fixed (char* ptr = name)
            {

            }
        }

        public static void Set(Variable* var, bool value)
        {
            char* valueString = value ? Memory.AllocateChars(4) : Memory.AllocateChars(5);
            *(valueString + 0) = value ? 't' : 'f';
            *(valueString + 1) = value ? 'r' : 'a';
            *(valueString + 2) = value ? 'u' : 'l';
            *(valueString + 3) = value ? 'e' : 's';
            if (!value) *(valueString + 4) = 'e';

            var->Value = new Value(value);
            Set(var, valueString);
        }

        public static void Set(Variable* var, int value)
        {
            var->Value = new Value(value);
            Set(var, MathUtility.ToChars(value));
        }

        public static void Set(Variable* var, float value)
        {
            var->Value = new Value(value);
            Set(var, MathUtility.ToChars(value));
        }

        public static void Set(Variable* var, string value)
        {
            var->Value = new Value(value);
            fixed (char* ptr = value)
            {
                Set(var, ptr);
            }
        }

        public static void Set(Variable* var, char* valueString)
        {
            if ((var->Flags & VariableFlags.Registered) == 0)
            {
                return;
            }

            if (var->ValueString == null)
            {
                // TODO: Need to duplicate memory?
                var->ValueString = StringUtility.Duplicate(valueString);
            }
            else
            {
                if (StringUtility.AreEqual(var->ValueString, valueString))
                {
                    return;
                }

                int length = StringUtility.GetLength(valueString);
                // Different lengths, realloc the string
                if (length != StringUtility.GetLength(var->ValueString))
                {
                    Memory.Free(var->ValueString);
                    //
                    // ALLOCATION
                    //
                    var->ValueString = Memory.AllocateChars(length + 1);
                }
                Memory.Copy(var->ValueString, valueString, length + 1);
            }
            
            if (var->Value.Type == ValueType.Unknown)
            {
                ValueType type = StringUtility.GetType(var->ValueString);
                switch (type)
                {
                    case ValueType.Bool:
                    {
                        var->Value = new Value(StringUtility.ToBool(var->ValueString));
                        break;
                    }
                    case ValueType.Int32:
                    {
                        var->Value = new Value(StringUtility.ToInt(var->ValueString));
                        break;
                    }
                    case ValueType.Float:
                    {
                        var->Value = new Value(StringUtility.ToFloat(var->ValueString));
                        break;
                    }
                    case ValueType.String:
                    {
                        var->Value = new Value(var->ValueString);
                        break;
                    }
                }
            }

            if (var->DefaultValueString == null)
            {
                var->DefaultValueString = var->ValueString;
            }

            // TODO: How do we fill the actions on game restart?
            //var->Callback();
        }

        public static void Register(Variable* variable)
        {

        }

        public static void AddVar(string name, byte value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, ushort value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, uint value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, ulong value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, sbyte value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, short value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, int value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, long value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, float value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, double value = 0, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, bool value = false, VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }
        public static void AddVar(string name, string value = "", VariableFlags flags = VariableFlags.None)
        {
            if ((flags & VariableFlags.Archive) != 0)
            {
                _varsToWrite.Add(name);
            }
            _vars.Add(name, new Value(value));
        }

        // PERF: Remove dictionary lookup. How?
        public static Value GetVar(string name) => _vars[name];

        public static byte GetVar(string name, byte defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Uint8, "Trying to get an byte value for var of type " + value.Type);
                return value.Uint8;
            }
            return defaultValue;
        }

        public static ushort GetVar(string name, ushort defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Uint16, "Trying to get an ushort value for var of type " + value.Type);
                return value.Uint16;
            }
            return defaultValue;
        }

        public static uint GetVar(string name, uint defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Uint32, "Trying to get an uint value for var of type " + value.Type);
                return value.Uint32;
            }
            return defaultValue;
        }

        public static ulong GetVar(string name, ulong defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Uint64, "Trying to get an ulong value for var of type " + value.Type);
                return value.Uint64;
            }
            return defaultValue;
        }

        public static sbyte GetVar(string name, sbyte defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Int8, "Trying to get an sbyte value for var of type " + value.Type);
                return value.Int8;
            }
            return defaultValue;
        }

        public static short GetVar(string name, short defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Int16, "Trying to get a short value for var of type " + value.Type);
                return value.Int16;
            }
            return defaultValue;
        }

        public static int GetVar(string name, int defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Int32, "Trying to get an int value for var of type " + value.Type);
                return value.Int32;
            }
            return defaultValue;
        }

        public static long GetVar(string name, long defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Int64, "Trying to get a long value for var of type " + value.Type);
                return value.Int64;
            }
            return defaultValue;
        }

        public static float GetVar(string name, float defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Float, "Trying to get a float value for var of type " + value.Type);
                return value.Float;
            }
            return defaultValue;
        }

        public static double GetVar(string name, double defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Double, "Trying to get a double value for var of type " + value.Type);
                return value.Double;
            }
            return defaultValue;
        }

        public static bool GetVar(string name, bool defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.Bool, "Trying to get a bool value for var of type " + value.Type);
                return value.Bool;
            }
            return defaultValue;
        }

        public static unsafe string GetVar(string name, string defaultValue)
        {
            if (_vars.TryGetValue(name, out Value value))
            {
                Assert.IsTrue(value.Type == ValueType.String, "Trying to get a string value for var of type " + value.Type);
                return new string(value.String);
            }
            return defaultValue;
        }

        public static bool TryGetVar(string name, out Value value)
        {
            if (_vars.TryGetValue(name, out value))
            {
                return true;
            }
            return false;
        }

        public static void SetVar(string name, byte value) => _vars[name] = new Value(value);
        public static void SetVar(string name, ushort value) => _vars[name] = new Value(value);
        public static void SetVar(string name, uint value) => _vars[name] = new Value(value);
        public static void SetVar(string name, ulong value) => _vars[name] = new Value(value);
        public static void SetVar(string name, sbyte value) => _vars[name] = new Value(value);
        public static void SetVar(string name, short value) => _vars[name] = new Value(value);
        public static void SetVar(string name, int value) => _vars[name] = new Value(value);
        public static void SetVar(string name, long value) => _vars[name] = new Value(value);
        public static void SetVar(string name, float value) => _vars[name] = new Value(value);
        public static void SetVar(string name, double value) => _vars[name] = new Value(value);
        public static void SetVar(string name, bool value) => _vars[name] = new Value(value);
        public static void SetVar(string name, string value) => _vars[name] = new Value(value);

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
                Value value = _vars[name];

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

        // PERF: Use char* marching instead of readlines
        public static bool AddVarsFromFile(string path)
        {
            if (File.Exists(path) == false)
            {
                return false;
            }

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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
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
                                AddVar(key, result, VariableFlags.Archive);
                            }
                            else
                            {
                                Log.Warning($"Could not parse value {value} to bool");
                            }

                            break;
                        }
                        case TypeString:
                        {
                            AddVar(key, value, VariableFlags.Archive);

                            break;
                        }
                    }
                }
            }

            return true;
        }
    }
}