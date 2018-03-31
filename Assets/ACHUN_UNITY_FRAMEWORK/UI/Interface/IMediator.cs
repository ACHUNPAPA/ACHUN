using System.Collections;
using System.Collections.Generic;
using AChun.Event;

namespace AChun.UI
{
    public interface IMediator
    {        

        void HandleNotification(IUINotification notification);

        void Init();


        void Update();


        void OnDestroy();


        void OnApplicationQuit();
    }
}
