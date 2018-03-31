using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Achun.File;

public class AssetbundlePackage
{
    private static string AndroidBundleOutputPath = Application.streamingAssetsPath + "/AndroidBundles";
    private static string AssetsPath = Application.dataPath + "/Assets/SceneAssets";

    [MenuItem("Assets/Assetbundle/Build Android Bundle")]
    private static void BuildAndroidAssetbundle()
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
        SetAllBundleName();
        if (Directory.Exists(AndroidBundleOutputPath))
        {
            BuildPipeline.BuildAssetBundles(AndroidBundleOutputPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
            Debug.Log(AndroidBundleOutputPath);
        }
        AssetDatabase.Refresh();
    }


    private static void SetAllBundleName()
    {
        DirectoryInfo dirInfo = FileTools.GetDirectoryInfo(AssetsPath);
        DirectoryInfo[] sceneDirs = dirInfo.GetDirectories();
        foreach (DirectoryInfo info in sceneDirs)
        {
            if (info == null)
                return;
            else
            {
                OnSceneFileSystemInfo(info,info.Name);
            }
        }
    }

    private static void OnSceneFileSystemInfo(FileSystemInfo fileSystemInfo,string sceneName)
    {
        if (!fileSystemInfo.Exists)
            return;
        DirectoryInfo dirInfo = fileSystemInfo as DirectoryInfo;
        FileSystemInfo[] fileSystemInfos = dirInfo.GetFileSystemInfos();
        foreach (var tmpFileSystemInfo in fileSystemInfos)
        {
            FileInfo fileInfo = tmpFileSystemInfo as FileInfo;
            if (fileInfo == null)
            {
                //是文件夹，所以递归
                OnSceneFileSystemInfo(tmpFileSystemInfo, sceneName);
            }
            else
            {
                if(!fileInfo.Name.Contains(".meta"))
                    SetBundleName("Assets/Assets/SceneAssets/" + sceneName + "/" + fileInfo.Name,sceneName);
            }
        }
    }


    private static void SetBundleName(string filePath,string sceneName)
    {
        AssetImporter importer = AssetImporter.GetAtPath(filePath);
        string[] s = filePath.Split('.');
        string suffix = s[s.Length - 1];
        importer.SetAssetBundleNameAndVariant(sceneName + "_" + suffix + ".unity3d","papa");
    }
}
