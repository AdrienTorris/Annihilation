using System;
using System.Threading;

namespace Vulkan.MemoryAllocator
{
    public static partial class Vma
    {
        public class MutexLock
        {
            private object _locker;

            public MutexLock(bool useMutex)
            {
                _locker = useMutex ? new object() : null;
                if (_locker != null)
                {
                    Monitor.Enter(_locker);
                }
            }

            public void Unlock()
            {
                Monitor.Exit(_locker);
            }
        }
    }
}