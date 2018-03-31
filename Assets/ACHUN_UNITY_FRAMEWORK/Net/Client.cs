using SocketIOClient.Eventing;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using WebSocket4Net;

namespace AChun.Net
{
    public class Client : IDisposable,IClient
    {
        private Timer socketHeartBeatTimer;
        private Thread dequeueOutBoundMsgTask;
        private ConcurrentQueue<string> outboundQueue;
        private int retryConnectionCount = 0;
        private int retryConnectionAttempts = 3;
        private readonly static object padLock = new object();

        protected Uri uri;
        protected WebSocket wsClient;
        protected RegistrationManager registrationManager;
        protected WebSocketVersion socketVersion = WebSocketVersion.Rfc6455;

        public SocketIOHandshake HandShake
        {
            get;
            private set;
        }

        public bool IsConnected
        {
            get
            {
                return ReadyState == WebSocketState.Open;
            }
        }

        public WebSocketState ReadyState
        {
            get
            {
                if (wsClient != null)
                    return wsClient.State;
                else
                    return WebSocketState.None;
            }
        }

        public event EventHandler Opened;
        public event EventHandler<MessageEventArgs> Message;
        public event EventHandler ConnectionRetryAttempt;
        public event EventHandler HeartBeatTimerEvent;
        public event EventHandler SocketConnectionClosed;
        public event EventHandler<ErrorMessageArgs> Error;
        public ManualResetEvent MessageQueueEmptyEvent = new ManualResetEvent(true);
        public ManualResetEvent ConnectionOpenEvent = new ManualResetEvent(false);

        public int RetryConnectionAttempts
        {
            get
            {
                return retryConnectionAttempts;
            }
            set
            {
                retryConnectionAttempts = value;
            }
        }


        public string lastErrorMessage = "";

        public Client(string url) : this(url,WebSocketVersion.Rfc6455)
        {

        }

        public Client(string url,WebSocketVersion socketVersion)
        {
            uri = new Uri(url);
            this.socketVersion = socketVersion;
            registrationManager = new RegistrationManager();
            outboundQueue = new ConcurrentQueue<string>();
            //dequeueOutBoundMsgTask = new Thread(new ThreadStart(dequeueOutBoundMsgTask));
            //dequeueOutBoundMsgTask.Start();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            lock (padLock)
            {
                if (!(ReadyState == WebSocketState.Connecting || ReadyState == WebSocketState.Open))
                {
                    //try
                    //{
                    //    ConnectionOpenEvent.Reset();
                    //    HandShake = requestHandshake(uri);

                    //    if (HandShake == null || string.IsNullOrEmpty(HandShake.SID))
                    //    {
                    //        lastErrorMessage = string.Format("", uri.ToString());
                    //        OnErrorEvent(this, new ErrorEventArgs(lastErrorMessage, new Exception()));
                    //    }
                    //    else
                    //    {
                    //        string wsScheme = (uri.Scheme == uri.);
                    //    }
                    //}
                    //catch(Exception ex)
                    //{
                    //    Trace.WriteLine(string.Format("",ex.Message));
                    //    OnErrorEvent(this,new ErrorEventArgs("SocketIO.Client.Connect threw an exception", ex));
                    //}
                }
            }
        }

        public IEndPointClient Connect(string endPoint)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Emit(string eventName, object payload)
        {
            throw new NotImplementedException();
        }

        public void Emit(string eventName, object payload, string endPoint, Action<object> callback)
        {
            throw new NotImplementedException();
        }

        public void On(string eventName, Action<IMessage> action)
        {
            throw new NotImplementedException();
        }

        public void On(string eventName, string endPoint, Action<IMessage> action)
        {
            throw new NotImplementedException();
        }

        public void Send(IMessage msg)
        {
            throw new NotImplementedException();
        }


        protected void dequeuOutboundMessages()
        {
            while (outboundQueue != null)
            {
                if (ReadyState == WebSocketState.Open)
                {
                    string msgString;
                    try
                    {
                        if (outboundQueue.TryDequeue(out msgString))
                            wsClient.Send(msgString);
                        else
                            MessageQueueEmptyEvent.Set();
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine("");
                    }
                }
                else
                {
                    ConnectionOpenEvent.WaitOne(2000);
                }
            }
        }
    }
}