using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AChun.Net
{
    public interface IMessage
    {
        SocketIOMessageTypes MessageType
        {
            get;
        }

        string RawMessage
        {
            get;
        }

        string Event
        {
            get;
        }


        int? AckId
        {
            get;
        }


        string EndPoint
        {
            get;
            set;
        }


        string MessageText
        {
            get;
        }

        JsonEncodedEventMessage Json
        {
            get;
        }


        string Encoded
        {
            get;
        }
    }
}
