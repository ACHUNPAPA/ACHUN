using System.Collections;
using System.Collections.Generic;
using AChun.Event;
using UnityEngine;

namespace AChun.UI
{
    public interface IView
    {
        GameObject gameObject
        {
            get;
            set;
        }  

        void RegisterMediator(string mediatorName,IMediator mediator,bool isCover);

        void RemoveMediator(string mediatorName);

        void HandleNotification(IUINotification notification);

        void Init(GameObject gameObject);


        void Update();


        void OnDestroy();


        void OnApplicationQuit();

        void Dispose();
    }
}
