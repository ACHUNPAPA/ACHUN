using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureEditor : MonoBehaviour
{
    [MenuItem("Assets/Tools/Merge Textures")]
    private static void MergeTextures()
    {
        Object[] objs = Selection.objects;
        Texture2D[] textures = new Texture2D[objs.Length];
        for (int i = 0; i < textures.Length; i++)
        {
            Debug.Log(AssetDatabase.GetAssetPath(objs[i]));
            TextureSetData.isTrue = true;
            textures[i] = objs[i] as Texture2D;
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(objs[i]));
        }

        Texture2D mergedTextures = new Texture2D(2048,2048);
        Rect[] rects = mergedTextures.PackTextures(textures,0);
        for (int i = 0; i < rects.Length; i++)
        {
            //for (int j = 0; j < textures[i].GetPixels().Length; j++)
            //    mergedTextures.SetPixel(rects[i].x * mergedTextures.width, rects[i].y * mergedTextures.height,textures[i].GetPixels()[j]);
            mergedTextures.SetPixels((int)rects[i].x * mergedTextures.width, (int)rects[i].y * mergedTextures.height, (int)rects[i].width * mergedTextures.width, (int)rects[i].y * mergedTextures.height,textures[i].GetPixels());
        }

        File.WriteAllBytes(Application.dataPath + "/PAPA.png",mergedTextures.EncodeToPNG());
        //AssetDatabase.ImportAsset(Application.dataPath + "/PAPA.png",ImportAssetOptions.Default);

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Tools/Merge Mesh")]
    private static void MergeMesh()
    {
        GameObject[] gos = Selection.gameObjects;
        Mesh[] meshes = new Mesh[gos.Length];
        Texture2D[] textures = new Texture2D[gos.Length];
        MeshFilter mf = null;
        for (int i = 0; i < gos.Length; i++)
        {
            mf = gos[i].GetComponent<MeshFilter>();
            if (mf == null || mf.mesh == null)
                return;
            meshes[i] = mf.sharedMesh;
            textures[i] = gos[i].GetComponent<MeshRenderer>().sharedMaterial.mainTexture as Texture2D;
            if (textures[i] == null)
                return;
        }

        foreach (Texture2D tex in textures)
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(tex));
        Texture2D mergedTexture = new Texture2D(2048,2048);
        Rect[] rects = mergedTexture.PackTextures(textures,0,2048);
        for(int i = 0;i < rects.Length;i++)
            mergedTexture.SetPixels((int)rects[i].x * mergedTexture.width, (int)rects[i].y * mergedTexture.height, (int)rects[i].width * mergedTexture.width, (int)rects[i].y * mergedTexture.height, textures[i].GetPixels());
        mergedTexture.Apply();

        Mesh newMesh = null;
        Vector2[] uv = null;
        for (int i = 0; i < gos.Length; i++)
        {
            newMesh = new Mesh();
            newMesh.name = meshes[i].name;
            uv = GetUV(meshes[i].uv);
            for (int j = 0; j < uv.Length; j++)
            {
                uv[j] = new Vector2(rects[i].x + uv[j].x * rects[i].width,rects[i].y + uv[j].x * rects[i].height);
            }
            newMesh.triangles = meshes[i].triangles;
            newMesh.vertices = meshes[i].vertices;
            newMesh.uv = meshes[i].uv;
            meshes[i] = newMesh;
        }

        System.IO.File.WriteAllBytes(Application.dataPath + "/PAPA.png",mergedTexture.EncodeToPNG());
        for (int i = 0; i < meshes.Length; i++)
        {
            if (AssetDatabase.Contains(meshes[i]))
                AssetDatabase.CreateAsset(Instantiate(meshes[i]), Application.dataPath + "/" + i + ".asset");
            else
                AssetDatabase.CreateAsset(meshes[i],Application.dataPath + "/" + i + ".asset");
        }

        Resources.UnloadUnusedAssets();
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    /// <summary>
    /// UV重定位
    /// </summary>
    /// <param name="uv"></param>
    private static Vector2[] GetUV(Vector2[] uv)
    {
        for (int i = 0; i < uv.Length; i++)
            if (uv[i].x > 1 || uv[i].x < 0 || uv[i].y > 1 || uv[i].y < 1)
            {
                uv[i].x -= Mathf.Floor(uv[i].x);
                uv[i].y -= Mathf.Floor(uv[i].y);
            }
        return uv;
    }
}


public class TextureSetData : AssetPostprocessor
{
    public static bool isTrue = false;
    void OnPreprocessTexture()
    {
        if (isTrue)
        {
            TextureImporter importer = assetImporter as TextureImporter;
            importer.textureType = TextureImporterType.Default;
            //importer.textureFormat = TextureImporterFormat.AutomaticTruecolor;
            importer.textureCompression = TextureImporterCompression.Compressed;
            importer.isReadable = true;
            importer.mipmapEnabled = false;
        }
    }

    void OnPostprocessTexture(Texture2D texture)
    {

    }
}