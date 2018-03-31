using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetReflaction
{
    private string[] dependenceBundles;

    public AssetReflaction(string[] dependencesBundles)
    {
        this.dependenceBundles = dependencesBundles;
    }

    public bool IsDepenceBundle(string abName)
    {
        int length = dependenceBundles.Length; ;
        for (int i = 0; i < length; i++)
            if (dependenceBundles[i] == abName)
                return true;
        return false;
    }

    public void Dispose()
    {
        dependenceBundles = null;
    }
}
