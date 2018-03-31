using System;
using System.Collections;
using System.Collections.Generic;
using AChun.Data;
using UnityEngine;

namespace AChun.UI
{
    public abstract class BaseProxy : IProxy
    {
        protected IDataObject dataObj;

        public string proxyName
        {
            get;
            protected set;
        }

        public virtual void Dispose()
        {
            dataObj = null;
        }

        public virtual void Init()
        {

        }

        public virtual void OnApplicationQuit()
        {
            Dispose();
        }

        public virtual void OnDestroy()
        {
            Dispose();
        }

        public virtual void Update()
        {
            
        }
    }
}