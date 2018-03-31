using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeEventType
{
    /// <summary>
    /// 受TimeScale影响
    /// </summary>
    Time,

    /// <summary>
    /// 使用真实时间
    /// </summary>
    IngoreTimeScale,

    /// <summary>
    /// 使用服务器时间
    /// </summary>
    RealSeverTime
}


public class TimeEvent
{
    /// <summary>
    /// 已过去时间
    /// </summary>
    public float elapsedTime;

    /// <summary>
    /// 剩余时间
    /// </summary>
    public float surplusTime;


    public int surplusTimeRound
    {
        get
        {
            return Mathf.RoundToInt(surplusTime);
        }
    }

    public int elapsedTimeRound
    {
        get
        {
            return Mathf.RoundToInt(elapsedTime);
        }
    }

    /// <summary>
    /// 携带的消息事件
    /// </summary>
    public object msgObj;


    public int id;


    private bool mPause = false;


    private float mStartTime;

    private float mEndTime;

    /// <summary>
    /// 下次触发的时间
    /// </summary>
    private float mNextTriggerTime;

    /// <summary>
    /// 更新间隔
    /// </summary>
    private float mUpdateIntervalTime;

    /// <summary>
    /// 使用真实时间
    /// </summary>
    private bool mIgnoreTimeScale;


    private bool mHasEventCallback;

    private bool mSingle;

    private bool mInit;

    private TimeEventType mTimeEventType = TimeEventType.Time;


    private float currentTime
    {
        get
        {
            if (mTimeEventType == TimeEventType.Time)
                return Time.time;
            return Time.realtimeSinceStartup;
        }
    }


    private Action<TimeEvent> mOnCompleteCallback;
    private Action<TimeEvent> mOnUpdateCallback;
    private Action mOnEmptyCompleteCallback;
    private Action mOnEmptyUpdateCallback;


    public void Init(float callTime, int eventGlobalID, TimeEventType timeType)
    {
        mTimeEventType = timeType;
        mStartTime = currentTime;
        mEndTime = mStartTime + callTime;
        mNextTriggerTime = mEndTime;
        id = eventGlobalID;
    }


    public bool Excute()
    {
        if (!mHasEventCallback)
            return true;

        bool excuteComplete = false;
        if (mNextTriggerTime <= currentTime)
        {
            elapsedTime = currentTime - mStartTime;
            surplusTime = mEndTime - currentTime;

            if (mOnUpdateCallback != null)
                mOnUpdateCallback(this);
            if (mOnEmptyUpdateCallback != null)
                mOnEmptyUpdateCallback();
            if (mNextTriggerTime >= mEndTime)
            {
                excuteComplete = true;
                if (mOnCompleteCallback != null)
                    mOnCompleteCallback(this);
                if (mOnEmptyCompleteCallback != null)
                    mOnEmptyCompleteCallback();
            }
            else
            {
                mNextTriggerTime = mStartTime + elapsedTime + mUpdateIntervalTime;
                mNextTriggerTime = Mathf.Min(mNextTriggerTime,mEndTime);
            }
        }
        return excuteComplete;
    }


    public TimeEvent SetIntervalTime(float intervalCallTime, Action<TimeEvent> onUpdate = null)
    {
        mUpdateIntervalTime = intervalCallTime;
        mNextTriggerTime = mStartTime + mUpdateIntervalTime;
        mNextTriggerTime = Mathf.Min(mNextTriggerTime,mEndTime);
        mOnUpdateCallback = onUpdate;
        mHasEventCallback = true;
        return this;
    }


    public TimeEvent SetPause(bool pauseState)
    {
        mPause = pauseState;
        return this;
    }


    public TimeEvent OnUpdate(Action<TimeEvent> onUpdate)
    {
        mHasEventCallback = true;
        mOnUpdateCallback = onUpdate;
        return this;
    }


    public TimeEvent OnUpdate(Action onUpdate)
    {
        mHasEventCallback = true;
        mOnEmptyUpdateCallback = onUpdate;
        return this;
    }


    public TimeEvent OnComplete(Action<TimeEvent> onComplete)
    {
        mHasEventCallback = true;
        mOnCompleteCallback = onComplete;
        return this;
    }


    public TimeEvent OnComplete(Action onComplete)
    {
        mHasEventCallback = true;
        mOnEmptyCompleteCallback = onComplete;
        return this;
    }


    public TimeEvent Single()
    {
        mSingle = true;
        if (!mHasEventCallback)
            Debug.LogError("请在设置完回调函数再使用Single功能");
        return this;
    }


    public TimeEvent Delay()
    {
        mSingle = true;
        if (!mHasEventCallback)
            Debug.LogError("");
        return this;
    }


    public bool CallbackEqual(TimeEvent targetTimeEvent)
    {
        if (mOnCompleteCallback == targetTimeEvent.mOnCompleteCallback || mOnEmptyCompleteCallback == targetTimeEvent.mOnEmptyCompleteCallback)
            return true;
        return false;
    }


    public TimeEvent Start()
    {
        mNextTriggerTime = mStartTime;
        mNextTriggerTime = Mathf.Min(mNextTriggerTime,mEndTime);
        Excute();
        return this;
    }


    public void Reset()
    {
        mInit = false;
        id = 0;
        mStartTime = 0;
        mEndTime = 0;
        mPause = false;
        mUpdateIntervalTime = 0;
        mHasEventCallback = false;
        mOnEmptyCompleteCallback = null;
        mOnEmptyUpdateCallback = null;
        mOnUpdateCallback = null;
    }
}

public class StartTimer : MonoBehaviour
{
    private static StartTimer _instance;
    public static StartTimer Instance
    {
        get
        {
            return _instance ?? (_instance = FindObjectOfType<StartTimer>());
        }
        set
        {
            _instance = value;
        }
    }

    //private readonly 
}
