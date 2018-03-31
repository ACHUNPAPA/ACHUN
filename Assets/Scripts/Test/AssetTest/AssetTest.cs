using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achun.Asset;

public class AssetTest : MonoBehaviour
{
    private AssetbundleManager assetbundleManager;

    GameObject g1;
    GameObject g2;
    GameObject g3;

    private void Awake()
    {
        assetbundleManager = new AssetbundleManager();
        StartCoroutine(assetbundleManager.LoadMenifest());
    }

    private void Start()
    {
        
   }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(GetAsset());          
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            assetbundleManager.GetAsset<Material>("commonassets_mat.unity3d.papa", "DiffusePixelLevelMat.mat");
            assetbundleManager.GetAsset<Shader>("commonassets_shader.unity3d.papa", "DiffusePixelLevel.shader");
            //g1 = Instantiate(assetbundleManager.GetAsset<GameObject>("0_prefab.unity3d.papa", "Capsule"));
            g2 = Instantiate(assetbundleManager.GetAsset<GameObject>("1_prefab.unity3d.papa", "Capsule (1).prefab"));
            assetbundleManager.GetAsset<Texture2D>("commonassets_jpg.unity3d.papa", "\u80E1\u7115.jpg");
            assetbundleManager.GetAsset<Shader>("commonassets_shader.unity3d.papa", "SingleTexture.shader");
            assetbundleManager.GetAsset<Shader>("commonassets_mat.unity3d.papa", "SingleTextureMat.mat");
            g3 = Instantiate(assetbundleManager.GetAsset<GameObject>("0_prefab.unity3d.papa", "Capsule (5).prefab"));
       }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Destroy(g2);
            //g2 = null;
            //assetbundleManager.UnLoadAsset("1_prefab.unity3d.papa", "Capsule (1).prefab");
            //assetbundleManager.UnloadAssetbundle("1_prefab.unity3d.papa");
            //assetbundleManager.UnLoadAsset("commonassets_mat.unity3d.papa", "DiffusePixelLevelMat.mat");
            //assetbundleManager.UnloadAssetbundle("commonassets_mat.unity3d.papa");
            //Destroy(g3);
            //g3 = null;
            assetbundleManager.UnLoadAsset("commonassets_jpg.unity3d.papa", "\u80E1\u7115.jpg");
            assetbundleManager.UnloadAssetbundle("commonassets_jpg.unity3d.papa",false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(assetbundleManager.LoadAssetbundleByName(Application.streamingAssetsPath + "/AndroidBundles", "commonassets_jpg.unity3d.papa"));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            assetbundleManager.GetAsset<Texture2D>("commonassets_jpg.unity3d.papa", "\u80E1\u7115.jpg");
        }
    }


    private IEnumerator GetAsset()
    {
        yield return assetbundleManager.LoadAssetbundleByName(Application.streamingAssetsPath + "/AndroidBundles", "0_prefab.unity3d.papa");
        StartCoroutine(assetbundleManager.LoadAssetbundleByName(Application.streamingAssetsPath + "/AndroidBundles", "1_prefab.unity3d.papa"));
    }
}
