using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achun.Asset
{
    public class AssetbundleMenifestLoader
    {
        private AssetBundleManifest menifest;
        private AssetBundle ab;
        private static string menifestPath = Application.streamingAssetsPath + "/AndroidBundles/AndroidBundles";
        public bool isFinish
        {
            get
            {
                return menifest != null;
            }
            private set
            { }
        }

        public AssetbundleMenifestLoader()
        {
            menifest = null;
            ab = null;
            isFinish = false;
        }


        public IEnumerator LoadMenifest()
        {
            using (WWW www = new WWW(menifestPath))
            {
                yield return www;
                if (www.isDone && string.IsNullOrEmpty(www.error))
                {
                    ab = www.assetBundle;
                    menifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                }
                else
                {
                    Debug.LogError(www.error);
                }
            }
        }


        public string[] GetAllDependencesBundle(string abName)
        {
            if (menifest != null)
                return menifest.GetAllDependencies(abName);
            return null;
        }

        public void Dispose()
        {
            if (ab != null)
                ab.Unload(true);
        }
    }
}
