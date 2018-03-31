using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.UI
{
    public abstract class BaseView : IView
    {
        public GameObject gameObject
        {
            get;
            set;
        }

        protected Dictionary<string, IMediator> mediators;

        public BaseView(GameObject gameObject)
        {
            Init(gameObject);
        }

        public virtual void Dispose()
        {
            
        }

        public void HandleNotification(IUINotification notification)
        {
            IMediator mediator;
            if (mediators.TryGetValue(notification.name, out mediator))
                mediator.HandleNotification(notification);
        }

        public virtual void Init(GameObject gameObject)
        {
            mediators = new Dictionary<string, IMediator>();
        }

        public virtual void OnApplicationQuit()
        {
            Dispose();
        }

        public virtual void OnDestroy()
        {
            Dispose();
        }

        public void RegisterMediator(string mediatorName, IMediator mediator,bool isCover = false)
        {
            if (!mediators.ContainsKey(mediatorName))
                mediators.Add(mediatorName, mediator);
            else if (mediators.ContainsKey(mediatorName) && isCover)
                mediators[mediatorName] = mediator;
            else
            {
                //LogError
            }
        }

        public void RemoveMediator(string mediatorName)
        {
            if (mediators.ContainsKey(mediatorName))
                mediators.Remove(mediatorName);
        }

        public void Update()
        {
            
        }
    }
}