using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.UI
{
    public abstract class BaseModel : IModel
    {
        protected Dictionary<string, IProxy> proxys;

        public BaseModel()
        {
            Init();
        }

        public IProxy GetDataObject(string proxyName)
        {
            IProxy proxy;
            if (proxys.TryGetValue(proxyName, out proxy))
                return proxy;
            //LogError
            return null;
        }

        public virtual void Init()
        {
            proxys = new Dictionary<string, IProxy>();
        }

        public virtual void OnApplicationQuit()
        {
            foreach (IProxy proxy in proxys.Values)
                proxy.OnApplicationQuit();
            proxys.Clear();
            proxys = null;
        }

        public virtual void OnDestroy()
        {
            foreach (IProxy proxy in proxys.Values)
                proxy.OnDestroy();
            proxys.Clear();
            proxys = null;
        }

        public void RegisterDataProxy(string proxyName, IProxy proxy)
        {
            if (proxys.ContainsKey(proxyName))
                return;
            proxys.Add(proxyName,proxy);
        }

        public void RemoveDataProxy(string proxyName)
        {
            IProxy proxy;
            if (proxys.TryGetValue(proxyName, out proxy))
            {
                proxy.Dispose();
                proxys.Remove(proxyName);
            }
        }

        public virtual void Update()
        {
            
        }
    }
}