using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Pool
{
    public class ObjectPool
    {
        private System.Func<IPoolItem> malloc;
        private System.Action<IPoolItem> free;

        private IPoolItem poolItem;
        private Queue<IPoolItem> pool;
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

        public ObjectPool(IPoolItem poolItem, int maxCount, System.Func<IPoolItem> malloc, System.Action<IPoolItem> free)
        {
            Init(poolItem, maxCount, malloc, free);
        }

        public void Init(IPoolItem poolItem, int maxCount, System.Func<IPoolItem> malloc, System.Action<IPoolItem> free)
        {
            if (poolItem == null)
            {
                //Log
                return;
            }
            this.poolItem = poolItem;
            pool = new Queue<IPoolItem>();
            this.maxCount = maxCount;
            poolName = string.Format(Define.poolName, poolItem.GetItemName());
            this.malloc = malloc;
            this.free = free;
        }

        public void Recycle(IPoolItem obj, System.Action<IPoolItem> onRecycle)
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

        public void Recycle(IPoolItem[] _objs, System.Action<IPoolItem> onRecycle)
        {
            if (_objs != null)
                for (int i = 0, length = _objs.Length; i < length; i++)
                    Recycle(_objs[i], onRecycle);
        }

        public IPoolItem[] Spawn(int count, System.Action<IPoolItem> onSpawn)
        {
            IPoolItem[] ret = new IPoolItem[count];//TODO 修改
            for (int i = 0; i < count; i++)
                ret[i] = Spawn(onSpawn);
            return ret;
        }

        public IPoolItem Spawn(System.Action<IPoolItem> onSpawn)
        {
            IPoolItem ret = null;
            if (pool == null || pool.Count <= 0)
            {
                if (malloc != null)
                    ret = malloc();
                ret = null;
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
                if (free != null)
                {
                    free(pool.Dequeue());
                }
            }
        }
    }
}