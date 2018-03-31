using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AChun.Net
{
    public sealed class ACKMessage : Message
    {
        private static Regex reAckId = new Regex(@"^(\d{1,})");
        private static Regex reAckPayload = new Regex(@"(?:[\d\+]*)(?<data>.*)$");
        private static Regex reAckComlex = new Regex(@"^\[(?<payload>.*)\]$");

        private static object acklock = new object();
        private static int _ackid = 0;
        public static int NextAckID
        {
            get
            {
                lock (acklock)
                {
                    _ackid++;
                    if (_ackid < 0)
                        _ackid = 0;
                    return _ackid;
                }
            }
        }

        public Action Callback;

        public ACKMessage() : base()
        {
            MessageType = SocketIOMessageTypes.ACK;
        }


        public static ACKMessage Deserialize(string rawMessage)
        {
            ACKMessage msg = new ACKMessage();
            msg.RawMessage = rawMessage;

            string[] args = rawMessage.Split(SPLITCHARS,4);
            if (args.Length == 4)
            {
                msg.EndPoint = args[2];
                int id;
                string[] parts = args[3].Split(new char[] { '+'});
                if (parts.Length > 1)
                {
                    if (int.TryParse(parts[0], out id))
                    {
                        msg.AckId = id;
                        msg.MessageText = parts[1];
                        Match payloadMatch = reAckComlex.Match(msg.MessageText);

                        if (payloadMatch.Success)
                        {
                            msg.Json = new JsonEncodedEventMessage();
                            msg.Json.args = new string[] { payloadMatch.Groups["patload"].Value};
                        }
                    }
                }
            }
            return msg;
        }


        public override string Encoded
        {
            get
            {
                int msgId = (int)MessageType;
                if (AckId.HasValue)
                {
                    if (Callback == null)
                        return string.Format("{0}:{1}:{2}:{3}",msgId,AckId ?? -1,EndPoint,MessageText);
                    else
                        return string.Format("{0}:{1}+:{2}:{3}", msgId, AckId ?? -1, EndPoint,MessageText);
                }
                return string.Format("{0}::{1}:{2}", msgId, EndPoint, MessageText);
            }
        }
    }
}
