using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Event
{
    public interface INotifier
    {
        void SendNotification(string facadeName,INotification notification);
    }
}