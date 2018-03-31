using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AChun.Net
{
    public interface IEndPointClient
    {
        void On(string eventName,Action<IMessage> action);

        void Emit(string eventName,Object payload,Action<Object> callback);

        void Send(IMessage msg);
    }
}
