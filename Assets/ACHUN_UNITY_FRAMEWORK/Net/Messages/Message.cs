using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AChun.Net
{
    public abstract class Message : IMessage
    {
        private static Regex re = new Regex(@"\d:\d:\w?:");
        public static char[] SPLITCHARS = new char[] { ':'};

        public SocketIOMessageTypes MessageType
        {
            get;
            protected set;
        }

        public string RawMessage
        {
            get;
            protected set;
        }

        public virtual string Event
        {
            get;
            set;
        }

        public int? AckId
        {
            get;
            protected set;
        }

        public string EndPoint
        {
            get;
            set;
        }

        public string MessageText
        {
            get;
            set;
        }


        private JsonEncodedEventMessage _json;

        public JsonEncodedEventMessage JsonEncodeMessage
        {
            get
            {
                return Json;
            }
            set
            {
                _json = value;
            }
        }


        public JsonEncodedEventMessage Json
        {
            get
            {
                if (_json == null)
                {
                    if (!string.IsNullOrEmpty(MessageText) && MessageText.Contains("name") && MessageText.Contains("args"))
                        _json = JsonEncodedEventMessage.Deserialize(MessageText);
                    else
                        _json = new JsonEncodedEventMessage();
                }
                return _json;
            }
            set
            {
                _json = value;
            }
        }

        public virtual string Encoded
        {
            get
            {
                int msgId = (int)MessageType;
                if (AckId.HasValue)
                    return string.Format("{0}:{1}:{2}:{3}", msgId, AckId ?? -1, EndPoint, MessageText);
                else
                    return string.Format("{0}::{1}:{2}",msgId,EndPoint,MessageText);
            }
        }


        public Message()
        {
            MessageType = SocketIOMessageTypes.Message;
        }

        public Message(string rawMessage) : this()
        {
            RawMessage = rawMessage;
            string[] args = rawMessage.Split(SPLITCHARS,4);
            if (args.Length == 4)
            {
                int id;
                if (int.TryParse(args[1], out id))
                    AckId = id;
                EndPoint = args[2];
                MessageText = args[3];
            }
        }


        public static Regex reMessageType = new Regex("^[0-8]{1}:",RegexOptions.IgnoreCase);


        public static IMessage Factory(string rawMessage)
        {
            if (reMessageType.IsMatch(rawMessage))
            {
                char id = rawMessage.First();
                switch (id)
                {
                    case '0':
                        break;
                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case '4':
                        //return JsonMessage.Deserialize(rawMessage);
                        break;
                    case '5':
                        break;
                    case '6':
                        break;
                    case '7':
                        break;
                    case '8':
                        break;
                    default:
                        Trace.WriteLine(string.Format("Message.Factory undetermined message: {0}",rawMessage));
                        break;
                }
            }
            else
            {
                Trace.WriteLine(string.Format("Message.Factory did not find matching message type: {0}",rawMessage));
                //return new NoopMessage();
            }
            return null;
        }
    }
}
