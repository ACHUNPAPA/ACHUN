using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AChun.Net
{
    public class JsonEncodedEventMessage
    {
        public string name
        {
            get;
            set;
        }


        public object[] args
        {
            get;
            set;
        }


        public JsonEncodedEventMessage()
        {

        }

        public JsonEncodedEventMessage(string name, object[] args)
        {
            this.name = name;
            this.args = args;
        }

        public JsonEncodedEventMessage(string name, object arg) : this(name, new object[] { arg })
        {

        }


        public IEnumerable<T> GetArgsAs<T>()
        {
            List<T> items = new List<T>();
            foreach (var i in args)
            {
                items.Add(JsonUtility.FromJson<T>(i.ToString()));
            }
            return items.AsEnumerable();
        }


        public string ToJsonString()
        {
            return JsonUtility.ToJson(this);
        }


        public static JsonEncodedEventMessage Deserialize(string jsonString)
        {
            JsonEncodedEventMessage msg = null;
            try
            {
                msg = JsonUtility.FromJson<JsonEncodedEventMessage>(jsonString);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
            return msg;
        }
    }
}
