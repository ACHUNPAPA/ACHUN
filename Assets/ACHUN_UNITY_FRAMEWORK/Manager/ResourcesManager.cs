using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achun
{
    [XLua.LuaCallCSharp]
    public class ResourcesManager
    {
        public static string name = "ResManager";
        public ResourcesManager()
        {
            Debug.Log("new ResourcesManager");
        }

        public void Init()
        {
            Debug.Log("Init");
        }
    }
}
