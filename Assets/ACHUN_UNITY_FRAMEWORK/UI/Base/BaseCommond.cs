using System;
using System.Collections;
using System.Collections.Generic;
using AChun.Event;

namespace AChun.UI
{
    public abstract class BaseCommond : ICommond
    {
        public string CommondName
        {
            get;
            protected set; 
        }

        public virtual void Excute(INotification notification)
        {

        }
    }
}