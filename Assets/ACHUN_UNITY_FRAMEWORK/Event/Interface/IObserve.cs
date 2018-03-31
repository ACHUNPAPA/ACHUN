using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Event
{
    public interface IObserve
    {
        void HandleNotification(INotification notification);
    }
}
