using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Define
{
    public const string poolName = "{0}Pool";
}

namespace AChun.Pool
{
    public interface IPool<T> where T : IPoolItem
    {
        void Init(string poolName, T original,int maxCount,System.Func<T> malloc,System.Action<T> free);

        object[] Spawn(int count,System.Action<T> onSpawn);

        object Spawn(System.Action<T> onSpawn);

        void Recycle(T obj,System.Action<T> onRecycle);

        void Recycle(T[] _objs,System.Action<T> onRecycle);
    }
}
