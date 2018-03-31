using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

public class UDPConnector
{
    private static UDPConnector _instance;
    public static UDPConnector Instance
    {
        get
        {
            if (_instance == null)
                _instance = new UDPConnector();
            return _instance;
        }
    }

    int bufferSize = 4096;
    UdpClient connector;
    Action<byte[], IPEndPoint, int> delReceive;


    public void Init(Action<byte[],IPEndPoint,int> receiveDel,int port)
    {
        delReceive = receiveDel;
        OnBind(port);
    }


    private void OnBind(int port)
    {
        connector = new UdpClient(port,AddressFamily.InterNetwork);
        OnStartReceive(connector);
        OnStartToSend();
    }


    private void OnStartToSend()
    {
        new Thread(new ThreadStart(DoSend)).Start();
    }


    public static bool isQuit = false;


    private void DoSend()
    {
        while (true)
        {
            if (isQuit)
                break;
            lock (sendLock)
            {
                List<int> remove = new List<int>();
                //foreach (var pv in packetsToSendPool)
                //{
                    //var p = pv.Value;
                    //if(p)
                //}
            }
        }
    }


    public void OnStartReceive(UdpClient s)
    {
        if (s == null)
            s = connector;
        byte[] bs = new byte[bufferSize];
        s.BeginReceive(OnReceived,new object());
    }


    private void OnReceived(IAsyncResult ar)
    {
        object[] objs = ar.AsyncState as object[];
        UdpClient s = objs[0] as UdpClient;
        IPEndPoint sender = new IPEndPoint(IPAddress.Any,0);
        byte[] bs = s.EndReceive(ar,ref sender);
        int buffer = bs.Length;
        if (buffer == 0)
        {
            if (delReceive != null)
                delReceive(null, sender, 0);
        }
        else
        {
            if (delReceive != null)
                lock (sendLock)
                {
                    int packetMetaID = BitConverter.ToInt32(bs,0);
                    int packetID = BitConverter.ToInt32(bs,4);

                    int totalSec = BitConverter.ToInt32(bs,8);
                    if (totalSec == 0)
                    {
                        int def = BitConverter.ToInt32(bs,16);
                        byte[] packetBuff = new byte[bs.Length - 20];
                        Array.Copy(bs,20,packetBuff,0,packetBuff.Length);
                    }
                }
        }
    }


    private object sendLock = new object();
}
