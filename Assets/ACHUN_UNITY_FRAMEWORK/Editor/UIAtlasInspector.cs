//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(UIAltas))]
//public class UIAtlasInspector : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        UIAltas atlas = target as UIAltas;
//        atlas.mainTex = EditorGUILayout.ObjectField("MainTexture", atlas.mainTex, typeof(Texture2D), false) as Texture2D;

//        if (GUILayout.Button("刷新数据"))
//        {
//            if (atlas.mainTex == null)
//            {
//                string path = EditorUtility.OpenFilePanel("选择图集",Application.dataPath,"png");
//                if (!string.IsNullOrEmpty(path))
//                {
//                    atlas.mainTex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

//                }
//            }

//            if (atlas.mainTex != null)
//            {
//                string path = AssetDatabase.GetAssetPath(atlas.mainTex);
//                TextureImporter importer = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(Selection.activeObject)) as TextureImporter;
//                if (importer != null && importer.textureType == TextureImporterType.Sprite && importer.spriteImportMode == SpriteImportMode.Multiple)
//                {
//                    object[] objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(atlas.mainTex));
//                    atlas.sprites.Clear();
//                    foreach (var obj in objs)
//                    {
//                        if (obj.GetType() == typeof(Texture2D))
//                            atlas.mainTex = obj as Texture2D;
//                        else if (obj.GetType() == typeof(Sprite))
//                            atlas.sprites.Add(obj as Sprite);
//                    }
//                }
//                else
//                {
//                    atlas.mainTex = null;
//                }          
//            }            
//        }
//        if (atlas.sprites.Count > 0)
//        {
//            foreach (Sprite s in atlas.sprites)
//            {
//                EditorGUILayout.ObjectField(s.name, s, typeof(Sprite), false);
//            }
//        }
//    }
//}
