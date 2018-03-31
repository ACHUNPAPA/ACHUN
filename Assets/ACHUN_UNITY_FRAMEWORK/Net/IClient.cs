using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSocket4Net;

namespace AChun.Net
{
    public interface IClient
    {
        event EventHandler Opened;
        event EventHandler<MessageEventArgs> Message;
        event EventHandler SocketConnectionClosed;
        event EventHandler<ErrorMessageArgs> Error;

        SocketIOHandshake HandShake
        {
            get;
        }

        bool IsConnected
        {
            get;
        }

        WebSocketState ReadyState
        {
            get;
        }

        void Connect();


        IEndPointClient Connect(string endPoint);


        void Close();


        void Dispose();


        void On(string eventName,Action<IMessage> action);


        void On(string eventName,string endPoint,Action<IMessage> action);


        void Emit(string eventName,Object payload);


        void Emit(string eventName,Object payload,string endPoint,Action<Object> callback);


        void Send(IMessage msg);
    }
}
