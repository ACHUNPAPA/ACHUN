using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    private bool get = false;
    private void Start()
    {
        StartCoroutine(DoTween(8, transform.position, Vector3.one * 10));
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        //    get = true;
    }

    private IEnumerator DoTween(float t, Vector3 start, Vector3 end)
    {
        float t1 = 0;
        Vector3 tmp;
        while (t1 <= t)
        {
            tmp = transform.position;
            t1 += Time.deltaTime;
            float percentage = Mathf.Clamp01(t1 / t);
            if(!get)
                transform.position = QuadraticEaseOut(percentage, start, end);
            else
                transform.position = LinearTween(percentage, start, end);
            Debug.Log(t1);
            yield return null;
            if (Input.GetKeyDown(KeyCode.A))
            {
                get = true;
                start = transform.position;
                t1 = 0;
                t = 2;
                end = transform.position + Vector3.one * 2;
            }
        }
    }

    private Vector3 LinearTween(float t, Vector3 start, Vector3 end)
    {
        return (1 - t) * start + t * end;
    }

    private Vector3 QuadraticEaseOut(float t, Vector3 start, Vector3 end)
    {
        Vector3 middle = (start + end) / 2;
        t *= 2;
        if (t <= 1)
            return LinearTween(t * t, start, middle);
        t -= 1;
        return LinearTween(t, middle, end);
    }

    private Vector3 QuadraticEaseIn(float t, Vector3 start, Vector3 end)
    {
        return LinearTween(t * t, start, end);
    }
}
