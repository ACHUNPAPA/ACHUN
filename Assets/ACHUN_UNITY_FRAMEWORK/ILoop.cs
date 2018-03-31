using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace AChun
{
    public interface ILoop
    {
        void InitGame();


        void LoopGame();


        void OnApplicationPause();


        void OnApplicationQuit();
    }
}
