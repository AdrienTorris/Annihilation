using System;
using System.Collections.Generic;

namespace Engine
{
    public class ManagerBase<T>
    {
        // Entities
        private static uint[] _generations;
        private static Queue<uint> _freeIndexQueue;
        private const int MinFreeIndices = 1024;

        // Components
        private static Dictionary<byte, ComponentManager> _componentManagerMap = new Dictionary<byte, ComponentManager> ();
        
        protected ManagerBase (int entityCount)
        {

        }
    }
}