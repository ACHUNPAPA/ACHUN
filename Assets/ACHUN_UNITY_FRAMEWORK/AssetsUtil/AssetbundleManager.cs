using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achun.Asset
{
    public class AssetbundleManager
    {
        private Dictionary<string, AssetbundleLoader> abLoaders;
        private AssetbundleMenifestLoader menifestLoader;

        public AssetbundleManager()
        {
            abLoaders = new Dictionary<string, AssetbundleLoader>();
            menifestLoader = new AssetbundleMenifestLoader();
        }

        public IEnumerator LoadAssetbundleByName(string url,string abName)
        {
            if (abLoaders.ContainsKey(abName))
                yield break;
            string[] abDependences = menifestLoader.GetAllDependencesBundle(abName);
            int length = abDependences.Length;

            for (int i = 0; i < length; i++)
            {
                if(!abLoaders.ContainsKey(abDependences[i]))
                    yield return LoadAssetbundleByName(url, abDependences[i]);
            }
            using (WWW www = new WWW(url + "/" + abName))
            {
                yield return www;
                if (www.isDone && string.IsNullOrEmpty(www.error))
                {                            
                    AssetBundle ab = www.assetBundle;
                    Debug.Log(ab.name);
                    abLoaders.Add(abName,new AssetbundleLoader(ab,menifestLoader.GetAllDependencesBundle(abName)));
                }
                else
                {
                    Debug.LogError(www.error);
                }
            }
        }

        public IEnumerator LoadMenifest()
        {
            if (menifestLoader.isFinish)
                yield break;
            yield return menifestLoader.LoadMenifest();
        }


        public T GetAsset<T>(string abName,string assetName) where T : Object
        {
            AssetbundleLoader abLoader = null;
            if (abLoaders.TryGetValue(abName, out abLoader))
            {
                return abLoader.GetAssetByName<T>(assetName);
            }
            return default(T);
        }


        public string[] GetBundleAllDependences(string abName)
        {
            return menifestLoader.GetAllDependencesBundle(abName);
        }


        public void UnloadAssetbundle(string abName,bool unloadAllLoadedObjects)
        {
            AssetbundleLoader abLoader = null;
            if (abLoaders.TryGetValue(abName, out abLoader))
            {
                abLoader.Dispose(unloadAllLoadedObjects);
                abLoaders.Remove(abName);
            }
        }


        public void UnLoadAsset(string abName,string assetName)
        {
            AssetbundleLoader abLoader = null;
            if (abLoaders.TryGetValue(abName, out abLoader))
            {
                abLoader.UnloadAsset(assetName);
            }
        }


        public void Release()
        {
            foreach (var abLoader in abLoaders.Values)
            {
                abLoader.Dispose(false);
            }
        }


        public void Dispose()
        {
            foreach (var abLoader in abLoaders.Values)
            {
                abLoader.Dispose(true);
            }
            abLoaders = null;
            menifestLoader.Dispose();
            menifestLoader = null;
        }
    }
}
