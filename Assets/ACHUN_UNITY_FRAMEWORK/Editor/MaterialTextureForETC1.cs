using UnityEngine;
using UnityEditor;
using System.IO;

public class MaterialTextureForETC1
{
    private static string defaultWhiteTexPath = "Assets/Default_Alpha.png";
    private static Texture2D defaultWhiteTex = null;

    [MenuItem("Assets/Textures/Depart RGB and Alpha")]
    private static void SeperateAllRGBAndAlphaChannel()
    {
        Debug.Log("");
        if (!GetDefaultWhiteTexture())
            return;

        string[] paths = Directory.GetFiles(Application.dataPath,"*",SearchOption.AllDirectories);
        foreach (string path in paths)
            if (!string.IsNullOrEmpty(path) && IsTextureFile(path) && !IsTextureConverted(path))
                SeperateAllRGBAndAlphaChannel(path);

        AssetDatabase.Refresh();
        Debug.Log("完成");
    }


    private static void SeperateAllRGBAndAlphaChannel(string path)
    {
        string assetRelativePath = GetRelativeAssetPath(path);
        SetTextureReadableEx(assetRelativePath);
        Texture2D sourceTex = Resources.Load<Texture2D>(assetRelativePath);
        if (!sourceTex)
        {
            Debug.LogError("没有图片");
            return;
        }

        TextureImporter ti = null;
        try
        {
            ti = TextureImporter.GetAtPath(assetRelativePath) as TextureImporter;
        }
        catch
        {
            return;
        }

        if (ti == null)
            return;

        bool bGenerateMipMap = ti.mipmapEnabled;
        Texture2D rgbTex = new Texture2D(sourceTex.width,sourceTex.height,TextureFormat.RGB24,bGenerateMipMap);
        rgbTex.SetPixels(sourceTex.GetPixels());

        Texture2D mipMapTex = new Texture2D(sourceTex.width,sourceTex.height,TextureFormat.RGB24,true);
        mipMapTex.SetPixels(sourceTex.GetPixels());
        mipMapTex.Apply();
        Color[] color2rdLevel = mipMapTex.GetPixels();
        Color[] colorAlpha = new Color[color2rdLevel.Length];
        if (color2rdLevel.Length != (mipMapTex.width + 1) / 2 * (mipMapTex.height + 1) / 2)
        {
            Debug.LogError("图片大小不对");
            return;
        }
        bool bAlphaExist = false;
        for (int i = 0; i < color2rdLevel.Length; i++)
        {
            colorAlpha[i].r = color2rdLevel[i].a;
            colorAlpha[i].g = color2rdLevel[i].a;
            colorAlpha[i].b = color2rdLevel[i].a;

            if (!Mathf.Approximately(color2rdLevel[i].a, 1.0f))
                bAlphaExist = true;
        }


        Texture2D alphaTex = null;
        if (bAlphaExist)
            alphaTex = new Texture2D((sourceTex.width + 1) / 2, (sourceTex.height + 1) / 2, TextureFormat.RGB24, bGenerateMipMap);
        else
            alphaTex = new Texture2D(defaultWhiteTex.width,defaultWhiteTex.height,TextureFormat.Alpha8,false);

        alphaTex.SetPixels(colorAlpha);
        rgbTex.Apply();

        byte[] bytes = rgbTex.EncodeToPNG();
        File.WriteAllBytes(assetRelativePath,bytes);
        byte[] alphaBytes = alphaTex.EncodeToPNG();
        File.WriteAllBytes(GetAlphaTexturePath(path),alphaBytes);

        ReimportAsset(assetRelativePath,rgbTex.width,rgbTex.height);
        ReimportAsset(GetAlphaTexturePath(path),alphaTex.width,alphaTex.height);
    }


    private static void ReimportAsset(string path, int width, int height)
    {
        try
        {
            AssetDatabase.ImportAsset(path);
        }
        catch
        {
            Debug.LogError("");
            return;
        }
        TextureImporter ti = null;
        try
        {
            ti = TextureImporter.GetAtPath(path) as TextureImporter;
        }
        catch
        {
            return;
        }

        if (ti != null)
        {
            ti.maxTextureSize = Mathf.Max(width,height);
            ti.anisoLevel = 0;
            ti.isReadable = false;
            ti.textureFormat = TextureImporterFormat.AutomaticCompressed;
            ti.textureType = TextureImporterType.Default;
            if (path.Contains("/UI/"))
                ti.textureType = TextureImporterType.GUI;
            AssetDatabase.ImportAsset(path);
        }
    }


    private static void SetTextureReadableEx(string relativeAssetPath)
    {
        TextureImporter ti = null;
        try
        {
            ti = TextureImporter.GetAtPath(relativeAssetPath) as TextureImporter;
        }
        catch
        {
            Debug.LogError("未能设置图片读写");
            return;
        }
        if (ti == null)
            return;
        ti.textureFormat = TextureImporterFormat.AutomaticCompressed;
        AssetDatabase.ImportAsset(relativeAssetPath);
    }    


    private static bool GetDefaultWhiteTexture()
    {
        defaultWhiteTex = Resources.Load<Texture2D>(defaultWhiteTexPath);
        if (defaultWhiteTex != null)
        {
            Debug.LogError("没有图");
            return false;
        }
        return true;
    }


    private static bool IsTextureFile(string path)
    {
        string filePath = path.ToLower();
        return filePath.ToLower().EndsWith(".psd") || filePath.EndsWith(".jpg") || filePath.EndsWith(".png");
    }


    private static bool IsTextureConverted(string path)
    {
        return path.Contains("_RGB.") || path.Contains("Alpha.");
    }


    private static string GetRGBTexturePath(string path)
    {
        return GetTexturePath(path,"_RGB.");
    }


    private static string GetAlphaTexturePath(string path)
    {
        return GetTexturePath(path,"_Alpha.");
    }


    private static string GetTexturePath(string texPath,string texRole)
    {
        string dir = Path.GetDirectoryName(texPath);
        string fileName = Path.GetFileNameWithoutExtension(texPath);
        return dir + "/" + fileName + texRole + ".png";
    }


    private static string GetRelativeAssetPath(string fullPath)
    {
        fullPath = GetRightFormatPath(fullPath);
        int index = fullPath.IndexOf("Assets");
        return fullPath.Substring(index);
    }


    private static string GetRightFormatPath(string path)
    {
        return path.Replace("\\","/");
    }
}