using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Pool
{
    public class PoolManager : AChun.IManager
    {
        private Dictionary<string, GameObjectPool> gameObjectPool_map;
        private Dictionary<string, ObjectPool> objectPool_map;

        public PoolManager()
        {
            Init();
        }

        public void Init()
        {
            gameObjectPool_map = new Dictionary<string, GameObjectPool>();
            objectPool_map = new Dictionary<string, ObjectPool>();
        }

        public void OnApplicationPause()
        {
            
        }

        public void OnApplicationQuit()
        {

        }

        public void OnDestroy()
        {
            Dispose();
        }

        public void Update()
        {
            
        }


        public void Dispose()
        {
            foreach (GameObjectPool item in gameObjectPool_map.Values)
            {
                item.Dispose();
            }
            gameObjectPool_map.Clear();
            gameObjectPool_map = null;

            foreach (ObjectPool item in objectPool_map.Values)
            {
                item.Dispose();
            }
            objectPool_map.Clear();
            objectPool_map = null;
        }


        #region GameObjectInterface


        public void RegisterGameObjectPool(GameObject original,int maxCount,Func<GameObject,GameObject> malloc,Action<GameObject> free)
        {
            string poolName = string.Format(Define.poolName,original.name);
            if (gameObjectPool_map != null && original != null && !gameObjectPool_map.ContainsKey(poolName))
            {
                gameObjectPool_map.Add(poolName,new GameObjectPool(original,maxCount,malloc,free));
            }
        }

        public GameObject Spawn(string poolName,Action<GameObject> onSpawn)
        {
            GameObject ret = null;
            if (gameObjectPool_map.ContainsKey(poolName))
                ret = gameObjectPool_map[poolName].Spawn(onSpawn);
            else
            {
                //Log
            }
            return ret;
        }

        public GameObject Spawn(GameObject obj, Action<GameObject> onSpawn)
        {
            string poolName = string.Format(Define.poolName);
            return Spawn(poolName,onSpawn);
        }

        public GameObject[] Spawn(string poolName,int count,Action<GameObject> onSpawn)
        {
            GameObject[] ret = null;
            if (gameObjectPool_map.ContainsKey(poolName))
                ret = gameObjectPool_map[poolName].Spawn(count,onSpawn);
            else
            {
                //Log
            }
            return ret;
        }


        public GameObject[] Spawn(GameObject obj, int count, Action<GameObject> onSpawn)
        {
            string poolName = string.Format(Define.poolName,obj.name);
            return Spawn(poolName,count,onSpawn);
        }


        public void Recycle(GameObject obj,Action<GameObject> onRecycle)
        {
            if (obj == null)
                return;
            string poolName = string.Format(Define.poolName,obj.name.Replace("(Clone)",""));
            if (gameObjectPool_map.ContainsKey(poolName))
            {
                gameObjectPool_map[poolName].Recycle(obj,onRecycle);
            }
        }


        public void Recycle(GameObject[] objs, Action<GameObject> onRecycle)
        {
            if (objs == null && objs.Length == 0)
                return;
            string poolName = string.Format(Define.poolName, objs[0].name);
            if (gameObjectPool_map.ContainsKey(poolName))
            {
                gameObjectPool_map[poolName].Recycle(objs, onRecycle);
            }
        }

        #endregion


        private void Alloc()
        {
            lock (gameObjectPool_map)
            {
                foreach (GameObjectPool pool in gameObjectPool_map.Values)
                {
                    if (pool.GetItemCount() < pool.maxCount)
                    {
                        pool.Dispose();
                    }
                }
            }

            lock (objectPool_map)
            {
                foreach (ObjectPool pool in objectPool_map.Values)
                {
                    if (pool.GetItemCount() < pool.maxCount)
                    {
                        pool.Dispose();
                    }
                }
            }
        }


        #region ObjectInterface

        public void RegisterObjectPool(IPoolItem original, int maxCount, Func<IPoolItem> malloc, Action<IPoolItem> free)
        {
            string poolName = string.Format(Define.poolName, original.name);
            if (objectPool_map != null && original != null && !objectPool_map.ContainsKey(poolName))
            {
                objectPool_map.Add(poolName, new ObjectPool(original, maxCount, malloc, free));
            }
        }

        public IPoolItem Spawn(string poolName, Action<IPoolItem> onSpawn)
        {
            IPoolItem ret = null;
            if (objectPool_map.ContainsKey(poolName))
                ret = objectPool_map[poolName].Spawn(onSpawn);
            else
            {
                //Log
            }
            return ret;
        }

        public IPoolItem Spawn(IPoolItem obj, Action<IPoolItem> onSpawn)
        {
            string poolName = string.Format(Define.poolName);
            return Spawn(poolName, onSpawn);
        }

        public IPoolItem[] Spawn(string poolName, int count, Action<IPoolItem> onSpawn)
        {
            IPoolItem[] ret = null;
            if (objectPool_map.ContainsKey(poolName))
                ret = objectPool_map[poolName].Spawn(count, onSpawn);
            else
            {
                //Log
            }
            return ret;
        }


        public IPoolItem[] Spawn(IPoolItem obj, int count, Action<IPoolItem> onSpawn)
        {
            string poolName = string.Format(Define.poolName, obj.name);
            return Spawn(poolName, count, onSpawn);
        }


        public void Recycle(IPoolItem obj, Action<IPoolItem> onRecycle)
        {
            if (obj == null)
                return;
            string poolName = string.Format(Define.poolName, obj.name);
            if (objectPool_map.ContainsKey(poolName))
            {
                objectPool_map[poolName].Recycle(obj, onRecycle);
            }
        }


        public void Recycle(IPoolItem[] objs, Action<IPoolItem> onRecycle)
        {
            if (objs == null && objs.Length == 0)
                return;
            string poolName = string.Format(Define.poolName, objs[0].name);
            if (objectPool_map.ContainsKey(poolName))
            {
                objectPool_map[poolName].Recycle(objs, onRecycle);
            }
        }
        #endregion
    }
}