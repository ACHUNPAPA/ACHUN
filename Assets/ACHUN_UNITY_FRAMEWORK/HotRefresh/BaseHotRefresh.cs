using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHotRefresh
{
    public abstract void Start();

    public abstract void Update();
    public abstract void OnDestroy();

    public abstract void DoString(string hotRefreshScript);

    public abstract System.Action GetFun(string funName);
}
