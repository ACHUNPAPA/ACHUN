using System.Collections;
using System.Collections.Generic;
using AChun.Event;
using UnityEngine;

namespace AChun.UI
{
    public interface IFacade : IObserve,INotifier
    {
        void Show();

        void Close();

        void Init(GameObject gameObject);        


        void Update();


        void OnDestroy();


        void OnApplicationQuit();
    }
}
