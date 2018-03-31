using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MsgDistribution
{
    public int num = 15;
    public List<BaseProtocol> msgList = new List<BaseProtocol>();
    public delegate void Delegate(BaseProtocol proto);
    private Dictionary<string, Delegate> eventDict = new Dictionary<string, Delegate>();
    private Dictionary<string, Delegate> onceDict = new Dictionary<string, Delegate>();


    public void Update()
    {
        for (int i = 0; i < num; i++)
        {
            if (msgList.Count > 0)
            {
                lock (msgList)
                {
                    msgList.RemoveAt(0);
                }
            }
            else
                break;
        }
    }


    public void DispatchMsgEvent(BaseProtocol protocol)
    {
        string protocolID = protocol.GetProtocolID();
        if (eventDict.ContainsKey(protocolID))
            eventDict[protocolID](protocol);

        if (onceDict.ContainsKey(protocolID))
        {
            onceDict[protocolID](protocol);
            onceDict[protocolID] = null;
            onceDict.Remove(protocolID);
        }        
    }


    public void AddListener(string name, Delegate callback)
    {
        if (eventDict.ContainsKey(name))
            eventDict[name] += callback;
        else
            eventDict.Add(name,callback);
    }


    public void AddOnceListener(string name, Delegate callback)
    {
        if (onceDict.ContainsKey(name))
            onceDict[name] += callback;
        else
            onceDict.Add(name,callback);
    }


    public void DelListener(string name, Delegate callback)
    {
        if (eventDict.ContainsKey(name))
        {
            eventDict[name] -= callback;
            if (eventDict[name] == null)
                eventDict.Remove(name);
        }
    }


    public void DelOnceListener(string name, Delegate callback)
    {
        if (onceDict.ContainsKey(name))
        {
            onceDict[name] -= callback;
            if (onceDict[name] == null)
                onceDict.Remove(name);
        }
    }
}
