using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AltasEditor
{
    [MenuItem("Assets/Create Altas")]
    private static void CreateAltasPrefab()
    {
        if (Selection.activeObject != null)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            TextureImporter importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject)) as TextureImporter;
            if (importer != null && importer.textureType == TextureImporterType.Sprite && importer.spriteImportMode == SpriteImportMode.Multiple)
            {
                UIAltas atlas = ScriptableObject.CreateInstance<UIAltas>();
                object[] objs = AssetDatabase.LoadAllAssetsAtPath(path);
                Debug.Log(objs.Length);
                foreach (var obj in objs)
                {
                    if (obj.GetType() == typeof(Texture2D))
                        atlas.mainTex = obj as Texture2D;
                    else if (obj.GetType() == typeof(Sprite))
                        atlas.sprites.Add(obj as Sprite);
                }

                AssetDatabase.CreateAsset(atlas,path.Replace(".png","_Atlas.prefab"));
                AssetDatabase.Refresh();
                //AssetDatabase.SaveAssets();
            }
        }
    }
}
