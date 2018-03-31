//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading;
//using System.Timers;
//using UnityEngine;

//public class PING
//{
//    private string Ping = "ping";
//    public int nowTime;

//    public PING()
//    {
//        nowTime = 0;
//    }

//    public byte[] ToBytes()
//    {
//        byte[] bytes = new byte[8];
//        int index = 0;
//        return bytes;
//    }
//}


//public class OnMessageEventArgs : EventArgs
//{
//    public readonly uint msgID;
//    public readonly byte[] buff;

//    public OnMessageEventArgs(uint msgID, byte[] buff)
//    {
//        this.msgID = (ushort)msgID;
//        this.buff = buff;
//    }
//}

//public class TCPNet
//{
//    public static System.Timers.Timer wTimer;
//    public static System.Timers.Timer bTimer;

//    #region 基本信息
//    public string IP = "";
//    public int port = 0;
//    public int pingValue = 0;
//    private PING Ping = new PING();
//    private byte[] PingBuff = new byte[8];

//    public int a = 0;
//    public uint ping = 0;
//    public uint maxPing = 0;
//    public uint minPing = 9999;
//    public uint lastPing = 0;
//    #endregion


//    #region 消息相关
//    private IPEndPoint serverInfo;
//    public Socket clientSocket;

//    private byte[] RecvBuffTemp = new byte[RECV_BUFLEN];
//    private int recvBuffLength = 0;
//    private const int RECV_BUFLEN = 0x20000;
//    private const int DECODE_BUFLEN = 0x80000;

//    /// <summary>
//    /// 当前处理数据消息数据包状态
//    /// 1、数据包头
//    /// 2、数据包体
//    /// </summary>
//    private int bufferState = 0;

//    /// <summary>
//    /// 当前处理长度，通过包头获得
//    /// </summary>
//    private int thisLen = 0;

//    /// <summary>
//    /// 服务器发来的数据包头大小
//    /// </summary>
//    private int headSize = 4;
//    private byte[] headBuff = new byte[4];

//    /// <summary>
//    /// 当前处理数据包位置
//    /// </summary>
//    private int curBuffLength = 0;

//    /// <summary>
//    /// 解压工具
//    /// </summary>
//    private LZW lzw = new LZW();

//    public List<int> NeverSendPro = new List<int>();
//    #endregion

//    #region 变量设置
//    private uint sendIndex = 0;

//    /// <summary>
//    /// 超时：毫秒
//    /// </summary>
//    private int timeOut = 2000;
//    #endregion

//    private static ManualResetEvent connextDone = new ManualResetEvent(false);
//    private static ManualResetEvent sendDone = new ManualResetEvent(false);
//    private static ManualResetEvent receiveDone = new ManualResetEvent(false);
//    private Thread recvThread;

//    #region 接收消息事件
//    #region 事件定义
//    #region 任何2C的消息都会触发此事件
//    public event OnMessageEventHandler OnMessage;
//    #endregion
//    #endregion

//    public delegate void OnMessageEventHandler(OnMessageEventArgs e);

//    public virtual void GetMessage(OnMessageEventArgs e)
//    {
//        if (OnMessage == null)
//            OnMessage += new OnMessageEventHandler(GameNet_OnMessage);
//        if (OnMessage != null)
//            OnMessage(e);
//    }


//    public void GameNet_OnMessage(OnMessageEventArgs e)
//    {
//        switch (e.msgID)
//        {
//            default:
//                break;
//        }
//    }
//    #endregion

//    #region 初始化
//    public void Clear()
//    {
//        ping = 0;
//        maxPing = 0;
//        minPing = 9999;
//        lastPing = 0;
//        bufferState = 0;
//        thisLen = 0;
//        headSize = 4;
//        curBuffLength = 0;
//        recvBuffLength = 0;
//        sendIndex = 0;
//        recvThread = null;
//        RecvBuffTemp = new byte[RECV_BUFLEN];
//        connextDone = new ManualResetEvent(false);
//    }

//    public TCPNet()
//    {
//        Init();
//    }

//    private void Init()
//    {
//        //定义IPV4，TCP模式的socket
//        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//        wTimer = new System.Timers.Timer(1000);
//        bTimer = new System.Timers.Timer(1000);
//        NeverSendPro.Clear();

//    }
//    #endregion

//    #region 连接 & 断开
//    /// <summary>
//    /// 断开连接
//    /// </summary>
//    public void Disconnect()
//    {
//        if (recvThread != null)
//        {
//            if (recvThread.IsAlive)
//            {
//                recvThread.Abort();
//                recvThread = null;
//            }
//        }
//        if (clientSocket == null)
//            return;
//        sendIndex = 0;
//        if (clientSocket.Connected)
//        {
//            //禁用发送和接收
//            clientSocket.Shutdown(SocketShutdown.Both);
//            clientSocket.Disconnect(false);
//        }
//        clientSocket.Close();
//    }

//    /// <summary>
//    /// 断开连接：退出
//    /// </summary>
//    public void Quit()
//    {
//        if (recvThread != null)
//        {
//            if (recvThread.IsAlive)
//            {
//                recvThread.Abort();
//            }
//        }
//        if (clientSocket == null)
//            return;
//        sendIndex = 0;
//        if (clientSocket.Connected)
//        {
//            try
//            {
//                //禁用发送和接收
//                clientSocket.Shutdown(SocketShutdown.Both);
//                //关闭socket，不允许重用
//            }
//            catch (Exception e)
//            {

//            }
//            clientSocket.Disconnect(false);
//        }
//        clientSocket.Close();
//        clientSocket = null;
//    }


//    public bool IsConnect()
//    {
//        if (clientSocket != null)
//            return clientSocket.Connected;
//        return false;
//    }


//    public bool Connect(string IP, int port)
//    {
//        this.IP = IP;
//        this.port = port;
//        this.IP = DnsHelper.GetHostIP(IP);
//        return Connect();
//    }

//    private bool Connect()
//    {
//        connextDone.Reset();
//        sendDone.Reset();
//        receiveDone.Reset();

//        try
//        {
//            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            IP = DNSHelper.GetHostIP(IP);
//            serverInfo = new IPEndPoint(IPAddress.Parse(IP), port);
//            clientSocket.BeginConnect(serverInfo, new AsyncCallback(ConnectCallback), clientSocket);

//            //禁用Nagle算法
//            clientSocket.NoDelay = true;
//            clientSocket.ReceiveBufferSize = 65532;
//            clientSocket.SendBufferSize = 65532;
//            if (connextDone.WaitOne(timeOut, false))
//            {
//                if (IsConnect())
//                {
//                    recvThread = new Thread(RecviveTest);
//                    recvThread.Start();
//                    return true;
//                }
//                else
//                    return false;
//            }
//        }
//        catch (Exception e)
//        {
//            if (recvThread != null)
//                recvThread.Abort();
//        }
//        return false;
//    }

//    private void ConnectCallback(IAsyncResult ar)
//    {
//        try
//        {
//            clientSocket.EndConnect(ar);
//            connextDone.Set();
//        }
//        catch (Exception e)
//        {

//        }
//    }
//    #endregion

//    #region 接收
//    private void RecviveTest()
//    {
//        try
//        {
//            while (true)
//            {
//                if (!clientSocket.Connected || clientSocket == null)
//                {
//                    continue;
//                }
//                if (recvBuffLength > RecvBuffTemp.Length)
//                    return;

//                long time = DateTime.Now.Ticks;
//                int bytesRead = clientSocket.Receive(RecvBuffTemp, recvBuffLength, RecvBuffTemp.Length - recvBuffLength, 0);
//                if (bytesRead > 0)
//                    HandleBuffer(bytesRead);
//            }
//        }
//        catch (Exception e)
//        {

//        }
//    }

//    /// <summary>
//    /// 数据是否被压缩
//    /// </summary>
//    bool isEncoded = false;

//    private void HandleBuffer(int curLength)
//    {
//        if (curLength > 0)
//            recvBuffLength += curLength;
//        if (bufferState == 0)
//        {
//            if (bufferState > recvBuffLength)//包头未收完
//            {
//                return;
//            }
//            Array.Copy(RecvBuffTemp, 0, headBuff, 0, headSize);
//            isEncoded = HandleHead(lzw, out thisLen);
//            bufferState = 1;
//            HandleBuffer(0);
//        }
//        if (bufferState == 1)//包体
//        {
//            if (thisLen == 0)
//            {
//                bufferState = 0;
//                recvBuffLength = 0;
//            }
//            if (thisLen + headSize > recvBuffLength)//包体数据不完整
//                return;
//            else
//            {
//                byte[] msgBuffer = new byte[thisLen];
//                Array.Copy(RecvBuffTemp, headSize, msgBuffer, 0, thisLen);
//                HandleMsg(lzw, msgBuffer);
//                bufferState = 0;

//                if (thisLen + headSize == recvBuffLength)
//                {
//                    recvBuffLength = 0;
//                    return;
//                }
//                else if (thisLen + headSize < recvBuffLength)
//                {
//                    int tempLen = recvBuffLength - thisLen - headSize;
//                    Array.Copy(RecvBuffTemp, headSize + thisLen, RecvBuffTemp, 0, tempLen);
//                    recvBuffLength = tempLen;
//                    HandleBuffer(0);
//                }
//            }
//        }
//    }

//    private void HandleMsg(LZW lzw, byte[] msgBuffer)
//    {
//        if (isEncoded)
//        {
//            isEncoded = false;
//            lzw.Clear();
//            #region 解压
//            #endregion
//        }
//        else
//        {
//            OnReceivePacket(msgBuffer, 0, msgBuffer.Length);
//        }
//    }

//    private void OnReceivePacket(byte[] buffer, int index, int length)
//    {
//        if (buffer == null || length <= 0 || length > 0xffff)
//            return;
//        if (buffer.Length == 8)
//        {
//            if (UtilityHelper.GB2312.GetString(buffer, 0, 4) == "pong")
//            {
//                uint _ping = BitConverter.ToUInt32(buffer, 4);
//                SetPing(_ping);
//                return;
//            }
//        }

//        byte[] bufferBase = new byte[4];
//        Array.Copy(buffer, index, bufferBase, 0, 4);
//        byte[] bufferTemp = new byte[length - 4];
//        Array.Copy(buffer, index + 4, bufferTemp, 0, length - 4);
//        MsgBase msg = MSGBase.GetValueByBytes(bufferBase, 0);
//        OnMessageEventArgs msgInfo = new OnMessageEventArgs(msg.msgID, bufferTemp);
//    }


//    private void SetPing(uint _ping)
//    {
//        ping = UtilityHelper.GetNowTimeUInt32() - _ping;
//        if (ping > maxPing)
//            maxPing = ping;
//        if (ping < minPing)
//            minPing = ping;
//        lastPing = _ping;
//    }


//    private bool HandleHead(LZW decoder, out int length)
//    {
//        DNPHDR head = DNPHDR.GetValueByBytes(headBuff, 0);
//        if ((headBuff.paclen == 0) || (head.pacloen >= 0xfff0))
//        {
//            if (head.paclen == 0xffff)
//                decoder.reset();
//            length = head.seqnum;
//            isEncoded = true;
//        }
//        else
//        {
//            length = head.paclen;
//            isEncoded = false;
//        }
//        return isEncoded;
//    }
//    #endregion

//    #region 发送
//    private void Send(byte[] buff, int length, int msgID)
//    {
//        if (clientSocket == null || !clientSocket.Connected)
//            return;
//        CRYPTION pHeader = new CRYPTION();
//        int len = length;
//        pHeader.dnphdr.seqnum = (ushort)sendIndex++;
//        pHeader.dnphdr.paclen = (ushort)len;
//        ulong u64 = (ulong)DateTime.Now.Ticks;
//        uint u32 = (uint)Environment.TickCount;
//        pHeader.signature = ((u64 << 32) | u32);
//        byte[] headerBuff = pHeader.ToBytes();
//        byte[] crc = new byte[headBuff.Length - sizeof(uint)];
//        Array.Copy(headBuff, sizeof(uint), crc, 0, crc.Length);
//        pHeader.crc32 = crc.GetCrc32(crc, (uint)crc.Length);
//        headBuff = pHeader.ToBytes();
//        MyDES.SecondEncryptDES(ref headBuff, 16);
//        byte[] res = new byte[buff.Length + headBuff.Length];
//        Array.Copy(headBuff, 0, res, 0, headBuff.Length);
//        Array.Copy(buff, 0, res, headBuff.Length, buff.Length);
//        lock (this)
//        {
//            try
//            {
//                if (msgID == 40011)
//                    clientSocket.BeginSend(res, 0, res.Length, 0, new AsyncCallback(SendPingCallback), clientSocket);
//                else
//                    clientSocket.BeginSend(res, 0, res.Length, 0, new AsyncCallback(SendCallback), clientSocket);
//            }
//            catch (Exception e)
//            {

//            }
//        }
//    }

//    private bool isConnected = true;

//    public void Send(byte[] buffer)
//    {
//        if (!IsConnect())
//        {
//            if (isConnected)
//            {
//                //断线次数加1
//                isConnected = false;
//            }
//            int protocolID = BitConverter.ToInt32(buffer, 0);

//            if (NeverSendPro.Contains(protocolID))
//                ;//结算场景未发协议重发;
//            //if()
//            return;
//        }
//        else if (IsConnect())
//            isConnected = true;

//        int msgID = BitConverter.ToInt32(buffer, 0);
//        MyDES.EncryptDES(ref buffer);
//        Send(buffer, buffer.Length, msgID);
//    }

//    private void SendCallback(IAsyncResult ar)
//    {
//        try
//        {
//            SocketError err;
//            int bytesSent = clientSocket.EndSend(ar, out err);
//            sendDone.Set();
//        }
//        catch (Exception e)
//        { }
//    }
//    #endregion

//    #region 状态变量
//    public bool IsWaitForEnterRoom = false;
//    #endregion
//}
