using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderTest : MonoBehaviour
{
    private UISprite s;
    private UISprite s1;

    private void Awake()
    {
        
    }

    private void Start()
    {
        s = GetComponent<UISprite>();
        s1 = transform.Find("Sprite").GetComponent<UISprite>();
        s1.spriteName = "Button";
        if (SceneManager.GetActiveScene().buildIndex == 0)
            s.spriteName = "Bright";
        else
            s.spriteName = "Dark";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(s.gameObject);
            s = null;
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            s1.gameObject.SetActive(!s1.gameObject.activeSelf);
        }
    }


    private void OnDisable()
    {
        
    }
}
