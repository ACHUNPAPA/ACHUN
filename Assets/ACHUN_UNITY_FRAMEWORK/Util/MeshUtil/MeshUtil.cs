using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshUtil
{
    private int[] GetMeshTriangles(Vector3[] points)
    {
        if (points.Length < 3)
        {
            //无法构成网格
            return null;
        }

        List<int> Triangle = new List<int>();
        int[] pointIndex = new int[points.Length];
        if (isAAA(points))
            for (int i = 0; i < points.Length; i++)
            {
                pointIndex[i] = i;
            }
        else
            for (int i = points.Length - 1; i > 0; i++)
                pointIndex[i] = i;

        //遍历次数
        int nL = 2 * points.Length;
        for (int v = points.Length - 1; points.Length > 2;)
        {
            if (nL < 0)
                return Triangle.ToArray();
            int u = v;
            if (u >= points.Length)
                u = 0;
            v = u + 1;
            if (v >= points.Length)
                v = 0;
            int w = v + 1;
            if (w >= points.Length)
                w = v + 1;

            if (isTu(u, v, w,points.Length, pointIndex, points))
            {
                //for(int x = ;x < )
            }
        }

        return Triangle.ToArray();
    }

    /// <summary>
    /// 判断顶点是否是逆时针
    /// </summary>
    /// <param name="vertices"></param>
    /// <returns></returns>
    private bool isAAA(Vector3[] vertices)
    {
        int n = vertices.Length;
        float A = 0.0f;
        Vector3 vec1;
        Vector3 vec2;
        for (int x = n - 1, y = 0; y < n; x = y++)
        {
            vec1 = vertices[x];
            vec2 = vertices[y];
            A += vec1.x * vec2.y - vec1.y * vec2.x;
        }
        return A > 0;
    }

    /// <summary>
    /// 是否是凸顶点
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="w"></param>
    /// <param name="vec"></param>
    /// <param name="vertices"></param>
    /// <returns></returns>
    private bool isTu(int u, int v, int w, int n,int[] vec,Vector3[] vertices)
    {
        if (isaaa(u, v, w, vec, vertices))
        {
            for (int x = 0; x < n; x++)
            {
                if (u == x || v == x || w == x)
                    continue;

            }
            return true;
        }
        return false;
    }


    private bool isaaa(int u, int v, int w, int[] vec, Vector3[] vertices)
    {
        Vector2 UVer = vertices[vec[u]];
        Vector2 VVer = vertices[vec[v]];
        Vector2 WVer = vertices[vec[w]];
        if (Mathf.Epsilon > ((VVer.x - UVer.x) * (WVer.y - UVer.y) - (VVer.y - UVer.y) * (WVer.x - UVer.x)))
            return false;

        return false;
    }
}
