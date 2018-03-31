using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Event
{
    public interface INotification
    {
        string name
        {
            get;
            set;
        }

        object[] body
        {
            get;
            set;
        }
    }
}
