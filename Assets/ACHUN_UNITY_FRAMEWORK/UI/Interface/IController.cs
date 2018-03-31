using System.Collections;
using System.Collections.Generic;
using AChun.Event;

namespace AChun.UI
{
    public interface IController
    {
        void RegisterCommond(string commondName,ICommond commond,bool isCover);

        void RemoveCommond(string commondName);

        void ExcuteCommond(INotification notification);

        void Init();


        void Update();


        void OnDestroy();


        void OnApplicationQuit();
    }
}
