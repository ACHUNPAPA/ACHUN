using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumatorTest : MonoBehaviour
{
    private void Start()
    {
        StartC(EnumatorT());
    }


    private IEnumerator EnumatorT()
    {
        WaitForSeconds sec = new WaitForSeconds(1.0f);
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("PAPA");
        sec = null;
    }


    private void StartC(IEnumerator arg)
    {
        while (arg.MoveNext())
        {

        }
    }
}
