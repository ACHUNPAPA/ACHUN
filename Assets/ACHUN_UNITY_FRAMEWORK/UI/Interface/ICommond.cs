using System.Collections;
using System.Collections.Generic;
using AChun.Event;

namespace AChun.UI
{
    public interface ICommond
    {
        void Excute(INotification notification);
    }
}
