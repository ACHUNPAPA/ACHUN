using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Common
{
    public abstract class Singleton<T> where T : Singleton<T>,new()
    {
        protected T _Instance;
        public T Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new T();
                return _Instance;
            }
        }

        protected Singleton()
        {
            Init();
        }


        protected abstract void Init();
    }
}