using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achun.SceneManager;

public class ResourcesMgrTest : MonoBehaviour
{
    private SceneManager sceneManager;
    private Texture2D tex;
    private GameObject cube;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        sceneManager = new SceneManager();
        StartCoroutine(sceneManager.GetLoadingScene());
    }

    private void Start()
    {
        StartCoroutine(CallRes());
    }

    private void Update()
    {
        if(tex != null)
            Debug.Log(tex.name);
        if (cube != null)
            Debug.Log(cube.name);
    }


    private IEnumerator CallRes()
    {
        using (WWW www = new WWW(Application.streamingAssetsPath + "/Test"))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {

            }
        }
    }
}
