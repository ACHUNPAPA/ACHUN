using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLog;

public class LogTest : MonoBehaviour
{
    private void Start()
    {
        Debuger.isDebug = true;
        Debuger.Log("Init");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debuger.isDebug = !Debuger.isDebug;
            Debuger.Log(Debuger.isDebug ? "open" : "close");
        }
    }
}
