using UnityEngine;
using System.Collections;

public class LerpTest : MonoBehaviour
{
    private Vector3 start;
    public Vector3 end;
    private float duration;
    private IEnumerator tween;
    //private Vector3 v;

    private void Start()
    {
        //start = transform.position;
        //end = Vector3.one * 6;
        //duration = 300;
        //tween = Tween();
        //v = end - start;
	Debug.Log(67.ToString("D1"));
	Debug.Log(7.ToString("D2"));
	Debug.Log(267.ToString("D3"));
	Debug.Log(267.ToString("D4"));
	Debug.Log(267.ToString("D5"));
    }

    private void Update()
    {
        //StartCoroutine(tween);
        //if(transform.position.x < end.x && transform.position.y < end.y && transform.position.z < end.z)
        //    transform.Translate(v.normalized);
    }


    private Vector3 Lerp(Vector3 start,Vector3 end,float duration)
    {
        return (1 - duration * duration) * start + duration * duration * end;
    }


    private Vector3 QuadraticEaseOut(Vector3 start,Vector3 end,float duration)
    {
        Vector3 v = (end - start) / 2;
        duration *= 2;
        if (duration <= 1)
            return Lerp(start,v,duration);
        duration -= 1;
        return Lerp(v,end,duration);
    }


    private IEnumerator Tween()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            Vector3 v = transform.position;
            elapsedTime += Time.deltaTime;            
            transform.position = QuadraticEaseOut(start,end,Mathf.Clamp01(elapsedTime / duration));
            yield return null;
        }
    }
}
