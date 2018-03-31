using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HotRefreshManager
{
    private BaseHotRefresh hotRefreshInstance;    

    public void Init()
    {
#if XLUA
        hotRefreshInstance = new XLuaInstance();
#endif
    }


    public void Start()
    {
        hotRefreshInstance.Start();
    }


    public void Update()
    {

    }


    public void OnDestroy()
    {
        hotRefreshInstance.OnDestroy();
    }


    public void DoString(string hotRefreshScript)
    {
        hotRefreshInstance.DoString(hotRefreshScript);
    }
}
