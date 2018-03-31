using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Pool
{
    public class GameObjectPool
    {
        private System.Func<GameObject,GameObject> malloc;
        private System.Action<GameObject> free;

        private GameObject original;
        private Queue<GameObject> pool;
        public int maxCount
        {
            get;
            private set;
        }
        public string poolName
        {
            get;
            private set;
        }

        public GameObjectPool(GameObject original,int maxCount,System.Func<GameObject,GameObject> instantiate,System.Action<GameObject> dispose)
        {
            Init(original,maxCount,instantiate,dispose);
        }

        public void Init(GameObject original, int maxCount, System.Func<GameObject,GameObject> instantiate,System.Action<GameObject> dispose)
        {
            if (original == null)
            {
                //Log
                return;
            }
            this.original = original;
            pool = new Queue<GameObject>();
            this.maxCount = maxCount;
            poolName = string.Format(Define.poolName,original.name);
            this.malloc = instantiate;
            this.free = dispose;
        }

        public void Recycle(GameObject obj,System.Action<GameObject> onRecycle)
        {
            if (obj != null)
            {
                if (onRecycle != null)
                    onRecycle(obj);
                if (pool != null && pool.Count < maxCount)
                    pool.Enqueue(obj);
                else
                {
                    if (free != null)
                        free(obj);
                }
            }
        }

        public void Recycle(GameObject[] _objs,System.Action<GameObject> onRecycle)
        {
            if (_objs != null)
                for (int i = 0, length = _objs.Length; i < length; i++)
                    Recycle(_objs[i],onRecycle);
        }

        public GameObject[] Spawn(int count,System.Action<GameObject> onSpawn)
        {
            GameObject[] ret = new GameObject[count];//TODO 修改
            for (int i = 0; i < count; i++)
                ret[i] = Spawn(onSpawn);
            return ret;
        }

        public GameObject Spawn(System.Action<GameObject> onSpawn)
        {
            GameObject ret = null;
            if (pool == null || pool.Count <= 0)
            {
                if (malloc != null && original != null)
                    ret = malloc(original);
                else
                    return null;
            }
            else
                ret = pool.Dequeue();
            if (onSpawn != null)
                onSpawn(ret);
            return ret;
        }


        public int GetItemCount()
        {
            if (pool == null)
            {
                //Log
                return 0;
            }
            return pool.Count;
        }


        public void Dispose()
        {
            while (pool.Count > 0)
            {
                if(free != null)
                    free(pool.Dequeue());
            }
        }
    }
}