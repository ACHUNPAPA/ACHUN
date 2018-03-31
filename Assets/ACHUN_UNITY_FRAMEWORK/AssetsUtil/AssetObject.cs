using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Achun.Asset
{
    public class AssetObject
    {
        private AssetBundle assetbundle;
        public Object asset
        {
            get;
            private set;
        }

        public AssetObject(AssetBundle assetbundle,Object asset)
        {
            this.assetbundle = assetbundle;
            this.asset = asset;
        }

        public void Dispose()
        {
            assetbundle = null;
            if (asset is GameObject)
                Resources.UnloadUnusedAssets();
            else
                Resources.UnloadAsset(asset);
            asset = null;
        }
    }
}
