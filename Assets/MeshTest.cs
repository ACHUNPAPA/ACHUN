using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshTest : MonoBehaviour
{
    public float[] parcentages;
    private float[] tmp;
    private Vector3[] veteices;
    public int[] directions;
    
    private MeshFilter meshFilter;
    private Mesh mesh;


    private void Start()
    {
        tmp = new float[parcentages.Length];
        veteices = new Vector3[] { new Vector3(0,0,0),new Vector3(0,1,0),new Vector3(0.5f,0.5f,0),new Vector3(0.5f,-0.5f,0),new Vector3(0,-1,0),new Vector3(-0.5f,-0.5f,0),new Vector3(-0.5f,0.5f,0) };
        meshFilter = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
    }

    private void Update()
    {
        for (int i = 0; i < parcentages.Length; i++)
        {
            if (parcentages[i] != tmp[i])
            {
                veteices[i + 1] *= Mathf.Clamp01(parcentages[i]);
                tmp[i] = parcentages[i];
            }
        }
        mesh.vertices = veteices;
        mesh.triangles = directions;
        for (int i = 0; i < mesh.uv.Length; i++)
        {
            mesh.uv[i] = veteices[i];
        }
        meshFilter.mesh = mesh;
        IntEquality dict = new IntEquality();
        Dictionary<int, string> d = new Dictionary<int, string>(dict);
    }
}


public abstract class MyEqualityComparer<T> : IEqualityComparer<T>
{
    public abstract bool Equals(T x, T y);

    public abstract int GetHashCode(T obj);
}


public class IntEquality : MyEqualityComparer<int>
{
    public override bool Equals(int x, int y)
    {
        return x == y;
    }

    public override int GetHashCode(int obj)
    {
        throw new System.NotImplementedException();
    }
}
