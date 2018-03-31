using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Achun.SceneManager
{
    public class SceneManager
    {
        private static Scene loadingScene;

        public SceneManager()
        {

        }

        public IEnumerator GetLoadingScene()
        {
            using (WWW www = new WWW(Application.streamingAssetsPath))
            {
                yield return www;

                if (string.IsNullOrEmpty(www.error))
                {
                    AssetBundle ab = www.assetBundle;
                    loadingScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("loadingScene");
                    ab.Unload(false);
                }
            }
        }


        public bool LoadScene(string sceneName)
        {
            return false;
        }


        public bool LoadSceneAsnyc(string sceneName)
        {
            return false;
        }
    }
}
