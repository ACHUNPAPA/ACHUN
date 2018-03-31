using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AChun.Pool;

public class PoolTest : MonoBehaviour
{
    private PoolManager poolManager;

    private GameObject cube;
    private List<GameObject> gos;

    private void Awake()
    {
        poolManager = new PoolManager();
        gos = new List<GameObject>();
        cube = GameObject.Find("Cube");
    }


    private void Start()
    {
        poolManager.RegisterGameObjectPool(cube, 30,MallocCube,
            (go) => Destroy(go)
            );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 20; i++)
            {
                poolManager.Spawn(string.Format(Define.poolName, cube.name), OnSpawn);
            }
        }
        Debug.Log(gos.Count);

        if (Input.GetKeyDown(KeyCode.B))
        {
            for (int i = 0; i < 20; i++)
                poolManager.Recycle(gos[gos.Count - 1],
                    (go) => { int id = gos.IndexOf(go);gos.RemoveAt(id) ; go.SetActive(false); });
        }
        Debug.Log(gos.Count);
    }

    private void OnApplicationPause(bool pause)
    {
        poolManager.OnApplicationPause();
    }

    private void OnApplicationQuit()
    {
        poolManager.OnApplicationQuit();
    }

    private void OnDestroy()
    {
        poolManager.OnDestroy();
    }


    private GameObject MallocCube(GameObject original)
    {
        GameObject go = Instantiate(original,original.transform.parent,false);
        go.AddComponent<CubeIndex>();
        return go;
    }


    private void OnSpawn(GameObject go)
    {
        CubeIndex idComponent = go.GetComponent<CubeIndex>();
        if (idComponent == null)
            idComponent = go.AddComponent<CubeIndex>();
        gos.Add(go);
        go.SetActive(true);
    }
}
