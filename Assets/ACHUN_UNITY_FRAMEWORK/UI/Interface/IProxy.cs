using System.Collections;
using System.Collections.Generic;
using AChun.Data;

namespace AChun.UI
{
    public interface IProxy
    {      
        void Init();


        void Update();


        void OnDestroy();


        void OnApplicationQuit();

        void Dispose();
    }
}