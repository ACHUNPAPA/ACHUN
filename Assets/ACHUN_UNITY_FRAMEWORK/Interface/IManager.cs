using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun
{
    public interface IManager
    {
        void Init();

        void Update();

        void OnDestroy();


        void OnApplicationPause();

        void OnApplicationQuit();
    }
}