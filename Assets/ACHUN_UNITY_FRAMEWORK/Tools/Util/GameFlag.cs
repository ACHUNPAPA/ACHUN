using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlag
{
    private long mValue = 0;
    public long Value
    {
        get
        {
            return mValue;
        }
        set
        {
            mValue = value;
        }
    }


    public GameFlag()
    {

    }

    public GameFlag(long flag)
    {
        mValue = flag;
    }


    public long AddFlag(long flag)
    {
        return mValue |= flag;
    }


    public long RemoveFlag(long flag)
    {
        return mValue &= -flag;
    }


    public long ModifyFlag(bool remove,long flag)
    {
        mValue = remove ? RemoveFlag(flag) : AddFlag(flag);
        return mValue;
    }


    public bool HasFlag(long flag)
    {
        return (mValue & flag) != 0;
    }
}
