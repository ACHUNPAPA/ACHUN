using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AChun.Net
{
    public enum SocketIOMessageTypes : sbyte
    {
        Dissconnect = 0,
        Connect = 1,
        HeartBeat = 3,
        Message = 4,
        JSONMessage = 5,
        Event = 5,
        ACK = 6,
        Error = 7,
        Noop = 8,
    }
}
