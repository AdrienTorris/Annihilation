using System;
using System.Collections.Generic;
using System.Text;

namespace TundraEngine.MessagePack
{
    public interface IMessagePackSerializationCallbackReceiver
    {
        void OnBeforeSerialize();
        void OnAfterDeserialize();
    }
}
