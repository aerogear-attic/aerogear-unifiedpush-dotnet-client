using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroGear
{
    interface Sender
    {
        Task Send(UnifiedMessage message, MessageResponseCallback callback);
        Task Send(UnifiedMessage message);
    }
}
