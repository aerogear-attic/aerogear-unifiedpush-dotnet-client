using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear
{
    interface Sender
    {
        void Send(UnifiedMessage message, MessageResponseCallback callback);
        void Send(UnifiedMessage message);
    }
}
