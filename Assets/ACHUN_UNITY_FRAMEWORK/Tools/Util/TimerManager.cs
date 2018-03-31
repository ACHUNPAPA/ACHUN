using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public delegate void Interval();

    public Dictionary<Interval, float> mIntervalDic = new Dictionary<Interval, float>();

    public void AddIntercal(Interval interval,float time)
    {
        if (mIntervalDic.ContainsKey(interval))
        {
            mIntervalDic[interval] = time;
        }
        else
        {
            mIntervalDic.Add(interval,time);
        }
    }

    public void Remove(Interval interval)
    {
        if (!mIntervalDic.ContainsKey(interval))
            mIntervalDic.Remove(interval);
    }

    private void Update()
    {
        if (mIntervalDic.Count > 0)
        {
            List<Interval> intervals = new List<Interval>();
            foreach (var item in mIntervalDic)
            {
                if (item.Value <= Time.time)
                {
                    intervals.Add(item.Key);
                }
            }
            for (int i = 0; i < intervals.Count; i++)
            {
                intervals[i]();
                Remove(intervals[i]);
            }
        }
    }
}
