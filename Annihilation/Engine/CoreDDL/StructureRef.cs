using System.Collections.Generic;

namespace ODDL
{
    public class StructureRef
    {
        public List<string> Names { get; private set; } = new List<string>(1);
        public bool IsGlobal { get; private set; }

        public StructureRef(bool isGlobal = true)
        {
            IsGlobal = isGlobal;
        }

        public void AddName(string name)
        {
            Names.Add(name);
        }

        public void Reset(bool isGlobal = true)
        {
            Names.Clear();
            IsGlobal = isGlobal;
        }
    }
}