using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
    public const float ONE_DIV_PI = 1.0F / Mathf.PI;

    public static float COS_15 = Mathf.Cos(Mathf.Deg2Rad * 15.0F);
    public static float COS_35 = Mathf.Cos(Mathf.Deg2Rad * 35.0F);
    public static float COS_45 = Mathf.Cos(Mathf.Deg2Rad * 45.0F);
    public static float COS_75 = Mathf.Cos(Mathf.Deg2Rad * 75.0F);
    public static float COS_60 = Mathf.Cos(Mathf.Deg2Rad * 60.0F);
    public static float COS_30 = Mathf.Cos(Mathf.Deg2Rad * 30.0F);
    public static float COS_20 = Mathf.Cos(Mathf.Deg2Rad * 20.0f);

    public static Vector2 AxisX2D = new Vector2(1,0);
    public static Vector2 AxisY2D = new Vector2(0,1);

    public static float EPSILON = 0.001f;

    /// <summary>
    /// 时间戳转换
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static System.DateTime TransToDataTime(uint t)
    {
        System.DateTime dt = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime());
        long LTime = long.Parse(t.ToString() + "0000000");
        System.TimeSpan toNow = new System.TimeSpan(LTime);
        return dt.Add(toNow);
    }

    /// <summary>
    /// 计算坐标间的距离
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float DistancePow(Vector3 a, Vector3 b)
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
    }


    public static float DistancePow(Vector2 a, Vector2 b)
    {
        return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y);
    }


    public static Vector3 Interp(Vector3[] pts, float t)
    {
        t = Mathf.Clamp(t,0.0f,2.0f);
        int numSections = pts.Length - 3;
        int currpt = Mathf.Min(Mathf.FloorToInt(t * numSections),numSections - 1);
        float u = t * numSections - currpt;
        Vector3 a = pts[currpt];
        Vector3 b = pts[currpt + 1];
        Vector3 c = pts[currpt + 2];
        Vector3 d = pts[currpt + 3];

        return 0.5f * (-a + 3f * b - 3f * c + d) * (u * u * u)
            + (2f * a - 5f * b + 4f * c - d) * (u * u)
            + (-a + c) * u
            + 2f * b;
    }


    public static float GetAngle(Vector3 form, Vector3 to)
    {
        Vector3 nVector = Vector3.zero;
        nVector.x = to.x;
        nVector.y = to.y;
        float a = to.y - nVector.y;
        float b = nVector.x - form.x;
        float tan = a / b;
        return Mathf.Atan(tan) * 180.0f * ONE_DIV_PI;
    }


    public static float Normalize(ref Vector3 vec)
    {
        float length = Mathf.Sqrt((vec.x * vec.x) + (vec.y * vec.y) + (vec.z * vec.z));
        if (length > 0)
        {
            float oneDivLength = 1.0f / length;
            vec.x = vec.x * oneDivLength;
            vec.y = vec.y * oneDivLength;
            vec.z = vec.z * oneDivLength;
        }
        return length;
    }

    /// <summary>
    /// 移动到某点
    /// </summary>
    /// <param name="dest"></param>
    /// <param name="cur"></param>
    /// <param name="speed"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public static Vector3 TryMoveToPosWithSpeed(Vector3 dest,Vector3 cur,float speed,float time)
    {
        Vector3 dir = dest - cur;
        float dis = Normalize(ref dir);
        if (speed * time < dis)
        {
            return cur + dir * speed * time;
        }
        else
        {
            return dest;
        }
    }


    public static Vector3 MoveToPosWithSpeedToOffset(Vector3 dest, Vector3 cur, float speed, float time)
    {
        Vector3 dir = dest - cur;
        Vector3 maxOffset = dir;
        float dis = Normalize(ref dir);
        if (speed * time < dis)
        {
            return dir * speed * time;
        }
        else
        {
            return maxOffset;
        }
    }

    /// <summary>
    /// float近似相等
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool IsEqualFloat(float a,float b)
    {
        return Mathf.Abs(a - b) < 0.001f;
    }


    public static bool IsEqualFloatRaw(float a,float b)
    {
        return Mathf.Abs(a - b) < 0.05f;
    }

    /// <summary>
    /// 3D坐标投影到屏幕坐标
    /// </summary>
    /// <param name="cam"></param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Vector2 ProjectToScreen(Camera cam, Vector3 point)
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(point);
        return new Vector2(screenPoint.x,screenPoint.y);
    }
}
