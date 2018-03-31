using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achun.Asset
{
    public class AssetbundleLoader
    {
        private Dictionary<string,AssetObject> assets;
        private AssetBundle assetbundle;
        private AssetReflaction assetReflaction;

        public AssetbundleLoader(AssetBundle assetbundle,string[] dependencesBundles)
        {
            assets = new Dictionary<string, AssetObject>();
            this.assetbundle = assetbundle;
            assetReflaction = new AssetReflaction(dependencesBundles);

        }

        private T LoadAssetByName<T>(string assetName) where T : Object
        {
            if (assetbundle == null)
                return default(T);
            T asset = assetbundle.LoadAsset<T>(assetName);
            assets.Add(assetName,new AssetObject(assetbundle, asset));
            return asset;
        }


        private IEnumerator LoadAssetAsyncByName<T>(string assetName, System.Action<Object> callback) where T : Object
        {
            if (assetbundle == null)
                yield break;
            AssetBundleRequest abRequest = assetbundle.LoadAssetAsync<T>(assetName);
            yield return abRequest;

            T asset = (T)abRequest.asset;
            if (asset != null)
            {
                if (callback != null)
                    callback(abRequest.asset);
                assets.Add(asset.name,new AssetObject(assetbundle, asset));
            }
            else
            {
                Debug.LogError("");
            }
        }


        public T GetAssetByName<T>(string assetName,System.Action<Object> callback = null) where T : Object
        {
            if (assets.ContainsKey(assetName))
                return (T)assets[assetName].asset;
            return LoadAssetByName<T>(assetName);
        }


        public void UnloadAsset(string assetName)
        {
            AssetObject assetObj = null;
            if (assets.TryGetValue(assetName, out assetObj))
            {
                assetObj.Dispose();
            }
        }

        public void Dispose(bool unloadAllLoadedObjects)
        {            
            assetbundle.Unload(unloadAllLoadedObjects);
            assetbundle = null;

            foreach(var assetObj in assets.Values)
                assetObj.Dispose();
            assets.Clear();
            assets = null;
        }
    }
}
