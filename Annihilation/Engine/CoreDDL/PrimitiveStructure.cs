using System;

namespace ODDL
{
    /// <summary>
    /// Base class for built-in primitive data structures in an OpenDDL file.
    /// </summary>
    public abstract class PrimitiveStructure : Structure
    {
        public uint ArraySize { get; protected set; }

        protected PrimitiveStructure(uint type)
        {

        }

        public abstract Result ParseData(string text);
    }
}