using System.Collections;
using System.Collections.Generic;

namespace AChun.UI
{
    public interface IModel
    {
        void RegisterDataProxy(string proxyName, IProxy proxy);

        void RemoveDataProxy(string proxyName);

        IProxy GetDataObject(string proxyName);

        void Init();


        void Update();


        void OnDestroy();


        void OnApplicationQuit();
    }
}
