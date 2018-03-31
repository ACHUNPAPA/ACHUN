using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.UI
{
    public abstract class BaseMediator : IMediator
    {
        public string mediatorName
        {
            get;
            protected set;
        }

        public virtual void HandleNotification(IUINotification notification)
        {
            
        }

        public virtual void Init()
        {

        }

        public virtual void OnApplicationQuit()
        {

        }

        public virtual void OnDestroy()
        {

        }

        public virtual void Update()
        {

        }
    }
}