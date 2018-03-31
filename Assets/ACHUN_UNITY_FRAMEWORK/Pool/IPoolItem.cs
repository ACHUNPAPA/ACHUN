using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChun.Pool
{
    public interface IPoolItem
    {
        string name
        {
            get;
            set;
        }

        string GetItemName();
    }
}