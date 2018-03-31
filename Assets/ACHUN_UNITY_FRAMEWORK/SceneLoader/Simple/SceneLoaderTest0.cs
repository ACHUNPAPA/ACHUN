using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTest0 : MonoBehaviour
{
    private UISprite s;
    private UISprite s1;

    private void Awake()
    {
        s = GetComponent<UISprite>();
        s1 = transform.Find("Sprite").GetComponent<UISprite>();
        s1.spriteName = "Dark";
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            s.spriteName = "Bright";
        else
            s.spriteName = "Dark";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {            
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            s1.gameObject.SetActive(!s1.gameObject.activeSelf);
        }
    }


    private void OnDisable()
    {
        Destroy(s.gameObject);
    }
}
