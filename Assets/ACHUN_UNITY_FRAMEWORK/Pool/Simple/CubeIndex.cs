using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CubeIndex : MonoBehaviour
{
    public string id;
    private bool isSet = false;

    private void Awake()
    {
        Set();
    }

    private void OnEnable()
    {
        Debug.Log(id + " enable");
    }

    private void OnDisable()
    {
        Debug.Log(id + " disable");
    }

    public void Set()
    {
        if (isSet)
            return;
        id = DateTime.Now.ToLongTimeString();
        isSet = true;
    }
}
