using System;
using System.Collections;
using System.Collections.Generic;
using AChun.Event;

namespace AChun.UI
{
    public abstract class BaseController : IController
    {
        protected Dictionary<string, ICommond> commonds;


        public BaseController()
        {
            Init();
        }

        public virtual void ExcuteCommond(INotification notification)
        {
            ICommond commond;
            if (commonds.TryGetValue(notification.name, out commond))
                commond.Excute(notification);
        }

        public virtual void Init()
        {
            commonds = new Dictionary<string, ICommond>();
        }

        public virtual void OnApplicationQuit()
        {
            commonds.Clear();
            commonds = null;
        }

        public virtual void OnDestroy()
        {
            commonds.Clear();
            commonds = null;
        }

        public void RegisterCommond(string commondName, ICommond commond,bool isCover = false)
        {
            if (!commonds.ContainsKey(commondName))
                commonds.Add(commondName, commond);
            else if (commonds.ContainsKey(commondName) && isCover)
                commonds[commondName] = commond;
            else
            {
                //LogError
            }
        }

        public void RemoveCommond(string commondName)
        {
            if (commonds.ContainsKey(commondName))
                commonds.Remove(commondName);
        }

        public virtual void Update()
        {
            
        }
    }
}