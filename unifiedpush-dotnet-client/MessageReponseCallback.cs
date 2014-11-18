using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroGear
{
    public interface MessageResponseCallback
    {
        void OnComplete(int statusCode);
        void OnError(Exception thrown);
    }
}
