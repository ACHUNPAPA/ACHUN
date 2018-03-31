using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChun.Net
{
    public class SocketIOHandshake
    {
        /// <summary>
        /// 
        /// </summary>
        public string SID
        {
            get;
            set;
        }

        /// <summary>
        /// 心跳包间隔时间
        /// </summary>
        public int HeartBeatTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 是否有错
        /// </summary>
        public bool HadError
        {
            get
            {
                return !string.IsNullOrEmpty(ErrorMessage);
            }
        }

        /// <summary>
        /// 心跳包间隔
        /// </summary>
        public TimeSpan HeartBeatInterval
        {
            get
            {
                return new TimeSpan(0, 0, HeartBeatTimeout);
            }
        }

        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectionTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> Transports = new List<string>();

        public SocketIOHandshake()
        {

        }

        public static SocketIOHandshake LoadFromString(string value)
        {
            SocketIOHandshake returnItem = new SocketIOHandshake();
            if (!string.IsNullOrEmpty(value))
            {
                string[] items = value.Split(new char[] { ':' });
                if (items.Count() == 4)
                {
                    int hb = 0;
                    int ct = 0;
                    returnItem.SID = items[0];

                    if (int.TryParse(items[1], out hb))
                    {
                        var pct = (int)(hb * .75);
                        returnItem.HeartBeatTimeout = pct;
                    }
                    if (int.TryParse(items[2], out ct))
                        returnItem.ConnectionTimeout = ct;
                    returnItem.Transports.AddRange(items[3].Split(new char[] { ',' }));
                    return returnItem;
                }
            }

            return null;
        }
    }
}