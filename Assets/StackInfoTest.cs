using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackInfoTest : MonoBehaviour
{
    StackInfo si = new StackInfo();
	// Use this for initialization
	void Start ()
    {
        GetStackInfo0();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GetStackInfo0()
    {
        GetStackInfo1();
    }


    private void GetStackInfo1()
    {
        GetStackInfo2();
    }


    private void GetStackInfo2()
    {
        GetStackInfo3();
    }

    private void GetStackInfo3()
    {
        int[] arr = new int[3];
        try
        {
            arr[4] = 5;
        }
        catch(Exception e)
        {
            Debug.LogError(si.Test() + "\nStackOverflow: https://stackoverflow.com/search?q=" + e);
        }
    }
}
