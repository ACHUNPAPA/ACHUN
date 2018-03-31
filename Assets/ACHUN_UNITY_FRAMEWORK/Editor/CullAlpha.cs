using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CullAlpha
{
    [MenuItem("Assets/CreateAlpha")]
    private static void CreateAlpha()
    {
        Object[] texObjs = Selection.objects;        
        foreach (var obj in texObjs)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            Texture2D tex = obj as Texture2D;
            Texture2D newTex = new Texture2D(tex.width,tex.height,TextureFormat.RGB565,false);
            Color color = Color.black;
            for (int j = 0; j < tex.width; j++)
            {
                for (int i = 0; i < tex.height; i++)
                {
                    color.r = tex.GetPixel(j,i).a;
                    newTex.SetPixel(j,i,color);
                    newTex.Apply();
                    System.IO.File.WriteAllBytes(path.Replace(".tga","_A.jpg"),newTex.EncodeToJPG());
                }
            }
        }
        AssetDatabase.Refresh();
    }
}
